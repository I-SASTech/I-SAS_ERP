<template>
    <modal :show="show" :modalLarge="true">
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="row">
                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <div>
                                        <span class="Heading1">{{ $t('ChequeBook.Cheques&Guarrntees') }}</span>
                                    </div>

                                </div>



                            </div>

                            <div class="row ">
                                <div class="form-group has-label col-sm-6 " v-bind:class="{'has-danger' : v$.chequeAndGurantee.serialNo.$error}">
                                    <label class="text  font-weight-bolder">{{ $t('ChequeBook.SerialNo') }} :<span class="text-danger"> *</span> </label>
                                    <input class="form-control" disabled v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.chequeAndGurantee.serialNo.$model" type="text" />
                                </div>

                                <div class="form-group has-label col-sm-6 " v-bind:class="{'has-danger' : v$.chequeAndGurantee.chequeNo.$error}">
                                    <label class="text  font-weight-bolder"> {{ $t('ChequeBook.ChequeNo') }}:<span class="text-danger"> *</span> </label>
                                    <input class="form-control" disabled v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.chequeAndGurantee.chequeNo.$model" type="number" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.ChequeDate') }} :<span class="text-danger" v-if="ChequeDateRequired"> *</span></label>
                                        <datepicker :isDisable="chequeAndGurantee.isCashed" @update:modelValue="GetValidiyDate(chequeAndGurantee.chequeDate)" v-model="chequeAndGurantee.chequeDate" />
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.IssuedTo') }} :</label>
                                        <issuedtodropdown @update:modelValue="GetName(IssuedTo)" v-bind:disabled="chequeAndGurantee.isCashed" v-model="IssuedTo" />

                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.IssueName') }}:<span class="text-danger"> *</span></label>
                                        <input v-bind:disabled="chequeAndGurantee.isCashed" class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="chequeAndGurantee.issuedToName" type="text" />


                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.Amount') }}:<span class="text-danger"> *</span></label>
                                        <input v-bind:disabled="chequeAndGurantee.isCashed" class="form-control" @click="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="chequeAndGurantee.amount" type="number" />

                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.ShortDetail') }}:</label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="chequeAndGurantee.shortDetail" type="text" />

                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6">
                                    <label>{{ $t('ChequeBook.Type') }}:<span class="text-danger"> *</span></label>


                                    <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="chequeAndGurantee.chequeTypes" v-bind:disabled="chequeAndGurantee.isCashed" @update:modelValue="GetChequeType(chequeAndGurantee.chequeTypes)" :options="['Normal', 'Advance', 'Security','Guarantee']" :show-labels="false" placeholder="Select Type">
                                    </multiselect>
                                    <multiselect v-else v-model="chequeAndGurantee.chequeTypes" v-bind:disabled="chequeAndGurantee.isCashed" :options="['يضمن','طبيعي', 'يتقدم', 'حماية']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    </multiselect>

                                </div>
                                <div class="form-group has-label col-sm-6 " >
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.CashDate') }}:<span class="text-danger"> *</span></label>
                                        <datepicker v-model="chequeAndGurantee.chequeTypeDate" />
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.Validity') }}: <span class="text-danger" v-if="ValidityDateAuto"> *</span></label>
                                        <datepicker v-if="ValidityDateAuto" :isDisable="ValidityDateAuto" v-model="chequeAndGurantee.validityDate" v-bind:key="DateRander2+randerValid" />
                                        <datepicker v-else :isDisable="chequeAndGurantee.isCashed" v-model="chequeAndGurantee.validityDate" v-bind:key="DateRander2" />
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6" v-if="chequeAndGurantee.chequeTypes=='Advance' || chequeAndGurantee.chequeTypes=='Guarantee' || chequeAndGurantee.chequeTypes=='Security' || chequeAndGurantee.chequeTypes=='حماية' || chequeAndGurantee.chequeTypes=='يتقدم' || chequeAndGurantee.chequeTypes=='يضمن'">
                                    <label>{{ $t('ChequeBook.Cash') }}:</label>


                                    <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="chequeAndGurantee.cashTypes" :options="['Reserved', 'Not Reserved']" :show-labels="false" placeholder="Select Type">
                                    </multiselect>
                                    <multiselect v-else v-model="chequeAndGurantee.cashTypes" :options="['محجوز', 'غير محجوز']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    </multiselect>

                                </div>

                                <div class="form-group has-label col-sm-6 " v-if="chequeAndGurantee.cashTypes=='Not Reserved' || chequeAndGurantee.cashTypes=='غير محجوز' ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.Alert') }}:<span class="text-danger"> *</span></label>
                                        <datepicker v-model="chequeAndGurantee.alertDate" />
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6">
                                    <label>{{ $t('ChequeBook.Status') }}:</label>


                                    <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="chequeAndGurantee.statusTypes" @update:modelValue="GetStatusTypes(chequeAndGurantee.statusTypes)" :options="['Cashed', 'Blocked','Cancelled','Returned']" :show-labels="false" placeholder="Select Type">
                                    </multiselect>
                                    <multiselect v-else v-model="chequeAndGurantee.statusTypes" :options="['صرف', 'ممنوع','ألغيت','عاد']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    </multiselect>

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="chequeAndGurantee.statusTypes=='Cashed' || chequeAndGurantee.statusTypes=='صرف' ">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.Date') }}:<span class="text-danger"> *</span></label>
                                        <datepicker v-model="chequeAndGurantee.statusDate" />
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="chequeAndGurantee.statusTypes=='Blocked' || chequeAndGurantee.statusTypes=='Cancelled'|| chequeAndGurantee.statusTypes=='Returned' || chequeAndGurantee.statusTypes=='ممنوع' || chequeAndGurantee.statusTypes=='ألغيت'|| chequeAndGurantee.statusTypes=='عاد'">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.Date') }}:<span class="text-danger"> *</span></label>
                                        <datepicker v-model="chequeAndGurantee.statusDate" />
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="chequeAndGurantee.statusTypes=='Blocked' || chequeAndGurantee.statusTypes=='Cancelled'|| chequeAndGurantee.statusTypes=='Returned' || chequeAndGurantee.statusTypes=='ممنوع' || chequeAndGurantee.statusTypes=='ألغيت'|| chequeAndGurantee.statusTypes=='عاد'">
                                    <div class="form-group">
                                        <label>{{ $t('ChequeBook.Remarks') }}:<span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="chequeAndGurantee.remarks" type="text" />

                                    </div>
                                </div>






                            </div>
                        </div>

                        <div v-if="!loading" class="modal-footer">

                            <div class="modal-footer justify-content-right">
                                <button class="btn btn-primary mr-2 float-left" v-on:click="Attachment()">
                                    {{ $t('AddSaleOrder.Attachment') }}
                                </button>
                                <button type="button" class="btn btn-primary  " v-on:click="SaveLoanRecovery" v-bind:disabled="v$.chequeAndGurantee.$invalid">{{ $t('ChequeBook.Save') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close(false)">{{ $t('AddColors.btnClear') }}</button>
                            </div>
                           

                           

                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
                <bulk-attachment :attachmentList="chequeAndGurantee.attachmentList" :show="isShow" v-if="isShow" @close="attachmentSave" />

            </div>
        </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    // import useVuelidate from '@vuelidate/core'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";


    export default {
        mixins: [clickMixin],
        props: ['show', 'chequeandgurantee', 'loanDetail'],
        components: {
            Loading,
            Multiselect,

        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                DateRander2:0,
                IssuedTo: '',
                isShow: false,
                ChequeDateRequired: false,
                ValidityDateAuto: false,
                currency: '',
                arabic: '',
                english: '',
                render: 0,
                dateRender: 0,
                randerValid: 0,
                loading: false,
                chequeAndGurantee: {},
            }
        },
        validations() {
            return {
                chequeAndGurantee: {
                    serialNo: {
                    },
                    chequeDate: {

                    },
                    issuedToName: {
                        required
                    },
                    chequeTypes: {
                        required
                    },
                    validityDate: {

                    },
                    chequeTypeDate: {
                        required
                    },



                    chequeNo: {

                    },
                    amount: {

                    },




                }
            }
        },
        methods: {
            
            GetValidiyDate: function (x) {

                
                var date;
                date = moment(x).add(180, 'days');
                this.chequeAndGurantee.validityDate = date;
                this.randerValid++;
            },
            Attachment: function () {
                this.isShow = true;
            },
            attachmentSave: function (attachment) {
                this.chequeAndGurantee.attachmentList = attachment;
                this.isShow = false;
            },
          
            close: function (x, y) {
                
                if (x == true)
                    this.$emit('close', x, y);
                else {
                    this.$emit('close', false);
                }

            },
            GetStatusTypes: function (x) {
                
                if (x == 'Cashed' || x == 'صرف') {
                    this.chequeAndGurantee.remarks = '';

                }

            },
            GetChequeType: function (x) {
                
                if (x == 'Normal' || x == 'Security' || x == 'طبيعي' || x == 'حماية') {
                    this.chequeAndGurantee.cashTypes = '';
                    this.chequeAndGurantee.alertDate = '';

                }

            },
            GetName: function (x) {
                
                this.chequeAndGurantee.issuedTo = x.id;
                this.chequeAndGurantee.issuedToName = x.name;
                this.chequeAndGurantee.issuerAccount = x.accountId;

            },
            SaveLoanRecovery: function () {
               
                var root = this;
                
                var dateCheck = moment(this.chequeAndGurantee.chequeDate).isAfter(this.chequeAndGurantee.validityDate)

               
                if (this.chequeAndGurantee.validityDate == '' || this.chequeAndGurantee.validityDate == null) {
                    if (this.ChequeDateRequired) {
                        root.$swal({
                            title: this.$t('ChequeBook.Error'),
                            text: this.$t('ChequeBook.ChequeDateRequired'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                        });
                        return;
                    }
                    
                }
                if (this.chequeAndGurantee.cashTypes == 'Not Reserved' || this.chequeAndGurantee.cashTypes == 'غير محجوز') {

                    if (this.chequeAndGurantee.alertDate == '' || this.chequeAndGurantee.alertDate == null) {
                        root.$swal({
                            title: this.$t('ChequeBook.Error'),
                            text: this.$t('ChequeBook.AlertDate'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                        });
                        return;
                    }
                }
                if (this.chequeAndGurantee.statusTypes=='Cashed' || this.chequeAndGurantee.statusTypes=='صرف' ) {

                    if (this.chequeAndGurantee.statusDate == '' || this.chequeAndGurantee.statusDate == null) {
                        root.$swal({
                            title: this.$t('ChequeBook.Error'),
                            text: this.$t('ChequeBook.DateRequired'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                        });
                        return;
                    }
                }
                if (this.chequeAndGurantee.statusTypes == 'Blocked' || this.chequeAndGurantee.statusTypes == 'Cancelled' || this.chequeAndGurantee.statusTypes == 'Returned' || this.chequeAndGurantee.statusTypes == 'ممنوع' || this.chequeAndGurantee.statusTypes == 'ألغيت' || this.chequeAndGurantee.statusTypes=='عاد') {

                    if (this.chequeAndGurantee.statusDate == '' || this.chequeAndGurantee.remarks == '' || this.chequeAndGurantee.statusDate == null || this.chequeAndGurantee.remarks == null) {
                        root.$swal({
                            title: this.$t('ChequeBook.Error'),
                            text: this.$t('ChequeBook.DateAndRemarksRequired'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                        });
                        return;
                    }
                }

                if (dateCheck) {
                    if (this.ChequeDateRequired && this.ValidityDateAuto) {
                        root.$swal({
                            title: this.$t('ChequeBook.Error'),
                            text: this.$t('ChequeBook.ValidDate'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3500,
                            timerProgressBar: true,
                        });
                        return;
                    }
                  
                }
                if (this.chequeAndGurantee.amount<=0) {
                    root.$swal({
                        title: this.$t('ChequeBook.Error'),
                        text: this.$t('ChequeBook.GreaterThanZero'),
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 3500,
                        timerProgressBar: true,
                    });
                    return;
                }
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.chequeAndGurantee.cashTypes == 'Not Reserved' || this.chequeAndGurantee.cashTypes == 'غير محجوز') {
                    this.chequeAndGurantee.cashType = 'NotReserved';
                }
                else if (this.chequeAndGurantee.cashTypes == 'محجوز' || this.chequeAndGurantee.cashTypes == 'Reserved') {
                    this.chequeAndGurantee.cashType = 'Reserved';
                }
                else {
                    this.chequeAndGurantee.cashType = 0;
                }


               



                this.chequeAndGurantee.chequeType = this.chequeAndGurantee.chequeTypes;
                this.chequeAndGurantee.statusType = this.chequeAndGurantee.statusTypes;
                if (this.chequeAndGurantee.chequeTypes == '') {
                    this.chequeAndGurantee.chequeType=0
                }
                if (this.chequeAndGurantee.statusTypes == '') {
                    this.chequeAndGurantee.statusType=0
                }

                if (this.$i18n.locale == 'ar') {


                    if (this.chequeAndGurantee.chequeTypes == 'طبيعي') {

                        this.chequeAndGurantee.chequeType = 'Normal'
                    }
                    else if (this.chequeAndGurantee.chequeTypes == 'يتقدم') {
                        this.chequeAndGurantee.chequeType = 'Advance'

                    }
                    else if (this.chequeAndGurantee.chequeTypes == 'حماية') {
                        this.chequeAndGurantee.chequeType = 'Security'

                    }
                    else if (this.chequeAndGurantee.chequeTypes == 'يضمن') {
                        this.chequeAndGurantee.chequeType = 'Guarantee'

                    }


                    if (this.chequeAndGurantee.statusTypes == 'صرف') {

                        this.chequeAndGurantee.statusType = 'Cashed'
                    }
                    else if (this.chequeAndGurantee.statusTypes == 'ممنوع') {
                        this.chequeAndGurantee.statusType = 'Blocked'

                    }
                    else if (this.chequeAndGurantee.statusTypes == 'ألغيت') {
                        this.chequeAndGurantee.statusType = 'Cancelled'

                    }
                    else if (this.chequeAndGurantee.statusTypes == 'عاد') {
                        this.chequeAndGurantee.statusType = 'Returned'

                    }
                  
                }
                
                if (this.chequeAndGurantee.statusType == null) {
                    this.chequeAndGurantee.statusType = 0;
                }
                if (this.chequeAndGurantee.chequeType == null) {
                    this.chequeAndGurantee.chequeType = 0;
                }
                if (this.chequeAndGurantee.cashType == null) {
                    this.chequeAndGurantee.cashType = 0;
                }
                this.$https.post('/Payroll/SaveChequeGuranteeInformation', this.chequeAndGurantee, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if (response.data.isSuccess == true) {


                            root.$swal({
                                title: root.$t('ChequeBook.Saved'),
                                text: root.$t('ChequeBook.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            root.close(true, root.chequeAndGurantee.bankId);

                        }
                        else {
                            root.$swal({
                                title: root.$t('ChequeBook.Error'),
                                text: root.$t('ChequeBook.YourLoanRecoveryNameAlreadyExist'),
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: root.$t('ChequeBook.SomethingWrong'),
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        mounted: function () {

            this.ValidityDateAuto = localStorage.getItem('ValidityDateAuto') == 'true' ? true : false;
            this.ChequeDateRequired = localStorage.getItem('ChequeDateRequired') == 'true' ? true : false;


            if (this.$i18n.locale == 'ar') {


                if (this.chequeAndGurantee.chequeTypes == 'Normal') {

                    this.chequeAndGurantee.chequeTypes = 'طبيعي'
                }
                else if (this.chequeAndGurantee.chequeTypes == 'Advance') {
                    this.chequeAndGurantee.chequeTypes = 'يتقدم'

                }
                else if (this.chequeAndGurantee.chequeTypes == 'Security') {
                    this.chequeAndGurantee.chequeTypes = 'حماية'

                }
                else if (this.chequeAndGurantee.chequeTypes == 'Guarantee') {
                    this.chequeAndGurantee.chequeTypes = 'يضمن'

                }


                if (this.chequeAndGurantee.statusTypes == 'Cashed') {

                    this.chequeAndGurantee.statusTypes = 'صرف'
                }
                else if (this.chequeAndGurantee.statusTypes == 'Blocked') {
                    this.chequeAndGurantee.statusTypes = 'ممنوع'

                }
                else if (this.chequeAndGurantee.statusTypes == 'Cancelled') {
                    this.chequeAndGurantee.statusTypes = 'ألغيت'

                }
                else if (this.chequeAndGurantee.statusTypes == 'Returned') {
                    this.chequeAndGurantee.statusTypes = 'عاد'

                }

                if (this.chequeAndGurantee.cashTypes == 'NotReserved') {
                    this.chequeAndGurantee.cashTypes = 'غير محجوز'
                }
                else if (this.chequeAndGurantee.cashTypes == 'Reserved') {
                    this.chequeAndGurantee.cashTypes = 'محجوز'
                }


            }
            else {
                if (this.chequeAndGurantee.cashTypes == 'NotReserved') {
                    this.chequeAndGurantee.cashTypes = 'Not Reserved'
                }
            }

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');



        },
        created: function () {
            this.chequeAndGurantee = this.chequeandgurantee;

        }
    }
</script>
<style scoped>
    .input-group-append .input-group-text, .input-group-prepend .input-group-text {
        background-color: #e3ebf1;
        border: 1px solid #e3ebf1;
        color: #000000;
    }

    .input-group .form-control {
        border-left: 1px solid #e3ebf1;
    }

        .input-group .form-control:focus {
            border-left: 1px solid #3178F6;
        }

    .input-group-text {
        border-radius: 0;
    }

    .Heading1 {
        font-size: 27px !important;
        font-style: normal;
        font-weight: 600;
        color: #3178F6;
    }

    .Heading2 {
        font-size: 20px !important;
        font-style: normal;
        color: black;
    }

    .SpanColor {
        font-size: 15px !important;
        font-style: normal;
        font-weight: 600;
    }
</style>


