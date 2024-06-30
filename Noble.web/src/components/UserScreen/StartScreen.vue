<template>
    <!--Page-Title-->
    <div class="container-fluid" v-if="role == 'HrUser'">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Employee Dashboard</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">{{ $t('AddBank.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">Employee Dashboard</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header bg-soft-primary">
                            <span class="me-2"><i class="mdi mdi-star"> </i> </span>Shortcuts
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center align-items-center">
                                                <div class="col">
                                                    <a href="javascript:void(0);">
                                                        Requested Time-Off
                                                    </a>
                                                </div>
                                                <div class="col-auto align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"
                                                             viewBox="0 0 24 24" fill="none" stroke="currentColor"
                                                             stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
                                                             class="feather feather-tag align-self-center text-muted icon-sm">
                                                            <path d="M20.59 13.41l-7.17 7.17a2 2 0 0 1-2.83 0L2 12V2h10l8.59 8.59a2 2 0 0 1 0 2.82z">
                                                            </path>
                                                            <line x1="7" y1="7" x2="7.01" y2="7"></line>
                                                        </svg>
                                                    </div>
                                                </div>
                                            </div>
                                        </div><!--end card-body-->
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center align-items-center">
                                                <div class="col">
                                                    <a href="javascript:void(0);" v-on:click="GoTo('/RegisterUser')">
                                                        My Profile
                                                    </a>
                                                </div>
                                                <div class="col-auto align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"
                                                             viewBox="0 0 24 24" fill="none" stroke="currentColor"
                                                             stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
                                                             class="feather feather-tag align-self-center text-muted icon-sm">
                                                            <path d="M20.59 13.41l-7.17 7.17a2 2 0 0 1-2.83 0L2 12V2h10l8.59 8.59a2 2 0 0 1 0 2.82z">
                                                            </path>
                                                            <line x1="7" y1="7" x2="7.01" y2="7"></line>
                                                        </svg>
                                                    </div>
                                                </div>
                                            </div>
                                        </div><!--end card-body-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card report-card">
                        <div class="card-header bg-soft-primary">
                            <span class="me-2"><i class="mdi mdi-information"></i></span>
                            My Infotmation
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <tbody>
                                        <tr>
                                            <td>Code</td>
                                            <td>{{ dashboard.employeeCode }}</td>
                                        </tr>
                                        <tr>
                                            <td>Department</td>
                                            <td v-if="dashboard.department != null">{{ dashboard.department }}</td>
                                            <td v-else></td>
                                        </tr>
                                        <tr>
                                            <td>Status</td>
                                            <td v-if="dashboard.status != null">{{ dashboard.status }}</td>
                                            <td v-else></td>
                                        </tr>
                                        <tr>
                                            <td>Gender</td>
                                            <td>{{ dashboard.gender }}</td>
                                        </tr>
                                        <tr>
                                            <td>Born</td>
                                            <td v-if="dashboard.born != null && dashboard.born != ''">
                                                {{ getDate(dashboard.born) }}
                                            </td>
                                            <td v-else></td>
                                        </tr>
                                        <tr>
                                            <td>Employee For</td>
                                            <td>{{ getDate(dashboard.employeeFor) }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card report-card">
                        <div class="card-header bg-soft-primary">
                            <span class="me-2"><i class="mdi mdi-timer-sand-full"></i></span>
                            Time Off / Leave Balances
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead>
                                        <tr>
                                            <th>
                                                Leave Type
                                            </th>
                                            <th>
                                                Total Leaves
                                            </th>
                                            <th>
                                                Leave Carried Forward
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="leave in dashboard.leaves" :key="leave.id">
                                            <td>{{ leave.leaveType }}</td>
                                            <td class="text-center">{{ leave.totLeaves }}</td>
                                            <td class="text-center">{{ leave.carriedForwardLeaves }}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card report-card">
                        <div class="card-header bg-soft-primary">
                            Total Leaves
                        </div>
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold">Total Leaves</p>
                                    <h3 class="m-0">{{ dashboard.leavePerLeaveYear }}</h3>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"
                                             fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"
                                             stroke-linejoin="round"
                                             class="feather feather-tag align-self-center text-muted icon-sm">
                                            <path d="M20.59 13.41l-7.17 7.17a2 2 0 0 1-2.83 0L2 12V2h10l8.59 8.59a2 2 0 0 1 0 2.82z">
                                            </path>
                                            <line x1="7" y1="7" x2="7.01" y2="7"></line>
                                        </svg>
                                    </div>
                                </div>
                            </div>
                            <hr class="hr-dashed">
                            <div class="text-center">
                                <h6 class="text-primary bg-soft-primary p-2 m-0 font-11 rounded">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"
                                         fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"
                                         stroke-linejoin="round"
                                         class="feather feather-calendar align-self-center icon-xs me-1">
                                        <rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect>
                                        <line x1="16" y1="2" x2="16" y2="6"></line>
                                        <line x1="8" y1="2" x2="8" y2="6"></line>
                                        <line x1="3" y1="10" x2="21" y2="10"></line>
                                    </svg>
                                    {{ dashboard.leavePeriod }}
                                </h6>
                            </div>
                        </div><!--end card-body-->
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header bg-soft-primary">
                            <span class="me-2"><i class="mdi mdi-calendar"> </i> </span> Calender
                        </div>
                        <div class="card-body">
                            <div class='demo-app'>
                                <!-- <div class='demo-app-sidebar'>
                                    <div class='demo-app-sidebar-section'>
                                        <h2>Instructions</h2>
                                        <ul>
                                            <li>Select dates and you will be prompted to create a new event</li>
                                            <li>Drag, drop, and resize events</li>
                                            <li>Click an event to delete it</li>
                                        </ul>
                                    </div>
                                    <div class='demo-app-sidebar-section'>
                                        <label>
                                            <input type='checkbox' :checked='calendarOptions.weekends' hidden
                                                @change='handleWeekendsToggle' />
                                        </label>
                                    </div>
                                    <div class='demo-app-sidebar-section'>
                                        <h2>All Events ({{ currentEvents.length }})</h2>
                                        <ul>
                                            <li v-for='event in currentEvents' :key='event.id'>
                                                <b>{{ event.startStr }}</b>
                                                <i>{{ event.title }}</i>
                                            </li>
                                        </ul>
                                    </div>
                                </div> -->
                                <div class='demo-app-main'>
                                    <!--<FullCalendar class='demo-app-calendar' :options='calendarOptions'>
                                        <template v-slot:eventContent='arg'>
                                            <b>{{ arg.timeText }}</b>
                                            <i>{{ arg.event.title }}</i>
                                        </template>
                                    </FullCalendar>-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid" v-else>
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('UserScreen.StartScreen') }}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item">
                                    <a href="javascript:void(0);"
                                       v-on:click="GoTo('/Dashboard1', 'DayStart_token')">
                                        {{ $t('UserScreen.Dashboard') }}
                                    </a>
                                </li>
                                <li class="breadcrumb-item active">{{ $t('UserScreen.StartScreen') }}</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 col-sm-10 mx-auto">
                <div class="mx-auto start_screen_img" >
                    <img src="NobleMainScreen.png"  />
                </div>
            </div>
        </div>
    </div>
