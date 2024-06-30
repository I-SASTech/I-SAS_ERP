<template>
    <div class="row" v-if="isValid('CanViewManualAttendance')">        

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('ManualAttendance.ManualAttendance') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ManualAttendance.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('ManualAttendance.ManualAttendance') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-on:click="GotoPage('/AttendanceReport')" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1" v-if="isValid('CanViewAttendanceReport')">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            {{ $t('ManualAttendance.AttendanceReport') }}
                        </a>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                            {{ $t('ProductMaster.Close') }}
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card " v-if="daysList.length==0">
                <dir class=".col-sm-3 offset-sm-4">
                    <h6> {{ $t('ManualAttendance.WeekHoliday') }}</h6>
                    <a v-on:click="GotoPage('/AddHolidayOfMonth')" href="javascript:void(0);" class="btn btn-outline-primary mx-1 ">
                        <i class="align-self-center icon-xs ti-plus"></i>
                        {{ $t('ManualAttendance.HolidaysSetup') }}
                    </a>
                </dir>
                
            </div>
    <div class="card" v-else>
        <div class="card-header">
            <h4 class="page-title text-center">{{currentMonth}}--{{nextMonth}}</h4>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <table class="table mb-0">
                    <thead class="thead-light table-hover">
                        <tr>
                            <th rowspan="2">
                                {{ $t('ManualAttendance.User') }}
                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[0].dayName}} <br />
                                                {{convertDate(daysList[0].weekDate)}}
                                                <br />
                                                {{daysList[0].holidayType}}

                                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[1].dayName}} <br />
                                                {{convertDate(daysList[1].weekDate)}}
                                                <br />
                                                {{daysList[1].holidayType}}
                                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[2].dayName}} <br />
                                                {{convertDate(daysList[2].weekDate)}}
                                                <br />
                                                {{daysList[2].holidayType}}
                                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[3].dayName}} <br />
                                                {{convertDate(daysList[3].weekDate)}}
                                                <br />
                                                {{daysList[3].holidayType}}
                                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[4].dayName}} <br />
                                                {{convertDate(daysList[4].weekDate)}}
                                                <br />
                                                {{daysList[4].holidayType}}
                                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[5].dayName}} <br />
                                                {{convertDate(daysList[5].weekDate)}}
                                                <br />
                                                {{daysList[5].holidayType}}
                                            </th>
                                            <th class="text-center" colspan="2">
                                                {{daysList[6].dayName}} <br />
                                                {{convertDate(daysList[6].weekDate)}}
                                                <br />
                                                {{daysList[6].holidayType}}
                                            </th>
                        </tr>
                        <tr>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.In') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('ManualAttendance.Out') }}
                                            </th>
                                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(employee) in employeelist" v-bind:key="employee.id">
                                            <td>
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EmployeeAttendence(employee.id)">   {{employee.englishName}}</a>
                                                </strong>

                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[0].isOnLeave">{{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[0].isAbsent">{{ $t('ManualAttendance.Absent') }}<br /></span>

                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" v-bind:id="employee.id + 'inlineCheckbox1'" @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[0].id,daysList[0].weekDate,employee.attendence[0])" v-model="employee.attendence[0].isCheckIn" v-bind:disabled="daysList[0].disable">
                                                    <label v-bind:for="employee.id + 'inlineCheckbox1'">  </label>
                                                </div>
                                                
                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[0].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[0].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" v-bind:id="employee.id + 'inlineCheckbox2'"   @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[0].id,daysList[0].weekDate,employee.attendence[0])" v-model="employee.attendence[0].isCheckOut" v-bind:disabled="daysList[0].disable">
                                                <label v-bind:for="employee.id + 'inlineCheckbox2'">  </label>
                                                </div>
                                            </td>



                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[1].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[1].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                               
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox3'"  @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[1].id,daysList[1].weekDate,employee.attendence[1])" v-model="employee.attendence[1].isCheckIn" v-bind:disabled="daysList[1].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox3'">  </label>
                                                </div>
                                               
                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[1].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[1].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox4'"  @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[1].id,daysList[1].weekDate,employee.attendence[1])" v-model="employee.attendence[1].isCheckOut" v-bind:disabled="daysList[1].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox4'">  </label>
                                                </div>
                                            </td>



                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[2].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[2].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" v-bind:id="employee.id + 'inlineCheckbox5'"   @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[2].id,daysList[2].weekDate,employee.attendence[2])" v-model="employee.attendence[2].isCheckIn" v-bind:disabled="daysList[2].disable" >
                                                <label  v-bind:for="employee.id + 'inlineCheckbox5'">  </label>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[2].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[2].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox6'"  @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[2].id,daysList[2].weekDate,employee.attendence[2])" v-model="employee.attendence[2].isCheckOut" v-bind:disabled="daysList[2].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox6'">  </label>
                                                </div>
                                            </td>




                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[3].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[3].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox7'"  @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[3].id,daysList[3].weekDate,employee.attendence[3])" v-model="employee.attendence[3].isCheckIn" v-bind:disabled="daysList[3].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox7'">  </label>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[3].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[3].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox8'"  @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[3].id,daysList[3].weekDate,employee.attendence[3])" v-model="employee.attendence[3].isCheckOut" v-bind:disabled="daysList[3].disable" value="">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox8'">  </label>
                                                </div>
                                            </td>



                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[4].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[4].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                               
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox9'" @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[4].id,daysList[4].weekDate,employee.attendence[4])" v-model="employee.attendence[4].isCheckIn" v-bind:disabled="daysList[4].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox9'">  </label>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[4].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[4].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox10'" @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[4].id,daysList[4].weekDate,employee.attendence[4])" v-model="employee.attendence[4].isCheckOut" v-bind:disabled="daysList[4].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox10'">  </label>
                                                </div>
                                            </td>



                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[5].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[5].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                               
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox11'"  @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[5].id,daysList[5].weekDate,employee.attendence[5])" v-model="employee.attendence[5].isCheckIn" v-bind:disabled="daysList[5].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox11'">  </label>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[5].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[5].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                               
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox12'" @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[5].id,daysList[5].weekDate,employee.attendence[5])" v-model="employee.attendence[5].isCheckOut" v-bind:disabled="daysList[5].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox12'">  </label>
                                                </div>
                                            </td>



                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[6].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[6].isAbsent"> {{ $t('ManualAttendance.Absent') }}<br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" v-bind:id="employee.id + 'inlineCheckbox13'"   @update:modelValue="SaveManualAttendenceRecord(employee,'checkIn',employee.attendence[6].id,daysList[6].weekDate,employee.attendence[6])" v-model="employee.attendence[6].isCheckIn" v-bind:disabled="daysList[6].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox13'">  </label>
                                                </div>

                                            </td>
                                            <td class="text-center">
                                                <span style="color:red;font-size:8px" v-if="employee.attendence[6].isOnLeave"> {{ $t('ManualAttendance.OnLeave') }}<br /></span>
                                                <span style="color:green;font-size:8px" v-else-if="employee.attendence[6].isAbsent">{{ $t('ManualAttendance.Absent') }} <br /></span>
                                                
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox"  v-bind:id="employee.id + 'inlineCheckbox14'"   @update:modelValue="SaveManualAttendenceRecord(employee,'checkOut',employee.attendence[6].id,daysList[6].weekDate,employee.attendence[6])" v-model="employee.attendence[6].isCheckOut" v-bind:disabled="daysList[6].disable">
                                                <label  v-bind:for="employee.id + 'inlineCheckbox14'">  </label>
                                                </div>
                                            </td>

                                        </tr>
                    </tbody>
                </table>
            </div>
            
        </div>
    </div>

    <TodayAttendenceModel :manualattendence="manualAttendence"
                                  :show="show"
                                  v-if="show"
                                  @close="IsSave"
                                  :type="type" />
