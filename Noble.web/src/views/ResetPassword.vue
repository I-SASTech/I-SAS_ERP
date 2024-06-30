<template>
    <body class="account-body accountbg">
        <div class="container">
            <div class="row vh-100 d-flex justify-content-center">
                <div class="col-12 align-self-center">
                    <div class="row">
                        <div class="col-lg-5 mx-auto">
                            <div class="card">
                                <div class="card-body p-0 auth-header-box bg-white">
                                    <div class="text-center p-3">
                                        <a href="/" class="logo logo-admin">
                                            <img src="NobleLogin.png" height="50" alt="logo" class="auth-logo">
                                        </a>
                                        <h4 class="mt-3 mb-1 fw-semibold  font-18" style="color: #0C213A;">Oobagest-ERP</h4>
                                        <p class="text-muted  mb-0">{{ $t('login.Tailoredforeveryonesmalltolargescale') }}</p>
                                    </div>
                                </div>
                                <div class="card-body p-0">
                                    <div class="tab-content">
                                        <div class="tab-pane active p-3" id="LogIn_Tab" role="tabpanel">
                                            <div class="form-horizontal auth-form">

                                                <div class="form-group mb-2">
                                                    <label class="form-label">New Password</label>
                                                    <div class="input-group">
                                                        <input class="form-control inputLength" placeholder="Enter Your New Password" @keyup.enter="updateInformation" v-model="v$.login.password.$model" :type="password" />
                                                    </div>
                                                </div><!--end form-group-->

                                                <div class="form-group mb-2">
                                                    <label class="form-label">Confirm Password</label>
                                                    <div class="input-group">
                                                        <input class="form-control inputLength" placeholder="Enter Your Confirm Password" @keyup.enter="updateInformation" v-model="v$.login.confirmPassword.$model" :type="password" />
                                                    </div>

                                                </div><!--end form-group-->

                                                <div class="form-group row my-3">
                                                    <div class="col-sm-6">

                                                            <input  @change="showPassword" type="checkbox" />
                                                            <label class="form-label text-muted mx-1" for="customSwitchSuccess">Show Password</label>

                                                    </div><!--end col-->
                                                    <div class="col-sm-6 text-end">

                                                    </div><!--end col-->
                                                </div><!--end form-group-->

                                                <div class="form-group mb-0 row">
                                                    <div class="col-12">
                                                        <small v-if="customError != ''" class="text-danger">{{ customError }}</small>
                                                        <button v-on:click="updateInformation" v-bind:disabled="v$.login.$invalid" class="btn btn-primary w-100 waves-effect waves-light" type="button">
                                                            <i v-if="loading" class="la la-refresh text-secondary la-spin progress-icon-spin" style="font-size:.8125rem;"></i>
                                                            <span v-else>Reset Password  <i class="fas fa-sign-in-alt ms-1"></i></span>
                                                        </button>
                                                    </div><!--end col-->
                                                </div> <!--end form-group-->
                                            </div><!--end form-->
                                            <!--<div class="m-3 text-center text-muted">
                                            <p class="mb-0">Don't have an account ?  <a href="auth-register.html" class="text-primary ms-2">Free Resister</a></p>
                                        </div>-->
                                            <div class="account-social">
                                                <h6 class="mb-3 " style="color:transparent;">-</h6>
                                            </div>
                                            <div class="btn-group w-100">
                                                <button type="button" class="btn btn-sm btn-outline-secondary">Support</button>
                                                <button type="button" class="btn btn-sm btn-outline-secondary">Contact Us</button>
                                                <button type="button" class="btn btn-sm btn-outline-secondary">Visit Us</button>
                                            </div>
                                        </div>
                                    </div>
                                </div><!--end card-body-->
                                <div class="card-body bg-light-alt text-center">
                                    <span class="text-muted d-none d-sm-inline-block">
                                        <small>Version 1.2.9.5 Last Updated Sep 12, 2022. Future Solutions (Pvt) Ltd.</small>
                                    </span>
                                </div>
                            </div><!--end card-->
                        </div><!--end col-->
                    </div><!--end row-->
                </div><!--end col-->
            </div><!--end row-->
        </div>
    </body>
    <!--<div class="container-fluid login_main" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'direction: rtl;'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">

        <div class="row" style="height:100vh;">
            <div id="fadeshow1" class="col-sm-6 col-lg-6  col-md-6 backgroundColorlightBlue">
                <div class="row" style="height:20vh;padding-top: 35px;">
                    <div class="col-12" style="margin-bottom:0px !important;">
                        <h6 class="nobleHeading " v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding-left: 40px;margin-bottom: 3px;' : 'padding-right: 40px !important;margin-bottom: 3px;text-align:right'">
                            {{ $t('login.ERPForBusinessSolutions') }}
                        </h6>
                        <p class="lightParagraph" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding-left: 40px;' : 'padding-right: 40px;text-align:right'">
                            {{ $t('login.Tailoredforeveryonesmalltolargescale') }}
                        </p>
                    </div>
                    <div class="col-12" style="margin-top:18px">

                    </div>
                </div>
                <div class="row" style="height:70vh; margin-top:5vh;margin-bottom:10vh;">
                    <div class="rounded mx-auto d-block">
                        <img src="/invoiceImage.png" style="height:60vh;" />
                    </div>
                </div>
                <div style="position: fixed; bottom: 40px; " v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'left: 40px;' : 'right: 40px;'">
                    <img src="/VatScreen.png" />
                </div>

                <p style=" position: absolute; bottom: 40px; line-height:1;" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align: right;right: 15px;' : 'text-align: left;left: 15px;'">
                    <span class="headingOfVersion">VERSION 1.0.0.5 Last Updated Dec 12, 2021</span><br />
                    <span style="font-weight:bold;font-size:12px;">TechQode (Pvt) Ltd.</span>
                </p>
            </div>
            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 " style="background-color:white;align-self: center;margin-top:-30px;">
                <ul class="navbar-nav text-right" style="position: fixed; top: 15px;z-index:1;" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? ' right: 20px;' : ' left: 20px;'">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle  pr-3" style="font-size:16px;" href="javascript:void(0)" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <i class="fas fa-globe"></i>
                        </a>
                        <div class="dropdown-menu" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'dropdown-menu-right' : 'dropdown-menu-left'" aria-labelledby="navbarDropdownMenuLink">
                            <a class="dropdown-item" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" @click="setLocale('en')">English</a>
                            <a class="dropdown-item" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="font-size: 0.8571em;  font-weight: 700" @click="setLocale('ar')">الـعــربـيـة</a>
                        </div>
                    </li>
                </ul>


                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 ">

                        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'login_div' : 'login_div-right'">
                            <div>

                                <img src="NobleLogin.png" style="width:auto;height:90px;">
                            </div>
                            <div class=" lightParagraph2" style="margin-top:50px">
                                <p>Reset Password</p>
                            </div>

                            <div class="form-group" style="margin-top:0px">
                                <label class="labelHeading font-weight-bold" style="margin-top:5px"> New Password</label>
                                <input class="form-control inputLength" placeholder="Enter Your New Password" @keyup.enter="updateInformation" v-model="v$.login.password.$model" :type="password" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                            </div>

                            <div class="form-group" style="margin-top:25px">
                                <label class="labelHeading font-weight-bold" style="margin-top:0px">  Confirm Password</label>
                                <input class="form-control inputLength" placeholder="Enter Your Confirm Password" @keyup.enter="updateInformation" v-model="v$.login.confirmPassword.$model" :type="password" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />-->
    <!--<span style="position:absolute;  top:43px;" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'right:9px;' : 'left:9px;'">

        <span @click="showPassword" style="color:#D3D7DF">
            <i style="font-size: 18px; color: #50565F;" v-bind:class="eyeValue?  'fa fa-eye-slash' : 'fas fa-eye'"></i>
        </span>
    </span>-->
    <!--</div>

                            <div class="row" style="margin-top:24px;">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group" style="display:inline-flex;">
                                        <input style="width: 20px; height: 20px;"  @change="showPassword" type="checkbox" />
                                        <span v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'RememberMe' : 'RememberMeRight' ">Show Password</span>
                                    </div>
                                </div>

                            </div>

                            <div class="row" style="margin-top:35px;">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <button type="button" class="btn btn-block btn-primary" v-on:click="updateInformation" v-bind:disabled="v$.login.$invalid">
                                        <span v-if="!loading">Reset Password</span>
                                        <img v-else src="/images/loading.gif" style="height:15px;" />

                                    </button>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <p v-if="customError != ''" class="text-left text-danger">{{ customError }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>-->




    <!--<div class="login-page">
        <div class="wrapper wrapper-full-page" style="background:url(/assets/img/bg/newBackground.jpg) no-repeat; background-size: cover; ">

            <div class="full-page section-image" filter-color="black">
                <div class="content">
                    <div class="container">
                        <div class="col-lg-5 col-md-5 ml-auto mr-auto">
                            <div>
                                <div class="card card-login">
                                    <div class="card-body">
                                        <h3 class="header text-center headingColor3">Reset Password</h3>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-key"></i>
                                                </span>
                                            </div>
                                            <input id="password" v-model="v$.login.password.$model" name="password" type="password" placeholder="New Password" class="form-control">
                                        </div>
                                        <span v-if="v$.login.password.$error" class="error text-left text-danger">
                                            <p v-if="!v$.login.password.required">Password is Required</p>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-key"></i>
                                                </span>
                                            </div>
                                            <input id="confirmPassword" v-model="v$.login.confirmPassword.$model" name="confirmPassword" type="password" placeholder="Confirm Password" class="form-control">
                                        </div>
                                        <span v-if="v$.login.confirmPassword.$error" class="error text-danger">
                                            <p v-if="!v$.login.confirmPassword.required">Confirm Password is Required</p>
                                            <p v-if="!v$.login.confirmPassword.sameAsPassword">Confirm Password must be same</p>
                                        </span>
                                    </div>
                                    <span v-if="customError != ''" class="error text-danger">
                                        <p>{{customError}}</p>
                                    </span>
                                    <div class="card-footer" v-if="login.password == login.confirmPassword">
                                        <button class="btn btn-outline-primary  btn-block mb-3" v-on:click="updateInformation">Reset Password</button>
                                    </div>
                                    <div class="card-footer" v-if="login.password != login.confirmPassword">
                                        <button disabled class="btn btn-outline-primary  btn-block mb-3" v-on:click="updateInformation">Reset Password</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <footer class="footer footer-black  footer-white ">
                    <div class="container-fluid">
                        <div class="row">
                            <nav class="footer-nav">
                                <ul>
                                    <li>
                                        <a href="javascript:void(0)" target="_blank"> Version 1.0.0000</a>
                                    </li>
                                </ul>
                            </nav>
                            <div class="credits ml-auto">
                                <span class="copyright">
                                    © 2021, <a href="https://www.techqode.com/" style="color:white; font-weight:bold;" target="_blank">Techqode</a> Pvt Ltd.-->
    <!--<a id="goto-website" class="headingColor2" href="javascript:void(0)" v-on:click="gotoWebSite">www.techqode.com</a>-->
    <!--</span>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
    </div>-->
</template>
<script>
    import useVuelidate from '@vuelidate/core'
    import { required, sameAs } from '@vuelidate/validators'; 
    export default {
        setup() {
            return { v$: useVuelidate() }
        },
        data() {
            return {
                login: {
                    password: '',
                    confirmPassword: '',
                    userId: '00000000-0000-0000-0000-000000000000'
                },
                customError: '',
                website: 'https://www.techqode.com/',
                password: "password",
                eyeValue: false,
                loading: false,
            }
        },
        validations() {
            return {
                login:
                {
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
                    }
                }
                }
        },
        methods: {
            gotoWebSite: function() {
                window.location.href = this.website;
            },

            cancel: function() {
                this.$router.push('/')
            },

            updateInformation: function () {
                var root = this;
                this.loading = true;

                var url = '/account/UpdatePassword';
                this.$https.post(url, this.login).then(function (response) {
                    
                    if (response.data != "Error") {
                        root.loading = false;
                        root.$swal({
                            title: "Success!",
                            text: "You've successfully changed your Password.!",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,
                        });
                        root.$router.push('/');
                    }
                    if (response.data == "Error") {
                        root.loading = false;
                        root.customError = "Please Enter Valid Email Address.";
                    }

                }).catch(error => {
                        console.log(error)
                    })
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
        },
        mounted: function () {
            this.login.userId = this.$route.query.id;
        }
    }
</script>
