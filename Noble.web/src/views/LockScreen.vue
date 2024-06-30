<template>
    <div class="container">
        <div class="row vh-100 d-flex justify-content-center">
            <div class="col-12 align-self-center">
                <div class="row">
                    <div class="col-lg-5 mx-auto">
                        <div class="card">
                            <div class="card-body p-0 auth-header-box">
                                <div class="text-center p-3">
                                    <a class="logo logo-admin">
                                        <img src="msFakhry.png" height="50" alt="logo" class="auth-logo">
                                    </a>
                                    <h4 class="mt-3 mb-1 fw-semibold font-18" style="color: #0C213A;">NMS Fakhry ERP</h4>
                                    <p class="text-muted  mb-0">Hello Please, enter your password to unlock the screen !</p>
                                </div>
                            </div>
                            <div class="card-body">

                                <div class="form-group mb-2">
                                    <label class="form-label" for="username">Password</label>
                                    <div class="input-group ">
                                        <input v-model="v$.login.password.$model" :type="password" class="form-control"
                                            @keyup.enter="userlogin" :placeholder="$t('login.EnteryourPassword')">
                                        <div style=" position: absolute; top: 24%; " @click="showPassword"
                                            v-bind:style="($i18n.locale == 'en' || $i18n.locale == 'pt' || isLeftToRight()) ? 'left: 94%' : 'left: 11px'">
                                            <i class="fas fa-eye" v-if="eyeValue == false"></i>
                                            <i class="fas fa-eye-slash" v-if="eyeValue == true"></i>
                                        </div>
                                    </div>
                                </div><!--end form-group-->

                                <div class="form-group mb-0 row">
                                    <small class="text-danger">{{ customError }}</small>
                                                    <button v-on:click="userlogin"
                                                            class="btn btn-primary w-100 waves-effect waves-light"
                                                            type="button">
                                                        <i v-if="loading"
                                                           class="la la-refresh text-secondary la-spin progress-icon-spin"
                                                           style="font-size:.8125rem;"></i>
                                                        <span v-else>
                                                            UnLock<i class="fas fa-sign-in-alt ms-1"></i>
                                                        </span>
                                                    </button>
                                </div> <!--end form-group-->
                            </div>

                        </div><!--end card-->
                    </div><!--end col-->
                </div><!--end row-->
            </div><!--end col-->
        </div><!--end row-->
    </div><!--end container-->
</template>
<script>
import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators';

