<template>
    <div class="row " v-if=" isValid('CanAddSignUpUser') || isValid('CanEditSignUpUser') " >
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000'">{{ $t('AddSignUp.SignUpDetails') }}</h4>
                                <h4 class="page-title" v-if="loginDetails.id!='00000000-0000-0000-0000-000000000000'">{{ $t('AddSignUp.SignUpDetails') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddSignUp.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddSignUp.SignUpDetails') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000'">
                    <div class="row">

                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.UserEmail') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.loginDetails.email.$error}">
                                <input class="form-control" v-model="v$.loginDetails.email.$model" @blur="EmailDuplicate(loginDetails.email)"  />
                                <span v-if="v$.loginDetails.email.$error" class="error text-danger">
                                    <span v-if="!v$.loginDetails.email.required"> {{ $t('AddSignUp.RegisterUser_Error_Required_EmailID') }} </span>
                                    <span v-if="!v$.loginDetails.email.email"> {{ $t('AddSignUp.RegisterUser_Error_Format_EmailID') }} </span>
                                </span>
                                <span class="text-right text-danger" v-if="emailExist">{{ $t('AddSignUp.EmailExist') }}</span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.UserId') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.loginDetails.userName.$error}">
                                <input class="form-control" v-model="v$.loginDetails.userName.$model"  />
                                <span v-if="v$.loginDetails.userName.$error" class="error text-danger">
                                    <span v-if="!v$.loginDetails.userName.required"> {{ $t('AddSignUp.RegisterUser_Error_Required_DisplayName') }}</span>
                                </span>
                            </div>
                        </div>
                        <!--<div class="col-sm-6 ">
                <label>{{ $t('SignUp.Terminal') }}</label>
                <div>
                    <terminal-dropdown v-model="loginDetails.terminalId" />
                </div>
            </div>-->
                        <div class="col-sm-6">
                            <label> {{ $t('AddSignUp.Password') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.loginDetails.password.$error}">

                                <div class="input-group mb-3">
                                    <input  id="password" v-model="v$.loginDetails.password.$model" name="password" :type="password1" class="form-control" v-bind:placeholder="$t('AddSignUp.Password')" aria-label="Recipient's username" aria-describedby="button-addon2">
                                    <button class="btn btn-secondary" v-on:mousedown="showPassword1" @mouseleave="hidepassword1" type="button" id="button-addon2"><i v-bind:class="eyeValue1?  'fa fa-eye-slash' : 'fas fa-eye'"></i></button>
                                </div>
                                <span v-if="v$.loginDetails.password.$error" class="error text-danger">
                                    <span v-if="!v$.loginDetails.password.required">{{ $t('AddSignUp.RegisterUser_Error_Required_Password') }}</span>
                                    <span v-if="!v$.loginDetails.password.strongPassword">{{ $t('AddSignUp.RegisterUser_Error_Format_Password') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.ConfirmPassword') }}:<span class="text-danger"> *</span></label>

                            <div v-bind:class="{'has-danger' : v$.loginDetails.confirmPassword.$error}">

                                <div class="input-group mb-3">
                                    <input id="password" v-model="v$.loginDetails.confirmPassword.$model" name="password" :type="password" class="form-control" v-bind:placeholder="$t('AddSignUp.Password')" aria-label="Recipient's username" aria-describedby="button-addon2">
                                    <button class="btn btn-secondary" v-on:mousedown="showPassword" @mouseleave="hidepassword" type="button" id="button-addon2"><i v-bind:class="eyeValue?  'fa fa-eye-slash' : 'fas fa-eye'"></i></button>
                                </div>


                                <!--<div class="input-group" >

                                    <input id="password" v-model="v$.loginDetails.confirmPassword.$model" name="password" :type="password" v-bind:placeholder="$t('AddSignUp.Password')" class="form-control"  v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? ' border-top-right-radius: 0; border-bottom-right-radius: 0;' : ' border-top-right-radius: 0; border-bottom-right-radius: 0;border-right: 2px !important' ">
                                    <div class="input-group-prepend" v-on:mousedown="showPassword" @mouseleave="hidepassword">
                                        <span class="input-group-text" style="padding:10px;border-top-right-radius: 5px;border-bottom-right-radius: 5px;border-right:1px solid #4d49716b;cursor:pointer;}">
                                            <i v-bind:class="eyeValue?  'fa fa-eye-slash' : 'fas fa-eye'"></i>
                                        </span>
                                    </div>-->

                                    <!--<button style="  border-left:!important none; border:1px solid rgb(190 190 191); background-color:white"  v-on:click="showPassword"><i class="fas fa-eye"></i></button>-->
                                <!--</div>-->     
                                <span v-if="v$.loginDetails.confirmPassword.$error" class="error text-danger">
                                    <span v-if="!v$.loginDetails.confirmPassword.required">{{ $t('AddSignUp.RegisterUser_Error_Required_ConfirmPassword') }}</span>
                                    <span v-if="!v$.loginDetails.confirmPassword.sameAsPassword">{{ $t('AddSignUp.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-6 ">
                            <label>{{ $t('AddSignUp.Roles') }}:<span class="text-danger"> *</span></label>
                            <div>
                                <roledropdown ref="roleComponent" @update:modelValue="SaleManRol(loginDetails.roleId)" v-model="loginDetails.roleId" :key="rander" :modelValue="loginDetails.roleId"></roledropdown>
                            </div>
                        </div>
                        <div class="col-sm-6  form-group">
                            <label>Terminal User Type :<span class="text-danger"> </span></label>
                                    
                            <multiselect :disabled="terminalUserTypeDisabled" :options="terminalUserTypeOptions" @update:modelValue="onCahngeTerminalUserType"
                                v-model="loginDetails.terminalUserType" :show-labels="false" placeholder="Terminal User Type">
                            </multiselect>
                        </div>
                        <div class="col-sm-6  form-group" v-if="(loginDetails.terminalUserType == 'Online' || loginDetails.terminalUserType == 'Both')" >
                            <label>Online</label>
                            <div>
                                <!--<terminal-dropdown v-model="loginDetails.terminalId" />-->
                                <terminal-dropdown  v-model="loginDetails.terminalId" :terminalType="terminalType" :terminalUserType="'Online'" :isSelect="true"  v-bind:key="terminalRander+terminalRander" :modelValue="loginDetails.terminalId" />
                            </div>
                        </div>
                        <div class="col-sm-6  form-group" v-if="(loginDetails.terminalUserType == 'Offline' ||  loginDetails.terminalUserType == 'Both')">
                            <label>Offline</label>
                            <div>
                                <!--<terminal-dropdown v-model="loginDetails.terminalId" />-->
                                <terminal-dropdown  v-model="loginDetails.terminalId" :terminalType="terminalType" :terminalUserType="'Offline'"  :isSelect="true" v-bind:key="terminalRanderOnline+terminalRander" :modelValue="loginDetails.terminalId" />
                            </div>
                        </div>
                        <div class="col-sm-6  form-group" >
                            <label>Bank Selection:</label>
                            <multiselect v-model="loginDetails.documentType"  :preselect-first="false"  :options="[ 'User Wise', 'Department Wise',]" :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                            </multiselect> 
                        </div>
                        <div class="col-sm-6  form-group" v-if="loginDetails.documentType=='User Wise'" >
                            <label>Bank</label>
                            <div>
                                <bankdropdown v-model="loginDetails.bankId" @update:modelValue="GetBank1Account" :modelValue="loginDetails.bankId"></bankdropdown>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body" v-if="loginDetails.id!='00000000-0000-0000-0000-000000000000'">
                    <div class="row">
                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.UserId') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.loginDetails.userName.$error}">
                                <input class="form-control" v-model="v$.loginDetails.userName.$model"  />
                                <span v-if="v$.loginDetails.userName.$error" class="error text-danger">
                                    <span v-if="!v$.loginDetails.userName.required"> {{ $t('AddSignUp.RegisterUser_Error_Required_DisplayName') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-6 ">
                            <label>{{ $t('AddSignUp.Roles') }}:<span class="text-danger"> *</span></label>
                            <div>
                                <roledropdown ref="roleComponent"  v-model="loginDetails.roleId" :key="rander" :modelValue="loginDetails.roleId" @update:modelValue="SaleManRol(loginDetails.roleId)"></roledropdown>
                            </div>
                        </div>
                        <div class="col-sm-6  form-group" >
                            <label>Bank Selection:</label>
                            <multiselect v-model="loginDetails.documentType"  :preselect-first="false"  :options="[ 'User Wise', 'Department Wise',]" :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                            </multiselect> 
                        </div>
                        <div class="col-sm-6  form-group" v-if="loginDetails.documentType=='User Wise'" >
                            <label>Bank</label>
                            <div>
                                <bankdropdown v-model="loginDetails.bankId" @update:modelValue="GetBank1Account" modelValue="loginDetails.bankId"></bankdropdown>
                            </div>
                        </div>
                        <div class="col-sm-6" hidden>
                            <label>{{ $t('AddSignUp.UserEmail') }}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.loginDetails.email.$error}">
                                <input disabled class="form-control " v-model="v$.loginDetails.email.$model"  />
                                <span v-if="v$.loginDetails.email.$error" class="error text-danger">
                                    <span v-if="!v$.loginDetails.email.required">{{ $t('AddSignUp.RegisterUser_Error_Required_EmailID') }}</span>
                                    <span v-if="!v$.loginDetails.email.email"> {{ $t('AddSignUp.RegisterUser_Error_Format_EmailID') }}</span>
                                </span>
                            </div>
                        </div>


                        <div class="col-sm-6 mb-2">
                        </div>
                       
                    </div>
                    


                </div>
                <div v-if="!loading" class="card-footer">
                    <div class="row">
                        <div v-if="!loading" class=" col-md-12">
                            <div class="button-items">
                                <button class="btn btn-primary" v-bind:disabled="v$.loginDetails.$invalid" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddSignUpUser')" v-on:click="SaveLoginDetails"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddSignUp.btnSave') }}</button>
                                <button class="btn btn-primary" v-bind:disabled="loginDetails.roleId==''" v-if="loginDetails.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditSignUpUser')" v-on:click="UpdateLoginDetails"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddSignUp.btnUpdate') }}</button>
                                <button class="btn btn-danger" v-on:click="Cancel">{{ $t('AddSignUp.btnClear') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
             
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>

            </div>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import useVuelidate from '@vuelidate/core'
    import { required } from '@vuelidate/validators';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                invoiveItem: false,
                terminalUserTypeOptions:[],
                invoiveBarCode: false,
                terminalUserTypeDisabled: false,
                invoiveBarCodeItem: false,
                terminalType: '',
                dayStart: '',
                arabic: '',
                onlineTerminalId:'',
                english: '',
                isTouch: '',
                invoiceWoInventory: '',
                registrationDate: '',
                arabicName: '',
                emailExist: false,
                loading: false,
                gender: '',
                idNumber: '',
                userId: '',
                rander: 0,
                randered: 0,
                randerBanks: 0,
                language: 'Nothing',
                password: "password",
                password1: "password",
                eyeValue: false,
                eyeValue1: false,
                isOpenDay: '',
                terminalRander: 0,
                terminalRanderOnline: 0,
                options: [],
                loginDetails: {
                    id: '00000000-0000-0000-0000-000000000000',
                    email: '',
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
                    terminalUserType:'',
                    invoiceWoInventory: false,
                    terminalId: '',
                    bankId: '',
                    documentType: '',
                    isTouchInvoice: false,
                    isActive: true,
                    allowAll: false,
                    permissionToStartExpenseDay: false,
                    isSupervisor: false,
                    temporaryCashReceiver: false,
                    temporaryCashIssuer: false,
                    temporaryCashRequester: false,
                    roleId: '',
                    days: 0,
                    limit: 0,
                    touchScreen: ''
                }
            }
        },
        validations() {
            return {
                loginDetails: {
                    userName: { required },
                    password:
                    {
                        required,
                        strongPassword(password) {
                            return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/.test(password);
                        }
                    },
                    confirmPassword:
                    {
                        required
                    },
                    email: { required},
                    
                }
            }
        },
        methods: {
            GetBank1Account: function (account) {
                debugger; //eslint-disable-line

                this.loginDetails.bankId = account.id;
                },
            onCahngeTerminalUserType: function(){
                
                if(this.loginDetails.terminalUserType == 'Both'){
                    this.terminalRanderOnline++
                    this.terminalRander++
                }
                else if(this.loginDetails.terminalUserType == 'Online'){
                    this.terminalRanderOnline++
                }
                else{

                    this.terminalRander++
                }
            },
            
            showPassword() {
                if (this.password === "password") {
                    this.password = "text";
                    this.eyeValue = true;
                }

            },
            hidepassword() {
                this.password = "password"
                this.eyeValue = false;
            },
            showPassword1() {

                if (this.password1 === "password") {
                    this.password1 = "text";
                    this.eyeValue1 = true;
                }

            },
            hidepassword1() {

                this.password1 = "password"
                this.eyeValue1 = false;
            },
            //userNameRejix: function (value) {
            //
            //    this.userName = value.replace(/[^A-Z0-9]/ig, "").toLowerCase();
            //    console.log(this.userName);
            //},
            SaleManRol: function (id) {
               
                if(id == null || id == '')
                {
                    return
                }
              
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesDetail?Id=' + id.id, { headers: { "Authorization": `Bearer ${token}` } })
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
                            root.GetTerminalData();
                        }
                    });


            },
            GetTerminalData() {
            var selectedRole = this.$refs.roleComponent?.GetNameOfSelected();
            if (selectedRole === 'Salesman') {
                this.terminalType = 'CashCounter';
                this.terminalRander++;
            } else {
                this.terminalType = 'All';
                this.terminalRander++;
            }
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.loginDetails.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddSignUp');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text:(this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            EmailDuplicate: function (x) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.emailExist = false;
                this.$https.get('/account/DuplicateEmail?email=' + x, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data == true) {
                            root.emailExist = true;
                        }
                        else {
                            root.emailExist = false;
                        }


                    })
            },

            Cancel: function () {
                this.$router.push({
                    path: '/signUp',

                })
            },
            getTerminalIds: function (value) {
                this.loginDetails.terminals = value;
            },
            getEmployeeDetails: function (event) {
                var selectedemployeeId = event;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + selectedemployeeId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.loginDetails.employeeId = response.data.id;
                            root.registrationDate = response.data.registrationDate;
                            if (root.arabic == 'true' && root.english == 'false') {
                                root.arabicName = response.data.arabicName;
                            }
                            else if (root.arabic == 'false' && root.english == 'true') {
                                root.arabicName = response.data.englishName;
                            }
                            else {
                                root.arabicName = response.data.arabicName;
                            }
                            //root.loginDetails.userName = response.data.englishName;
                            root.loginDetails.email = response.data.email;
                            root.gender = response.data.gender;
                            root.idNumber = response.data.idNumber;
                            root.registrationDate = moment().format("DD MM YYYY");
                            if (root.loginDetails.email != '' && root.loginDetails.email != null) {
                                root.EmailDuplicate(root.loginDetails.email);
                            }
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            SaveLoginDetails: function () {
                
                this.loading = true;
                var root = this;
                var token = '';
                
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.emailExist) {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الإلكتروني موجود بالفعل!',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1700,
                        timerProgressBar: true,
                    });
                }
                else {

                    if (this.isTouch == 'Touch Invoice') {
                        this.loginDetails.isTouchInvoice = true
                    }
                    else {
                        this.loginDetails.isTouchInvoice = false
                    }
                    localStorage.setItem('AllowAll', this.loginDetails.allowAll);
                    if (this.loginDetails.roleId.id != undefined || this.loginDetails.roleId.id != null) {
                        this.loginDetails.roleId = this.loginDetails.roleId.id;

                    }
                    if(this.loginDetails.documentType=='Department Wise')
                    {
                        this.loginDetails.bankId='';
                    }
                  
                    root.$https
                        .post('/account/SaveUser', this.loginDetails, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(response => {
                            if (response.data != "Already Exists") {
                                root.loading = false
                                root.info = response.data.bpi


                                root.$swal.fire({
                                    icon: 'success',
                                    title: 'Saved Successfully',
                                    showConfirmButton: false,
                                    timer: 1800,
                                    timerProgressBar: true,

                                });
                                root.$router.push('/signUp');
                            }
                            else {
                                root.loading = false
                                root.info = response.data.bpi

                                root.$swal.fire({
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
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Server Error',
                                    text: error,
                                });

                                root.loading = false
                        })
                        .finally(() => root.loading = false)
                }
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
                this.loginDetails.isActive = !this.loginDetails.isActive;
                localStorage.setItem('AllowAll', this.loginDetails.allowAll);
                if (this.loginDetails.roleId.id != undefined || this.loginDetails.roleId.id != null) {
                    this.loginDetails.roleId = this.loginDetails.roleId.id;

                }
                if(this.loginDetails.documentType=='Department Wise')
                    {
                        this.loginDetails.bankId='';
                    }
                root.$https
                    .post('/account/SaveUser', this.loginDetails, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null) {
                            root.loading = false

                            root.$swal.fire({
                                icon: 'success',
                                title: 'Saved Successfully',
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
                                title: 'Server Error',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            getUserWiseRecords: function () {

                if (this.$route.query.data != undefined) {
                    this.loginDetails.id = this.$route.query.data.id;
                    
                    this.loginDetails.onlineTerminalId = this.$route.query.data.onlineTerminalId;
                    if(this.$route.query.data.terminalUserType != 0)
                        this.loginDetails.terminalUserType = this.$route.query.data.terminalUserType;
                    this.userId = this.$route.query.data.userId;
                    this.loginDetails.roleId = this.$route.query.data.roleId;
                    this.loginDetails.bankId = this.$route.query.data.bankId;
                    this.loginDetails.documentType = this.$route.query.data.documentType;
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
            this.$emit('update:modelValue', this.$route.name);
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
        },
        mounted: function () {
            this.invoiveItem = localStorage.getItem('invoiveItem') == "true" ? true : false;
            this.invoiveBarCode = localStorage.getItem('invoiveBarCode') == "true" ? true : false;
            this.invoiveBarCodeItem = localStorage.getItem('invoiveBarCodeItem') == "true" ? true : false;
            this.GetTerminalData();

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

        }
    })
</script>
<style scoped>
</style>