</div>

</div>
<div v-else> <acessdenied></acessdenied></div>
</template>

<script>
    import moment from 'moment';
    //import Checkbox from 'vue-material-checkbox'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        //components: { Checkbox },
        mixins: [clickMixin],
        data: function () {
            return {
                month: '',
                value: false,
                checkbox: false,
                currentMonth: '',
                nextMonth: '',
                year: '',
                arabic: '',
                english: '',
                searchQuery: '',
                employeeCheckIn: [],
                employeelist: [],
                daysList: [],
                search: '',
                show: false,

                manualAttendence: {
                    id: '00000000-0000-0000-0000-000000000000',
                    checkIn: '',
                    date: '',
                    isOnLeave: false,
                    isPreviousAttendence: true,
                    isCheckIn: false,
                    isCheckOut: false,
                    checkOut: '',
                    employeeId: '',
                    checkType: '',
                    description: '',
                },
            }
        },


        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            IsSave: function () {
                this.GetManualRecord();
            },
            EmployeeAttendence: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/EmployeeOverTimeQuery?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/EmployeeHourOfAttendence',
                            query: {
                                data: '',
                                Add: false
                            }
                        })
                    }
                });
            },
            SaveManualAttendenceRecord: function (employee, checkType, id, weekDate,attendence) {
                
                this.loading = true;

                if (checkType == 'checkIn') {

                    if (id == '00000000-0000-0000-0000-000000000000') {
                        this.manualAttendence = {
                            id: id,
                            checkIn: '',
                            isOnLeave: false,
                            isAbsent: false,
                            isCheckIn: true,
                            isCheckOut: true,
                            isPreviousAttendence: true,
                            checkOut: '',
                            date: weekDate,
                            employeeId: employee.id,
                            checkType: checkType,
                            description: '',

                        }
                        this.show = !this.show;
                        this.type = "Add";
                    }
                    else {
                        this.manualAttendence.id = id;
                        this.manualAttendence.employeeId = employee.id;
                        this.manualAttendence.checkType = 'Not Check';
                        this.manualAttendence.checkIn = attendence.checkIn;
                        this.manualAttendence.date = attendence.date;
                        this.manualAttendence.checkOut = attendence.checkOut;
                        this.manualAttendence.checkOut = attendence.checkOut;
                        this.manualAttendence.isCheckIn = attendence.isCheckIn;
                        this.manualAttendence.isCheckOut = attendence.isCheckOut;
                        this.manualAttendence.isAbsent = attendence.isAbsent;
                        this.manualAttendence.isOnLeave = attendence.isOnLeave;
                       
                        this.show = !this.show;
                        this.type = "Edit";
                    }



                }
                if (checkType == 'checkOut') {
                    if (id == '00000000-0000-0000-0000-000000000000') {
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Attendence',
                                text: 'First Click on Check In !',
                            });
                        this.GetManualRecord();

                        return;

                    }
                    else {
                        this.manualAttendence.id = id;
                        this.manualAttendence.employeeId = employee.id;
                        this.manualAttendence.checkType = checkType;
                        this.manualAttendence.id = id;
                        this.manualAttendence.employeeId = employee.id;
                        this.manualAttendence.checkType = 'Not Check';
                        this.manualAttendence.checkIn = attendence.checkIn;
                        this.manualAttendence.isCheckIn = attendence.isCheckIn;
                        this.manualAttendence.isCheckOut = attendence.isCheckOut;
                        this.manualAttendence.isOnLeave = attendence.isOnLeave;
                        this.manualAttendence.isAbsent = attendence.isAbsent;
                        this.manualAttendence.date = attendence.date;
                        if (this.manualAttendence.isCheckOut) {
                            this.manualAttendence.checkOut = attendence.checkOut;
                        }
                        else {
                            this.manualAttendence.checkOut = '';
                        }
                        this.show = !this.show;
                        this.type = "Edit";
                    }

                }

                

                //var root = this;
                //var token = '';
                //if (token == '') {
                //    token = localStorage.getItem('token');
                //}
                //this.manualAttendence.id = id;
                //this.manualAttendence.employeeId = employee.id;
                //this.manualAttendence.checkType = checkType;
                //if (checkType == 'checkIn') {
                //    this.manualAttendence.isCheckIn = true;
                //}
                //if (checkType == 'checkOut') {
                //    this.manualAttendence.isCheckOut = true;
                //}
                //this.$https
                //    .post('/Payroll/SaveManualAttendence', this.manualAttendence, { headers: { "Authorization": `Bearer ${token}` } })
                //    .then(response => {
                //        root.loading = false
                //        root.info = response.data.bpi

                //        root.$swal.fire({
                //            icon: 'success',
                //            title: 'Saved Successfully',
                //            showConfirmButton: false,

                //            timer: 800,
                //            timerProgressBar: true,

                //        });
                //        root.GetManualRecord();
                //    })
                //    .catch(error => {
                //        
                //        console.log(error)
                //        root.$swal.fire(
                //            {
                //                icon: 'error',
                //                title: error.response.data,
                //                text: 'You Enter Wrong Steps',
                //            });

                //        root.loading = false
                //    })
                //    .finally(() => root.loading = false)
            },
            convertDate: function (date) {
                return moment(date).format('DD MMM YYYY');
            },

            GetManualRecord: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.show = false;
                root.$https.get('Payroll/ManualList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.month = response.data.month;
                        root.daysList = response.data.dayOfWeekLookUpModel;
                        root.employeelist = response.data.employeeManualAttendence;
                        root.employeeCheckIn = response.data.employeeCheckIn;

                    }
                });
            },

        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
            this.GetManualRecord();
            //this.GetEmployeeData(this.search);


        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currentMonth = moment().format('MMM')
            this.nextMonth = moment().add(1, 'M').format("MMM");
        }
    }
</script>

<style scoped>
    .tbl_head tr th {
        padding-bottom: 0;
    }
</style>