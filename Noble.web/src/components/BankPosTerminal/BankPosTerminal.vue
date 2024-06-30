<template>
    <div class="row" v-if="isValid('CanViewPosTerminal')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('BankPosTerminal.BankPosTerminal') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('BankPosTerminal.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('BankPosTerminal.BankPosTerminal') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" v-if="isValid('CanAddPosTerminal')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('BankPosTerminal.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('BankPosTerminal.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('BankPosTerminal.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>
                                        {{ $t('BankPosTerminal.TerminalId') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('BankPosTerminal.BankName') }}
                                    </th>
                                    <th>
                                        {{ $t('BankPosTerminal.Status') }}
                                    </th>


                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(bankPosTerminal,index) in resultQuery" v-bind:key="bankPosTerminal.id">
                                    <td>
                                        {{index+1}}
                                    </td>

                                    <td v-if="isValid('CanEditPosTerminal')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditBankPosTerminal(bankPosTerminal.id)">  {{bankPosTerminal.terminalId}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        <strong>
                                            {{bankPosTerminal.terminalId}}
                                        </strong>
                                    </td>

                                    <td class="text-center">
                                        {{bankPosTerminal.bankName}}
                                    </td>
                                    <td class="text-center">
                                        <span v-if="bankPosTerminal.isActive" class="badge badge-boxed  badge-outline-success">{{$t('BankPosTerminal.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('BankPosTerminal.De-Active')}}</span>
                                    </td>



                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <bankPosTerminalmodal :bankPosTerminal="newBankPosTerminal"
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
                bankPosTerminallist: [],
                newBankPosTerminal: {
                    id: '',
                    terminalId: '',
                    bankId: '',
                    isActive: true
                },
                type: '',
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.bankPosTerminallist.filter((bankPosTerminal) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => bankPosTerminal.terminalId.toLowerCase().includes(v) || bankPosTerminal.bankName.toLowerCase().includes(v))
                    })
                } else {
                    return root.bankPosTerminallist;
                }
            },
        },
        methods: {
            openmodel: function () {
                this.newBankPosTerminal = {
                    id: '00000000-0000-0000-0000-000000000000',
                    terminalId: '',
                    bankId: '',
                    isActive: true

                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetBankPosTerminalData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Company/BankPosTerminalList?isActive=false', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.bankPosTerminallist = response.data.bankPosTerminals;
                    }
                });
            },
            EditBankPosTerminal: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/BankPosTerminalDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newBankPosTerminal.id = response.data.id;
                            root.newBankPosTerminal.terminalId = response.data.terminalId;
                            root.newBankPosTerminal.bankId = response.data.bankId;
                            root.newBankPosTerminal.isActive = response.data.isActive;
                            root.show = !root.show;
                            root.type = "Edit"
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            }
        },
        created: function () {
            this.$emit('input', this.$route.name);
        },
        mounted: function () {
            this.GetBankPosTerminalData();
        }
    }
</script>