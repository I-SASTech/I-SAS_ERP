<template>
    <modal :show="show" :modalLarge="true" >
        <div class="modal-content" v-if=" isValid('CanPayAdvanceFromView') || isValid('CanAddAdvancePayment') || isValid('CanAdvancePaymentFromList') || isValid('CanServicePayAdvanceFromView')">
            <div class="modal-header">
                <h6 class="modal-title" v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000'  && formName=='BankReceipt'">{{ $t('PurchaseOrderPayment.UpdateCustomerPayReceipt')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName=='BankPay'">{{ $t('PurchaseOrderPayment.UpdateSupplierPaymentReceipt')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000' && formName=='BankReceipt'">{{ $t('PurchaseOrderPayment.AddCustomerPayReceipt')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'  && formName=='BankPay'">{{ $t('PurchaseOrderPayment.AddSupplierPaymentReceipt')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'  && formName=='PettyCash'">{{ $t('PurchaseOrderPayment.AddPettyCash')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000'  && formName=='PettyCash'">{{ $t('PurchaseOrderPayment.UpdatePettyCash')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'  && formName=='AdvanceReceipt'">{{ $t('PurchaseOrderPayment.AdvancePayment')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000'  && formName=='AdvanceReceipt'">{{ $t('PurchaseOrderPayment.UpdateAdvancePayment')}} -  {{ paymentVoucher.voucherNumber }}</h6>

                <h6 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'  && formName=='AdvancePay'">{{ $t('PurchaseOrderPayment.AdvancePay')}} -  {{ paymentVoucher.voucherNumber }}</h6>
                <h6 class="modal-title" v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000'  && formName=='AdvancePay'">{{ $t('PurchaseOrderPayment.UpdateAdvancePay')}} -  {{ paymentVoucher.voucherNumber }}</h6>

                <h6 class="modal-title" v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'  && formName=='ContractorAdvancePay'">Contractor Advance Payment -  {{ paymentVoucher.voucherNumber }}</h6>

                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>{{ $t('PurchaseOrderPayment.Date') }} :<span class="text-danger"> *</span></label>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.date.$error}">
                            <datepicker v-model="v$.paymentVoucher.date.$model"></datepicker>
                            <span v-if="v$.paymentVoucher.date.$error" class="error">
                                <span v-if="!v$.paymentVoucher.date.required">{{formName}}  {{ $t('PurchaseOrderPayment.DateRequired') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='PettyCash'">
                        <label>
                            {{ $t('PurchaseOrderPayment.PaymentMode') }}:
                            <span class="text-danger"> *</span>
                        </label>

                        <div class="form-group">

                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " disabled v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else disabled v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('PurchaseOrderPayment.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>

                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6" v-else>
                        <label>
                            {{ $t('PurchaseOrderPayment.PaymentMode') }}:
                            <span class="text-danger"> *</span>
                        </label>

                        <div class="form-group">

                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else v-model="paymentVoucher.paymentMode" @update:modelValue="GetAccount(paymentVoucher.paymentMode)" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('PurchaseOrderPayment.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>

                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>
                            {{ $t('PurchaseOrderPayment.PaymentType') }}:
                            <span class="text-danger" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' "></span>
                            <span class="text-danger" v-else> *</span>
                        </label>



                        <div class="form-group" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">

                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " disabled v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else v-model="paymentVoucher.paymentMethod" disabled :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('PurchaseOrderPayment.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>

                        </div>
                        <div class="form-group" v-else>

                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else v-model="paymentVoucher.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('PurchaseOrderPayment.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>

                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' ">
                            {{ $t('PurchaseOrderPayment.CashAccount') }}:
                            <span class="text-danger"> *</span>
                        </label>
                        <label v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' ">
                            {{ $t('PurchaseOrderPayment.BankAccount') }}:
                            <span class="text-danger"> *</span>
                        </label>
                        <label v-else>
                            {{ $t('PurchaseOrderPayment.BankAccount') }}:
                            <span class="text-danger"> *</span>
                        </label>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-if="paymentVoucher.paymentMode=='Cash' || paymentVoucher.paymentMode=='السيولة النقدية' " v-bind:key="randerAccount">
                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="CashPay" :advance="'true'"></accountdropdown>
                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}}  {{ $t('PurchaseOrderPayment.AccountRequired') }}</span>
                            </span>
                        </div>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else-if="paymentVoucher.paymentMode=='Bank' || paymentVoucher.paymentMode=='مصرف' " v-bind:key="randerAccount">
                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="BankPay" :advance="'true'"></accountdropdown>
                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}}  {{ $t('PurchaseOrderPayment.AccountRequired') }}</span>
                            </span>
                        </div>  <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error}" v-else>
                            <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="BankPay" :advance="'true'"></accountdropdown>
                            <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{formName}}  {{ $t('PurchaseOrderPayment.AccountRequired') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label v-if="formName=='CashReceipt' || formName=='BankReceipt' || formName=='AdvanceReceipt' ">
                            {{ $t('PurchaseOrderPayment.CustomerAccount') }}:
                            <span class="text-danger"> *</span>
                        </label>
                        <label v-if="formName=='ContractorAdvancePay' ">
                            Contractor Account
                            <span class="text-danger"> *</span>

                        </label>
                        <label v-if="formName=='PettyCash' ">
                            {{ $t('PurchaseOrderPayment.Account') }}:
                            <span class="text-danger"> *</span>

                        </label>
                        <label v-if="formName=='BankPay' || formName=='CashPay' || formName=='AdvancePay'">
                            {{ $t('PurchaseOrderPayment.SupplierAccount') }}:
                            <span class="text-danger"> *</span>

                        </label>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error}" v-if=" formName=='PettyCash'">
                            <accountdropdown v-model="v$.paymentVoucher.contactAccountId.$model" :formName="'PettyCashAccount'"></accountdropdown>
                            <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.contactAccountId.required">{{formName}}  {{ $t('PurchaseOrderPayment.AccountRequired') }}</span>
                            </span>
                        </div>
                        <div class="form-group" v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error}" v-else>
                            <accountdropdown :formNames="formName" v-model="v$.paymentVoucher.contactAccountId.$model"></accountdropdown>
                            <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                <span v-if="!v$.paymentVoucher.contactAccountId.required">{{formName}}  {{ $t('PurchaseOrderPayment.AccountRequired') }}</span>
                            </span>
                        </div>
                    </div>

                    <div hidden class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                        <label>
                            {{ $t('PurchaseOrderPayment.SaleInvoice') }}
                        </label>
                        <div class="form-group">
                            <sale-invoice-dropdown ref="saleInvoiceDropdown" v-model="paymentVoucher.saleInvoice" v-bind:isExpense="true" @update:modelValue="getSaleNetAmount" :key="saleInvoiceRander" v-bind:isCredit="true" :contactId="paymentVoucher.contactAccountId" :isDisabled="isShow" />

                        </div>
                    </div>
                    <div hidden class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='BankPay' || formName=='CashPay'">
                        <label>
                            {{ $t('PurchaseOrderPayment.PurchaseInvoice') }}
                        </label>
                        <div class="form-group">
                            <purchaseinvoicedropdown @update:modelValue="getPurchaseNetAmount" ref="purchaseInvoiceDropdown" :modelValue="paymentVoucher.purchaseInvoice" v-model="paymentVoucher.purchaseInvoice" v-bind:isExpense="true" :key="purchaseInvoiceRander" :supplierAccountId="paymentVoucher.contactAccountId" :isDisabled="isShow" />

                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6" v-if="paymentVoucher.paymentMethod=='Cheque' || paymentVoucher.paymentMethod=='التحقق من' ">
                        <label>
                            {{ $t('PurchaseOrderPayment.ChequeNumber') }}
                        </label>
                        <div class="form-group">
                            <input v-model="paymentVoucher.chequeNumber" class="form-control" type="text" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>
                            {{ $t('PurchaseOrderPayment.Amount') }} :
                            <span class="text-danger"> *</span>
                        </label>
                        <div class="form-group">
                            <my-currency-input v-model="paymentVoucher.amount" @update:modelValue="zeroPrice(paymentVoucher.amount)"></my-currency-input>
                            <!--<input v-model="paymentVoucher.amount" class="form-control" type="number" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />-->
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6" v-if=" formName=='PettyCash' ">
                        <label>
                            {{ $t('PurchaseOrderPayment.PattyCashType') }}
                        </label>
                        <div class="form-group">
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="paymentVoucher.pettyCash" :options="['Temporary', 'General', 'Advance']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else v-model="paymentVoucher.pettyCash" :options="['مؤقت', 'عام', 'تقدم']" :show-labels="false" v-bind:placeholder="$t('PurchaseOrderPayment.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-lg-12 mt-4 ">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                        <div class="form-group pe-3">
                                            <label>{{ $t('PurchaseOrderPayment.Narration') }} / {{$t('PurchaseOrderPayment.Remarks')}}:</label>
                                            <textarea v-model="paymentVoucher.narration" class="form-control" rows="3" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4" v-if="attachment==false">
                                        <div class="form-group ps-3">
                                            <label>
                                                {{ $t('PurchaseOrderPayment.Attachment') }}
                                            </label>
                                            <input ref="imgupload1" type="file" id="file-input"
                                                   @change="uploadImage"
                                                   name="image" style="opacity:1;padding:25px">
                                            <div>
                                                <small class="text-muted">
                                                    {{ $t('PurchaseOrderPayment.FileSize') }}
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="" v-else>
                                        <div class="col-md-12">

                                            <div class="col-sm-6 float-right">
                                                <a href="javascript:void(0)" class="btn btn-outline-primary  float-right" v-on:click="attachment=false"><i class="fa fa-upload"></i> Upload</a>
                                            </div>
                                        </div>
                                        <div class="card-body ">
                                            <div class=" table-responsive">
                                                <table class="table ">
                                                    <thead class="m-0">
                                                        <tr>
                                                            <th>#</th>
                                                            <th>{{ $t('PurchaseOrderPayment.Date') }} </th>
                                                            <th>{{ $t('PurchaseOrderPayment.Attachment') }}</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr v-for="(contact,index) in paymentVoucher.paymentVoucherAttachments" v-bind:key="index">
                                                            <td v-if="contact.path!=null ">
                                                                {{index+1}}
                                                            </td>
                                                            <th v-if="contact.path!=null ">{{$formatDate(paymentVoucher.date)   }}</th>
                                                            <td v-if="contact.path!=null ">
                                                                <button class="btn btn-primary  btn-icon mr-2"
                                                                        v-on:click="DownloadAttachment(contact.path)">
                                                                    <i class="fa fa-download"></i>
                                                                </button>
                                                            </td>
                                                        </tr>
                                                    </tbody>
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
            <div class="modal-footer" v-if="paymentVoucher.id=='00000000-0000-0000-0000-000000000000'">
                    <button type="button" class="btn btn-soft-primary btn-s " v-bind:disabled="v$.paymentVoucher.$invalid" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{ $t('PurchaseOrderPayment.SaveAndPost') }}</button>
                    <!--<button type="button" class="btn btn-primary  ml-2" v-bind:disabled="v$.paymentVoucher.$invalid" v-if=" isValid('Can Save Bank Pay as Draft') || isValid('Can Save Cash Pay as Draft')|| isValid('Can Save Bank Receipt as Draft')||  isValid('Can Save Cash Receipt as Draft')" v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i>  {{ $t('PaymentVoucher.SaveasDraft') }}</button>-->
                <button class="btn btn-danger " v-on:click="onCancel" >  {{ $t('PurchaseOrderPayment.Cancel') }}</button>
            </div>
            <div class="modal-footer" v-else>
                <button type="button" class="btn btn-soft-primary btn-sm " v-on:click="SaveVoucher('Rejected')"><i class="far fa-save"></i> {{ $t('PurchaseOrderPayment.SaveasReject') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{ $t('PurchaseOrderPayment.SaveAndPost') }}</button>               
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="onCancel">{{ $t('PurchaseOrderPayment.Cancel') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import moment from "moment";
    export default {
        name: "PurchaseOrderPayment",
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['formName', 'show', 'purchaseOrderId', 'isPurchase', 'isSaleOrder', 'customerAccountId', 'totalAmount', 'newbatchProcessContractorId','newisContractor'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                batchProcessContractorId: this.newbatchProcessContractorId,
                isContractor: this.newisContractor,
                ispayable: true,
                render: 0,
                saleInvoiceRander: 0,
                purchaseInvoiceRander: 0,
                isShow: true,
                attachment: false,
                paymentVoucher: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    voucherNumber: '',
                    chequeNumber: '',
                    narration: '',
                    paymentVoucherType: '',
                    amount: 0,
                    approvalStatus: 'Draft',
                    purchaseInvoice: '',
                    saleInvoice: '',
                    bankCashAccountId: '',
                    reparingOrderId: '',
                    contactAccountId: '',
                    paymentMode: '',
                    paymentMethod: '',
                    userName: '',
                    branchId: '',
                },
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVAt: 0,
                    inclusiveVat: 0
                },
                loading: false,
                type: '',
                isBank: true,
                voucherNumberRander: 0,
                language: 'Nothing',
                CashPay: 'CashPay',
                BankPay: 'BankPay',
                randerAccount: 0,
                disable: false,

            }
        },
        created() {

        },

        validations() {
            return {
                paymentVoucher: {
                    voucherNumber: {
                        required
                    },
                    paymentMode: {
                        required
                    },
                    date: {
                        required
                    },
                    bankCashAccountId: {
                        required
                    },
                    contactAccountId: {
                        required
                    },
                    amount: { minValue: minValue(1) }
                }
                }
        },
        methods: {
            //getAmount: function () {
            //

            //    var totalAmount = this.$refs.childComponentRef.getTotalAmount();
            //    this.paymentVoucher.amount = totalAmount;

            //},
            close: function () {
                this.$emit('close');
            },
            GetAccount: function (x) {


                if (x == 'السيولة النقدية' || x == 'Bank') {
                    this.randerAccount++;

                }
                else if (x == 'مصرف' || x == 'Cash') {
                    this.randerAccount++;
                }

            },
            DownloadAttachment(path) {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },

            uploadImage() {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = null;

                file = this.$refs.imgupload1.files;

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {


                        if (response.data != null) {

                            root.paymentVoucher.path = response.data;

                        }
                    },
                        function () {
                            this.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            zeroPrice: function (x) {

                if (x == 0) {
                    this.disable = true;


                }
                else {
                    this.disable = false;
                }

            },
            UpdateStatus: function (status) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/PaymentVoucher/UpdateStatusPaymentVoucher?id=' + this.paymentVoucher.id + '&approvalStatus=' + status, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    window.location.href = "/paymentVoucherList?formName=" + root.formName;
                                }
                            }
                        });

                    }
                    else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        });
                    }

                }, function (value) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: value,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                ).catch(error => {

                    var customError = JSON.stringify(error.response.data.error);
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: customError,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                    root.loading = false;
                });
            },
            getSaleNetAmount: function () {

                this.paymentVoucher.amount = this.$refs.saleInvoiceDropdown.GetAmountOfSelected()
            },
            getPurchaseNetAmount: function () {
                this.paymentVoucher.amount = this.$refs.purchaseInvoiceDropdown.GetAmountOfSelected()
            },
            enableInvoiceDropdown: function () {

                this.paymentVoucher.amount = 0;
                this.paymentVoucher.chequeNumber = ''
                this.paymentVoucher.saleInvoice = '00000000-0000-0000-0000-000000000000';
                this.paymentVoucher.purchaseInvoice = '00000000-0000-0000-0000-000000000000';
                if (this.formName == 'CashReceipt' || this.formName == 'BankReceipt') {
                    this.isShow = false
                    this.saleInvoiceRander++;
                }
                else if (this.formName == 'BankPay' || this.formName == 'CashPay') {
                    this.isShow = false
                    this.purchaseInvoiceRander++;
                }

            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    if (this.paymentVoucher.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addPaymentVoucherformName?formName=' + this.formName);
                    }
                    else {

                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },

            GetAutoCodeGenerator: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/PaymentVoucher/AutoGenerateCode?paymentVoucherType=' + value + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.paymentVoucher.voucherNumber = response.data;
                        root.voucherNumberRander++;
                    }
                });
            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            SaveVoucher: function (x) {

                if (this.$i18n.locale == 'ar') {
                    if (this.paymentVoucher.pettyCash == 'مؤقت') {
                        this.paymentVoucher.pettyCash = 1;
                    }
                    if (this.paymentVoucher.pettyCash == 'عام') {
                        this.paymentVoucher.pettyCash = 2;
                    }
                    if (this.paymentVoucher.pettyCash == 'تقدم') {
                        this.paymentVoucher.pettyCash = 3;
                    }
                    if (this.paymentVoucher.paymentMethod == 'التحقق من') {
                        this.paymentVoucher.paymentMethod = 1;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'تحويل') {
                        this.paymentVoucher.paymentMethod = 2;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'الوديعة') {
                        this.paymentVoucher.paymentMethod = 3;
                    }
                    else {
                        this.paymentVoucher.paymentMethod = 0;
                    }

                    if (this.paymentVoucher.paymentMode == 'السيولة النقدية') {
                        this.paymentVoucher.paymentMode = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'مصرف') {
                        this.paymentVoucher.paymentMode = 1;
                    }
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (this.paymentVoucher.paymentMethod == 'Cheque') {
                        this.paymentVoucher.paymentMethod = 1;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Transfer') {
                        this.paymentVoucher.paymentMethod = 2;
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Deposit') {
                        this.paymentVoucher.paymentMethod = 3;
                    }
                    else {
                        this.paymentVoucher.paymentMethod = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'Cash') {
                        this.paymentVoucher.paymentMode = 0;
                    }
                    if (this.paymentVoucher.paymentMode == 'Bank') {
                        this.paymentVoucher.paymentMode = 1;
                    }
                }
                if (this.paymentVoucher.paymentMethod != 1) {
                    this.paymentVoucher.chequeNumber = '';
                }
                this.loading = true;
                var root = this;
                var token = '';
                this.paymentVoucher.approvalStatus = x;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.batchProcessContractorId == undefined || this.batchProcessContractorId == null) {
                    this.batchProcessContractorId = '00000000-0000-0000-0000-000000000000';
                    this.isContractor = false;
                }
                this.paymentVoucher.branchId = localStorage.getItem('BranchId');
                                
                this.$https.post('/PaymentVoucher/AddPaymentVoucher?purchaseOrderId=' + this.purchaseOrderId + '&isPurchase=' + this.isPurchase + '&isSaleOrder=' + this.isSaleOrder + '&isContractor=' + this.isContractor + '&batchProcessContractorId=' + this.batchProcessContractorId, this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Add') {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: response.data.message.isAddUpdate,
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 2000,
                                    timerProgressBar: true,
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
                                });
                                root.onCancel();
                            }
                            else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    text: response.data.message.isAddUpdate,
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 2000,
                                    timerProgressBar: true,
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
                                });
                                root.onCancel();
                            }

                        }).catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    type: 'error',
                                    icon: 'error',
                                    title: root.$t('PurchaseOrderPayment.Error'),
                                    text: error.response.data,
                                    confirmButtonClass: "btn btn-danger",
                                    showConfirmButton: true,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });

                            root.loading = false
                        })
                        .finally(() => root.loading = false)
                },

               
           
            getpaymentVoucherDetails: function (paymentVoucherItem) {

                this.paymentVoucher = paymentVoucherItem;
            },
            onCancel: function () {
                if (this.isReparing == true) {
                    this.$emit('close',false);
                }
                else {
                    this.$emit('close');

                }
            },
        },
        watch: {
            formName: function () {
                if (this.formName == 'BankPay') {
                    if (this.$route.query.details == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.paymentVoucher.paymentVoucherType = this.formName;
                        this.paymentVoucher.contactAccountId = this.contactAccountId;
                    }
                    if (this.$route.query.details != undefined) {
                        this.attachment = true;
                        this.paymentVoucher = this.$route.query.details.message;
                        this.paymentVoucher.paymentVoucherType = 'BankPay';
                        this.paymentVoucherDetails = this.$route.query.details.message.paymentVoucherDetails;
                        if (this.$i18n.locale == 'ar') {
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'التحقق من';
                            }
                            else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'تحويل';
                            }
                            else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'الوديعة';
                            }
                            else {
                                this.paymentVoucher.paymentMethod = '';
                            }

                            if (this.paymentVoucher.paymentMode == 0) {
                                this.paymentVoucher.paymentMode = 'السيولة النقدية';
                            }
                            if (this.paymentVoucher.paymentMode == 1) {
                                this.paymentVoucher.paymentMode = 'مصرف';
                            }



                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'Cheque';
                            }
                            else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'Transfer';
                            }
                            else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'Deposit';
                            }
                            else {
                                this.paymentVoucher.paymentMethod = '';
                            }
                            if (this.paymentVoucher.paymentMode == 0) {
                                this.paymentVoucher.paymentMode = 'Cash';
                            }
                            if (this.paymentVoucher.paymentMode == 1) {
                                this.paymentVoucher.paymentMode = 'Bank';
                            }

                        }
                    }
                }

                if (this.formName == 'BankReceipt') {
                    if (this.$route.query.details == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.paymentVoucher.paymentVoucherType = this.formName;
                        this.paymentVoucher.contactAccountId = this.contactAccountId;
                    }
                    if (this.$route.query.details != undefined) {
                        this.attachment = true;
                        this.paymentVoucher = this.$route.query.details.message;
                        this.paymentVoucher.paymentVoucherType = 'BankReceipt';
                        this.paymentVoucherDetails = this.$route.query.details.message.paymentVoucherDetails;
                        if (this.$i18n.locale == 'ar') {
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'التحقق من';
                            }
                            else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'تحويل';
                            }
                            else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'الوديعة';
                            }
                            else {
                                this.paymentVoucher.paymentMethod = '';
                            }

                            if (this.paymentVoucher.paymentMode == 0) {
                                this.paymentVoucher.paymentMode = 'السيولة النقدية';
                            }
                            if (this.paymentVoucher.paymentMode == 1) {
                                this.paymentVoucher.paymentMode = 'مصرف';
                            }



                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'Cheque';
                            }
                            else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'Transfer';
                            }
                            else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'Deposit';
                            }
                            else {
                                this.paymentVoucher.paymentMethod = '';
                            }
                            if (this.paymentVoucher.paymentMode == 0) {
                                this.paymentVoucher.paymentMode = 'Cash';
                            }
                            if (this.paymentVoucher.paymentMode == 1) {
                                this.paymentVoucher.paymentMode = 'Bank';
                            }

                        }
                    }
                }
            }
            //    customerAccountId: function () {
            //
            //        if (this.customerAccountId != undefined) {

            //            this.paymentVoucher.contactAccountId = this.customerAccountId;

            //        }
            //    }
        },
        mounted: function () {

            this.language = this.$i18n.locale;
            this.paymentVoucher.date = moment().format("DD MMM YYYY");
            if (this.formName == 'BankPay') {
                if (this.$route.query.details == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;

                }
                if (this.$route.query.details != undefined) {
                    this.paymentVoucher = this.$route.query.details.message;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'BankPay';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }



                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }

                    }

                }
            }
            if (this.formName == 'BankReceipt') {
                if (this.$route.query.details == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    this.paymentVoucher.contactAccountId = this.customerAccountId;
                    this.paymentVoucher.amount = this.totalAmount;
                }
                if (this.$route.query.details != undefined) {

                    this.paymentVoucher = this.$route.query.details.message;
                    this.isShow = false
                    this.attachment = true;
                    this.saleInvoiceRander++;
                    this.paymentVoucher.paymentVoucherType = 'BankReceipt';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }



                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }

                    }

                }
            }

            if (this.formName == 'AdvanceReceipt') {
                if (this.$route.query.details == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    this.paymentVoucher.contactAccountId = this.customerAccountId;
                    if (this.totalAmount == null || this.totalAmount == undefined || this.totalAmount == '') {
                        this.paymentVoucher.amount = 0;
                    }
                    else {
                        this.paymentVoucher.amount = this.totalAmount;

                    }
                }
                if (this.$route.query.details != undefined) {

                    this.paymentVoucher = this.$route.query.details.message;
                    this.isShow = false
                    this.attachment = true;
                    this.saleInvoiceRander++;
                    this.paymentVoucher.paymentVoucherType = 'AdvanceReceipt';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }



                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }

                    }

                }
            }

            if (this.formName == 'AdvancePay') {
                if (this.$route.query.details == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    this.paymentVoucher.contactAccountId = this.customerAccountId;

                    this.paymentVoucher.amount = this.totalAmount;
                }

                if (this.$route.query.details != undefined) {
                    this.paymentVoucher = this.$route.query.details.message;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'AdvancePay';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }



                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }

                    }

                }
            }
            if (this.formName == 'ContractorAdvancePay') {
                if (this.$route.query.details == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    this.paymentVoucher.contactAccountId = this.customerAccountId;
                    
                    this.paymentVoucher.amount = this.totalAmount;
                }
                
                if (this.$route.query.details != undefined) {
                    this.paymentVoucher = this.$route.query.details.message;
                    this.isShow = false
                    this.attachment = true;
                    this.purchaseInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'ContractorAdvancePay';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }



                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }

                    }

                }
            }
            this.render++;
        }
    }
</script>