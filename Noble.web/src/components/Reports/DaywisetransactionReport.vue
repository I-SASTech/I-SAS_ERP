<template>
    <div class="row" v-if="isValid('CanViewDayWiseTransactions')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('DaywisetransactionReport.DaywiseTransactions') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('DaywisetransactionReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('DaywisetransactionReport.DaywiseTransactions') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a  v-if="isValid('CanPrintAccountLedger')"  v-on:click="PrintRdlc()"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('AccountLedger.Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
           
            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center">
                        <div class="col-lg-4">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('DaywisetransactionReport.Search')"
                                    aria-label="Example text with button addon"
                                    aria-describedby="button-addon1, button-addon2">
                                
                            </div>
                        </div>
                        <div class=" col-lg-4   form-group">
                            <label>{{ $t('DaywisetransactionReport.Date') }}</label>
                            <datepicker v-model="date" :key="render" />
                        </div>

                    </div>
                </div>
                <div class="card-body" v-for="(contact, index) in resultQuery" v-bind:key="index">
                    <div>
                        <h3>{{ contact.header }}</h3>
                        <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>

                                        <th>
                                            {{ $t('DaywisetransactionReport.Date') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.AccountType') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.Costcenter') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.Account') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.Document') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.TransactionType') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.Debit') }}
                                        </th>
                                        <th>
                                            {{ $t('DaywisetransactionReport.Credit') }}
                                        </th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(product, i) in contact.dayWiseTransactionLookup" v-bind:key="i">
                                        <td>
                                            {{ i + 1 }}
                                        </td>
                                        <td>
                                            {{ product.date.split(' ')[0] }}
                                        </td>
                                        <td>
                                            {{ product.accountType }}
                                        </td>
                                        <td>
                                            {{ product.costCentre }}
                                        </td>
                                        <td>
                                            {{ product.accountName }}
                                        </td>
                                        <td>
                                            {{ product.documentNumber }}
                                        </td>
                                        <td>
                                            {{ (getTransactionType(product.transactionType)) }}
                                        </td>
                                        <td>
                                            {{ Number(product.debit).toLocaleString() }}
                                        </td>
                                        <td>
                                            {{ Number(product.credit).toLocaleString() }}
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="text-end ">

                        <label style="font-weight:bold;font-size:16px;">{{ $t('DaywisetransactionReport.Total') }}: <span> {{ parseFloat(contact.total) >= 0 ? 'Dr '+ contact.total: 'Cr '+ parseFloat(contact.total)*(-1) }}</span></label>
                    </div>
                </div>


            </div>
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
export default {
    mixins: [clickMixin],
    props: ['formName'],
    data: function () {
        return {
            search: '',
            date: '',
            transactionList: [],
        }
    },
    created: function () {
        this.date = moment().format("DD MMM YYYY");
    },
    watch: {
        date: function (date) {
            this.date = date;
            this.search = '';
            this.GetTransactionList(this.date)
        },

    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.search) {
                return root.transactionList.filter((x) => {

                    return root.search.toLowerCase().split(' ').every(v => x.header.toLowerCase().includes(v))
                })
            } else {
                return root.transactionList;
            }
            //    || x.dayWiseTransactionLookup.filter((y) => { root.search.toLowerCase().split(' ').every(w => y.accountName.toLowerCase().includes(w)) })
        },
    },
    methods: {

        getTransactionType: function (x) {
            if (x == 0) {
                return 'Purchase Invoice'
            }
            else if (x == 1) {
                return 'Cash Receipt'
            }

            else if (x == 2) {
                return 'Bank Receipt'
            }
            else if (x == 3) {
                return 'Cash Pay'
            }
            else if (x == 4) {
                return 'Bank Pay'
            }
            else if (x == 5) {
                return 'Stock In'
            }
            else if (x == 6) {
                return 'Stock Out'
            }
            else if (x == 7) {
                return 'Journal Voucher'
            }
            else if (x == 8) {
                return 'Expense Voucher'
            }
            else if (x == 9) {
                return 'Sale Invoice'
            }
            else if (x == 10) {
                return 'Purchase Return'
            } else if (x == 11) {
                return 'Purchase Return'
            }
            else if (x == 12) {
                return 'Day End'
            }
            else if (x == 13) {
                return 'WareHouse Transfer From'
            }
            else if (x == 14) {
                return 'Production Batch'
            }
            else if (x == 15) {
                return 'Stock Production'
            }
            else if (x == 16) {
                return 'Production Remaining Stock'
            }
            else if (x == 17) {
                return 'Production Damage Stock'
            }
            else if (x == 18) {
                return 'Petty Cash'
            }
            else if (x == 19) {
                return 'WareHouse Transfer To'
            }
        },

        getDate: function (date) {
            return moment(date).format('l');
        },
        PrintRdlc: function(){
                this.isShown = false;
                
                this.$https.get('/Report/DayWiseTransection?date=' + this.date, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', 'Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();
                    });
            },
        GetTransactionList: function (date) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.isShown = false;

            this.$https.get('/Report/GetTransactionList?date=' + date, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {

                        root.transactionList = response.data;
                    }
                }).catch(error => {

                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: 'Some thing went wrong!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                });
        },
    },

    mounted: function () {

    }
}
</script>




