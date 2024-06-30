<template>
    <div class="row">
        <div class="col-lg-12 px-3">
            <div class="row">
                <div class="col-sm-6">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;">LS</span>
                            <div class="media-body align-self-center ms-3">
                                <h6 class="m-0 font-20">{{ $t('List View Setup') }} </h6>
                                <div class="col d-flex ">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <hr class="hr-dashed hr-menu mt-0" />
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'DisplayName' }"
                        v-on:click="makeActive('DisplayName')" data-bs-toggle="tab" href="#home" role="tab"
                        aria-selected="true">
                        {{ $t('Client/Customer') }}
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'ItemList' }" data-bs-toggle="tab" href="#profile"
                        role="tab" aria-selected="false" v-on:click="makeActive('ItemList')">
                        {{ $t('Suppliers') }}
                    </a>
                </li>
                <li class="nav-item" hidden >
                    <a class="nav-link" v-bind:class="{ active: active == 'SaleList' }" data-bs-toggle="tab" href="#profile"
                        role="tab" aria-selected="false" v-on:click="makeActive('SaleList')">
                        {{ $t('Sale List') }}
                    </a>
                </li>

            </ul>
            <div class="tab-content">
                <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'DisplayName' }">
                    <div class="col-lg-12">
                        <h5 class="fw-bold">Client/Customer Lists Columns</h5>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.code" :key="render"
                                        v-on:change="GetCustomerListOrderValue('code')" type="checkbox"
                                        id="inline1">
                                    <label for="inline1">{{ $t('Code') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.customerType" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerType')" type="checkbox"
                                        id="inline2">
                                    <label for="inline2">{{ $t('Customer Type') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.customerNameEn" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerNameEn')" type="checkbox"
                                        id="inline3">
                                    <label for="inline3">{{ $t('Customer Name English') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.customerNameAr" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerNameAr')" type="checkbox"
                                        id="inline4">
                                    <label for="inline4">{{ $t('Customer Name Arabic') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.displayName" :key="render"
                                        v-on:change="GetCustomerListOrderValue('displayName')" type="checkbox"
                                        id="inline5">
                                    <label for="inline5">{{ $t('Customer Display Name') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.companyNameEn" :key="render"
                                        v-on:change="GetCustomerListOrderValue('companyNameEn')" type="checkbox"
                                        id="inline6">
                                    <label for="inline6">{{ $t('Company Name English') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.companyNameAr" :key="render"
                                        v-on:change="GetCustomerListOrderValue('companyNameAr')" type="checkbox"
                                        id="inline7">
                                    <label for="inline7">{{ $t('Company Name Arabic') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.mobileNo" :key="render"
                                        v-on:change="GetCustomerListOrderValue('mobileNo')" type="checkbox"
                                        id="inline8">
                                    <label for="inline8">{{ $t('Mobile No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.whatsappNo" :key="render"
                                        v-on:change="GetCustomerListOrderValue('whatsappNo')" type="checkbox"
                                        id="inline9">
                                    <label for="inline9">{{ $t('Whatsapp No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.registrationDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('registrationDate')" type="checkbox"
                                        id="inline10">
                                    <label for="inline10">{{ $t('Registration Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.date" :key="render"
                                        v-on:change="GetCustomerListOrderValue('date')" type="checkbox"
                                        id="inline11">
                                    <label for="inline11">{{ $t('Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.email" :key="render"
                                        v-on:change="GetCustomerListOrderValue('email')" type="checkbox"
                                        id="inline12">
                                    <label for="inline12">{{ $t('Email') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.crn" :key="render"
                                        v-on:change="GetCustomerListOrderValue('crn')" type="checkbox"
                                        id="inline13">
                                    <label for="inline13">{{ $t('Commercial Reg No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.taxNo" :key="render"
                                        v-on:change="GetCustomerListOrderValue('taxNo')" type="checkbox"
                                        id="inline14">
                                    <label for="inline14">{{ $t('VAT/NTN/Tax No ') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.customerCode" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerCode')" type="checkbox"
                                        id="inline15">
                                    <label for="inline15">{{ $t('Customer Code') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.paymentTerms" :key="render"
                                        v-on:change="GetCustomerListOrderValue('paymentTerms')" type="checkbox"
                                        id="inline16">
                                    <label for="inline16">{{ $t('Payment Terms') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.address" :key="render"
                                        v-on:change="GetCustomerListOrderValue('address')" type="checkbox"
                                        id="inline17">
                                    <label for="inline17">{{ $t('Address') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.customerCategory" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerCategory')" type="checkbox"
                                        id="inline39">
                                    <label for="inline39">{{ $t('Customer Category') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="customer.runningBalance" :key="render"
                                        v-on:change="GetCustomerListOrderValue('runningBalance')" type="checkbox"
                                        id="inline39">
                                    <label for="inline39">{{ $t('Customer Running Balance') }}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="hr-dashed hr-menu my-2" />
                    <div class="col-md-12 mb-5 pb-4">
                        <h5 class="fw-bold">Client/Customer List Header Display Order</h5>
                        <p><b> Please Drag and Drop Items to Update the Item list Display order.</b></p>
                        <h6>Client/Customer columns will be</h6>
                        <draggable class="col-sm-12 border py-2 d-flex" v-model="resultData2" style="margin: unset;">
                            <div class="row align-items-baseline p-0 m-0 mb-1 me-2" style="background:#f0f8ff"
                                v-for="(module) in resultData2" v-bind:key="module.name">
                                <div>
                                    <h5>
                                        <a href="javascript:void(0)">
                                            {{ module.displayName }}
                                        </a>
                                    </h5>
                                </div>
                            </div>
                        </draggable>
                    </div>
                </div>
                <div class="tab-pane pt-3" id="profile" role="tabpanel" v-bind:class="{ active: active == 'ItemList' }">
                    <div class="col-lg-12">
                        <h5 class="fw-bold">Supplier Lists Columns</h5>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.code" :key="render"
                                        v-on:change="GetSupplierListOrderValue('code')" type="checkbox"
                                        id="inline18">
                                    <label for="inline18">{{ $t('Code') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.customerType" :key="render"
                                        v-on:change="GetSupplierListOrderValue('customerType')" type="checkbox"
                                        id="inline19">
                                    <label for="inline19">{{ $t('Supplier Type') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.customerNameEn" :key="render"
                                        v-on:change="GetSupplierListOrderValue('customerNameEn')" type="checkbox"
                                        id="inline20">
                                    <label for="inline20">{{ $t('Supplier Name English') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.customerNameAr" :key="render"
                                        v-on:change="GetSupplierListOrderValue('customerNameAr')" type="checkbox"
                                        id="inline21">
                                    <label for="inline21">{{ $t('Supplier Name Arabic') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.displayName" :key="render"
                                        v-on:change="GetSupplierListOrderValue('displayName')" type="checkbox"
                                        id="inline22">
                                    <label for="inline22">{{ $t('Supplier Display Name') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.companyNameEn" :key="render"
                                        v-on:change="GetSupplierListOrderValue('companyNameEn')" type="checkbox"
                                        id="inline23">
                                    <label for="inline23">{{ $t('Company Name English') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.companyNameAr" :key="render"
                                        v-on:change="GetSupplierListOrderValue('companyNameAr')" type="checkbox"
                                        id="inline34">
                                    <label for="inline34">{{ $t('Company Name Arabic') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.mobileNo" :key="render"
                                        v-on:change="GetSupplierListOrderValue('mobileNo')" type="checkbox"
                                        id="inline24">
                                    <label for="inline24">{{ $t('Mobile No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.whatsappNo" :key="render"
                                        v-on:change="GetSupplierListOrderValue('whatsappNo')" type="checkbox"
                                        id="inline25">
                                    <label for="inline25">{{ $t('Whatsapp No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.registrationDate" :key="render"
                                        v-on:change="GetSupplierListOrderValue('registrationDate')" type="checkbox"
                                        id="inline26">
                                    <label for="inline26">{{ $t('Registration Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.date" :key="render"
                                        v-on:change="GetSupplierListOrderValue('date')" type="checkbox"
                                        id="inline27">
                                    <label for="inline27">{{ $t('Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.email" :key="render"
                                        v-on:change="GetSupplierListOrderValue('email')" type="checkbox"
                                        id="inline28">
                                    <label for="inline28">{{ $t('Email') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.crn" :key="render"
                                        v-on:change="GetSupplierListOrderValue('crn')" type="checkbox"
                                        id="inline29">
                                    <label for="inline29">{{ $t('Commercial Reg No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.taxNo" :key="render"
                                        v-on:change="GetSupplierListOrderValue('taxNo')" type="checkbox"
                                        id="inline30">
                                    <label for="inline30">{{ $t('VAT/NTN/Tax No ') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.customerCode" :key="render"
                                        v-on:change="GetSupplierListOrderValue('customerCode')" type="checkbox"
                                        id="inline31">
                                    <label for="inline31">{{ $t('Customer Code') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.paymentTerms" :key="render"
                                        v-on:change="GetSupplierListOrderValue('paymentTerms')" type="checkbox"
                                        id="inline32">
                                    <label for="inline32">{{ $t('Payment Terms') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.address" :key="render"
                                        v-on:change="GetSupplierListOrderValue('address')" type="checkbox"
                                        id="inline33">
                                    <label for="inline33">{{ $t('Address') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.address" :key="render"
                                        v-on:change="GetSupplierListOrderValue('address')" type="checkbox"
                                        id="inline35">
                                    <label for="inline35">{{ $t('Address') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.customerCategory" :key="render"
                                        v-on:change="GetSupplierListOrderValue('customerCategory')" type="checkbox"
                                        id="inline36">
                                    <label for="inline36">{{ $t('Supplier Category') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="supplier.runningBalance" :key="render"
                                        v-on:change="GetSupplierListOrderValue('runningBalance')" type="checkbox"
                                        id="inline37">
                                    <label for="inline37">{{ $t('Supplier Running Balance') }}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="hr-dashed hr-menu my-2" />
                    <div class="col-md-12 mb-5 pb-4">
                        <h5 class="fw-bold">Supplier List Header Display Order</h5>
                        <p><b> Please Drag and Drop Items to Update the Item list Display order.</b></p>
                        <h6>Supplier columns will be</h6>
                        <draggable class="col-sm-12 border py-2 d-flex" v-model="resultData" style="margin: unset;">
                            <div class="row align-items-baseline p-0 m-0 mb-1 me-2" style="background:#f0f8ff"
                                v-for="(module) in resultData" v-bind:key="module.name">
                                <div>
                                    <h5>
                                        <a href="javascript:void(0)">
                                            {{ module.displayName }}
                                        </a>
                                    </h5>
                                </div>
                            </div>
                        </draggable>
                    </div>
                </div>
                <div class="tab-pane pt-3" id="profile" role="tabpanel" hidden v-bind:class="{ active: active == 'SaleList' }">
                    <div class="col-lg-12">
                        <h5 class="fw-bold">Supplier Lists Columns</h5>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.registatrationNo" :key="render"
                                        v-on:change="GetCustomerListOrderValue('registatrationNo')" type="checkbox"
                                        id="inline41">
                                    <label for="inline41">{{ $t('Invoice No') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.date" :key="render"
                                        v-on:change="GetCustomerListOrderValue('date')" type="checkbox"
                                        id="inline42">
                                    <label for="inline42">{{ $t('Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.customerName" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerName')" type="checkbox"
                                        id="inline43">
                                    <label for="inline43">{{ $t('Customer Name') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.createdBy" :key="render"
                                        v-on:change="GetCustomerListOrderValue('createdBy')" type="checkbox"
                                        id="inline44">
                                    <label for="inline44">{{ $t('Created By') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.createdFrom" :key="render"
                                        v-on:change="GetCustomerListOrderValue('createdFrom')" type="checkbox"
                                        id="inline45">
                                    <label for="inline45">{{ $t('Created From') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.saleHoldType" :key="render"
                                        v-on:change="GetCustomerListOrderValue('saleHoldType')" type="checkbox"
                                        id="inline46">
                                    <label for="inline46">{{ $t('SaleHoldType') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.netAmount" :key="render"
                                        v-on:change="GetCustomerListOrderValue('netAmount')" type="checkbox"
                                        id="inline47">
                                    <label for="inline47">{{ $t('Net Amount') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.warehouse" :key="render"
                                        v-on:change="GetCustomerListOrderValue('warehouse')" type="checkbox"
                                        id="inline48">
                                    <label for="inline48">{{ $t('Warehouse ') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.invoiceDueDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('invoiceDueDate')" type="checkbox"
                                        id="inline49">
                                    <label for="inline49">{{ $t('Invoice Due Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.reference" :key="render"
                                        v-on:change="GetCustomerListOrderValue('reference')" type="checkbox"
                                        id="inline50">
                                    <label for="inline50">{{ $t('Reference') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.poFor" :key="render"
                                        v-on:change="GetCustomerListOrderValue('poFor')" type="checkbox"
                                        id="inline51">
                                    <label for="inline51">{{ $t('PO For') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.transporter" :key="render"
                                        v-on:change="GetCustomerListOrderValue('transporter')" type="checkbox"
                                        id="inline52">
                                    <label for="inline52">{{ $t('Transporter / Cargo') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.dispatchDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('dispatchDate')" type="checkbox"
                                        id="inline53">
                                    <label for="inline53">{{ $t('Dispatch Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.deliveryDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('deliveryDate')" type="checkbox"
                                        id="inline54">
                                    <label for="inline54">{{ $t('Delivery Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.pickupDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('pickupDate')" type="checkbox"
                                        id="inline55">
                                    <label for="inline55">{{ $t('Pickup Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.referredBy" :key="render"
                                        v-on:change="GetCustomerListOrderValue('referredBy')" type="checkbox"
                                        id="inline56">
                                    <label for="inline56">{{ $t('Referred By') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.customerInquiry" :key="render"
                                        v-on:change="GetCustomerListOrderValue('customerInquiry')" type="checkbox"
                                        id="inline57">
                                    <label for="inline57">{{ $t('Customer Inquiry') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.poNumber" :key="render"
                                        v-on:change="GetCustomerListOrderValue('poNumber')" type="checkbox"
                                        id="inline58">
                                    <label for="inline58">{{ $t('PO Number') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.poDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('poDate')" type="checkbox"
                                        id="inline59">
                                    <label for="inline59">{{ $t('PO Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.quotationNoDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('quotationNoDate')" type="checkbox"
                                        id="inline60">
                                    <label for="inline60">{{ $t('Quotation No. & Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.validUptoFor" :key="render"
                                        v-on:change="GetCustomerListOrderValue('validUptoFor')" type="checkbox"
                                        id="inline61">
                                    <label for="inline61">{{ $t('Valid Upto / For') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.proformaInvoiceNoDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('proformaInvoiceNoDate')" type="checkbox"
                                        id="inline62">
                                    <label for="inline62">{{ $t('Proforma Invoice No. & Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.purchaseOrderNoDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('purchaseOrderNoDate')" type="checkbox"
                                        id="inline63">
                                    <label for="inline63">{{ $t('Purchase Order No. & Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.saleOrderNoDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('saleOrderNoDate')" type="checkbox"
                                        id="inline64">
                                    <label for="inline64">{{ $t('Sale Order No. & Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.inquiryNoDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('inquiryNoDate')" type="checkbox"
                                        id="inline65">
                                    <label for="inline65">{{ $t('Inquiry No. & Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.deliveryNoteNoDate" :key="render"
                                        v-on:change="GetCustomerListOrderValue('deliveryNoteNoDate')" type="checkbox"
                                        id="inline66">
                                    <label for="inline66">{{ $t('Delivery Note No. & Date') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.discountAmount" :key="render"
                                        v-on:change="GetCustomerListOrderValue('discountAmount')" type="checkbox"
                                        id="inline67">
                                    <label for="inline67">{{ $t('Discount Amount') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="sale.vatAmount" :key="render"
                                        v-on:change="GetCustomerListOrderValue('vatAmount')" type="checkbox"
                                        id="inline68">
                                    <label for="inline68">{{ $t('Vat Amount') }}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>

        </div>

        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class="col-md-12">
                    <div class="button-items">
                        <button class="btn btn-outline-primary" v-if="active == 'DisplayName'"
                            v-on:click="SaveItemViewSetup('Customer')"><i class="far fa-save "></i> {{ $t('AddCustomer.btnSave')
                            }}</button>
                        <button class="btn btn-outline-primary" v-if="active == 'ItemList'"
                            v-on:click="SaveItemViewSetup('Supplier')"><i class="far fa-save "></i> {{ $t('AddCustomer.btnSave')
                            }}</button>
                        <button class="btn btn-outline-primary" v-if="active == 'SaleList'"
                            v-on:click="SaveItemViewSetup('Supplier')"><i class="far fa-save "></i> {{ $t('AddCustomer.btnSave')
                            }}
                        </button>
                        <button class="btn btn-danger" v-on:click="GotoPage('/StartScreen')">{{ $t('AddCustomer.Cancel')
                        }}</button>
                    </div>
                </div>
            </div>
        </div>
        <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
    </div>
</template>

<script>
 import Loading from 'vue-loading-overlay';
 import 'vue-loading-overlay/dist/css/index.css';
 import { VueDraggableNext } from 'vue-draggable-next'
export default {
    components: {
        Loading,
        draggable: VueDraggableNext,
    },
    data: function () {
        return {
            loading: false,
            customer:{
                code:false,
                customerType:false,
                customerNameEn:false,
                customerNameAr:false,
                displayName:false,
                companyNameEn:false,
                companyNameAr:false,
                mobileNo:false,
                whatsappNo:false,
                registrationDate:false,
                date:false,
                email:false,
                crn:false,
                taxNo:false,
                customerCode:false,
                paymentTerms:false,
                address:false,
                customerCategory:false,
                runningBalance:false,
            },
            supplier:{
                code:false,
                customerType:false,
                customerNameEn:false,
                customerNameAr:false,
                displayName:false,
                companyNameEn:false,
                companyNameAr:false,
                mobileNo:false,
                whatsappNo:false,
                registrationDate:false,
                date:false,
                email:false,
                crn:false,
                taxNo:false,
                customerCode:false,
                paymentTerms:false,
                address:false,
                customerCategory:false,
                runningBalance:false,
            },
            active: 'DisplayName',
            customerListView:[],
            supplierListView:[],
            listOrderSetups:{
                customerListView:[],
                supplierListView:[],
            },
            sale:{
                registatrationNo: false,
                date: false,
                customerName: false,
                createdBy: false,
                createdFrom: false,
                saleHoldType: false,
                netAmount: false,
                warehouse: false,
                invoiceDueDate: false,
                reference: false,
                poFor: false,
                transporter: false,
                dispatchDate: false,
                deliveryDate: false,
                pickupDate: false,
                referredBy: false,
                customerInquiry: false,
                poNumber: false,
                poDate: false,
                quotationNoDate: false,
                validUptoFor: false,
                proformaInvoiceNoDate: false,
                purchaseOrderNoDate: false,
                saleOrderNoDate: false,
                inquiryNoDate: false,
                deliveryNoteNoDate: false,
                discountAmount:false,
                vatAmount: false,
            },
            render:0
        }
    },
    computed: {
        resultData: {
            get() {
                return this.supplierListView;
            },

            set(val) {
                this.supplierListView = val;
                var count = 1;
                this.supplierListView.forEach(function (x) {
                    x.sequence = count
                    count++
                });
            }
        },
        resultData2: {
            get() {
                return this.customerListView;
            },

            set(val) {
                this.customerListView = val;
                var count = 1;
                this.customerListView.forEach(function (x) {
                    x.sequence = count
                    count++
                });
            }
        }
    },
    methods: {
        makeActive: function (item) {
            this.active = item;
            if (item == "DisplayName") {
                this.GetItemViewSetup();
            }
            else if (item == "ItemList") {
                this.GetItemViewSetup();
            }
            else if (item == "SaleList") {
                this.GetItemViewSetup();
            }
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        GetSupplierListOrderValue: function (value) {

            const trueValues = Object.values(this.supplier).filter(value => value);
            const count = trueValues.length;

            if (count > 10) {

                if (value == "code") {
                    this.supplier.code = false;
                }
                else if (value == "customerType") {
                    this.supplier.customerType = false;
                }
                else if (value == "customerNameEn") {
                    this.supplier.customerNameEn = false;
                }
                else if (value == "customerNameAr") {
                    this.supplier.customerNameAr = false;
                }
                else if (value == "displayName") {
                    this.supplier.displayName = false;
                }
                else if (value == "companyNameEn") {
                    this.supplier.companyNameEn = false;
                }
                else if (value == "companyNameAr") {
                    this.supplier.companyNameAr = false;
                }
                else if (value == "mobileNo") {
                    this.supplier.mobileNo = false;
                }
                else if (value == "whatsappNo") {
                    this.supplier.whatsappNo = false;
                }
                else if (value == "registrationDate") {
                    this.supplier.registrationDate = false;
                }
                else if (value == "date") {
                    this.supplier.date = false;
                }
                else if (value == "email") {
                    this.supplier.email = false;
                }
                else if (value == "crn") {
                    this.supplier.crn = false;
                }
                else if (value == "taxNo") {
                    this.supplier.taxNo = false;
                }
                else if (value == "paymentTerms") {
                    this.supplier.paymentTerms = false;
                }
                else if (value == "address") {
                    this.supplier.address = false;
                }
                else if (value == "customerCode") {
                    this.supplier.customerCode = false;
                }
                else if (value == "customerCategory") {
                    this.supplier.customerCategory = false;
                }
                else if (value == "runningBalance") {
                    this.supplier.runningBalance = false;
                }

                this.render++;
                this.$swal({
                    title: 'List Columns',
                    text: 'You can not select more than Ten values',
                    type: 'error',
                    icon: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
            else {
                if (value == "code") {
                    if (this.supplier.code) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Code',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });

                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerType") {
                    if (this.supplier.customerType) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Supplier Type',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerNameEn") {
                    if (this.supplier.customerNameEn) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Supplier Name English',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerNameAr") {
                    if (this.supplier.customerNameAr) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Supplier Name Arabic',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "displayName") {
                    if (this.supplier.displayName) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Supplier Display Name',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "companyNameEn") {
                    if (this.supplier.companyNameEn) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Company Name English',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "companyNameAr") {
                    if (this.supplier.companyNameAr) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Company Name Arabic',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "mobileNo") {
                    if (this.supplier.mobileNo) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Mobile No',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "whatsappNo") {
                    if (this.supplier.whatsappNo) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Whatsapp No',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "registrationDate") {
                    if (this.supplier.registrationDate) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Registration Date',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "date") {
                    if (this.supplier.date) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Date',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "email") {
                    if (this.supplier.email) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Email',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "paymentTerms") {
                    if (this.supplier.paymentTerms) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Payment Terms',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "taxNo") {
                    if (this.supplier.taxNo) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'VAT/NTN/Tax No',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerCode") {
                    if (this.supplier.customerCode) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Code',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "paymentTerms") {
                    if (this.supplier.paymentTerms) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Payment Terms',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "address") {
                    if (this.supplier.address) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Address',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerCategory") {
                    if (this.supplier.customerCategory) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Supplier Category',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "runningBalance") {
                    if (this.supplier.runningBalance) {
                        this.supplierListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Supplier Running Balance',
                            sequence: 8
                        });
                        this.supplierListView = this.supplierListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.supplierListView = this.supplierListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
            }
        },
        GetCustomerListOrderValue: function (value) {

            const trueValues = Object.values(this.customer).filter(value => value);
            const count = trueValues.length;

            if (count > 10) {

                if (value == "code") {
                    this.customer.code = false;
                }
                else if (value == "customerType") {
                    this.customer.customerType = false;
                }
                else if (value == "customerNameEn") {
                    this.customer.customerNameEn = false;
                }
                else if (value == "customerNameAr") {
                    this.customer.customerNameAr = false;
                }
                else if (value == "displayName") {
                    this.customer.displayName = false;
                }
                else if (value == "companyNameEn") {
                    this.customer.companyNameEn = false;
                }
                else if (value == "companyNameAr") {
                    this.customer.companyNameAr = false;
                }
                else if (value == "mobileNo") {
                    this.customer.mobileNo = false;
                }
                else if (value == "whatsappNo") {
                    this.customer.whatsappNo = false;
                }
                else if (value == "registrationDate") {
                    this.customer.registrationDate = false;
                }
                else if (value == "date") {
                    this.customer.date = false;
                }
                else if (value == "email") {
                    this.customer.email = false;
                }
                else if (value == "crn") {
                    this.customer.crn = false;
                }
                else if (value == "taxNo") {
                    this.customer.taxNo = false;
                }
                else if (value == "paymentTerms") {
                    this.customer.paymentTerms = false;
                }
                else if (value == "address") {
                    this.customer.address = false;
                }
                else if (value == "customerCode") {
                    this.customer.customerCode = false;
                }
                else if (value == "customerCategory") {
                    this.customer.customerCategory = false;
                }
                else if (value == "runningBalance") {
                    this.customer.runningBalance = false;
                }

                this.render++;
                this.$swal({
                    title: 'List Columns',
                    text: 'You can not select more than Ten values',
                    type: 'error',
                    icon: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
            else {
                if (value == "code") {
                    if (this.customer.code) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Code',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });

                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerType") {
                    if (this.customer.customerType) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Type',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerNameEn") {
                    if (this.customer.customerNameEn) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Name English',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerNameAr") {
                    if (this.customer.customerNameAr) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Name Arabic',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "displayName") {
                    if (this.customer.displayName) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Display Name',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "companyNameEn") {
                    if (this.customer.companyNameEn) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Company Name English',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "companyNameAr") {
                    if (this.customer.companyNameAr) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Company Name Arabic',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "mobileNo") {
                    if (this.customer.mobileNo) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Mobile No',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "whatsappNo") {
                    if (this.customer.whatsappNo) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Whatsapp No',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "registrationDate") {
                    if (this.customer.registrationDate) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Registration Date',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "date") {
                    if (this.customer.date) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Date',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "email") {
                    if (this.customer.email) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Email',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "paymentTerms") {
                    if (this.customer.paymentTerms) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Payment Terms',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "taxNo") {
                    if (this.customer.taxNo) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'VAT/NTN/Tax No',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerCode") {
                    if (this.customer.customerCode) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Code',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "paymentTerms") {
                    if (this.customer.paymentTerms) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Payment Terms',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "address") {
                    if (this.customer.address) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Address',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "customerCategory") {
                    if (this.customer.customerCategory) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Category',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "address") {
                    if (this.customer.address) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Address',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "runningBalance") {
                    if (this.customer.runningBalance) {
                        this.customerListView.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Customer Running Balance',
                            sequence: 8
                        });
                        this.customerListView = this.customerListView.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.customerListView = this.customerListView.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
            }
        },
       
        SaveItemViewSetup: function (documentName) {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            

            this.listOrderSetups.supplierListView = [];
            this.supplierListView.forEach(x => {
                this.listOrderSetups.supplierListView.push(x);
            })
            this.listOrderSetups.customerListView = [];
            this.customerListView.forEach(x => {
                this.listOrderSetups.customerListView.push(x);
            })

            this.$https.post('/Product/SaveListOrderingSetup?documentName=' + documentName, this.listOrderSetups, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data.isSuccess) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data has been Saved' : 'حفظ بنجاح',
                        type: 'success',
                        icon: 'success',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        confirmButtonClass: "btn btn-success",
                        buttonsStyling: false
                    })
                    root.GetItemViewSetup();
                }
                else {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: response.data.isAddUpdate,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                root.loading = false;
            }
            ).catch(() => {
                root.$swal({
                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: "Please Contact with Support.",
                    type: 'error',
                    confirmButtonClass: "btn btn-info",
                    buttonsStyling: false
                });
                root.loading = false;
            });
        },
        GetItemViewSetup: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


            root.$https.get('/Product/GetItemOrderList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.customerListView = [];
                    root.supplierListView = [];

                    root.customerListView = response.data.customerListView;
                    root.supplierListView = response.data.supplierListView;

                   
                    root.customerListView.forEach((x) => {
                        
                        if (x.name == "code") {
                            root.customer.code = true;
                        }
                        else if (x.name == "customerType") {
                            root.customer.customerType = true;
                        }
                        else if (x.name == "customerNameEn") {
                            root.customer.customerNameEn = true;
                        }
                        else if (x.name == "customerNameAr") {
                            root.customer.customerNameAr = true;
                        }
                        else if (x.name == "displayName") {
                            root.customer.displayName = true;
                        }
                        else if (x.name == "companyNameEn") {
                            root.customer.companyNameEn = true;
                        }
                        else if (x.name == "companyNameAr") {
                            root.customer.companyNameAr = true;
                        }
                        else if (x.name == "mobileNo") {
                            root.customer.mobileNo = true;
                        }
                        else if (x.name == "whatsappNo") {
                            root.customer.whatsappNo = true;
                        }
                        else if (x.name == "registrationDate") {
                            root.customer.registrationDate = true;
                        }
                        else if (x.name == "date") {
                            root.customer.date = true;
                        }
                        else if (x.name == "email") {
                            root.customer.email = true;
                        }
                        else if (x.name == "crn") {
                            root.customer.crn = true;
                        }
                        else if (x.name == "taxNo") {
                            root.customer.taxNo = true;
                        }
                        else if (x.name == "paymentTerms") {
                            root.customer.paymentTerms = true;
                        }
                        else if (x.name == "address") {
                            root.customer.address = true;
                        }
                        else if (x.name == "customerCode") {
                            root.customer.customerCode = true;
                        }
                        else if (x.name == "customerCategory") {
                            root.customer.customerCategory = true;
                        }
                        else if (x.name == "runningBalance") {
                            root.customer.runningBalance = true;
                        }
                    })
                    root.supplierListView.forEach((x) => {
                        if (x.name == "code") {
                            root.supplier.code = true;
                        }
                        else if (x.name == "customerType") {
                            root.supplier.customerType = true;
                        }
                        else if (x.name == "customerNameEn") {
                            root.supplier.customerNameEn = true;
                        }
                        else if (x.name == "customerNameAr") {
                            root.supplier.customerNameAr = true;
                        }
                        else if (x.name == "displayName") {
                            root.supplier.displayName = true;
                        }
                        else if (x.name == "companyNameEn") {
                            root.supplier.companyNameEn = true;
                        }
                        else if (x.name == "companyNameAr") {
                            root.supplier.companyNameAr = true;
                        }
                        else if (x.name == "mobileNo") {
                            root.supplier.mobileNo = true;
                        }
                        else if (x.name == "whatsappNo") {
                            root.supplier.whatsappNo = true;
                        }
                        else if (x.name == "registrationDate") {
                            root.supplier.registrationDate = true;
                        }
                        else if (x.name == "date") {
                            root.supplier.date = true;
                        }
                        else if (x.name == "email") {
                            root.supplier.email = true;
                        }
                        else if (x.name == "crn") {
                            root.supplier.crn = true;
                        }
                        else if (x.name == "taxNo") {
                            root.supplier.taxNo = true;
                        }
                        else if (x.name == "paymentTerms") {
                            root.supplier.paymentTerms = true;
                        }
                        else if (x.name == "address") {
                            root.supplier.address = true;
                        }
                        else if (x.name == "customerCode") {
                            root.supplier.customerCode = true;
                        }
                        else if (x.name == "customerCategory") {
                            root.supplier.customerCategory = true;
                        }
                        else if (x.name == "runningBalance") {
                            root.supplier.runningBalance = true;
                        }
                    })
                    root.render++;
                }
            });

        },
    },
    created: function () {
        this.GetItemViewSetup();
    },

    mounted: function () { },
}
</script>

<style>
.circle-singleline {
    margin: 20px;
    width: 60px;
    height: 60px;
    border-radius: 50%;
    font-size: 30px;
    text-align: center;
    background: blue;
    color: #fff;
    vertical-align: middle;
    line-height: 60px;
}</style>