</template>

<script>


    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    // import { INITIAL_EVENTS, createEventId } from '../../Mixins/event.js'

    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                kg: '',
                role: '',
                dashboard: [],
                currentEvents: [],
                isProceed: false,
                isAccount: false,
                terms: false,
                isEmployee: true,
            }
        },
        methods: {

            getDate: function (date) {
                return moment(date).format('LL');
            },

            GoTo: function (link, token) {
                this.$router.push({ path: link, query: { token_name: token, fromDashboard: 'true' } });
            },

            GetHrEmployeeDashboard: function (val) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Hr/HrEmployeeDashboard?userId=' + val, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.dashboard = response.data;
                        root.dashboard.holidays.forEach(item => {
                            root.calendarOptions.events.push(item);
                        })
                    }
                });
            },

            handleWeekendsToggle() {
                this.calendarOptions.weekends = !this.calendarOptions.weekends // update a property
            },






            // handleDateSelect(selectInfo) {
            //     let title = prompt('Please enter a new title for your event')
            //     let calendarApi = selectInfo.view.calendar
            //     calendarApi.unselect() // clear date selection
            //     if (title) {
            //         calendarApi.addEvent({
            //             id: createEventId(),
            //             title,
            //             start: selectInfo.startStr,
            //             end: selectInfo.endStr,
            //             allDay: selectInfo.allDay
            //         })
            //     }
            // },
            // handleEventClick(clickInfo) {
            //     if (confirm(`Are you sure you want to delete the event '${clickInfo.event.title}'`)) {
            //         clickInfo.event.remove()
            //     }
            // },
            // handleEvents(events) {
            //     this.currentEvents = events
            // }
        },
        mounted() {
            // Assuming you have jQuery
            
        },
        created: function () {
            this.role = localStorage.getItem('RoleName');
            var userId = localStorage.getItem('EmployeeId');
            
            if (this.role == 'HrUser') {
                this.GetHrEmployeeDashboard(userId);
            }
        },

    }
</script>