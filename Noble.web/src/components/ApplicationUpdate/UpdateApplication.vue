<template>
    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="col-lg-6 col-sm-6 ml-auto mr-auto " style="margin-top:100px;">
            <div class="card " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">

                <div class="row">
                    <div class="col-12">
                        <table class="table table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <thead class="m-0">
                                <tr>
                                    <th style="text-align:center;">
                                        {{$t('UpdateApplication.CurrentVersion')}}
                                    </th>
                                    <th style="text-align:center;">
                                        {{$t('UpdateApplication.LatestVersion')}}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="text-align:center;">{{latestVersion}}</td>
                                    <td style="text-align:center;">{{applicationVersion}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-12" >
                        <!---->
                        <a href="javascript:void(0)" class="btn btn-primary " :disabled="latestVersion == applicationVersion" style="float: right "
                           v-on:click="ApplicationUpdate">
                            {{$t('UpdateApplication.UpdateApplication')}}
                        </a>
                    </div>
                </div>
            </div>
        </div>

    </div>


</template>

<script>
    //import axios from 'axios'
    export default {

        data: function () {
            return {
                latestVersion: '',
                applicationVersion: '',
                loginHistory: {
                    userId: '',
                    isLogin: false,
                    companyId: ''
                }
            }
        },
        methods: {
            CheckApplicationUpdate: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var url = '/Company/CheckApplicationUpdate';
                this.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (!response.data.isSuccess) {
                        root.latestVersion = response.data.applicationVersion;
                        root.applicationVersion = response.data.latestVersion;

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
                });
            },
            logoutHistorySave: function () {

                this.loginHistory.userId = localStorage.getItem('UserID')
                this.loginHistory.companyId = localStorage.getItem('CompanyID')
                this.$https.post('/account/LoginHistory', this.loginHistory).then(function (response) {
                    if (response.data == 1)
                        console.log('Logout History save done');
                    else
                        console.log('Logout History not save due to some error ' + response.data);
                });
            },
            logout: function () {
                var root = this;
                var counterId = localStorage.getItem('CounterId')
                var companyId = localStorage.getItem('CompanyID')
                //  var Swal = this.$swal
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {
                    
                    if (response.data == "Success") {

                        root.logoutHistorySave();
                        //root.$session.destroy();
                        root.$store.commit('logout');
                        localStorage.clear();
                        //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));

                        window.location.href = "http://localhost:8008/?=" + companyId + "=" + counterId;
                        root.$router.push('/')
                        return "Success"
                    }
                    else {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Error Logging Out'
                            });
                        return "Not Success"
                    }

                });

            },
            ApplicationUpdate: function () {
                
                this.logout();
                
                
                
            },
        },
        created() {
            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {
            localStorage.setItem('ApplicationUpdateRefresh', 0)
            this.CheckApplicationUpdate()
        }
    }


</script>