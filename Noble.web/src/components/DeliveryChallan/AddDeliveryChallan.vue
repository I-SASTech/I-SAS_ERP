<template>
    <div v-if="isValid('CanEditDeliveryNote') || isValid('CanAddDeliveryNote')">
        <div class="row">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">
                                        {{ $t('DeliveryNote.AddDeliveryNote') }} <span style="font-weight:bold">
                                            -
                                            {{ purchase.registrationNo }}
                                        </span>
                                    </h4>
                                    <h4 v-else-if="isView" class="page-title">
                                        {{ $t('DeliveryNote.ViewDeliveryNote') }} <span style="font-weight:bold">
                                            -
                                            {{ purchase.registrationNo }}
                                        </span>
                                    </h4>
                                    <h4 v-else class="page-title"> {{ $t('DeliveryNote.UpdateDeliveryNote') }} <span
                                            style="font-weight:bold"> - {{ purchase.registrationNo }}</span></h4>

                                </div>

                                <div class="col-auto align-self-center">
                                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                        class="btn btn-sm btn-outline-danger">
                                        {{ $t('Sale.Close') }}
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />

                <div class="row">
                    <div class="col-lg-6">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4 text-left">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('DeliveryNote.Customer') }} :
                                    <span class="text-danger">*</span></span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <customerdropdown v-model="purchase.customerId" ref="CustomerDropdown"
                                    @update:modelValue="emptyCashCustomer" :modelValue="purchase.customerId"
                                    :isCredit="sale.isCredit" />

                                <a v-if="purchase.customerId != null && purchase.customerId != ''"
                                    href="javascript:void(0);" data-bs-toggle="offcanvas" ref="offcanvasRight"
                                    data-bs-target="#offcanvasRight" aria-controls="offcanvasRight" class="text-primary">{{
                                        $t('AddSale.ViewCustomerDetails') }}</a>
                                <a v-else href="javascript:void(0);" class="text-secondary">
                                    {{ $t('AddSale.ViewCustomerDetails') }}</a>
                                <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                                    aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                                    <div class="offcanvas-header">
                                        <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddSale.ViewCustomerDetails') }}
                                        </h5>
                                        <button
                                            v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'margin-left:257px !important' : 'margin-left:0px !important'"
                                            type="button" class="btn btn-outline-primary"
                                            @click="UpdateCustomerDetail(sale.customerIdForUpdate)">{{ $t('AddSale.Update')
                                            }}</button>
                                        <button
                                            v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'"
                                            type="button" class="btn-close text-reset filter-green "
                                            data-bs-dismiss="offcanvas" aria-label="Close"></button>
                                    </div>
                                    <div class="offcanvas-body">
                                        <div class="row">
                                            <div class="col-lg-12 form-group">
                                                <label> {{ $t('AddSale.CustomerId') }}:</label>
                                                <input type="text" class="form-control" readonly
                                                    v-model="sale.customerCode" />
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <label>{{ $t('Display Name') }} :</label>
                                                <input type="text" class="form-control" readonly v-model="sale.name" />
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <div class="row">
                                                    <label>{{ $t('Contact Person Name') }} :</label>
                                                    <div class="col-lg-4 form-group">
                                                        <input type="text" class="form-control" readonly
                                                            v-model="sale.prefix" />
                                                    </div>

                                                    <div class="col-lg-4 form-group">
                                                        <input type="text" class="form-control" readonly
                                                            v-model="sale.englishName" />
                                                    </div>
                                                    <div class="col-lg-4 form-group">
                                                        <input type="text" class="form-control" readonly
                                                            v-model="sale.arabicName" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <div class="row">
                                                    <label>{{ $t('Company Name') }} :</label>

                                                    <div class="col-lg-6 form-group">
                                                        <input type="text" class="form-control" readonly
                                                            v-model="sale.companyNameEnglish" />
                                                    </div>
                                                    <div class="col-lg-6 form-group">
                                                        <input type="text" class="form-control" readonly
                                                            v-model="sale.companyNameArabic" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <label>{{ $t('AddCustomer.CommercialRegistrationNo') }} :</label>
                                                <input type="text" class="form-control"
                                                    v-model="sale.commercialRegistrationNo" />
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <label>{{ $t('AddCustomer.VAT/NTN/Tax No') }} :</label>
                                                <input type="text" class="form-control" v-model="sale.vatNo" />
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <label>{{ $t('AddSale.Mobile') }} :</label>
                                                <input type="text" class="form-control" v-model="sale.contactNo1" />
                                            </div>

                                            <div class="col-lg-12 form-group">
                                                <label>{{ $t('AddCustomer.Email') }} :</label>
                                                <input type="text" class="form-control" v-model="sale.email" />
                                            </div>

                                            <div class="col-lg-12 form-group">
                                                <label>{{ $t('AddSale.CustomerAddress') }} :</label>
                                                <textarea rows="3" v-model="sale.address" class="form-control"> </textarea>
                                            </div>
                                            <div class="row" v-if="multipleAddress">
                                                <div class="chat-box-left"
                                                    style="width: 100%; height: 330px !important;min-height: 200px;">
                                                    <ul class="nav nav-pills mb-3 nav-justified" id="pills-tab"
                                                        role="tablist">
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="general_chat_tab"
                                                                data-bs-toggle="pill" href="#general_chat">Delivery</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="group_chat_tab" data-bs-toggle="pill"
                                                                href="#group_chat">Shipping</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="personal_chat_tab" data-bs-toggle="pill"
                                                                href="#personal_chat">Billing</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="National_tab" data-bs-toggle="pill"
                                                                href="#National_chat">National</a>
                                                        </li>
                                                    </ul>

                                                    <div class="chat-list" data-simplebar="init"
                                                        style="height:330px !important ;min-height: 200px;">
                                                        <div class="simplebar-wrapper" style="margin: 0px;">
                                                            <div class="simplebar-height-auto-observer-wrapper">
                                                                <div class="simplebar-height-auto-observer"></div>
                                                            </div>
                                                            <div class="simplebar-mask">
                                                                <div class="simplebar-offset"
                                                                    style="right: 0px; bottom: 0px;">
                                                                    <div class="simplebar-content-wrapper"
                                                                        style="height: 250px !important;min-height: 200px; overflow: hidden scroll;">
                                                                        <div class="simplebar-content"
                                                                            style="padding: 0px;min-height: 200px; ">
                                                                            <div class="tab-content " id="pills-tabContent">
                                                                                <div class="tab-pane fade active show"
                                                                                    id="general_chat">
                                                                                    <a href="javascript:void(0)"
                                                                                        v-for="person  in deliveryAddressList"
                                                                                        v-bind:key="person.id">
                                                                                        <div class="media new-message "
                                                                                            style="padding: 7px 10px 10px 7px !important;"
                                                                                            v-if="person.type == 'Delivery'">
                                                                                            <div class="media-left"
                                                                                                style="text-align: left !important;">
                                                                                                <div
                                                                                                    class="checkbox form-check-inline d-flex">
                                                                                                    <span
                                                                                                        class="badge badge-boxed  badge-outline-primary me-2"
                                                                                                        v-bind:class="person.isDefault ? 'visibilityOn' : 'visibility'"
                                                                                                        v-show="person.isDefault">Default</span>
                                                                                                    <input type="checkbox"
                                                                                                        v-if="person.isOffice"
                                                                                                        v-model="person.isOffice"
                                                                                                        v-bind:id="person.id"
                                                                                                        v-on:change="DefaultOnListDelivery(person, false)">
                                                                                                    <input type="checkbox"
                                                                                                        v-else
                                                                                                        v-model="person.isOffice"
                                                                                                        v-bind:id="person.id"
                                                                                                        v-on:change="DefaultOnListDelivery(person, true)"><label
                                                                                                        v-bind:for="person.id"></label>
                                                                                                </div>

                                                                                            </div>
                                                                                            <div class="media-body">
                                                                                                <div class="d-inline-block"
                                                                                                    style="width: 100% !important;text-align:left;">
                                                                                                    <h6>{{ person.area }}
                                                                                                    </h6>
                                                                                                    <p>{{ person.address }}
                                                                                                    </p>
                                                                                                    <p>{{
                                                                                                        person.googleLocation
                                                                                                    }}</p>
                                                                                                    <p>{{ person.nearBy }}
                                                                                                    </p>

                                                                                                </div>

                                                                                            </div><!-- end media-body -->
                                                                                        </div>

                                                                                    </a>
                                                                                    <div
                                                                                        class="text-center mt-4 d-flex justify-content-center align-items-center flex-column ">
                                                                                        <div>
                                                                                            <a v-on:click="AddRow1('Delivery')"
                                                                                                data-bs-dismiss="offcanvas"
                                                                                                aria-label="Close"
                                                                                                href="javascript:void(0);"
                                                                                                class="btn btn-sm btn-outline-primary mx-1">

                                                                                                Register an Address
                                                                                            </a>
                                                                                        </div>
                                                                                        <div v-if="!deliveryAddressList.some(x => x.type == 'Delivery')"
                                                                                            class="text-center"> No Address
                                                                                            Registered</div>

                                                                                    </div>

                                                                                </div>
                                                                                <!--end general chat-->

                                                                                <div class="tab-pane fade" id="group_chat">
                                                                                    <div class="tab-pane fade active show"
                                                                                        id="general_chat">
                                                                                        <a href="javascript:void(0)"
                                                                                            v-for="person  in deliveryAddressList"
                                                                                            v-bind:key="person.id">
                                                                                            <div class="media new-message "
                                                                                                style="padding: 7px 10px 10px 7px !important;"
                                                                                                v-if="person.type == 'Shipping'">
                                                                                                <div class="media-left"
                                                                                                    style="text-align: left !important;">
                                                                                                    <div
                                                                                                        class="checkbox form-check-inline d-flex">
                                                                                                        <span
                                                                                                            class="badge badge-boxed  badge-outline-primary me-2"
                                                                                                            v-bind:class="person.isDefault ? 'visibilityOn' : 'visibility'"
                                                                                                            v-show="person.isDefault">Default</span>
                                                                                                        <input
                                                                                                            type="checkbox"
                                                                                                            v-if="person.isOffice"
                                                                                                            v-model="person.isOffice"
                                                                                                            v-bind:id="person.id"
                                                                                                            v-on:change="DefaultOnList(person, false)">
                                                                                                        <input
                                                                                                            type="checkbox"
                                                                                                            v-else
                                                                                                            v-model="person.isOffice"
                                                                                                            v-bind:id="person.id"
                                                                                                            v-on:change="DefaultOnList(person, true)"><label
                                                                                                            v-bind:for="person.id"></label>
                                                                                                    </div>

                                                                                                </div>
                                                                                                <div class="media-body">
                                                                                                    <div class="d-inline-block"
                                                                                                        style="width: 100% !important;text-align:left;">
                                                                                                        <h6>{{ person.area
                                                                                                        }}</h6>
                                                                                                        <p>{{ person.address
                                                                                                        }}</p>
                                                                                                        <p>{{
                                                                                                            person.googleLocation
                                                                                                        }}</p>
                                                                                                        <p>{{ person.nearBy
                                                                                                        }}</p>

                                                                                                    </div>

                                                                                                </div>
                                                                                                <!-- end media-body -->
                                                                                            </div>

                                                                                        </a>
                                                                                        <div
                                                                                            class="text-center d-flex justify-content-center align-items-center flex-column mt-4">
                                                                                            <a v-on:click="AddRow1('Shipping')"
                                                                                                data-bs-dismiss="offcanvas"
                                                                                                aria-label="Close"
                                                                                                href="javascript:void(0);"
                                                                                                class="btn btn-sm btn-outline-primary mx-1">

                                                                                                Register an Address
                                                                                            </a>
                                                                                        </div>
                                                                                        <div v-if="!deliveryAddressList.some(x => x.type == 'Shipping')"
                                                                                            class="text-center"> No Address
                                                                                            Registered</div>

                                                                                    </div>

                                                                                </div>
                                                                                <!--end group chat-->

                                                                                <div class="tab-pane fade"
                                                                                    id="personal_chat">
                                                                                    <div class="tab-pane fade active show"
                                                                                        id="general_chat">
                                                                                        <a href="javascript:void(0)"
                                                                                            v-for="person  in deliveryAddressList"
                                                                                            v-bind:key="person.id">
                                                                                            <div class="media new-message "
                                                                                                style="padding: 7px 10px 10px 7px !important;"
                                                                                                v-if="person.type == 'Billing'">
                                                                                                <div class="media-left"
                                                                                                    style="text-align: left !important;">
                                                                                                    <div
                                                                                                        class="checkbox form-check-inline d-flex">
                                                                                                        <span
                                                                                                            class="badge badge-boxed  badge-outline-primary me-2"
                                                                                                            v-bind:class="person.isDefault ? 'visibilityOn' : 'visibility'"
                                                                                                            v-show="person.isDefault">Default</span>
                                                                                                        <input
                                                                                                            type="checkbox"
                                                                                                            v-if="person.isOffice"
                                                                                                            v-model="person.isOffice"
                                                                                                            v-bind:id="person.id"
                                                                                                            v-on:change="DefaultOnList(person, false)">
                                                                                                        <input
                                                                                                            type="checkbox"
                                                                                                            v-else
                                                                                                            v-model="person.isOffice"
                                                                                                            v-bind:id="person.id"
                                                                                                            v-on:change="DefaultOnList(person, true)"><label
                                                                                                            v-bind:for="person.id"></label>
                                                                                                    </div>

                                                                                                </div>
                                                                                                <div class="media-body">
                                                                                                    <div class="d-inline-block"
                                                                                                        style="width: 100% !important;text-align:left;">
                                                                                                        <h6>{{ person.area
                                                                                                        }}</h6>
                                                                                                        <p>{{ person.address
                                                                                                        }}</p>
                                                                                                        <p>{{
                                                                                                            person.googleLocation
                                                                                                        }}</p>
                                                                                                        <p>{{ person.nearBy
                                                                                                        }}</p>

                                                                                                    </div>

                                                                                                </div>
                                                                                                <!-- end media-body -->
                                                                                            </div>

                                                                                        </a>
                                                                                        <div
                                                                                            class="text-center d-flex justify-content-center align-items-center flex-column mt-4">
                                                                                            <a v-on:click="AddRow1('Billing')"
                                                                                                data-bs-dismiss="offcanvas"
                                                                                                aria-label="Close"
                                                                                                href="javascript:void(0);"
                                                                                                class="btn btn-sm btn-outline-primary mx-1">

                                                                                                Register an Address
                                                                                            </a>
                                                                                        </div>
                                                                                        <div v-if="!deliveryAddressList.some(x => x.type == 'Billing')"
                                                                                            class="text-center"> No Address
                                                                                            Registered</div>

                                                                                    </div>

                                                                                    <!--end media-body-->
                                                                                </div>
                                                                                <div class="tab-pane fade"
                                                                                    id="National_chat">
                                                                                    <div class="tab-pane fade active show"
                                                                                        id="general_chat">
                                                                                        <a href="javascript:void(0)"
                                                                                            v-for="person  in deliveryAddressList"
                                                                                            v-bind:key="person.id">
                                                                                            <div class="media new-message "
                                                                                                style="padding: 7px 10px 10px 7px !important;"
                                                                                                v-if="person.type == 'National'">
                                                                                                <div class="media-left"
                                                                                                    style="text-align: left !important;">
                                                                                                    <div
                                                                                                        class="checkbox form-check-inline d-flex">
                                                                                                        <span
                                                                                                            class="badge badge-boxed  badge-outline-primary me-2"
                                                                                                            v-bind:class="person.isDefault ? 'visibilityOn' : 'visibility'"
                                                                                                            v-show="person.isDefault">Default</span>
                                                                                                        <input
                                                                                                            type="checkbox"
                                                                                                            v-if="person.isOffice"
                                                                                                            v-model="person.isOffice"
                                                                                                            v-bind:id="person.id"
                                                                                                            v-on:change="DefaultOnList(person, false)">
                                                                                                        <input
                                                                                                            type="checkbox"
                                                                                                            v-else
                                                                                                            v-model="person.isOffice"
                                                                                                            v-bind:id="person.id"
                                                                                                            v-on:change="DefaultOnList(person, true)"><label
                                                                                                            v-bind:for="person.id"></label>
                                                                                                    </div>

                                                                                                </div>
                                                                                                <div class="media-body">
                                                                                                    <div class="d-inline-block"
                                                                                                        style="width: 100% !important;text-align:left;">
                                                                                                        <h6>{{ person.area
                                                                                                        }}</h6>
                                                                                                        <p>{{ person.address
                                                                                                        }}</p>
                                                                                                        <p>{{
                                                                                                            person.googleLocation
                                                                                                        }}</p>
                                                                                                        <p>{{ person.nearBy
                                                                                                        }}</p>

                                                                                                    </div>

                                                                                                </div>
                                                                                                <!-- end media-body -->
                                                                                            </div>

                                                                                        </a>
                                                                                        <div
                                                                                            class="text-center d-flex justify-content-center align-items-center flex-column mt-4">
                                                                                            <div>
                                                                                                <a v-on:click="AddRow1('National')"
                                                                                                    data-bs-dismiss="offcanvas"
                                                                                                    aria-label="Close"
                                                                                                    href="javascript:void(0);"
                                                                                                    class="btn btn-sm btn-outline-primary mx-1">

                                                                                                    Register an Address
                                                                                                </a>
                                                                                            </div>

                                                                                            <div
                                                                                                v-if="!deliveryAddressList.some(x => x.type == 'National')">
                                                                                                No Address Registered</div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <!--end media-body-->
                                                                                </div>

                                                                                <!--end personal chat-->
                                                                            </div>
                                                                            <!--end tab-content-->
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="simplebar-placeholder"
                                                                style="width: auto; height: 330px !important;min-height: 200px;">
                                                            </div>
                                                        </div>
                                                        <div class="simplebar-track simplebar-horizontal"
                                                            style="visibility: hidden;">
                                                            <div class="simplebar-scrollbar"
                                                                style="width: 0px; display: none;"></div>
                                                        </div>
                                                        <div class="simplebar-track simplebar-vertical"
                                                            style="visibility: visible;">
                                                            <div class="simplebar-scrollbar"
                                                                style="height: 328px !important; transform: translate3d(0px, 0px, 0px); display: block;">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6" v-if="!isDeliveryChallan">
                        <div class="row form-group" v-if="isValid('CreditInvoices') && !isDeliveryChallan">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">
                                    {{
                                        $t('AddSale.SaleOrder')
                                    }}
                                </span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <saleorderdropdown v-model="purchase.saleOrderId" :isservice="isService1"
                                    :isQuotation="purchase.isQuotation" :isDisabled="true"
                                    :modelValue="purchase.saleOrderId" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6" v-if="!isDeliveryChallan">
                        <div class="row form-group"
                            v-if="isValid('CreditInvoices') && (isValid('CanViewSaleOrder') || isValid('CanAddSaleOrder'))">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> Sale Invoice</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <sale-invoice-dropdown v-model="purchase.saleInvoiceId" :isDisabled="true"
                                    :isService="isService1" :value="purchase.saleInvoiceId" />
                            </div>
                        </div>
                    </div>


                </div>


                <DeliveryChallanItem @update:modelValue="SavePurchaseItems" :key="rander" :taxMethod="purchase.taxMethod"
                    :taxRateId="purchase.taxRateId" :isTemplate="true" :isView="isView"
                    :isDeliveryChallan="isDeliveryChallan" />
                <div class="row">
                    <div class="col-lg-12 mt-4 mb-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-5">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8" style="border-right: 1px solid rgb(238, 238, 238);">
                                        <div class="form-group pe-3">
                                            <label> {{ $t('DeliveryNote.Note') }} :</label>
                                            <textarea class="form-control" v-model="purchase.description" rows="3" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group ps-3">
                                            <div class="font-xs mb-1"> {{ $t('AddQuotation.Attachment') }}</div>
                                            <button type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"
                                                v-on:click="Attachment()">
                                                <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddQuotation.Attachment') }}
                                            </button>
                                            <div>
                                                <small class="text-muted">
                                                    {{ $t('AddQuotation.FileSize') }}
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <loading v-model:active="loading" :can-cancel="false" :is-full-page="false"></loading>
                    <div class="col-lg-12 ">
                    </div>

                </div>
                <bulk-attachment :attachmentList="purchase.attachmentList" :show="show" v-if="show"
                    @close="attachmentSave" />
            </div>
            <div class="col-lg-12 invoice-btn-fixed-bottom">
                <div v-if="!loading && purchase.id === '00000000-0000-0000-0000-000000000000'">
                    <div class="button-items">
                        <button class="btn btn-primary  mr-2" v-on:click="savePurchase('Approved')"
                            v-if="isValid('CanAddDeliveryNote') && !isView"
                            v-bind:disabled="v$.purchase.$invalid || purchase.deliveryChallanItems.filter(x => x.outOfStock).length > 0 == true ? true : false">

                            <i class="far fa-save"></i> Save
                        </button>
                        <button class="btn btn-danger  mr-2" v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>
                    </div>
                </div>
                <div v-if="!loading && purchase.id != '00000000-0000-0000-0000-000000000000'">
                    <div class="button-items">
                        <button class="btn btn-primary  mr-2" v-on:click="savePurchase('Approved')"
                            v-bind:disabled="v$.purchase.$invalid || purchase.deliveryChallanItems.filter(x => x.outOfStock).length > 0 == true ? true : false"
                            v-if="isValid('CanEditDeliveryNote') && !isView">
                            <i class="far fa-save"></i> Update
                        </button>
                        <button class="btn btn-danger  mr-2" v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>
                    </div>
                </div>

            </div>

        </div>
        <AddAddress :newaddress="newAddress" :isSale="true" :show="show1" v-if="show1" @close="show1 = false"
            @IsSave="IsSave" :type="type" />

    </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'

