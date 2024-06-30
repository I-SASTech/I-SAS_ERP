<template>
    <div class="row " v-if=" isValid('CanAddSignUpUser') || isValid('CanEditSignUpUser')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddSignUp.LoginPermission') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddSignUp.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddSignUp.LoginPermission') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-6 form-group" v-if="dayStart!='false'">
                            <label>{{ $t('AddSignUp.User') }}</label>
                            <div>
                                <usersDropdown v-model="loginDetails.userId" :modelValue="loginDetails.userId" :alluser="'true'"></usersDropdown>
                            </div>
                        </div>
                        

                        <div class="col-sm-6  form-group" v-if="dayStart!='false'">
                            <label>{{ $t('AddSignUp.Invoice') }}</label>
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="isTouch" class="mb-2" :options="['Touch Invoice','Invoice Barcode']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddSignUp.SelectOption')">
                            </multiselect>
                            <multiselect v-else v-model="isTouch" class="mb-2" :options="['Touch Invoice', 'Invoice Barcode']" :show-labels="false" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :placeholder="$t('AddSignUp.SelectOption')">
                            </multiselect>
                        </div>
                        <div class="col-sm-6  form-group">
                            <label>{{ $t('AddSignUp.Roles') }}:<span class="text-danger"> *</span></label>
                            <div>
                                <roledropdown ref="roleComponent" @update:modelValue="SaleManRol(loginDetails.roleId)" v-model="loginDetails.roleId" :key="rander" :modelValue="loginDetails.roleId"></roledropdown>
                            </div>
                        </div>
                        <div class="col-sm-6  form-group">
                            <label>Terminal User Type :<span class="text-danger"> </span></label>
                                    
                            <multiselect :options="terminalUserTypeOptions" @update:modelValue="onCahngeTerminalUserType"
                                v-model="loginDetails.terminalUserType" :show-labels="false" placeholder="Terminal User Type">
                            </multiselect>
                        </div>
                        <div class="col-sm-6 form-group" v-if="loginDetails.terminalUserType == 'Offline' || loginDetails.terminalUserType == 'Both'">
                            <label >Offline</label>
                            <div>
                                <terminal-dropdown :terminalType="terminalType" v-model="loginDetails.terminalId" :terminalUserType="'Offline'" :isSelect="true"  v-bind:key="terminalRander" :modelValue="loginDetails.terminalId" />
                            </div>
                        </div>
                        <div class="col-sm-6  form-group" v-if="loginDetails.terminalUserType == 'Online' || loginDetails.terminalUserType == 'Both'" >
                            <label >Online</label>                            
                            <div>
                                <terminal-dropdown :terminalType="terminalType" v-model="loginDetails.onlineTerminalId" :terminalUserType="'Online'" :isSelect="true"  v-bind:key="terminalRander" :modelValue="loginDetails.onlineTerminalId" />
                            </div>
                        </div>
                       
                       
                    </div>
                    <div class="row " :key="randered">
                        <div class="col-lg-6 pt-2">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="loginDetails.isExpenseAccount">
                                <label for="inlineCheckbox1"> {{ $t('AddSignUp.AllowDailyExpense') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6 pt-2">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox2" v-model="loginDetails.changePriceDuringSale">
                                <label for="inlineCheckbox2">  {{ $t('AddSignUp.AllowedDuringSale') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox3" v-model="loginDetails.giveDicountDuringSale">
                                <label for="inlineCheckbox3">  {{ $t('AddSignUp.AllowedtoGivDiscount') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox44" v-model="loginDetails.viewCounterDetails">
                                <label for="inlineCheckbox44">  {{ $t('AddSignUp.AllowedCounterDetails') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox4" v-model="loginDetails.transferCounter">
                                <label for="inlineCheckbox4">  {{ $t('AddSignUp.AllowedTransferCounter') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox5" v-model="loginDetails.closeCounter">
                                <label for="inlineCheckbox5">  {{ $t('AddSignUp.CloseCounter') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="invoiceWoInventory=='true' && dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="InvoiceWoInventory" v-model="loginDetails.invoiceWoInventory">
                                <label for="InvoiceWoInventory">  {{ $t('AddSignUp.InvoiceWoInventory') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox6" v-model="loginDetails.allowAll">
                                <label for="inlineCheckbox6">  {{ $t('AddSignUp.AllowAll') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox7" v-model="loginDetails.permissionToStartExpenseDay">
                                <label for="inlineCheckbox7">  {{ $t('AddSignUp.PermissionToStartExpenseDay') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox8" v-model="loginDetails.holdCounter">
                                <label for="inlineCheckbox8">  {{ $t('AddSignUp.AllowedtoHoldCounter') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox9" v-model="loginDetails.closeDay">
                                <label for="inlineCheckbox9">  {{ $t('AddSignUp.AllowedtoCloseDay') }} </label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox10" v-model="loginDetails.isSupervisor">
                                <label for="inlineCheckbox10">  Is Supervisor </label>
                            </div>
                        </div>

                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox13" v-model="loginDetails.processSaleReturn">
                                <label for="inlineCheckbox13">  {{ $t('AddSignUp.AllowedSaleReturn') }}</label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox14" v-model="loginDetails.dailyExpenseList">
                                <label for="inlineCheckbox14">  {{ $t('AddSignUp.AllowedExpenseList') }}</label>
                            </div>
                        </div>
                        <div class="col-lg-6" v-if="dayStart!='false'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox15" v-model="loginDetails.startDay">
                                <label for="inlineCheckbox15">  {{ $t('AddSignUp.AllowedtoStartDay') }}</label>
                            </div>
                        </div>

                        <!-- <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox17" v-model="loginDetails.isActive">
                                <label for="inlineCheckbox17">  {{ $t('AddSignUp.IsActive') }}</label>
                            </div>
                        </div> -->
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="temporaryCashReceiver" v-model="loginDetails.temporaryCashReceiver">
                                <label for="temporaryCashReceiver">  {{ $t('AddSignUp.TemporaryCashReceiver') }}</label>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="temporaryCashIssuer" v-model="loginDetails.temporaryCashIssuer">
                                <label for="temporaryCashIssuer">  {{ $t('AddSignUp.TemporaryCashIssuer') }}</label>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="temporaryCashRequester" v-model="loginDetails.temporaryCashRequester">
                                <label for="temporaryCashRequester">  {{ $t('AddSignUp.TemporaryCashRequester') }}</label>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="allowViewAllData" v-model="loginDetails.allowViewAllData">
                                <label for="allowViewAllData">  {{ $t('AddSignUp.AllowViewAllData') }}</label>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="isOverAllAccess" v-model="loginDetails.isOverAllAccess">
                                <label for="isOverAllAccess">  OverAllAccess</label>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6" v-if="loginDetails.temporaryCashReceiver">
                            <label>{{ $t('AddSignUp.Days') }} :<span class="text-danger"> *</span></label>
                            <input class="form-control" type="number" v-model="loginDetails.days" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                        </div>
                        <div class="col-sm-6" v-if="loginDetails.temporaryCashReceiver">
                            <label>{{ $t('AddSignUp.Limit') }} :<span class="text-danger"> *</span></label>
                            <input class="form-control" type="number" v-model="loginDetails.limit" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                        </div>
                    </div>
                </div>

                <div v-if="!loading" class="card-footer">
                    <div class="row">
                        <div v-if="!loading" class=" col-md-12">
                            <div class="button-items">
                                <button class="btn btn-primary" v-bind:disabled=" !isOpenDay && loginDetails.terminalId==''" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddSignUpUser')" v-on:click="SaveLoginDetails"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddSignUp.btnSave') }}</button>
                                
                                <button class="btn btn-primary" v-bind:disabled="!isOpenDay && loginDetails.terminalId==''" v-if="loginDetails.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditSignUpUser')" v-on:click="UpdateLoginDetails"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddSignUp.btnUpdate') }}</button>
                                <button class="btn btn-danger" v-on:click="Cancel">{{ $t('AddSignUp.btnClear') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default ({
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        
        data: function () {
            return {
                terminalUserTypeOptions:[],
                terminalUserTypeDisabled: false,
                loading: false,
                invoiveItem: false,
                invoiveBarCode: false,
                invoiveBarCodeItem: false,
                terminalType: '',
                dayStart: '',
                arabic: '',
                english: '',
                isTouch: '',
                invoiceWoInventory: '',
                registrationDate: '',
                arabicName: '',
                emailExist: false,
                gender: '',
                idNumber: '',
                userId: '',
                rander: 0,
                randered: 0,
                language: 'Nothing',
                password: "password",
                password1: "password",
                eyeValue: false,
                eyeValue1: false,
                isOpenDay: '',
                terminalRander: 0,
                options: [],
                loginDetails: {
                    id: '00000000-0000-0000-0000-000000000000',
                    email: '',
                    userId: '',
                    employeeId: '',
                    userName: '',
                    password: '',
                    confirmPassword: '',
                    isExpenseAccount: false,
                    changePriceDuringSale: false,
                    giveDicountDuringSale: false,
                    viewCounterDetails: false,
                    transferCounter: false,
                    closeCounter: false,
                    holdCounter: false,
                    closeDay: false,
                    processSaleReturn: false,
                    dailyExpenseList: false,
                    shiftStartTime: false,
                    shiftEndTime: false,
                    invoiceWoInventory: false,
                    terminalId: '',
                    onlineTerminalId: '',
                    isTouchInvoice: false,
                    isActive: true,
                    allowAll: false,
                    permissionToStartExpenseDay: false,
                    isSupervisor: false,
                    temporaryCashReceiver: false,
                    temporaryCashIssuer: false,
                    temporaryCashRequester: false,
                    allowViewAllData: false,
                    roleId: '',
                    days: 0,
                    limit: 0,
                    touchScreen: '',
                    terminalUserType:'',
                    isOverAllAccess:false
                }
            }
        },
        methods: {
            onCahngeTerminalUserType: function () {   
                
                this.terminalRander++
            },

            UpdateLoginDetails: function (userId) {
                
                this.userId = userId;
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.isTouch == 'Touch Invoice') {
                    this.loginDetails.isTouchInvoice = true
                }
                else {
                    this.loginDetails.isTouchInvoice = false
                }

                if (this.loginDetails.roleId.id != undefined || this.loginDetails.roleId.id != null) {
                    this.loginDetails.roleId = this.loginDetails.roleId.id;

                }

                if (this.loginDetails.terminalUserType == 'Offline') {
                    this.loginDetails.onlineTerminalId = '';
                }

                if (this.loginDetails.terminalUserType =='Online') {
                    this.loginDetails.terminalId = '';
                }

                this.loginDetails.isActive = !this.loginDetails.isActive;
                localStorage.setItem('AllowAll', this.loginDetails.allowAll);
                root.$https
                    .post('/account/SaveUser', this.loginDetails, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null) {
                            root.loading = false

                            root.$swal.fire({
                                icon: 'success',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                showConfirmButton: false,
                                timer: 1800,
                                timerProgressBar: true,

                            });
                            root.$router.push('/signUp');
                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Server Error' : 'خطأ في الخادم', 
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },


            Cancel: function () {
                this.$router.push({
                    path: '/signUp',

                })
            },
            SaleManRol: function (Id) {
                this.GetTerminalData();
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (response.data.name == 'Sales Man' || response.data.nameArabic == '') {
                                root.loginDetails.invoiceWoInventory = true;
                                root.loginDetails.isExpenseAccount = true;
                                root.loginDetails.changePriceDuringSale = true;
                                root.loginDetails.giveDicountDuringSale = true;
                                root.loginDetails.viewCounterDetails = true;
                                root.loginDetails.transferCounter = true;
                                root.loginDetails.closeCounter = true;
                                root.loginDetails.holdCounter = true;
                                root.loginDetails.closeDay = true;
                                root.loginDetails.startDay = true;
                                root.loginDetails.processSaleReturn = true;
                                root.loginDetails.dailyExpenseList = true;
                                root.loginDetails.terminals = true;
                                root.randered++;
                            }
                            else {
                                root.loginDetails.isExpenseAccount = false;
                                root.loginDetails.invoiceWoInventory = false;
                                root.loginDetails.changePriceDuringSale = false;
                                root.loginDetails.giveDicountDuringSale = false;
                                root.loginDetails.viewCounterDetails = false;
                                root.loginDetails.transferCounter = false;
                                root.loginDetails.closeCounter = false;
                                root.loginDetails.holdCounter = false;
                                root.loginDetails.closeDay = false;
                                root.loginDetails.startDay = false;
                                root.loginDetails.processSaleReturn = false;
                                root.loginDetails.dailyExpenseList = false;
                                root.loginDetails.terminals = false;
                                root.loginDetails.isActive = true;
                                root.randered++;
                            }
                        }
                    });


            },

            GetTerminalData: function () {
                    // Check if roleComponent is defined
                    if (this.$refs.roleComponent) {
                        var selectedRole = this.$refs.roleComponent.GetNameOfSelected();

                        if (selectedRole === 'Salesman') {
                            this.terminalType = 'CashCounter';
                            this.terminalRander++;
                        } else {
                            this.terminalType = 'All';
                            this.terminalRander++;
                        }
                    } else {
                        console.error("roleComponent is not defined.");
                        // Handle the case where roleComponent is not defined
                    }
                },

            SaveLoginDetails: function () {
                this.loading = true;
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }

                {

                    if (this.isTouch == 'Touch Invoice') {
                        this.loginDetails.isTouchInvoice = true
                    }
                    else {
                        this.loginDetails.isTouchInvoice = false
                    }
                    localStorage.setItem('AllowAll', this.loginDetails.allowAll);

                    root.$https
                        .post('/account/SaveLoginPermission', this.loginDetails, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(response => {
                            if (response.data != "Already Exists") {
                                this.loading = false
                                this.info = response.data.bpi


                                this.$swal.fire({
                                    icon: 'success',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    showConfirmButton: false,
                                    timer: 1800,
                                    timerProgressBar: true,

                                });
                                this.$router.push('/signUp');
                            }
                            else {
                                this.loading = false
                                this.info = response.data.bpi

                                this.$swal.fire({
                                    icon: 'error',
                                    title: 'UserId Already Exist',
                                    showConfirmButton: false,
                                    timer: 1800,
                                    timerProgressBar: true,

                                });
                            }
                        })
                        .catch(error => {
                            console.log(error)
                            this.$swal.fire(
                                {
                                    icon: 'error',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Server Error' : 'خطأ في الخادم', 
                                    text: error,
                                });

                            this.loading = false
                        })
                        .finally(() => this.loading = false)
                }
            },
            getUserWiseRecords: function () {

                if (this.$route.query.data != undefined) {
                    this.loginDetails.id = this.$route.query.data.id;
                    this.userId = this.$route.query.data.userId;
                    this.loginDetails.roleId = this.$route.query.data.roleId;
                    this.loginDetails.invoiceWoInventory = this.$route.query.data.invoiceWoInventory;
                    this.loginDetails.changePriceDuringSale = this.$route.query.data.changePriceDuringSale;
                    this.loginDetails.isExpenseAccount = this.$route.query.data.isExpenseAccount;
                    this.loginDetails.giveDicountDuringSale = this.$route.query.data.giveDicountDuringSale;
                    this.loginDetails.viewCounterDetails = this.$route.query.data.viewCounterDetails;
                    this.loginDetails.transferCounter = this.$route.query.data.transferCounter;
                    this.loginDetails.closeCounter = this.$route.query.data.closeCounter;
                    this.loginDetails.isSupervisor = this.$route.query.data.isSupervisor;
                    this.loginDetails.holdCounter = this.$route.query.data.holdCounter;
                    this.loginDetails.closeDay = this.$route.query.data.closeDay;
                    this.loginDetails.startDay = this.$route.query.data.startDay;
                    this.loginDetails.processSaleReturn = this.$route.query.data.processSaleReturn;
                    this.loginDetails.dailyExpenseList = this.$route.query.data.dailyExpenseList;
                    this.loginDetails.terminalId = this.$route.query.data.terminalId;
                    this.loginDetails.onlineTerminalId = this.$route.query.data.onlineTerminalId;
                    this.loginDetails.firstName = this.$route.query.data.firstName;
                    this.loginDetails.lastName = this.$route.query.data.lastName;
                    this.loginDetails.email = this.$route.query.data.email;
                    this.loginDetails.userName = this.$route.query.data.userName;
                    this.loginDetails.userId = this.$route.query.data.userId;
                    this.loginDetails.isActive = !this.$route.query.data.isActive;
                    this.loginDetails.allowAll = this.$route.query.data.allowAll;
                    this.loginDetails.permissionToStartExpenseDay = this.$route.query.data.permissionToStartExpenseDay;
                    this.loginDetails.touchScreen = this.$route.query.data.touchScreen;
                    this.loginDetails.temporaryCashReceiver = this.$route.query.data.temporaryCashReceiver;
                    this.loginDetails.temporaryCashIssuer = this.$route.query.data.temporaryCashIssuer;
                    this.loginDetails.temporaryCashRequester = this.$route.query.data.temporaryCashRequester;
                    this.loginDetails.days = this.$route.query.data.days;
                    this.loginDetails.limit = this.$route.query.data.limit;
                    this.loginDetails.allowViewAllData = this.$route.query.data.allowViewAllData;
                    if(this.$route.query.data.terminalUserType != 0)
                    this.loginDetails.terminalUserType = this.$route.query.data.terminalUserType;
                    this.loginDetails.isOverAllAccess = this.$route.query.data.isOverAllAccess;

                    if (this.$route.query.data.isTouchInvoice) {
                        this.isTouch = 'Touch Invoice'
                    }
                    else {
                        this.isTouch = 'Invoice Barcode'
                    }
                    this.GetTerminalData();
                    this.rander++;
                    this.randered++;

                }
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            
            if(localStorage.getItem('TerminalUserType') == 'Online'){
                this.terminalUserTypeOptions.push('Online')
                this.loginDetails.terminalUserType = 'Online'
                this.terminalUserTypeDisabled = true
            }
            else if(localStorage.getItem('TerminalUserType') == 'Offline'){
                this.terminalUserTypeOptions.push('Offline')
                this.terminalUserTypeDisabled = true
                this.loginDetails.terminalUserType = 'Offline'
            }
            else{
                this.terminalUserTypeOptions.push('Online')
                this.terminalUserTypeOptions.push('Offline')
                this.terminalUserTypeOptions.push('Both')
                this.terminalUserTypeDisabled = false
            }
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.invoiveItem = localStorage.getItem('invoiveItem') == "true" ? true : false;
            this.invoiveBarCode = localStorage.getItem('invoiveBarCode') == "true" ? true : false;
            this.invoiveBarCodeItem = localStorage.getItem('invoiveBarCodeItem') == "true" ? true : false;


            if (this.invoiveItem && this.invoiveBarCode && this.invoiveBarCodeItem) {
                this.options = ['Touch Invoice', 'Invoice Barcode', 'Invoice Barcode Item'];
            }
            else if (this.invoiveItem && this.invoiveBarCode) {
                this.options = ['Touch Invoice', 'Invoice Barcode'];
            }
            else if (this.invoiveBarCode && this.invoiveBarCodeItem) {
                this.options = ['Invoice Barcode', 'Invoice Barcode Item'];
            }
            else if (this.invoiveItem && this.invoiveBarCodeItem) {
                this.options = ['Touch Invoice', 'Invoice Barcode Item'];
            }
            else if (this.invoiveItem) {
                this.options = ['Touch Invoice'];
            }
            else if (this.invoiveBarCode) {
                this.options = ['Invoice Barcode'];
            }
            else if (this.invoiveBarCodeItem) {
                this.options = ['Invoice Barcode Item'];
            }
            this.dayStart = localStorage.getItem('DayStart');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isOpenDay = localStorage.getItem('IsOpenDay') == 'true' ? true : false;
            this.language = this.$i18n.locale;
            this.getUserWiseRecords();
            this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory');

        },
    })
</script>
<style scoped>
</style>