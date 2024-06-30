<template>
    <div class="row" v-if="isValid('CanFlushDatabase')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('FlushDatabase.FlushDatabase') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a
                                            href="javascript:void(0);">{{ $t('FlushDatabase.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('FlushDatabase.FlushDatabase') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Terminal.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="card">
                        <div class="card-body">
                            <div class="row" v-if="lang == 'en'">
                                <div class="col-md-6 pb-3" v-for="list in flushList" v-bind:key="list.id">

                                    <div class="checkbox">
                                        <input v-model="list.checked"
                                            v-on:change="onSelectTable($event.target.checked, list.id)" :id="list.id"
                                            type="checkbox">
                                        <label :for="list.id">
                                            {{ list.name }}
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row" v-if="lang == 'ar'">
                                <div class="col-md-6 pb-3" v-for="list in flushListArabic" v-bind:key="list.id">
                                    <h5>
                                        <input type="checkbox" v-model="list.checked"
                                            v-on:change="onSelectTable($event.target.checked, list.id)">

                                        {{ list.name }}
                                    </h5>
                                </div>
                            </div>
                            <div>
                                <a href="javascript:void(0)" class="btn btn-outline-primary  " :disabled="loading1"
                                    v-on:click="SupervisorLogin">
                                    {{ $t('FlushDatabase.Flush') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
            </div>

            <loading :name="loading1" v-model:active="loading1"
                     :can-cancel="true"
                     :is-full-page="true"></loading>

            <supervisor-login-model @close="onCloseEvent"
                                    :show="show"
                                    :isFlushData="true"
                                    :isReset="false"
                                    v-if="show" />
        </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>


<script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin';
    
    
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                loading1: false,
                flushList: [
                    { id: 1, name: "All Data", table: "AllData", checked: false },
                    { id: 2, name: "Except Product info", table: "ProductInfo", checked: false },
                    { id: 3, name: "Except Product info, customer and supplier", table: "Contact", checked: false },
                ],
                flushListArabic: [
                    { id: 1, name: "كل المعلومات", table: "AllData", checked: false },
                    { id: 2, name: "باستثناء معلومات المنتج", table: "ProductInfo", checked: false },
                    { id: 3, name: "باستثناء معلومات المنتج والعميل والمورد", table: "Contact", checked: false },
                ],
                show: false,
                records: '',

            loginHistory: {
                userId: '',
                isLogin: false,
                companyId: ''
            },
            lang: ''
        }
    },
    methods: {
        languageChange: function (lan) {
            if (this.lang == lan) {
                var getLocale = this.$i18n.locale;
                this.lang = getLocale;
                this.$router.go('/FlushDatabase');
            }
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        onSelectTable: function (isChecked, id) {
            var root = this;
            this.flushList.forEach(x => {
                if (x.id != id) {
                    x.checked = false;
                } else {
                    x.checked = true;
                    root.records = x.table;
                }
            })
        },
        onCloseEvent: function (flag) {
            if (flag) {
                this.flush()
            }
            this.show = false
        },
        SupervisorLogin: function () {
            this.show = true;
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
            var url = '/account/logout';
            this.$https.post(url, this.login).then(function (response) {

                if (response.data == "Success") {

                        root.logoutHistorySave();
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
        },

            flush: function () {
                var root = this;
                this.loading1 = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading1 = true;

            root.$https
                .get('/System/FlushRecords?records=' + this.records, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.status == 200) {
                        root.logout();
                        root.$swal({
                            title: "Success!",
                            text: "Flush data successfully",
                            type: 'error',
                            confirmButtonClass: "btn btn-Success",
                            buttonStyling: false,

                        });

                    }
                    root.loading1 = false;

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هناك خطأ ما!', 
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                        root.loading1 = false;
                    });
            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.lang = localStorage.getItem('locales')
        }

}
</script>