<template>
    <div class="row" v-if="isValid('CanAddHolidaySetup')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddHolidayOfMonth.HolidaysSetup') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('AddSalaryTemplate.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row mb-5">
                <div class="col-lg-6">

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddHolidayOfMonth.InTime') }} : <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <vue-timepicker v-model="holidays.inTime" format="hh:mm A"  input-width="100%" />
                        </div>
                    </div>


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddHolidayOfMonth.WeekHoliday') }}  :</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-model="processContractors" @update:modelValue="contractorListId(processContractors)" :options="options" :multiple="true" placeholder="Select Days" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '">
                            </multiselect>
                        </div>
                    </div>

                </div>
                <div class="col-lg-6">



                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddHolidayOfMonth.OutTime') }} :</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <vue-timepicker v-model="holidays.outTime" format="hh:mm A"  input-width="100%" />
                        </div>
                    </div>

                </div>

                <hr class="hr-dashed hr-menu mt-0" />

                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="width: 5%;">
                                            #
                                        </th>
                                        <th style="width:60%;">{{ $t('AddHolidayOfMonth.Description') }}</th>
                                        <th style="width:25%;">{{ $t('AddHolidayOfMonth.Date') }}</th>
                                        <th style="width:10%;">{{ $t('AddHolidayOfMonth.Action') }}</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(holiday,index) in holidays.guestedHolidays" v-bind:key="holiday.id">
                                        <td>{{index+1}}</td>
                                        <td>
                                            <input class="form-control" v-model="holiday.description" v-bind:placeholder="$t('AddLineItem.WriteHere')" />
                                        </td>


                                        <td>
                                            <datepicker v-model="holiday.date"></datepicker>
                                        </td>
                                        <td class="text-center" v-if="index == holidays.guestedHolidays.length - 1 && addItem == false">
                                            <button title="Add New Item" class="btn btn-sm btn-danger btn-icon btn-round" v-on:click="addDailyExpense">
                                                <i class="fa fa-plus"></i>
                                            </button>
                                        </td>
                                        <td class="text-center" v-else>
                                            <a href="javascript:void(0);" @click="removeExpense(holiday.id)">
                                                <i class="las la-trash-alt text-secondary font-16"></i>
                                            </a>

                                        </td>



                                    </tr>
                                    <tr v-if="addItem">
                                        <td class="">{{holidays.guestedHolidays.length+1}} </td>
                                        <td>
                                            <input class="form-control" v-model="holidayItem.description" v-bind:placeholder="$t('AddLineItem.WriteHere')" style="width:100% !important;height:40px;" />
                                        </td>
                                        <td>
                                            <datepicker v-model="holidayItem.date" :key="refresh"></datepicker>
                                        </td>


                                        <td class="text-center" v-if="holidayItem.date==''  ">
                                            <button title="Add New Item" class="btn btn-sm  btn-icon btn-danger btn-round" disabled v-on:click="addDailyExpense">
                                                <i class="fa fa-plus"></i>
                                            </button>
                                        </td>
                                        <td class="text-center" v-else>
                                            <button title="Add New Item" class="btn btn-sm  btn-icon btn-danger btn-round" v-on:click="addDailyExpense">
                                                <i class="fa fa-plus"></i>
                                            </button>
                                        </td>


                                    </tr>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-lg-12 invoice-btn-fixed-bottom ">

                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveHolidays"
                                v-if="holidays.id=='00000000-0000-0000-0000-000000000000'  "
                                >
                            <i class="far fa-save"></i>{{ $t('AddHolidayOfMonth.Save') }}
                        </button>

                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveHolidays"
                                v-if="holidays.id!='00000000-0000-0000-0000-000000000000'  "
                                >
                            <i class="far fa-save"></i> {{ $t('AddHolidayOfMonth.Update') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="GoToHolidays">
                            {{ $t('AddHolidayOfMonth.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    //import VueCtkDateTimePicker from 'vue-ctk-date-time-picker';
    //import 'vue-ctk-date-time-picker/dist/vue-ctk-date-time-picker.css';
    //import Vue from 'vue'
    //Vue.component('VueCtkDateTimePicker', VueCtkDateTimePicker);
    import VueTimepicker from 'vue3-timepicker/src/VueTimepicker.vue'
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    //import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    export default ({

        mixins: [clickMixin],
        components: {
            Multiselect,
            VueTimepicker,
            Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                show: false,
                addItem: false,
                weekDays: '',
                inTime: '',
                outTime: '',
                options: [],
                processContractors: [],


                active: 'personal',
                rendered: 0,
                holidays: {
                    id: '00000000-0000-0000-0000-000000000000',
                    inTime: '',
                    outTime: '',
                    weekHolidays: [],
                    guestedHolidays: [],
                },
                holidayItem: {
                    id: '',
                    description: '',
                    date: '',
                },
                loading: false,
                tableLength: 0,
                cardLength: 0,
                refresh: 0,
                randerTime: 0,
            }
        },
        //validations() {
        //    holidays:
        //    {
        //        code: {

        //        },


        //    }
        //},

        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            contractorListId: function (value) {
                var root = this;
                root.holidays.weekHolidays = value;

            },
            getData: function (x, value) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Payroll/WeekDaysList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.name,
                                nameArabic: result.nameArabic,
                            })
                            if (x) {

                                if (value != undefined) {
                                    value.forEach(function (ids) {

                                        if (ids.id == result.id) {

                                            root.processContractors.push({
                                                id: result.id,
                                                name: result.name,
                                                nameArabic: result.nameArabic,
                                            });
                                            root.contractorListId(root.processContractors);
                                        }
                                    })

                                }
                            }

                        })
                    }


                    //if (response.data != null) {
                    //    response.data.results.forEach(function (cat) {


                    //        root.options.push({
                    //            id: cat.id,
                    //            name: cat.name,
                    //            nameArabic: cat.nameArabic,
                    //        })


                    //    })
                    //}
                });
            },

            addDailyExpense: function () {

                this.loading = true;
                this.holidays.guestedHolidays.push({
                    id: this.createUUID(),
                    date: this.holidayItem.date,
                    description: this.holidayItem.description
                });

                this.holidayItem = {
                    id: '',
                    date: '',
                    description: '',
                };
                this.refresh += 1;
            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },

            removeExpense: function (id) {

                var ds = this.holidays.guestedHolidays.findIndex(function (i) {
                    return i.id === id;
                });

                this.holidays.guestedHolidays.splice(ds, 1);
            },
            GoToHolidays: function () {
                this.$router.push('/ManualAttendance');
            },
            GetHolidays: function () {
                var root = this;
                var id = '';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }



                
                root.$https.get('/Payroll/HolidaysDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {





                    if (response.data != null && response.data != '') {

                        root.holidays = response.data;

                        //root.inTime = moment(root.holidays.inTime).format('llll');
                        //root.outTime = moment(root.holidays.outTime).format('llll');
                        root.getData(true, root.holidays.weekHolidays);

                    }
                    else {
                        root.getData(false);

                    }


                });

            },
            SaveHolidays: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                //this.holidays.inTime = this.holidays.inTime.hh + ':' + this.holidays.inTime.mm ;
                //this.holidays.outTime = this.holidays.outTime.hh + ':' + this.holidays.outTime.mm ;
                this.$https
                    .post('/Payroll/SaveHolidays', this.holidays, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal.fire({
                            icon: 'success',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                            showConfirmButton: false,

                            timer: 800,
                            timerProgressBar: true,

                        });

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },

        },
        created: function () {

            this.$emit('update:modelValue', this.$route.name);

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

            //if (this.$route.query.data == undefined) {
            //    console.log("Best of luck ");
            //}
            //if (this.$route.query.data != undefined) {

            //    this.holidays = this.$route.query.data;

            //}
        },
        mounted: function () {
            this.holidays.inTime = moment().format('llll');
            this.holidays.outTime = moment().format('llll');
            this.GetHolidays();

            this.addItem = this.holidays.guestedHolidays.length > 0 ? false : true;

        }
    })

</script>