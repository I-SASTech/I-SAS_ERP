<template>
    <div class="row" v-bind:key="rander">
        <div class="col-lg-8 col-sm-8 offset-sm-2">
            <div class="card">
                <div class="card-header text-center">
                    <h4>
                        {{ $t('Setup.Letusproceed') }}

                    </h4>
                </div>
                <div class="card-body " >

                    <div class="col-12 text-center" v-if="coa==null ">
                        <div class="card-stats">
                            <div class="icon-big text-center text-danger">
                                <i class="fas fa-chart-bar text-danger"></i>
                            </div>

                        </div>
                        <h6 class="text-center"> {{ $t('Setup.Step1') }}</h6>
                        <a href="javascript:void(0)" v-on:click="ChartOfAccount"> {{ $t('Setup.SetupChartofAccounts') }}</a>
                    </div>
                    <div class="col-12 text-center" v-else>
                        <div class="card-stats">
                            <div class="icon-big text-center text-success">
                                <i class="fas fa-chart-bar text-success"></i>
                            </div>
                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step1') }}</h6>
                        <h6> {{ $t('Setup.SetupChartofAccounts') }}</h6>
                    </div>


                    <div class="col-12 text-center" v-if="companyProfile==null  ">
                        <div class="card-stats">
                            <div class="icon-big text-center text-danger">
                                <i class="far fa-id-card text-danger"></i>
                            </div>

                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step2') }}</h6>
                        <a href="javascript:void(0)" v-on:click="CompanyProfile">{{ $t('Setup.SetupCompanyProfile') }}</a>
                    </div>
                    <div class="col-12 text-center" v-else>
                        <div class="card-stats">
                            <div class="icon-big text-center text-success">
                                <i class="far fa-id-card text-success"></i>
                            </div>

                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step2') }}</h6>
                        <h6 class="text-center">{{ $t('Setup.SetupCompanyProfile') }}</h6>
                    </div>




                    <div class="col-12 text-center" v-if="currency1==null ">
                        <div class="card-stats">
                            <div class="icon-big text-center text-danger">
                                <i class=" far fa-money-bill-alt text-danger"></i>
                            </div>
                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step3') }}</h6>
                        <a href="javascript:void(0)" v-on:click="AddCurrency">{{ $t('Setup.SetupDefaultCurrency') }}</a>
                    </div>
                    <div class="col-12 text-center" v-else>
                        <div class="card-stats">
                            <div class="icon-big text-center text-success">
                                <i class=" far fa-money-bill-alt text-success"></i>
                            </div>

                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step3') }}</h6>
                        <h6 class="text-center">{{ $t('Setup.SetupDefaultCurrency') }}</h6>
                    </div>



                    <div class="col-12 text-center" v-if="taxRate==null ">
                        <div class="card-stats">
                            <div class="icon-big text-center text-danger">
                                <!--<i class="fas  fa-percent text-danger"></i>-->
                                <img src="orange.png" style="width:35px !important" />
                            </div>

                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step4') }}</h6>
                        <a href="javascript:void(0)" v-on:click="AddTax">{{ $t('Setup.SetupDefaultVAT') }}</a>
                    </div>
                    <div class="col-12 text-center" v-else>
                        <div class="card-stats">
                            <div class="icon-big text-center text-success">
                                                                <!--<i class="fas  fa-percent text-danger"></i>-->

                                <img src="success.png" style="width:35px !important"/>
                            </div>

                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step4') }}</h6>
                        <h6 class="text-center">{{ $t('Setup.SetupDefaultVAT') }}</h6>
                    </div>


                    <div class="col-12 text-center" v-if="financialYear==null ">
                        <div class="card-stats">
                            <div class="icon-big text-center text-danger">
                                <i class="fas fa-chart-line text-danger"></i>
                            </div>
                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step5') }}</h6>
                        <a href="javascript:void(0)" v-on:click="AddFinancialYear">{{ $t('Setup.SetupFinancialYear') }}</a>
                    </div>
                    <div class="col-12 text-center" v-else>
                        <div class="card-stats">
                            <div class="icon-big text-center text-success">
                                <i class="fas fa-chart-line text-success"></i>
                            </div>

                        </div>
                        <h6 class="text-center">{{ $t('Setup.Step5') }}</h6>
                        <h6 class="text-center">{{ $t('Setup.SetupFinancialYear') }}</h6>
                    </div>
                    <div class="col-12 mt-2 text-center">
                        <button type="button" class="btn btn-primary " v-if="coa && currency1  && taxRate && companyProfile && financialYear" v-on:click="Proceed"><i class="far fa-check-circle"></i> {{ $t('Setup.Proceed') }}</button>
                        <button type="button" class="btn btn-primary " disabled v-else v-on:click="Proceed"><i class="far fa-check-circle"></i> {{ $t('Setup.Proceed') }}</button>
                    </div>
                    <div class="card-footer col-md-3" v-if="loading">
                        <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                    </div>
                </div>

            </div>
        </div>
        <currencymodel :newcurrency="newCurrency"
                       :show="show"
                       :setup="true"
                       v-if="show"
                       @close="show = false"
                       @CurrencySave="CurrencySave"
                       :type="type" />

        <taxratemodel :newtaxrate="newTaxRate"
                      :setup="true"
                      :show="show1"
                      v-if="show1"
                      @TaxSave="TaxSave"
                      @close="show1 = false"
                      :type="type1" />

    </div>



