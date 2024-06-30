<template>
    <modal show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 v-if="type != 'Edit'" class="modal-title m-0" id="exampleModalDefaultLabel">Leaves Types</h6>
                <h6 v-if="type == 'Edit'" class="modal-title m-0" id="exampleModalDefaultLabel">Update Leaves Types</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-body">
                                <form id="form-horizontal" class="form-horizontal form-wizard-wrapper wizard clearfix"
                                    role="application">
                                    <div class="steps clearfix">
                                        <ul role="tablist">
                                            <li role="tab" class="done" v-bind:class="{'current': wizards === '1' }">
                                                <a v-on:click="wizards = '1'"><span
                                                        class="number">1.</span> Basic</a>
                                            </li>
                                            <li role="tab" class="done mx-5" v-bind:class="{'current': wizards === '2' }">
                                                <a v-on:click="wizards = '2'"><span class="number">2.</span>
                                                    Carry Forward</a></li>
                                            <li role="tab" class="done" v-bind:class="{'current': wizards === '3' }">
                                                <a v-on:click="wizards = '3'"><span class="number">3.</span> Advanced</a></li>
                                        </ul>
                                    </div>
                                    <div class="content clearfix">
                                        <h3 id="form-horizontal-h-0" class="title">Seller Details</h3>
                                        <fieldset v-if="wizards == '1'" role="tabpanel" class="body" style="">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Leave Name:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <input v-model="v$.leaveType.leaveName.$model" type="text" class="form-control border_input">
                                                            <span v-if="v$.leaveType.leaveName.$error" class="error text-danger"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label for="txtLastNameBilling"
                                                            class="col-lg-3 col-form-label text-end pe-3">Leave Color:</label>
                                                        <div class="col-lg-8">
                                                            <input v-model="leaveType.leaveColor" type="color" class="form-control form-control-color border_input" id="exampleColorInput" title="Choose your color">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row text-center">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3"> Leaves Per Leave Period:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <input v-model="leaveType.leavesPerLeavePeriod" type="number" class="form-control border_input">
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Admin can assign leave to employees:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.adminAssignLeave" class="form-select">
                                                                <option disabled selected="">Select Admin can assign leave to employees</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Employees can apply for this leave type:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.employeesApplyForLeaveType" class="form-select">
                                                                <option disabled selected="">Select Employees can apply for this leave type</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Employees can apply beyond the current leave balance:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.beyondCurrentLeaveBalance" class="form-select">
                                                                <option disabled selected="">Select Employees can apply beyond the current leave balance</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </fieldset>

                                        <h3 class="title">Company Document</h3>
                                        <fieldset v-if="wizards == '2'" role="tabpanel" class="body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Leave Carried Forward:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.leaveCarriedForward1" class="form-select">
                                                                <option disabled selected="">Select Leave Carried Forward</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3"> Percentage of Leave Carried Forward:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <input v-model="leaveType.percentageLeaveCF" type="number" class="form-control border_input" >
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Maximum Carried Forward Amount:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <input v-model="leaveType.maximumCFAmount" type="number" class="form-control border_input" >
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Carried Forward Leave Availability Period:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.cfLeaveAvailabilityPeriodToString" class="form-select">
                                                                <option disabled selected="">Select Carried Forward Leave Availability Period</option>
                                                                <option value="1">1 Month</option>
                                                                <option value="2">2 Month</option>
                                                                <option value="3">3 Month</option>
                                                                <option value="4">4 Month</option>
                                                                <option value="5">5 Month</option>
                                                                <option value="6">6 Month</option>
                                                                <option value="7">7 Month</option>
                                                                <option value="8">8 Month</option>
                                                                <option value="9">9 Month</option>
                                                                <option value="10">10 Month</option>
                                                                <option value="11">11 Month</option>
                                                                <option value="12">1 Year</option>
                                                                <option value="13">No Limit</option>
                                                            </select>
                                                        </div>
                                                </div>
                                                </div>
                                            </div><!--end row-->
                                            
                                        </fieldset><!--end fieldset-->

                                        <h3 class="title">Bank Details</h3>
                                        <fieldset v-if="wizards == '3'" role="tabpanel" class="body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Leave Group:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <leavegroupdropdown v-model="leaveType.leaveGroupId" :modelValue="leaveType.leaveGroupId" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Leave Accrue Enabled:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.leaveAccrueEnabled" class="form-select">
                                                                <option disabled selected="">Select Leave Accrue Enabled</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Proportionate leaves on Joined Date:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.proportionateLeaves" class="form-select">
                                                                <option disabled selected="">Select Proportionate leaves on Joined Date</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Use Employee Leave Period:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.employeeLeavePeriod" class="form-select">
                                                                <option disabled selected="">Select Use Employee Leave Period</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>

                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group row">
                                                        <label
                                                            class="col-lg-3 col-form-label text-end pe-3">Send Notification Emails:<span class="text-danger">*</span>
                                                        </label>
                                                        <div class="col-lg-8">
                                                            <select v-model="leaveType.sendNotificationEmails" class="form-select">
                                                                <option disabled selected="">Select Send Notification Emails</option>
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset><!--end fieldset-->
                                    </div>
                                    <div class="actions clearfix">
                                        <ul role="menu" aria-label="Pagination">
                                            <li v-if="wizards == '1'" class="disabled">
                                                <a href="#previous" role="menuitem">Previous</a>
                                            </li>
                                            <li v-if="wizards == '2'">
                                                <a href="#previous" v-on:click="wizards = '1'" role="menuitem">Previous</a>
                                            </li>
                                            <li v-if="wizards == '3'">
                                                <a href="#previous" v-on:click="wizards = '2'" role="menuitem">Previous</a>
                                            </li>
                                            <li  class="" v-if="wizards == '1'">
                                                <a href="#next" v-on:click="wizards = '2'" role="menuitem">Next</a>
                                            </li>
                                            <li  class="" v-if="wizards == '2'">
                                                <a href="#next" v-on:click="wizards = '3'" role="menuitem">Next</a>
                                            </li>
                                            <li  v-if="wizards == '3'">
                                                <button type="button" v-if="type!='Edit'" v-bind:disabled="v$.leaveType.$invalid" class="btn btn-soft-primary"  v-on:click="SaveLeaveType()">Save</button>                
                                                <button type="button" class="btn btn-soft-primary"  v-on:click="SaveLeaveType()" v-bind:disabled="v$.leaveType.$invalid" v-if="type=='Edit'">Update</button>
                                            </li>
                                        </ul>
                                    </div>
                                </form><!--end form-->
                            </div><!--end card-body-->
                        </div><!--end card-->
                    </div><!--end col-->
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
export default {
    props: ['show', 'newleaveType', 'type'],
    mixins: [clickMixin],
    components: {
        Loading,
    },
      setup() {
            return { v$: useVuelidate() }
        },

    data: function () {
        return {
            leaveType: this.newleaveType,
            wizards:'1',
            arabic: '',
            english: '',
            render: 0,
            loading: false,
           
        }
    },
    validations() {
       return{
         leaveType: {
            leaveName: {
                required,
                maxLength: maxLength(30)
            },
        }
       }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveLeaveType:function(){
            var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.leaveType.adminAssignLeave = parseInt(this.leaveType.adminAssignLeave);
                this.leaveType.employeesApplyForLeaveType = parseInt(this.leaveType.employeesApplyForLeaveType);
                this.leaveType.beyondCurrentLeaveBalance = parseInt(this.leaveType.beyondCurrentLeaveBalance);
                this.leaveType.cFLeaveAvailabilityPeriod1 = parseInt(this.leaveType.cfLeaveAvailabilityPeriodToString);
                this.leaveType.leaveAccrueEnabled = parseInt(this.leaveType.leaveAccrueEnabled);
                this.leaveType.leaveCarriedForward1 = parseInt(this.leaveType.leaveCarriedForward1);
                this.leaveType.proportionateLeaves = parseInt(this.leaveType.proportionateLeaves);
                this.leaveType.employeeLeavePeriod = parseInt(this.leaveType.employeeLeavePeriod);
                this.leaveType.sendNotificationEmails = parseInt(this.leaveType.sendNotificationEmails);

    
                this.$https.post('/Hr/SaveLeaveType', this.leaveType, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            if (root.leaveType.id == "00000000-0000-0000-0000-000000000000") {

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
                                text: response.data,
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
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
    }
}
</script>
<style scoped>
ul {
    list-style: none !important;
}
</style>