export default {
    setup() {
        return { v$: useVuelidate() }
    },
    name: 'lockOut-component',
    data() {
        return {
            isPortuguese: false,
            langs: ['en', 'ar'],
            login: {
                email: '',
                password: '',
                rememberMe: true
            },
            password: "password",
            eyeValue: false,
            roles: [],
            open: false,
            website: 'https://www.techqode.com/',
            error: '',
            show: false,
            customError: '',
            loading: false,
            isEmployee: true,
            isAccount: false,
            IsProceed: false,
            heading: '',
            description: '',
            companyName: '',
            applicationName: '',
            email: '',
            favName: '',
            terms: false,
            os: '',
            loginHistory: {
                userId: '',
                isLogin: true,
                operatingSystem: '',
                companyId: ''
            },
            isPayment: false,
            expirationMsg: '',

        }
    },
    validations() {
        return {
            login:
            {
                email:
                {
                    required
                },
                password:
                {
                    required
                }
            }
        }
    },

    mounted() {

    },
    methods: {

        GetData: function () {

            if (this.IsProceed == false) {
                this.isAccount = true;
                localStorage.setItem("isAccount", this.isAccount);
                if (this.terms == true) {
                    this.$router.push('/setup');

                }
                else {
                    this.$router.push('/TermsAndConditions');

                }
            }

            else {

                this.$router.push('/StartScreen');
            }
        },
        loginHistorySave: function () {

            this.$https.post('/account/LoginHistory', this.loginHistory).then(function (response) {
                if (response.data == 1)
                    console.log('Logout History save done');
                else
                    console.log('Logout History not save due to some error ' + response.data);
            });
        },

        showPassword() {
            if (this.password === "password") {
                this.password = "text";
                this.eyeValue = true;
            }
            else {
                this.password = "password"
                this.eyeValue = false;
            }

        },
        hidepassword() {
            this.password = "password"
            this.eyeValue = false;
        },
        setLocale(locale) {

            this.$i18n.locale = locale
            localStorage.setItem('locales', locale);
        },






        userlogin: function () {
            var root = this;
            root.customError = '';
            root.loading = true;
            var url = '/account/login';

            this.$https.post(url, this.login).then(function (response) {

                if (response.data == true) {
                    root.$router.push({
                        path: '/Authenticator',
                        query: { data: root.login }
                    })

                }
                else {
                    document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));
                    //root.clearCookie()
                    //if (root.login.rememberMe) {
                    //    root.setCookie();
                    //} else {
                    //    document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));
                    //}
                    if (response.data.companyId == "00000000-0000-0000-0000-000000000000") {

                        root.loading = false;
                        root.customError = 'Invalid Login Attempt.';

                    }
                    else if (response.data.isUseMachine) {
                        root.$store.login(this.login.email);
                        localStorage.setItem('CompanyID', response.data.companyId);
                        localStorage.setItem('token', response.data.token);
                        root.$router.push({
                            path: '/NotPermission',
                            query: { data: response.data.expiration, machine: true }
                        });
                    }
                    else if (response.data.isNotPayment) {
                        root.$store.login(this.login.email);
                        localStorage.setItem('CompanyID', response.data.companyId)
                        localStorage.setItem('token', response.data.token);
                        root.$router.push({
                            path: '/NotPermission',
                            query: { data: response.data.expiration }
                        });
                    }
                    else if (response.data.expiration != "" && response.data.expiration != null && response.data.isNotPayment == false && response.data.isPayment == false) {
                        root.$store.login(this.login.email);
                        localStorage.setItem('CompanyID', response.data.companyId)
                        localStorage.setItem('token', response.data.token);
                        root.$router.push({
                            path: '/NotPermission',
                            query: { data: response.data.expiration }
                        });
                    }

                    else {

                        root.loginHistory.userId = response.data.userId;
                        root.loginHistory.companyId = response.data.companyId;
                        root.loginHistorySave();

                        root.isPayment = response.data.isPayment
                        root.expirationMsg = response.data.expiration
                        var getLocale = localStorage.getItem('locales');
                        root.$store.login(true);
                        localStorage.clear();

                        localStorage.setItem('locales', getLocale);
                        localStorage.setItem('Index', 0);
                        localStorage.setItem('token', response.data.token);
                        localStorage.setItem('UserId', response.data.userId);
                        localStorage.setItem('UserName', response.data.userName);
                        localStorage.setItem('overWrite', response.data.overWrite);
                        localStorage.setItem('LoginUserName', response.data.loginUserName);
                        localStorage.setItem('RoleName', response.data.roleName);
                        localStorage.setItem('ImagePath', response.data.imagePath);
                        localStorage.setItem('CompanyID', response.data.companyId);
                        localStorage.setItem('UserID', response.data.userId);
                        localStorage.setItem('FullName', response.data.fullName);
                        localStorage.setItem('DayStartTime', response.data.dayStartTime);
                        localStorage.setItem('PrinterName', response.data.printerName);
                        localStorage.setItem('IsHeaderFooter', response.data.isHeaderFooter);
                        response.data.terminalId == null ? localStorage.setItem('TerminalId', '') : localStorage.setItem('TerminalId', response.data.terminalId);
                        localStorage.setItem('CashAccountId', response.data.cashAccountId);
                        localStorage.setItem('BankAccountId', response.data.bankAccountId);

                        //localStorage.setItem('SimpleInvoice', response.data.simpleInvoice);

                        localStorage.setItem('InvoicePrint', response.data.invoicePrint);
                        //localStorage.setItem('PurchaseOrder', response.data.purchaseOrder);

                        //localStorage.setItem('VersionAllow', response.data.versionAllow);



                        //Default Settings
                        if (response.data.roleName == 'User') {
                            localStorage.setItem('IsCashCustomer', response.data.defaultSettingModel.isCashCustomer);
                            localStorage.setItem('IsSaleCredit', response.data.defaultSettingModel.isSaleCredit);
                            localStorage.setItem('IsPurchaseCredit', response.data.defaultSettingModel.isPurchaseCredit);
                            localStorage.setItem('IsCustomeCredit', response.data.defaultSettingModel.isCustomeCredit);
                            localStorage.setItem('IsCustomerPayCredit', response.data.defaultSettingModel.isCustomerPayCredit);
                            localStorage.setItem('IsSupplierPayCredit', response.data.defaultSettingModel.isSupplierPayCredit);
                            localStorage.setItem('IsSupplierCredit', response.data.defaultSettingModel.isSupplierCredit);

                        }







                        //localStorage.setItem('IsProduction', response.data.isProduction);

                        /*PrintHeaderDetail*/
                        localStorage.setItem('PrintTemplate', response.data.printTemplate);
                        localStorage.setItem('ReturnDays', response.data.returnDays);
                        localStorage.setItem('PrintSizeA4', response.data.printSizeA4);
                        localStorage.setItem('TermsInAr', response.data.termsInAr);
                        localStorage.setItem('TermsInEng', response.data.termsInEng);
                        localStorage.setItem('AllowAll', response.data.allowAll);

                        localStorage.setItem('IsDayStart', response.data.isDayStart);
                        localStorage.setItem('Step1', response.data.step1);
                        localStorage.setItem('Step2', response.data.step2);
                        localStorage.setItem('Step3', response.data.step3);
                        localStorage.setItem('Step4', response.data.step4);
                        localStorage.setItem('Step5', response.data.step5);
                        localStorage.setItem('CounterId', response.data.counterId);
                        localStorage.setItem('IsSupervisor', response.data.isSupervisor);
                        localStorage.setItem('TermsCondition', response.data.termsCondition);
                        localStorage.setItem('WareHouseId', response.data.warehouseId);

                        localStorage.setItem('IsPermissionToStartDay', response.data.isPermissionToStartDay);
                        localStorage.setItem('IsDailyExpense', response.data.isExpenseAccount);
                        localStorage.setItem('IsPermissionToCloseDay', response.data.isPermissionToCloseDay);
                        //localStorage.setItem('IsMasterProductPermission', response.data.isMasterProductPermission);
                        localStorage.setItem('NobleRole', response.data.nobleRole);

                        localStorage.setItem('BusinessLogo', response.data.businessLogo);
                        localStorage.setItem('BusinessNameArabic', response.data.businessNameArabic);
                        localStorage.setItem('BusinessNameEnglish', response.data.businessNameEnglish);
                        localStorage.setItem('BusinessTypeArabic', response.data.businessTypeArabic);
                        localStorage.setItem('BusinessTypeEnglish', response.data.businessTypeEnglish);
                        localStorage.setItem('CompanyNameArabic', response.data.companyNameArabic);
                        localStorage.setItem('CompanyNameEnglish', response.data.companyNameEnglish);

                        root.terms = response.data.termsCondition;
                        localStorage.setItem('IsExpenseDay', response.data.isExpenseDay);
                        localStorage.setItem('TransferCounter', response.data.transferCounter);
                        /*PrintHeaderDetail End*/

                        localStorage.setItem('IsBlindPrint', response.data.isBlindPrint);
                        localStorage.setItem('AutoPaymentVoucher', response.data.autoPaymentVoucher);
                        localStorage.setItem('IsDeliveryNote', response.data.isDeliveryNote);
                        localStorage.setItem('TermAndConditionLength', response.data.termAndConditionLength);
                        localStorage.setItem('SalesMan', response.data.userRoleName);
                        localStorage.setItem('OnPageLoadItem', response.data.onPageLoadItem);
                        localStorage.setItem('isTouchInvoice', response.data.isTouchInvoice);
                        localStorage.setItem('touchScreen', response.data.touchScreen);

                        localStorage.setItem('BranchId', response.data.branchId == null ? '' : response.data.branchId);
                        localStorage.setItem('BranchName', response.data.branchName);
                        localStorage.setItem('MainBranch', response.data.mainBranch);


                        response.data.companyOptions.forEach(function (item) {
                            if (item.optionValue == 'bool') {
                                localStorage.setItem(item.label, item.value);
                            }
                            else {
                                localStorage.setItem(item.label, item.optionValue);
                            }
                        });
                        //localStorage.setItem("CompanyOption", JSON.stringify(response.data.companyOptions));




                        if (response.data.employeeId != null) {
                            /*   root.$router.push('/daystart')*/
                            localStorage.setItem('EmployeeId', response.data.employeeId);
                        }

                        /*iSProceed*/
                        localStorage.setItem('isProceed', response.data.isProceed);
                        if (response.data.isProceed) {
                            root.IsProceed = true;
                        }
                        if (response.data.phoneNo != null) {
                            localStorage.setItem('PhoneNo', response.data.phoneNo);
                            root.isEmployee = false;
                            // root.GetData();
                        }
                        if (response.data.roleName == 'User') {
                            root.$router.push('/Welcome');
                        }

                        else {

                            if (response.data.userRoleName == 'Sales Man') {

                                if (response.data.isDayStart == false) {
                                    if (localStorage.getItem('DayStart') == 'false') {
                                        root.$router.push('/Welcome');
                                    }
                                    else {
                                        if (localStorage.getItem("BankDetail") == "true")
                                            root.$router.push('/Welcome');
                                        //    root.$router.push('/WholeSaleDay');
                                        else

                                            root.$router.push('/Welcome');
                                        //root.$router.push({ path: '/dayStart', query: { token_name: 'DayStart_token', fromDashboard: 'true' } });

                                    }


                                }
                                else {
                                    if (response.data.touchInvoice == 'Touch Invoice') {
                                        root.$router.push('/TouchScreen');
                                    }
                                    else {
                                        root.$router.push('/TouchScreen');
                                    }
                                }
                            }
                            else {

                                root.$router.push('/Welcome');
                            }
                        }

                    }
                }

            }).catch(error => {
                root.customError = JSON.stringify(error.response.data.error);
                root.loading = false;
            })
        },



    },
    created: function () {
        this.login.email = localStorage.getItem('UserName');


        this.$store.lockOut();
        localStorage.clear();
        localStorage.setItem('UserName', this.login.email);


    }
}
</script>