</template>
<script>

    
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        components: {
            Loading
        },

        data: function () {

            return {
                userID:'',
                show: false,
                show1: false,
                coa: false,
                currency1: false,
                companyId: '',
                taxRate: false,
                isAccount: false,
                companyProfile: false,
                financialYear: false,
                rander: 0,
                loading: false,
                step1: false,
                step2: false,
                step3: false,
                step4: false,
                step5: false,
                currencylist: [

                ],
                newCurrency: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    sign: '',
                    arabicSign: '',
                    image: '',
                    setup: true,
                    isActive: true
                },
                type: '',
                type1: '',
                newTaxRate: {
                    id: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    code: '',
                    rate: 0,
                    taxMethod: '',
                    setup: true,
                    isActive: true
                },
                stepsVm: {
                    companyId: '',
                    step1: false,
                    step2: false,
                    step3: false,
                    step4: false,
                    step5: false,
                },

            }
        },

        methods: {
            AddFinancialYear: function () {
                if (this.coa == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep1'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else if (this.companyProfile == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep2'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else if (this.currency1 == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep3'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else if (this.taxRate == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep4'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else {
                    this.$router.push({
                        path: '/FinancialYear',
                        query: {
                            IsSetup: true
                        }
                    });
                }
            },
            AddTax: function () {
                debugger; //eslint-disable-line
                if (this.coa == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep1'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else if (this.companyProfile == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep2'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else if (this.currency1 == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep3'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else {
                    this.newTaxRate = {
                        id: '00000000-0000-0000-0000-000000000000',
                        name: '',
                        nameArabic: '',
                        description: '',
                        code: '',
                        rate: 0,
                        isActive: true,
                        setup: true
                    }
                    this.show1 = !this.show1;
                    this.type1 = "Add";
                }
            },
            TaxSave: function (x) {
                //eslint-disable-line
                this.loading = true;
                if (x == true) {


                    this.taxRate = x;
                    localStorage.setItem('taxRate', true);
                    this.taxRate = localStorage.getItem('taxRate');
                }

                var token = '';
                var root = this;
                this.stepsVm.companyId = localStorage.getItem('CompanyID');
                this.stepsVm.step4 = true;
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post('/account/SetupUpdateInCompany', root.stepsVm, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data) {
                            console.log("Tax Rate Setup Update")
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    })

            },
            CurrencySave: function (x) {
                this.loading = true;
                var root = this;

                if (x == true) {


                    this.currency1 = x;
                    localStorage.setItem('currency1', true);
                    this.currency1 = localStorage.getItem('currency1');
                }

                this.stepsVm.companyId = localStorage.getItem('CompanyID');
                this.stepsVm.step3 = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post('/account/SetupUpdateInCompany', root.stepsVm, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data) {
                            console.log("Currency Setup Update")
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    })

            },
            AddCurrency: function () {
                if (this.coa == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep1'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else if (this.companyProfile == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep2'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else {
                    this.newCurrency = {
                        id: '00000000-0000-0000-0000-000000000000',
                        name: '',
                        nameArabic: '',
                        sign: '',
                        arabicSign: '',
                        image: '',
                        isActive: true,
                        setup: true
                    };
                    this.show = !this.show;
                    this.type = "Add";
                    /*this.currency1 = true;*/
                }
            },
            GetUserPermission: function (x) {

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Account/GetUserPermission?id=' + x, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        localStorage.setItem('changePriceDuringSale', response.data.changePriceDuringSale);
                        localStorage.setItem('giveDicountDuringSale', response.data.giveDicountDuringSale);
                        //    localStorage.setItem('InvoiceWoInventoryUser', response.data.invoiceWoInventory);
                    }
                });
            },
            Proceed: function () {
                
                var token = '';
                var root = this;
                if (this.step1 == 'true' && this.step2 == 'true' && this.step3 == 'true' && this.step4 == 'true'&& this.step5 == 'true') {
                    root.$router.push('/StartScreen');
                }
                else {


                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    
                    root.loading = true;
                    root.$https.get('/Company/DefaultRolesOnLocation?companyId=' + this.companyId, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                root.GetUserPermission(root.userID);
                             /*   localStorage.setItem('EmployeeId', response.data.employeeId);*/
                                localStorage.setItem('IsPermissionToStartDay', true);
                                localStorage.setItem('IsPermissionToCloseDay', true);
                                localStorage.setItem('IsSupervisor', true);
                                root.$swal({
                                    icon: 'success',
                                    title: root.$t('Setup.Saved'),
                                    text: root.$t('Setup.ThankYou'),
                                    showConfirmButton: false,
                                    timer: 2000,
                                    timerProgressBar: true,
                                });
                                root.loading = false;
                                root.$router.push('/StartScreen');
                            }

                        });
                }



            },
            ChartOfAccount: function () {
                this.$router.push({
                    path: '/CoaTemplate',
                    query: {
                        IsSetup: true
                    }
                });
            },
            CompanyProfile: function () {
                
                if (this.coa == null) {
                    this.$swal({
                        title: this.$t('Setup.Error'),
                        icon: 'error',
                        text: this.$t('Setup.CompleteStep1'),
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else {
                    this.$router.push({
                        path: '/CompanyInfo',
                        query: {
                            isProceed: true
                        }
                    });
                }
            },
            AddCompany: function () {

                this.$router.push({
                    path: '/SetupAccount',
                    query: {
                        isProceed: true
                    }
                });

            },


        },

        mounted: function () {
            this.userID = localStorage.getItem('UserID');
            if (this.$route.query.coa != undefined) {
                //eslint-disable-line
                localStorage.setItem('coa', true);

                this.rander++;
            }

            if (this.$route.query.step5 == "true") {
                localStorage.setItem('FinancialYear', true);
                localStorage.setItem('Step5', true);
            }
            this.coa = localStorage.getItem('coa');

            this.currency1 = localStorage.getItem('currency1');
            this.taxRate = localStorage.getItem('taxRate');
            this.companyProfile = localStorage.getItem('companyProfile');
            this.companyId = localStorage.getItem('CompanyID');
            this.financialYear = localStorage.getItem('FinancialYear');


            this.step1 = localStorage.getItem('Step1');
            if (this.step1 == 'true') {
                this.coa = 'true';
            }
            this.step2 = localStorage.getItem('Step2');
            if (this.step2 == 'true') {
                this.companyProfile = 'true';
            }
            this.step3 = localStorage.getItem('Step3');
            if (this.step3 == 'true') {
                this.currency1 = 'true';
            }
            this.step4 = localStorage.getItem('Step4');
            
            if (this.step4 == 'true') {
                this.taxRate = 'true';
            }
            this.step5 = localStorage.getItem('Step5');
            
            if (this.step5 == 'true') {
                this.financialYear = 'true';
            }




        }
    }
</script>
<style scoped>
    .icon-big {
        font-size: 2em;
        min-height: 42px;
    }
</style>
