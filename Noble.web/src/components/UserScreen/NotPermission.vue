<template>
    <div class="row">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card border-0">
                <div>
                    <div class="card-body">
                        <h1 class="text-center"><code>{{ $t('NotPermission.AccessDenied') }}</code></h1>
                        <!-- <hr class="w3-border-white w3-animate-left" style="margin:auto;width:50%"> -->
                        <h3 class="text-center">{{message}}</h3>
                        <div class="row">

                            <div class="col-lg-4 text-center">

                            </div>
                            <div class=" col-lg-4 text-center" v-if="isUseMachine">

                            </div>
                            <div class=" col-lg-4 text-center" v-else>
                                <button type="button" class="btn btn-primary  btn-block" v-on:click="UpdateCompanyPermission">
                                    <span>
                                        {{ $t('NotPermission.SyncYourUpdatedLicense') }}
                                    </span>
                                </button>
                            </div>
                            <div class="col-lg-4 text-center">

                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>

<script>


    import clickMixin from '@/Mixins/clickMixin'
    import axios from 'axios'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },

        data: function () {
            return {
                loading: false,
                companyId: '',
                noblePermissions: '',
                message: '',
                isNotPayment: false,
                isUseMachine: false
            }
        },
        methods: {
            UpdateCompanyPermission: function () {

                this.loading = true;
                var root = this;
                axios.get(root.$PermissionIp + '/NoblePermission/GetAllPermissionData?id=' + this.companyId + '&systemType=' + root.$SystemType).then(function (response) {
                    if (response.data != null) {

                        root.noblePermissions = response.data.result
                        root.SaveNoblePermissions(root.companyId)
                    }

                }).catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: "Please Contact to support to update license",

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    root.loading = false

                });
            },
            SaveNoblePermissions: function (locationId) {
                this.loading = true;
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.noblePermissions.locationId = locationId;
                if (this.noblePermissions != '' || this.noblePermissions != null || this.noblePermissions != undefined) {
                    this.$https.post('/Company/SaveNoblePermission', this.noblePermissions, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.isSuccess == true) {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.logout()
                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                            }
                            root.loading = false
                        }).catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    text: error,

                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });

                            root.loading = false
                        });
                        
                }
            },

            logout: function () {
                var root = this;
                //  var Swal = this.$swal
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {

                    if (response.data == "Success") {

                        //root.logoutHistorySave();
                        //root.$session.destroy();
                        root.$store.commit('logout');
                        localStorage.clear();
                        //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));




                        root.$router.push('/')
                    }
                    else {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Error Logging Out'
                            });
                    }

                });

            }
        },
        mounted: function () {
            if (this.$route.query.data != undefined) {
                this.message = this.$route.query.data
            }
            if (this.$route.query.data != undefined) {
                this.isNotPayment = this.$route.query.isPayment
            }
            this.isUseMachine = this.$route.query.machine == "true" ? true : false
            this.companyId = localStorage.getItem('CompanyID')
        },
    }
</script>