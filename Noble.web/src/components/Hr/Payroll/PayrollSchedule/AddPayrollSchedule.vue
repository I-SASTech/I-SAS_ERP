<template>
     <modal :show="show" :modalLarge="true" v-if="isValid('CanAddPayRollSchedule') || isValid('CanEditPayRollSchedule')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{
                        $t('AddPayrollSchedule.UpdatePayrollSchedule')
                }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddPayrollSchedule.AddPayrollSchedule') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-6">
                                        <label class="text  font-weight-bolder">{{ $t('AddPayrollSchedule.ScheduleName') }} : <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="payrollschedule.name" type="text" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayPeriod') }} :<span class="text-danger"> *</span></label>
                                        <multiselect :options="payPeriodOptions" @update:modelValue="GetChangeDate()" v-model="payrollschedule.payPeriod" :show-labels="false" v-bind:placeholder="$t('AddPayrollSchedule.SelectType')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        </multiselect>
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.IfPayDayFallOnHoliday') }} :<span class="text-danger"> *</span></label>
                                        <multiselect :options="ifPayDayFallOnHolidayOptions" v-model="payrollschedule.ifPayDayFallOnHoliday" :show-labels="false" v-bind:placeholder="$t('AddPayrollSchedule.SelectType')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        </multiselect>
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayPeriodStartDate') }} :<span class="text-danger"> *</span></label>
                                        <datepicker v-model="payrollschedule.payPeriodStartDate" v-bind:key="randerDate" @update:modelValue="SetPeriodEndDate(payrollschedule.payPeriodStartDate)" :isDisabled="payrollschedule.payPeriod!='' && payrollschedule.payPeriod!=null?false : true" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayPeriodEndDate') }} :<span class="text-danger"> *</span></label>
                                        <datepicker v-model="payrollschedule.payPeriodEndDate" :isDisable="true" :key="dateRender+randerDate" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayYourEmployeesOnDay') }} :<span class="text-danger"> *</span></label>
                                        <datepicker v-model="payrollschedule.payDate" v-bind:key="randerDate" @update:modelValue="CheckDate(payrollschedule.payPeriodEndDate, payrollschedule.payDate)" />
                                    </div>

                    <div class="form-group col-md-2">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="payrollschedule.default">
                            <label for="inlineCheckbox1"> {{ $t('AddPayrollSchedule.Default') }} </label>
                        </div>
                    </div>
                    <div class="form-group col-md-2">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox2" v-model="payrollschedule.isHourlyPay">
                            <label for="inlineCheckbox2"> {{ $t('AddPayrollSchedule.IsHourlyPay') }} </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePayrollschedule"
                    v-bind:disabled="v$.payrollschedule.$invalid" v-if="type != 'Edit' && isValid('CanAddPayRollSchedule')">{{
                            $t('AddPayrollSchedule.Save')
                    }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePayrollschedule"
                    v-bind:disabled="v$.payrollschedule.$invalid" v-if="type == 'Edit' && isValid('CanEditPayRollSchedule')">{{
                            $t('AddPayrollSchedule.Update')
                    }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('AddPayrollSchedule.Cancel')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
    <!-- <modal :show="show" :modalLarge="true" v-if="isValid('CanAddPayRollSchedule') || isValid('CanEditPayRollSchedule')">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" v-if="type=='Edit'">
                                <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('AddPayrollSchedule.UpdatePayrollSchedule') }}</h5>
                            </div>

                            <div class="modal-header" v-else>
                                <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('AddPayrollSchedule.AddPayrollSchedule') }}</h5>
                            </div>

                            <div class="card-body" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <div class="row ">
                                    <div class="form-group has-label col-sm-6">
                                        <label class="text  font-weight-bolder">{{ $t('AddPayrollSchedule.ScheduleName') }} : <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="payrollschedule.name" type="text" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayPeriod') }} :<span class="text-danger"> *</span></label>
                                        <multiselect :options="payPeriodOptions" @update:modelValue="GetChangeDate()" v-model="payrollschedule.payPeriod" :show-labels="false" v-bind:placeholder="$t('AddPayrollSchedule.SelectType')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        </multiselect>
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.IfPayDayFallOnHoliday') }} :<span class="text-danger"> *</span></label>
                                        <multiselect :options="ifPayDayFallOnHolidayOptions" v-model="payrollschedule.ifPayDayFallOnHoliday" :show-labels="false" v-bind:placeholder="$t('AddPayrollSchedule.SelectType')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        </multiselect>
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayPeriodStartDate') }} :<span class="text-danger"> *</span></label>
                                        <datepicker v-model="payrollschedule.payPeriodStartDate" v-bind:key="randerDate" @update:modelValue="SetPeriodEndDate(payrollschedule.payPeriodStartDate)" :isDisabled="payrollschedule.payPeriod!='' && payrollschedule.payPeriod!=null?false : true" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayPeriodEndDate') }} :<span class="text-danger"> *</span></label>
                                        <datepicker v-model="payrollschedule.payPeriodEndDate" :isDisable="true" :key="dateRender+randerDate" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.PayYourEmployeesOnDay') }} :<span class="text-danger"> *</span></label>
                                        <datepicker v-model="payrollschedule.payDate" v-bind:key="randerDate" @update:modelValue="CheckDate(payrollschedule.payPeriodEndDate, payrollschedule.payDate)" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.Default') }} :<span class="text-danger"> *</span></label>
                                        <toggle-button v-model="payrollschedule.default" class="pr-2 pl-2 pt-2" color="#3178F6" />
                                    </div>

                                    <div class="form-group has-label col-sm-6">
                                        <label>{{ $t('AddPayrollSchedule.IsHourlyPay') }} :<span class="text-danger"> *</span></label>
                                        <toggle-button v-model="payrollschedule.isHourlyPay" class="pr-2 pl-2 pt-2" color="#3178F6" />
                                    </div>
                                </div>
                            </div>

                            <div v-if="!loading">
                                <div class="modal-footer justify-content-right" v-if="type=='Edit' && isValid('CanEditPayRollSchedule')">
                                    <button type="button" class="btn btn-primary  " v-on:click="SavePayrollschedule" v-bind:disabled="v$.payrollschedule.$invalid || dateCheck"> {{ $t('AddPayrollSchedule.Update') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddPayrollSchedule.Cancel') }}</button>
                                </div>
                                <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('CanAddPayRollSchedule')">
                                    <button type="button" class="btn btn-primary  " v-on:click="SavePayrollschedule" v-bind:disabled="v$.payrollschedule.$invalid || dateCheck"> {{ $t('AddPayrollSchedule.Save') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddPayrollSchedule.Cancel') }}</button>
                                </div>
                            </div>
                            <div v-else>
                                <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied> -->
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { required} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import Multiselect from 'vue-multiselect'
    import moment from "moment";

    export default {
        mixins: [clickMixin],
        props: ['show', 'newPayrollSchedule', 'type'],
        components: {
            Loading,
            Multiselect
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                payrollschedule: this.newPayrollSchedule,
                currency: '',
                arabic: '',
                english: '',
                loading: false,
                dateCheck: false,
                payPeriodOptions:[],
                ifPayDayFallOnHolidayOptions: [],
                dateRender:0,
                randerDate:0,
            }
        },
        validations() {
          return{
              payrollschedule: {
                name: {
                    required
                },
                payPeriod: {
                    required
                },
                payPeriodStartDate: {
                    required
                },
                payPeriodEndDate: {
                    required
                },
                ifPayDayFallOnHoliday: {
                    required
                },
                payDate: {
                    required
                },
            }
          }
        },
        methods: {
            GetChangeDate: function () {
                this.payrollschedule.payPeriodStartDate = '';
                this.payrollschedule.payPeriodEndDate = '';
                this.payrollschedule.payDate = '';
                this.randerDate++;
            },
            close: function () {
                this.$emit('close');
            },

            CheckDate: function (endDate, date) {
                var root = this;
                root.dateCheck = moment(endDate).isAfter(date)
                if (root.dateCheck) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: 'Pay date equal or greater then pay period end date',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        icon: 'error',
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
            },

            SetPeriodEndDate: function (x) {
                
                if (x!='') {
                    if (this.payrollschedule.payPeriod == 'Fortnightly') {
                        this.payrollschedule.payPeriodEndDate = moment(this.payrollschedule.payPeriodStartDate).add(13, 'days').format("DD MMM YYYY");
                    }
                    else {
                        this.payrollschedule.payPeriodEndDate = moment(this.payrollschedule.payPeriodStartDate).add(1, 'M').format('DD MMM YYYY');
                    }
                    this.dateRender++;
                }                
            },

            SavePayrollschedule: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                

                this.$https.post('/Payroll/SavePayrollSchedule', this.payrollschedule, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();

                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your Brand Name Already Exist!' : 'اسم علامتك التجارية موجود بالفعل!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {

                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        created: function () {
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.payPeriodOptions = ['Per Month', 'Fortnightly'];
                this.ifPayDayFallOnHolidayOptions = ['Run Payroll Earlier', 'Run Payroll Later'];
            }
            //else {
            //    this.calculateAmountOptions = ['٪ من الراتب', 'مثبت'];
            //    this.taxOptions = ['خاضع للضريبة', 'غير خاضعة للضريبة'];
            //}

        }
    }
</script>
