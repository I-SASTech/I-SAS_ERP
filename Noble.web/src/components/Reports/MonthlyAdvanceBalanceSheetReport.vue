<template>
    <div class="row" v-if="isValid('CanViewBalanceSheetReport')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Advance Balance Sheet</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{ $t("BalanceSheetReport.Home") }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">Advance Balance Sheet</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc()" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="far fa-file-excel font-14"></i>
                                    {{ $t("BalanceSheetReport.ExportCsv") }}
                                </a>

                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t("Categories.Close") }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row align-items-end">
                <div class=" col-lg-4   form-group">
                    <label>Select a Period:</label>
                    <div class="d-flex">
                        <multiselect v-model="reportOpt" :disabled="disablePeriod" :key="disablePeriodRender"
                            :options="['Today', 'This Week', 'This Month', 'This Quarter', 'This Year', 'Yesterday', 'Previous Week', 'Previous Month', 'Previous Quarter', 'Previous Year', 'Custom']"
                            :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetDateTime()">
                        </multiselect>
                        <a class="btn btn-soft-primary mx-2" v-on:click="AdvanceFilters()" id="button-addon2"
                            value="Advance Filter">
                            <i class="fa fa-filter"></i>
                        </a>
                    </div>
                </div>
                <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
                <div class=" col-lg-4  form-group">
                    <button v-if="(reportOpt == '' || reportOpt == null) && (numberOfPeriods == 0)" disabled
                        href="javascript:void(0);" class="btn btn-outline-primary me-2">
                        Apply Filters
                    </button>
                    <button v-else v-on:click="getData()" href="javascript:void(0);" class="btn btn-outline-primary me-2">
                        Apply Filters
                    </button>
                    <a v-on:click="RemoveFilters()" href="javascript:void(0);" class="btn btn-outline-danger">
                        Clear Filters
                    </a>
                </div>
            </div>
            <div class="row">
                <div class=" col-lg-4   form-group" v-if="showDates">
                    <label>From Date:</label>
                    <datepicker v-model="fromDate" :period="true" :key="rander" />
                </div>
                <div class=" col-lg-4   form-group" v-if="showDates">
                    <label>To Date:</label>
                    <datepicker v-model="toDate" :period="true" :key="rander" />
                </div>
                <div class=" col-lg-4   form-group" v-if="advanceFilters">
                    <label>Compare With:</label>
                    <multiselect v-model="compareWith"
                        :options="['Previous Year(s)', 'Previous Period(s)', 'Previous Quarter(s)', 'Previous Month(s)']"
                        :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetPeriods()">
                    </multiselect>
                    <div class="mt-1 d-flex align-items-center">
                        <input type="checkbox" class="me-1"><span>Arrange period/year from latest to oldest</span>
                    </div>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousYear && advanceFilters">
                    <label>Number of Year(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4', '5']" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousPeriod && advanceFilters">
                    <label>Number of Period(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="financialYears" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousQuarter && advanceFilters">
                    <label>Number of Quarter(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4']" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousMonth && advanceFilters">
                    <label>Number of Month(s)</label>
                    <multiselect v-model="numberOfPeriods"
                        :options="['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']" :show-labels="false"
                        v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>


            </div>
            <hr class="hr-dashed hr-menu mt-0" />
           <div class="row">
                <div class="col-md-12 d-flex border" v-if="showComparisonTable">
                    <div class="card col-md-3 border-0">
                        <div class="card-body border-0">
                            <div class="col-lg-12">
                                <div class="table-responsive  " v-for="item in comparisonList1"
                                    :key="item.compareWithPeriodName">
                                    <h6 class="opac">{{ item.compareWithPeriodName }}</h6>
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <h4>{{ $t('BalanceSheetReport.Assets') }}</h4>
                                            </tr>
                                            <tr class="table_list_bg">
                                                <th style="width: 80%;">{{ $t('BalanceSheetReport.Account') }}</th>
                                                <th style="width: 20%;">{{ $t(' Code') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                <td style="width: 80%;">
                                                    <span>{{ asset.costCenter }}</span>
                                                </td>
                                                <td style="width: 20%;">{{ asset.code }}</td>
                                                <hr />
                                            </tr>
                                            <tr>
                                                <td><b>{{ $t('BalanceSheetReport.Total') }}</b></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <h4>{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                            </tr>
                                            <tr class="table_list_bg">
                                                <th>{{ $t('BalanceSheetReport.Account') }}</th>
                                                <th>{{ $t(' Code') }}</th>
                                            </tr>

                                        </thead>
                                        <tbody>
                                            <tr v-for="(liability) in item.liabilities " v-bind:key="liability.Id">

                                                <td>
                                                    <span>{{ liability.costCenter }}</span>
                                                </td>
                                                <td>{{ liability.code }}</td>
                                                <hr />
                                            </tr>

                                            <tr>
                                                <td><b>{{ $t('BalanceSheetReport.Total') }}</b></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <h4>{{ $t('BalanceSheetReport.Equity') }}</h4>
                                            </tr>
                                            <tr class="table_list_bg">
                                                <th>{{ $t('BalanceSheetReport.Account') }}</th>
                                                <th>{{ $t(' Code') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <tr v-for="(equity) in item.equity " v-bind:key="equity.Id">


                                                <td>
                                                    <span>{{ equity.costCenter }}</span>
                                                </td>
                                                <td>{{ equity.code }}</td>
                                                <hr />
                                            </tr>
                                            <tr>
                                                <td>
                                                    Current Earnings
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td><b>{{ $t('Total Equity') }}</b></td>
                                                <td></td>
                                            </tr>
                                            <tr>

                                            </tr>
                                            <tr>
                                                <td><b>{{ $t('BalanceSheetReport.Total') }} {{
                                                    $t('BalanceSheetReport.EquityandLiabilities') }}</b>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td><b>{{ $t('BalanceSheetReport.Total') }} {{ $t('BalanceSheetReport.Assets')
                                                }}</b></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9 d-flex bxr" ref="scrollable" @pointerdown="onPointerDown" @pointerup="onPointerUp">
                        <div class="card col-md-3 border-0 pointers" v-if="comparisonList1.length > 0">
                            <div class="card-body border-0">
                                <div>
                                    <div class="table-responsive " v-for="item in comparisonList1"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Assets') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities " v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>
                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity " v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">({{
                                                                    Number(Math.abs(parseFloat(item.netAmount +
                                                                        item.totalEquities).toFixed(2))).toLocaleString()
                                                                }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>

                                                        <b>{{
                                                            Number(Math.abs(parseFloat((item.netAmount + item.totalEquities) +
                                                                item.totalLiabilities).toFixed(2))).toLocaleString()
                                                        }}</b>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers" v-if="comparisonList2.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList2"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers" v-if="comparisonList3.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList3"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers" v-if="comparisonList4.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList4"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers" v-if="comparisonList5.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList5"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers" v-if="comparisonList6.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList6"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers " v-if="comparisonList7.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList7"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers " v-if="comparisonList8.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList8"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers " v-if="comparisonList9.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList9"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers " v-if="comparisonList10.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList10"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers " v-if="comparisonList11.length > 0">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList11"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-3 border-0 pointers " v-if="comparisonList12.length > 0" 
                            style="margin-right: 155px;">
                            <div class="card-body border-0">
                                <div>

                                    <div class="table-responsive  " v-for="item in comparisonList12"
                                        :key="item.compareWithPeriodName">
                                        <h6>{{ item.compareWithPeriodName }}</h6>
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">Assets</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(asset) in item.assets" v-bind:key="asset.Id">
                                                    <td>
                                                        <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">
                                                    <th>{{ $t('Total') }}</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(liability) in item.liabilities" v-bind:key="liability.Id">
                                                    <td>
                                                        <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>

                                                <tr>

                                                    <td><b>
                                                            <span v-if="item.totalLiabilities < 0" :class="'text-danger'">({{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }})</span>
                                                            <span v-else>{{
                                                                Number(Math.abs(parseFloat(item.totalLiabilities).toFixed(2))).toLocaleString()
                                                            }}</span>
                                                        </b></td>
                                                </tr>
                                            </tbody>
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <h4 class="opac">{{ $t('BalanceSheetReport.Equity') }}</h4>
                                                </tr>
                                                <tr class="table_list_bg">

                                                    <th>{{ $t('Total') }}</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr v-for="(equity) in item.equity" v-bind:key="equity.Id">
                                                    <td>
                                                        <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }})</span>
                                                        <span v-else>{{
                                                            Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                        }}</span>
                                                    </td>
                                                    <hr />
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span v-if="item.netAmount < 0" :class="'text-danger'">
                                                            ({{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }})
                                                        </span>
                                                        <span v-else>
                                                            {{
                                                                Number(Math.abs(parseFloat(item.netAmount).toFixed(2))).toLocaleString()
                                                            }}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="(item.netAmount + item.totalEquities) < 0"
                                                                :class="'text-danger'">
                                                                ({{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }})
                                                            </span>
                                                            <span v-else>
                                                                {{ Number(Math.abs(parseFloat(item.netAmount +
                                                                    item.totalEquities).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            {{ Number(Math.abs(parseFloat((item.netAmount + item.totalEquities)
                                                                + item.totalLiabilities).toFixed(2))).toLocaleString() }}
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <span v-if="item.totalAssets < 0" :class="'text-danger'">
                                                                ({{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString()
                                                                }})
                                                            </span>
                                                            <span v-else>
                                                                {{
                                                                    Number(Math.abs(parseFloat(item.totalAssets).toFixed(2))).toLocaleString() }}
                                                            </span>
                                                        </b>
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
           </div>

            <div class="row">
                <div class="card col-md-12" v-if="showTable">
                    <div class="card-body">
                        <div class="col-lg-12">
                            <div class="table-responsive  ">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <h4>{{ $t('BalanceSheetReport.Assets') }}</h4>
                                        </tr>
                                        <tr class="table_list_bg">
                                            <th>{{ $t('BalanceSheetReport.Account') }}</th>
                                            <th>{{ $t('Account Code') }}</th>
                                            <th>{{ $t('Total') }} - ({{ fromDate }}-{{ toDate }})</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(asset) in assetsResults" v-bind:key="asset.Id">
                                            <td>
                                                <span>{{ asset.costCenter }}</span>
                                            </td>
                                            <td>{{ asset.code }}</td>
                                            <td>
                                                <span v-if="asset.amount < 0" :class="'text-danger'">({{
                                                    Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                }})</span>
                                                <span v-else>{{
                                                    Number(Math.abs(parseFloat(asset.amount).toFixed(2))).toLocaleString()
                                                }}</span>
                                            </td>
                                            <hr />
                                        </tr>
                                        <tr>
                                            <td><b>{{ $t('BalanceSheetReport.Total') }}</b></td>
                                            <td></td>
                                            <td><b>
                                                    <span v-if="totalAssets < 0" :class="'text-danger'">({{
                                                        Number(Math.abs(parseFloat(totalAssets).toFixed(2))).toLocaleString()
                                                    }})</span>
                                                    <span v-else>{{
                                                        Number(Math.abs(parseFloat(totalAssets).toFixed(2))).toLocaleString()
                                                    }}</span>
                                                </b></td>
                                        </tr>
                                    </tbody>
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <h4>{{ $t('BalanceSheetReport.Liabilities') }}</h4>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(liability) in liabilitiesResults" v-bind:key="liability.Id">

                                            <td>
                                                <span>{{ liability.costCenter }}</span>
                                            </td>
                                            <td>{{ liability.code }}</td>
                                            <td>
                                                <span v-if="liability.amount < 0" :class="'text-danger'">({{
                                                    Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                }})</span>
                                                <span v-else>{{
                                                    Number(Math.abs(parseFloat(liability.amount).toFixed(2))).toLocaleString()
                                                }}</span>
                                            </td>
                                            <hr />
                                        </tr>

                                        <tr>
                                            <td><b>{{ $t('BalanceSheetReport.Total') }}</b></td>
                                            <td></td>
                                            <td><b>
                                                    <span v-if="totalLiabilities < 0" :class="'text-danger'">({{
                                                        Number(Math.abs(parseFloat(totalLiabilities).toFixed(2))).toLocaleString()
                                                    }})</span>
                                                    <span v-else>{{
                                                        Number(Math.abs(parseFloat(totalLiabilities).toFixed(2))).toLocaleString()
                                                    }}</span>
                                                </b></td>
                                        </tr>
                                    </tbody>
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <h4>{{ $t('BalanceSheetReport.Equity') }}</h4>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <tr v-for="(equity) in equityResults" v-bind:key="equity.Id">


                                            <td>
                                                <span>{{ equity.costCenter }}</span>
                                            </td>
                                            <td>{{ equity.code }}</td>
                                            <td>
                                                <span v-if="equity.amount < 0" :class="'text-danger'">({{
                                                    Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                }})</span>
                                                <span v-else>{{
                                                    Number(Math.abs(parseFloat(equity.amount).toFixed(2))).toLocaleString()
                                                }}</span>
                                            </td>
                                            <hr />
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Current Year Earnings</b>
                                            </td>
                                            <td></td>
                                            <td>
                                                <b>
                                                    <span v-if="NetIncome < 0" :class="'text-danger'">({{
                                                        Number(Math.abs(parseFloat(NetIncome).toFixed(2))).toLocaleString()
                                                    }})</span>
                                                    <span v-else>{{
                                                        Number(Math.abs(parseFloat(NetIncome).toFixed(2))).toLocaleString()
                                                    }}</span>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>{{ $t('BalanceSheetReport.Total') }}</b></td>
                                            <td></td>
                                            <td><b>
                                                    <span v-if="totalEquity < 0" :class="'text-danger'">({{
                                                        Number(Math.abs(parseFloat(totalEquity).toFixed(2))).toLocaleString()
                                                    }})</span>
                                                    <span v-else>{{
                                                        Number(Math.abs(parseFloat(totalEquity).toFixed(2))).toLocaleString()
                                                    }}</span>
                                                </b></td>
                                        </tr>
                                        <br />
                                        <br />
                                        <tr>
                                            <td><b>{{ $t('BalanceSheetReport.Total') }} {{
                                                $t('BalanceSheetReport.EquityandLiabilities') }}</b></td>
                                            <td></td>
                                            <td>
                                                <b>{{
                                                    Number(Math.abs(parseFloat(totalEquityLiability).toFixed(2))).toLocaleString()
                                                }}</b>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td><b>{{ $t('BalanceSheetReport.Total') }} {{ $t('BalanceSheetReport.Assets')
                                            }}</b></td>
                                            <td></td>
                                            <td>
                                                <b><span>{{
                                                    Number(Math.abs(parseFloat(totalAssets).toFixed(2))).toLocaleString()
                                                }}</span></b>
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
        <invoicedetailsprint :show="show1" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="show1 = false" @IsSave="IsSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from "@/Mixins/clickMixin";
import moment from "moment";
import Multiselect from "vue-multiselect";



export default {
    mixins: [clickMixin],
    props: ["formName"],
    components: {
        Multiselect,
        
    },
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',
            reportsrc: '',
            changereportt: 0,
            show1: false,
            financialYears: [],
            showDates: false,

            disablePeriod: false,
            disablePeriodRender: 0,

            assetsCostCenter: [],
            liabilitiesCostCenter: [],
            equitiesCostCenter: [],

            showComparisonTable: false,

            compareWith: '',
            isPreviousYear: false,
            isPreviousPeriod: false,
            isPreviousQuarter: false,
            isPreviousMonth: false,
            numberOfPeriods: '',

            showTable: false,
            loading: false,


            reportOpt: "",
            dateRender: 0,
            render: 0,
            show: false,

            date: '',
            fromDate: '',
            toDate: '',
            rander: 0,
            printRender: 0,
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            isShown: false,
            assetsResults: [],
            assets: [],
            liabilities: [],
            liabilitiesResults: [],
            equity: [],
            equityResults: [],
            records: [],
            advanceFilters: false,
            combineDate: '',
            language: 'Nothing',
            totalAssets: 0,
            totalLiabilities: 0,
            totalEquity: 0,
            totalEquityLiability: 0,
            totalIncome: 0,
            totalExpense: 0,
            NetIncome: 0,
            isNegative: false,

            comparisonList1: [],
            comparisonList2: [],
            comparisonList3: [],
            comparisonList4: [],
            comparisonList5: [],
            comparisonList6: [],
            comparisonList7: [],
            comparisonList8: [],
            comparisonList9: [],
            comparisonList10: [],
            comparisonList11: [],
            comparisonList12: [],

            pointerFrom: 0,
            elementFrom: 0,
            pointerDown: false,
        };
    },
    watch: {
        
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
        AdvanceFilters: function () {
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.reportOpt = '';
            this.advanceFilters = !this.advanceFilters;
            this.showDates = false;
            this.disablePeriod = !this.disablePeriod;
            this.disablePeriodRender++;
            this.showTable = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.showComparisonTable = false;
        },
        RemoveFilters: function () {
            this.reportOpt = '';
            this.show = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.showTable = false;
            this.showComparisonTable = false;
            this.advanceFilters = false;
            this.disablePeriod = false;
            this.disablePeriodRender++;
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        convertDate: function (date) {
            return moment(date).format("l");
        },
        GetDateTime: function () {

            if (this.reportOpt == 'Today') {
                this.fromDate = moment().format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'This Week') {
                // Get the current date
                const currentDate = moment();

                // Get the Monday date of the current week (first day of the week)
                const firstDayOfWeek = moment(currentDate).startOf('week');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfWeek.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'This Month') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the current month
                const firstDayOfMonth = moment(currentDate).startOf('month');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfMonth.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");

                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'This Quarter') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the current quarter
                const firstDayOfQuarter = moment(currentDate).startOf('quarter');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfQuarter.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");

                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'This Year') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the current year
                const firstDayOfYear = moment(currentDate).startOf('year');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfYear.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'Yesterday') {
                // Get the current date
                const currentDate = moment();

                // Get yesterday's date
                const yesterday = moment(currentDate).subtract(1, 'day');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = yesterday.format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'Previous Week') {
                // Get the current date
                const currentDate = moment();

                // Get the first day (Monday) of the previous week
                const firstDayOfPreviousWeek = moment(currentDate).subtract(1, 'week').startOf('isoWeek');

                // Get the end day (Sunday) of the previous week
                const endDayOfPreviousWeek = moment(currentDate).subtract(1, 'week').endOf('isoWeek');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousWeek.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousWeek.format("DD MMM YYYY");
                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'Previous Month') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the previous month
                const firstDayOfPreviousMonth = moment(currentDate).subtract(1, 'month').startOf('month');

                // Get the end day of the previous month
                const endDayOfPreviousMonth = moment(currentDate).subtract(1, 'month').endOf('month');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousMonth.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousMonth.format("DD MMM YYYY");
                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'Previous Quarter') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the previous quarter
                const firstDayOfPreviousQuarter = moment(currentDate).subtract(1, 'quarter').startOf('quarter');

                // Get the end day of the previous quarter
                const endDayOfPreviousQuarter = moment(currentDate).subtract(1, 'quarter').endOf('quarter');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousQuarter.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousQuarter.format("DD MMM YYYY");
                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'Previous Year') {
                // Get the current date
                const currentDate = moment();

                // Get the first day of the previous year
                const firstDayOfPreviousYear = moment(currentDate).subtract(1, 'year').startOf('year');

                // Get the end day of the previous year
                const endDayOfPreviousYear = moment(currentDate).subtract(1, 'year').endOf('year');

                // Format the dates as "DD MMM YYYY"
                this.fromDate = firstDayOfPreviousYear.format("DD MMM YYYY");
                this.toDate = endDayOfPreviousYear.format("DD MMM YYYY");

                this.show = false;
                this.showTable = false;
                this.showComparisonTable = false;
                this.showDates = false;
            }
            if (this.reportOpt == 'Custom') {

                this.toDate = moment().format("DD MMM YYYY");
                const yesterday = moment(this.toDate).subtract(6, 'day');
                // Format the dates as "DD MMM YYYY"
                this.fromDate = yesterday.format("DD MMM YYYY");
                this.show = false;
                this.showDates = true;
                this.dateRender++;
                this.showTable = false;
                this.showComparisonTable = false;
            }
        },
        GetPeriods: function () {
            if (this.compareWith == 'Previous Year(s)') {
                this.financialYears = [];
                this.isPreviousYear = true;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.numberOfPeriods = '';
                this.showDates = false;
            }
            if (this.compareWith == 'Previous Period(s)') {
                this.isPreviousYear = false;
                this.isPreviousPeriod = true;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.getFinancialYears();
                this.numberOfPeriods = '';
                this.showDates = false;
            }
            if (this.compareWith == 'Previous Quarter(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = true;
                this.isPreviousMonth = false;
                this.numberOfPeriods = '';
                this.showDates = false;
            }
            if (this.compareWith == 'Previous Month(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = true;
                this.numberOfPeriods = '';
                this.showDates = false;
            }
        },
        findDataByCode: function (code, list) {
            return list.find((item) => item.code === code);
        },
        getData: function () {

            var root = this;
            var token = "";
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.$https.get("/Report/AdvanceBalanceSheet?fromDate=" + this.fromDate + "&toDate=" + this.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&branchId=' + this.branchId, { headers: { Authorization: `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.records = response.data;
                        if (root.records.balanceSheetComparison != null) {
                            root.showTable = false;
                            root.showComparisonTable = true;

                            root.records.balanceSheetComparison.forEach((item, index) => {
                                if (index == 0) {
                                    //Assets
                                    var assetRes = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes.push(newData);
                                        }
                                    });
                                    assetRes.sort((a, b) => a.code.localeCompare(b.code));


                                    //Liabilities
                                    var liabilitiesRes = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes.push(newData);
                                        }
                                    });


                                    liabilitiesRes.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes.push(newData);
                                        }
                                    });



                                    equityRes.sort((a, b) => a.code.localeCompare(b.code));

                                    root.comparisonList1 = [];
                                    root.comparisonList1.push({ compareWithPeriodName: item.compareWith, assets: assetRes, liabilities: liabilitiesRes, equity: equityRes, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList2 = [];
                                    root.comparisonList3 = [];
                                    root.comparisonList4 = [];
                                    root.comparisonList5 = [];
                                    root.comparisonList6 = [];
                                    root.comparisonList7 = [];
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 1) {

                                    //Assets
                                    var assetRes1 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes1 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes1);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes1.push(newData);
                                        }
                                    });



                                    assetRes1.sort((a, b) => a.code.localeCompare(b.code));


                                    //Liabilities
                                    var liabilitiesRes1 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes1 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes1);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes1.push(newData);
                                        }
                                    });



                                    liabilitiesRes1.sort((a, b) => a.code.localeCompare(b.code));


                                    //Equities
                                    var equityRes1 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes1 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes1);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes1.push(newData);
                                        }
                                    });

                                    equityRes1.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList2 = [];
                                    root.comparisonList2.push({ compareWithPeriodName: item.compareWith, assets: assetRes1, liabilities: liabilitiesRes1, equity: equityRes1, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList3 = [];
                                    root.comparisonList4 = [];
                                    root.comparisonList5 = [];
                                    root.comparisonList6 = [];
                                    root.comparisonList7 = [];
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 2) {

                                    //Assets
                                    var assetRes2 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes2 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes2);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes2.push(newData);
                                        }
                                    });



                                    assetRes2.sort((a, b) => a.code.localeCompare(b.code));


                                    //Liabilities
                                    var liabilitiesRes2 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes2 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes2);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes2.push(newData);
                                        }
                                    });



                                    liabilitiesRes2.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes3 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes3 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes3);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes3.push(newData);
                                        }
                                    });



                                    equityRes3.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList3 = [];
                                    root.comparisonList3.push({ compareWithPeriodName: item.compareWith, assets: assetRes2, liabilities: liabilitiesRes2, equity: equityRes3, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList4 = [];
                                    root.comparisonList5 = [];
                                    root.comparisonList6 = [];
                                    root.comparisonList7 = [];
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 3) {

                                    //Assets
                                    var assetRes4 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes4 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes4);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes4.push(newData);
                                        }
                                    });



                                    assetRes4.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes4 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes4 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes4);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes4.push(newData);
                                        }
                                    });



                                    liabilitiesRes4.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes4 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes4 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes4);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes4.push(newData);
                                        }
                                    });



                                    equityRes4.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList4 = [];
                                    root.comparisonList4.push({ compareWithPeriodName: item.compareWith, assets: assetRes4, liabilities: liabilitiesRes4, equity: equityRes4, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList5 = [];
                                    root.comparisonList6 = [];
                                    root.comparisonList7 = [];
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 4) {

                                    //Assets
                                    var assetRes5 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes5 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes5);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes5.push(newData);
                                        }
                                    });


                                    assetRes5.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes5 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes5 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes5);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes5.push(newData);
                                        }
                                    });



                                    liabilitiesRes5.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes5 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes5 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes5);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes5.push(newData);
                                        }
                                    });


                                    equityRes5.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList5 = [];
                                    root.comparisonList5.push({ compareWithPeriodName: item.compareWith, assets: assetRes5, liabilities: liabilitiesRes5, equity: equityRes5, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList6 = [];
                                    root.comparisonList7 = [];
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 5) {

                                    //Assets
                                    var assetRes6 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes6 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes6);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes6.push(newData);
                                        }
                                    });


                                    assetRes6.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes6 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes6 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes6);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes6.push(newData);
                                        }
                                    });

                                    liabilitiesRes6.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes6 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes6 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes6);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes6.push(newData);
                                        }
                                    });

                                    equityRes6.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList6 = [];
                                    root.comparisonList6.push({ compareWithPeriodName: item.compareWith, assets: assetRes6, liabilities: liabilitiesRes6, equity: equityRes6, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList7 = [];
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 6) {

                                    //Assets
                                    var assetRes7 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes7 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes7);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes7.push(newData);
                                        }
                                    });


                                    assetRes7.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes7 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes7 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes7);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes7.push(newData);
                                        }
                                    });

                                    liabilitiesRes7.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes7 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes7 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes7);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes7.push(newData);
                                        }
                                    });

                                    equityRes7.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList7 = [];
                                    root.comparisonList7.push({ compareWithPeriodName: item.compareWith, assets: assetRes7, liabilities: liabilitiesRes7, equity: equityRes7, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList8 = [];
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 7) {

                                    //Assets
                                    var assetRes8 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes8 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes8);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes8.push(newData);
                                        }
                                    });


                                    assetRes8.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes8 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes8 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes8);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes8.push(newData);
                                        }
                                    });

                                    liabilitiesRes8.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes8 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes8 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes8);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes8.push(newData);
                                        }
                                    });

                                    equityRes8.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList8 = [];
                                    root.comparisonList8.push({ compareWithPeriodName: item.compareWith, assets: assetRes8, liabilities: liabilitiesRes8, equity: equityRes8, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList9 = [];
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 8) {

                                    //Assets
                                    var assetRes9 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes9 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes9);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes9.push(newData);
                                        }
                                    });


                                    assetRes9.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes9 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes9 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes9);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes9.push(newData);
                                        }
                                    });

                                    liabilitiesRes9.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes9 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes9 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes9);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes9.push(newData);
                                        }
                                    });

                                    equityRes9.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList9 = [];
                                    root.comparisonList9.push({ compareWithPeriodName: item.compareWith, assets: assetRes9, liabilities: liabilitiesRes9, equity: equityRes9, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList10 = [];
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 9) {

                                    //Assets
                                    var assetRes10 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes10 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes10);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes10.push(newData);
                                        }
                                    });


                                    assetRes10.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes10 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes10 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes10);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes10.push(newData);
                                        }
                                    });

                                    liabilitiesRes10.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes10 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes10 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes10);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes10.push(newData);
                                        }
                                    });

                                    equityRes10.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList10 = [];
                                    root.comparisonList10.push({ compareWithPeriodName: item.compareWith, assets: assetRes10, liabilities: liabilitiesRes10, equity: equityRes10, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList11 = [];
                                    root.comparisonList12 = [];
                                }
                                else if (index == 10) {

                                    //Assets
                                    var assetRes11 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes11 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes11);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes11.push(newData);
                                        }
                                    });


                                    assetRes11.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes11 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes11 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes11);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes11.push(newData);
                                        }
                                    });

                                    liabilitiesRes11.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes11 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes11 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes11);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes11.push(newData);
                                        }
                                    });

                                    equityRes11.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList11 = [];
                                    root.comparisonList11.push({ compareWithPeriodName: item.compareWith, assets: assetRes11, liabilities: liabilitiesRes11, equity: equityRes11, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                    root.comparisonList12 = [];
                                }
                                else if (index == 11) {
                                    root.compareWithPeriodName12 = item.compareWith;
                                    //Assets
                                    var assetRes12 = [];
                                    const sumsByAssets = {};
                                    item.asset.forEach((res) => {
                                        const { code, amount, costCenter } = res;
                                        const key = code;
                                        if (sumsByAssets[key] === undefined) {
                                            sumsByAssets[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByAssets[key].amount += amount;
                                    });

                                    assetRes12 = Object.values(sumsByAssets);

                                    root.assetsCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, assetRes12);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            assetRes12.push(newData);
                                        }
                                    });


                                    assetRes12.sort((a, b) => a.code.localeCompare(b.code));



                                    //Liabilities
                                    var liabilitiesRes12 = [];
                                    const sumsByliabilities = {};
                                    item.liability.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByliabilities[key] === undefined) {
                                            sumsByliabilities[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByliabilities[key].amount += amount;
                                    });

                                    liabilitiesRes12 = Object.values(sumsByliabilities);

                                    root.liabilitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, liabilitiesRes12);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            liabilitiesRes12.push(newData);
                                        }
                                    });

                                    liabilitiesRes12.sort((a, b) => a.code.localeCompare(b.code));



                                    //Equities
                                    var equityRes12 = [];
                                    const sumsByequity = {};
                                    item.equity.forEach((item) => {
                                        const { code, amount, costCenter } = item;
                                        const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                        if (sumsByequity[key] === undefined) {
                                            sumsByequity[key] = {
                                                code,
                                                costCenter,
                                                amount: 0,
                                            };
                                        }
                                        sumsByequity[key].amount += amount;
                                    });

                                    equityRes12 = Object.values(sumsByequity);
                                    root.equitiesCostCenter.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, equityRes12);
                                        if (!existingData) {
                                            const newData = {
                                                code: item.code,
                                                costCenter: item.costCenterName,
                                                amount: 0,
                                            };
                                            equityRes12.push(newData);
                                        }
                                    });

                                    equityRes12.sort((a, b) => a.code.localeCompare(b.code));
                                    root.comparisonList12 = [];
                                    root.comparisonList12.push({ compareWithPeriodName: item.compareWith, assets: assetRes12, liabilities: liabilitiesRes12, equity: equityRes12, totalAssets: item.totalAssets, totalLiabilities: item.totalLiabilities, totalEquities: item.totalEquities, netAmount: item.netAmount });
                                }
                            })
                        }
                        else {
                            root.showTable = true;
                            root.assets = response.data.asset;
                            const sumsByAssets = {};
                            root.assets.forEach((item) => {
                                const { code, amount, costCenter } = item;
                                const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                if (sumsByAssets[key] === undefined) {
                                    sumsByAssets[key] = {
                                        code,
                                        costCenter,
                                        amount: 0,
                                    };
                                }
                                sumsByAssets[key].amount += amount;
                            });

                            root.assetsResults = Object.values(sumsByAssets);

                            root.totalAssets = response.data.totalAssets;

                            root.liabilities = response.data.liability;

                            const sumsByliabilities = {};
                            root.liabilities.forEach((item) => {
                                const { code, amount, costCenter } = item;
                                const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                if (sumsByliabilities[key] === undefined) {
                                    sumsByliabilities[key] = {
                                        code,
                                        costCenter,
                                        amount: 0,
                                    };
                                }
                                sumsByliabilities[key].amount += amount;
                            });

                            root.liabilitiesResults = Object.values(sumsByliabilities);

                            root.totalLiabilities = response.data.totalLiabilities;

                            root.NetIncome = response.data.yearlyIncome;

                            root.equity = response.data.equity;

                            const sumsByequity = {};
                            root.equity.forEach((item) => {
                                const { code, amount, costCenter } = item;
                                const key = code; // Combining Code and either CostCenter or Banks to form a unique key
                                if (sumsByequity[key] === undefined) {
                                    sumsByequity[key] = {
                                        code,
                                        costCenter,
                                        amount: 0,
                                    };
                                }
                                sumsByequity[key].amount += amount;
                            });

                            root.equityResults = Object.values(sumsByequity);

                            root.totalEquity = root.NetIncome + response.data.totalEquities;

                            root.totalEquityLiability = root.totalEquity + root.totalLiabilities
                        }
                        root.loading = false;
                    }
                });


        },

        getCostCenter: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.$https.get("/Report/GetCostCenterList", { headers: { Authorization: `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.assetsCostCenter = response.data.assets;
                        root.liabilitiesCostCenter = response.data.liabilities;
                        root.equitiesCostCenter = response.data.equities;
                    }
                });
        },
        IsSave: function () {
            this.showReport = !this.showReport;
        },
        PrintRdlc: function () {
            var companyId = '';
            companyId = localStorage.getItem('CompanyID');
            

            this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?fromDate=' + this.fromDate + "&toDate=" + this.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith+'&companyId=' + companyId + '&formName=AdvanceBalanceSheetReport'
            this.changereportt++;
            this.show1 = !this.show1;
        },
        getFinancialYears: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.$https.get("/Report/GetYearlyPeriodList", { headers: { Authorization: `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        const financialYear = response.data;

                        financialYear.forEach((item) => {
                            root.financialYears.push(item.name);
                        })
                    }
                });
        },

        onDrag: function (event) {
            if (event.pointerType === 'mouse') {
                this.$refs.scrollable.scrollLeft = this.elementFrom - event.clientX + this.pointerFrom;
            }
        },
        onPointerDown: function (event) {
            if (event.pointerType === 'mouse') {
                this.pointerDown = true;
                this.pointerFrom = event.clientX;
                this.elementFrom = this.$refs.scrollable.scrollLeft;

                document.addEventListener('pointermove', this.onDrag);
            }
        },
        onPointerUp: function (event) {
            if (event.pointerType === 'mouse') {
                document.removeEventListener('pointermove', this.onDrag);
            }
        },
    },
    mounted: function () {
        this.language = this.$i18n.locale;
        this.fromDate = moment().format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.getCostCenter();
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    },
};
</script>

<style scoped>
    .bxr {
        overflow: auto;
        width: 70vw;
    }
    .opac {
        opacity: 0 !important;
    }
    .table-responsive {
        overflow-x: hidden !important;
    }
    .pointers {
        cursor: pointer;
    }
    .pointers:last-child{
        margin-right: 155px !important;
    }

</style>