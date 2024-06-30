<template>
   <div class="row" v-if="isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll') || isValid('CanEditOpenRunPayroll')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                              
                                <h4 class="page-title">{{ $t('AddRunPayroll.AddRunPayroll') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <span class="badge badge-danger" v-if="runPayroll.status"> {{ $t('AddRunPayroll.Close') }}</span>
                                <span class="badge badge-success" v-else>{{ $t('AddRunPayroll.Open') }}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row mb-5">

                <div class="col-lg-6">

                    <div class="row form-group" v-if="runPayroll.id=='00000000-0000-0000-0000-000000000000'">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{
                                    $t('AddRunPayroll.PayPeriod')
                            }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <payroll-schedule-dropdown v-model="runPayroll.payrollScheduleId" :modelValue="runPayroll.payrollScheduleId" @update:modelValue="GetPayrollSchedule" />
                        </div>
                    </div>


                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddRunPayroll.Designation')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <designationDropdown v-model="designationId" @update:modelValue="GetSalaryTemplateData" />
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">

                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddRunPayroll.Department')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <departmentDropdown v-model="departmentId" @update:modelValue="GetSalaryTemplateData" />
                        </div>
                    </div>

                </div>
                
                <hr class="hr-dashed hr-menu mt-0"/>
               
                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('AddRunPayroll.EmployeeEnglish') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.EmployeeArabic') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.BaseSalary') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.Allowance') }}
                                        </th>
                                        <th class="text-center" v-if="runPayroll.hourly">
                                            {{ $t('AddRunPayroll.Hour') }}
                                        </th>
                                        <th v-if="runPayroll.hourly">
                                            {{ $t('AddRunPayroll.HourAmount') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.GrossSalary') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.Deduction') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.CONTRIBUTION') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.IncomeTax') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.Loan') }}
                                        </th>
                                        <th>
                                            {{ $t('AddRunPayroll.NetSalary') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(salaryTemplate ,index) in runPayroll.salaryTemplateList" v-bind:key="salaryTemplate.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>
                                        <td v-if="isValid('CanEditSaleryTemplate')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditSalaryTemplate(salaryTemplate, salaryTemplate.id)">{{salaryTemplate.employeeEnglishName}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>
                                            <strong>
                                                {{salaryTemplate.employeeEnglishName}}
                                            </strong>
                                        </td>
                                        <td>
                                            {{salaryTemplate.employeeArabicName}}
                                        </td>
                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.baseSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>

                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.allowanceAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>


                                        <td v-if="runPayroll.hourly">
                                            <input v-model="salaryTemplate.hour" @keyup="updateLineTotal(salaryTemplate.hour, salaryTemplate)" type="number" @focus="$event.target.select()" class="form-control text-center" />

                                        </td>

                                        <td v-if="runPayroll.hourly">
                                            {{currency}} {{  parseFloat(salaryTemplate.hourAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>

                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.grossSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.deductionAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.contributionAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.incomeTaxAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.loanAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{  parseFloat(salaryTemplate.netSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                       
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                        <hr />
            <div class="row">
                <div class="col-lg-6">
                    <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                    <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to')
                    }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
}}</span>
                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of')
                        }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                </div>
                <div class=" col-lg-6">
                    <div class="overflow-auto float-end" v-on:click="GetBrandData()">
                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                            :per-page="10" first-text="First" prev-text="Previous" next-text="Next"
                            last-text="Last"></b-pagination>
                    </div>
                </div>
            </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-lg-12 invoice-btn-fixed-bottom ">

                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate(false)" 
                        v-if="isValid('CanDraftRunPayroll')"
                           v-bind:disabled="v$.runPayroll.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddRunPayroll.Submit') }}
                        </button>

                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate(true)" 
                        v-if="isValid('CanAddRunPayroll')"
                           v-bind:disabled="v$.runPayroll.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddRunPayroll.SubmitAndApprove') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="Close()">
                            {{ $t('AddRunPayroll.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>


<script>
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    // import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
         setup() {
            return { v$: useVuelidate() }
        },
         components: {
            Loading
        },
        data: function () {
            return {
                 loading: false,
                salaryTemplateId: '',
                designationId: '',
                departmentId: '',
                employeeId: '',
                currency: '',
                searchQuery: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                arabic: '',
                english: '',
                data: '',
                show: false,

                runPayroll: {
                    id: '00000000-0000-0000-0000-000000000000',
                    payrollScheduleId: '',
                    status: false,
                    hourly: false,
                    salaryTemplateList: [],
                }
            }
        },
        validations() {
          return{
              runPayroll:
            {
                payrollScheduleId: {
                    required
                },
                salaryTemplateList: {
                    required
                },
            },
          }

        },
        watch: {
            search: function () {
                this.GetSalaryTemplateData();
            }
        },
        methods: {
            Close: function () {
                if (this.isValid('CanViewRunPayroll')) {
                    this.$router.push('/RunPayroll');
                }
                else {
                    this.$router.go();
                }
               
            },

            UpdateRecord: function (record, salaryTemplate) {
                
                this.show = false;
                var result = this.runPayroll.salaryTemplateList.find((x) => {
                    return x.id == this.salaryTemplateId;
                });

                if (result != undefined && !salaryTemplate.zeroSalary) {
                    result.salaryDetailList = salaryTemplate.salaryDetailList;

                    result.autoIncomeTax = salaryTemplate.autoIncomeTax;
                    result.incomeTax = salaryTemplate.incomeTax;

                    result.grossSalary = record.grossSalary;
                    result.allowanceAmount = record.allowanceAmount;
                    result.deductionAmount = record.deductionAmount;
                    result.contributionAmount = record.contributionAmount;
                    result.incomeTaxAmount = record.taxPerPeriod;
                    result.netSalary = parseFloat(record.netSalary);
                    result.netSalary = parseFloat(record.netSalary) - result.loanAmount;
                    result.zeroSalary = false;
                    result.reason = '';
                }

                if (result != undefined && salaryTemplate.zeroSalary) {
                    result.employeeSalaryDetail = salaryTemplate;
                    result.runPayrollSalaryDetailList = [];

                    result.baseSalary = 0;
                    result.grossSalary = 0;
                    result.allowanceAmount = 0;
                    result.deductionAmount = 0;
                    result.contributionAmount = 0;
                    result.incomeTaxAmount = 0;
                    result.loanAmount = 0;
                    result.netSalary = 0;
                    result.zeroSalary = true;
                    result.reason = salaryTemplate.reason;
                }

            },

            updateLineTotal: function (e, item) {
                
                e = Math.round(e);
                item.hour = e;
                item.hourAmount = item.weekdayHourlySalary * e;
                item.netSalary = (item.grossSalary + item.hourAmount) - (item.deductionAmount + item.contributionAmount + item.incomeTaxAmount + item.loanAmount);
            },

            getPage: function () {
                this.GetSalaryTemplateData();
            },

            GetPayrollSchedule: function () {
                
                this.runPayroll.hourly = this.runPayroll.payrollScheduleId.isHourlyPay;
                this.runPayroll.payrollScheduleId = this.runPayroll.payrollScheduleId.id;
                this.GetSalaryTemplateData();
            },

            SaveSalaryTemplate: function (status) {
                this.runPayroll.status = status;

                this.loading = true;
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.post('/Payroll/SaveRunPayroll', root.runPayroll, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {

                        if (response.data.isSuccess) {
                            root.loading = false

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: "Saved Sucessfully",
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewRunPayroll')) {
                                        root.$router.push('/RunPayroll');
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                }
                            });
                        }

                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }

                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,

                            });

                        this.loading = false
                    })
                    .finally(() => this.loading = false)
            },


            GetSalaryTemplateData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                

                root.$https.get('Payroll/RunPayrollDetailList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage + '&designationId=' + this.designationId + '&departmentId=' + this.departmentId + '&payrollScheduleId=' + this.runPayroll.payrollScheduleId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        response.data.results.forEach(function (result) {
                            result.hour = 0;
                            result.hourAmount = 0;
                            result.zeroSalary = false;
                            result.reason = '';
                        });

                        root.runPayroll.salaryTemplateList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditSalaryTemplate: function (detail, id) {
                var root = this;

                root.data = detail;
                root.salaryTemplateId = id;
                root.show = !root.show;
            },

            
            removeItem: function (id) {
                this.runPayroll.salaryTemplateList = this.runPayroll.salaryTemplateList.filter((prod) => {
                    return prod.id != id;
                });

            },

        },

        created: function () {
            this.$emit('update:modelValue', this.$route.name);
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            
            if (this.$route.query.data != undefined) {

                var data = this.$route.query.data;
                this.runPayroll.id = data.id;
                this.runPayroll.status = data.status;
                this.runPayroll.hourly = data.hourly;
                this.runPayroll.payrollScheduleId = data.payrollScheduleId;
                this.runPayroll.salaryTemplateList = data.salaryTemplateList;
                
                if (this.runPayroll.hourly) {
                    this.runPayroll.salaryTemplateList.forEach(function (result) {
                        root.updateLineTotal(result.hour, result);
                    });
                }

            }
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
        }
    }
</script>