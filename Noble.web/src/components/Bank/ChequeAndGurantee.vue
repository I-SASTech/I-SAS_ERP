<template>
    <div class="row " v-if="isValid('CanViewCheque')">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div>
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('ChequeBook.Cheques&Guarrntees') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PurchaseBill.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('ChequeBook.Cheques&Guarrntees') }}</li>
                        </ol>
                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />
                <div>
                    <div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('ChequeBook.SelectBank') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <bankdropdown v-model="bankId" @update:modelValue="GetBankAccount(bankId)"></bankdropdown>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" style="margin-top:18px;">
                               
                            </div>

                        </div>

                        <div class="mt-2">
                            <div v-bind:key="randerList">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr class="text-capitalize text-center">
                                            <th>#</th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.BookNo') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.SNo') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.ChequeNo') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.ChequeDate') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.IssuedTo') }}

                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Amount') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.ShortDetail') }}

                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Type') }}

                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Validity') }}

                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Cash') }}

                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Alert') }}

                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Status') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Date') }}


                                            </th>



                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(region,index) in chequeBookItems" v-bind:key="region.id">
                                            <td>
                                                {{index+1}}
                                            </td>
                                            <td class="text-center ">
                                                {{region.bookNo}}

                                            </td>
                                            <td class="text-center ">
                                                <span v-if="region.isBlock && isValid('CanEditCheque')" style="color:red">{{region.serialNo}} (Blocked)</span>
                                                <strong v-else-if="!region.isPaid && isValid('CanEditCheque')">
                                                    <a href="javascript:void(0)" v-on:click="EditRegion(region.id)">{{region.serialNo}} </a>
                                                </strong>
                                                <span v-else>
                                                    {{region.serialNo}}
                                                </span>
                                            </td>

                                            <td class="text-center ">

                                                <span v-if="region.isBlock && isValid('CanEditCheque')" style="color:red">{{region.chequeNo}} </span>
                                                <strong v-else-if="!region.isPaid  && isValid('CanEditCheque')">
                                                    <a href="javascript:void(0)" v-on:click="EditRegion(region.id)">{{region.chequeNo}} </a>
                                                </strong>
                                                <span v-else>
                                                    {{region.chequeNo}}
                                                </span>

                                            </td>



                                            <td class="text-center ">
                                                {{getDate(region.chequeDate)}}
                                            </td>
                                            <td class="text-center">
                                                {{region.issuedToName}}
                                            </td>
                                            <td class="text-center">
                                                {{region.amount}}

                                            </td>
                                            <td class="text-center">
                                                {{region.shortDetail}}

                                            </td>

                                            <td class="text-center">
                                                {{region.chequeTypes }}

                                            </td>
                                            <td class="text-center">
                                                {{getDate(region.validityDate) }}

                                            </td>
                                            <td class="text-center">
                                                {{CashType(region.cashTypes) }}

                                            </td>
                                            <td class="text-center">
                                                {{getDate(region.alertDate) }}

                                            </td>
                                            <td class="text-center">
                                                {{region.statusTypes }}

                                            </td>
                                            <td class="text-center">
                                                {{getDate(region.statusDate) }}

                                            </td>





                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
                <chequeandguranteemodel :show="show"
                                        :chequeandgurantee="chequeAndGurantee"
                                        v-if="show"
                                        @close="RefreshList" />
            </div>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";


    export default {
        mixins: [clickMixin],
        name: 'check-and-guurantee',
        data: function () {
            return {
                show: false,
                randerList: 0,
                bankId: '',
                arabic: '',
                english: '',
                searchQuery: '',

                type: '',
                chequeBookItems: [

                ],
                chequeAndGurantee: [

                ],

            }
        },
        computed: {
            resultQuery: function () {

                var root = this;

                if (this.searchQuery) {

                    return this.banklist.filter((cur) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => cur.name.toLowerCase().includes(v) || cur.nameArabic.toLowerCase().includes(v) || cur.code.toLowerCase().includes(v))
                    })
                } else {
                    return root.banklist;
                }
            },
        },

        methods: {

            getDate: function (date) {
                if (date == null || date == '')
                    return '';
                else
                    return moment(date).format('l');
            },
            CashType: function (CashType) {
                if (CashType == 'NotReserved')
                    return 'Not Reserved'
                else
                    return CashType;
            },
            RefreshList: function (x, Id) {
                
                if (x == true) {
                    var root = this;

                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Payroll/ChequeAndGuranteeDetail?bankId=' + Id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            
                            root.chequeBookItems = response.data;
                            root.randerList++;
                            root.show = false;

                        }
                    });
                }
                else {
                    this.show = false;

                }
            },

            EditRegion: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Payroll/GetChequeGuranteeDetailQuery?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.chequeAndGurantee = response.data;
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

            },

            AddBank: function () {
                // this.$router.push('/AddBank')
                 this.$router.push({
                            path: '/AddBank',
                            query: {
                                Add: true,
                            }
                        })
            },
            GetBankAccount: function (bankId) {
                

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/ChequeAndGuranteeDetail?bankId=' + bankId.id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.chequeBookItems = response.data;
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
            //    this.GetBankData();
        }
    }
</script>