<template>
    <div>
        <div class="invoice-table table-responsive mt-5" v-if="formName =='BankReceipt' || formName =='BankPay'">
            <table class="table table-bordered" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead>
                    <tr class="text-capitalize text-center">
                        <th class="text-center" style=" width: 15%;"> {{ $t('AddPaymentVoucherDetail.Date') }}</th>
                        <th class="text-center" style=" width: 15%;" v-if="formName=='CashReceipt' || formName=='BankReceipt'">{{ $t('AddPaymentVoucherDetail.CustomerAccount') }}</th>
                        <th class="text-center" style=" width: 15%;" v-if="formName=='BankPay' || formName=='CashPay'">{{ $t('AddPaymentVoucherDetail.SupplierAccount') }}</th>
                        <th class="text-center" style=" width: 15%;">{{ $t('AddPaymentVoucherDetail.ChequeNumber') }}</th>
                        <th class="text-center" style=" width: 15%;" v-if="formName=='BankPay' || formName=='CashPay'">{{ $t('AddPaymentVoucherDetail.PurchaseInvoice') }}</th>
                        <th class="text-center" style=" width: 15%;" v-if="formName=='CashReceipt' || formName=='BankReceipt'"> {{ $t('AddPaymentVoucherDetail.SaleInvoice') }}</th>
                        <th class="text-left" style=" width: 15%;">{{ $t('AddPaymentVoucherDetail.Description') }}</th>
                        <th class="text-center" style=" width: 10%;">{{ $t('AddPaymentVoucherDetail.Amount') }}</th>
                        <th  style=" width: 5%;"></th>
                    </tr>
                </thead>
                <tbody  :key="render">
                    <tr class="text-left" v-for="items in paymentVoucherDetails" v-bind:key="items.id">
                        <td>
                            <datepicker v-model="items.date" :dropdowndatecss="true"></datepicker>
                        </td>
                        <td>
                            <accountdropdown v-model="items.contactAccountId" :formNames="formName" :dropdownaccount="'dropdownAccountcss'" v-bind:accounts="accounts" v-bind:accountsvalue="batchaccountValue" :key="accountrender"></accountdropdown>
                        </td>
                        <td>
                            <input type="text" class="inpcss chequeNumber" v-model="items.chequeNumber" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </td>
                        <td v-if="formName=='BankPay' || formName=='CashPay'">
                            <purchaseinvoicedropdown v-model="items.purchaseInvoice" :dropdownaccount="'dropdownAccountcss'" v-bind:selectedIdPrucahse="paymentVoucherDetails" :isClass="true" v-bind:isExpense="true" :modelValue="items.purchaseInvoice" @update:modelValue="getPurchaseDetail(items.purchaseInvoice)" />
                        </td> 
                        <td v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                            <sale-invoice-dropdown v-model="items.saleInvoice" :dropdownaccount="'dropdownAccountcss'" v-bind:isCredit="true" :isClass="true" v-bind:modelValue="items.saleInvoice" v-bind:isExpense="true" v-bind:selectedIdInvoice="paymentVoucherDetails" @update:modelValue="getSaleDetail(items.saleInvoice)" />
                        </td>
                        <td>
                            <input type="text" class="inpcss batchvno" v-model="items.description" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </td>
                        <td>
                            <input v-model="items.amount" class="inpcssdebit text-right" type="number" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </td>
                        <td>
                            <button title="Remove Item" class="btn btn-secondary btn-neutral  btn-icon" v-on:click="removeRow(items.id)">
                                <i class="nc-icon nc-simple-remove"></i>
                            </button>
                        </td>
                    </tr>
                    <tr >
                        <td>
                            <datepicker v-model="item.date" :dropdowndatecss="'dropdownDatecss'" :key="refresh"></datepicker>
                        </td>
                        <td>
                            <accountdropdown v-model="item.contactAccountId" :formNames="formName" :dropdownaccount="'dropdownAccountcss'" v-bind:accounts="accounts" :key="refresh"></accountdropdown>
                        </td>
                        <td>
                            <input type="number" class="inpcss chequeNumber" v-model="item.chequeNumber" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </td>
                        <td v-if="formName=='BankPay' || formName=='CashPay'">
                            <purchaseinvoicedropdown v-model="item.purchaseInvoice" v-bind:selectedIdPrucahse="paymentVoucherDetails" v-bind:isExpense="true" :dropdownaccount="'dropdownAccountcss'" :key="refresh" :isClass="true" @update:modelValue="getPurchaseDetail(item.purchaseInvoice)" />
                        </td>
                        <td v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                            <sale-invoice-dropdown v-model="item.saleInvoice" :dropdownaccount="'dropdownAccountcss'"  v-bind:isCredit="true" v-bind:isExpense="true" v-bind:selectedIdInvoice="paymentVoucherDetails"  :key="refresh" :modelValue="item.saleInvoice" :isClass="true" @update:modelValue="getSaleDetail(item.saleInvoice)" />
                        </td>
                        <td>
                            <input type="text" class="inpcssdebit batchvno" v-model="item.description" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </td>
                        <td class="text-center">
                            <input v-model="item.amount" class="inpcssdebit  text-right" type="number" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        </td>
                        <td style="width:60px" >
                            <button title="Add New Item" class="btn btn-icon btn-sm " v-on:click="addLineItem()">
                                <i class="nc-icon bord rounded-circle nc-simple-add"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="invoice-table table-responsive mt-5" v-else>
            <table class="table table-bordered" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead>
                    <tr class="text-capitalize text-center">
                        <th class="text-center'" style=" width: 20%;">{{ $t('AddPaymentVoucherDetail.Date') }}</th>
                        <th class="text-center" style=" width: 15%;" v-if="formName=='CashReceipt' || formName=='BankReceipt'">{{ $t('AddPaymentVoucherDetail.CustomerAccount') }}</th>
                        <th class="text-center" style=" width: 15%;" v-if="formName=='BankPay' || formName=='CashPay'">{{ $t('AddPaymentVoucherDetail.SupplierAccount') }}</th>
                        <th class="text-center" style=" width: 20%;" v-if="formName=='BankPay' || formName=='CashPay'">{{ $t('AddPaymentVoucherDetail.PurchaseInvoice') }}</th>
                        <th class="text-center" style=" width: 20%;" v-if="formName=='CashReceipt' || formName=='BankReceipt'">{{ $t('AddPaymentVoucherDetail.SaleInvoice') }}</th>
                        <th style=" width: 20%;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('AddPaymentVoucherDetail.Description') }}</th>
                        <th class="text-center" style=" width: 15%;">{{ $t('AddPaymentVoucherDetail.Amount') }}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-left" v-for="items in paymentVoucherDetails" v-bind:key="items.id">
                        <td>
                            <datepicker v-model="items.date" :dropdowndatecss="'dropdownDatecss'"></datepicker>
                        </td>
                        <td>
                            <accountdropdown v-model="items.contactAccountId" :formNames="formName" :dropdownaccount="'dropdownAccountcss'" v-bind:accounts="accounts" v-bind:accountsvalue="batchaccountValue" :key="accountrender"></accountdropdown>
                        </td>
                        <td v-if="formName=='BankPay' || formName=='CashPay'">
                            <purchaseinvoicedropdown v-model="items.purchaseInvoice" :dropdownaccount="'dropdownAccountcss'"  v-bind:selectedIdPrucahse="paymentVoucherDetails" :isClass="true" :modelValue="items.purchaseInvoice" v-bind:isExpense="true"  @update:modelValue="getPurchaseDetail(items.purchaseInvoice)" />
                        </td>
                        <td v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                            <sale-invoice-dropdown v-model="items.saleInvoice" :dropdownaccount="'dropdownAccountcss'"  v-bind:isCredit="true"  v-bind:modelValue="items.saleInvoice" :isClass="true" v-bind:isExpense="true" v-bind:selectedIdInvoice="paymentVoucherDetails"   @update:modelValue="getSaleDetail(items.saleInvoice)" />
                        </td>
                        <td>
                            <input type="text" class="inpcss batchvno" v-model="items.description" />
                        </td>
                        <td>
                            <input v-model="items.amount" class="inpcssdebit text-right" type="number" />
                        </td>
                        <td>
                            <button title="Remove Item" class="btn btn-secondary btn-sm btn-neutral  btn-icon" v-on:click="removeRow(items.id)">
                                <i class="nc-icon nc-simple-remove"></i>
                            </button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <datepicker v-model="item.date" :dropdowndatecss="'dropdownDatecss'" :key="refresh"></datepicker>
                        </td>
                        <td>
                            <accountdropdown v-model="item.contactAccountId" :formNames="formName" :dropdownaccount="'dropdownAccountcss'" v-bind:accounts="accounts" :key="refresh"></accountdropdown>
                        </td>
                        <td v-if="formName=='BankPay' || formName=='CashPay'">
                            <purchaseinvoicedropdown v-model="item.purchaseInvoice" :dropdownaccount="'dropdownAccountcss'"  v-bind:selectedIdPrucahse="paymentVoucherDetails"  :isClass="true" v-bind:isExpense="true" @update:modelValue="getPurchaseDetail(item.purchaseInvoice)" :key="refresh"/>
                        </td>
                        <td v-if="formName=='CashReceipt' || formName=='BankReceipt'">
                            <sale-invoice-dropdown v-model="item.saleInvoice" :dropdownaccount="'dropdownAccountcss'"  v-bind:isCredit="true"    :modelValue="item.saleInvoice" v-bind:isExpense="true" v-bind:selectedIdInvoice="paymentVoucherDetails" :isClass="true" @update:modelValue="getSaleDetail(item.saleInvoice)" :key="refresh" />
                        </td>
                        <td>
                            <input type="text" class="inpcssdebit batchvno" v-model="item.description" />
                        </td>
                        <td class="text-center">
                            <input v-model="item.amount" class="inpcssdebit  text-right" type="number" />
                        </td>
                        <td style="width:60px">
                            <button title="Add New Item" class="btn btn-sm  btn-icon " v-on:click="addLineItem()">
                                <i class="nc-icon bord rounded-circle nc-simple-add"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
