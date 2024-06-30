<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">
                <div class="setup_menu" v-if="isValid('CanViewSignUpUser')" v-on:click="GotoPage('/signUp')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/sign up user.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('UserSetup.SignUpUser') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('UserSetup.SignUpUserDescription') }}
                        </p>
                    </div>
                </div>
               

                <div class="setup_menu" v-if=" isValid('CanViewUserRole')" v-on:click="GotoPage('/Roles')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/user permissions.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('UserSetup.UserRoles') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('UserSetup.UserRolesUserDescription') }}
                        </p>
                    </div>
                </div>
                <!--<div class="setup_menu" v-if="isValid('CanViewUserPermission')" v-on:click="GotoPage('/Permissions')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/User roles.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('UserSetup.Permissions') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('UserSetup.PermissionsUserDescription') }}
                        </p>
                    </div>
                </div>-->

                <div class="setup_menu" v-if="isValid('Can View Permission')" v-on:click="GotoPage('/CompanyOption')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/User roles.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('UserSetup.CompanyOption') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('UserSetup.PermissionsCompanyOptionDescription') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-on:click="GotoPage('/UserDefineFlow')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/User roles.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('UserSetup.UserDefineFlow') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('UserSetup.UserDefineFlowDescription') }}
                        </p>
                    </div>
                </div>


            </div>
        </div>
        <div class="setup_main_panel" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'setup_main_panel_ar'">

            <div class="col-md-12 col-lg-12 ">
                <div class="card img mt-2 mb-2" style="background-color: #3178F6;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'imageLeft' : 'imageRight'">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-2 pt-2 ">
                                <img src="User Setup.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">{{ $t('UserSetup.UserSetup') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('UserSetup.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page">{{ $t('UserSetup.UserSetup') }}</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>

                    </div>
                </div>
                <div v-if="path == 'UserSetup' ">
                    <div class="row">
                        <div class="col-12" style="text-align:center;width:250px; height:250px;margin-top:141px">
                            <img src="Empty form.svg" />
                            <h5 class="HeadingOfEmpty">{{ $t('UserSetup.EmptyForms') }}</h5>
                            <p>{{ $t('UserSetup.Selectformtheleft') }}</p>
                        </div>
                    </div>
                </div>
                <div v-else>
                    <router-view></router-view>

                </div>
            </div>

        </div>
    </div>
    <!--<div v-else> <acessdenied></acessdenied></div>-->
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data() {
            return {

                path: 'UserSetup',
                role: ''
            }
        },
       
        methods: {
            syncSetup: function () {
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/System/SyncSetup', { headers: { "Authorization": `Bearer ${token}` } });
            },
            GotoPage: function (link) {
                this.path = link;
                this.$router.push({
                    path: link,
                });
            },
        },
        created: function () {
            this.path = this.$route.name;
            this.$emit('update:modelValue', this.$route.name);
        },
        beforeMount: function () {

            this.role = localStorage.getItem("RoleName");
        }
    }
</script>
<style scoped>
    .img {
        height: 160px;
        background-size: contain !important;
        background-repeat: no-repeat !important;
    }
</style>
