<template>
    <div class="col-lg-12">
        <div class="row">
            <div class="col-lg-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Leave Management</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddBank.Home') }}</a></li>
                                <li class="breadcrumb-item active">Leave Management</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" v-on:click="GetLeaveData()" data-bs-toggle="tab" href="#LeaveTypes"
                            role="tab" aria-selected="false">Leave
                            Types</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" v-on:click="GetLeavePeriod()" data-bs-toggle="tab" href="#leaveperiod"
                            role="tab" aria-selected="false">Leave Period</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link " v-on:click="GetWorkWeek()" data-bs-toggle="tab" href="#workweek" role="tab"
                            aria-selected="true">Work Week</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link " v-on:click="GetLeaveHoliday()" data-bs-toggle="tab" href="#holiday" role="tab"
                            aria-selected="true">Holiday</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link " v-on:click="GetLeaveRules()" data-bs-toggle="tab" href="#leaverules" role="tab"
                            aria-selected="true">Leave Rules</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link " v-on:click="GetLeaveGroup()" data-bs-toggle="tab" href="#leavegroups"
                            role="tab" aria-selected="true">Leave Groups</a>
                    </li>
                </ul>

                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-pane p-3 active" id="LeaveTypes" role="tabpanel">
                        <div class="card">
                            <div class="card-header ">
                                <div class="row align-items-baseline">
                                    <div class="col-lg-8">
                                        <div class="input-group ">
                                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                                <i class="fas fa-search"></i>
                                            </button>
                                            <input v-model="search" type="text" class="form-control"
                                                :placeholder="$t('Search')" aria-label="Example text with button addon"
                                                aria-describedby="button-addon1">
                                        </div>
                                    </div>
                                    <div class="col-lg-4 text-end pe-5">
                                        <a href="javascript:void(0);" v-on:click="openmodel('leavetype')"
                                            class="btn btn-sm btn-outline-primary">
                                            Add New
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    #
                                                </th>
                                                <th>
                                                    Leave Name
                                                </th>
                                                <th>
                                                    Leave Accrue Enabled
                                                </th>
                                                <th>
                                                    Leave Carried Forward
                                                </th>
                                                <th>
                                                    Leaves Per Year
                                                </th>
                                                <th>
                                                    Leaves Group
                                                </th>
                                                <th>

                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(leave, index) in leaveTypeList" v-bind:key="leave.id">
                                                <td v-if="currentPage === 1">
                                                    {{ index + 1 }}
                                                </td>
                                                <td v-else>
                                                    {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                </td>
                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditLeaveType(leave.id)">{{
                                                            leave.leaveName }}</a>
                                                    </strong>
                                                </td>
                                                <td v-if="leave.leaveAccrueEnabled">
                                                    Yes
                                                </td>
                                                <td v-else>
                                                    No
                                                </td>
                                                <td v-if="leave.leaveCarriedForward1">
                                                    Yes
                                                </td>
                                                <td v-else>
                                                    No
                                                </td>

                                                <td>
                                                    {{ leave.leavesPerLeavePeriod }}
                                                </td>
                                                <td>
                                                    {{ leave.leaveGroup }}
                                                </td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing')
                                        }} {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
    $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{
        $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                            $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                            {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                                $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    </div>
                                    <div class=" col-lg-6">
                                        <div class="float-end" v-on:click="GetLeaveData()">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="tab-pane p-3" id="leaveperiod" role="tabpanel">
                        <div class="card">
                            <div class="card-header ">
                                <div class="row align-items-baseline">
                                    <div class="col-lg-8">
                                        <div class="input-group ">
                                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                                <i class="fas fa-search"></i>
                                            </button>
                                            <input v-model="searchleaveperiod" type="text" class="form-control"
                                                :placeholder="$t('Search')" aria-label="Example text with button addon"
                                                aria-describedby="button-addon1">
                                        </div>
                                    </div>
                                    <div class="col-lg-4 text-end pe-5">
                                        <a href="javascript:void(0);" v-on:click="openmodel('leaveperiod')"
                                            class="btn btn-sm btn-outline-primary">
                                            Add New
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    #
                                                </th>
                                                <th>
                                                    Name
                                                </th>
                                                <th>
                                                    Period Start
                                                </th>
                                                <th>
                                                    Period End
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(leave, index) in leaveperiodList" v-bind:key="leave.id">
                                                <td v-if="currentPage === 1">
                                                    {{ index + 1 }}
                                                </td>
                                                <td v-else>
                                                    {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                </td>
                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)"
                                                            v-on:click="EditLeavePeriod(leave.id)">{{ leave.name }}</a>
                                                    </strong>
                                                </td>
                                                <td>{{ getTimeOnly(leave.periodStart) }}</td>

                                                <td>{{ getTimeOnly(leave.periodEnd) }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing')
                                        }} {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
    $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{
        $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                            $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                            {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                                $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    </div>
                                    <div class=" col-lg-6">
                                        <div class="float-end" v-on:click="GetLeavePeriod()">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane p-3 " id="workweek" role="tabpanel">
                        <div class="card">
                            <div class="card-header ">
                                <div class="row align-items-baseline">
                                    <div class="col-lg-8">
                                        <div class="input-group ">
                                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                                <i class="fas fa-search"></i>
                                            </button>
                                            <input v-model="searchworkweek" type="text" class="form-control"
                                                :placeholder="$t('Search')" aria-label="Example text with button addon"
                                                aria-describedby="button-addon1">
                                        </div>
                                    </div>
                                    <div class="col-lg-4 text-end pe-5">
                                        <a href="javascript:void(0);" v-on:click="openmodel('workweek')"
                                            class="btn btn-sm btn-outline-primary">
                                            Add New
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    #
                                                </th>
                                                <th>
                                                    Day
                                                </th>
                                                <th>
                                                    Status
                                                </th>
                                                <th>
                                                    Country
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(leave, index) in workweekList" v-bind:key="leave.id">
                                                <td v-if="currentPage === 1">
                                                    {{ index + 1 }}
                                                </td>
                                                <td v-else>
                                                    {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                </td>
                                                <td v-if="leave.day == 1">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Monday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-else-if="leave.day == 2">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Tuesday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-else-if="leave.day == 3">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Wednesday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-else-if="leave.day == 4">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Thursday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-else-if="leave.day == 5">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Friday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-else-if="leave.day == 6">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Saturday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-else-if="leave.day == 7">
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditWorkWeek(leave.id)">
                                                            Sunday
                                                        </a>
                                                    </strong>
                                                </td>
                                                <td v-if="leave.status == 1">Full Day</td>
                                                <td v-else-if="leave.status == 2">Half Day</td>
                                                <td v-else-if="leave.status == 3">Non-Working Day</td>
                                                <td>{{ leave.country }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing')
                                        }} {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
    $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{
        $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                            $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                            {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                                $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    </div>
                                    <div class=" col-lg-6">
                                        <div class="float-end" v-on:click="GetWorkWeek()">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane p-3 " id="holiday" role="tabpanel">
                        <div class="card">
                            <div class="card-header ">
                                <div class="row align-items-baseline">
                                    <div class="col-lg-8">
                                        <div class="input-group ">
                                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                                <i class="fas fa-search"></i>
                                            </button>
                                            <input v-model="searchholiday" type="text" class="form-control"
                                                :placeholder="$t('Search')" aria-label="Example text with button addon"
                                                aria-describedby="button-addon1">
                                        </div>
                                    </div>
                                    <div class="col-lg-4 text-end pe-5">
                                        <a href="javascript:void(0);" v-on:click="openmodel('holiday')"
                                            class="btn btn-sm btn-outline-primary">
                                            Add New
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th width="5%">#</th>
                                    <th width="15%">
                                        {{ $t('Holiday.HolidayType') }}
                                    </th>
                                    <th width="20%">
                                        {{ $t('Holiday.PaidStatus') }}
                                    </th>

                                    <th width="20%">
                                        {{ $t('Holiday.Close') }} Date
                                    </th>
                                    <th width="30%">
                                        {{ $t('Holiday.Description') }}
                                    </th>
                                    <th width="10%">
                                        {{ $t('Holiday.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(color,index) in holydaylist" v-bind:key="color.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>
                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditLeaveHoliday(color.id)">
                                                <span v-if="color.holidayType==1" class="badge badge-boxed  badge-outline-primary">{{$t('Holiday.National')}}</span>
                                                <span v-if="color.holidayType==2" class="badge badge-boxed  badge-outline-primary">{{$t('Holiday.Guested')}}</span>
                                            </a>
                                        </strong>
                                    </td>
                                    <td>
                                        <span v-if="color.paidStatus" class="badge badge-boxed  badge-outline-primary">{{$t('Holiday.Paid')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Holiday.UnPaid')}}</span>
                                    </td>

                                    <td>{{getDate(color.date)}}</td>

                                    <td>{{color.description}}</td>

                                    <td>
                                        <span v-if="color.isActive" class="badge badge-boxed  badge-outline-success">{{$t('Holiday.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Holiday.De-Active')}}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                    <div class="float-start">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetLeaveHoliday()">
                            <b-pagination pills size="sm" v-model="currentPage"
                                          :total-rows="rowCount"
                                          :per-page="10"
                                          :first-text="$t('Table.First')"
                                          :prev-text="$t('Table.Previous')"
                                          :next-text="$t('Table.Next')"
                                          :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
                        </div>
                    </div>
                    <div class="tab-pane p-3 " id="leaverules" role="tabpanel">
                        <div class="card">
                            <div class="card-header ">
                                <div class="row align-items-baseline">
                                    <div class="col-lg-8">
                                        <div class="input-group ">
                                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                                <i class="fas fa-search"></i>
                                            </button>
                                            <input type="text" class="form-control" :placeholder="$t('Search')"
                                                aria-label="Example text with button addon"
                                                aria-describedby="button-addon1">
                                        </div>
                                    </div>
                                    <div class="col-lg-4 text-end pe-5">
                                        <a href="javascript:void(0);" v-on:click="openmodel('leaverules')"
                                            class="btn btn-sm btn-outline-primary">
                                            Add New
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    #
                                                </th>
                                                <th>
                                                    Leave Type
                                                </th>
                                                <th>
                                                    Leave Group
                                                </th>
                                                <th>
                                                    Leave Period
                                                </th>
                                                <th>
                                                    Leaves Per Year
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(leave, index) in leaverulesList" :key="leave.id">
                                                <td v-if="currentPage === 1">
                                                    {{ index + 1 }}
                                                </td>
                                                <td v-else>
                                                    {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                </td>
                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditLeaverRules(leave.id)">
                                                            {{ leave.leaveTypeName }}
                                                        </a>
                                                    </strong>

                                                </td>
                                                <td>
                                                    {{ leave.leaveGroupName }}
                                                </td>
                                                <td>
                                                    {{ leave.leavePeriodName }}
                                                </td>
                                                <td>
                                                    {{ leave.leavesPerLeavePeriod }}
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing')
                                        }} {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
    $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{
        $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                            $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                            {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                                $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    </div>
                                    <div class=" col-lg-6">
                                        <div class="float-end" v-on:click="GetLeaveRules()">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane p-3 " id="leavegroups" role="tabpanel">
                        <div class="card">
                            <div class="card-header ">
                                <div class="row align-items-baseline">
                                    <div class="col-lg-8">
                                        <div class="input-group ">
                                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                                <i class="fas fa-search"></i>
                                            </button>
                                            <input type="text" class="form-control" :placeholder="$t('Search')"
                                                aria-label="Example text with button addon"
                                                aria-describedby="button-addon1">
                                        </div>
                                    </div>
                                    <div class="col-lg-4 text-end pe-5">
                                        <a href="javascript:void(0);" v-on:click="openmodel('leavegroups')"
                                            class="btn btn-sm btn-outline-primary">
                                            Add New
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive" v-for="leave in leaveGroupList" :key="leave.id">
                                    <h4><strong>
                                            <a href="javascript:void(0)" v-on:click="EditLeaveGroup(leave.id)">
                                                {{ leave.name }}
                                            </a>
                                        </strong>
                                    </h4>
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    #
                                                </th>
                                                <th>
                                                    Employee
                                                </th>
                                                <th>
                                                    Details
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(sub, index) in leave.leaveGroupEmployees" :key="sub.id">
                                                <td v-if="currentPage === 1">
                                                    {{ index + 1 }}
                                                </td>
                                                <td v-else>
                                                    {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                </td>
                                                <td>{{ sub.employeeName }}</td>
                                                <td>{{ leave.details }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing')
                                        }} {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
    $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{
        $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                            $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                            {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                                $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    </div>
                                    <div class=" col-lg-6">
                                        <div class="float-end" v-on:click="GetLeaveGroup()">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div><!--end card-body-->
        </div>
        <addleavetypes :newleaveType="leaveTypes" :show="show" v-if="show" @close="IsSave" :type="type" />
        <addleaveperiod :leaveperiod="leavePeriod" :show="showLeavePeriod" v-if="showLeavePeriod" @close="IsSave"
            :type="type" />
        <addworkweek :newworkweek="workweek" :show="showworkweek" v-if="showworkweek" @close="IsSave" :type="type" />
        <addholidaysetup :newHoliday="newHoliday"
                             :show="showholiday"
                             v-if="showholiday"
                             @close="IsSave"
                             :type="type" />
        <addleaverules :newleaverules="leaverules" :show="showleaverules" v-if="showleaverules" @close="IsSave" :type="type" />
        <addleavegroups :newleavegroup="leavegroup" :show="showleavegroups" v-if="showleavegroups" @close="IsSave"
            :type="type" />
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment';
export default {
    mixins: [clickMixin],
    name: 'Leave',
    data: function () {
        return {
            show: false,
            showLeavePeriod: false,
            showworkweek: false,
            showholiday: false,
            showleaverules: false,
            showleavegroups: false,
            leaveTypes: {
                id: '',
                leaveName: '',
                leaveColor: '',
                leavesPerLeavePeriod: '',
                adminAssignLeave: false,
                employeesApplyForLeaveType: false,
                beyondCurrentLeaveBalance: false,
                percentageLeaveCF: '',
                maximumCFAmount: '',
                leaveCarriedForward1: false,
                cFLeaveAvailabilityPeriod1: '',
                cfLeaveAvailabilityPeriodToString: '',
                leaveGroup: '',
                leaveGroupId: '',
                leaveAccrueEnabled: false,
                proportionateLeaves: false,
                employeeLeavePeriod: false,
                sendNotificationEmails: false,
            },
            leaverules: {
                id: '',
                leaveTypeId: '',
                leaveGroupId: '',
                designationId: '',
                employeeId: '',
                requiredExperience: '',
                departmentId: '',
                leavePeriodId: '',
                leavesPerLeavePeriod: '',
                adminAssignLeave: false,
                employeesApplyForLeaveType: false,
                beyondCurrentLeaveBalance: false,
                leaveAccrueEnabled: false,
                leaveCarriedForward1: false,
                percentageLeaveCF: '',
                maximumCFAmount: '',
                cFLeaveAvailabilityPeriod1: '',
                proportionateLeaves: false,
            },
            leavePeriod: {
                id: '',
                name: '',
                periodStart: '',
                periodEnd: '',
            },
            workweek: {
                id: '',
                day: '',
                status: '',
                country: '',
            },
            newHoliday: {
                    id: '',
                    holidayType: '',
                    date: '',
                    year: '',
                    color:'',
                    description: '',
                    paidStatus: false,
                    isActive: true
            },
            leavegroup: {
                id: '',
                name: '',
                leaveGroupEmployees: [{ employeeId: '', leaveGroupId: '' }],
                details: '',
            },
            type: '',
            search: '',
            searchleaveperiod: '',
            searchworkweek: '',
            searchholiday: '',
            searchleaverules: '',
            searchleavegroups: '',
            searchpaidtimeoff: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            currentPageleaveperiod: 1,
            pageCountleaveperiod: '',
            rowCountleaveperiod: '',
            leaveTypeList: [],
            leaveperiodList: [],
            workweekList: [],
            holydaylist: [],
            leaveGroupList: [],
            leaverulesList: []
        }
    },
    watch: {
        search: function (val) {
            this.GetLeaveData(val, 1);
        },
        searchleaveperiod: function (val) {
            this.GetLeavePeriod(val, 1);
        },
        searchworkweek: function (val) {
            this.GetWorkWeek(val, 1);
        },
        searchholiday: function (val) {
            this.GetLeaveHoliday(val, 1);
        },
        searchleaverules: function (val) {
            this.GetLeaveRules(val, 1);
        },
        searchleavegroups: function (val) {
            this.GetLeaveGroup(val, 1);
        },
        searchpaidtimeoff: function (val) {
            this.GetLeaveHoliday(val, 1);
        }
    },
    methods: {
        getDate: function (date) {
                return moment(date).format('LL');
            },
        getTimeOnly: function (x) {
            return moment(x).format('lll');
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function (val) {
            //Leave Type
            if (val == 'leavetype') {
                this.leaveTypes = {
                    id: '00000000-0000-0000-0000-000000000000',
                    leaveName: '',
                    leaveColor: '#000000',
                    leavesPerLeavePeriod: '',
                    adminAssignLeave: false,
                    employeesApplyForLeaveType: false,
                    beyondCurrentLeaveBalance: false,
                    percentageLeaveCF: '',
                    maximumCFAmount: '',
                    cFLeaveAvailabilityPeriod1: '',
                    cfLeaveAvailabilityPeriodToString: '',
                    leaveGroupId: '',
                    leaveCarriedForward1: false,
                    leaveAccrueEnabled: false,
                    proportionateLeaves: false,
                    employeeLeavePeriod: false,
                    sendNotificationEmails: false,
                },
                    this.show = !this.show;
            }
            //Leave Period
            if (val == 'leaveperiod') {
                this.leavePeriod = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    periodStart: '',
                    periodEnd: '',
                },
                    this.showLeavePeriod = !this.showLeavePeriod;
            }
            //Work Week
            if (val == 'workweek') {
                this.workweek = {
                    id: '00000000-0000-0000-0000-000000000000',
                    day: '',
                    status: '',
                    country: '',
                },
                    this.showworkweek = !this.showworkweek;
            }
            //Holiday
            if (val == 'holiday') {
                this.newHoliday = {
                    id: '00000000-0000-0000-0000-000000000000',
                    holidayType: '',
                    date: '',
                    year: '',
                    color:'',
                    description: '',
                    paidStatus: false,
                    isActive: true
                }
                    this.showholiday = !this.showholiday;
            }
            if (val == 'leaverules') {
                this.leaverules = {
                    id: '00000000-0000-0000-0000-000000000000',
                    leaveTypeId: '00000000-0000-0000-0000-000000000000',
                    leaveGroupId: '00000000-0000-0000-0000-000000000000',
                    leavesPerLeavePeriod: '',
                    adminAssignLeave: false,
                    employeesApplyForLeaveType: false,
                    beyondCurrentLeaveBalance: false,
                    leaveAccrueEnabled: false,
                    leaveCarriedForward1: false,
                    percentageLeaveCF: '',
                    maximumCFAmount: '',
                    cFLeaveAvailabilityPeriod1: '',
                    proportionateLeaves: false,
                },
                    this.showleaverules = !this.showleaverules;
            }
            if (val == 'leavegroups') {
                this.leavegroup = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    leaveGroupEmployees: [{ employeeId: '', leaveGroupId: '' }],
                    details: '',
                },
                    this.showleavegroups = !this.showleavegroups;
            }
            this.type = "Add";
        },
        IsSave: function () {
            //Leave Type
            this.show = false;
            //Leave Period
            this.showLeavePeriod = false;
            //Work Week
            this.showworkweek = false;
            //Holiday
            this.showholiday = false;
            //Leave Rules
            this.showleaverules = false;
            //Leave Groups
            this.showleavegroups = false;
            //Leave Type
            this.GetLeaveData(this.search, this.currentPage);
            //Leave Period
            this.GetLeavePeriod(this.searchleaveperiod, this.currentPage);
            //Work Week
            this.GetWorkWeek(this.searchworkweek, this.currentPage);
            //Leave Holiday
            this.GetLeaveHoliday(this.searchholiday, this.currentPage);
            //Leave Group
            this.GetLeaveGroup(this.searchleavegroups, this.currentPage);
            //Leave Rules
            this.GetLeaveRules(this.searchleaverules, this.currentPage);

        },
        getPage: function () {
            //Leave Type
            this.GetLeaveData(this.search, this.currentPage);
            //Leave Period
            this.GetLeavePeriod(this.searchleaveperiod, this.currentPage);
            //Work Week
            this.GetWorkWeek(this.searchworkweek, this.currentPage);
            //Leave Holiday
            this.GetLeaveHoliday(this.searchholiday, this.currentPage);
            //Leave Group
            this.GetLeaveGroup(this.searchleavegroups, this.currentPage);
            //Leave Rules
            this.GetLeaveRules(this.searchleaverules, this.currentPage);
        },
        //Leave type
        GetLeaveData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Hr/LeaveTypeList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.leaveTypeList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        //Edit Leave Type
        EditLeaveType: function (id) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Hr/LeaveTypeDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.leaveTypes.id = response.data.id;
                        root.leaveTypes.leaveName = response.data.leaveName;
                        root.leaveTypes.leaveColor = response.data.leaveColor;
                        root.leaveTypes.leavesPerLeavePeriod = parseInt(response.data.leavesPerLeavePeriod);
                        root.leaveTypes.adminAssignLeave = response.data.adminAssignLeave == true ? 1 : 0;
                        root.leaveTypes.employeesApplyForLeaveType = response.data.employeesApplyForLeaveType == true ? 1 : 0;
                        root.leaveTypes.beyondCurrentLeaveBalance = response.data.beyondCurrentLeaveBalance == true ? 1 : 0;
                        root.leaveTypes.percentageLeaveCF = parseInt(response.data.percentageLeaveCF);
                        root.leaveTypes.maximumCFAmount = parseInt(response.data.maximumCFAmount);
                        root.leaveTypes.cfLeaveAvailabilityPeriodToString = response.data.cfLeaveAvailabilityPeriodToString;
                        root.leaveTypes.leaveGroupId = response.data.leaveGroupId;
                        root.leaveTypes.leaveCarriedForward1 = response.data.leaveCarriedForward1 == true ? 1 : 0;
                        root.leaveTypes.leaveAccrueEnabled = response.data.leaveAccrueEnabled == true ? 1 : 0;
                        root.leaveTypes.proportionateLeaves = response.data.proportionateLeaves == true ? 1 : 0;
                        root.leaveTypes.employeeLeavePeriod = response.data.employeeLeavePeriod == true ? 1 : 0;
                        root.leaveTypes.sendNotificationEmails = response.data.sendNotificationEmails == true ? 1 : 0;
                        root.show = !root.show;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        //Leave Period
        GetLeavePeriod: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Hr/LeavePeriodList?searchTerm=' + this.searchleaveperiod + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.leaveperiodList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        //Edit Leave Period
        EditLeavePeriod: function (id) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Hr/LeavePeriodDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.leavePeriod = response.data;
                        root.showLeavePeriod = !root.showLeavePeriod;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        //Work Week
        GetWorkWeek: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Hr/WorkWeekList?searchTerm=' + this.searchworkweek + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.workweekList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        //Edit Work Week
        EditWorkWeek: function (id) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Hr/WorkWeekDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.workweek = response.data;
                        root.showworkweek = !root.showworkweek;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        //Leave Holiday
        GetLeaveHoliday: function () {
            var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Hr/GetHolidaysList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.holydaylist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
        },
        //Edit Leave Holiday
        EditLeaveHoliday: function (id) {
            var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Hr/GetHolidaysDetails?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newHoliday.id = response.data.id;
                            root.newHoliday.holidayType = response.data.holidayType == 1 ? 'National' :'Guested';
                            root.newHoliday.date = response.data.date;
                            root.newHoliday.year = response.data.year;
                            root.newHoliday.description = response.data.description;
                            root.newHoliday.paidStatus = response.data.paidStatus;
                            root.newHoliday.isActive = response.data.isActive;
                            root.newHoliday.color = response.data.color;
                            root.showholiday = !root.showholiday;
                            root.type = "Edit"
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
        },
        //Leave Group
        GetLeaveGroup: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Hr/LeaveGroupList?searchTerm=' + this.searchleavegroups + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.leaveGroupList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        //Edit Leave Group
        EditLeaveGroup: function (id) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Hr/LeaveGroupDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.leavegroup = response.data;
                        root.showleavegroups = !root.showleavegroups;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        //Leave Group
        GetLeaveRules: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Hr/LeaveRulesList?searchTerm=' + this.searchleaverules + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.leaverulesList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        EditLeaverRules: function (id) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Hr/LeaveRulesDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.leaverules = response.data;
                        root.leaverules.adminAssignLeave = response.data.adminAssignLeave == true ? 1 : 0;
                        root.leaverules.employeesApplyForLeaveType = response.data.employeesApplyForLeaveType == true ? 1 : 0;
                        root.leaverules.beyondCurrentLeaveBalance = response.data.beyondCurrentLeaveBalance == true ? 1 : 0;
                        root.leaverules.leaveCarriedForward1 = response.data.leaveCarriedForward1 == true ? 1 : 0;
                        root.leaverules.leaveAccrueEnabled = response.data.leaveAccrueEnabled == true ? 1 : 0;
                        root.leaverules.proportionateLeaves = response.data.proportionateLeaves == true ? 1 : 0;
                        root.leaverules.employeeLeavePeriod = response.data.employeeLeavePeriod == true ? 1 : 0;
                        root.leaverules.cFLeaveAvailabilityPeriod1 = response.data.cfLeaveAvailabilityPeriod1.toString();

                        root.showleaverules = !root.showleaverules;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
    },
    created: function () {
    },
    mounted: function () {
        //Leave Type
        this.GetLeaveData();
        // //Leave Period
        // this.GetLeavePeriod();
        // //Work Week
        // this.GetWorkWeek();
        //  //Leave Holiday
        //  this.GetLeaveHoliday();
    }
}
</script>