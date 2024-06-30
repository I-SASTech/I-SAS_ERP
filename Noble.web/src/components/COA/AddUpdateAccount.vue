<template>
    <div class="row" id="chartofaccount" v-cloak  v-if=" isValid('CanViewCOA')">


        <div>
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
            <div v-else>
                <div class="col-lg-12 col-sm-12">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="row">
                                    <div class="col">
                                        <h4 class="page-title">{{ $t('AddUpdateAccount.ChartOfAccount') }}</h4>
                                        <ol class="breadcrumb">
                                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{$t('AddUpdateAccount.Home')}}</a></li>
                                            <li class="breadcrumb-item active">{{ $t('AddUpdateAccount.ChartOfAccount') }}</li>
                                        </ol>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                            {{ $t('PurchaseOrder.Close') }}
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card mt-4">
                        <div class="card-header  mb-2">

                            <h5>{{ $t('AddUpdateAccount.ChartOfAccount') }}</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="mb-1">
                                        <div>
                                            <ul class="nav nav-tabs" data-tabs="tabs">
                                                <li class="nav-item" v-for="accountType in accounts.accountTypes" v-bind:key="accountType.id">
                                                    <a class="nav-link" v-bind:class="{'active show':active == accountType.name}" v-on:click="makeActive(accountType.name)" :id="accountType.name" data-toggle="pill" :href="'#'+accountType.id" role="tab" :aria-controls="accountType.id" aria-selected="true">{{($i18n.locale == 'en')?accountType.name:accountType.nameArabic}}</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="tab-content mt-4" id="nav-tabContent">
                                        <div v-for="accountType in accounts.accountTypes" v-bind:key="accountType.code">
                                            <div v-if="active == accountType.name">
                                                <div class="mt-2">
                                                    <div>

                                                        <div class="accordion" id="accordionExample">
                                                            <div v-for="(costCenter,index) in accountType.costCenters" :key="index" class="accordion-item">
                                                                <h5 class="accordion-header m-0" :id="'headingOne'+index">
                                                                    <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse" v-bind:data-bs-target="'#'+'collapseOne'+index" aria-expanded="false" v-bind:aria-controls="'collapseOne'+index">
                                                                        {{$i18n.locale == 'en' ?costCenter.code+': '+  costCenter.name :costCenter.code+': '+ costCenter.nameArabic }}
                                                                    </button>
                                                                </h5>
                                                                <div v-bind:id="'collapseOne'+index" class="accordion-collapse collapse" :aria-labelledby="'headingOne'+index" data-bs-parent="#accordionExample" style="">
                                                                    <div class="accordion-body">
                                                                        <table class="table">
                                                                            <thead class="thead-light table-hover">
                                                                                <tr>
                                                                                    <th>
                                                                                        {{ $t('AddUpdateAccount.Code') }}
                                                                                    </th>
                                                                                    <th v-if="english=='true'">
                                                                                        {{$englishLanguage($t('AddUpdateAccount.Name'))   }}
                                                                                    </th>
                                                                                    <th v-if="isOtherLang()">
                                                                                        {{$arabicLanguage($t('AddUpdateAccount.Name'))   }}
                                                                                    </th>
                                                                                    <th>
                                                                                        {{ $t('AddUpdateAccount.Status') }}
                                                                                    </th>
                                                                                    <!--<th>
                                                                                Opening Balance
                                                                            </th>-->
                                                                                    <th v-if=" isValid('CanEditCOA')">
                                                                                        {{ $t('AddUpdateAccount.Action') }}
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr v-for="account in costCenter.accounts " v-bind:key="account.id">
                                                                                    <td>
                                                                                        {{account.code}}

                                                                                    </td>
                                                                                    <td v-if="english=='true'">
                                                                                        {{account.name}}<br />
                                                                                        <span class="small">{{account.description}}</span>
                                                                                    </td>
                                                                                    <td v-if="isOtherLang()">
                                                                                        {{account.nameArabic}}<br />
                                                                                        <span class="small">{{account.description}}</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span v-if="account.isActive" class="badge badge-boxed  badge-outline-success">{{$t('AddUpdateAccount.Active')}}</span>
                                                                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('AddUpdateAccount.De-Active')}}</span>
                                                                                    </td>
                                                                                    <td v-if="(costCenter.code=='160000' || costCenter.code=='120000' || costCenter.code=='200000' || costCenter.code=='210000') && isValid('CanEditCOA')">
                                                                                    </td>
                                                                                    <td v-else-if="isValid('CanEditCOA')">
                                                                                        <button title="Edit Account" class="btn btn-soft-primary  btn-icon btn-sm" v-on:click="updateModal(account.id, accountType.id, 'Edit')">
                                                                                            <i class="fas fa-pencil-alt"></i>
                                                                                        </button>
                                                                                        &nbsp;
                                                                                        <!--<button title="Delete Account" class="btn btn-danger  btn-icon btn-sm" v-on:click="removeAccount(account.id,costCenter.id,accountType.id)">
                                                                                    <i class="fa fa-trash"></i>
                                                                                </button>-->
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                            <tfoot>
                                                                                <tr>
                                                                                    <td colspan="4" class="text-center" v-if=" isValid('CanAddCOA')">
                                                                                        <button title="Add New Account" class="btn btn-soft-primary  btn-icon btn-sm" v-on:click="addModal(costCenter.id, accountType.id, 'Add')">
                                                                                            <i class="fas fa-plus"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                            </tfoot>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <coa-modal :show="showModal"
                   :newaccount="newAccount"
                   :account-type-id="accountTypeId"
                   v-on:close="showModal = false"
                   :type="type"
                   v-if="showModal" />
    </div>
    <acessdenied v-else :model=true></acessdenied>
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
                arabic: '',
                english: '',
                Accounts: '',
                poItem: [],
                loading: false,
                showModal: false,
                active: 'Assets',
                type: "",
                template: '',
                collpase: '',
                accountTypeId: '',
                accountType: '',
                newAccount: {
                    costCenterId: '',
                    code: '',
                    description: '',
                    id: '',
                    name: '',
                    nameArabic: '',
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
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            makeActiveCollapse: function (item) {
                this.collpase = item;
            },
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
                    confirmButtonText: this.$t('AddUpdateAccount.YesDeleteIt'),
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
                                root.loading = true;
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
                                nameArabic: response.data.nameArabic,
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
                    nameArabic: '',
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
            this.$emit('update:modelValue', this.$route.name);
            this.GetData();

        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.makeActive('Assets');
        },
    }
</script>