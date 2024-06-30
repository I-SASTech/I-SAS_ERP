<template>
    <div class="row " v-if="isValid('CanViewCheque')">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div>
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('ChequeBook.ChequeDashboard') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PurchaseBill.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('ChequeBook.ChequeDashboard') }}</li>
                        </ol>
                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />

                <div>
                    <div>
                        <div class="row mb-3">

                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('ChequeBook.SelectBank') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <BankMultiDropdown v-model="bankId"></BankMultiDropdown>
                                    </div>
                                </div>
                            </div>

                        </div>



                    </div>

                </div>

                <chequeandguranteemodel :show="show"
                                        :chequeandgurantee="chequeAndGurantee"
                                        v-if="show"
                                        @close="RefreshList" />
            </div>
            <div class=" row ">
                <div class=" col-xs-12 col-sm-8 col-md-12 col-lg-12"   >
                    <div class="card" v-for="chequeBook in chequeBook" v-bind:key="chequeBook.chequeType">
                        <h6 class="text-center"> {{chequeBook.chequeType}}</h6>
                        <div class="row">
                            <div class="col-6">
                                <p class="text-center LabelSize">{{ $t('ChequeBook.Cashed') }}</p>
                                <div class="row">

                                    <div class="col-6 text-center">
                                        <p class="" style="font-size:13px;font-weight:600">{{ $t('ChequeBook.PreviousMonth') }}</p>
                                        <p class="FontSize">{{ $t('ChequeBook.NoofCheques') }}  : {{chequeBook.totalPCashedCheque}}</p>
                                        <p class="FontSize">{{ $t('ChequeBook.Amount') }}  : <span class="PaddingToAmount">{{chequeBook.totalPCashedAmount}}</span></p>
                                    </div>
                                    <div class="col-6 text-center">
                                        <p class="" style="font-size:13px;font-weight:600">{{ $t('ChequeBook.CurrentMonth') }}  </p>

                                        <p class="FontSize"> {{ $t('ChequeBook.NoofCheques') }} : {{chequeBook.totalCashedCheque}}</p>
                                        <p class="FontSize"> {{ $t('ChequeBook.Amount') }} : <span class="PaddingToAmount">{{chequeBook.totalCashedAmount}}</span></p>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="col-6">
                                <p class="text-center LabelSize">{{ $t('ChequeBook.Reserved') }}</p>
                                <div class="row">
                                    <div class="col-lg-6 text-center">
                                        <p class="" style="font-size:13px;font-weight:600"> {{ $t('ChequeBook.CurrentMonth') }}</p>

                                        <p class="FontSize">{{ $t('ChequeBook.ReservedAmount') }}  : {{chequeBook.reservedCAdvacne}}</p>
                                        <p class="FontSize"> {{ $t('ChequeBook.NonReservedAmount') }} : {{chequeBook.notReservedCAdvacne}}</p>

                                    </div>
                                    <div class="col-lg-6 text-center">
                                        <p class="" style="font-size:13px;font-weight:600"> {{ $t('ChequeBook.NextMonth') }} </p>
                                        <p class="FontSize"> {{ $t('ChequeBook.ReservedAmount') }}: {{chequeBook.reservedNAdvacne}}</p>
                                        <p class="FontSize"> {{ $t('ChequeBook.NonReservedAmount') }} : {{chequeBook.notReservedNAdvacne}}</p>
                                    </div>
                                </div>
                                
                            </div>
                           

                        </div>
                    </div>


                </div>
             
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
        name: 'check-and-guurantee-dashboard',
        data: function () {
            return {
                show: false,
                randerList: 0,
                bankId: '',
                id: '',
                fromDate: '',
                arabic: '',
                english: '',

                type: '',
                chequeBook: [],
                chequeLookUp: [],
                   
                ids: [],
              

            }
        },
        watch: {
            bankId: function () {
                
                var root = this;
                root.ids = [];
                this.bankId.forEach(function (cat) {
                    root.ids.push({ id: cat.id });

                })
                this.GetBankAccount();

            },
        },
        methods: {

            getDate: function (date) {
                if (date == null || date == '')
                    return '';
                else
                    return moment(date).format('l');
            },


            GetBankAccount: function () {
                

                
                var root = this;
              
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

              
                console.log(root.ids);
                root.$https.post('/Payroll/ChequeBookDashboardList', this.ids, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.chequeBook = response.data;
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
        }
    }
</script>
<style scoped>

    .PaddingToAmount{
        padding-left:20px
    }
    .FontSize {
        font-size: 15px;
        font-weight: 400;
    }
    .LabelSize {
        font-size: 17px;
        font-weight: 600
    }
</style>