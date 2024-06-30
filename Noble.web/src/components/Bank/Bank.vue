<template>
    <div class="row" v-if="isValid('CanViewBank')">


        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Bank.ListOfBank') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Bank.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Bank.ListOfBank') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddBank')" v-on:click="AddBank" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Bank.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Bank.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('Search')"
                            aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>
                                        #
                                    </th>
                                    <th>
                                        {{ $t('Bank.CODE') }}
                                    </th>
                                    <th v-if="english == 'true'">
                                        {{$englishLanguage($t('Bank.BANKNAME'))   }}
                                    </th>
                                    <th v-if="isOtherLang()">
                                        {{$arabicLanguage($t('Bank.BANKNAME'))   }}
                                    </th>
                                    <th>
                                        {{ $t('Bank.IBNNUMBER') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Bank.Currency') }}
                                    </th>
                                    <th>
                                        {{ $t('Bank.Type') }}
                                    </th>
                                    <th>
                                        {{ $t('Bank.Reference') }}
                                    </th>


                                    <th>
                                        {{ $t('Bank.Status') }}
                                    </th>
                                    <th class="text-center" v-if="isValid('CanAddCheque')">
                                        Action
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details, index) in resultQuery" v-bind:key="details.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <td v-if="isValid('CanEditBank')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditBankInfo(details.id)">
                                                {{
                                                        details.code
                                                }}
                                            </a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{ details.code }}
                                    </td>
                                    <td v-if="english == 'true'">
                                        {{ details.bankName }}
                                    </td>
                                    <td v-if="isOtherLang()">
                                        {{ details.nameArabic }}
                                    </td>
                                    <td>
                                        {{ details.ibnNumber }}
                                    </td>
                                    <td class="text-center">{{ details.currencyName }}</td>
                                    <td class="text-center">{{ details.accounType }}</td>
                                    <td>
                                        {{ details.reference }}
                                    </td>

                                    <td>
                                        <span v-if="details.active" class="badge badge-boxed  badge-outline-success">
                                            {{
                                                    $t('Bank.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{
                                                    $t('Bank.De-Active')
                                            }}
                                        </span>
                                    </td>


                                    <td class="text-center " v-if="isValid('CanAddCheque')">
                                        <a href="javascript:void(0)" class="btn btn-soft-primary btn-sm"
                                            v-on:click="openmodel(details.id)">
                                            {{
                                                    $t('ChequeBook.Cheque')
                                            }}
                                        </a>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <chequelistmodel :show="show" :bankId="bankId" v-if="show" @close="show = false" />
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

export default {
    mixins: [clickMixin],
    name: 'bank-component',
    data: function () {
        return {
            show: false,
            bankId: '',
            arabic: '',
            english: '',
            searchQuery: '',

            type: '',
            banklist: [

            ]
        }
    },
    computed: {
        resultQuery: function () {

            var root = this;

            if (this.searchQuery) {

                return this.banklist.filter((cur) => {
                    return root.searchQuery.toLowerCase().split(' ').every(v => cur.bankName.toLowerCase().includes(v) || cur.nameArabic.toLowerCase().includes(v) || cur.code.toLowerCase().includes(v))
                })
            } else {
                return root.banklist;
            }
        },
    },

    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function (id) {

            this.show = !this.show;
            this.bankId = id;
        },
        GetBankData: function () {
            var root = this;
            var url = '/Accounting/BankList';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.banklist = response.data.banks;
                }
            });
        },
        AddBank: function () {
            //this.$router.push('/AddBank')
            this.$router.push({
                            path: '/AddBank',
                            query: {
                                Add: true,
                            }
                        })
        },
        EditBankInfo: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Accounting/BankDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.$store.GetEditObj(response.data);
                    root.$router.push({
                        path: '/AddBank',
                        query: {
                            Add: false,
                            data: ''
                        }
                    })
                }
            });
        },
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetBankData();
    }
}
</script>