import moment from "moment";

import {
    required
} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
//import Vue3Barcode from 'vue3-barcode'
export default {
    mixins: [clickMixin],
    props: ['formName'],
    components: {
        Loading
    },
    setup() {
        return { v$: useVuelidate() }
    },
    data: function () {
        return {
            newAddress: {
                id: '',
                area: '',
                address: '',
                city: '',
                country: '',
                billingZipCode: '',
                latitude: '',
                langitutue: '',
                fromTime: '',
                toTime: '',
                billingPhone: '',
                deliveryHolidays: [],
                type: '',
                add: 'Add',
                isActive: false,
                isForm: false,
                isDefault: false,
                isOffice: false,
                allHour: false,
                allDaySelection: false,
                isNew: false,
            },
            type: '',
            randerCustomer: 0,
            isService1: false,
            isView: false,
            isDeliveryChallan: false,
            deliveryAddressList: [],

            daterander: 0,
            rander: 0,
            render: 0,
            multipleAddress: false,

            sale: {
                id: "00000000-0000-0000-0000-000000000000",
                customerCode: "",
                prefix: "",
                englishName: "",
                arabicName: "",
                companyNameEnglish: "",
                companyNameArabic: "",
                commercialRegistrationNo: "",
                customerIdForUpdate: "",
                vatNo: "",
                contactNo1: '',
                email: '',
                address: '',
                shippingAddress: '',
                deliveryAddress: '',
                isCashCustomer: '',

            },
            purchase: {
                id: "00000000-0000-0000-0000-000000000000",
                date: "",
                registrationNo: "",
                customerId: "",
                saleOrderId: "",
                refrence: "",
                days: '',
                purpose: "Quotation",
                for: "",
                purchaseOrder: "",
                paymentMethod: "",
                sheduleDelivery: "",
                note: '',
                description: '',
                isFreight: false,
                isLabour: false,
                isQuotation: true,
                deliveryChallanItems: [],
                attachmentList: [],
                path: '',
                clientPurchaseNo: '',
                isService: false,
                importExportItems: [],
                orderTypeId: '',
                incotermsId: '',
                commodities: '',
                natureOfCargo: '',
                attn: '',
                quotationValidDate: '',
                freeTimePOL: '',
                freeTimePOD: '',
                taxMethod: '',
                taxRateId: '',
                deliveryId: "",
                shippingId: "",
                billingId: "",
                nationalId: "",
                branchId: "",
            },
            loading: false,
            show: false,
            show1: false,
            importExportSale: false,

            itemRender: 0,
            serviceId: '',
            stuffingLocationId: '',
            portOfLoadingId: '',
            portOfDestinationId: '',
            carrierId: '',
            ft: '',
            hc: '',
            tt: '',
            etd: '',
            saleDefaultVat: '',
        };
    },

    computed: {
        isAddProductValid: function () {

            if (this.serviceId == '' || this.serviceId == null || this.serviceId == undefined || this.serviceId == '00000000-0000-0000-0000-000000000000') {
                return true
            }

            return false;
        },

    },
    validations() {
        return {
            purchase: {
                date: {
                    required
                },
                description: {},
                registrationNo: {},
                refrence: {},

                deliveryChallanItems: {
                    required
                },
            },
        }
    },
    methods: {

        DefaultOnListDelivery: function (record, value) {
            {
                if (value) {
                    this.deliveryAddressList.forEach(function (cat) {
                        if (record.id !== cat.id) {
                            if (cat.type == 'Delivery') {
                                cat.isOffice = true;

                            }
                        }
                    });
                }
                else {
                    this.deliveryAddressList.forEach(function (cat) {
                        {
                            // Set isOffice to false for other records
                            if (cat.type == 'Delivery') {
                                cat.isOffice = false;

                            }
                        }
                    });
                }

                // Loop through the delivery address list

            }
        },
        DefaultOnList: function (record, value) {
            {
                if (value) {
                    this.deliveryAddressList.forEach(function (cat) {
                        if (record.id !== cat.id) {
                            if (cat.type != 'Delivery') {
                                cat.isOffice = false;

                            }
                        } else {
                            // Set isOffice to true for the current record
                            record.isOffice = true;
                        }
                    });
                }
                else {
                    this.deliveryAddressList.forEach(function (cat) {
                        {
                            // Set isOffice to false for other records
                            if (cat.type != 'Delivery') {
                                cat.isOffice = false;

                            }
                        }
                    });
                }

                // Loop through the delivery address list

            }
        },


        AddRow1: function (type) {

            var isFirst = this.deliveryAddressList.some(x => x.type == type);
            var isDefault = this.deliveryAddressList.some(x => x.type == type && x.isDefault)

            this.newAddress = {
                id: this.createUUID(),
                area: '',
                address: '',
                city: '',
                country: '',
                contactName: '',
                contactNumber: '',
                billingZipCode: '',
                langitutue: '',
                latitude: '',
                fromTime: '',
                toTime: '',
                add: 'Add',
                billingPhone: '',
                deliveryHolidays: [],
                type: type,
                isActive: true,
                allHour: false,
                allDaySelection: false,
                isDefault: isFirst ? false : true,
                isOffice: false,
                isNew: false,
                isAlreadyDefault: isDefault,
                isFirst: isFirst ? false : true,
            };
            this.show1 = !this.show1;
            this.type = 'Add';
        },
        IsSave: function (value) {

            var root = this;

            if (value.add == 'Add' || value.add == 'Duplicate') {
                if (value.isDefault) {
                    value.isOffice = value.isDefault;
                    if (this.deliveryAddressList.length > 0) {
                        this.deliveryAddressList.forEach(function (cat) {
                            if (cat.type == value.type) {
                                cat.isDefault = false;
                                cat.isOffice = false;
                            }
                        })
                    }
                }
                if (value.add == 'Add') {
                    value.isNew = true;
                }

                this.deliveryAddressList.push(value);

            } else {
                if (value.isDefault) {
                    value.isOffice = value.isDefault;
                    if (this.deliveryAddressList.length > 0) {
                        this.deliveryAddressList.forEach(function (cat) {
                            if (cat.type == value.type && value.id != cat.id) {
                                cat.isDefault = false;
                                cat.isOffice = false;
                            }
                        })
                    }
                }

            }
            root.$swal({
                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                type: 'success',
                confirmButtonClass: "btn btn-success",
                buttonStyling: false,
                icon: 'success',
                timer: 1500,
                timerProgressBar: true,

            });

            this.show1 = false;

        },
        UpdateCustomerDetail: function () {

            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');

            }
            var customer = {
                id: this.sale.customerIdForUpdate,
                code: this.sale.customerCode,
                prefix: this.sale.prefix,
                arabicName: this.sale.arabicName,
                englishName: this.sale.englishName,
                companyNameEnglish: this.sale.companyNameEnglish,
                companyNameArabic: this.sale.companyNameArabic,
                commercialRegistrationNo: this.sale.commercialRegistrationNo,
                vatNo: this.sale.vatNo,
                contactNo1: this.sale.contactNo1,
                email: this.sale.email,
                billingAddress: this.sale.address,
                shippingAddress: this.sale.shippingAddress,
                isUpdate: true,
                isCustomer: true,
            }
            customer.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;
            if (this.deliveryAddressList != null && this.deliveryAddressList.length > 0) {
                customer.deliveryAddressList = this.deliveryAddressList
            }

            root.$https
                .post('/Contact/SaveContact', customer, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(response => {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                    } else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'There is something wrong. Please contact to support.' : 'هناك شيء ما خاطئ. يرجى الاتصال للدعم.',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }

                })
                .catch(error => {
                    console.log(error)
                    root.$swal.fire({
                        icon: 'error',
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: error.response.data,
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });

                    root.loading = false
                })
                .finally(() => root.loading = false)
        },
        emptyCashCustomer: function (customerId, advanceAccountId, customerDetail) {
            console.log(customerId, advanceAccountId)

            if (customerDetail != null && customerDetail != undefined && customerDetail != '') {
                this.isWalkIn = false;
                this.sale.customerCode = customerDetail.code;
                this.sale.name = customerDetail.name;
                this.sale.prefix = customerDetail.prefix;
                this.sale.englishName = customerDetail.englishName;
                this.sale.arabicName = customerDetail.arabicName;
                this.sale.companyNameEnglish = customerDetail.companyNameEnglish;
                this.sale.companyNameArabic = customerDetail.companyNameArabic;
                this.sale.commercialRegistrationNo = customerDetail.commercialRegistrationNo;
                this.sale.customerIdForUpdate = customerDetail.id;
                this.sale.vatNo = customerDetail.vatNo;
                this.sale.contactNo1 = customerDetail.contactNo1;
                this.sale.email = customerDetail.email;
                this.sale.address = customerDetail.address;
                this.sale.shippingAddress = customerDetail.shippingAddress;
                this.sale.isCashCustomer = customerDetail.isCashCustomer;
                this.deliveryAddressList = customerDetail.deliveryAddressList;

                if(this.purchase.deliveryId !=null && this.purchase.deliveryId!=''&& this.purchase.deliveryId!=undefined )
                {

                    let  id=this.purchase.deliveryId;
                            this.deliveryAddressList.forEach(function (cat) {
                                if (id== cat.id && cat.type == 'Delivery') {
                                    cat.isOffice = true;
                                } 
                            });


                }


                if(this.purchase.billingId !=null && this.purchase.billingId!=''&& this.purchase.billingId!=undefined )
                        {
                            let  id=this.purchase.billingId;
                            this.deliveryAddressList.forEach(function (cat) {
                                if (id== cat.id) {
                                    cat.isOffice = true;
                                } else {
                                    if (cat.type != 'Delivery') {
                                            cat.isOffice = false;

                                        }


                                }
                            });



                        }


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

        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },
        Attachment: function () {
            this.show = true;
        },

        attachmentSave: function (attachment) {
            this.purchase.attachmentList = attachment;
            this.show = false;
        },

        RanderCustomer: function () {
            this.randerCustomer++;
        },

        AutoIncrementCode: function () {

            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.IsService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
            var service = false
            if (this.isService1) {
                service = true;
            }
            if (this.IsService) {
                service = true;
            }
            root.$https
                .get('/Purchase/DeliveryChallanAutoGenerateNo?IsService=' + service + '&branchId=' + localStorage.getItem('BranchId'), {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                    if (response.data != null) {
                        root.purchase.registrationNo = response.data;
                    }
                });
        },
        SavePurchaseItems: function (deliveryChallanItems) {

            this.purchase.deliveryChallanItems = deliveryChallanItems;
        },
        savePurchase: function (status) {
            this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

            if (this.multipleAddress) {
                if (this.deliveryAddressList.length > 0) {
                    var defaultAddress = this.deliveryAddressList.find(x => x.type == 'Delivery' && x.isOffice);
                    if (defaultAddress != null) {
                        this.purchase.deliveryId = defaultAddress.id;
                    }
                    else
                        {
                            this.purchase.deliveryId ='';
                        }

                    // var shippingAddress = this.deliveryAddressList.find(x => x.type == 'Shipping' && x.isOffice);
                    // if (shippingAddress != null) {
                    //     this.purchase.shippingId = shippingAddress.id;
                    // }
                    var billingAddress = this.deliveryAddressList.find(x =>x.isOffice && x.type != 'Delivery');
                    if (billingAddress != null) {
                        this.purchase.billingId = billingAddress.id;
                    }
                    else
                        {
                            this.purchase.billingId  ='';
                        }

                    // var nationalAddress = this.deliveryAddressList.find(x => x.type == 'National' && x.isOffice);
                    // if (nationalAddress != null) {
                    //     this.purchase.nationalId = nationalAddress.id;
                    // }

                }

            }




            this.purchase.branchId = localStorage.getItem('BranchId');

            this.purchase.approvalStatus = status;
            this.purchase.isDeliveryChallan = this.isDeliveryChallan;
            this.purchase.deliveryAddress = this.purchase.address;
            this.IsService = localStorage.getItem('IsService') == 'true' ? true : false;

            if (this.IsService) {
                this.purchase.isService = true;
            }

            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.$https
                .post('/Purchase/SaveDeliveryChallanInformation', root.purchase, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(response => {
                    root.loading = false
                    root.info = response.data

                    root.$swal({
                        title: "Saved!",
                        text: "Data Saved Successfully!",
                        type: 'success',
                        icon: 'success',
                        timer: 1500,
                        timerProgressBar: true,
                    }).then(function (response) {
                        if (response != undefined) {

                            if (root.isDeliveryChallan) {
                                root.$router.push('/DeliveryChallan');

                            } else if (root.$route.query.isSaleOrder == 'true' && root.$route.query.isService == 'true') {
                                if (root.formName == 'Quotation') {
                                    root.$router.push({
                                        path: '/SaleServiceOrder',
                                        query: {
                                            formName: 'Quotation',
                                        }
                                    });
                                }
                                if (root.formName == 'SaleOrder') {
                                    root.$router.push({
                                        path: '/SaleServiceOrder',
                                        query: {
                                            formName: 'SaleOrder',
                                        }
                                    });
                                }
                                if (root.formName == 'ServiceQuotation') {
                                    root.$router.push({
                                        path: '/SaleServiceOrder',
                                        query: {
                                            formName: 'ServiceQuotation',
                                        }
                                    });
                                }
                                if (root.formName == 'ServiceSaleOrder') {
                                    root.$router.push({
                                        path: '/SaleServiceOrder',
                                        query: {
                                            formName: 'ServiceSaleOrder',
                                        }
                                    });
                                }
                            } else if (root.$route.query.isSaleOrder == 'false' && root.$route.query.isService == 'true') {
                                root.$router.push('/SaleService');
                            } else if (root.$route.query.isSaleOrder == 'true') {
                                if (root.$route.query.isSaleOrder == 'true') {
                                    root.$router.push('/SaleOrder');
                                } else {
                                    root.$router.push('/SaleService');
                                }
                            } else {
                                if (root.$route.query.isSaleOrder == 'true') {
                                    root.$router.push('/SaleOrder');
                                } else {
                                    root.$router.push('/SaleService');
                                }
                            }
                        }
                    });

                })
                .catch(error => {
                    
                    console.log(error);
                    if (localStorage.getItem('IsMultiUnit') == 'true') {
                        root.purchase.deliveryChallanItems.forEach(function (x) {

                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));

                        });
                    }
                    root.$swal.fire({
                        icon: 'error',
                        title: 'Something Went Wrong!',
                        text: error.response.data,
                    });

                    root.loading = false
                })
                .finally(() => root.loading = false)

        },

        goToPurchase: function () {
            var root = this;
            if (this.isDeliveryChallan) {
                this.$router.push('/DeliveryChallan');

            } else if (this.$route.query.isSaleOrder == 'true' && this.$route.query.isService == 'true') {
                if (this.formName == 'Quotation') {
                    root.$router.push({
                        path: '/SaleServiceOrder',
                        query: {
                            formName: 'Quotation',
                        }
                    });
                }
                if (this.formName == 'SaleOrder') {
                    root.$router.push({
                        path: '/SaleServiceOrder',
                        query: {
                            formName: 'SaleOrder',
                        }
                    });
                }
                if (this.formName == 'ServiceQuotation') {
                    root.$router.push({
                        path: '/SaleServiceOrder',
                        query: {
                            formName: 'ServiceQuotation',
                        }
                    });
                }
                if (this.formName == 'ServiceSaleOrder') {
                    root.$router.push({
                        path: '/SaleServiceOrder',
                        query: {
                            formName: 'ServiceSaleOrder',
                        }
                    });
                }
            } else if (this.$route.query.isSaleOrder == 'false' && this.$route.query.isService == 'true') {
                this.$router.push('/SaleService');
            } else if (this.$route.query.isSaleOrder == 'true') {
                this.$router.push('/SaleOrder');
            } else {
                this.$router.push('/SaleService');
            }

        },
    },
    created: function () {
        var root = this;
        
        if (root.$route.query.Add == 'false') {
            root.$route.query.data = root.$store.isGetEdit;
            console.log(root.$route.query.data)
        }
        root.$emit('update-modelValue', root.$route.name);
        root.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

        if (root.$route.query.data != undefined) {

            if (root.$route.query.isService == 'true') {
                root.isService = true;
            }
            if (root.$route.query.isDeliveryChallan == 'true') {

                root.isDeliveryChallan = true;

            }
            if (root.$route.query.isView == 'true') {
                root.isView = true;
            }
            if (root.$route.query.Edit == 'true') {
                console.log("nothing")
            }
            else {
                if (root.$route.query.Add == 'false') {

                    root.AutoIncrementCode();

                } else {
                    root.purchase.bilingAddress = root.$route.query.data.bilingAddress;
                }
            }


            root.purchase = root.$route.query.data;

            if (root.$route.query.isSaleOrder == 'true') {

                if (root.$route.query.Add == 'false') {
                    root.purchase.saleOrderId = root.purchase.id;
                    root.purchase.id = "00000000-0000-0000-0000-000000000000";
                    root.purchase.bilingAddress = true
                    root.$route.query.data.saleOrderItems = root.$route.query.data.saleOrderItems.filter(function (list_item) {
                        return list_item.quantity != 0
                    });
                    root.purchase.deliveryChallanItems = root.$route.query.data.saleOrderItems

                } else {
                    if (root.purchase.bilingAddress) {
                        root.purchase.customerBilingAddress = root.purchase.customerAddress;

                    } else {
                        root.purchase.customerShippingAddress = root.purchase.customerAddress;

                    }
                }

            }
            else {
                if (root.$route.query.Add == 'true') {
                    root.purchase.isQuotation = false;

                    root.$route.query.data.saleItems = root.$route.query.data.saleItems.filter(function (list_item) {
                        return list_item.quantity != 0
                    });

                    root.purchase.saleInvoiceId = root.purchase.id;
                    root.purchase.customerBilingAddress = root.purchase.customerAddress;
                    root.purchase.customerShippingAddress = root.purchase.shippingAddress;
                    root.purchase.deliveryChallanItems = root.$route.query.data.saleItems
                    root.purchase.bilingAddress = true
                    root.purchase.id = "00000000-0000-0000-0000-000000000000";

                } else {

                    if (root.purchase.bilingAddress) {
                        root.purchase.customerBilingAddress = root.purchase.customerAddress;

                    } else {
                        root.purchase.customerShippingAddress = root.purchase.customerAddress;

                    }
                }

            }
            if (root.isService1) {
                root.purchase.isService = true;
            } else {
                root.purchase.isService = false;
            }

            root.purchase.date = moment(root.purchase.date).format('llll');

            root.attachment = true;
            root.rander++;
            root.render++;
            root.rendered++;
        }
        else {

            root.IsService = localStorage.getItem('IsService') == 'true' ? true : false;

            if (root.$route.query.Add == 'true') {

                root.AutoIncrementCode();

            }
            if (root.$route.query.isDeliveryChallan == 'true') {

                root.isDeliveryChallan = true;

            }

        }

    },
    mounted: function () {
        this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

        this.purchase.date = moment().format('llll');
        this.daterander++;
    },
};
</script>
