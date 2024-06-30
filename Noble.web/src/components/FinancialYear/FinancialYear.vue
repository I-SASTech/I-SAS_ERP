<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('FinancialYear.SubmissionPeriod') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">{{ $t('FinancialYear.SubmissionPeriod') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">

                                <a v-on:click="GotoPage('/FinancialYearClosing')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary">
                                    Financial Year Closing
                                </a>

                                <a v-if="isValid('CanCreateFinancialYear')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1" data-bs-toggle="offcanvas"
                                   v-on:click="GetSetting()"
                                   data-bs-target="#offcanvasLeft" aria-controls="offcanvasLeft">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('FinancialYear.Setting') }}
                                </a>

                                <a v-if="isValid('CanCreateFinancialYear')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary " data-bs-toggle="offcanvas"
                                   data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('FinancialYear.AddNew') }}
                                </a>

                                <a v-on:click="CreatePreviousYear()" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-minus"></i> {{ $t('FinancialYear.CreatePreviousYear') }}
                                </a>

                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('FinancialYear.Close') }}
                                </a>

                                <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                                     aria-labelledby="offcanvasRightLabel">
                                    <div class="offcanvas-header">
                                        <h5 id="offcanvasRightLabel" class="m-0">
                                            {{ $t('FinancialYear.SubmissionPeriod') }}
                                        </h5>
                                        <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                                                aria-label="Close"></button>
                                    </div>
                                    <div class="offcanvas-body">
                                        <div class="form-group col-lg-sm"
                                             v-if="currentyear.financialYearList?.length == 0 && ($i18n.locale == 'en' || isLeftToRight())">
                                            <select class="form-control" v-model="selectedMonth" @change="GetCurrentYear">
                                                <option value="" selected disabled hidden>Choose here</option>
                                                <!--<option value="0">{{ $t('FinancialYear.SelectMonth') }}</option>-->
                                                <option v-for="(month, index) in monthOfYear" :key="index">
                                                    {{ month }}
                                                </option>
                                            </select>
                                        </div>
                                        <div class="col-lg-12 form-group"
                                             v-if="currentyear.financialYearList?.length == 0 && $i18n.locale == 'ar'">
                                            <select class="form-control" v-model="selectedMonth" @change="GetCurrentYear">
                                                <option value="" selected disabled hidden>Choose here</option>
                                                <!--<option value="0">{{ $t('FinancialYear.SelectMonth') }}</option>-->
                                                <option v-for="(month, index) in monthOfYearArabic" :key="index">
                                                    {{ month }}
                                                </option>
                                            </select>
                                        </div>
                                        <div class="col-lg-12 form-group" v-if="isYear">
                                            <select class="form-control" v-model="year">
                                                <option value="0">{{ $t('FinancialYear.SelectYear') }}</option>
                                                <option v-for="year in currentyear.yearToList" :key="year">
                                                    {{ year }}
                                                </option>
                                            </select>
                                        </div>
                                        <!--<div class="col-lg-12 form-group">
                                            <multiselect v-model="monthType" :options="['Month', 'Quarterly', '6 Months', 'Year']"
                                                         :show-labels="false">
                                            </multiselect>

                                        </div>-->
                                        <div>

                                            <a href="javascript:void(0)" class="btn btn-outline-primary"
                                               data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="CreateYear"
                                               v-if="$i18n.locale == 'ar' && isValid('CanCreateFinancialYear')">يخلق</a>
                                            <a href="javascript:void(0)" class="btn btn-outline-primary"
                                               data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="CreateYear"
                                               v-else-if="isValid('CanCreateFinancialYear')">Create</a>


                                        </div>
                                    </div>
                                </div>

                                <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasLeft"
                                     aria-labelledby="offcanvasRightLabel">
                                    <div class="offcanvas-header">
                                        <h5 id="offcanvasRightLabel" class="m-0">
                                            Financial Year Setting
                                        </h5>
                                        <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                                    </div>
                                    <div class="offcanvas-body">
                                        <div class="col-lg-12 form-group">
                                            <div class="checkbox form-check-inline mx-2">
                                                <input type="checkbox" id="inlineCheckbox1" v-model="setting.isAutoClosing">
                                                <label for="inlineCheckbox1"> Is Auto Closing </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <multiselect v-model="setting.closingType" :options="['Month', 'Quarterly', '6 Months', 'Year']"
                                                         :show-labels="false">
                                            </multiselect>

                                        </div>
                                        <div>
                                            <a href="javascript:void(0)" class="btn btn-outline-primary"
                                               data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="SaveSetting()"
                                               v-if="$i18n.locale == 'ar'">يخلق</a>

                                            <a href="javascript:void(0)" class="btn btn-outline-primary"
                                               data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="SaveSetting()"
                                               v-else>Save</a>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <!-- Start Accordian Setting -->

        <div class="card">
            <div class="card-body" :key="rander">
                <div class="accordion" id="accordionExample">
                    <div class="accordion-item" v-for="(year, index) in comapnyPeriod" :key="index">
                        <h5 class="accordion-header m-0" :id="'headingOne' + index">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse"
                                    v-bind:data-bs-target="'#collapseOne' + index" aria-expanded="false"
                                    :aria-controls="'collapseOne' + index">
                                Financial Year {{ year.year }}
                            </button>
                        </h5>
                        <div :id="'collapseOne' + index" class="accordion-collapse collapse"
                             :aria-labelledby="'headingOne' + index" data-bs-parent="#accordionExample">
                            <div class="card">

                                <div class="card-body">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        #
                                                    </th>
                                                    <th>
                                                        {{ $t('FinancialYear.PeriodName') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('FinancialYear.Year') }}
                                                    </th>
                                                    <!--<th>

                                                    </th>-->
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(period, yearInd) in year.companySubmissionPeriod"
                                                    :key="period.periodDescription">
                                                    <td>{{ yearInd + 1 }}</td>
                                                    <td>{{ period.periodDescription }}</td>
                                                    <td>{{ period.year }}</td>
                                                    <!--<td>
                                                        <div class="form-check form-switch pt-1">
                                                            <input class="form-check-input" v-on:change="ClosePersiod(period.id, period.isPeriodClosed)" v-model="period.isPeriodClosed" type="checkbox">
                                                        </div>

                                                    </td>-->
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>


                                </div>
                            </div>


                        </div>

                    </div>
                    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>

                </div>
            </div>
            <!--end card-body-->
        </div>

        <!-- End Accordian Setting -->
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        data: function () {

            return {
                loading: false,
                rander: 0,
                year: '0',
                comapnyPeriod: [],
                currentyear: [],
                isYear: true,
                selectedMonth: '',
                monthType: '',
                lang: '',
                monthOfYear: [
                    'January',
                    'February',
                    'March',
                    'April',
                    'May',
                    'June',
                    'July',
                    'August',
                    'September',
                    'October',
                    'November',
                    'December'
                ],
                monthOfYearArabic: [
                    'كانون الثاني',
                    'شهر فبراير',
                    'مارس',
                    'أبريل',
                    'قد',
                    'يونيو',
                    'تموز',
                    'أغسطس',
                    'شهر تسعة',
                    'اكتوبر',
                    'شهر نوفمبر',
                    'ديسمبر'
                ],
                stepsVm: {
                    companyId: '',
                    step1: false,
                    step2: false,
                    step3: false,
                    step4: false,
                },
                setting: {
                    id: '00000000-0000-0000-0000-000000000000',
                    closingType: '',
                    isAutoClosing: false,
                }

            }
        },
        methods: {
            
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            ClosePersiod: function (id, status) {

                var root = this;
                var token = localStorage.getItem('token');

                root.$https.get('/Company/CloseMonthPeriod?id=' + id + '&status=' + status, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess) {
                            root.$swal.fire({
                                icon: 'success',
                                title: 'Employee Status Change',
                                showConfirmButton: false,
                                timer: 1800,
                                timerProgressBar: true,

                            });
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            },

            SaveSetting: function () {
                var root = this;
                this.loading = true;
                var token = localStorage.getItem('token');

                this.$https.post('/FinancialYear/SaveFinancialYearSetting', this.setting, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.id != "00000000-0000-0000-0000-000000000000") {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
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
            },

            GetSetting: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/FinancialYear/GetFinancialYearSetting', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.setting.id = response.data.id;
                            root.setting.closingType = response.data.closingType;
                            root.setting.isAutoClosing = response.data.isAutoClosing;                
                        }
                    }).catch(error => {
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
            },


            CreatePreviousYear: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/AddUpdatePreviousFinancialYear', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != "00000000-0000-0000-0000-000000000000") {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.GetCurrentYear();
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
            },


            CreateYear: function () {

                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var index = ''
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    index = root.monthOfYear.indexOf(root.selectedMonth)
                }
                else {

                    var monthEngName = root.monthOfYear[root.monthOfYearArabic.indexOf(root.selectedMonth)]
                    index = root.monthOfYear.indexOf(monthEngName)
                }


                if (this.year != "0") {
                    this.$https.get('/Company/AddFinancialYear?year=' + this.year + '&month=' + index + '&monthType=' + this.monthType, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.comapnyPeriod = response.data.companySubmissionPeriod;
                            root.currentyear = response.data;
                            root.selectedMonth = response.data.monthName;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Created!' : 'مخلوق!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Financial year created Successfully!' : 'تم إنشاء السنة المالية بنجاح!',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            if (root.$route.query.IsSetup === "true") {
                                //root.$router.push('/Setup')
                                root.stepsVm.companyId = localStorage.getItem('CompanyID'),
                                    root.stepsVm.step5 = true,
                                    root.$https.post('/account/SetupUpdateInCompany', root.stepsVm, { headers: { "Authorization": `Bearer ${token}` } })
                                        .then(function (response) {

                                            if (response.data) {
                                                localStorage.setItem('companyProfile', true);

                                                root.$router.push({
                                                    path: '/Setup',
                                                    query: {
                                                        step5: true
                                                    }
                                                });

                                            } else {
                                                console.log("error: something wrong from db.");
                                            }
                                        })

                            }
                            else {
                                root.GetCurrentYear()
                            }
                            //window.location.href = ('/Company/CreateSubmissionYear');
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هناك خطأ ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                    });
                }

            },

            GetCurrentYear: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var month = ''
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    month = root.selectedMonth == undefined ? '' : root.selectedMonth;
                }
                else {

                    var monthEngName = root.monthOfYear[root.monthOfYearArabic.indexOf(root.selectedMonth)]
                    month = root.selectedMonth == undefined ? '' : monthEngName;
                }

                this.$https.get('/Company/GetCurrentYear?month=' + month, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {


                        if (response.data.financialYearList != undefined) {
                            root.comapnyPeriod = response.data.financialYearList;

                        }

                        root.currentyear = response.data;

                        if (response.data.monthName != "") {
                            if ((root.$i18n.locale == 'en' || root.isLeftToRight())) {
                                root.selectedMonth = response.data.monthName;
                            }
                            else {

                                root.selectedMonth = root.monthOfYearArabic[root.monthOfYear.indexOf(response.data.monthName)]

                            }
                        }
                        root.rander++;
                    }
                }).catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هناك خطأ ما!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                });
            }
        },
        created: function () {

            this.lang = localStorage.getItem('')
            this.GetCurrentYear()


        },

    }
</script>