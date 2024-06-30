<template>
    <div id="chartofaccount" v-cloak v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div v-if="accounts===''">
            <div class="col-lg-4 col-sm-4 offset-sm-4 mt-4" >
                <div class="card card-signup text-center">
                    <div class="card-header ">
                        <h4 class="card-title DayHeading">{{ $t('COA.SelectaccountType') }}</h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row ">
                                    <div class="col-md-12">
                                        <div v-if="!loading">
                                            <div class="form-group" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                <select v-model="template" class="form-control"  id="account" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                    <option value="" hidden>{{ $t('CoaTemplate.SelectTemplate') }}</option>
                                                    <option :value="templatetype">{{ $t('CoaTemplate.TemplateType') }}</option>
                                                </select>
                                            </div>
                                            <button class="btn btn-primary btn-block " v-on:click="createChartOfAccount(template)" v-bind:class="{'disabled': template==''}">{{ $t('CoaTemplate.Save') }}</button><br />
                                        </div>

                                        <div v-if="loading">
                                            <div class="row">
                                                <div class="col-lg-5 mr-auto ml-auto text-center">
                                                    <div class="loadingio-spinner-dual-ball-44dlc48bacw">
                                                        <div class="ldio-m86dw9oanea">
                                                            <div> </div>
                                                            <div> </div>
                                                            <div> </div>
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
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

        <coa-modal :show="showModal"
                   :newaccount="newAccount"
                   :account-type-id="accountTypeId"
                   v-on:close="showModal = false"
                   :type="type"
                   v-if="showModal" />
    </div>
    
</template>
<script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
         components: {
            Loading
        },
        data: function () {
            return {
                accounts: '',
                Accounts: '',
                poItem: [],
                loading: false,
                showModal: false,
                active: 'Assets',
                type: "",
                template: '',
                accountTypeId: '',
                accountType: '',
                newAccount: {
                    costCenterId: '',
                    code: '',
                    description: '',
                    id: '',
                    name: '',
                    openingBalance: '',
                    runingBalance: '',
                    isActive: true
                },
                stepsVm: {
                    companyId: '',
                    step1: false,
                    step2: false,
                    step3: false,
                    step4: false,
                },
                templatetype: 'Business',
                accountslist: []
            }
        },
        methods: {
            makeActive: function (item) {
                this.active = item;
            },
            removeAccount: function (Id, costCenterId, accountTypeId) {
                var root = this;
                // working with IE and Chrome both


                this.$swal({
                    title: this.$t('AddUpdateAccount.AreYouSure'),
                    text: this.$t('AddUpdateAccount.NotRecover'),
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: this.$t('AddUpdateAccount.YesDeleteIt')
                }).then(function () {


                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Accounting/RemoveAccount?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data) {
                                var accountDelete = root.accounts.accountTypes.find(function (x) {
                                    return x.id === accountTypeId;
                                }).costCenters.find(function (y) {
                                    return y.id === costCenterId;
                                }).accounts.findIndex(function (z) {
                                    return z.id === Id;
                                });
                                root.accounts.accountTypes.find(function (x) {
                                    return x.id === accountTypeId;
                                }).costCenters.find(function (y) {
                                    return y.id === costCenterId;
                                }).accounts.splice(accountDelete, 1);

                                root.$swal({
                                    title: this.$t('AddUpdateAccount.Deleted'),
                                    text: this.$t('AddUpdateAccount.AccountDeleted'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 800,
                                    timerProgressBar: true,
                                });
                            } else {
                                console.log("error: something wrong from db.");
                            }
                        },
                            function (error) {
                                root.$swal({
                                    title: this.$t('AddUpdateAccount.Error'),
                                    type: 'error',
                                    text: error,
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 800,
                                    timerProgressBar: true,
                                });
                            });
                });
            },
            createChartOfAccount: function (template) {

                var root = this;
                this.loading = true;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('/Accounting/TemplateAccountSetup?template=' + template, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(

                        root.stepsVm.companyId = localStorage.getItem('CompanyID'),
                        root.stepsVm.step1 = true,

                        root.$https.post('/account/SetupUpdateInCompany', root.stepsVm, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                 root.loading = false
                                if (response.data) {
                                    localStorage.setItem('coa', true);
                                    if (root.$route.query.IsSetup != undefined) {
                                        root.$router.push({
                                            path: '/Setup',
                                            query: {
                                                coa: true
                                            }
                                        });
                                    }
                                } else {
                                    console.log("error: something wrong from db.");
                                }
                            })



                    )
            },
            updateModal: function (Id, accountTypId, type) {
                var root = this;
                this.showModal = !this.showModal;
                this.type = type;
                this.accountTypeId = accountTypId;

                // working with IE and Chrome both



                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Accounting/GetAccount?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data) {
                            root.newAccount = {
                                id: response.data.id,
                                code: response.data.code,
                                costCenterId: response.data.costCenterId,
                                name: response.data.name,
                                isActive: response.data.isActive,
                                description: response.data.description,
                                openingBalance: response.data.openingBalance,
                                runingBalance: response.data.runingBalance,
                            };
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function () {
                            root.loading = false;
                        });

            },

            addModal: function (costCenterId, accountTypId, type) {
                this.showModal = !this.showModal;
                this.type = type;
                this.accountTypeId = accountTypId;

                this.newAccount = {
                    id: '',
                    code: '',
                    costCenterId: costCenterId,
                    name: '',
                    isActive: true,
                    description: ''
                };

            },
            GetData: function () {
                var token = '';
                var root = this;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Accounting/Charts', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.templatetype = "Business"
                        root.$store.GetAccountList(response.data);
                        root.accounts = root.$store.isaccounts;
                    })
            }
        },
        created: function () {
            
            this.GetData();

        },
        mounted: function () {
            
            this.makeActive('Assets');
        },
    }
</script>