<style scoped>
    .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
        padding: 0px 7px;
    }
</style>
<script>
    import moment from 'moment'
    export default {
        props: ['formName', 'render', 'paymentVoucherDetailss'],
        data: function () {
            return {
                paymentVoucherDetails: [],
                item: {
                    id: '',
                    description: '',
                    contactAccountId: '',
                    purchaseInvoice: '',
                    saleInvoice: '',
                    date: '',
                    amount: '',
                    chequeNumber: '',
                },
                addItem: false,
                accounts: [],
                accountIdValue: [],
                accountslevelone: [],
                batchaccountValue: [],
                voucherNo: '',
                number: '',
                rander: 0,
                accountrender: 0,
                purchaserander: 0,
                refresh: 0,
                currency: '',
                searchTerm:'',
                supplierId: '00000000-0000-0000-0000-000000000000',


            }
        },

        methods: {
            getPurchaseDetail: function (id) {
                
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                 {
                    root.$https.get('/PurchasePost/PurchasePostDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            
                            root.item.amount = response.data.netAmount;
                        }

                    });

                }
            },
            getSaleDetail: function (id) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleAmountDetail?id=' + id , { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.item.amount = response.data.netAmount;
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },
            addLineItem: function () {
                
                this.item.id = this.createUUID();
                if (this.item.date == '' || this.item.contactAccountId == '' || this.item.amount == ''){
                    return;
                }
                else{                    
                    this.paymentVoucherDetails.push({
                        id: this.item.id,
                        description: this.item.description,
                        date: this.item.date,
                        amount: this.item.amount,
                        chequeNumber: this.item.chequeNumber,
                        contactAccountId: this.item.contactAccountId,
                        purchaseInvoice: this.item.purchaseInvoice,
                        saleInvoice: this.item.saleInvoice,
                    });
                    this.addItem = false;
                    this.item = {
                        id: '',
                        date: moment().format('llll'),
                        amount: '',
                        contactAccountId: '',
                        purchaseInvoice: '',
                        saleInvoice: '',
                        chequeNumber: '',
                        description: '',
                    };
                    this.purchaserander;
                    this.refresh = this.refresh + 1;
                }

            },
            removeRow: function (id) {
                var ds = this.paymentVoucherDetails.findIndex(function (i) {
                    return i.id === id;
                });
                this.paymentVoucherDetails.splice(ds, 1);
            },
            NewItem: function () {
                
                this.addItem = true;

                this.item = {
                    date: '',
                    amount: 0.00,
                    contactAccountId: '',
                    purchaseInvoice: '',
                    saleInvoice: '',
                    chequeNumber: '',
                    description: '',
                }
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

        },
        updated: function () {
            this.$emit('paymentVoucherDetails', this.paymentVoucherDetails);

        },
        mounted: function () {

            // this.rander = 1;
            // this.accountrender++;
            // this.refresh++;
            
            this.paymentVoucherDetails = this.paymentVoucherDetailss;
            
            this.item.date = moment().format('llll');
            this.currency = localStorage.getItem('currency');

        },
    }
</script>