<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{ $t('AddManualAttendance.UpdateManualAttendance') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddManualAttendance.AddManualAttendance') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-6 ">

                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="attendence.isCheckIn"
                                   v-on:change="GetChangeVlaue(attendence.isCheckIn, 'checkIn')">
                            <label for="inlineCheckbox1">{{ $t('AddManualAttendance.CheckIn') }} : </label>
                        </div>

                    </div>
                    <div class="form-group has-label col-sm-6 ">

                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox2" v-model="attendence.isOnLeave"
                                   v-on:change="GetChangeVlaue(attendence.isOnLeave, 'onLeave')">
                            <label for="inlineCheckbox2">{{ $t('AddManualAttendance.OnLeave') }} : </label>
                        </div>

                    </div>
                    <div class="form-group has-label col-sm-6 ">

                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox3" v-model="attendence.isAbsent"
                                   v-on:change="GetChangeVlaue(attendence.isAbsent, 'isAbsent')">
                            <label for="inlineCheckbox3"> {{ $t('AddManualAttendance.Absent') }} :  </label>
                        </div>

                    </div>
                    <div class="form-group has-label col-sm-12 " v-if="attendence.isPreviousAttendence">
                        <label class="text  font-weight-bolder"> {{ $t('AddManualAttendance.InTime') }} : </label>
                        <vue-timepicker v-model="attendence.checkIn" input-width="100%" />
                    </div>
                    <div class="form-group has-label col-sm-12 " v-if="attendence.isPreviousAttendence">
                        <label class="text  font-weight-bolder"> {{ $t('AddManualAttendance.OutTime') }} : </label>
                        <vue-timepicker v-model="attendence.checkOut" input-width="100%" />
                    </div>



                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">{{ $t('AddManualAttendance.Description') }} : </label>
                        <textarea class="form-control" v-model="attendence.description" type="text" />
                    </div>


                </div>








            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="Saveattendence"
                        v-bind:disabled="v$.attendence.$invalid" v-if="type != 'Edit'">
                    {{ $t('AddManualAttendance.SaveAttendance') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="Saveattendence"
                        v-bind:disabled="v$.attendence.$invalid" v-if="type == 'Edit'">
                    {{ $t('AddManualAttendance.UpdateAttendance') }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()"> {{ $t('AddManualAttendance.Cancel') }}</button>
            </div>
        </div>

        <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>




    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'    
    
    import VueTimepicker from 'vue3-timepicker/src/VueTimepicker.vue'
    import moment from 'moment';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['show', 'manualattendence', 'type'],
        mixins: [clickMixin],
        components: {
            
            VueTimepicker,
            Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                randerVal: 0,
                loading: false,
                attendence: {},
            }
        },
        methods: {
            GetChangeVlaue: function (isValue, type) {

                if (type == 'checkIn') {
                    if (isValue) {
                        this.attendence.isOnLeave = false;
                        this.attendence.isAbsent = false;

                    }
                }
                if (type == 'onLeave') {
                    if (isValue) {
                        this.attendence.isCheckIn = false;
                        this.attendence.isAbsent = false;

                    }
                }
                if (type == 'isAbsent') {
                    if (isValue) {
                        this.attendence.isCheckIn = false;
                        this.attendence.isOnLeave = false;

                    }
                }
                this.randerVal++;
            },
            close: function () {
                this.$emit('close');
            },

            Saveattendence: function () {

                var root = this;
                this.loading = true;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.attendence.isPreviousAttendence) {
                    if (!this.attendence.isOnLeave && !this.attendence.isAbsent) {
                        if (this.attendence.checkIn == '') {
                            this.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Attendence',
                                    text: 'Check In is Required !',
                                });
                            this.loading = false;
                            return;
                        }
                    }
                    if (this.attendence.checkOut == '') {
                        this.attendence.isCheckOut = false;
                    }
                    else {
                        this.attendence.isCheckOut = true;
                        this.attendence.checkOut = moment(this.attendence.date).format('DD MMM YYYY') + ' ' + this.attendence.checkOut;

                    }

                    this.attendence.checkIn = moment(this.attendence.date).format('DD MMM YYYY') + ' ' + this.attendence.checkIn;




                }
                this.$https
                    .post('/Payroll/SaveManualAttendence', this.attendence, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal.fire({
                            icon: 'success',
                            title: 'Saved Successfully',
                            showConfirmButton: false,

                            timer: 800,
                            timerProgressBar: true,

                        });
                        root.close();
                    })
                    .catch(error => {

                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: error.response.data,
                                text: 'You Enter Wrong Steps',
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            }
        },
        created: function () {
            this.attendence = this.manualattendence;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');


        }
    }
</script>
