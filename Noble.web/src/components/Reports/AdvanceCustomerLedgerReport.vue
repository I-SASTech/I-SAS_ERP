<template>
    <div class="row"
        v-if="(isValid('CanViewCustomerLedger') && documentName) || (isValid('CanViewSupplieLedger') && !documentName)">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col" v-if="documentName">
                                <h4 class="page-title">Advance {{ $t('CustomerLedgerReport.CustomerLedgerReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('CustomerLedgerReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">Advance {{
                                        $t('CustomerLedgerReport.CustomerLedgerReport') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col" v-if="!documentName">
                                <h4 class="page-title">Advance {{ $t('CustomerLedgerReport.SupplierLedgerReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('CustomerLedgerReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">Advance {{
                                        $t('CustomerLedgerReport.SupplierLedgerReport') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc()" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('StockReport.Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row align-items-center">
                <div class="row align-items-end">
                    <div class=" col-lg-4   form-group">
                        <label>Select a Period:</label>
                        <multiselect v-model="reportOpt" :disabled="disablePeriod" :key="disablePeriodRender"
                            :options="['Today', 'This Week', 'This Month', 'This Quarter', 'This Year', 'Yesterday', 'Previous Week', 'Previous Month', 'Previous Quarter', 'Previous Year', 'Custom']"
                            :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetDateTime()">
                        </multiselect>
                    </div>
                    <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
                    <div class=" col-lg-4  form-group pe-0">
                        <a class="btn btn-soft-primary me-2" v-on:click="AdvanceFilters()" id="button-addon2"
                            value="Advance Filter">
                            <i class="fa fa-filter"></i>
                        </a>
                        <button v-if="(reportOpt == '' || reportOpt == null) && (numberOfPeriods == 0)" disabled
                            href="javascript:void(0);" class="btn btn-outline-primary me-2">
                            Apply Filters
                        </button>
                        <button v-else v-on:click="GetInventoryList()" href="javascript:void(0);"
                            class="btn btn-outline-primary me-2">
                            Apply Filters
                        </button>
                        <a v-on:click="RemoveFilters()" href="javascript:void(0);" class="btn btn-outline-danger">
                            Clear Filters
                        </a>
                    </div>
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
            <div class="d-flex bor" v-if="showCompareTable" :key="rander" ref="scrollable" @pointerdown="onPointerDown"
                @pointerup="onPointerUp">
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList1.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList1"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList2.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList2"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList3.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList3"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList4.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList4"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList5.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList5"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList6.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList6"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList7.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList7"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList8.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList8"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList9.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList9"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList10.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList10"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList11.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList11"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-lg-10 pointers border-0" v-if="userLedgerCompareList12.length > 0">
                    <div class="card-body border-0">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in userLedgerCompareList12"
                                :key="item.compareWithValue">
                                <h6>{{ item.compareWithValue }}</h6>
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in item.contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(item.totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(item.totalDebit - item.totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" v-if="showTable" :key="rander">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div>
                            <div class="table-responsive">
                                <table class="table table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                <span v-if="isCustomer == 'true'">Customer Name</span>
                                                <span v-else>Supplier Name</span>
                                            </th>
                                            <th style="width: 10%;">
                                                {{ $t('CustomerLedgerReport.Account') }} {{ $t('CustomerLedgerReport.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.VatNo') }}
                                            </th>
                                            <th>
                                                {{ $t('CustomerLedgerReport.Amount') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(contact, index) in contactList" v-bind:key="contact.id">
                                            <td>{{ index + 1 }}</td>
                                            <td style="width: 10%;">{{ contact.contactCode }}</td>

                                            <td>
                                                <span v-if="language == 'en'">{{ contact.contactName == '' ||
                                                    contact.contactName == null
                                                    ? contact.contactNameArabic : contact.contactName }}</span>
                                                <span v-else>{{ contact.contactNameArabic == '' ||
                                                    contact.contactNameArabic == null
                                                    ? contact.contactName : contact.contactNameArabic }}</span>
                                            </td>
                                            <td style="width: 10%;">{{ contact.accountCode }}</td>
                                            <td>{{ contact.vatNo }}</td>
                                            <td> {{ contact.amount > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(contact.amount) }}</td>
                                        </tr>
                                        <tr style="font-size:15px;font-weight:bold;">
                                            <td colspan="3" class="text-center" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.TotalDebit') }}:
                                                {{ (parseFloat(totalDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                            <td colspan="2" class="text-left" style="padding-top:60px">
                                                {{ $t('CustomerLedgerReport.TotalCredit')
                                                }}:{{ (parseFloat(totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
    "$1,") }}
                                            </td>

                                            <td colspan="3" class="text-left" style="padding-top:60px">{{
                                                $t('CustomerLedgerReport.Total') }}:
                                                {{ (parseFloat(totalDebit - totalCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="show = false" @IsSave="IsSave" />
        
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>

import moment from "moment";
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from "vue-multiselect";



export default {
    mixins: [clickMixin],
    props: ['formName'],
    components: {
        Multiselect,
        
    },
    data: function () {
        return {
            documentName: this.formName,
            allowBranches: false,
            branchIds: [],
            branchId: '',
            isPeriod: true,
            showDates: false,
            financialYears: [],
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

            disablePeriod: false,
            disablePeriodRender: 0,

            rander: 0,
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            changereportt: 0,
            isCustomer: false,
            show: false,
            render: 0,
            printRender: 0,
            printRenderPdf: 0,
            accountId: '00000000-0000-0000-0000-000000000000',
            fromDate: '',
            dateType: '',
            toDate: '',
            formType: false,
            contactList: [],
            resultList: [],
            isShown: false,
            advanceFilters: false,
            isDownload: false,
            combineDate: '',
            language: 'Nothing',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },

            sumDebit: 0,
            sumCredit: 0,
            total: 0,
            showCompareTable: false,
            compareWithValue: '',
            userLedgerCompareList1: [],
            userLedgerCompareList2: [],
            userLedgerCompareList3: [],
            userLedgerCompareList4: [],
            userLedgerCompareList5: [],
            userLedgerCompareList6: [],
            userLedgerCompareList7: [],
            userLedgerCompareList8: [],
            userLedgerCompareList9: [],
            userLedgerCompareList10: [],
            userLedgerCompareList11: [],
            userLedgerCompareList12: [],
            pointerFrom: 0,
            elementFrom: 0,
            pointerDown: false,
        }
    },
    watch: {
        documentName: function () {
            
            if (this.$route.query.formName == "true") {
                this.documentName = true;
                this.formType = true;
                this.reportOpt = '';
                this.show = false;
                this.showDates = false;
                this.numberOfPeriods = '';
                this.compareWith = '';
                this.showTable = false;
                this.showComparisonTable = false;
                this.isPeriod = true;
                this.financialYears = [];
                this.userLedgerCompareList1 = [];
                this.userLedgerCompareList2 = [];
                this.userLedgerCompareList3 = [];
                this.userLedgerCompareList4 = [];
                this.userLedgerCompareList5 = [];
                this.userLedgerCompareList6 = [];
                this.userLedgerCompareList7 = [];
                this.userLedgerCompareList8 = [];
                this.userLedgerCompareList9 = [];
                this.userLedgerCompareList10 = [];
                this.userLedgerCompareList11 = [];
                this.userLedgerCompareList12 = [];
                this.rander++;
            }
            else {
                this.formType = false;
                this.documentName = false;
                this.reportOpt = '';
                this.show = false;
                this.showDates = false;
                this.numberOfPeriods = '';
                this.compareWith = '';
                this.showTable = false;
                this.isPeriod = true;
                this.showComparisonTable = false;
                this.financialYears = [];
                this.userLedgerCompareList1 = [];
                this.userLedgerCompareList2 = [];
                this.userLedgerCompareList3 = [];
                this.userLedgerCompareList4 = [];
                this.userLedgerCompareList5 = [];
                this.userLedgerCompareList6 = [];
                this.userLedgerCompareList7 = [];
                this.userLedgerCompareList8 = [];
                this.userLedgerCompareList9 = [];
                this.userLedgerCompareList10 = [];
                this.userLedgerCompareList11 = [];
                this.userLedgerCompareList12 = [];
                this.rander++;
            }
        },
        branchIds: function () {
            var root = this;
            this.branchId = '';
            this.branchIds.forEach(function (result) {
                root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
            })
            this.GetInventoryList();
        }
    },
    computed: {

        totalDebit: function () {
            return this.contactList.reduce(function (a, c) {

                if (c.amount > 0) {
                    return a + Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                }
                else {
                    return a + 0;
                }
            }, 0)
        },

        totalCredit: function () {
            return this.contactList.reduce(function (a, c) {
                if (c.amount <= 0) {
                    return a + Number((c.amount.toFixed(3).slice(0, -1) < 0 ? c.amount.toFixed(3).slice(0, -1) * -1 : c.amount.toFixed(3).slice(0, -1)) || 0)
                }
                else {
                    return a + 0;
                }
            }, 0)
        },

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
            this.compareWith = "";
            this.numberOfPeriods = "";
            this.showTable = false;
            this.showCompareTable = false;
        },
        RemoveFilters: function () {
            this.reportOpt = '';
            this.show = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.showTable = false;
            this.showComparisonTable = false;
            this.showCompareTable = false;
            this.advanceFilters = false;
            this.disablePeriod = false;
            this.disablePeriodRender++;
            this.disablePeriod = false;
            this.disablePeriodRender++;
            this.showDates = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
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
                this.showDates = false;
                this.showTable = false;
                this.showComparisonTable = false;
            }
            if (this.reportOpt == 'Custom') {

                this.toDate = moment().format("DD MMM YYYY");
                const yesterday = moment(this.toDate).subtract(6, 'day');
                // Format the dates as "DD MMM YYYY"
                this.fromDate = yesterday.format("DD MMM YYYY");
                this.show = false;
                this.showDates = true
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
                this.isPeriod = false;
                this.numberOfPeriods = '';
            }
            if (this.compareWith == 'Previous Period(s)') {
                this.isPreviousYear = false;
                this.isPreviousPeriod = true;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.getFinancialYears();
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
            if (this.compareWith == 'Previous Quarter(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = true;
                this.isPreviousMonth = false;
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
            if (this.compareWith == 'Previous Month(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = true;
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
        },
        getDate: function (date) {
            if (date == null || date == '')
                return '';
            else {
                return moment(date).format('LLL');

            }
        },
        GetInventoryList: function () {
            var root = this;
            var token = '';
            this.isCustomer = this.formType;
            this.showTable = false;
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.isShown = false;
            this.loading = true;
            this.$https.get('/Report/CustomerLedger?IsCustomer=' + this.isCustomer + '&FromDate=' + this.fromDate + '&ToDate=' + this.toDate + '&formName=' + this.formType + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (root.compareWith != '' && root.compareWith != null) {
                            root.showCompareTable = true;
                            const compareWithList = response.data;

                            compareWithList.forEach((item, index) => {
                                if (index == 0) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList1 = [];
                                    root.userLedgerCompareList1.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList2 = [];
                                    root.userLedgerCompareList3 = [];
                                    root.userLedgerCompareList4 = [];
                                    root.userLedgerCompareList5 = [];
                                    root.userLedgerCompareList6 = [];
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 1) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList2 = [];
                                    root.userLedgerCompareList2.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList3 = [];
                                    root.userLedgerCompareList4 = [];
                                    root.userLedgerCompareList5 = [];
                                    root.userLedgerCompareList6 = [];
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 2) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList3 = [];
                                    root.userLedgerCompareList3.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList4 = [];
                                    root.userLedgerCompareList5 = [];
                                    root.userLedgerCompareList6 = [];
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 3) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList4 = [];
                                    root.userLedgerCompareList4.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList5 = [];
                                    root.userLedgerCompareList6 = [];
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 4) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList5 = [];
                                    root.userLedgerCompareList5.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList6 = [];
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 5) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList6 = [];
                                    root.userLedgerCompareList6.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 6) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList7 = [];
                                    root.userLedgerCompareList7.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 7) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList8 = [];
                                    root.userLedgerCompareList8.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 8) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList9 = [];
                                    root.userLedgerCompareList9.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 9) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList10 = [];
                                    root.userLedgerCompareList10.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 10) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList11 = [];
                                    root.userLedgerCompareList11.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });
                                    root.userLedgerCompareList12 = [];

                                }
                                if (index == 11) {
                                    const customerLedgerList = item.compareWithList;

                                    root.sumDebit = 0;
                                    root.sumCredit = 0;
                                    customerLedgerList.forEach(x => {
                                        if (x.amount > 0) {
                                            root.sumDebit = root.sumDebit + x.amount;
                                        }
                                        else {
                                            root.sumCredit = root.sumCredit + x.amount;
                                        }

                                    });
                                    root.userLedgerCompareList12 = [];
                                    root.userLedgerCompareList12.push({ contactList: customerLedgerList, compareWithValue: item.compareWith, totalDebit: root.sumDebit, totalCredit: root.sumCredit });

                                }
                            });
                        }
                        else {
                            root.showTable = true;
                            root.contactList = response.data;
                        }
                    }
                    root.loading = false;
                });
        },

        nonNegative: function (value) {
            return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        },
        languageChange: function (lan) {
            if (this.language == lan) {

                var getLocale = this.$i18n.locale;
                this.language = getLocale;

                this.$router.go('/CustomerLedgerReport');

            }


        },
        IsSave: function () {
            this.showReport = !this.showReport;
        },
        PrintRdlc:function() {
            
            var companyId = '';
                        companyId = localStorage.getItem('CompanyID');
                    
                    this.isCustomer = this.formType;
                        this.reportsrc1=  this.$ReportServer+'/ReportManagementModule/LedgerReports/LedgerReports.aspx?fromDate=' + this.fromDate + '&toDate=' + this.toDate +  '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith+'&formName=AdvanceCustomerLedger'+'&companyId='+companyId +'&IsCustomer=' + this.isCustomer
                        this.changereportt++;
                        this.show = !this.show;
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
        
        this.isShown = false;
        this.language = this.$i18n.locale;
        this.documentName = this.$route.query.formName == "true" ? true : false;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.render++;
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>
<style scoped>.bor {
    border: 1px solid #e3ebf6;
    overflow: auto;
    width: 90vw;
}

.table-responsive {
    overflow-x: hidden !important;
}

.pointers {
    cursor: pointer;
}

.pointers:last-child {
    margin-right: 155px !important;
}</style>