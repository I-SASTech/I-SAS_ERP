<template>
    <div>
        <div class="wrapper wrapper-full-page" style="background:url(/assets/img/bg/newBackground.jpg) no-repeat; background-size: cover; ">
            <div class="row header" style="height: 30px;">
                <div class="col-lg-12 col-sm-12">
                    <img src="/assets/img/noblelogowithicon.png" style="margin-left: 130px; margin-top: 35px; width:150%; max-width:150px;">
                </div>
            </div>
            <div class="body full-page section-image mt-4" filter-color="black">
                <div class="content">
                    <div class="container">
                        <div class="col-lg-5 col-md-5 ml-auto mr-auto">
                            <div>
                                <div class="card card-login">
                                    <div class="card-body">
                                        <h3 class="header text-center headingColor3">New Password</h3>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-key"></i>
                                                </span>
                                            </div>
                                            <input id="password" v-model="v$.login.password.$model" name="password" type="password" placeholder="New Password" class="form-control">
                                        </div>
                                        <span v-if="v$.login.password.$error" class="error text-danger">
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
                                        <button class="btn btn-outline-primary  btn-block mb-3" v-on:click="updateInformation">New Password</button>
                                    </div>
                                    <div class="card-footer" v-if="login.password != login.confirmPassword">
                                        <button disabled class="btn btn-outline-primary  btn-block mb-3" v-on:click="updateInformation">New Password</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 70px;">
                <div class="col-6 ml-auto" style="padding-left: 15px;">
                    <p class="ml-4 mt-5 headingColor2">
                        Version 1.0.0000
                    </p>
                </div>
                <div class="col-6 mr-auto" style="padding-right: 15px;">
                    <p class="mr-4 mt-5 text-right headingColor2">
                        © 2021, Techqode Pvt Ltd. <br />
                        <a id="goto-website" class="headingColor2" href="javascript:void(0)" v-on:click="gotoWebSite">www.techqode.com</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
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
                    id: '00000000-0000-0000-0000-000000000000'
                },
                customError: '',
                website: 'https://www.techqode.com/'
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
            gotoWebSite: function () {
                window.location.href = this.website;
            },
            cancel: function () {
                this.$router.push('/')
            },
            updateInformation: function () {
                var root = this;
                var url = '/account/UpdatePassword?userId=' + this.login.id + '&password=' + this.login.password;
                this.$https.get(url).then(function (response) {

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
                        root.$router.push('/');
                    }
                    if (response.data == "Error") {

                        root.customError = "Please Enter Valid Email Address.";
                    }

                }).catch(error => {
                    console.log(error)
                })
            }
        },
        mounted: function () {
            this.login.id = this.$route.query.id;
        }
    }
</script>
<style scoped>
    .headingColor {
        color: #1e6eca !important;
        font-family: "Montserrat", "Helvetica Neue", Arial, sans-serif;
        font-size: 15px;
    }

    .headingColor3 {
        color: #1e6eca !important;
        font-family: "Montserrat", "Helvetica Neue", Arial, sans-serif;
        font-size: 26px;
    }

    .headingColor2 {
        color: #1e6eca !important;
        font-family: "Montserrat", "Helvetica Neue", Arial, sans-serif;
        font-size: 10px;
    }

    .forgotPassword {
        color: blue;
        font-family: 'Times New Roman', Times, serif;
        font-size: 16px;
    }
</style>