<template>
    <div class="row" v-if="isValid('CanViewTerminal')|| isValid('Noble Admin')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Terminal.Terminal') }} <span v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')"> / {{ $t('Terminal.CashCounters') }}</span></h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Terminal.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Terminal.Terminal') }} <span v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')"> / {{ $t('Terminal.CashCounters') }}</span></li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddTerminal')" v-on:click="openmodel"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Terminal.AddTerminal') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Terminal.Close') }}
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
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('Terminal.SearchbyName')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>

                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>
                                        {{ $t('Terminal.Code') }} <span v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')"> / {{ $t('Terminal.CashCounters') }}</span>
                                    </th>
                                    <th v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                        {{ $t('Terminal.Printer') }}
                                    </th>
                                    <th>
                                        {{ $t('Terminal.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(terminal,index) in resultQuery" v-bind:key="index">
                                    <td>
                                        {{index+1}}
                                    </td>

                                    <td v-if="isValid('CanEditTerminal') || isValid('Noble Admin')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditTerminal(terminal.id)">{{terminal.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{terminal.code}}
                                    </td>
                                    <td v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                        {{terminal.printerName}}
                                    </td>
                                    <td>
                                        <span v-if="terminal.isActive"
                                              class="badge badge-boxed  badge-outline-success">
                                            {{
                                                $t('Terminal.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{
                                                $t('Terminal.De-Active')
                                            }}
                                        </span>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                   
                   
                </div>
            </div>

            <terminalmodel :newterminal="newTerminal"
                           :show="show"
                           v-if="show"
                           @close="show = false"
                           :type="type" />
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
        data: function () {
            return {
                searchQuery: '',
                show: false,
                terminallist: [],
                newTerminal: {
                    id: '',
                    code: '',
                    printerName: '',
                    macAddress: '',
                    accountId: '',
                    ipAddress: '',
                    isActive: false,
                    cashAccountId: '',
                    posTerminalId: '',
                    terminalType: '',
                    onPageLoadItem: false,
                    companyNameEnglish: '',
                    businessNameEnglish: '',
                    businessTypeEnglish: '',
                    companyNameArabic: '',
                    businessNameArabic: '',
                    businessTypeArabic: '',
                    businessLogo: '',
                    terminalUserType: '',
                    overWrite:false
                },
                type: '',
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.terminallist.filter((terminal) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => terminal.code.toLowerCase().includes(v))
                    })
                } else {
                    return root.terminallist;
                }
            },
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            openmodel: function () {
                this.newTerminal = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    printerName: '',
                    macAddress: '',
                    ipAddress: '',
                    accountId: '',
                    posTerminalId: '',
                    isActive: false,
                    onPageLoadItem: false,
                    companyId: this.companyId,
                    companyNameEnglish: '',
                    businessNameEnglish: '',
                    businessTypeEnglish: '',
                    companyNameArabic: '',
                    businessNameArabic: '',
                    businessTypeArabic: '',
                    terminalUserType: '',
                    businessLogo: '',
                    overWrite: false,
                }
                //if (this.overWrite == 'true') {

                //}
                
                this.show = !this.show;
                this.type = "Add";
            },
            GetTerminalData: function (companyId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Company/TerminalList?companyId=' + companyId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetTerminalList(response.data.terminals);
                        root.terminallist = response.data.terminals;
                    }
                });
            },
            EditTerminal: function (terminal) {


                if (terminal.macAddress == null) {
                    var root = this;

                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Company/TerminalDetail?Id=' + terminal + '&companyId=' + this.companyId, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data) {
                                
                                root.newTerminal.id = response.data.id;

                                root.newTerminal.ipAddress = response.data.ipAddress;
                                root.newTerminal.macAddress = response.data.macAddress;
                                root.newTerminal.code = response.data.code;
                                root.newTerminal.accountId = response.data.accountId;
                                root.newTerminal.isActive = response.data.isActive;
                                root.newTerminal.cashAccountId = response.data.cashAccountId;
                                root.newTerminal.posTerminalId = response.data.posTerminalId;
                                root.newTerminal.printerName = response.data.printerName;
                                root.newTerminal.onPageLoadItem = response.data.onPageLoadItem;
                                root.newTerminal.companyNameEnglish = response.data.companyNameEnglish;
                                root.newTerminal.businessNameEnglish = response.data.businessNameEnglish;
                                root.newTerminal.businessTypeEnglish = response.data.businessTypeEnglish;
                                root.newTerminal.companyNameArabic = response.data.companyNameArabic;
                                root.newTerminal.businessNameArabic = response.data.businessNameArabic;
                                root.newTerminal.businessTypeArabic = response.data.businessTypeArabic;
                                root.newTerminal.businessLogo = response.data.businessLogo == null ? '' : response.data.businessLogo;
                                root.newTerminal.categoryIdList = response.data.categoryIdList;
                                root.newTerminal.terminalUserType = response.data.terminalUserType;
                                root.newTerminal.companyId = root.companyId;
                                root.newTerminal.overWrite = response.data.overWrite;
                                if (response.data.terminalType == 1)
                                    root.newTerminal.terminalType = 'Terminal';
                                else if (response.data.terminalType == 2)
                                    root.newTerminal.terminalType = 'CashCounter';
                                else
                                    root.newTerminal.terminalType = '';
                                root.show = !root.show;
                                root.type = "Edit"
                            } else {
                                console.log("error: something wrong from db.");
                            }
                        },
                            function (error) {
                                this.loading = false;
                                console.log(error);
                            });

                }

            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            if (this.$route.query.id != undefined) {
                this.companyId = this.$route.query.id;
                this.overWrite = this.$route.query.option;
                this.GetTerminalData(this.$route.query.id);
            } else {
                this.companyId = '';
                this.GetTerminalData('');
            }
        }
    }
</script>