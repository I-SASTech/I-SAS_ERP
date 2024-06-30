<template>

    <body>
        <!--Left Sidenav-->
        <div class="left-sidenav">
            <!-- LOGO -->
            <div class="brand text-start ms-2">
                <a href="index.html" class="logo">
                    <span>
                        <img src="msFakhry.png" alt="logo-small" class="logo-sm"
                             style="width:100px;height:auto; max-height:45px;">
                    </span>

                </a>
            </div>
            <!--end logo-->
            <div class="menu-content h-100" data-simplebar>
                <ul class="metismenu left-sidenav-menu">
                    <li>
                        <a href="javascript:void(0);" v-on:click="GoTo('/TermsAndConditions')">
                            <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>
                                {{ $t('TermsDashboard.TermsCondition') }}
                            </span>
                        </a>
                    </li>
                    <li v-if="termsCondition=='true'">
                        <a href="javascript:void(0);" v-on:click="GoTo('/Setup')">
                            <i data-feather="clock" class="align-self-center menu-icon"></i><span>
                                {{ $t('TermsDashboard.CompanySetup') }}
                            </span>
                        </a>
                    </li>









                </ul>

            </div>
        </div>
        <!--end left-sidenav-->

        <div class="page-wrapper">
            <!--Top Bar Start-->
            <div class="topbar">
                <!--Navbar-->
                <nav class="navbar-custom">
                    <ul class="list-unstyled topbar-nav float-end mb-0">
                        <li class="dropdown hide-phone">
                            <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect"
                               data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false"
                               aria-expanded="false">
                                <i data-feather="search" class="topbar-icon"></i>
                            </a>

                            <div class="dropdown-menu dropdown-menu-end dropdown-lg p-0">
                                <!-- Top Search Bar -->
                                <div class="app-search-topbar">
                                    <form action="#" method="get">
                                        <input type="search" name="search" class="from-control top-search mb-0"
                                               placeholder="Type text...">
                                        <button type="submit"><i class="ti-search"></i></button>
                                    </form>
                                </div>
                            </div>
                        </li>

                        <li class="dropdown notification-list">
                            <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect"
                               data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false"
                               aria-expanded="false">
                                <i data-feather="bell" class="align-self-center topbar-icon"></i>
                                <span class="badge bg-danger rounded-pill noti-icon-badge">2</span>
                            </a>
                            <!--<div class="dropdown-menu dropdown-menu-end dropdown-lg pt-0">

                                <h6 class="dropdown-item-text font-15 m-0 py-3 border-bottom d-flex justify-content-between align-items-center">
                                    Notifications <span class="badge bg-primary rounded-pill">2</span>
                                </h6>
                                <div class="dropdown-menu dropdown-menu-end">
                                    <a v-on:click="UserProfile" class="dropdown-item" href="javascript:void(0)">
                                        <i data-feather="user" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.MyProfile')
                                        }}
                                    </a>



                                </div>
                                All
                                <a href="javascript:void(0);" class="dropdown-item text-center text-primary">
                                    View all <i class="fi-arrow-right"></i>
                                </a>
                            </div>-->
                        </li>
                        <li class="dropdown">
                            <a class="nav-link dropdown-toggle waves-effect waves-light nav-user"
                               data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false"
                               aria-expanded="false">

                                <i class="fas fa-globe"></i>
                                Language
                            </a>
                            <div class="dropdown-menu dropdown-menu-end">
                                <a v-if="english=='true'" @click="setLocale('en')" class="dropdown-item" href="javascript:void(0)">
                                    English
                                </a>
                                <a v-if="arabic=='true'" @click="setLocale('ar')" class="dropdown-item" href="javascript:void(0)" >
                                    Arabic
                                </a>
                                <a v-if="portugues==true" @click="setLocale('pt')" class="dropdown-item" href="javascript:void(0)" >
                                    Portugues
                                </a>
                               
                            </div>
                        </li>

                        <li class="dropdown">
                            <a class="nav-link dropdown-toggle waves-effect waves-light nav-user"
                               data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false"
                               aria-expanded="false">
                                <span class="mx-1 nav-user-name hidden-sm">{{ DisplayUserName }}</span>
                                <img src="assets/images/users/user-5.jpg" alt="profile-user"
                                     class="rounded-circle thumb-xs" />
                            </a>
                            <div class="dropdown-menu dropdown-menu-end">
                                <a v-on:click="UserProfile" class="dropdown-item" href="javascript:void(0)">
                                    <i data-feather="user" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.MyProfile')
                                    }}
                                </a>
                                <a v-on:click="UpdateCompanyPermission" v-if="role != 'Noble Admin'"
                                   class="dropdown-item" href="javascript:void(0)">
                                    <i data-feather="settings"
                                       class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.SyncPermission')
                                    }}
                                </a>
                                <div class="dropdown-divider mb-0"></div>
                                <a class="dropdown-item" href="javascript:void(0)" v-on:click="logout()">
                                    <i data-feather="power" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.LogOut')
                                    }}
                                </a>
                            </div>
                        </li>
                    </ul>

                    <ul class="list-unstyled topbar-nav mb-0">
                        <li>
                            <button class="nav-link button-menu-mobile">
                                <i data-feather="menu" class="align-self-center topbar-icon"></i>
                            </button>
                        </li>

                    </ul>
                </nav>
                <!--end navbar-->
            </div>
            <!--Top Bar End-->
            <!--Page Content-->
            <div class="page-content">
                <router-view @update:modelValue="logoutUser"></router-view>
                <div v-if="dashboard == '/dashboard'">
                    <dashboard></dashboard>
                </div>


                <footer class="footer text-center text-sm-start">
                    <span>
                        &copy;
                        2022 <a href="https://www.techqode.com/" target="_blank" class="fw-normal">Nms Fakhry(Pvt) Ltd .</a>
                    </span>

                    <span class="text-muted d-none d-sm-inline-block float-end">
                        Version 1.2.9.5 Last Updated Dec 18, 2023
                    </span>
                </footer>
            </div>
        </div>
        <supervisor-login-model @close="onCloseEvent"
                                :show="show"
                                :isFlushData="true"
                                :isReset="true"
                                v-if="show" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </body>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import axios from 'axios'
    //import language from 'element-ui/lib/locale'
    //import ar from 'element-ui/lib/locale/lang/ar'
    //import en from 'element-ui/lib/locale/lang/en'
    //import pt from 'element-ui/lib/locale/lang/pt'
    export default {
        mixins: [clickMixin],
        name: 'locale-changer',
        components: {
            Loading,
        },

        data() {
            return {
                loading: false,
                portugues: false,

                noblePermissions: '',
                companyId: '',
                langs: ['en', 'ar'],
                DisplayUserName: '',
                role: '',
                dashboard: '',
                ur: '',
                isAccount: '',
                arabic: '',
                english: '',
                termsCondition: false,
                applicationName: ''
            }
        },
        methods: {
            GoTo: function (link, token, fromDashboard, formName, fromService) {
                localStorage.setItem('IsService', fromService);
                this.$router.push({ path: link, query: { token_name: token, fromDashboard: fromDashboard, formName: formName } });
            },
            UpdateCompanyPermission: function () {

                this.loading = true
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
                            text: 'Please Contact to support to update license',

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    root.loading = false

                });
            },
            SaveNoblePermissions: function (locationId) {

                var root = this;
                this.loading = true;
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
            setLocale(locale) {
                
                this.$i18n.locale = locale;
                //if (locale == 'en') {
                //    language.use(en)
                //}
                //else if (locale == 'pt') {
                //    language.use(pt)
                //}
                //else {
                //    language.use(ar)
                //}

                localStorage.setItem('locales', locale);
                this.$router.go(this.$router.currentRoute.fullPath)

            },


            UserProfile: function () {
                this.$router.push('/SetupUser');
            },
            logout: function () {
                var root = this;
                //  var Swal = this.$swal
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {

                    if (response.data == "Success") {
                        // root.$swal.fire(
                        // {
                        //     toast: true,
                        //     position: 'top-end',
                        //     showConfirmButton: false,
                        //     timer: 1500,
                        //     timerProgressBar: true,
                        //     icon: 'success',
                        //     title: 'Logged Out Successfully',
                        //     didOpen: (toast) => {
                        //         toast.addEventListener('mouseenter', Swal.stopTimer)
                        //         toast.addEventListener('mouseleave', Swal.resumeTimer)
                        // }});
                        //eslint-disable-line
                        //root.$session.destroy();
                        root.$store.commit('logout');
                        localStorage.clear();
                        //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));

                        //localStorage.removeItem('token');
                        //localStorage.removeItem('UserName');
                        //localStorage.removeItem('RoleName');
                        //localStorage.removeItem('CompanyID');
                        //localStorage.removeItem('UserID');
                        //localStorage.removeItem('FullName');
                        //localStorage.removeItem('locales');
                        //localStorage.removeItem('ImagePath');
                        //localStorage.removeItem('EmployeeId');
                        //localStorage.removeItem('currency');




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
            this.portugues = localStorage.getItem('Portugues') == "true" ? true : false;

            this.companyId = localStorage.getItem('CompanyID')
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.applicationName = localStorage.getItem('ApplicationName')
            if (this.$store.isAuthenticated) {
                //eslint-disable-line
                this.DisplayUserName = localStorage.getItem('UserName');
                this.role = localStorage.getItem('RoleName');
                this.isAccount = localStorage.getItem('isAccount');
                this.termsCondition = localStorage.getItem('TermsCondition');
                this.dashboard = this.$router.options.routes;

            }
            else {
                this.$router.push('/')
            }
        }

    }
</script>
