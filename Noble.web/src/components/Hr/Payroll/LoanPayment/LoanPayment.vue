<template>
    <div class="row" v-if="isValid('CanViewLoanPayment')">

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('LoanPayment.LoanPayment') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('LoanPayment.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('LoanPayment.LoanPayment') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-if="isValid('CanAddLoanPayment')" v-on:click="AddLoanPayment" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-primary mx-1">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            {{ $t('LoanPayment.AddNew') }}
                        </a>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-danger">
                            {{ $t('LoanPayment.Close') }}
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="input-group">
                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                        class="fas fa-search"></i></button>
                <input v-model="search" type="text" class="form-control" :placeholder="$t('LoanPayment.Search')"
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
                                            {{ $t('LoanPayment.Employee') }}
                                        </th>
                                        <th>
                                            {{ $t('LoanPayment.LoanDate') }}
                                        </th>

                                        <th>
                                            {{ $t('LoanPayment.LoanType') }}
                                        </th>
                                        <th>
                                            {{ $t('LoanPayment.Loans') }}
                                        </th>
                                        <th>
                                            {{ $t('LoanPayment.RecoveryLoans') }}

                                        </th>
                                        <th>
                                            {{ $t('LoanPayment.Payment') }}

                                        </th>
                                        <th>
                                            {{ $t('LoanPayment.RecoveryBalance') }}
                                        </th>
                                        <th>
                                            {{ $t('LoanPayment.Status') }}
                                        </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(loanPayment ,index) in loanPaymentList" v-bind:key="loanPayment.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>
                                        <td>
                                            <strong>
                                                <!--<a href="javascript:void(0)" v-on:click="EditLoanPayment(loanPayment.id)">{{loanPayment.employeeName}}</a>-->
                                                {{loanPayment.employeeName}}
                                            </strong>
                                        </td>
                                        <th>
                                            {{loanPayment.loanDate}}

                                        </th>
                                        <td>
                                            {{GetLoanType(loanPayment.loanType)}}
                                        </td>
                                        <td>
                                            {{currency}}  {{parseFloat(loanPayment.loanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td v-if="isValid('CanEditLoanPayment')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="LoanDetail(loanPayment)"> {{currency}}  {{parseFloat(loanPayment.recoveryLoanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</a>
                                            </strong>
                                          
                                        </td>
                                        <td v-else>
                                            <strong>
                                                {{currency}}  {{parseFloat(loanPayment.recoveryLoanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </strong>
                                          
                                        </td>
                                        <td>
                                            {{currency}}  {{parseFloat(loanPayment.payment).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}}  {{parseFloat(loanPayment.remainingLoan).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        
                                        <td>
                                        <span v-if="loanPayment.isActive" class="badge badge-boxed badge-outline-danger ">{{$t('LoanPayment.Close')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-success">{{$t('LoanPayment.Open')}}</span>
                                    </td>
                                    </tr>
                    </tbody>
                </table>
            </div>
            <hr />
            <div class="row">
                <div class="col-lg-6">
                    <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                    <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to')
                    }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
}}</span>
                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of')
                        }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                </div>
                <div class=" col-lg-6">
                    <div class=" float-end" v-on:click="GetBrandData()">
                        <b-pagination pills size="sm" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              :first-text="$t('Table.First')"
                                              :prev-text="$t('Table.Previous')"
                                              :next-text="$t('Table.Next')"
                                              :last-text="$t('Table.Last')" ></b-pagination>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <loanDetailModel :loanDetail="loanDetail"
                        :show="show"
                        v-if="show"
                        @close="RefreshRecord"
                         />
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
                currency: '',
                loanPaymentList: [],
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                arabic: '',
                english: '',
                show: false,
                loanDetail:[]
            }
        },
        watch: {
            search: function () {
                this.GetLoanPaymentData();
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({path: link});
            },
            AddLoanPayment: function () {
                this.$router.push('/AddLoanPayment');

            },
            GetLoanType: function (x) {
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    // Loan Type
                    if (x == 1) {
                        return 'Loan'
                    }
                    else if (x == 2) {
                        return  'Advance'
                    }
                    else if (x== 3) {
                        return  'Provident Fund'
                    }


                  

                    // Recovery Method

                   
                }
                else {
                    // Loan Type
                    if (x== 1) {
                        return  'يقرض'
                    }
                    else if (x== 2) {
                        return  'تقدم'
                    }
                    else if (x== 3) {
                        return  'صندوق التوفير او الادخار'
                    }


                    // Recovery Method

                  
                }
            },
            LoanDetail: function (loanPaymentList) {
                
                this.loanDetail = loanPaymentList
                
                this.show = !this.show;
            },
            RefreshRecord: function () {
                this.show = false;
                this.GetLoanPaymentData();
            },
            getPage: function () {
                this.GetLoanPaymentData();
            },


            GetLoanPaymentData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('Payroll/LoanPaymentList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.loanPaymentList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditLoanPayment: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/LoanPaymentDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            
                            root.$router.push({
                                path: '/AddLoanPayment',
                                query: { data: response.data }
                            })



                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            }
        },

        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            this.GetLoanPaymentData();

        }
    }
</script>