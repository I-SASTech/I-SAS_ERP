<template>
    <div class="row"
        v-if="isValid('CanViewDraftOrder') || isValid('CanViewInProcessOrder') || isValid('CanAddPurchaseOrder') || isValid('CanEditPurchaseOrder') || isValid('CanAddSupplierQuotation') || isValid('CanEditSupplierQuotation')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col d-flex align-items-baseline">
                    <div class="media">
                        <span class="circle-singleline" style="background-color: #1761FD !important;"
                            v-if="formName == 'SupplierQuotation'">SQ</span>
                        <span class="circle-singleline" style="background-color: #1761FD !important;" v-else>PO</span>
                        <div class="media-body align-self-center ms-3">

                            <h6 v-if="formName == 'SupplierQuotation'" class="m-0 font-20">
                                {{
                                    $t('PurchaseOrder.SupplierQuotation')
                                }} <span class="mx-2" style="font-size: 13px !important;">{{ purchase.date }}</span>
                            </h6>
                            <h6 v-else class="m-0 font-20">
                                {{ $t('PurchaseOrder.PurchaseOrder') }} <span class="mx-2"
                                    style="font-size: 13px !important;">{{ purchase.date }}</span>
                            </h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;">
                                    {{ purchase.registrationNo }}
                                </p>
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
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row">
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchaseOrder.Supplier') }}: <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <supplierdropdown v-model="v$.purchase.supplierId.$model"
                                :disable="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'"
                                :modelValue="purchase.supplierId" :status="purchase.isRaw" v-bind:key="supplierRender" />
                            <a v-if="purchase.supplierId != null && purchase.supplierId != ''"
                                v-on:click="GetSupplierDetails()" href="javascript:void(0);" data-bs-toggle="offcanvas"
                                ref="offcanvasRight" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"
                                class="text-primary mt-2">Supplier Detail</a>
                            <a v-else href="javascript:void(0);" class="text-secondary mt-2">
                                Supplier Detail
                            </a>
                            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                                aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                                <div class="offcanvas-header">
                                    <h5 id="offcanvasRightLabel" class="m-0">Supplier Detail</h5>
                                    <button
                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'margin-left:257px !important' : 'margin-left:0px !important'"
                                        type="button" class="btn btn-outline-primary"
                                        @click="UpdateCustomerDetail(sale.customerIdForUpdate)">
                                        {{
                                            $t('AddSale.Update')
                                        }}
                                    </button>
                                    <button
                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'"
                                        type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas"
                                        aria-label="Close"></button>
                                </div>
                                <div class="offcanvas-body">
                                    <div class="row">
                                        <div class="col-lg-12 form-group">
                                            <label> Supplier ID:</label>
                                            <input type="text" class="form-control" readonly v-model="sale.code" />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('Display Name') }} :</label>
                                            <input type="text" class="form-control" readonly
                                                v-model="sale.customerDisplayName" />
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
                                            <input type="text" class="form-control" v-model="sale.commercialRegistrationNo"
                                                disabled />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddCustomer.VAT/NTN/Tax No') }} :</label>
                                            <input type="text" class="form-control" v-model="sale.vatNo" disabled />
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
                                            <label>Supplier Address :</label>
                                            <textarea rows="3" v-model="sale.billingAddress"
                                                class="form-control"> </textarea>
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
                                                                                                        v-on:change="DefaultOnList(person, false)">
                                                                                                    <input type="checkbox"
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
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">

                        </label>
                        <div class="inline-fields col-lg-8">
                            <a href="javascript:void(0);" class="text-primary" v-on:click="VatInputValues()">
                                {{ $t('AddStockValue.Discount&Vat/Taxoptions') }}
                            </a>
                        </div>
                    </div>
                    <div class="row" v-if="isVATInput">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">
                                    {{
                                        $t('AddSale.DiscountType')
                                    }}
                                </span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <multiselect
                                    :options="[$t('AddStockValue.AtTransactionLevel'), $t('AddStockValue.AtLineItemLevel')]"
                                    @update:modelValue="ChangeVat(discountTypeOption, 'DiscountType')"
                                    v-model="discountTypeOption"
                                    @select="purchase.isDiscountOnTransaction = (discountTypeOption === 'At Transaction Level' ? false : true)"
                                    :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </div>
                        </div>
                        <div class="row form-group"
                            v-if="saleDefaultVat == 'DefaultVatHead' || saleDefaultVat == 'DefaultVatHeadItem'">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">
                                    {{ $t('AddPurchase.TaxMethod') }}
                                    :<span class="text-danger"> *</span>
                                </span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                    :options="['Inclusive', 'Exclusive']"
                                    @update:modelValue="ChangeVat(purchase.taxMethod, 'TaxMethod')"
                                    @click="purchase.isFixed = false" v-model="purchase.taxMethod" :show-labels="false"
                                    v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                                <multiselect v-else :options="['شامل', 'غير شامل']"
                                    @update:modelValue="ChangeVat(purchase.taxMethod, 'TaxMethod')"
                                    v-model="purchase.taxMethod" @select="purchase.isFixed = false" :show-labels="false"
                                    v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                    v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </div>
                        </div>

                        <div class="row form-group"
                            v-if="saleDefaultVat == 'DefaultVatHead' || saleDefaultVat == 'DefaultVatHeadItem'">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.VAT%') }} :<span
                                        class="text-danger"> *</span></span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <taxratedropdown v-model="purchase.taxRateId" v-bind:modelValue="purchase.taxRateId"
                                    @update:modelValue="ChangeVat(purchase.taxRateId, 'TaxRateId')" :key="customerRender" />
                            </div>
                        </div>
                    </div>

                    <!-- <div class="row form-group"
                        v-if="formName != 'SupplierQuotation' && isValid('CanViewSupplierQuotation')">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchaseOrder.SupplierQuotation') }}:
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <purchase-order-dropdown v-model="purchase.supplierQuotationId"
                                @update:modelValue="GetPoData(purchase.supplierQuotationId, false)"
                                :supplierQuotation="'supplierQuotation'" :modelValue="purchase.supplierQuotationId" />
                        </div>
                    </div> -->
                </div>

                <div class="col-lg-6">

                    <a v-if="purchase.supplierId != null && purchase.supplierId != ''" href="javascript:void(0);"
                        data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight"
                        class="text-primary">
                        Options
                    </a>
                    <a v-else href="javascript:void(0);" class="text-secondary">Options</a>
                    <div class="row" v-bind:key="randerEffect">
                        <div class="col-md-12" v-if="selectedValue1 != '' && selectedValue1 != null"
                            :key="canvasSelectValueRender">
                            <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                <span>{{ selectedValue1 }}</span>
                                <span style="position:absolute; right: -12px; top: -8px;">
                                    <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                        style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                        @click="RemoveEffect('RemoveItems')">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </span>
                            </div>
                        </div>

                        <div class="col-lg-12 pt-2" v-if="purchase.invoiceNo != '' && purchase.invoiceNo != null">
                            <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                <span>
                                    {{ $t('AddPurchaseOrder.SupplierQuotationNumber') }} :- {{
                                        purchase.invoiceNo
                                    }}
                                </span>
                                <span style="position:absolute; right: -12px; top: -8px;">
                                    <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                        style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                        @click="RemoveEffect('invoiceNo')">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </span>
                            </div>
                        </div>

                        <div class="col-lg-12 pt-2" v-if="purchase.invoiceDate != '' && purchase.invoiceDate != null">
                            <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                <span>
                                    {{ $t('AddPurchaseOrder.QuotationDate') }} :- {{
                                        purchase.invoiceDate
                                    }}
                                </span>
                                <span style="position:absolute; right: -12px; top: -8px;">
                                    <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                        style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                        @click="RemoveEffect('invoiceDate')">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-12 pt-2" v-if="purchase.reference != '' && purchase.reference != null">
                            <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                <span>
                                    {{ $t('Reference') }} :- {{
                                        purchase.reference
                                    }}
                                </span>
                                <span style="position:absolute; right: -12px; top: -8px;">
                                    <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                        style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                        @click="RemoveEffect('reference')">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </span>
                            </div>
                        </div>

                        <div class="col-lg-12 pt-2" v-if="purchase.isRaw">
                            <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                <span>
                                    {{ $t('AddPurchase.RawProduct') }} :- {{
                                        purchase.isRaw
                                    }}
                                </span>
                                <span style="position:absolute; right: -12px; top: -8px;">
                                    <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                        style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('isRaw')">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </span>
                            </div>
                        </div>



                    </div>

                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight2"
                        aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">Options</h5>
                            <button
                                v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'"
                                type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas"
                                aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <div class="row">
                                <div class="col-md-12 mb-2" v-if="selectedValue != '' && selectedValue != null"
                                    :key="canvasSelectValueRender">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ selectedValue }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect('CanvasSelectValue')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>

                                <div class="col-md-7"
                                    v-if="formName != 'SupplierQuotation' && isValid('CanViewSupplierQuotation')">
                                    <div class="row">
                                        <div class="col-lg-6 form-group text-right">
                                            <b>{{ $t('PurchaseOrder.SupplierQuotation') }} </b>
                                        </div>
                                        <div class="col-lg-6 form-group text-left">
                                            <button v-if="expandSupplierQuotation"
                                                v-on:click="ExpandSupplierQuotation(false)" type="button"
                                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                <i class="ti-angle-double-up"></i>
                                            </button>
                                            <button v-else v-on:click="ExpandSupplierQuotation(true)" type="button"
                                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                <i class="ti-angle-double-down"></i>
                                            </button>
                                        </div>
                                        <div v-if="expandSupplierQuotation" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in supplierQuotationList" v-bind:key="index"
                                                style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);"
                                                    v-on:click="GetSQId(saleValue.id, saleValue.registrationNumber, saleValue.date, saleValue.netAmount)">
                                                    <span>
                                                        {{ index + 1 }}-{{ saleValue.registrationNumber }}--{{
                                                            saleValue.date
                                                        }}
                                                    </span>
                                                    <span class="float-end">
                                                        {{ currency }} {{
                                                            parseFloat(saleValue.netAmount).toFixed(3).slice(0,
                                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                                        }}
                                                    </span>
                                                </a>
                                                <br />
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12 text-end mt-2 mb-2"
                                    v-if="formName != 'SupplierQuotation' && isValid('CanViewSupplierQuotation')">
                                    <div class="button-items">
                                        <button class="btn btn-outline-primary"
                                            v-bind:disabled="(supplierQuotationId == '')"
                                            v-on:click="GetPoData(supplierQuotationId, false)">
                                            <i class="far fa-save "></i>
                                            {{ $t('AddCustomer.btnSave') }}
                                        </button>
                                        <button type="button" v-on:click="RemoveEffect('purchaseInoiceId')"
                                            class="btn btn-danger" data-bs-dismiss="offcanvas" aria-label="Close">
                                            Cancel
                                        </button>
                                    </div>
                                </div>

                                <div class="accordion" id="accordionExample">
                                    <div class="accordion-item">
                                        <h5 class="accordion-header m-0" id="headingOne">
                                            <button class="accordion-button fw-semibold" type="button"
                                                data-bs-toggle="collapse" data-bs-target="#collapseOne"
                                                aria-expanded="false" aria-controls="collapseOne">
                                                Additional Options
                                            </button>
                                        </h5>
                                        <div id="collapseOne" class="accordion-collapse collapse "
                                            aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-lg-6 form-group">
                                                        <label class="col-form-label">
                                                            <span class="tooltip-container text-dashed-underline "
                                                                v-if="formName == 'SupplierQuotation'">
                                                                {{ $t('SQ No. & Date') }}: <span
                                                                    class="text-danger">*</span>
                                                            </span>
                                                            <span class="tooltip-container text-dashed-underline " v-else>
                                                                {{ $t('AddPurchaseOrder.SupplierQuotationNumber') }}: <span
                                                                    class="text-danger">*</span>
                                                            </span>
                                                        </label>
                                                        <div class="inline-fields">
                                                            <input
                                                                v-bind:disabled="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'"
                                                                v-model="additionalOptions.invoiceNo" class="form-control"
                                                                type="text">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="formName != 'SupplierQuotation'">
                                                        <label class="col-form-label">
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{ $t('AddPurchaseOrder.QuotationDate') }}: <span
                                                                    class="text-danger">*</span>
                                                            </span>
                                                        </label>
                                                        <div class="inline-fields">
                                                            <datepicker v-model="additionalOptions.invoiceDate"
                                                                :key="randerEffect" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 form-group"
                                                        v-if="isValid('CanViewAutoTemplate') && formName != 'SupplierQuotation'">
                                                        <label class="col-form-label">
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{ $t('AddPurchaseOrder.PurchaseTemplate') }} :
                                                            </span>
                                                        </label>
                                                        <div class="inline-fields">
                                                            <auto-purchase-template-dropdown
                                                                v-model="purchase.purchaseTemplateId"
                                                                @update:modelValue="GetPurchaseTemplate(purchase.purchaseTemplateId)" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 form-group">
                                                        <label class="col-form-label">
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{ $t('Reference') }}:
                                                            </span>

                                                        </label>
                                                        <div class="inline-fields">
                                                            <input
                                                                v-bind:disabled="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'"
                                                                v-model="additionalOptions.reference" class="form-control"
                                                                type="text">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12 form-group" v-if="raw == 'true'">
                                                        <label class="col-form-label">
                                                        </label>
                                                        <div class="inline-fields">
                                                            <div class="checkbox form-check-inline mx-2">
                                                                <input type="checkbox" id="inlineCheckbox1"
                                                                    v-model="additionalOptions.isRaw"
                                                                    @change="ChangeSupplier">
                                                                <label for="inlineCheckbox1">
                                                                    {{
                                                                        $t('AddPurchaseReturn.RawProduct')
                                                                    }}
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 text-end mt-2 mb-2">
                                                        <div class="button-items">
                                                            <button class="btn btn-outline-primary"
                                                                v-bind:disabled="(additionalOptions.invoiceDate == '') && (additionalOptions.invoiceNo == '') && !additionalOptions.isRaw"
                                                                v-on:click="SaveCanvasData('Additional')">
                                                                <i class="far fa-save "></i>
                                                                {{ $t('AddCustomer.btnSave') }}
                                                            </button>
                                                            <button type="button"
                                                                v-on:click="RemoveCanvasData('Additional')"
                                                                class="btn btn-danger " data-bs-dismiss="offcanvas"
                                                                aria-label="Close">
                                                                Cancel
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 text-end mt-2 mb-4">
                                        <div class="button-items">

                                            <button type="button" class="btn btn-danger " data-bs-dismiss="offcanvas"
                                                aria-label="Close">
                                                Close Options
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <purchase-item @update:modelValue="SavePurchaseItems" ref="childComponentRef"
                    :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" :raw="purchase.isRaw" :isSerial="true"
                    :po="true" :purchaseid="purchase.id" :key="rander" :purchaseOrderId="purchase.purchaseOrderId"
                    @discountChanging="updateDiscountChanging" :adjustmentProp="purchase.discount"
                    :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction"
                    :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed"
                    :purchaseItems="purchase.purchaseOrderItems" :isBeforeTax="purchase.isBeforeTax"
                    @summary="updateSummary" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items" v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">
                        <button class="btn btn-outline-primary mx-2  " v-if="isValid('CanViewDraftOrder')"
                            v-on:click="savePurchase('Draft')"
                            v-bind:disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchaseOrder.SaveAsDraft') }}
                        </button>

                        <button class="btn btn-outline-primary mx-2 " v-on:click="savePurchase('InProcess')"
                            v-if="isValid('CanViewInProcessOrder') && isValid('CanAllowOrderVersion') && formName != 'SupplierQuotation'"
                            v-bind:disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchaseOrder.ConfirmAsInProcess') }}
                        </button>

                        <button class="btn btn-outline-primary mx-2" v-on:click="savePurchase('Approved')"
                            v-if="isValid('CanAddPurchaseOrder')"
                            :disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchaseOrder.SaveAsPost') }}
                        </button>

                        <button class="btn btn-danger " v-on:click="goToPurchase">
                            {{ $t('AddPurchaseOrder.Cancel') }}
                        </button>
                    </div>
                    <div class="button-items"
                        v-if="purchase.approvalStatus === 4 && purchase.id != '00000000-0000-0000-0000-000000000000'">
                        <button class="btn btn-outline-primary mx-2" v-on:click="savePurchase('Draft')"
                            v-if="isValid('CanViewDraftOrder') && isValid('CanEditPurchaseOrder')"
                            :disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchaseOrder.UpdateAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary mx-2" v-on:click="savePurchase('InProcess')"
                            v-if="isValid('CanViewInProcessOrder') && isValid('CanAllowOrderVersion') && isValid('CanEditPurchaseOrder') && formName != 'SupplierQuotation'"
                            :disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchaseOrder.UpdateInProcess') }}
                        </button>
                        <button class="btn btn-outline-primary mx-2" v-on:click="savePurchase('Approved')"
                            v-if="isValid('CanAddPurchaseOrder') && isValid('CanEditPurchaseOrder')"
                            :disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchaseOrder.UpdateAsPost') }}
                        </button>
                        <button class=" btn btn-danger" v-on:click="goToPurchase">
                            {{ $t('AddPurchaseOrder.Cancel') }}
                        </button>
                    </div>
                    <div v-if="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'">
                        <button class="btn btn-outline-primary mx-2" v-on:click="savePurchase('InProcess')"
                            v-if="isValid('CanViewInProcessOrder') && isValid('CanAllowOrderVersion') && isValid('CanEditPurchaseOrder')"
                            :disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            {{ $t('AddPurchaseOrder.UpdateInProcess') }}
                        </button>
                        <button class="btn btn-outline-primary mx-2 " v-on:click="savePurchase('Approved')"
                            v-if="isValid('CanAddPurchaseOrder') || isValid('CanEditPurchaseOrder')"
                            :disabled="v$.purchase.$invalid || purchase.purchaseOrderItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchaseOrderItems.filter(x => x.unitPrice == '').length > 0">
                            {{ $t('AddPurchaseOrder.UpdateAsPost') }}
                        </button>
                        <button class="btn btn-danger mx-2" v-on:click="goToPurchase">
                            {{ $t('AddPurchaseOrder.Cancel') }}
                        </button>
                    </div>
                </div>

                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                    <div class="form-group pe-3">
                                        <label>{{ $t('AddPurchaseOrder.TermandCondition') }}:</label>
                                        <textarea class="form-control " rows="3" v-model="purchase.note" />
                                    </div>
                                </div>
                                <div class="col-lg-4" v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddPurchaseOrder.AttachFiles') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                            class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                You can upload a maximum of 10 files, 5MB each
                                            </small>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4"
                                    v-if="purchase.approvalStatus === 4 && purchase.id != '00000000-0000-0000-0000-000000000000'">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddPurchaseOrder.AttachFiles') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                            class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                You can upload a maximum of 10 files, 5MB each
                                            </small>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4"
                                    v-if="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddPurchaseOrder.AttachFiles') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                            class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                You can upload a maximum of 10 files, 5MB each
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="accordion mt-3 mb-5" id="accordionExample1"
                                    v-if="(purchase.approvalStatus === 5 || purchase.approvalStatus === 9) && purchase.id != '00000000-0000-0000-0000-000000000000' && internationalPurchase == 'true'">
                                    <div class="accordion-item"
                                        v-if="isValid('CanUploadAttachment') || isValid('CanDownloadAttachment')">
                                        <h5 class="accordion-header m-0" id="headingOne1">
                                            <button class="accordion-button collapsed fw-semibold" type="button"
                                                data-bs-toggle="collapse" data-bs-target="#collapseOne1"
                                                aria-expanded="false" aria-controls="collapseOne1">
                                                {{ $t('AddPurchaseOrder.Attachment') }}
                                            </button>
                                        </h5>
                                        <div id="collapseOne1" class="accordion-collapse collapse "
                                            aria-labelledby="headingOne1" data-bs-parent="#accordionExample1">
                                            <div class="accordion-body">
                                                <import-attachment :purchase="purchase" :show="attachment" v-if="attachment"
                                                    @close="attachmentSave" :document="'Purchase'" />
                                                <div class="row">
                                                    <div class="col-sm-12  form-group"
                                                        v-if="isValid('CanUploadAttachment')">
                                                        <a href="javascript:void(0)" class="btn btn-outline-primary"
                                                            v-on:click="attachment = true">
                                                            {{ $t('AddPurchaseOrder.Upload') }}
                                                        </a>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div class=" table-responsive">
                                                            <table class="table ">
                                                                <thead class="thead-light m-0">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>{{ $t('AddPurchaseOrder.Date') }} </th>
                                                                        <th>{{ $t('AddPurchaseOrder.Description') }} </th>
                                                                        <th v-if="isValid('CanDownloadAttachment')">
                                                                            {{
                                                                                $t('AddPurchaseOrder.Attachment')
                                                                            }}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(contact, index) in purchase.purchaseAttachments"
                                                                        v-bind:key="index">
                                                                        <td>
                                                                            {{ index + 1 }}
                                                                        </td>
                                                                        <th>{{ getDate(contact.date) }}</th>
                                                                        <th>{{ contact.description }}</th>

                                                                        <td v-if="isValid('CanDownloadAttachment')">
                                                                            <button class="btn btn-primary  btn-icon mr-2"
                                                                                v-if="contact.path != ''"
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
                                    <div class="accordion-item"
                                        v-if="isValid('CanAddOrderAction') || isValid('CanViewOrderAction')">
                                        <h5 class="accordion-header m-0" id="headingTwo2">
                                            <button class="accordion-button collapsed fw-semibold" type="button"
                                                data-bs-toggle="collapse" data-bs-target="#collapseTwo2"
                                                aria-expanded="false" aria-controls="collapseTwo2">
                                                {{ $t('AddPurchaseOrder.Actions') }}
                                            </button>
                                        </h5>
                                        <div id="collapseTwo2" class="accordion-collapse collapse"
                                            aria-labelledby="headingTwo2" data-bs-parent="#accordionExample1">
                                            <div class="accordion-body">
                                                <add-company-action :action="action" :show="show" v-if="show"
                                                    @close="IsSave" :document="'Purchase'" />
                                                <div class="row">
                                                    <div class="col-sm-12 form-group" v-if="isValid('CanAddOrderAction')">
                                                        <a href="javascript:void(0)" class="btn btn-outline-primary"
                                                            v-on:click="show = true">
                                                            {{ $t('AddPurchaseOrder.Action') }}
                                                        </a>
                                                    </div>

                                                    <div class="col-sm-12">
                                                        <div class=" table-responsive" v-if="isValid('CanViewOrderAction')">
                                                            <table class="table ">
                                                                <thead class="thead-light m-0">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>{{ $t('AddPurchaseOrder.Status') }}</th>
                                                                        <th>{{ $t('AddPurchaseOrder.Date') }} </th>
                                                                        <th>
                                                                            {{ $t('AddPurchaseOrder.Description/Reason') }}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(process, index) in purchase.actionProcess"
                                                                        v-bind:key="process.id">
                                                                        <td>
                                                                            {{ index + 1 }}
                                                                        </td>
                                                                        <th>
                                                                            <span class="badge badge-primary">
                                                                                {{
                                                                                    process.processName
                                                                                }}
                                                                            </span>
                                                                        </th>
                                                                        <th>{{ getDate(process.date) }}</th>
                                                                        <th>{{ process.description }}</th>

                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="accordion-item"
                                        v-if="isValid('CanAddAdvancePayment') || isValid('CanViewAdvancePayment') || isValid('CanViewDetailAdvancePayment')">
                                        <h5 class="accordion-header m-0" id="headingTwo3">
                                            <button class="accordion-button collapsed fw-semibold" type="button"
                                                data-bs-toggle="collapse" data-bs-target="#collapseTwo3"
                                                aria-expanded="false" aria-controls="collapseTwo3">
                                                {{ $t('AddPurchaseOrder.Payment') }}
                                            </button>
                                        </h5>
                                        <div id="collapseTwo3" class="accordion-collapse collapse"
                                            aria-labelledby="headingTwo3" data-bs-parent="#accordionExample1">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-sm-12 form-group"
                                                        v-if="isValid('CanAddAdvancePayment')">
                                                        <a href="javascript:void(0)" class="btn btn-outline-primary"
                                                            v-on:click="payment = true">
                                                            {{ $t('AddPurchaseOrder.AddPayment') }}
                                                        </a>
                                                    </div>

                                                    <div class="col-sm-12">
                                                        <purchaseorder-payment :totalAmount="totalAmount"
                                                            :customerAccountId="advanceAccountId" :show="payment"
                                                            v-if="payment" @close="paymentSave" :isPurchase="'true'"
                                                            :isSaleOrder="'false'" :purchaseOrderId="purchase.id"
                                                            :formName="'AdvancePay'" />

                                                        <div class=" table-responsive"
                                                            v-if="isValid('CanViewAdvancePayment')">
                                                            <table class="table ">
                                                                <thead class="m-0 thead-light">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th style="width:20%;">
                                                                            {{
                                                                                $t('AddPurchaseOrder.Date')
                                                                            }}
                                                                        </th>
                                                                        <th class="text-right">
                                                                            {{
                                                                                $t('AddPurchaseOrder.Amount')
                                                                            }}
                                                                        </th>
                                                                        <th class="text-center">
                                                                            {{
                                                                                $t('AddPurchaseOrder.PaymentMode')
                                                                            }}
                                                                        </th>
                                                                        <th>{{ $t('AddPurchaseOrder.Description') }} </th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(payment, index) in purchase.paymentVoucher"
                                                                        v-bind:key="index">
                                                                        <td>
                                                                            {{ index + 1 }}
                                                                        </td>
                                                                        <th>{{ getDate(payment.date) }}</th>
                                                                        <th class="text-right">
                                                                            {{ currency }}
                                                                            {{
                                                                                parseFloat(payment.amount).toFixed(3).slice(0,
                                                                                    -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                                        "$1,")
                                                                            }}
                                                                        </th>
                                                                        <th class="text-center">
                                                                            <span v-if="payment.paymentMode == 0">
                                                                                {{
                                                                                    $t('AddPurchaseOrder.Cash')
                                                                                }}
                                                                            </span><span v-if="payment.paymentMode == 1">
                                                                                {{
                                                                                    $t('AddPurchaseOrder.Bank')
                                                                                }}
                                                                            </span>
                                                                        </th>
                                                                        <th>{{ payment.narration }}</th>
                                                                        <th>
                                                                            <a href="javascript:void(0)"
                                                                                title="Payment View"
                                                                                class="btn  btn-icon btn-primary btn-sm"
                                                                                v-on:click="ViewPaymentVoucher(payment.id, false)"
                                                                                v-if="isValid('CanViewDetailAdvancePayment')">
                                                                                <i class=" fas fa-eye"></i>
                                                                            </a>
                                                                        </th>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="accordion-item"
                                        v-if="isValid('CanViewOrderExpense') || isValid('CanAddOrderExpense') || isValid('CanViewDetailOrderExpense')">
                                        <h5 class="accordion-header m-0" id="headingTwo4">
                                            <button class="accordion-button collapsed fw-semibold" type="button"
                                                data-bs-toggle="collapse" data-bs-target="#collapseTwo4"
                                                aria-expanded="false" aria-controls="collapseTwo4">
                                                {{ $t('AddPurchaseOrder.Expense') }}
                                            </button>
                                        </h5>
                                        <div id="collapseTwo4" class="accordion-collapse collapse"
                                            aria-labelledby="headingTwo4" data-bs-parent="#accordionExample1">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-sm-12 form-group" v-if="isValid('CanAddOrderExpense')">
                                                        <a href="javascript:void(0)" class="btn btn-outline-primary "
                                                            v-on:click="expense = true">
                                                            {{
                                                                $t('AddPurchaseOrder.AddExpense')
                                                            }}
                                                        </a>
                                                    </div>

                                                    <div class="col-sm-12">
                                                        <purchaseorder-expense :show="expense" v-if="expense"
                                                            @close="expenseSave" :newisPurchase="'true'"
                                                            :purchaseOrderId="purchase.id" :formName="'AdvanceExpense'" />

                                                        <div class=" table-responsive"
                                                            v-if="isValid('CanViewOrderExpense')">
                                                            <table class="table ">
                                                                <thead class="m-0 thead-light">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th style="width:20%;">
                                                                            {{
                                                                                $t('AddPurchaseOrder.Date')
                                                                            }}
                                                                        </th>
                                                                        <th class="text-right">
                                                                            {{
                                                                                $t('AddPurchaseOrder.Amount')
                                                                            }}
                                                                        </th>
                                                                        <th class="text-center">
                                                                            {{
                                                                                $t('AddPurchaseOrder.PaymentMode')
                                                                            }}
                                                                        </th>
                                                                        <th>{{ $t('AddPurchaseOrder.Description') }} </th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(payment, index) in purchase.purchaseOrderExpenses"
                                                                        v-bind:key="index">
                                                                        <td>
                                                                            {{ index + 1 }}
                                                                        </td>
                                                                        <th>{{ getDate(payment.date) }}</th>
                                                                        <th class="text-right">
                                                                            {{ currency }}
                                                                            {{
                                                                                parseFloat(payment.amount).toFixed(3).slice(0,
                                                                                    -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                                        "$1,")
                                                                            }}
                                                                        </th>
                                                                        <th class="text-center">
                                                                            <span v-if="payment.paymentMode == 0">
                                                                                {{
                                                                                    $t('AddPurchaseOrder.Cash')
                                                                                }}
                                                                            </span><span v-if="payment.paymentMode == 1">
                                                                                {{
                                                                                    $t('AddPurchaseOrder.Bank')
                                                                                }}
                                                                            </span>
                                                                        </th>
                                                                        <th>{{ payment.narration }}</th>
                                                                        <th>
                                                                            <a href="javascript:void(0)"
                                                                                title="Payment View"
                                                                                class="btn  btn-icon btn-primary btn-sm"
                                                                                v-on:click="ViewPaymentVoucher(payment.id, true)"
                                                                                v-if="isValid('CanViewDetailOrderExpense')">
                                                                                <i class=" fas fa-eye"></i>
                                                                            </a>
                                                                        </th>
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
                    </div>
                </div>
            </div>

            <bulk-attachment :attachmentList="purchase.attachmentList" :show="isAttachshow" v-if="isAttachshow"
                @close="attachmentSaved" />
            <purchase-order-payment-view :data="paymentview" :formName="'AdvancePay'" @close="paymentView"
                :show="isPaymentview" v-if="isPaymentview" />
            <purchase-order-payment-view :data="paymentview" :formName="'AdvanceExpense'" @close="paymentView"
                :show="isExpenseview" v-if="isExpenseview" />
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true">
            </loading>
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'

