<template>
    <div class="row"
        v-if="((isValid('CanAddPettyCash') || isValid('CanDraftPettyCash') || isValid('CanRejectPettyCash') || isValid('CanEditPettyCash')) && formName == 'PettyCash') || ((isValid('CanAddCPR') || isValid('CanDraftCPR') || isValid('CanRejectCPR') || isValid('CanEditCPR')) && formName == 'BankReceipt') || ((isValid('CanAddSPR') || isValid('CanDraftSPR') || isValid('CanRejectSPR') || isValid('CanEditSPR')) && (formName == 'BankPay' || formName == 'AdvancePurchase')) || ((isValid('CanDraftSPR') || isValid('CanAddSPR')) && formName == 'AdvanceReceipt')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        <div class="row">
                            <div class="col d-flex align-items-baseline">
                                <div class="media">
                                    <span class="circle-singleline"
                                        style="background-color: #1761FD !important;margin:10px !important"
                                        v-if="formName == 'BankPay'">SP</span>
                                    <span class="circle-singleline"
                                        style="background-color: #1761FD !important;margin:10px !important"
                                        v-else-if="formName == 'AdvancePurchase'">AP</span>
                                    <span class="circle-singleline"
                                        style="background-color: #1761FD !important;margin:10px !important"
                                        v-else-if="formName == 'AdvanceReceipt'">AR</span>
                                    <span class="circle-singleline"
                                        style="background-color: #1761FD !important;margin:10px !important"
                                        v-else-if="formName == 'BankReceipt'">CP</span>
                                    <span class="circle-singleline"
                                        style="background-color: #1761FD !important;margin:10px !important"
                                        v-else-if="formName == 'PettyCash'">PC</span>
                                    <div class="media-body align-self-center ms-3">
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'BankReceipt'">
                                            {{ $t('AddPaymentVoucher.UpdateCustomerPayReceipt') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'BankPay'">
                                            {{ $t('AddPaymentVoucher.UpdateSupplierPaymentReceipt') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'AdvancePurchase'">
                                            Update Advance Payment</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000' && formName == 'BankReceipt'">
                                            {{ $t('AddPaymentVoucher.AddCustomerPayReceipt') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000' && formName == 'BankPay'">
                                            {{ $t('AddPaymentVoucher.AddSupplierPaymentReceipt') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000' && formName == 'AdvancePurchase'">
                                            Add Advance Payment</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000' && formName == 'PettyCash'">
                                            {{ $t('AddPaymentVoucher.AddPettyCash') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000' && formName == 'AdvanceReceipt'">
                                            {{ $t('PurchaseOrderPayment.AdvancePayment') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'PettyCash'">
                                            {{ $t('AddPaymentVoucher.UpdatePettyCash') }}</h6>
                                        <h6 class="m-0 font-20"
                                            v-if="paymentVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'AdvanceReceipt'">
                                            Update Advance Payment</h6>
                                        <div class="col d-flex ">
                                            <p class="text-muted mb-0" style="font-size:13px !important;"><b>{{
                                                paymentVoucher.voucherNumber }}</b> &nbsp;&nbsp; <span>{{
        paymentVoucher.date }}</span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label
                                                v-if="formName == 'CashReceipt' || formName == 'BankReceipt' || formName == 'AdvanceReceipt'">
                                                {{ $t('AddPaymentVoucher.CustomerAccount') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label v-if="formName == 'PettyCash'">
                                                {{ $t('AddPaymentVoucher.Account') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label
                                                v-if="(formName == 'BankPay' || formName == 'AdvancePurchase') || formName == 'CashPay'">
                                                {{ $t('AddPaymentVoucher.SupplierAccount') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error }"
                                                v-if="formName == 'PettyCash'">
                                                <accountdropdown  v-model="v$.paymentVoucher.contactAccountId.$model"
                                                    :formName="'PettyCashAccount'" @update:modelValue="enableInvoiceDropdown(true)"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{ formName }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error }"
                                                v-else-if="formName == 'AdvanceReceipt'">
                                                <!-- <accountdropdown 
                                                    v-model="v$.paymentVoucher.contactAccountId.$model"
                                                    :formName="'Customer'" @update:modelValue="RemoveRecord"></accountdropdown> -->
                                                    <customersupplieraccountsdropdown :isCustomer="true" v-model="v$.paymentVoucher.contactAccountId.$model" :paymentTerm="'Credit'"  @update:modelValue="setValue"/>
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{ formName }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error }"
                                                v-else-if="formName == 'AdvancePurchase'">
                                                <accountdropdown 
                                                    v-model="v$.paymentVoucher.contactAccountId.$model"
                                                    :formName="'Supplier'" @update:modelValue="RemoveRecord"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{ formName }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>

                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error }"
                                                v-else-if="formName == 'BankReceipt'">
                                                    <customersupplieraccountsdropdown :isCustomer="true" v-model="v$.paymentVoucher.contactAccountId.$model" :paymentTerm="'Credit'"  @update:modelValue="setValue"/>
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{ formName }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error }"
                                                v-else-if="formName == 'BankPay'">
                                                    <customersupplieraccountsdropdown :isCustomer="false" v-model="v$.paymentVoucher.contactAccountId.$model" :paymentTerm="'Credit'"
                                                    :formName="'Supplier'" @update:modelValue="RemoveRecord && enableInvoiceDropdown(true)" />
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{ formName }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.contactAccountId.$error }"
                                                v-else>
                                                <accountdropdown 
                                                    :formNames="formName"
                                                    v-model="v$.paymentVoucher.contactAccountId.$model"
                                                    :key="renderAccountDropdown" @update:modelValue="enableInvoiceDropdown(true)"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.contactAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.contactAccountId.required">{{ formName }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label
                                                v-if="(formName == 'BankPay' || formName == 'AdvancePurchase') || formName == 'CashPay'">
                                                {{ $t('AddPaymentVoucher.SupplierRunningBalance') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label
                                                v-else-if="formName == 'CashReceipt' || formName == 'BankReceipt' || formName == 'AdvanceReceipt'">
                                                {{ $t('AddPaymentVoucher.CustomerRunningBalance') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label v-else>
                                                Running Balance:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <div class="form-group">

                                                <input disabled v-model="runningBalance" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label>{{ $t('Date of Payment') }} :<span class="text-danger"> *</span></label>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.paymentDate.$error }">
                                                <datepicker v-model="v$.paymentVoucher.paymentDate.$model"></datepicker>
                                                <span v-if="v$.paymentVoucher.paymentDate.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.paymentDate.required">{{ formName }} {{
                                                        $t('AddPaymentVoucher.DateRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName == 'PettyCash'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentMode') }}:
                                                <span class="text-danger"> *</span>
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())" disabled
                                                    v-model="paymentVoucher.paymentMode"
                                                    @update:modelValue="GetAccount(paymentVoucher.paymentMode)"
                                                    :options="['Cash', 'Bank']" :show-labels="false"
                                                    placeholder="Select Mode Of Payment">
                                                </multiselect>
                                                <multiselect v-else disabled v-model="paymentVoucher.paymentMode"
                                                    @update:modelValue="GetAccount(paymentVoucher.paymentMode)"
                                                    :options="['السيولة النقدية', 'مصرف']" :show-labels="false"
                                                    placeholder="Select Mode Of Payment">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-else-if="formName == 'AdvanceReceipt'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentMode') }}:
                                                <span class="text-danger"> *</span>
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                                    :disabled="isTemporaryCashIssue" v-model="paymentVoucher.paymentMode"
                                                    @update:modelValue="GetAccount(paymentVoucher.paymentMode)"
                                                    :options="['Cash', 'Bank']" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else :disabled="isTemporaryCashIssue"
                                                    v-model="paymentVoucher.paymentMode"
                                                    @update:modelValue="GetAccount(paymentVoucher.paymentMode)"
                                                    :options="['السيولة النقدية', 'مصرف']" :show-labels="false"
                                                    v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-else>
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentMode') }}:
                                                <span class="text-danger"> *</span>
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                                    :disabled="isTemporaryCashIssue" v-model="paymentVoucher.paymentMode"
                                                    @update:modelValue="GetAccount(paymentVoucher.paymentMode)"
                                                    :options="paymentModeOpt" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else :disabled="isTemporaryCashIssue"
                                                    v-model="paymentVoucher.paymentMode"
                                                    @update:modelValue="GetAccount(paymentVoucher.paymentMode)"
                                                    :options="paymentModeOptAr" :show-labels="false"
                                                    v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentType') }}:
                                                <span class="text-danger"
                                                    v-if="paymentVoucher.paymentMode == 'Cash' || paymentVoucher.paymentMode == 'السيولة النقدية' || paymentVoucher.paymentMode == 'Advance' || paymentVoucher.paymentMode == 'يتقدم'"></span>
                                                <span class="text-danger" v-else> *</span>
                                            </label>

                                            <div class="form-group"
                                                v-if="paymentVoucher.paymentMode == 'Cash' || paymentVoucher.paymentMode == 'السيولة النقدية' || paymentVoucher.paymentMode == 'Advance' || paymentVoucher.paymentMode == 'يتقدم'">

                                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                                    :key="optRender" disabled v-model="paymentVoucher.paymentMethod"
                                                    :options="paymentTypeOptions" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="paymentVoucher.paymentMethod" :key="optRender"
                                                    disabled :options="paymentTypeOptionsAr" :show-labels="false"
                                                    v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                            <div class="form-group" v-else>

                                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                                    :key="optRender" v-model="paymentVoucher.paymentMethod"
                                                    :options="paymentTypeOptions" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="paymentVoucher.paymentMethod" :key="optRender"
                                                    :options="paymentTypeOptionsAr" :show-labels="false"
                                                    v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="formName == 'AdvanceReceipt' && (isValid('PremiumARAdvancePayment') || isValid('StandardARAdvancePayment'))">
                                            <label>
                                                Document Type:
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-model="paymentVoucher.documentType"
                                                    @update:modelValue="paymentVoucher.documentId = ''"
                                                    :options="['Proforma Invoice', 'Sale Order', 'Quotation']"
                                                    :show-labels="false" placeholder="Select Type">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-else-if="formName == 'AdvancePurchase' && (isValid('PremiumARAdvancePayment') || isValid('StandardARAdvancePayment'))">
                                            <label>
                                                Document Type:
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-model="paymentVoucher.documentType"
                                                    @update:modelValue="GetValue"
                                                    :options="['Supplier Quotation', 'Purchase Order']" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.documentType == 'Purchase Order' && formName == 'AdvancePurchase' && (isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment')) && paymentVoucher.contactAccountId != ''">
                                            <label>
                                                Purchase Order :
                                            </label>

                                            <div class="form-group">
                                                <purchase-order-dropdown :isPartially="true" :key="randerRecord"
                                                    ref="purchaseDropDown" 
                                                    v-model="paymentVoucher.documentId"
                                                    :contactAccountId='paymentVoucher.contactAccountId'
                                                    :modelValue="paymentVoucher.documentId" @update:modelValue="GetValue('purchaseorder')" />


                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-else-if="paymentVoucher.documentType == 'Supplier Quotation' && formName == 'AdvancePurchase' && (isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment')) && paymentVoucher.contactAccountId != ''">
                                            <label>
                                                Supplier Quotation :
                                            </label>

                                            <div class="form-group">
                                                <purchase-order-dropdown :isPartially="true" :key="randerRecord"
                                                    ref="purchaseDropDown"
                                                    v-model="paymentVoucher.documentId"
                                                    :contactAccountId='paymentVoucher.contactAccountId'
                                                    :supplierQuotation="'supplierQuotation'"
                                                    :modelValue="paymentVoucher.documentId" @update:modelValue="GetValue('supplierQuotation')" />


                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.documentType == 'Proforma Invoice' && formName == 'AdvanceReceipt' && (isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment')) && paymentVoucher.contactAccountId != ''">
                                            <label>
                                                Proforma Invoice :
                                            </label>

                                            <div class="form-group">
                                                <sale-invoice-dropdown :isPartially="true" ref="saleInvoiceDropdown"
                                                    v-model="paymentVoucher.documentId"
                                                    :contactId='paymentVoucher.contactAccountId'
                                                    :modelValue="paymentVoucher.documentId" :status="'ProformaInvoice'"
                                                    :isCredit="'ProformaInvoice'" :isService="isService" :key="proformaRender" @update:modelValue="GetValue('saleinvoice')" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.documentType == 'Sale Order' && formName == 'AdvanceReceipt' && (isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment')) && paymentVoucher.contactAccountId != ''">
                                            <label>
                                                Sale Order :
                                            </label>

                                            <div class="form-group">
                                                <saleorderdropdown :isPartially="true" ref="saleOrderDropdown"
                                                    v-model="paymentVoucher.documentId"
                                                    :contactAccountId='paymentVoucher.contactAccountId'
                                                    :modelValue="paymentVoucher.documentId" :isservice="isService"  @update:modelValue="GetValue('saleorder')" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.documentType == 'Quotation' && formName == 'AdvanceReceipt' && (isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment')) && paymentVoucher.contactAccountId != ''">
                                            <label>
                                                Quotation :
                                            </label>

                                            <div class="form-group">
                                                <quotationdropdown :isPartially="true" ref="quotationDropdown"
                                                    v-model="paymentVoucher.documentId"
                                                    :contactAccountId='paymentVoucher.contactAccountId'
                                                    :modelValue="paymentVoucher.documentId" :isservice="isService" @update:modelValue="GetValue('quotation')"/>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.documentType == 'Delivery Challan' && formName == 'AdvanceReceipt' && isValid('StandardARAdvancePayment')">
                                            <label>
                                                Delivery Note :
                                            </label>

                                            <div class="form-group">
                                                <DeliveryChallanDropdown v-model="paymentVoucher.documentId"
                                                    :modelValue="paymentVoucher.documentId" :isservice="isService" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="(isValid('StandardAdvancePayment') && formName == 'BankReceipt') || (isValid('PremiumAdvancePayment') && formName == 'BankReceipt') || formName == 'PettyCash'">
                                            <label
                                                v-if="(isValid('StandardAdvancePayment') || isValid('SimpleAdvancePayment') || isValid('PremiumAdvancePayment') || isValid('MultipleAdvancePayment')) && formName == 'BankReceipt'">
                                                {{ $t('Payment Against Invoice') }} :

                                            </label>
                                            <label v-else>
                                                {{ $t('AddPaymentVoucher.SaleInvoice') }} :

                                            </label>
                                            <div class="form-group">
                                                <sale-invoice-dropdown ref="saleInvoiceDropdown"
                                                    v-model="paymentVoucher.saleInvoice" :isPartially="true"
                                                    v-bind:isExpense="true" 
                                                    :key="saleInvoiceRander" v-bind:isCredit="true"
                                                    :contactId="paymentVoucher.contactAccountId" :isDisabled="isShow"
                                                    :status="'Credit'" :isService="isService" @update:modelValue="getSaleNetAmount" />

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="(isValid('StandardSupplierAdvancePayment') && formName == 'BankPay') || (isValid('PremiumSupplierAdvancePayment') && formName == 'BankPay')">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PurchaseInvoice') }}
                                            </label>
                                            <div class="form-group">
                                                <purchaseinvoicedropdown 
                                                    ref="purchaseInvoiceDropdown"
                                                    :modelValue="paymentVoucher.purchaseInvoice"
                                                    v-model="paymentVoucher.purchaseInvoice" v-bind:isExpense="true"
                                                    :key="purchaseInvoiceRander"
                                                    :supplierAccountId="paymentVoucher.contactAccountId"
                                                    :isDisabled="isShow" :isService="isService"  @update:modelValue="getPurchaseNetAmount"/>

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="((isValid('StandardAdvancePayment') || isValid('PremiumAdvancePayment')) && formName == 'BankReceipt') || ((isValid('StandardSupplierAdvancePayment') || isValid('PremiumSupplierAdvancePayment')) && formName == 'BankPay') || ((isValid('PremiumARAdvancePayment') || isValid('StandardARAdvancePayment')) && (formName == 'AdvancePurchase' || formName == 'AdvanceReceipt'))">
                                            <label>
                                                {{ $t('Invoice Amount') }}:
                                            </label>
                                            <div class="form-group">
                                                <my-currency-input v-model="paymentVoucher.invoiceAmount"
                                                    :key="currencyRender" :disable="true"></my-currency-input>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="((isValid('StandardAdvancePayment') || isValid('PremiumAdvancePayment')) && formName == 'BankReceipt') || ((isValid('StandardSupplierAdvancePayment') || isValid('PremiumSupplierAdvancePayment')) && formName == 'BankPay') || ((isValid('PremiumARAdvancePayment') || isValid('StandardARAdvancePayment')) && (formName == 'AdvancePurchase' || formName == 'AdvanceReceipt'))">
                                            <label>
                                                {{ $t('Remaining Balance') }} :
                                            </label>
                                            <div class="form-group">
                                                <my-currency-input v-model="paymentVoucher.remainingBalance"
                                                    :key="currencyRender" :disable="true"></my-currency-input>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.paymentMode != 'Advance' || paymentVoucher.paymentMode == 'يتقدم'">
                                            <label
                                                v-if="paymentVoucher.paymentMode == 'Cash' || paymentVoucher.paymentMode == 'السيولة النقدية'">
                                                {{ $t('AddPaymentVoucher.CashAccount') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label
                                                v-else-if="paymentVoucher.paymentMode == 'Bank' || paymentVoucher.paymentMode == 'مصرف'">
                                                {{ $t('AddPaymentVoucher.BankAccount') }}
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label
                                                v-else-if="paymentVoucher.paymentMode == 'Advance' || paymentVoucher.paymentMode == 'يتقدم'">
                                                {{ $t('AddPaymentVoucher.AdvanceAccount') }} :
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <label v-else>
                                                {{ $t('AddPaymentVoucher.BankAccount') }}:
                                                <span class="text-danger"> *</span>

                                            </label>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error }"
                                                v-if="paymentVoucher.paymentMode == 'Cash' || paymentVoucher.paymentMode == 'السيولة النقدية'"
                                                v-bind:key="randerAccount">
                                                <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model"
                                                    :formName="CashPay"
                                                    :isPurchase="formName == 'BankReceipt' ? false : true"
                                                    :disabled="isTemporaryCashIssue"  @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)"></accountdropdown>
                                                <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{ formName
                                                    }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error }"
                                                v-else-if="paymentVoucher.paymentMode == 'Bank' || paymentVoucher.paymentMode == 'مصرف'"
                                                v-bind:key="randerAccount">
                                                <accountdropdown  v-model="v$.paymentVoucher.bankCashAccountId.$model" :formName="BankPay"
                                                    :isPurchase="formName == 'BankReceipt' ? false : true"  @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)">
                                                </accountdropdown>
                                                <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{ formName
                                                    }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                            <div class="form-group"
                                                v-bind:class="{ 'has-danger': v$.paymentVoucher.bankCashAccountId.$error }"
                                                v-else>
                                                <accountdropdown v-model="v$.paymentVoucher.bankCashAccountId.$model"
                                                    :formName="BankPay"
                                                    :isPurchase="formName == 'BankReceipt' ? false : true"  @update:modelValue="GetBankOpeningBalance(paymentVoucher.bankCashAccountId)"> 
                                                </accountdropdown>
                                                <span v-if="v$.paymentVoucher.bankCashAccountId.$error" class="error">
                                                    <span v-if="!v$.paymentVoucher.bankCashAccountId.required">{{ formName
                                                    }}
                                                        {{ $t('AddPaymentVoucher.AccountRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <label
                                                v-if="paymentVoucher.paymentMode == 'Cash' || paymentVoucher.paymentMode == 'السيولة النقدية'">
                                                {{ $t('AddPaymentVoucher.CashAccountRunningBalance') }}:

                                            </label>
                                            <label
                                                v-else-if="paymentVoucher.paymentMode == 'Bank' || paymentVoucher.paymentMode == 'مصرف'">
                                                {{ $t('AddPaymentVoucher.BankAccountRunningBalance') }}

                                            </label>
                                            <label
                                                v-else-if="paymentVoucher.paymentMode == 'Advance' || paymentVoucher.paymentMode == 'يتقدم'">
                                                {{ $t('AddPaymentVoucher.AdvanceAccountRunningBalance') }} :

                                            </label>
                                            <label v-else-if="isValid('PremiumAdvancePayment')">
                                                {{ $t('Advance Account Running Balance') }}
                                            </label>
                                            <label v-else>
                                                {{ $t('AddPaymentVoucher.BankAccountRunningBalance') }}
                                            </label>
                                            <div class="form-group">
                                                <input disabled v-model="runningBalanceCashBank" class="form-control"
                                                    type="text" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.paymentMode == 'Bank' || paymentVoucher.paymentMode == 'مصرف'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.TransactionID') }}:
                                            </label>
                                            <div class="form-group">
                                                <input v-model="paymentVoucher.transactionId" class="form-control"
                                                    type="text" />
                                            </div>

                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="((isValid('SimpleAdvancePayment') || isValid('StandardAdvancePayment') || isValid('PremiumAdvancePayment')) && formName == 'BankReceipt') || ((isValid('SimpleSupplierAdvancePayment') || isValid('StandardSupplierAdvancePayment') || isValid('PremiumSupplierAdvancePayment')) && formName == 'BankPay') || formName == 'PettyCash' || formName == 'AdvanceReceipt' || formName == 'AdvancePurchase'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.Amount') }} <span
                                                    v-if="((isValid('StandardAdvancePayment') || isValid('SimpleAdvancePayment') || isValid('PremiumAdvancePayment')) && formName == 'BankReceipt') || ((isValid('SimpleSupplierAdvancePayment') || isValid('StandardSupplierAdvancePayment') || isValid('PremiumSupplierAdvancePayment')) && formName == 'BankPay')">
                                                    Received</span>:
                                                <span class="text-danger"> *</span>
                                            </label>
                                            <div class="form-group">
                                                <my-currency-input v-model="paymentVoucher.amount"
                                                    @update:modelValue="zeroPrice(paymentVoucher.amount)"></my-currency-input>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6"
                                            v-if="isValid('MultipleSupplierAdvancePayment') && formName == 'BankPay'">
                                            <br>
                                            <button id="bEdit" type="button"
                                                class="btn btn-sm btn-soft-primary btn-circle me-1"
                                                v-bind:disabled="(!isMultiPayment) && (paymentVoucher.contactAccountId == null || paymentVoucher.contactAccountId == '')"
                                                v-on:click="AddMultiPayment()">Select Purchases</button>
                                            &nbsp; &nbsp; &nbsp; <span class="fw-bold">Total Purchases
                                            </span>:({{ paymentVoucher.paymentVoucherItems.length }}) <span
                                                class="fw-bold">Amount :</span> ({{ paymentVoucher.amount }})

                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="!isValid('MultiplePayment') && (formName == 'CashPay')">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PurchaseInvoice') }}
                                            </label>
                                            <div class="form-group">
                                                <purchaseinvoicedropdown 
                                                    ref="purchaseInvoiceDropdown"
                                                    :modelValue="paymentVoucher.purchaseInvoice"
                                                    v-model="paymentVoucher.purchaseInvoice" v-bind:isExpense="true"
                                                    :key="purchaseInvoiceRander"
                                                    :supplierAccountId="paymentVoucher.contactAccountId"
                                                    :isDisabled="isShow" :isService="isService" @update:modelValue="getPurchaseNetAmount" />

                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="paymentVoucher.paymentMethod == 'Cheque' || paymentVoucher.paymentMethod == 'التحقق من'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.ChequeNumber') }}
                                            </label>
                                            <div class="form-group">
                                                <input v-model="paymentVoucher.chequeNumber" class="form-control"
                                                    type="text" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName == 'PettyCash'">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PattyCashType') }}
                                            </label>
                                            <div class="form-group">
                                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                                    v-model="paymentVoucher.pettyCash"
                                                    :options="['Temporary', 'General', 'Advance']" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="paymentVoucher.pettyCash"
                                                    :options="['مؤقت', 'عام', 'تقدم']" :show-labels="false"
                                                    v-bind:placeholder="$t('AddPaymentVoucher.SelectOption')">
                                                </multiselect>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6"
                                            v-if="isValid('MultipleAdvancePayment') && formName == 'BankReceipt'">
                                            <button id="bEdit" type="button"
                                                class="btn btn-sm btn-soft-primary btn-circle me-1"
                                                v-bind:disabled="(!isMultiPayment) && (paymentVoucher.contactAccountId == null || paymentVoucher.contactAccountId == '')"
                                                v-on:click="AddMultiPayment()">Select Invoices</button><br>
                                            <span v-for="(payment, index) in  paymentVoucher.paymentVoucherItems"
                                                v-bind:key="payment.saleInvoiceId">
                                                <span class="fw-bold">{{ index + 1 }}</span>-&nbsp;&nbsp;<span
                                                    class="fw-bold">{{ payment.description }}&nbsp;&nbsp;{{ currency
                                                    }}&nbsp;
                                                    &nbsp;({{
                                                        parseFloat(payment.payAmount).toFixed(3).slice(0,
                                                            -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                "$1,") }})</span><br>
                                            </span>
                                            <span class="fw-bold">Total : ( {{
                                                parseFloat(paymentVoucher.amount).toFixed(3).slice(0,
                                                    -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                        "$1,") }})</span>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 mt-4 mb-5">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                            <div class="form-group pe-3">
                                                                <label> {{ $t('AddPaymentVoucher.Narration') }} /
                                                                    {{ $t('AddPaymentVoucher.Remarks') }}</label>
                                                                <textarea v-model="paymentVoucher.narration" rows="3"
                                                                    class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class="form-group ps-3"
                                                                v-if="(isValid('CanAddPettyCashAttachment') && formName == 'PettyCash') || (isValid('CanAddCPRAttachment') && formName == 'BankReceipt') || (isValid('CanAddAttachment') && (formName == 'BankPay' || formName == 'AdvancePurchase'))">
                                                                <div class="font-xs mb-1">{{
                                                                    $t('AddPaymentVoucher.Attachment') }}</div>
                                                                <button v-on:click="Attachment()" type="button"
                                                                    class="btn btn-light btn-square btn-outline-dashed mb-1"><i
                                                                        class="fas fa-cloud-upload-alt"></i> {{
                                                                            $t('AddSaleOrder.Attachment') }} </button>
                                                                <div>
                                                                    <small class="text-muted">
                                                                        {{ $t('AddPaymentVoucher.FileSize') }}
                                                                    </small>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12" v-if="formName == 'BankReceipt'">
                                    <h6 class="font-20"> Payment Refund</h6>
                                    <table class="table table-hover">
                                        <thead class="thead-light ">
                                            <tr>
                                                <th>Debtor</th>
                                                <th>Creditor</th>
                                                <th>Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="item in paymentVoucher.paymentRefunds" :key="item.id">
                                                <td>{{ item.bankName }}</td>
                                                <td>{{ item.contactName }}</td>
                                                <td>{{ currency }} {{ item.amount }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 invoice-btn-fixed-bottom">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="button-items"
                                        v-if="paymentVoucher.id == '00000000-0000-0000-0000-000000000000'">
                                        <button type="button" class="btn btn-outline-primary  "
                                            v-bind:disabled="v$.paymentVoucher.$invalid || (isTemporaryCashIssue ? (temporaryCashIssue < paymentVoucher.amount) : false)"
                                            v-if="(isValid('CanAddPettyCash') && formName == 'PettyCash') || (isValid('CanAddCPR') && formName == 'BankReceipt') || (isValid('CanAddSPR') && (formName == 'BankPay' || formName == 'AdvancePurchase')) || ((isValid('CanDraftSPR') || isValid('CanAddSPR')) && formName == 'AdvanceReceipt')"
                                            v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{
                                                $t('AddPaymentVoucher.SaveandPost') }}</button>
                                        <button type="button" class="btn btn-outline-primary   "
                                            v-bind:disabled="v$.paymentVoucher.$invalid"
                                            v-if="((isValid('CanDraftPettyCash') && formName == 'PettyCash') || (isValid('CanDraftCPR') && formName == 'BankReceipt') || (isValid('CanDraftSPR') && (formName == 'BankPay' || formName == 'AdvancePurchase'))) && !isTemporaryCashIssue || ((isValid('CanDraftSPR') || isValid('CanAddSPR')) && formName == 'AdvanceReceipt')"
                                            v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i> {{
                                                $t('AddPaymentVoucher.SaveasDraft') }}</button>
                                        <button class="btn btn-danger  " v-on:click="onCancel"> {{
                                            $t('AddPaymentVoucher.Cancel') }}</button>

                                    </div>
                                    <div v-else-if="paymentVoucher.isView" class="button-items">

                                        <button type="button" class="btn btn-outline-primary mx-2  "
                                            v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> Update
                                            Attachment </button>
                                        <button class="btn btn-danger  " v-on:click="onCancel"> {{
                                            $t('AddPaymentVoucher.Cancel') }}</button>

                                    </div>
                                    <div v-else class="button-items">

                                        <div>
                                            <button type="button" class="btn btn-outline-primary   "
                                                v-if="(isValid('CanRejectPettyCash') && formName == 'PettyCash') || (isValid('CanRejectCPR') && formName == 'BankReceipt') || (isValid('CanRejectSPR') && (formName == 'BankPay' || formName == 'AdvancePurchase')) || formName == 'AdvanceReceipt'"
                                                v-on:click="SaveVoucher('Rejected')"><i class="far fa-save"></i> {{
                                                    $t('AddPaymentVoucher.SaveasReject') }}</button>
                                            <button type="button" class="btn btn-outline-primary   "
                                                v-if="(isValid('CanEditPettyCash') && formName == 'PettyCash') || (isValid('CanEditCPR') && formName == 'BankReceipt') || (isValid('CanEditSPR') && (formName == 'BankPay' || formName == 'AdvancePurchase')) || formName == 'AdvanceReceipt'"
                                                v-on:click="SaveVoucher('Approved')"><i class="far fa-save"></i> {{
                                                    $t('AddPaymentVoucher.UpdateandPost') }} </button>
                                            <button type="button" class="btn btn-outline-primary   "
                                                v-if="(isValid('CanDraftPettyCash') && formName == 'PettyCash') || (isValid('CanDraftCPR') && formName == 'BankReceipt') || (isValid('CanDraftSPR') && (formName == 'BankPay' || formName == 'AdvancePurchase')) || formName == 'AdvanceReceipt'"
                                                v-on:click="SaveVoucher('Draft')"><i class="far fa-save"></i> {{
                                                    $t('AddPaymentVoucher.UpdateasDraft') }}</button>
                                            <button class="btn btn-danger  " v-on:click="onCancel"> {{
                                                $t('AddPaymentVoucher.Cancel') }}</button>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <bulk-attachment :attachmentList="paymentVoucher.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        <PaymentVoucherItem :isDisable="false" :newpaymentVoucherItems="paymentVoucherItems"
            :paymentVoucheramount="paymentVoucher.amount" :show="isPayment" v-if="isPayment" @close="PaymentSave"
            @updatedailyExpenseRows="updatedailyExpenseRows" :isSaleInvoice="isSaleInvoice"
            :isPurchaseInvoice="isPurchaseInvoice" :paymentVoucher="paymentVoucher" />
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin';
import Multiselect from 'vue-multiselect';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
import {
    minValue,
    required
} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';

import moment from "moment";
export default {
    mixins: [clickMixin],
    components: {
        Multiselect,
        Loading
    },
    props: ['formName'],
    setup() {
        return { v$: useVuelidate() }
    },
    data: function () {
        return {
            proformaRender:0,
            temporaryCashIssue: 0,
            randerRecord: 0,
            isTemporaryCashIssue: false,
            isAging: false,
            currency: '',
            multipleDocument: '',
            isService: false,
            ispayable: true,
            render: 0,
            saleInvoiceRander: 0,
            purchaseInvoiceRander: 0,
            isShow: true,
            isMultiPayment: false,
            isPayment: false,
            attachment: false,
            paymentVoucherItems: [],
            paymentVoucher: {
                id: '00000000-0000-0000-0000-000000000000',
                date: '',
                paymentDate: '',
                isView: false,
                voucherNumber: '',
                documentType: '',
                transactionId: '',
                chequeNumber: '',
                narration: '',
                paymentVoucherType: '',
                amount: 0,
                approvalStatus: 'Draft',
                purchaseInvoice: '',
                saleInvoice: '',
                bankCashAccountId: '',
                contactAccountId: '',
                paymentMode: '',
                natureOfPayment: '',
                paymentMethod: '',
                userName: '',
                temporaryCashIssueId: '',
                attachmentList: [],
                MultipleDocument: [],
                paymentVoucherItems: [],
                branchId: '',
                remainingBalance: 0,
                invoiceAmount: 0,
            },
            paymentTerm: '',
            loading: false,
            type: '',
            isBank: true,
            voucherNumberRander: 0,
            language: 'Nothing',
            CashPay: 'CashPay',
            BankPay: 'BankPay',
            randerAccount: 0,
            disable: false,
            show: false,
            runningBalance: 0,
            runningBalanceCashBank: 0,

            //Payment Type Options
            paymentTypeOptions: [],
            paymentTypeOptionsAr: [],

            //Payment Mode Options
            paymentModeOpt: [],
            paymentModeOptAr: [],

            optRender: 0,
            currencyRender: 0,
            premiumAdvance: false,
            isSaleInvoice: false,
            renderAccountDropdown: 0,
            isForm: false,
            isPurchaseInvoice: false,
        }
    },

    validations() {
        return {
            paymentVoucher: {
                voucherNumber: {
                    required
                },
                paymentDate: {
                    required
                },
                bankCashAccountId: {
                    required
                },
                contactAccountId: {
                    required
                },
                amount: {
                    minValue: minValue(0.01)
                }
            }
        }
    },
    watch: {
        formName: function () {
            if (this.formName == 'BankPay' || this.formName == 'AdvancePurchase') {

                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.$route.query.data != undefined) {
                    this.attachment = true;
                    this.paymentVoucher = this.$route.query.data;
                    this.paymentVoucher.paymentVoucherType = 'BankPay';
                    this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                    this.enableInvoiceDropdown(false);
                    this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'يتقدم';
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        } else if (this.paymentVoucher.paymentMethod == 4) {
                            this.paymentVoucher.paymentMethod = 'Debit Card';
                        } else if (this.paymentVoucher.paymentMethod == 5) {
                            this.paymentVoucher.paymentMethod = 'Credit Card';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }
                }
            }
            if (this.formName == 'BankReceipt' || this.formName == 'AdvanceReceipt') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.$route.query.data != undefined) {
                    this.attachment = true;
                    this.paymentVoucher = this.$route.query.data.message;
                    this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                    this.enableInvoiceDropdown(false);
                    if (this.formName == 'AdvanceReceipt') {
                        this.paymentVoucher.paymentVoucherType = 'AdvanceReceipt';

                    }
                    else {
                        this.paymentVoucher.paymentVoucherType = 'BankReceipt';

                    }
                    this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'يتقدم';
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        } else if (this.paymentVoucher.paymentMethod == 4) {
                            this.paymentVoucher.paymentMethod = 'Debit Card';
                        } else if (this.paymentVoucher.paymentMethod == 5) {
                            this.paymentVoucher.paymentMethod = 'Credit Card';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }
                }
            }
            if (this.formName == 'PettyCash') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                }
                if (this.$route.query.data != undefined) {
                    this.paymentVoucher = this.$route.query.data.message;
                    this.paymentVoucher.paymentVoucherType = 'PettyCash';
                    this.paymentVoucherDetails = this.$route.query.data.message.paymentVoucherDetails;
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.pettyCash == 1) {
                            this.paymentVoucher.pettyCash = 'مؤقت';
                        }
                        if (this.paymentVoucher.pettyCash == 2) {
                            this.paymentVoucher.pettyCash = 'عام';
                        }
                        if (this.paymentVoucher.pettyCash == 3) {
                            this.paymentVoucher.pettyCash = 'تقدم';
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.pettyCash == 1) {
                            this.paymentVoucher.pettyCash = 'Temporary';
                        }
                        if (this.paymentVoucher.pettyCash == 2) {
                            this.paymentVoucher.pettyCash = 'General';
                        }
                        if (this.paymentVoucher.pettyCash == 3) {
                            this.paymentVoucher.pettyCash = 'Advance';
                        }

                    }
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'يتقدم';
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        } else if (this.paymentVoucher.paymentMethod == 4) {
                            this.paymentVoucher.paymentMethod = 'Debit Card';
                        } else if (this.paymentVoucher.paymentMethod == 5) {
                            this.paymentVoucher.paymentMethod = 'Credit Card';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }

                }

            }
        },
    },
    methods: {
        setValue(){
       
            // this.paymentVoucher.contactAccountId=val;
            this.enableInvoiceDropdown(true);
        },
            updatedailyExpenseRows: function (detail, amount) {
                this.paymentVoucher.paymentVoucherItems = [];
                this.paymentVoucher.paymentVoucherItems = detail;
                this.paymentVoucher.amount = amount;
                if (!this.isAging) {
                    this.paymentVoucherItems = detail;

                }

                this.isPayment = false;

            },
            RemoveRecord: function (x) {
                this.paymentVoucher.amount = 0
                console.log(x);
                this.paymentVoucher.documentId = '';
                this.paymentVoucher.documentType = '';

            },
            GetValue: function (value) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (value == 'saleorder') {
                    let totalAmount = 0;
                    if (this.isForm) {
                        totalAmount = this.paymentVoucher.amount;
                    }
                    else {
                        totalAmount = this.$refs.saleInvoiceDropdown.GetAmountOfSelected();
                    }

                    this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.documentId,
                        {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
                        .then(function (response) {
                            if (response.data != null) {
                                root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                                root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                                root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                            }
                        });

                }
                else if (value == 'quotation') {
                    let totalAmount = 0;
                    if (this.isForm) {
                        totalAmount = this.paymentVoucher.amount;
                    }
                    else {
                        totalAmount = this.$refs.saleInvoiceDropdown.GetAmountOfSelected();
                    }

                    this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.documentId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                            root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                            root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                        }
                    });
                }
                else if (value == 'saleinvoice') {
                    var totalAmount = 0;
                    if (this.isForm) {
                        totalAmount = this.paymentVoucher.amount;
                    }
                    else {
                        totalAmount = this.$refs.saleInvoiceDropdown.GetAmountOfSelected();
                    }

                    this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.documentId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                            root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                            root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                        }
                    });

                }
                else if (value == 'purchaseorder') {
                    let totalAmount = 0;
                    if (this.isForm) {
                        totalAmount = this.paymentVoucher.amount;
                    }
                    else {
                        totalAmount = this.$refs.purchaseDropDown.GetAmountOfSelected();
                    }

                    this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.documentId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                            root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                            root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                        }
                    });
                }
                else if (value == 'supplierQuotation') {
                    let totalAmount = 0;
                    if (this.isForm) {
                        totalAmount = this.paymentVoucher.amount;
                    }
                    else {
                        totalAmount = this.$refs.purchaseDropDown.GetAmountOfSelected();
                    }

                    this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.documentId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                            root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                            root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                        }
                    });
                }
                else {
                    this.paymentVoucher.amount = 0;
                    this.paymentVoucher.documentId = '';

                    if (this.$refs.purchaseDropDown != undefined && this.$refs.purchaseDropDown != null) {
                        this.$refs.purchaseDropDown.Clear();
                        if (this.paymentVoucher.documentType == 'Purchase Order' || this.paymentVoucher.documentType == 'Supplier Quotation') {
                            this.randerRecord++;
                        }
                    }

                }

            },
            Attachment: function () {
                this.show = true;
            },
            attachmentSave: function (attachment) {
                this.paymentVoucher.attachmentList = attachment;
                this.show = false;
            },
            AddMultiPayment: function () {

                this.isPayment = true;
            },
            PaymentSave: function () {
                this.isPayment = false;
            },
            GetAccount: function (x) {

                if (x == 'السيولة النقدية' || x == 'Bank') {
                    this.randerAccount++;

                } else if (x == 'مصرف' || x == 'Cash') {
                    this.randerAccount++;
                } else if (x == 'يتقدم' || x == 'Advance') {
                    this.randerAccount++;
                    this.getAdvanceAmount();
                }
                this.GetOptions();
            },
            DownloadAttachment(path) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFile?filePath=' + path, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    },
                    responseType: 'blob'
                })
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
                root.$https.post('/Company/UploadFilesAsync', fileData, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
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

                } else {
                    this.disable = false;
                }

            },
            GetBankOpeningBalance: function (id) {

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (id == '' || id == undefined || id == null) {
                    return;
                }
                var root = this
                this.$https.get('/Contact/GetCustomerRunningBalance?id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.runningBalanceCashBank = parseFloat(response.data) >= 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                    }
                });
            },

            UpdateStatus: function (status) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/PaymentVoucher/UpdateStatusPaymentVoucher?id=' + this.paymentVoucher.id + '&approvalStatus=' + status, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {

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

                    } else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
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
                }).catch(error => {

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
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var root = this;
                var totalAmount = 0;
                if (root.isSaleInvoice) {
                    totalAmount = root.paymentVoucher.amount;
                }
                else {
                    totalAmount = root.$refs.saleInvoiceDropdown.GetAmountOfSelected();
                }
                // var totalAmount = root.$refs.saleInvoiceDropdown.GetAmountOfSelected();
                this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.saleInvoice, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                        root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                        root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                    }
                });

            },
            getAdvanceAmount: function () {
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var root = this
                this.$https.get('/PaymentVoucher/GetAdvanceBalance?contactId=' + root.paymentVoucher.contactAccountId + '&branchId=' + localStorage.getItem('BranchId') + '&formName=' + this.formName, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.runningBalanceCashBank = parseFloat(response.data.advanceBalance).toFixed(2);

                        root.paymentVoucher.bankCashAccountId = response.data.accountId;
                        root.premiumAdvance = true;
                    }
                });

            },
            getPurchaseNetAmount: function () {
                var root = this;
                 //eslint-disable-line
                if(root.paymentVoucher.purchaseInvoice == null || root.paymentVoucher.purchaseInvoice == '')
                {
                    return
                }

                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }
               

                var totalAmount = 0;
                if (root.isPurchaseInvoice) {
                    totalAmount = root.paymentVoucher.amount;
                }
                else {
                    totalAmount = root.$refs.purchaseInvoiceDropdown.GetAmountOfSelected();
                }
                //var totalAmount = this.$refs.purchaseInvoiceDropdown.GetAmountOfSelected();
                this.$https.get('/Sale/GetRemainingInvoiceAmount?id=' + root.paymentVoucher.purchaseInvoice, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.paymentVoucher.invoiceAmount = parseFloat(totalAmount).toFixed(2);
                        root.paymentVoucher.amount = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                        root.paymentVoucher.remainingBalance = (parseFloat(totalAmount) - parseFloat(response.data)).toFixed(2);
                    }
                });
            },
            getData: function (x) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');
                this.paymentVoucherItems = [];
                if (x == true) {
                    if (this.formName == 'BankPay' || this.formName == 'AdvancePurchase') {

                        this.$https.get('/PurchasePost/PurchasePostList?isDropDown=' + true + '&supplierid=' + '00000000-0000-0000-0000-000000000000' + '&isExpense=' + true + '&supplierAccountId=' + this.paymentVoucher.contactAccountId + '&isService=' + root.isService + '&branchId=' + branchId, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        }).then(function (response) {

                            if (response.data != null) {
                                response.data.results.forEach(function (result) {

                                    if (result != undefined) {
                                        root.paymentVoucherItems.push({
                                            purchaseInvoiceId: result.id,
                                            saleInvoiceId: null,
                                            amount: result.netAmount,
                                            payAmount: 0.00,
                                            isAging: root.isAging,
                                            isActive: root.isAging ? true : false,
                                            total: 0.00,
                                            extraAmount: 0.00,
                                            description: result.registrationNumber,
                                            dueAmount: result.netAmount - result.receivedAmount,
                                            receivedAmount: 0,
                                            partiallyInvoices: result.partiallyInvoices,
                                            date: moment(result.date).format('LLL')
                                        });
                                        root.isMultiPayment = true;

                                    }

                                })

                                

                            }
                        })

                    }
                    else {
                        var url = '';

                        {
                            url = '/Sale/SaleList?status=' + 'Credit' + '&isDropdown=' + true + '&isService=' + root.isService + '&isExpense=' + true + '&contactId=' + this.paymentVoucher.contactAccountId + '&branchId=' + branchId
                        }

                        this.$https.get(url, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        }).then(function (response) {

                            if (response.data != null) {

                                response.data.results.sales.forEach(function (result) {

                                    if (result != undefined) {
                                        root.paymentVoucherItems.push({
                                            saleInvoiceId: result.id,
                                            purchaseInvoiceId: null,
                                            amount: result.netAmount,
                                            payAmount: 0.00,
                                            isAging: root.isAging,
                                            isActive: root.isAging ? true : false,
                                            total: 0.00,
                                            extraAmount: 0.00,
                                            description: result.registrationNumber,
                                            dueAmount: result.netAmount - result.receivedAmount,
                                            receivedAmount: result.receivedAmount,
                                            partiallyInvoices: result.partiallyInvoices,
                                            date: moment(result.date).format('LLL')
                                        });
                                        root.isMultiPayment = true;

                                    }

                                })

                            }

                        });

                    }

                } else {
                    this.paymentVoucherItems = this.paymentVoucher.paymentVoucherItems;

                }

            },
            enableInvoiceDropdown: function (x) {
                
                this.isMultiPayment = false;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var root = this
                if (root.paymentVoucher.contactAccountId == '') {
                    return;
                }

                this.$https.get('/Contact/GetCustomerRunningBalance?id=' + root.paymentVoucher.contactAccountId, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.runningBalance = parseFloat(response.data) >= 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                    }
                }).catch(error => {
                    console.log(error)

                    root.$swal.fire({
                        type: 'error',
                        icon: 'error',
                        title: error.message,
                        text: error.message,
                        confirmButtonClass: "btn btn-danger",
                        showConfirmButton: true,
                        timer: 5000,
                        timerProgressBar: true,
                    });
                    root.loading = false
                })
                    .finally(() => console.log('finally'));

                if (this.isValid('MultiplePayment') || (this.isValid('MultipleAdvancePayment') && this.formName == 'BankReceipt') || (this.isValid('MultipleSupplierAdvancePayment') && this.formName == 'BankPay')) {
                    this.getData(x);

                    // if (this.isValid('UnFollowAging')) {
                    //     this.isAging = false;
                    // }

                } else {
                    if (this.paymentVoucher.saleInvoice === undefined || this.paymentVoucher.saleInvoice === null) {

                        this.paymentVoucher.saleInvoice = '00000000-0000-0000-0000-000000000000';
                    }

                    if (this.paymentVoucher.purchaseInvoice === undefined || this.paymentVoucher.purchaseInvoice === null) {

                        this.paymentVoucher.purchaseInvoice = '00000000-0000-0000-0000-000000000000';
                    }

                    if (this.formName == 'CashReceipt' || this.formName == 'BankReceipt') {
                        this.isShow = false
                        this.isMultiPayment = false
                        this.saleInvoiceRander++;
                    } else if (this.formName == 'BankPay' || this.formName == 'AdvancePurchase' || this.formName == 'CashPay') {
                        this.isShow = false
                        this.isMultiPayment = false
                        this.purchaseInvoiceRander++;
                    }

                }
            },
            GetAutoCodeGenerator: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/PaymentVoucher/AutoGenerateCode?paymentVoucherType=' + value + '&branchId=' + localStorage.getItem('BranchId'), {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {

                        root.paymentVoucher.voucherNumber = response.data;
                        root.voucherNumberRander++;

                    }
                });
            },
            SaveVoucher: function (x) {
                

                localStorage.setItem('active', x);

                if (this.$i18n.locale == 'ar') {
                    //Petty Cash

                    if (this.paymentVoucher.pettyCash == 'مؤقت') {
                        this.paymentVoucher.pettyCash = 'Temporary';
                    }
                    if (this.paymentVoucher.pettyCash == 'عام') {
                        this.paymentVoucher.pettyCash = 'General';
                    }
                    if (this.paymentVoucher.pettyCash == 'تقدم') {
                        this.paymentVoucher.pettyCash = 'Advance';
                    }

                    //Payment Method
                    if (this.paymentVoucher.paymentMethod == 'التحقق من') {
                        this.paymentVoucher.paymentMethod = 'Deposit';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'تحويل') {
                        this.paymentVoucher.paymentMethod = 'Transfer';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'الوديعة') {
                        this.paymentVoucher.paymentMethod = 'Cheque';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'بطاقة') {
                        this.paymentVoucher.paymentMethod = 'Card';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'بطاقة ائتمان') {
                        this.paymentVoucher.paymentMethod = 'DebitCard';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'بطاقة إئتمان') {
                        this.paymentVoucher.paymentMethod = 'CreditCard';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'كثافة العمليات. تحويل') {
                        this.paymentVoucher.paymentMethod = 'IntTransfer';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'سيد بطاقة الائتمان') {
                        this.paymentVoucher.paymentMethod = 'CreditCardMaster';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'تأشيرة بطاقة الائتمان') {
                        this.paymentVoucher.paymentMethod = 'CreditCardVisa';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'أمريكان اكسبريس') {
                        this.paymentVoucher.paymentMethod = 'AmericanExpress';
                    }

                    //Payment Mode
                    if (this.paymentVoucher.paymentMode == 'السيولة النقدية') {
                        this.paymentVoucher.paymentMode = 'Bank';
                    }
                    if (this.paymentVoucher.paymentMode == 'مصرف') {
                        this.paymentVoucher.paymentMode = 'Cash';
                    }
                    if (this.paymentVoucher.paymentMode == 'يتقدم') {
                        this.paymentVoucher.paymentMode = 'Advance';
                    }

                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {

                    //Payment Method

                    if (this.paymentVoucher.paymentMethod == 'Cheque') {
                        this.paymentVoucher.paymentMethod = 'Cheque';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Transfer') {
                        this.paymentVoucher.paymentMethod = 'Transfer';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Deposit') {
                        this.paymentVoucher.paymentMethod = 'Deposit';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Debit Card') {
                        this.paymentVoucher.paymentMethod = 'DebitCard';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Credit Card') {
                        this.paymentVoucher.paymentMethod = 'CreditCard';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Card') {
                        this.paymentVoucher.paymentMethod = 'Card';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Int. Transfer') {
                        this.paymentVoucher.paymentMethod = 'IntTransfer';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Credit Card Master') {
                        this.paymentVoucher.paymentMethod = 'CreditCardMaster';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'Çredit Card Visa') {
                        this.paymentVoucher.paymentMethod = 'CreditCardVisa';
                    }
                    else if (this.paymentVoucher.paymentMethod == 'American Express') {
                        this.paymentVoucher.paymentMethod = 'AmericanExpress';
                    }
                    else {
                        this.paymentVoucher.paymentMethod = 'Default';
                    }

                    //Payment Mode
                    if (this.paymentVoucher.paymentMode == 'Cash') {
                        this.paymentVoucher.paymentMode = 'Cash';
                    }
                    if (this.paymentVoucher.paymentMode == 'Bank') {
                        this.paymentVoucher.paymentMode = 'Bank';
                    }
                    if (this.paymentVoucher.paymentMode == 'Advance') {
                        this.paymentVoucher.paymentMode = 'Advance';
                    }

                }
                if (this.paymentVoucher.paymentMethod != 'Cheque') {
                    this.paymentVoucher.chequeNumber = '';
                }
                var root = this;
                var token = '';
                this.paymentVoucher.approvalStatus = x;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                // this.paymentVoucher.date = this.paymentVoucher.date + " " + moment().format("hh:mm A");
                this.paymentVoucher.branchId = localStorage.getItem('BranchId');


                if (this.multipleDocument != null && this.multipleDocument != '' && this.multipleDocument != undefined) {
                    if (this.multipleDocument.length > 0) {
                        this.paymentVoucher.multipleDocument = this.multipleDocument.map(x => x.id);

                    }
                }

                this.paymentVoucher.amount = parseFloat(this.paymentVoucher.amount);
                this.paymentVoucher.invoiceAmount = parseFloat(this.paymentVoucher.invoiceAmount);
                this.paymentVoucher.receivedAmount = parseFloat(this.paymentVoucher.receivedAmount);
                debugger; //eslint-disable-line

                this.$https.post('/PaymentVoucher/AddPaymentVoucher?premiumAdvance=' + this.premiumAdvance, this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                        }).then(function (result) {
                            if (result) {
                                if (root.isTemporaryCashIssue) {
                                    root.$router.push({
                                        path: '/TemporaryCashIssue',
                                    })
                                } else {
                                    if (root.ispayable) {
                                        root.$router.push({
                                            path: "/paymentVoucherList",
                                            query: {
                                                data: 'PaymentVoucherLists' + root.formName,
                                                formName: root.formName,
                                            }
                                        })
                                        // window.location.href = "/addPaymentVoucher?formName=" + root.formName;
                                    }
                                }
                            }
                        });

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
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    root.$router.push({
                                        path: '/paymentVoucherList',
                                        query: {
                                            data: 'PaymentVoucherLists' + root.formName,
                                            formName: root.formName,
                                        }
                                    })
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
                }).catch(error => {

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
            getpaymentVoucherDetails: function (paymentVoucherItem) {

                this.paymentVoucher = paymentVoucherItem;
            },
            onCancel: function () {
                if (((this.isValid('CanViewPettyCash') || this.isValid('CanDraftPettyCash')) && this.formName == 'PettyCash') || ((this.isValid('CanViewCPR') || this.isValid('CanDraftCPR')) && (this.formName == 'BankReceipt') || this.formName == 'AdvanceReceipt') || ((this.isValid('CanViewSPR') || this.isValid('CanDraftSPR')) && this.formName == 'BankPay' || this.formName == 'AdvancePurchase')) {
                    
                    if(this.$route.query.isFormProforma == 'ServiceProformaInvoice')
                    {
                        this.$router.push({
                            path: '/ServiceProformaInvoice',
                            query: {
                                token_name: "Sales_token",
                                fromDashboard: true,
                                formName : "ServiceProformaInvoice" 
                            }
                        })
                    }
                    else
                    {
                        this.$router.push({
                            path: '/paymentVoucherList',
                            query: {
                                formName: this.formName,
                            }
                        })
                    }
                }
               

            },
            GetOptions: function () {

                if ((this.isValid('SimpleAdvancePayment') && this.formName == 'BankReceipt') || (this.isValid('SimpleSupplierAdvancePayment') && this.formName == 'BankPay')) {
                    this.paymentModeOpt = ['Cash', 'Bank'];
                    this.paymentModeOptAr = ['السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions = ['Cheque', 'Transfer', 'Deposit', 'Card'];
                    this.paymentTypeOptionsAr = ['بطاقة', 'التحقق من', 'تحويل', 'الوديعة'];
                }
                else if ((this.isValid('StandardAdvancePayment') && this.formName == 'BankReceipt') || (this.isValid('StandardSupplierAdvancePayment') && this.formName == 'BankPay')) {
                    this.paymentModeOpt = ['Cash', 'Bank'];
                    this.paymentModeOptAr = ['السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions = ['Cheque', 'Transfer', 'Deposit', 'Debit Card', 'Credit Card'];
                    this.paymentTypeOptionsAr = ['بطاقة إئتمان', 'بطاقة ائتمان', 'التحقق من', 'تحويل', 'الوديعة'];
                }
                else if ((this.isValid('PremiumAdvancePayment') && this.formName == 'BankReceipt') || (this.isValid('PremiumSupplierAdvancePayment') && this.formName == 'BankPay')) {
                    this.paymentModeOpt = ['Cash', 'Bank', 'Advance'];
                    this.paymentModeOptAr = ['يتقدم', 'السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions = ['Cheque', 'Transfer', 'Int. Transfer', 'Deposit', 'Debit Card', 'Credit Card Master', 'Credit Card Visa', 'American Express'];
                    this.paymentTypeOptionsAr = ['أمريكان اكسبريس', 'تأشيرة بطاقة الائتمان', 'سيد بطاقة الائتمان', 'بطاقة ائتمان', 'التحقق من', 'كثافة العمليات. تحويل', 'تحويل', 'الوديعة'];
                }
                else if ((this.isValid('MultipleAdvancePayment') && this.formName == 'BankReceipt') || (this.isValid('MultipleSupplierAdvancePayment') && this.formName == 'BankPay')) {
                    this.paymentModeOpt = ['Cash', 'Bank', 'Advance'];
                    this.paymentModeOptAr = ['يتقدم', 'السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions = ['Cheque', 'Transfer', 'Int. Transfer', 'Deposit', 'Debit Card', 'Credit Card Master', 'Credit Card Visa', 'American Express'];
                    this.paymentTypeOptionsAr = ['أمريكان اكسبريس', 'تأشيرة بطاقة الائتمان', 'سيد بطاقة الائتمان', 'بطاقة ائتمان', 'التحقق من', 'كثافة العمليات. تحويل', 'تحويل', 'الوديعة'];
                }
                else {
                    this.paymentModeOpt = ['Cash', 'Bank'];
                    this.paymentModeOptAr = ['السيولة النقدية', 'مصرف'];


                    this.paymentTypeOptions = ['Cheque', 'Transfer', 'Deposit', 'Debit Card', 'Credit Card'];
                    this.paymentTypeOptionsAr = ['بطاقة إئتمان', 'بطاقة ائتمان', 'التحقق من', 'تحويل', 'الوديعة'];
                }

                this.optRender++;
            },
    },
    created: function () {
            var root = this;
            
            if (root.$route.query.Add == 'false') {
                root.$route.query.data = this.$store.isGetEdit;
            }

            this.paymentVoucher.date = moment().format("DD MMM YYYY hh:mm A");
            this.paymentVoucher.paymentDate = moment().format("DD MMM YYYY");
            this.currency = localStorage.getItem('currency');
            root.GetOptions();
            this.$emit('update:modelValue', this.$route.name + this.formName);
            this.isService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;


            if (this.formName == 'BankPay' || this.formName == 'AdvancePurchase') {
                this.paymentTerm = 'Credit';
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (localStorage.getItem('IsSupplierPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                }
                else if (this.$route.query.data.formName == "SupplierQuotation") {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                    this.paymentVoucher.documentType = "Supplier Quotation";
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.renderAccountDropdown++;
                    this.enableInvoiceDropdown(true);
                    this.paymentVoucher.documentId = this.$route.query.data.saleId;
                    this.isForm = this.$route.query.data.isForm;
                    this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;
                    this.GetValue('supplierQuotation');
                }
                else if (this.$route.query.data.formName == "PurchaseOrder") {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                    this.paymentVoucher.documentType = "Purchase Order";
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.renderAccountDropdown++;
                    this.enableInvoiceDropdown(true);
                    this.paymentVoucher.documentId = this.$route.query.data.saleId;
                    this.isForm = this.$route.query.data.isForm;
                    this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;
                    this.GetValue('purchaseorder');
                }
                else if (this.$route.query.data.isPurchaseInvoice) {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;

                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }

                    if (this.isValid('PremiumSupplierAdvancePayment')) {
                        this.paymentVoucher.purchaseInvoice = this.$route.query.data.saleId;
                        this.renderAccountDropdown++;
                        this.enableInvoiceDropdown(true);

                        // Is Purchase Invoice
                        this.isPurchaseInvoice = this.$route.query.data.isPurchaseInvoice;
                        this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;

                        this.getPurchaseNetAmount();
                    }
                    if (this.isValid('MultipleSupplierAdvancePayment')) {
                        this.paymentVoucher.amount = this.$route.query.data.remainingAmount;
                        this.enableInvoiceDropdown(true);
                        this.paymentVoucher.purchaseInvoice = this.$route.query.data.saleId;
                        this.isPurchaseInvoice = this.$route.query.data.isPurchaseInvoice;
                    }
                }
                else if (this.$route.query.data != undefined) {
                    this.isSaleInvoice = this.$route.query.data.isSaleInvoice;


                    if (this.$route.query.data.isTemporaryCashIssue) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.isTemporaryCashIssue = this.$route.query.data.isTemporaryCashIssue;
                        this.paymentVoucher.temporaryCashIssueId = this.$route.query.data.id;
                        this.temporaryCashIssue = this.$route.query.data.amount - (this.$route.query.data.returnAmount + this.$route.query.data.voucherAmount);

                        //this.paymentVoucher = this.$route.query.data.message;
                        this.isShow = false
                        this.attachment = true;
                        this.paymentVoucher.bankCashAccountId = this.$route.query.data.temporaryCashAccountId;
                        this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                        this.enableInvoiceDropdown(false);
                        this.purchaseInvoiceRander++
                        this.paymentVoucher.paymentVoucherType = 'BankPay';
                        if (this.$i18n.locale == 'ar') {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                    } else {
                        this.paymentVoucher = this.$route.query.data;
                        this.isShow = false
                        this.attachment = true;
                        this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                        this.enableInvoiceDropdown(false);
                        this.purchaseInvoiceRander++
                        this.paymentVoucher.paymentVoucherType = 'BankPay';

                        this.paymentVoucher.date = this.$route.query.data.dates;
                        if (this.$i18n.locale == 'ar') {
                            //Payment Method
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'التحقق من';
                            }
                            else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'تحويل';
                            }
                            else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'الوديعة';
                            }
                            else if (this.paymentVoucher.paymentMethod == 6) {
                                this.paymentVoucher.paymentMethod = 'Card';
                            }
                            else if (this.paymentVoucher.paymentMethod == 4) {
                                this.paymentVoucher.paymentMethod = 'Debit Card';
                            }
                            else if (this.paymentVoucher.paymentMethod == 5) {
                                this.paymentVoucher.paymentMethod = 'Credit Card';
                            }
                            else if (this.paymentVoucher.paymentMethod == 7) {
                                this.paymentVoucher.paymentMethod = 'Int. Transfer';
                            }
                            else if (this.paymentVoucher.paymentMethod == 8) {
                                this.paymentVoucher.paymentMethod = 'Credit Card Master';
                            }
                            else if (this.paymentVoucher.paymentMethod == 9) {
                                this.paymentVoucher.paymentMethod = 'Credit Card Visa';
                            }
                            else if (this.paymentVoucher.paymentMethod == 10) {
                                this.paymentVoucher.paymentMethod = 'American Express';
                            }
                            else {
                                this.paymentVoucher.paymentMethod = '';
                            }

                            //Payment Mode
                            if (this.paymentVoucher.paymentMode == 0) {
                                this.paymentVoucher.paymentMode = 'السيولة النقدية';
                            }
                            if (this.paymentVoucher.paymentMode == 1) {
                                this.paymentVoucher.paymentMode = 'مصرف';
                            }
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'يتقدم';
                                this.getAdvanceAmount();
                            }

                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {

                            //Payment Method
                            if (this.paymentVoucher.paymentMethod == 1) {
                                this.paymentVoucher.paymentMethod = 'Cheque';
                            }
                            else if (this.paymentVoucher.paymentMethod == 2) {
                                this.paymentVoucher.paymentMethod = 'Transfer';
                            }
                            else if (this.paymentVoucher.paymentMethod == 3) {
                                this.paymentVoucher.paymentMethod = 'Deposit';
                            }
                            else if (this.paymentVoucher.paymentMethod == 4) {
                                this.paymentVoucher.paymentMethod = 'Debit Card';
                            }
                            else if (this.paymentVoucher.paymentMethod == 5) {
                                this.paymentVoucher.paymentMethod = 'Credit Card';
                            }
                            else if (this.paymentVoucher.paymentMethod == 6) {
                                this.paymentVoucher.paymentMethod = 'Card';
                            }
                            else if (this.paymentVoucher.paymentMethod == 7) {
                                this.paymentVoucher.paymentMethod = 'Int. Transfer';
                            }
                            else if (this.paymentVoucher.paymentMethod == 8) {
                                this.paymentVoucher.paymentMethod = 'Credit Card Master';
                            }
                            else if (this.paymentVoucher.paymentMethod == 9) {
                                this.paymentVoucher.paymentMethod = 'Çredit Card Visa';
                            }
                            else if (this.paymentVoucher.paymentMethod == 10) {
                                this.paymentVoucher.paymentMethod = 'American Express';
                            }
                            else {
                                this.paymentVoucher.paymentMethod = '';
                            }

                            //Payment Mode
                            if (this.paymentVoucher.paymentMode == 0) {
                                this.paymentVoucher.paymentMode = 'Cash';
                            }
                            if (this.paymentVoucher.paymentMode == 1) {
                                this.paymentVoucher.paymentMode = 'Bank';
                            }
                            if (this.paymentVoucher.paymentMode == 5) {
                                this.paymentVoucher.paymentMode = 'Advance';
                                this.getAdvanceAmount();
                            }

                        }
                    }
                }
            }
            if (this.formName == 'PettyCash') {
                

                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (this.$i18n.locale == 'ar') {
                        this.paymentVoucher.paymentMode = 'السيولة النقدية';

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        this.paymentVoucher.paymentMode = 'Cash';

                    }
                }
                if (this.$route.query.data != undefined) {
                    this.isSaleInvoice = this.$route.query.data.isSaleInvoice;
                    this.paymentVoucher = this.$route.query.data;
                    this.isShow = false
                    this.attachment = true;
                    this.saleInvoiceRander++
                    this.paymentVoucher.paymentVoucherType = 'PettyCash';
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.pettyCash == 1) {
                            this.paymentVoucher.pettyCash = 'مؤقت';
                        }
                        if (this.paymentVoucher.pettyCash == 2) {
                            this.paymentVoucher.pettyCash = 'عام';
                        }
                        if (this.paymentVoucher.pettyCash == 3) {
                            this.paymentVoucher.pettyCash = 'تقدم';
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.paymentVoucher.pettyCash == 1) {
                            this.paymentVoucher.pettyCash = 'Temporary';
                        }
                        if (this.paymentVoucher.pettyCash == 2) {
                            this.paymentVoucher.pettyCash = 'General';
                        }
                        if (this.paymentVoucher.pettyCash == 3) {
                            this.paymentVoucher.pettyCash = 'Advance';
                        }

                    }
                    if (this.$i18n.locale == 'ar') {
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        } else {
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
                        } else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        } else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        } else if (this.paymentVoucher.paymentMethod == 4) {
                            this.paymentVoucher.paymentMethod = 'Debit Card';
                        } else if (this.paymentVoucher.paymentMethod == 5) {
                            this.paymentVoucher.paymentMethod = 'Credit Card';
                        } else {
                            this.paymentVoucher.paymentMethod = '';
                        }
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                        }

                    }

                }
            }
            if (this.formName == 'BankReceipt' || this.formName == 'AdvanceReceipt') {

                this.paymentTerm = 'Credit';
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.paymentVoucher.paymentVoucherType = this.formName;

                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                }

                else if (this.$route.query.data.formName == "ServiceSaleOrder") {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                    this.paymentVoucher.documentType = "Sale Order";
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.renderAccountDropdown++;
                    this.enableInvoiceDropdown(true);
                    this.paymentVoucher.documentId = this.$route.query.data.saleId;
                    this.isForm = this.$route.query.data.isForm;
                    this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;
                    this.GetValue('saleorder');
                }
                else if (this.$route.query.data.formName == "ServiceQuotation") {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                    this.paymentVoucher.documentType = "Quotation";
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.renderAccountDropdown++;
                    this.enableInvoiceDropdown(true);
                    this.paymentVoucher.documentId = this.$route.query.data.saleId;
                    this.isForm = this.$route.query.data.isForm;
                    this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;
                    this.GetValue('quotation');
                }
                else if (this.$route.query.data.formName == "ProfomaInvoice") {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;
                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }
                    this.paymentVoucher.documentType = "Proforma Invoice";
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.renderAccountDropdown++;
                    this.enableInvoiceDropdown(true);
                    this.paymentVoucher.documentId = this.$route.query.data.saleId;
                    this.isForm = this.$route.query.data.isForm;
                    this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;
                    this.GetValue('saleinvoice');
                    this.proformaRender++;
                }
                else if (this.$route.query.data.isSaleInvoice) {
                    this.paymentVoucher.contactAccountId = this.$route.query.data.accountId;
                    this.GetAutoCodeGenerator(this.formName);
                    this.isShow = false
                    this.attachment = true;
                    this.paymentVoucher.paymentVoucherType = this.formName;

                    if (localStorage.getItem('IsCustomerPayCredit') != 'true') {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية'
                    } else {
                        this.paymentVoucher.paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف'
                    }

                    if (this.isValid('PremiumAdvancePayment')) {
                        this.paymentVoucher.saleInvoice = this.$route.query.data.saleId;
                        this.renderAccountDropdown++;
                        this.enableInvoiceDropdown(true);

                        // Is Sale Invoice
                        this.isSaleInvoice = this.$route.query.data.isSaleInvoice;
                        this.paymentVoucher.amount = this.$route.query.data.netSaleAmount;

                        this.getSaleNetAmount();
                    }
                    if (this.isValid('MultipleAdvancePayment')) {
                        this.paymentVoucher.amount = this.$route.query.data.remainingAmount;
                        this.enableInvoiceDropdown(true);
                        this.paymentVoucher.saleInvoice = this.$route.query.data.saleId;
                        this.isSaleInvoice = this.$route.query.data.isSaleInvoice;
                    }
                }
                else if (this.$route.query.data != undefined) {

                    this.paymentVoucher = this.$route.query.data;
                    this.paymentVoucher.isView = this.$route.query.isView == 'true' || this.$route.query.isView == true ? true : false;
                    this.isShow = false
                    this.GetBankOpeningBalance(this.paymentVoucher.bankCashAccountId);
                    this.enableInvoiceDropdown(false);
                    this.attachment = true;
                    this.saleInvoiceRander++;
                    this.isSaleInvoice = this.$route.query.data.isSaleInvoice;
                    if (this.formName == 'AdvanceReceipt') {
                        this.paymentVoucher.paymentVoucherType = 'AdvanceReceipt';
                    }
                    else {
                        this.paymentVoucher.paymentVoucherType = 'BankReceipt';
                    }

                    this.paymentVoucher.date = this.$route.query.data.dates;
                    if (this.$i18n.locale == 'ar') {

                        //Payment Method
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'التحقق من';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'تحويل';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'الوديعة';
                        }
                        else if (this.paymentVoucher.paymentMethod == 6) {
                            this.paymentVoucher.paymentMethod = 'Card';
                        }
                        else if (this.paymentVoucher.paymentMethod == 4) {
                            this.paymentVoucher.paymentMethod = 'Debit Card';
                        }
                        else if (this.paymentVoucher.paymentMethod == 5) {
                            this.paymentVoucher.paymentMethod = 'Credit Card';
                        }
                        else if (this.paymentVoucher.paymentMethod == 7) {
                            this.paymentVoucher.paymentMethod = 'Int. Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 8) {
                            this.paymentVoucher.paymentMethod = 'Credit Card Master';
                        }
                        else if (this.paymentVoucher.paymentMethod == 9) {
                            this.paymentVoucher.paymentMethod = 'Credit Card Visa';
                        }
                        else if (this.paymentVoucher.paymentMethod == 10) {
                            this.paymentVoucher.paymentMethod = 'American Express';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        //Payment Mode
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'السيولة النقدية';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'مصرف';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'يتقدم';
                            this.getAdvanceAmount();
                        }

                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {

                        //Payment Method
                        if (this.paymentVoucher.paymentMethod == 1) {
                            this.paymentVoucher.paymentMethod = 'Cheque';
                        }
                        else if (this.paymentVoucher.paymentMethod == 2) {
                            this.paymentVoucher.paymentMethod = 'Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 3) {
                            this.paymentVoucher.paymentMethod = 'Deposit';
                        }
                        else if (this.paymentVoucher.paymentMethod == 4) {
                            this.paymentVoucher.paymentMethod = 'Debit Card';
                        }
                        else if (this.paymentVoucher.paymentMethod == 5) {
                            this.paymentVoucher.paymentMethod = 'Credit Card';
                        }
                        else if (this.paymentVoucher.paymentMethod == 6) {
                            this.paymentVoucher.paymentMethod = 'Card';
                        }
                        else if (this.paymentVoucher.paymentMethod == 7) {
                            this.paymentVoucher.paymentMethod = 'Int. Transfer';
                        }
                        else if (this.paymentVoucher.paymentMethod == 8) {
                            this.paymentVoucher.paymentMethod = 'Credit Card Master';
                        }
                        else if (this.paymentVoucher.paymentMethod == 9) {
                            this.paymentVoucher.paymentMethod = 'Çredit Card Visa';
                        }
                        else if (this.paymentVoucher.paymentMethod == 10) {
                            this.paymentVoucher.paymentMethod = 'American Express';
                        }
                        else {
                            this.paymentVoucher.paymentMethod = '';
                        }

                        //Payment Mode
                        if (this.paymentVoucher.paymentMode == 0) {
                            this.paymentVoucher.paymentMode = 'Cash';
                        }
                        if (this.paymentVoucher.paymentMode == 1) {
                            this.paymentVoucher.paymentMode = 'Bank';
                        }
                        if (this.paymentVoucher.paymentMode == 5) {
                            this.paymentVoucher.paymentMode = 'Advance';
                            this.getAdvanceAmount();
                        }

                    }

                }
            }
    },
    mounted: function () {
            this.isMultiPayment = false;
            this.language = this.$i18n.locale;
            this.render++;
    }
}

</script>

<style scoped>
.badge-icon {
    border-radius: 50%;
    background-color: red;
    color: white;
}

.bg-success {
    background-color: #3c873c !important;
}

.filter-green {
    filter: invert(17%) sepia(80%) saturate(6562%) hue-rotate(357deg) brightness(98%) contrast(117%);
    opacity: 1 !important;
}

.full_size {
    position: absolute;
    top: 0;
    left: 22px;
    width: 100%;
    height: 100%;
    display: block;
    z-index: 9;
    font-size: 0;
}

.circle-singleline {
    margin: 20px;
    width: 60px;
    height: 60px;
    border-radius: 50%;
    font-size: 36px;
    text-align: center;
    background: blue;
    color: #fff;
}

.custom_code::after {
    background: purple !important;
}</style>
