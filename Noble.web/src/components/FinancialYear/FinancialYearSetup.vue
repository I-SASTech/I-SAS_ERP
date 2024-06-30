<template>
    <div class="row" >
        <div class="col-lg-6 col-sm-6">
            <form class="form-horizontal" id="PeriodForm">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title DayHeading">{{$t('FinancialYearSetup.SubmissionPeriod')}}</h4>
                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-sm-6" v-if="currentyear.companySubmissionPeriod.length==0 && ($i18n.locale == 'en' ||isLeftToRight())">
                                <select class="form-control" v-model="selectedMonth" @change="GetCurrentYear">
                                    <option value="0">{{$t('FinancialYearSetup.SelectMonth')}}</option>
                                    <option v-for="(month,index) in monthOfYear" :key="index"> {{month}}</option>
                                </select>
                            </div>
                            <div class="col-sm-6" v-if="currentyear.companySubmissionPeriod.length==0 && $i18n.locale == 'ar'">
                                <select class="form-control" v-model="selectedMonth" @change="GetCurrentYear">
                                    <option value="0">{{$t('FinancialYearSetup.SelectMonth')}}</option>
                                    <option v-for="(month,index) in monthOfYearArabic" :key="index"> {{month}}</option>
                                </select>
                            </div>
                            <div class="col-sm-6" v-if="isYear">
                                <select class="form-control" v-model="year">
                                    <option value="0">{{$t('FinancialYearSetup.SelectYear')}}</option>
                                    <option v-for="year in currentyear.yearToList" :key="year"> {{year}}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <a href="javascript:void(0)" class="btn btn-outline-primary" v-on:click="CreateYear" v-if="($i18n.locale == 'ar'  && isValid('CanCreateFinancialYear')) || ($i18n.locale == 'ar' && isSetup)">يخلق</a>
                        <a href="javascript:void(0)" class="btn btn-outline-primary" v-on:click="CreateYear" v-else-if="(isValid('CanCreateFinancialYear')) || isSetup">Create</a>
                        
                        <router-link :to="'/StartScreen'" v-if="!isSetup"><a href="javascript:void(0)" class="btn btn-outline-danger ml-1"> {{ $t('FinancialYear.Close') }}</a></router-link>
                    </div>
                </div>
            </form>
        </div>
        <div class="col-lg-6 col-sm-6">
            <div class="card">
                <div class="card-header">
                    <h4 class="card-title DayHeading">{{$t('FinancialYearSetup.Periods')}}</h4>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table_list_bg">
                            <thead class="">
                                <tr>
                                    <th>
                                        {{$t('FinancialYearSetup.PeriodName')}}
                                    </th>
                                    <th>
                                        {{$t('FinancialYearSetup.Year')}}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="period in comapnyPeriod" :key="period.periodDescription">
                                    <td>{{period.periodDescription}}</td>
                                    <td>{{period.year}}</td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {

            return {
                year: '0',
                comapnyPeriod: [],
                currentyear: [],
                isYear: true,
                selectedMonth: '',
                lang: '',
                isSetup:false,
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
                }

            }
        },
        methods: {
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
                    this.$https.get('/Company/AddFinancialYear?year=' + this.year + '&month=' + index, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            if (response.data.companySubmissionPeriod != undefined) {
                                root.comapnyPeriod = response.data.companySubmissionPeriod;

                            }
                            root.currentyear = response.data;
                            root.selectedMonth = response.data.monthName;
                            root.$swal({
                                title: "Created!",
                                text: "Financial year created Successfully!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            
                            if (root.isSetup) {
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
            
            this.isSetup = this.$route.query.IsSetup === "true"?true:false
            this.lang = localStorage.getItem('')
            this.GetCurrentYear()


        },

    }
</script>