import moment from "moment";
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
import {
    required
} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
import Multiselect from 'vue-multiselect'
//import VueBarcode from 'vue-barcode';
export default {
    props: ['formName'],

    mixins: [clickMixin],
    components: {
        Multiselect,
        Loading
    },
    setup() {
        return { v$: useVuelidate() }
    },
    data: function () {
        return {
            sale: {},

            supplierDuplicateId: '',
            supplierQuotationId: '',
            canvasSelectValueRender: 0,
            multipleAddress: false,
            expandSupplierQuotation: false,
            supplierQuotationList: [],
            selectedValue: '',
            selectedValue1: '',
            additionalOptions: {
                isRaw: false,
                invoiceNo: '',
                invoiceDate: '',
                reference: '',
            },
            randerEffect: 0,

            isVATInput: false,

            discountTypeOption: 'At Line Item Level',
            adjustmentSignProp: '+',
            defaultVat: '',

            internationalPurchase: '',
            advanceAccountId: '',
            currency: '',
            totalAmount: 0,
            daterander: 0,
            vatRander: 0,
            rander: 0,
            isService: false,
            attachment: false,
            isAttachshow: false,
            purchase: {
                id: "00000000-0000-0000-0000-000000000000",
                date: "",
                supplierQuotationId: "",
                registrationNo: "",
                supplierId: "",
                invoiceNo: "",
                quotationId: "",
                invoiceDate: "",
                reference: "",
                purchaseOrder: "",
                note: '',
                purchaseOrderItems: [],
                attachmentList: [],
                taxMethod: '',
                taxRateId: '',
                purchaseTemplateId: '',
                path: '',
                isRaw: false,
                internationalPurchase: false,

                discount: 0,
                isDiscountOnTransaction: false,
                isFixed: false,
                isBeforeTax: true,
                transactionLevelDiscount: 0,

                grossAmount: 0,
                vatAmount: 0,
                discountAmount: 0,
                totalAmount: 0,
                branchId: '',
            },
            raw: '',
            loading: false,
            language: 'Nothing',
            options: [],
            deliveryAddressList: [],
            supplierRender: 0,
            show: false,
            payment: false,
            expense: false,
            action: {
                id: '00000000-0000-0000-0000-000000000000',
                purchaseOrderId: '',
                processId: '',
                date: '',
                description: '',
            },
            paymentview: '',
            isExpenseview: false,
            isPaymentview: false,
        };
    },
    validations() {
        return {
            purchase: {
                date: {
                    required
                },
                registrationNo: {
                    required
                },
                supplierId: {
                    required
                },
                invoiceNo: {},
                invoiceDate: {},

                purchaseOrderItems: {
                    required
                },
            },
        }
    },
    methods:
    {
        DefaultOnList: function (record, value) {
            {
                if (value) {
                    this.deliveryAddressList.forEach(function (cat) {
                        if (record.id !== cat.id) {
                                cat.isOffice = false;

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
                                cat.isOffice = false;

                        }
                    });
                }

                // Loop through the delivery address list

            }
        },


        ChangeVat: function (value, prop) {
            this.$refs.childComponentRef.changeVatInformation(value, prop);
        },

        GetSupplierDetails: function () {

            if(this.supplierDuplicateId==this.purchase.supplierId)
                {
                    return ;
                }
            if (this.purchase.supplierId != null && this.purchase.supplierId != '') {
                var root = this;
                this.supplierDuplicateId=this.purchase.supplierId;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                root.$https.get('/Contact/ContactDetail?id=' + this.purchase.supplierId + '&multipleAddress='+this.multipleAddress, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.sale = response.data;
                        root.deliveryAddressList = root.sale.deliveryAddressList;

                        let  id=root.purchase.billingId;
                        if(id !=null && id!=''&& id!=undefined )
                        {
                            root.deliveryAddressList.forEach(function (cat) {
                                if (id== cat.id) {
                                    cat.isOffice = true;
                                } else {

                                            cat.isOffice = false;

                                }
                            });



                        }

                    });
            }
        },
        UpdateCustomerDetail: function () {

            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');

            }
            var customer = {
                id: this.sale.id,
                code: this.sale.code,
                prefix: this.sale.prefix,
                customerDisplayName: this.sale.customerDisplayName,
                arabicName: this.sale.arabicName,
                englishName: this.sale.englishName,
                companyNameEnglish: this.sale.companyNameEnglish,
                companyNameArabic: this.sale.companyNameArabic,
                commercialRegistrationNo: this.sale.commercialRegistrationNo,
                vatNo: this.sale.vatNo,
                contactNo1: this.sale.contactNo1,
                email: this.sale.email,
                billingAddress: this.sale.billingAddress,
                shippingAddress: this.sale.shippingAddress,
                isUpdate: true,
                isCustomer: false,
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
        VatInputValues: function () {
            this.isVATInput = !this.isVATInput;
        },
        SaveCanvasData: function (value) {
            if (value == 'Additional') {
                this.purchase.isRaw = this.additionalOptions.isRaw;
                this.purchase.invoiceNo = this.additionalOptions.invoiceNo;
                this.purchase.invoiceDate = this.additionalOptions.invoiceDate;
                this.purchase.reference = this.additionalOptions.reference;
                this.randerEffect++;
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                    type: 'success',
                    confirmButtonClass: "btn btn-success",
                    buttonStyling: false,
                    icon: 'success',
                    timer: 1500,
                    timerProgressBar: true,

                });
            }
        },
        RemoveEffect: function (value) {
            if (value == 'isRaw') {
                this.purchase.isRaw = false;
                this.additionalOptions.isRaw = false;
                this.randerEffect++;
            }
            if (value == 'invoiceNo') {
                this.purchase.invoiceNo = '';
                this.additionalOptions.invoiceNo = '';
                this.randerEffect++;
            }
            if (value == 'invoiceDate') {
                this.purchase.invoiceDate = '';
                this.additionalOptions.invoiceDate = '';
                this.randerEffect++;
            }
            if (value == 'reference') {
                this.purchase.reference = '';
                this.additionalOptions.reference = '';
                this.randerEffect++;
            }
            if (value == 'CanvasSelectValue') {
                this.selectedValue = '';
                this.purchase.purchaseOrderId = '';
                this.canvasSelectValueRender++;
            }
            if (value == 'RemoveItems') {
                this.selectedValue1 = '';
                this.selectedValue = '';
                this.purchase.purchaseOrderItems = [];
                this.purchase.purchaseOrderId = '';
                this.$refs.childComponentRef.clearList();

                this.supplierRender++;
            }

        },
        RemoveCanvasData: function (value) {
            if (value == 'Additional') {
                this.purchase.invoiceNo = '';
                this.additionalOptions.invoiceNo = '';
                this.purchase.invoiceDate = '';
                this.additionalOptions.invoiceDate = '';
                this.additionalOptions.reference = '';
                this.purchase.isRaw = false;
                this.additionalOptions.isRaw = false;

            }
        },
        ExpandSupplierQuotation: function (val) {
            this.expandSupplierQuotation = val;

            if (val) {
                this.GetSQList();
            }
        },
        GetSQList: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var branchId = localStorage.getItem('BranchId');
            if (this.purchase.supplierId != null && this.purchase.supplierId != '') {
                root.$https.get('/Purchase/PurchaseOrderList?SupplierId=' + this.purchase.supplierId + '&isDropdown=' + true + '&documentType=' + 'SupplierQuotation' + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.supplierQuotationList = response.data.results;
                    });
            }

        },
        GetSQId: function (id, registrationNumber, date, netAmount) {
            this.supplierQuotationId = id;
            this.selectedValue = registrationNumber + ' - ' + date + ' - ' + netAmount;
            this.canvasSelectValueRender++;
        },
        GetPoData: function (id) {

            if (this.selectedValue != null && this.selectedValue != '') {
                this.selectedValue1 = this.selectedValue;
            }

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var multi = localStorage.getItem('IsMultiUnit') == 'true' ? true : false
            root.$https.get('/Purchase/PurchaseOrderDetail?Id=' + id + '&isMultiUnit=' + multi + '&documentType=' + 'SupplierQuotation', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.purchase.purchaseOrderItems = [];
                        root.purchase.supplierQuotationId = response.data.id;
                        root.purchase.supplierId = response.data.supplierId;
                        root.purchase.taxMethod = response.data.taxMethod;
                        root.purchase.taxRateId = response.data.taxRateId;
                        root.purchase.note = response.data.note;
                        //root.additionalOptions.reference = response.data.reference;
                        //root.additionalOptions.invoiceNo = response.data.invoiceNo;
                        // root.purchase.reference = response.data.reference;
                        //root.purchase.invoiceNo = response.data.invoiceNo;
                        root.$refs.childComponentRef.ClearRecord();


                        response.data.purchaseOrderItems.forEach(function (so) {

                            {

                                so.taxRateId = root.purchase.taxRateId;
                                so.taxMethod = root.purchase.taxMethod;
                                so.discount = 0;
                                so.serial = '';
                                so.fixDiscount = 0;
                                if (so.isService == true) {
                                    root.$refs.childComponentRef.newItemProduct(true, so);

                                }
                                else {
                                    root.$refs.childComponentRef.addProduct(so.productId, so.product, so, true, root.purchase.taxRateId, root.purchase.taxMethod);

                                }
                            }
                        });




                        root.expandSupplierQuotation = false;
                        root.supplierQuotationId = '';
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
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        GetSaleOrderDetail: function (id) {


            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            if (id != undefined) {

                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.$refs.childComponentRef.clearList();
                            response.data.saleOrderItems.forEach(function (so) {

                                if (root.isService) {
                                    if (so.productId == null || so.productId == undefined) {
                                        root.$refs.childComponentRef.newItemProductForQuotation(so.productId, so, false, root.purchase.taxRateId, root.purchase.taxMethod);

                                    } else {
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, null, false, root.purchase.taxRateId, root.purchase.taxMethod);

                                    }

                                } else {
                                    root.$refs.childComponentRef.addProduct(so.productId, so.product, null, false, root.purchase.taxRateId, root.purchase.taxMethod);

                                }
                            });

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            }
        },
        updateSummary: function (summary) {
            this.purchase.totalAfterDiscount = summary.totalAfterDiscount;
            this.purchase.grossAmount = summary.total;
            this.purchase.vatAmount = summary.vat;
            this.purchase.discountAmount = summary.discount;
            this.purchase.totalAmount = summary.withVat;

        },

        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },

        Attachment: function () {
            this.isAttachshow = true;
        },

        attachmentSaved: function (attachment) {
            this.purchase.attachmentList = attachment;
            this.isAttachshow = false;
        },

        GetPurchaseTemplate: function (id) {
            var root = this;

            root.$https.get('Purchase/PurchaseTemplateDetail?id=' + id, {
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem('token')}`
                }
            })
                .then(function (response) {
                    if (response.data != null && response.data != '') {
                        root.purchase.supplierId = response.data.supplierId;
                        root.purchase.taxMethod = response.data.taxMethod;
                        root.purchase.taxRateId = response.data.taxRateId;
                        root.purchase.note = response.data.note;

                        root.$refs.childComponentRef.clearList();

                        response.data.purchaseOrderItems.forEach(function (item) {
                            if (item.product.inventory != null && item.product.inventory.currentQuantity < (item.product.stockLevel == '' ? 0 : parseFloat(item.product.stockLevel))) {

                                root.$refs.childComponentRef.addProduct(item.productId, item.product, item, true, root.purchase.taxRateId, root.purchase.taxMethod);

                            }

                        });
                        root.supplierRender++;
                        root.vatRander++;
                        root.rendered++;
                    }
                });
        },

        getTotalAmount: function () {
            this.totalAmount = this.$refs.childComponentRef.getTotalAmount();
        },
        getDate: function (date) {
            if (date == null || date == undefined) {
                return "";
            } else {
                return moment(date).format('LLL');
            }
        },
        ChangeSupplier: function () {
            this.supplierRender++;
            this.rander++;
        },
        languageChange: function (lan) {
            if (this.language == lan) {
                if (this.purchase.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/addproduct');
                } else {
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

                        root.purchase.path = response.data;

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

        AutoIncrementCode: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            var terminalId = '';

            if (localStorage.getItem('TerminalId') != null && localStorage.getItem('TerminalId') != undefined && localStorage.getItem('TerminalId') != "null" && localStorage.getItem('TerminalId') != 'null') {
                terminalId = localStorage.getItem('TerminalId');
            }
            root.$https.get("/Purchase/PurchaseOrderAutoGenerateNo?terminalId=" + terminalId + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&userID=' + localStorage.getItem('UserID') + '&documentType=' + this.formName + '&branchId=' + localStorage.getItem('BranchId'), {
                headers: {
                    Authorization: `Bearer ${token}`
                },
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.purchase.registrationNo = response.data;
                    }
                });
        },
        SavePurchaseItems: function (purchaseOrderItems, discount, adjustmentSignProp, transactionLevelDiscount) {

            this.purchase.purchaseOrderItems = purchaseOrderItems;

            this.purchase.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

            this.purchase.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            this.getTotalAmount();
        },
        updateDiscountChanging: function (isFixed, isBeforeTax) {
            this.purchase.isFixed = isFixed
            this.purchase.isBeforeTax = isBeforeTax
        },

        savePurchase: function (status) {

            this.purchase.approvalStatus = status
            localStorage.setItem('active', status);

            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            if (this.multipleAddress) {
                if (this.deliveryAddressList.length > 0) {
                    

                    // var shippingAddress = this.deliveryAddressList.find(x => x.type == 'Shipping' && x.isOffice);
                    // if (shippingAddress != null) {
                    //     this.purchase.shippingId = shippingAddress.id;
                    // }
                    var billingAddress = this.deliveryAddressList.find(x =>x.isOffice);
                    if (billingAddress != null) {
                        this.purchase.billingId = billingAddress.id;
                    }
                    else
                        {
                            this.purchase.billingId ='';
                        }

                    // var nationalAddress = this.deliveryAddressList.find(x => x.type == 'National' && x.isOffice);
                    // if (nationalAddress != null) {
                    //     this.purchase.nationalId = nationalAddress.id;
                    // }

                }

            }
            


            this.purchase.internationalPurchase = this.internationalPurchase == 'true' ? true : false;
            this.purchase.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            //root.purchase.date = root.purchase.date + " " + moment().format("hh:mm A");
            this.purchase.branchId = localStorage.getItem('BranchId');
            this.$https.post('/Purchase/SavePurchaseOrderInformation', root.purchase, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(response => {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                        root.info = response.data.bpi
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                if (root.formName == 'SupplierQuotation') {
                                    root.$router.push({
                                        path: '/purchaseorder',
                                        query: {
                                            data: 'purchaseorders',
                                            formName: 'SupplierQuotation',
                                        }
                                    })
                                } else {
                                    root.$router.push({
                                        path: '/purchaseorder',
                                        query: {
                                            data: 'purchaseorders'
                                        }
                                    })
                                }
                            }
                        });
                    } else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        root.info = response.data.bpi
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {

                                if (root.formName == 'SupplierQuotation') {
                                    root.$router.push({
                                        path: '/purchaseorder',
                                        query: {
                                            data: 'purchaseorders',
                                            formName: 'SupplierQuotation',
                                        }
                                    })
                                } else {
                                    root.$router.push({
                                        path: '/purchaseorder',
                                        query: {
                                            data: 'purchaseorders'
                                        }
                                    })
                                }

                            }
                        });
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
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
                        type: 'error',
                        icon: 'error',
                        title: root.$t('AddPurchaseOrder.Error'),
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
        IsSave: function () {
            this.show = false;
            this.GetProcessType();
        },
        attachmentSave: function () {
            this.GetAttachment();
            this.GetProcessType();
            this.attachment = false;
            this.GetProcessType();
        },
        paymentSave: function () {
            this.payment = false;
            this.GetPaymentVoucher();
            this.GetProcessType();
        },
        expenseSave: function () {
            this.expense = false;
            this.GetExpenseVoucher();
            this.GetProcessType();
        },
        GetExpenseVoucher: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('Purchase/PurchaseOrderExpensePaymentList?id=' + this.purchase.id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null && response.data != '') {
                        root.purchase.purchaseOrderExpenses = response.data;
                    }
                });
        },
        GetPaymentVoucher: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('Purchase/PurchaseOrderPaymentList?id=' + this.purchase.id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null && response.data != '') {
                        root.purchase.paymentVoucher = response.data;
                    }
                });
        },
        GetAttachment: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('Purchase/PurchaseOrderAttachmentList?id=' + this.purchase.id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null && response.data != '') {
                        root.purchase.purchaseAttachments = response.data;
                    }
                });
        },
        GetProcessType: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('Purchase/PurchaseOrderActionList?id=' + this.purchase.id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null && response.data != '') {
                        root.purchase.actionProcess = response.data;
                    }
                });
        },
        goToPurchase: function () {
            var root = this;

            if (root.formName == 'SupplierQuotation') {
                root.$router.push({
                    path: '/purchaseorder',
                    query: {
                        data: 'purchaseorders',
                        formName: 'SupplierQuotation',
                    }
                })
            } else {
                root.$router.push({
                    path: '/purchaseorder',
                    query: {
                        data: 'purchaseorders'
                    }
                })
            }

        },
        ViewPaymentVoucher: function (id, expense) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Purchase/PurchaseOrderPaymentDetail?Id=' + id + '&expense=' + expense, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {
                    root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + response.data.paymentVoucherId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.paymentview = response.data;
                            if (expense) {
                                root.isExpenseview = true;
                            } else {
                                root.isPaymentview = true;
                            }
                        }
                    });
                }
            });
        },
        paymentView: function () {
            this.isPaymentview = false;
            this.isExpenseview = false;
        }
    },
    created: function () {


        var root = this;
        this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

        if (root.$route.query.Add == 'false') {
            root.$route.query.data = this.$store.isGetEdit;
        }

        this.$emit('update:modelValue', this.$route.name);
        this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
        this.defaultVat = localStorage.getItem('DefaultVat');
        this.isService = localStorage.getItem('ServicePurchase') == 'true' ? true : false;


        if (this.$route.query.data != undefined) {
            this.purchase = this.$route.query.data;
            this.purchase.clone = this.$route.query.clone == 'true' ? true : false;
            this.purchase.isConversion = this.$route.query.isConversion == 'true' ? true : false;
            if (this.purchase.isConversion) {
                this.purchase.supplierQuotationId = this.$route.query.data.id;
                this.purchase.supplierId = this.$route.query.data.supplierId;
                this.purchase.date = moment(this.purchase.date).format("LLL");
                this.selectedValue = this.$route.query.data.registrationNo + " " + moment(this.purchase.date).format("LLL") + " " + this.$route.query.data.netAmount;
                this.selectedValue1 = this.$route.query.data.registrationNo + " " + moment(this.purchase.date).format("LLL") + " " + this.$route.query.data.netAmount;

                this.purchase.taxRateId = localStorage.getItem('TaxRateId');
                this.purchase.taxMethod = localStorage.getItem('taxMethod');
                this.discountTypeOption = localStorage.getItem('DiscountTypeOption');
                this.purchase.isDiscountOnTransaction = localStorage.getItem('DiscountTypeOption') === 'At Transaction Level' ? true : false;

                this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'


                this.purchase.supplierQuotationNo = this.selectedValue1;
                this.purchase.documentType = '';
                this.purchase.id = '00000000-0000-0000-0000-000000000000';
                this.AutoIncrementCode();


            }
            else {


                if (this.purchase.clone) {
                    this.purchase.id = '00000000-0000-0000-0000-000000000000';
                    this.AutoIncrementCode();


                }


                this.purchase.supplierId = this.$route.query.data.supplierId;
                this.selectedValue = this.$route.query.data.supplierQuotationNo;
                this.selectedValue1 = this.$route.query.data.supplierQuotationNo;
                this.additionalOptions.isRaw = this.purchase.isRaw;
                this.additionalOptions.invoiceNo = this.purchase.invoiceNo;
                this.additionalOptions.invoiceDate = this.purchase.invoiceDate;
                this.additionalOptions.reference = this.purchase.reference;

                this.action.purchaseOrderId = this.purchase.id;
                this.advanceAccountId = this.$route.query.data.advanceAccountId;
                this.purchase.date = moment(this.purchase.date).format("LLL");
                this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'

                this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'

                this.rander++;
                this.rendered++;
            }
        }
        else {
            if (this.formName == 'SupplierQuotation') {
                this.purchase.documentType = "SupplierQuotation";

            }
            this.purchase.taxRateId = localStorage.getItem('TaxRateId');
            this.purchase.taxMethod = localStorage.getItem('taxMethod');
            this.discountTypeOption = localStorage.getItem('DiscountTypeOption');
            this.purchase.isDiscountOnTransaction = localStorage.getItem('DiscountTypeOption') === 'At Transaction Level' ? true : false;

            this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'
        }
    },
    mounted: function () {


        this.language = this.$i18n.locale;
        this.currency = localStorage.getItem('currency');
        this.internationalPurchase = localStorage.getItem('InternationalPurchase');
        //this.versionAllow = localStorage.getItem('VersionAllow');
        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
            this.options = ['Inclusive', 'Exclusive'];
        } else {
            this.options = ['شامل', 'غير شامل'];
        }
        this.raw = localStorage.getItem('IsProduction');

        if (this.$route.query.data == undefined) {
            this.AutoIncrementCode();

            if (this.$route.query.id != undefined) {

                if (this.$route.query.isQuotation == 'true' || this.$route.query.isQuotation == true) {
                    this.GetSaleOrderDetail(this.$route.query.id);

                }

            }

            this.purchase.date = moment().format("LLL");
            this.daterander++;
        }
    },
};
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
    font-size: 30px;
    text-align: center;
    background: blue;
    color: #fff;
    vertical-align: middle;
    line-height: 60px;
}

.custom_code1::after {
    background: gray !important;
}

.custom_code::after {
    background: purple !important;
}

.visibility {
    display: block !important;
    visibility: hidden !important;
}

.visibilityOn {
    display: block !important;
    visibility: visible !important;
}</style>
