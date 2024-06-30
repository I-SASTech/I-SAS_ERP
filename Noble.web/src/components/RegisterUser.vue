<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('RegisterUser.MySetting') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">{{ $t('RegisterUser.MySetting') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" v-bind:key="rander">
                <div class="card-body">
                    <div class="nav-tabs-custom text-center pb-4">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link text-center active" data-bs-toggle="tab" href="#Profile_Post" role="tab" aria-selected="true"><i class="far fa-user d-block"></i>{{ $t('RegisterUser.Profile') }}</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-center" data-bs-toggle="tab" href="#Profile_Portfolio" role="tab" aria-selected="false"><i class="fa fa-eye d-block" aria-hidden="true"></i>{{ $t('RegisterUser.ChangePassword') }}</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-center" data-bs-toggle="tab" href="#Profile_Settings" role="tab" aria-selected="false"><i class="fas fa-history d-block"></i>{{$t('RegisterUser.LoginHistory')}}</a>
                            </li>
                        </ul>
                    </div>

                    <div class="tab-content" id="pills-tabContent">

                        <div class="tab-pane fade active show" id="Profile_Post" role="tabpanel" aria-labelledby="Profile_Post_tab">
                            <div class="row">
                                <div class="col-lg-10 col-xl-10 mx-auto">
                                    <div class="card">
                                        <div class="card-header">
                                            <div class="row align-items-center">
                                                <div class="col">
                                                    <h4 class="card-title">{{ $t('RegisterUser.ProfileSetting') }}</h4>
                                                </div><!--end col-->
                                            </div>  <!--end row-->
                                        </div><!--end card-header-->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-4" v-bind:key="randerImage">
                                                    <Userprofileimage v-on:picPath="getImage" v-bind:path="ImagePath" />
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group row mb-mt-2">
                                                        <label class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.FirstName') }}:*</label>
                                                        <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.user.firstName.$error}">
                                                            <input class="form-control " autofocus="autofocus" v-model="v$.user.firstName.$model" />
                                                            <span v-if="v$.user.firstName.$error" class="error text-danger">
                                                                <span v-if="!v$.user.firstName.required">{{ $t('RegisterUser.NameRequired') }}</span>
                                                                <span v-if="!v$.user.firstName.maxLength">{{ $t('RegisterUser.NameMaximum') }}</span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.LastName') }}:*</label>
                                                        <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.user.lastName.$error}">
                                                            <input class="form-control" autofocus="autofocus" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.user.lastName.$model" />
                                                            <span v-if="v$.user.lastName.$error" class="error text-danger">
                                                                <span v-if="!v$.user.lastName.required">{{ $t('RegisterUser.NameRequired') }}</span>
                                                                <span v-if="!v$.user.lastName.maxLength">{{ $t('RegisterUser.NameMaximum') }}</span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="user.email" class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.RegisterUser_EmailID') }}:*</label>
                                                        <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.user.email.$error}">
                                                            <input class="form-control" disabled type="text" v-model="v$.user.email.$model" />
                                                            <span v-if="v$.user.email.$error" class="error text-danger">
                                                                <span v-if="!v$.user.email.required">{{ $t('RegisterUser.RegisterUser_Error_Required_EmailID') }}</span>
                                                                <span v-if="!v$.user.email.email">{{ $t('RegisterUser.RegisterUser_Error_Format_EmailID') }}</span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-lg-12 col-xl-12">
                                                            <button class="btn btn-soft-primary   mr-2" v-on:click="SaveUser">{{ $t('RegisterUser.UpdateProfile') }}</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div> <!--end col-->
                            </div><!--end row-->
                        </div>
                        <div class="tab-pane fade" id="Profile_Portfolio" role="tabpanel" aria-labelledby="portfolio_detail_tab">
                            <div class="col-lg-10 col-xl-10 mx-auto">
                                <div class="card">
                                    <div class="card-header">
                                        <div class="row align-items-center">
                                            <div class="col">
                                                <h4 class="card-title">{{ $t('RegisterUser.ChangePassword') }}</h4>
                                            </div><!--end col-->
                                        </div>  <!--end row-->
                                    </div><!--end card-header-->
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-8 mx-auto">
                                                <div class="form-group row">
                                                    <label for="user.password" v-if="isPassword==false" class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.CurrentPasswordVerification') }}</label>
                                                    <label for="user.password" v-else class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.CurrentPassword') }}:*</label>
                                                    <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.userPassword.currentPassword.$error}">
                                                        <input class="form-control" type="password" v-model="v$.userPassword.currentPassword.$model" />
                                                        <span v-if="v$.userPassword.currentPassword.$error" class="error text-danger">
                                                            <span v-if="!v$.userPassword.currentPassword.required">{{ $t('RegisterUser.RegisterUser_Error_Required_Password') }}</span>
                                                        </span>
                                                    </div>
                                                    <div class="button-items">
                                                        <button class="btn btn-primary mt-2" v-if="isPassword==false" v-on:click="IsCurrentPassword">
                                                            {{ $t('RegisterUser.VerifyCurrentPassword') }}
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="form-group row" v-if="isPassword">
                                                    <label for="user.password" class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.RegisterUser_Password') }}:*</label>
                                                    <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.userPassword.password.$error}">
                                                        <input class="form-control " type="password" v-model="v$.userPassword.password.$model" />
                                                        <span v-if="v$.userPassword.password.$error" class="error text-danger">
                                                            <span v-if="!v$.userPassword.password.required">{{ $t('RegisterUser.RegisterUser_Error_Required_Password') }}</span>
                                                            <span v-if="!v$.userPassword.password.strongPassword">{{ $t('RegisterUser.RegisterUser_Error_Format_Password') }}</span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group row" v-else>
                                                    <label for="user.password" class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.RegisterUser_Password') }}:*</label>
                                                    <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.userPassword.password.$error}">
                                                        <input class="form-control" type="password" disabled v-model="v$.userPassword.password.$model" />
                                                        <span v-if="v$.userPassword.password.$error" class="error text-danger">
                                                            <span v-if="!v$.userPassword.password.required">{{ $t('RegisterUser.RegisterUser_Error_Required_Password') }}</span>
                                                            <span v-if="!v$.userPassword.password.strongPassword">{{ $t('RegisterUser.RegisterUser_Error_Format_Password') }}</span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group row" v-if="isPassword">
                                                    <label for="user.confirmPassword" class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.RegisterUser_ConfirmPassword') }}:*</label>
                                                    <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.userPassword.confirmPassword.$error}">
                                                        <input class="form-control " type="password" v-model="v$.userPassword.confirmPassword.$model" />
                                                        <span v-if="v$.userPassword.confirmPassword.$error" class="error text-danger">
                                                            <span v-if="!v$.userPassword.confirmPassword.required">{{ $t('RegisterUser.RegisterUser_Error_Required_ConfirmPassword') }}</span>
                                                            <span v-if="!v$.userPassword.confirmPassword.sameAsPassword">{{ $t('RegisterUser.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group row" v-else>
                                                    <label for="user.confirmPassword" class="col-xl-12 col-lg-12  mb-lg-0 align-self-center">{{ $t('RegisterUser.RegisterUser_ConfirmPassword') }}:*</label>
                                                    <div class="col-lg-12 col-xl-12" v-bind:class="{'has-danger' : v$.userPassword.confirmPassword.$error}">
                                                        <input class="form-control " disabled type="password" v-model="v$.userPassword.confirmPassword.$model" />
                                                        <span v-if="v$.userPassword.confirmPassword.$error" class="error text-danger">
                                                            <span v-if="!v$.userPassword.confirmPassword.required">{{ $t('RegisterUser.RegisterUser_Error_Required_ConfirmPassword') }}</span>
                                                            <span v-if="!v$.userPassword.confirmPassword.sameAsPassword">{{ $t('RegisterUser.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-lg-12 col-xl-12">
                                                        <div v-if="!loading">
                                                            <button class="btn btn-soft-primary" v-bind:disabled="v$.userPassword.$invalid" v-on:click="updateInformation">{{ $t('RegisterUser.UpdatePassword') }}</button>
                                                        </div>
                                                        <div v-else>
                                                            <loading v-model:active="loading" :can-cancel="true" :on-cancel="onCancel" :is-full-page="true"></loading>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade " id="Profile_Settings" role="tabpanel" aria-labelledby="settings_detail_tab">
                            <div class="col-lg-10 col-xl-10 mx-auto">
                                <div class="card">
                                    <div class="card-header">
                                        <div class="row align-items-center">
                                            <div class="col">
                                                <h4 class="card-title">{{$t('RegisterUser.LoginHistory')}}</h4>
                                            </div><!--end col-->
                                        </div>  <!--end row-->
                                    </div><!--end card-header-->
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group row" v-if="role=='Admin'">
                                                    <div class="col-md-4">
                                                        <label>{{$t('RegisterUser.SelectUser')}}</label>
                                                        <usersDropdown :isloginhistory="isloginhistory" @update:modelValue="getUser"></usersDropdown>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>{{ $t('RegisterUser.FromDate') }}</label>
                                                        <datepicker v-model="fromDate" :key="randerDate" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>{{ $t('RegisterUser.ToDate') }}</label>
                                                        <datepicker v-model="toDate" :key="randerDate" />
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-body">
                                                        <div class="col-lg-12">
                                                            <div>
                                                                <div class="table-responsive">
                                                                    <table class="table mb-0">
                                                                        <thead class="thead-light table-hover">
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th v-if="role=='Admin'">
                                                                                    {{$t('RegisterUser.RegisterUser_UserName')}}
                                                                                </th>
                                                                                <th>
                                                                                    {{$t('RegisterUser.LoginDate')}}
                                                                                </th>
                                                                                <th>
                                                                                    {{$t('RegisterUser.LoginTime')}}
                                                                                </th>
                                                                                <th>
                                                                                    {{$t('RegisterUser.LogoutDate')}}
                                                                                </th>
                                                                                <th>
                                                                                    {{$t('RegisterUser.LogoutTime')}}
                                                                                </th>


                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr v-for="(history,index) in loginHistoryList" :key="index">
                                                                                <td>
                                                                                    {{index+1}}
                                                                                </td>

                                                                                <td v-if="role=='Admin'">
                                                                                    <strong>
                                                                                        {{fullName}}
                                                                                    </strong>
                                                                                </td>

                                                                                <td>
                                                                                    {{history.loginDate}}
                                                                                </td>
                                                                                <td>
                                                                                    {{history.loginTime}}
                                                                                </td>
                                                                                <td>{{history.logoutDate}}</td>
                                                                                <td>{{history.logoutTime}}</td>

                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div><!--end tab-pane-->
                        <div v-if="active == 'TwoFactor'">
                            <div>
                                <div class="row" v-if="isMainScreen">
                                    <div class="col-sm-10 text-center mx-auto">
                                        <div class="row text-center">
                                            <div class="col-lg-8 col-sm-8 mx-auto">
                                                <div>
                                                    <h4>{{ $t('RegisterUser.Setuptwofactorauthentication') }}</h4>
                                                    <p>
                                                        {{ $t('RegisterUser.Descriptoin1') }}
                                                        {{ $t('RegisterUser.Descriptoin2') }}
                                                    </p>
                                                    <div>
                                                        <div>
                                                            <div class="button-items">
                                                                <button type="submit" class="btn btn-primary " v-if="disable2FaButton==false && disableResetButton==false" v-on:click="Disable2Fa">  {{ $t('RegisterUser.DISABLE2Fa') }}</button>
                                                                <button type="submit" class="btn btn-primary " v-on:click="ResetAuthenticator" v-if="disableResetButton==false">{{ $t('RegisterUser.RESETAUTHENTICATORAPP') }}</button>
                                                                <div class="text-center">
                                                                    <button class="btn btn-primary  " v-if="disable2FaButton==true || disableResetButton==true" v-on:click="Add2Factor(false)">{{ $t('RegisterUser.AddAuthenticatorApp') }}</button>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" v-else-if="is2Factor">
                                    <div class="col-sm-10 text-center mx-auto" v-if="recoveryCode==''">
                                        <div class="card-header">
                                            <h6> {{ $t('RegisterUser.Descriptoin2') }}</h6>
                                            <p>{{ $t('RegisterUser.TouseDescription') }}</p>
                                            <div>

                                                <ol class="list">
                                                    <li>
                                                        <p>
                                                            {{ $t('RegisterUser.Downloadtwo-factor') }}
                                                            <a href="https://go.microsoft.com/fwlink/?Linkid=825071">{{ $t('RegisterUser.WindowsPhone') }}</a>,
                                                            <a href="https://go.microsoft.com/fwlink/?Linkid=825072">{{ $t('RegisterUser.Android') }}</a> {{ $t('RegisterUser.and') }}
                                                            <a href="https://go.microsoft.com/fwlink/?Linkid=825073">{{ $t('RegisterUser.iOS') }}</a> {{ $t('RegisterUser.or') }}
                                                            {{ $t('RegisterUser.GoogleAuthenticatorFor') }}
                                                            <a href="https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&amp;hl=en">{{ $t('RegisterUser.Android') }}</a> {{ $t('UserProfile.and') }}
                                                            <a href="https://itunes.apple.com/us/app/google-authenticator/id388497605?mt=8">{{ $t('RegisterUser.iOS') }}</a>.
                                                        </p>
                                                    </li>

                                                    <li>
                                                        <qr-code :text="key"></qr-code>
                                                    </li>
                                                    <li>
                                                        <p>
                                                            {{ $t('RegisterUser.Onceyouhavescanned') }}
                                                            {{ $t('RegisterUser.withauniquecode') }}
                                                        </p>
                                                        <div class="row">
                                                            <div class="col-md-6 mx-auto">

                                                                <div>

                                                                    <label>  {{ $t('RegisterUser.VerificationCode') }}</label>
                                                                    <div>
                                                                        <input class="form-control" v-model="verificationCode" />

                                                                    </div>
                                                                </div>
                                                                <div class="button-items">
                                                                    <button type="submit" class="btn btn-primary mt-2" v-on:click="VerificationCode"> {{ $t('RegisterUser.Verify') }}</button>
                                                                </div>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ol>
                                            </div>
                                        </div>

                                        <div v-if="!loading">
                                            <!--<button class="btn btn-primary   mr-2" v-on:click="Add2Factor">Add Authenticator App</button>-->
                                        </div>
                                        <div>
                                            <loading v-model:active="loading" :can-cancel="true" :on-cancel="onCancel" :is-full-page="true"></loading>
                                        </div>

                                    </div>
                                    <div class="col-sm-10 text-center mx-auto">
                                        <div class="card-header">
                                            <h3>{{ $t('RegisterUser.RecoveryCodes') }}</h3>
                                            <p>
                                                <span class="glyphicon glyphicon-warning-sign"></span>
                                                <strong>{{ $t('RegisterUser.Putthesecodes') }}</strong>
                                            </p>
                                            <p>
                                                {{ $t('RegisterUser.Ifyoulose') }}
                                            </p>
                                            <p>
                                                {{ $t('RegisterUser.Generatingnewrecovery') }}
                                                {{ $t('RegisterUser.usedinanauthenticator') }}
                                            </p>
                                            <div class="jumbotron">  <p>{{recoveryCode }}</p></div>

                                            <button type="submit" class="btn btn-primary " v-on:click="MainScreen">{{ $t('RegisterUser.MainScreen') }}</button>

                                        </div>

                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-sm-10 text-center mx-auto">
                                        <div class="card-header">
                                            <h6>{{ $t('RegisterUser.Setuptwofactorauthentication') }}</h6>
                                            <p>{{ $t('RegisterUser.Twofactorauthenticationprovidesr') }}</p>
                                        </div>

                                        <div v-if="!loading">
                                            <button class="btn btn-primary   mr-2" v-on:click="Add2Factor">{{ $t('RegisterUser.AddAuthenticatorApp') }}</button>
                                        </div>
                                        <div v-else>
                                            <loading v-model:active="loading" :can-cancel="true" :on-cancel="onCancel" :is-full-page="true"></loading>
                                        </div>

                                    </div>

                                </div>

                            </div>

                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>


</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'

    import useVuelidate from '@vuelidate/core'
    import { required, maxLength, sameAs, email } from '@vuelidate/validators'; 
    import moment from "moment";
    export default ({
        mixins: [clickMixin],
        
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                fromDate: '',
                toDate: '',
                user: {
                    id: '',
                    firstName: '',
                    lastName: '',
                    email: '',
                    imagePath: ''
                },
                role: '',
                fullName: '',
                userPassword: {
                    currentPassword: '',
                    password: '',
                    confirmPassword: '',
                },
                userVerify: {
                    userName: '',
                    userId: '',
                    password: '',
                },
                isloginhistory: true,
                loading: false,
                rander: 0,
                token: '',
                UserName: '',
                UserID: '',
                ImagePath: '',
                active: 'profile',
                isPassword: false,
                is2Factor: false,
                isMainScreen: false,
                AllowMainScreen: false,
                disable2FaButton: false,
                disableResetButton: false,
                key: '',
                verificationCode: '',
                recoveryCode: '',
                randerImage: 0,
                loginHistoryList: [],
                userIdForHistory: '00000000-0000-0000-0000-000000000000',
                randerDate: 0
            }
        },
        validations() {
            return {
                user:
                {
                    firstName:
                    {
                        required,
                        maxLength: maxLength(20)
                    },
                    lastName:
                    {
                        required,
                        maxLength: maxLength(20)
                    },


                    email:
                    {
                        required,
                        email
                    }
                },
                userPassword: {
                    currentPassword:
                    {
                        required,
                    },

                    password:
                    {
                        required,
                        strongPassword(password) {
                            return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/.test(password);
                        }
                    },
                    confirmPassword:
                    {
                        required,
                        sameAsPassword: sameAs('password')
                    },
                },
            }
        },
        watch: {
            toDate: function () {

                this.LoginHistoryList();
            },
            fromDate: function () {
                this.LoginHistoryList();
            }
        },
        methods: {

            getUser: function (value) {

                this.fullName = value.name;
                this.userIdForHistory = value.id;
                this.LoginHistoryList()
            },
            LoginHistoryList: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.role == 'Admin' && this.userIdForHistory != '') {

                    root.$https.get('account/LoginHistoryList?userId=' + this.userIdForHistory + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate, { headers: { "Authorization": `Bearer ${token}` } }).then(function (responce) {

                        root.loginHistoryList = responce.data
                        root.role = responce.data[0].userRole
                    });
                }
                else {
                    root.$https.get('account/LoginHistoryList?userId=' + this.UserID, { headers: { "Authorization": `Bearer ${token}` } }).then(function (responce) {

                        root.loginHistoryList = responce.data
                        root.role = responce.data[0].userRole
                    });
                }
            },
            MainScreen: function () {
                this.isMainScreen = true;
                this.disable2FaButton = false;
                this.disableResetButton = false;
            },
            Disable2Fa: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                root.$https
                    .get('/Account/Disable2Fa?id=' + this.UserID, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data == false) {

                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Your !',
                                    text: 'Not Disable Your 2-Factor Authentication ',
                                    showConfirmButton: false,
                                    timer: 1000,
                                    timerProgressBar: true,

                                });
                        }
                        if (response.data == true) {
                            root.disable2FaButton = true;
                            root.recoveryCode = '';

                            root.$swal({
                                title: "Success!",
                                text: "Successfully Disable Your 2-Factor Authentication !",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,
                            });

                        }
                    });
            },
            ResetAuthenticator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                root.$https
                    .get('/Account/ResetAuthenticator?id=' + this.UserID, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data == false) {

                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Your !',
                                    text: 'Not Reset Your 2-Factor App ',
                                    showConfirmButton: false,
                                    timer: 1000,
                                    timerProgressBar: true,

                                });
                        }
                        if (response.data == true) {
                            root.disableResetButton = true;
                            root.recoveryCode = '';

                            root.$swal({
                                title: "Success!",
                                text: "Successfully Reset Your 2-Factor App !",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,
                            });

                        }
                    });
            },
            VerificationCode: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                root.$https
                    .get('/Account/OnPostAsync?id=' + this.UserID + '&code=' + this.verificationCode, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data == false) {

                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Your Enter Wrong Verification Code!',
                                    text: 'Your Enter Wrong Verification Code!',
                                    showConfirmButton: false,
                                    timer: 1000,
                                    timerProgressBar: true,

                                });
                        }
                        if (response.data != null) {

                            root.verificationCode = '';
                            root.recoveryCode = response.data;

                        }
                    });
            },

            GenerateString: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Account/LoadSharedKeyAndQrCodeUriAsync?id=' + this.UserID, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            root.key = response.data;

                        }
                    });
            },
            Add2Factor: function () {
                this.recoveryCode = '';
                this.isMainScreen = false;
                this.is2Factor = true;

                this.GenerateString();


                //var root = this;
                //var token = '';
                //if (token == '') {
                //    token = localStorage.getItem('token');
                //}
                //
                //root.$https
                //    .get('/Account/Allow2Factor?id=' + this.UserID, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                //

                //        if (response.data == true) {
                //
                //            root.isMainScreen = false;
                //            root.is2Factor = true;

                //            root.GenerateString();


                //        }
                //        if (response.data == false) {

                //            root.customError = "You Enter Wrong Verification Code";
                //        }

                //    }).catch(error => {
                //        console.log(error)
                //    });

            },
            updateInformation: function () {

                var root = this;
                this.userVerify.userId = this.UserID;
                this.userVerify.password = this.userPassword.password;
                this.$https.post('/account/UpdatePassword', this.userVerify).then(function (response) {

                    if (response.data != "Error") {
                        root.$swal({
                            title: "Success!",
                            text: "You've successfully changed your Password.!",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,
                        });

                    }
                    if (response.data == "Error") {

                        root.customError = "You Enter invalid entries";
                    }

                }).catch(error => {
                    console.log(error)
                })
            },

            IsCurrentPassword: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.userVerify.userName = this.UserName;
                this.userVerify.password = this.userPassword.currentPassword;

                root.$https
                    .post('/Company/PasswordVerify', this.userVerify, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data == true) {
                            root.isPassword = true;

                            root.$swal({
                                title: "Verified!",
                                text: "Confirm Password is match.!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,
                            });
                        }
                        else {
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Your Password is not match!',
                                    text: 'Your Password is not match!',
                                    showConfirmButton: false,
                                    timer: 1000,
                                    timerProgressBar: true,

                                });
                        }
                    });
            },
            getBase64Image: function (path) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            root.ImagePath = response.data;
                            root.randerImage++;

                        }
                    });
            },
            GetImage: function () {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Company/ImageSearch?id=' + this.UserID, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            root.ImagePath = response.data.imagePath;
                            root.user.firstName = response.data.firstName;
                            root.user.lastName = response.data.lastName;
                            root.isMainScreen = response.data.twoFactorEnabled;
                            root.getBase64Image(root.ImagePath);

                        }
                    });
            },
            getImage: function (value) {

                this.user.imagePath = value;
                this.getBase64Image(this.user.imagePath);

            },
            GoToCompany: function () {

                this.$router.push('/user');
            },
            makeActive: function (tab) {
                this.active = tab;
            },
            SaveUser: function () {
                //es;int-disable-line
                this.loading = true;
                var root = this;
                this.user.id = this.UserID;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .post('/Company/UpdateUser', root.user, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        this.loading = false
                        this.info = response.data.bpi
                        this.$swal.fire({
                            icon: 'success',
                            title: 'Saved Successfully',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,

                        });

                        root.rander++;
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });

                        this.loading = false
                    })
                    .finally(() => this.loading = false)
            }
        },
        mounted: function () {
            this.LoginHistoryList();

            this.fullName = localStorage.getItem('FullName');
            this.UserName = localStorage.getItem('UserName');
            this.UserID = localStorage.getItem('UserID');
            this.CompanyId = localStorage.getItem('CompanyID');
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.user.email = this.UserName;
            this.randerDate++
            this.GetImage();





        }
    })
</script>