<template>
    <div v-if="isValid('CanViewBanTransaction')" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group ">
                                <h5 class="card-title DayHeading ">{{$t('BankTransactionReport.BankTransactionReport')}}</h5>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                <router-link :to="'/AllReports'">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                </router-link>
                                <a href="javascript:void(0)" v-if="isValid('CanPrintBanTransaction')" class="btn btn-outline-primary " v-on:click="PrintDetails(false)">{{ $t('BankTransactionReport.Print') }}</a>
                                <a href="javascript:void(0)" v-if="isValid('CanPrintBanTransaction')" class="btn btn-outline-primary " v-on:click="PrintDetails(true)"><i class="far fa-file-pdf"></i> Export PDF</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row ml-2 mr-2" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'pr-3'">
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group ">
                            <label>{{ $t('BankTransactionReport.FromDate') }}</label>
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group ">
                            <label>{{ $t('BankTransactionReport.ToDate') }}</label>
                            <datepicker v-model="toDate" :key="render" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group ">
                            <label>Bank Account</label>
                            <accountdropdown v-model="accountId" :formName="'BankReceipt'" :modelValue="accountId" @update:modelValue="accountValue(accountId)" :key="render" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-3">

                        <div class="form-group ">
                            <label>In/Out </label>
                            <multiselect v-model="bankInOut" :preselect-first="true" :options="[ 'In', 'Out','In/Out']" :show-labels="false" placeholder="Select Type">
                            </multiselect>

                        </div>

                    </div>
                </div>
                <div class="card-body">
                    <h6 class="col-md-12" v-if="bankInOut=='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                        {{ $t('BankTransactionReport.OpeningBalance') }}  :{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}
                    </h6>
                    <div>
                        <div class="table-responsive">
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            Date
                                        </th>
                                        <th>
                                            Document No
                                        </th>
                                        <th>
                                            Transaction Type
                                        </th>
                                        <th>
                                            Description
                                        </th>
                                        <th>
                                            Payment Type
                                        </th>

                                        <th v-if="bankInOut=='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            Debit
                                        </th>
                                        <th v-if="bankInOut=='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            Credit
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            Amount
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(ledger,index) in resultQuery" v-bind:key="ledger.id">
                                        <td>
                                            {{index+1}}
                                        </td>
                                        <td>
                                            {{ledger.date}}
                                        </td>
                                        <td>
                                            {{ledger.documentNo}}
                                        </td>
                                        <td>
                                            {{getTransactionType(ledger.transactionType)}}
                                        </td>
                                        <td>
                                            {{ledger.description}}
                                        </td>
                                        <td>
                                            {{ledger.bankName==null || ledger.bankName==''  ?'Bank':ledger.bankName}}
                                        </td>
                                        <td v-if="bankInOut==='In' || bankInOut==='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{bankInOut!='In/Out'?'Dr':''}}  {{ ledger.debit}}
                                        </td>
                                        <td v-if="bankInOut==='Out' || bankInOut==='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{bankInOut!='In/Out'?'Cr':''}} {{ ledger.credit}}
                                        </td>
                                        <td v-if="bankInOut==='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ledger.amount>0?'Dr':'Cr'}} {{nonNegative(ledger.amount) }}
                                        </td>
                                    </tr>

                                    <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                        
                                        <td colspan="6" class="text-center"  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            <h6>{{ $t('BankTransactionReport.ClosingBalance') }}:</h6>
                                        </td>

                                        <td v-if="bankInOut==='In' || bankInOut==='In/Out'" class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            <h6>Dr {{nonNegative(ledger.totalDebit)}}</h6>
                                        </td>
                                        <td v-if="bankInOut==='Out' || bankInOut==='In/Out'" class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            <h6>Cr {{nonNegative(ledger.totalCredit)}}</h6>
                                        </td>

                                        <td v-if="bankInOut==='In/Out'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            <h6>{{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <BankTransactionReportPrint :headerFooter="headerFooter" :ledger="ledger" :isPrint="isShown" :bankInOut="bankInOut" :fromDate="fromDate" :printDetails="resultQuery" :toDate="toDate" v-if="isShown" v-bind:key="printRender"></BankTransactionReportPrint>
        <BankTransactionPdf :headerFooter="headerFooter" :ledger="ledger" :isPrint="isDownload" :bankInOut="bankInOut" :fromDate="fromDate" :printDetails="resultQuery" :toDate="toDate" v-if="isDownload" v-bind:key="printRenderpdf"></BankTransactionPdf>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Multiselect from 'vue-multiselect'


    export default {
        mixins: [clickMixin],
        components: {
            Multiselect
        },
        data: function () {
            return {
                ledger: {
                    bankList: [],
                    openingBalance: 0,
                    runningBalance: 0,
                    totalCredit: 0,
                    totalDebit: 0,
                },
                render: 0,
                accountId: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                bankInOut: '',
                bankList: [],
                isShown: false,
                isDownload: false,
                formName: 'Account Ledger',
                printRender: 0,
                printRenderpdf: 0,
                advanceFilters: false,
                combineDate: '',
                language: 'Nothing',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            }
        },


        watch: {
            fromDate: function (fromDate) {
                this.isShown = false;
                if (this.accountId != null && this.accountId != '' && this.accountId != '00000000-0000-0000-0000-000000000000') {
                    this.fromDate = fromDate;
                    this.GetInventoryList(this.fromDate, this.toDate, this.accountId);

                }
            },
            toDate: function (toDate) {
                this.isShown = false;

                if (this.accountId != null && this.accountId != '' && this.accountId != '00000000-0000-0000-0000-000000000000') {
                    this.toDate = toDate;
                    this.GetInventoryList(this.fromDate, this.toDate, this.accountId);
                }
            }

        },
        computed: {            

            resultQuery: function () {

                var root = this;
                if (this.bankInOut == 'In') {
                    return root.ledger.bankList.filter((region) => {
                        return region.debit > 0


                    })
                }
                else if (this.bankInOut == 'Out') {
                    return root.ledger.bankList.filter((region) => {
                        return region.credit > 0


                    })
                }

                else {
                    return root.ledger.bankList;
                }
            },
        },
        methods: {
            nonNegative: function (value) {
                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },

            GetHeaderDetail: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {

                        if (response.data != null) {

                            root.headerFooter.company = response.data;
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/FreeofCostSale');

                }


            },



            accountValue: function (x) {
                var accountid = x;
                //this.GetHeaderDetail()
                this.GetInventoryList(this.fromDate, this.toDate, accountid);
            },
            getDate: function (date) {

                return moment(date).format('l');
            },
            GetInventoryList: function (fromdate, todate, accountId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.isShown = false;
                this.$https.get('/Report/GetBankTransactionQuery?fromDate=' + fromdate + '&toDate=' + todate + '&accountId=' + accountId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            
                            root.ledger.bankList = response.data.bankLook;
                            root.ledger.openingBalance = response.data.opening;
                            root.ledger.runningBalance = response.data.runningBalance;
                            root.ledger.totalDebit = response.data.totalDebit;
                            root.ledger.totalCredit = response.data.totalCredit;
                        }
                    });
            },
         
            PrintDetails: function (download) {
                
                var root = this;
                if (download) {
                    this.GetHeaderDetail();
                    root.isDownload = true;
                    root.printRenderpdf++;
                }
                else {
                    this.GetHeaderDetail();
                    root.isShown = true;
                    root.printRender++;
                }
            },


            getTransactionType(transactionType) {
                if (transactionType == 'StockOut') return this.$t('BankTransactionReport.StockOut')
                if (transactionType == 'JournalVoucher') return 'Journal Voucher'
                if (transactionType == 'ExpenseVoucher') return 'Expense Voucher'
                if (transactionType == 'Expense') return 'Expense'
                if (transactionType == 'BankPay') return 'Bank Pay'
                if (transactionType == 'BankReceipt') return 'Bank Receipt'
                if (transactionType == 'StockIn') return this.$t('BankTransactionReport.StockIn')
                if (transactionType == 'SaleInvoice') return this.$t('BankTransactionReport.SaleInvoice')
                if (transactionType == 'PurchaseReturn') return this.$t('BankTransactionReport.PurchaseReturn')
                if (transactionType == 'PurchasePost') return this.$t('BankTransactionReport.Purchase')
                if (transactionType == 'CashReceipt') return this.$t('BankTransactionReport.Cash')
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
        },

        mounted: function () {
            this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            this.headerFooter.footerAr = localStorage.getItem('TermsInAr');

            this.language = this.$i18n.locale;
            this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.render++;
        }
    }
</script>
<style scoped>

    table {
        width: 100%;
    }

    thead, tbody tr {
        display: table;
        width: 100%;
        table-layout: fixed;
    }

    tbody {
        display: block;
        overflow-y: auto;
        table-layout: fixed;
        max-height: 600px;
    }

    ::-webkit-scrollbar {
        width: 11px !important;
        height: 10px !important;
    }
</style>