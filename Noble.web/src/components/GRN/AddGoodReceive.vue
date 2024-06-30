<template>
    <div class="row"
        v-if="isValid('CanViewGoodsReceiveasDraft') || isValid('CanAddGoodsReceiveasPost') || isValid('CanEditGoodsReceiveasDraft')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col d-flex align-items-baseline">
                    <div class="media">
                        <span class="circle-singleline" style="background-color: #1761FD !important;">GR</span>
                        <div class="media-body align-self-center ms-3">

                            <h6 class="m-0 font-20">
                                {{ $t('GoodReceive.AddGoodReceive') }} <span class="mx-2"
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
                                {{ $t('AddPurchaseOrder.Supplier') }} :
                                <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <supplierdropdown v-model="v$.purchase.supplierId.$model" :paymentTerm="purchase.isCredit"
                                v-bind:key="supplierRender"
                                :disable="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'"
                                :modelValue="purchase.supplierId" :status="purchase.isRaw" />
                            <a v-if="purchase.supplierId != null && purchase.supplierId != ''"
                                v-on:click="GetSupplierDetails()" href="javascript:void(0);" data-bs-toggle="offcanvas"
                                ref="offcanvasRight" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"
                                class="text-primary mt-2">Supplier Detail</a>
                            <a v-else href="javascript:void(0);" class="text-secondary mt-2">
                                Supplier Detail
                            </a>
                        </div>
                    </div>
                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                        aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">Supplier Detail</h5>
                            <button
                                v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'margin-left:257px !important' : 'margin-left:0px !important'"
                                type="button" class="btn btn-outline-primary"
                                @click="UpdateCustomerDetail(sale.customerIdForUpdate)">{{ $t('AddSale.Update') }}</button>
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
                                    <input type="text" class="form-control" readonly v-model="sale.customerDisplayName" />
                                </div>
                                <div class="col-lg-12 form-group">
                                    <div class="row">
                                        <label>{{ $t('Contact Person Name') }} :</label>
                                        <div class="col-lg-4 form-group">
                                            <input type="text" class="form-control" readonly v-model="sale.prefix" />
                                        </div>
                                        <div class="col-lg-4 form-group">
                                            <input type="text" class="form-control" readonly v-model="sale.englishName" />
                                        </div>
                                        <div class="col-lg-4 form-group">
                                            <input type="text" class="form-control" readonly v-model="sale.arabicName" />
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
                                    <textarea rows="3" v-model="sale.billingAddress" class="form-control"> </textarea>
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


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">

                        </label>
                        <div class="inline-fields col-lg-8">
                            <a href="javascript:void(0);" class="text-secondary" v-if="purchase.isEditPaidInvoice">
                                {{ $t('AddStockValue.Discount&Vat/Taxoptions') }}2
                            </a>
                            <a href="javascript:void(0);" class="text-primary" v-on:click="VatInputValues()" v-else>
                                {{ $t('AddStockValue.Discount&Vat/Taxoptions') }}
                            </a>

                        </div>
                    </div>

                    <div class="row" v-if="isVATInput">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">{{ $t('AddSale.DiscountType')
                                }}</span>
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
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.TaxMethod') }}
                                    :<span class="text-danger"> *</span></span>
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



                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <div class="col-lg-1">

                        </div>
                        <div class="inline-fields col-lg-11">
                            <a v-if="purchase.supplierId != null && purchase.supplierId != ''" href="javascript:void(0);"
                                data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight"
                                class="text-primary">Options</a>
                            <a v-else href="javascript:void(0);" class="text-secondary">Options</a>

                            <div class="row" v-bind:key="randerEffect">
                                <div class="col-md-12" v-if="selectedValue1 != '' && selectedValue1 != null"
                                    :key="poRender">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ selectedValue }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveRecord5()">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2" v-if="purchase.invoiceNo != '' && purchase.invoiceNo != null">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>
                                            {{ $t('AddPurchase.SupplierInvoiceNumber') }} :- {{
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
                                <div class="col-lg-12 pt-2" v-if="purchase.isRaw">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>
                                            {{ $t('AddPurchase.RawProduct') }} :- {{
                                                purchase.isRaw
                                            }}
                                        </span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect('isRaw')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-12 pt-2" v-if="purchase.poNumber != '' && purchase.poNumber != null">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>PO No. & Date :- {{ purchase.poNumber }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect('poNumber')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-12 pt-2"
                                    v-if="purchase.goodsRecieveNumberAndDate != '' && purchase.goodsRecieveNumberAndDate != null">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>SQ No. & Date :- {{ purchase.goodsRecieveNumberAndDate }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect('goodsRecieveNumberAndDate')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-12 pt-2" v-if="purchase.reference != '' && purchase.reference != null">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>Reference :- {{ purchase.reference }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect('reference')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight2"
                        aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">Options</h5>
                            <!-- <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button> -->
                            <!-- <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'margin-left:412px !important' : 'margin-left:0px !important'" type="button" class="btn btn-outline-primary" v-on:click="UpdateDetail">{{ $t('AddSale.Update') }}</button> -->
                            on
                            <button
                                v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'"
                                type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas"
                                aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <div class="row">
                                <div class="col-md-12 mb-2" v-if="selectedValue != '' && selectedValue != null"
                                    :key="poRender">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ selectedValue }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveRecord3('poRender')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>

                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-lg-6 form-group text-right">
                                            <b>{{ $t('AddPurchase.PurchaseOrder') }} </b>
                                        </div>
                                        <div class="col-lg-6 form-group text-left">
                                            <button v-if="expandPurchaseOrder" v-on:click="ExpandPurchaseOrder(false)"
                                                type="button"
                                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                <i class="ti-angle-double-up"></i>
                                            </button>
                                            <button v-else v-on:click="ExpandPurchaseOrder(true)" type="button"
                                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                <i class="ti-angle-double-down"></i>
                                            </button>
                                        </div>
                                        <div v-if="expandPurchaseOrder" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in poSuppliers" v-bind:key="index"
                                                style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);"
                                                    v-on:click="GetPOId(saleValue.id, saleValue.registrationNumber, saleValue.date, saleValue.netAmount)">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{
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
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-lg-6 form-group text-right">
                                            <b>Supplier Quotation </b>
                                        </div>
                                        <div class="col-lg-6 form-group text-left">
                                            <button v-if="expandGoodsRecieve" v-on:click="ExpandGoodsRecieve(false)"
                                                type="button"
                                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                <i class="ti-angle-double-up"></i>
                                            </button>
                                            <button v-else v-on:click="ExpandGoodsRecieve(true)" type="button"
                                                class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                <i class="ti-angle-double-down"></i>
                                            </button>
                                        </div>
                                        <div v-if="expandGoodsRecieve" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in poGoodsRecieveSupplier" v-bind:key="index"
                                                style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);"
                                                    v-on:click="GetGoodsRecievedId(saleValue.id, saleValue.registrationNumber, saleValue.date, saleValue.netAmount)">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{
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
                                <div class="col-md-12 text-end mt-2 mb-2">
                                    <div class="button-items">
                                        <button class="btn btn-outline-primary"
                                            v-bind:disabled="(poIdForPI == '' && poIdForPI == null) && (gdIdForPI == null && gdIdForPI == null)"
                                            v-on:click="GetPoData(poIdForPI, false, gdIdForPI)">
                                            <i class="far fa-save "></i>
                                            {{ $t('AddCustomer.btnSave') }}
                                        </button>
                                        <button type="button" v-on:click="RemoveRecord6('PoAndGdRec')"
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
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{ $t('AddPurchase.SupplierInvoiceNumber') }}:
                                                            </span>
                                                        </label>
                                                        <div class="inline-fields">
                                                            <input v-model="recordForAdditionalOpt.invoiceNo"
                                                                class="form-control" type="text">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanSelectWarehouse')">
                                                        <label class="col-form-label">
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{ $t('AddPurchase.WareHouse') }} :<span
                                                                    class="text-danger">
                                                                    *
                                                                </span>
                                                            </span>
                                                        </label>
                                                        <div class="inline-fields">
                                                            <warehouse-dropdown v-model="v$.purchase.wareHouseId.$model" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12 form-group" v-if="raw == 'true'">
                                                        <label class="col-form-label">
                                                        </label>
                                                        <div class="inline-fields">
                                                            <div class="checkbox form-check-inline mx-2">
                                                                <input type="checkbox" id="inlineCheckbox1"
                                                                    v-model="recordForAdditionalOpt.isRaw"
                                                                    @change="ChangeSupplier">
                                                                <label for="inlineCheckbox1">
                                                                    {{
                                                                        $t('AddPurchase.RawProduct')
                                                                    }}
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 text-end mt-2 mb-2">
                                                        <div class="button-items">
                                                            <button class="btn btn-outline-primary"
                                                                v-bind:disabled="recordForAdditionalOpt.invoiceNo == '' && !recordForAdditionalOpt.isRaw"
                                                                v-on:click="SaveRecord3('Additional')">
                                                                <i class="far fa-save "></i>
                                                                {{ $t('AddCustomer.btnSave') }}
                                                            </button>
                                                            <button type="button" v-on:click="RemoveRecord3('Additional')"
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
                                    <div class="accordion-item">
                                        <h5 class="accordion-header m-0" id="headingTwo">
                                            <button class="accordion-button collapsed fw-semibold" type="button"
                                                data-bs-toggle="collapse" data-bs-target="#collapseTwo"
                                                aria-expanded="false" aria-controls="collapseTwo">
                                                Manual References
                                            </button>
                                        </h5>
                                        <div id="collapseTwo" class="accordion-collapse collapse"
                                            aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-lg-12 form-group">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                PO No & Date
                                                            </span>
                                                        </label>
                                                        <input v-model="recordForAdditionalOpt.poNumber"
                                                            class="form-control" />
                                                    </div>
                                                    <div class="col-lg-12 form-group">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                SQ No. & Date
                                                            </span>
                                                        </label>
                                                        <input v-model="recordForAdditionalOpt.goodsRecieveNumberAndDate"
                                                            class="form-control" />
                                                    </div>
                                                    <div class="col-lg-12 form-group">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                Reference:
                                                            </span>
                                                        </label>
                                                        <input v-model="recordForAdditionalOpt.reference"
                                                            class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-end mt-2 mb-2">
                                                    <div class="button-items">
                                                        <button class="btn btn-outline-primary"
                                                            v-bind:disabled="recordForAdditionalOpt.poNumber == '' && recordForAdditionalOpt.goodsRecieveNumberAndDate == '' && recordForAdditionalOpt.reference == ''"
                                                            v-on:click="SaveRecord3('Manual')">
                                                            <i class="far fa-save "></i>
                                                            {{ $t('AddCustomer.btnSave') }}
                                                        </button>
                                                        <button type="button" v-on:click="RemoveRecord3('Manual')"
                                                            class="btn btn-danger ">
                                                            Cancel
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12 text-end mt-2">
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

                <purchase-item @update:modelValue="SavePurchaseItems" ref="childComponentRef" @summary="updateSummary"
                    :purchaseItems="purchase.goodReceiveNoteItems" :taxMethod="purchase.taxMethod"
                    :taxRateId="purchase.taxRateId" :raw="purchase.isRaw" :purchaseOrderId="purchase.purchaseOrderId"
                    :key="randerLineItem" @discountChanging="updateDiscountChanging" :adjustmentProp="purchase.discount"
                    :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction"
                    :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed"
                    :documentType="'goodReceive'" :isBeforeTax="purchase.isBeforeTax" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div v-if="!loading && purchase.id == '00000000-0000-0000-0000-000000000000'"
                        class="col-md-12 arabicLanguage">

                        <button class="btn btn-outline-primary me-2" v-on:click="savePurchasePost('Draft')"
                            v-if="isValid('CanViewPurchaseDraft')"
                            v-bind:disabled="v$.purchase.$invalid || purchase.goodReceiveNoteItems.filter(x => x.totalPiece == '').length > 0 || purchase.goodReceiveNoteItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchase.SaveAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2" v-on:click="savePurchasePost('Approved')"
                            v-if="isValid('CanAddPurchaseInvoice')"
                            v-bind:disabled="v$.purchase.$invalid || purchase.goodReceiveNoteItems.filter(x => x.totalPiece == '').length > 0 || purchase.goodReceiveNoteItems.filter(x => x.unitPrice == '').length > 0 || (isFifo ? (purchase.goodReceiveNoteItems.filter(x => x.expiryDate == '').length > 0 || purchase.goodReceiveNoteItems.filter(x => x.batchNo == '').length > 0) : false)">
                            <i class="far fa-save"></i> {{ $t('AddPurchase.SaveAndPost') }}
                        </button>
                        <button class="btn btn-danger me-2" v-on:click="goToPurchase">
                            {{ $t('AddPurchase.Cancel') }}
                        </button>
                    </div>
                    <div v-if="!loading && purchase.id != '00000000-0000-0000-0000-000000000000'"
                        class="col-md-12 arabicLanguage">

                        <button class="btn btn-outline-primary me-2" v-on:click="savePurchasePost('Draft')"
                            v-if="isValid('CanViewPurchaseDraft') && isValid('CanEditPurchaseInvoice')"
                            v-bind:disabled="v$.purchase.$invalid || purchase.goodReceiveNoteItems.filter(x => x.totalPiece == '').length > 0 || purchase.goodReceiveNoteItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddPurchase.UpdateAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary me-2" v-on:click="savePurchasePost('Approved')"
                            v-if="isValid('CanAddPurchaseInvoice') || isValid('CanEditPurchaseInvoice')"
                            v-bind:disabled="v$.purchase.$invalid || purchase.goodReceiveNoteItems.filter(x => x.totalPiece == '').length > 0 || purchase.goodReceiveNoteItems.filter(x => x.unitPrice == '').length > 0 || (isFifo ? (purchase.goodReceiveNoteItems.filter(x => x.expiryDate == '').length > 0 || purchase.goodReceiveNoteItems.filter(x => x.batchNo == '').length > 0) : false)">
                            <i class="far fa-save"></i> {{ $t('AddPurchase.UpdateAsPost') }}
                        </button>
                        <button class="btn btn-danger me-2" v-on:click="goToPurchase">
                            {{ $t('AddPurchase.Cancel') }}
                        </button>
                    </div>
                </div>

                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">

                                </div>
                                <div class="col-lg-4" v-if="purchase.id == '00000000-0000-0000-0000-000000000000'">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddPurchase.AttachFiles') }}</div>

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
                                <div class="col-lg-4" v-if="purchase.id != '00000000-0000-0000-0000-000000000000'">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddPurchase.AttachFiles') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                            class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddPurchase.FileSize') }}
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
                                <div class="accordion" role="tablist"
                                    v-if="internationalPurchase == 'true' && purchase.purchaseOrderId != null && purchase.purchaseOrderId != ''">
                                    <b-card no-body class="mb-1" v-if="isValid('CanViewPIAttachment')">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <b-button block v-b-toggle.accordion-1 variant="primary">
                                                {{
                                                    $t('AddPurchase.Attachment')
                                                }}
                                            </b-button>
                                        </b-card-header>
                                        <b-collapse id="accordion-1" accordion="my-accordion" role="tabpanel">
                                            <b-card-body>
                                                <import-attachment :purchase="purchase" :show="attachment" v-if="attachment"
                                                    @close="attachmentSave" :document="'Purchase'" />
                                                <div>
                                                    <div class="row"
                                                        v-if="purchase.id != '00000000-0000-0000-0000-000000000000'">
                                                        <div class="col-md-12 text-right">
                                                            <a href="javascript:void(0)" class="btn btn-outline-primary "
                                                                v-on:click="attachment = true">
                                                                {{
                                                                    $t('AddPurchase.Upload')
                                                                }}
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class=" table-responsive">
                                                        <table class="table ">
                                                            <thead class="m-0">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>{{ $t('AddPurchase.Date') }} </th>
                                                                    <th v-if="isValid('CanDownloadPIAttachment')">
                                                                        {{
                                                                            $t('AddPurchase.Attachment')
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
                                                                    <td v-if="isValid('CanDownloadPIAttachment')">
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
                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>
                                    <b-card no-body class="mb-1" v-if="isValid('CanViewPIActions')">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <b-button block v-b-toggle.accordion-2 variant="primary">
                                                {{
                                                    $t('AddPurchase.Actions')
                                                }}
                                            </b-button>
                                        </b-card-header>
                                        <b-collapse id="accordion-2" accordion="my-accordion" role="tabpanel">
                                            <b-card-body>
                                                <add-company-action :action="action" :show="show" v-if="show"
                                                    @close="IsSave" :document="'Purchase'" />

                                                <div class="row">
                                                    <div class="col-md-12"
                                                        v-if="purchase.id != '00000000-0000-0000-0000-000000000000'">
                                                        <div class="col-sm-6 float-right">
                                                            <a href="javascript:void(0)"
                                                                class="btn btn-outline-primary  float-right"
                                                                v-on:click="show = true">{{ $t('AddPurchase.Action') }}</a>
                                                        </div>
                                                    </div>
                                                    <div class=" table-responsive">
                                                        <table class="table ">
                                                            <thead class="m-0">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>{{ $t('AddPurchase.Status') }}</th>
                                                                    <th>{{ $t('AddPurchase.Date') }} </th>
                                                                    <th>{{ $t('AddPurchase.Description/Reason') }}</th>
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
                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>
                                    <b-card no-body class="mb-1" v-if="isValid('CanViewPIPayments')">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <b-button block v-b-toggle.accordion-3 variant="primary">
                                                {{
                                                    $t('AddPurchase.Payment')
                                                }}
                                            </b-button>
                                        </b-card-header>
                                        <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
                                            <b-card-body>
                                                <div>
                                                    <div class=" table-responsive">
                                                        <table class="table ">
                                                            <thead class="m-0">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th style="width:20%;">
                                                                        {{ $t('AddPurchase.Date') }}
                                                                    </th>
                                                                    <th style="width:20%;">
                                                                        {{
                                                                            $t('AddPurchase.VoucherNumber')
                                                                        }}
                                                                    </th>
                                                                    <th class="text-right">
                                                                        {{
                                                                            $t('AddPurchase.Amount')
                                                                        }}
                                                                    </th>
                                                                    <th class="text-center">
                                                                        {{
                                                                            $t('AddPurchase.PaymentMode')
                                                                        }}
                                                                    </th>
                                                                    <th>{{ $t('AddPurchase.Description') }} </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="(payment, index) in purchase.paymentVoucher"
                                                                    v-bind:key="index">
                                                                    <td>
                                                                        {{ index + 1 }}
                                                                    </td>
                                                                    <th>{{ getDate(payment.date) }}</th>
                                                                    <th>{{ payment.voucherNumber }}</th>
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
                                                                                $t('AddPurchase.Cash')
                                                                            }}
                                                                        </span><span v-if="payment.paymentMode == 1">
                                                                            {{
                                                                                $t('AddPurchase.Bank')
                                                                            }}
                                                                        </span>
                                                                    </th>
                                                                    <th>{{ payment.narration }}</th>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>
                                    <b-card no-body class="mb-1" v-if="isValid('CanViewPIExpenses')">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <b-button block v-b-toggle.accordion-4 variant="primary">
                                                {{
                                                    $t('AddPurchase.Expense')
                                                }}
                                            </b-button>
                                        </b-card-header>
                                        <b-collapse id="accordion-4" accordion="my-accordion" role="tabpanel">
                                            <b-card-body>
                                                <div>
                                                    <div class=" table-responsive">
                                                        <table class="table ">
                                                            <thead class="m-0">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th style="width:20%;">
                                                                        {{ $t('AddPurchase.Date') }}
                                                                    </th>
                                                                    <th style="width:20%;">
                                                                        {{
                                                                            $t('AddPurchase.VoucherNumber')
                                                                        }}
                                                                    </th>
                                                                    <th class="text-center">
                                                                        {{
                                                                            $t('AddPurchase.Amount')
                                                                        }}
                                                                    </th>
                                                                    <th class="text-center">
                                                                        {{
                                                                            $t('AddPurchase.PaymentMode')
                                                                        }}
                                                                    </th>
                                                                    <th>{{ $t('AddPurchase.Description') }} </th>
                                                                    <th> </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="(payment, index) in purchase.purchasePostExpense"
                                                                    v-bind:key="index">
                                                                    <td>
                                                                        {{ index + 1 }}
                                                                    </td>
                                                                    <th>{{ getDate(payment.date) }}</th>
                                                                    <th>{{ payment.voucherNumber }}</th>
                                                                    <th>
                                                                        <decimal-to-fixed v-model="payment.amount" />

                                                                        <!--{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}-->
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <span v-if="payment.paymentMode == 0">

                                                                            {{ $t('AddPurchase.Cash') }}
                                                                        </span><span v-if="payment.paymentMode == 1">

                                                                            {{ $t('AddPurchase.Bank') }}
                                                                        </span>
                                                                    </th>

                                                                    <th>{{ payment.narration }}</th>
                                                                    <th>
                                                                        <a href="javascript:void(0)" title="Remove"
                                                                            class="btn  btn-icon btn-danger btn-sm"
                                                                            v-on:click="removeExpense(payment.id)"
                                                                            v-if="isValid('CanDeletePIExpenses')">
                                                                            <i class="fa fa-times"></i>
                                                                        </a>
                                                                    </th>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>
                                </div>

                                <div class="card-footer col-md-3 text-left" v-if="loading">
                                    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                                </div>



                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :attachmentList="purchase.attachmentList" :show="isAttachshow" v-if="isAttachshow"
            @close="attachmentSave" />
        <purchaseInvoice :printDetails="printDetails" v-if="printDetails.length != 0" v-bind:key="printRender">
        </purchaseInvoice>
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
import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
import Multiselect from 'vue-multiselect'
//import Vue3Barcode from 'vue3-barcode'
export default {
    mixins: [clickMixin],
    name: "AddPurchase",
    components: {
        Loading,
        Multiselect
    },
    setup() {
        return { v$: useVuelidate() }
    },

    data: function () {
        return {
            sale: {},

            multipleAddress: false,
            expandGoodsRecieve: false,
            poGoodsRecieveSupplier: [],
            deliveryAddressList: [],

            poRender: 0,
            poIdForPI: '',
            gdIdForPI: '',
            selectedValue: '',
            selectedValue1: '',
            poSuppliers: [],
            expandPurchaseOrder: false,

            randerEffect: 0,
            isPaymentOpt: false,

            discountTypeOption: 'At Line Item Level',
            adjustmentSignProp: '+',

            isVATInput: false,

            defaultVat: '',
            accountRender: 0,
            isFifo: false,
            CanSelectWarehouse: false,
            purchaseOrder: false,
            internationalPurchase: '',
            currency: '',

            recordForAdditionalOpt: {
                invoiceNo: "",
                poNumber: "",
                reference: "",
                isRaw: false,
                goodsRecieveNumberAndDate: ""
            },
            purchase: {
                id: "00000000-0000-0000-0000-000000000000",
                date: "",
                poNumber: "",
                goodsRecieveNumberAndDate: "",
                allowPreviousFinancialPeriod: false,
                purchaseOrderId: "",
                bankCashAccountId: "",
                paymentType: "Cash",
                registrationNo: "",
                supplierId: "",
                invoiceNo: "",
                isCredit: false,
                isPurchaseReturn: false,
                invoiceDate: "",
                purchaseOrder: "",
                wareHouseId: "",
                goodReceiveNoteItems: [],
                attachmentList: [],
                isRaw: false,
                bomId: '',
                isPurchasePost: false,
                taxMethod: '',
                taxRateId: "00000000-0000-0000-0000-000000000000",
                actionProcess: [],
                purchaseAttachments: [],
                paymentVoucher: [],
                purchasePostExpense: [],
                partiallyPurchase: false,
                autoPurchaseVoucher: false,
                expenseToGst: false,
                internationalPurchase: false,
                colorVariants: false,
                dueAmount: 0,
                saleOrderId:'',
                discount: 0,
                isDiscountOnTransaction: false,
                isFixed: false,
                isBeforeTax: true,
                transactionLevelDiscount: 0,

                grossAmount: 0,
                vatAmount: 0,
                discountAmount: 0,
                totalAmount: 0,

                note: '',
            },

            printId: '00000000-0000-0000-0000-000000000000',
            printDetails: [],
            options: [],
            paymentTypeOptions: [],
            loading: false,
            rander: 0,
            randerSupplier: 0,
            raw: '',
            printRender: 0,
            randerLineItem: 0,
            autoNumber: '',
            supplierDuplicateId: '',
            language: 'Nothing',
            supplierRender: 0,
            wareRander: 0,
            show: false,
            isAttachshow: false,
            attachment: false,
            saleDefaultVat: '',

            action: {
                id: '00000000-0000-0000-0000-000000000000',
                purchaseOrderId: '',
                processId: '',
                date: '',
                description: '',
            },
        };
    },
    validations() {
        return {
            purchase: {
                date: { required },
                registrationNo: { required },
                supplierId: { required },
                invoiceDate: {},
                wareHouseId: {},
                goodReceiveNoteItems: { required },

                paymentType: {
                    required: requiredIf(function () {
                        return this.purchase.isCredit ? false : true;
                    }),
                    // required: requiredIf((x) => {
                    //     if (x.isCredit)
                    //         return false;
                    //     return true;
                    // })
                },
            },
        }
    },
    methods: {
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
                alert(this.supplierDuplicateId);
                this.supplierDuplicateId=this.purchase.supplierId;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                root.$https.get('/Contact/ContactDetail?id=' + this.purchase.supplierId + '&multipleAddress='+this.multipleAddress, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.sale = response.data;
                        if(root.multipleAddress)
                        {
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
        RemoveRecord5: function () {
            this.selectedValue1 = '';
            this.purchase.goodReceiveNoteItems = [];
            this.purchase.purchaseOrderId = '';
            this.$refs.childComponentRef.ClearRecord();


        },
        RemoveRecord3: function (val) {
            if (val == 'Additional') {
                this.recordForAdditionalOpt.invoiceNo = '';
                this.recordForAdditionalOpt.isRaw = false;
                this.purchase.invoiceNo = '';
                this.isRaw = false;

                this.randerEffect++;
            }
            else if (val == 'Manual') {
                this.purchase.poNumber = "";
                this.recordForAdditionalOpt.poNumber = "";
                this.recordForAdditionalOpt.goodsRecieveNumberAndDate = "";
                this.recordForAdditionalOpt.reference = "";
                this.purchase.goodsRecieveNumberAndDate = "";
                this.randerEffect++;
            }
            else if (val == 'poRender') {
                this.selectedValue = '';
                this.poRender++;
            }
            else if (val == 'PoAndGdRec') {
                this.poIdForPI = '';
                this.gdIdForPI = '';
                this.purchase.purchaseOrderId = this.poIdForPI;
                this.poRender++;
            }

        },
        GetPOId: function (id, registrationNumber, date, netAmount) {
            this.gdIdForPI = '';
            this.poIdForPI = id;
            this.selectedValue = registrationNumber + ' - ' + date + ' - ' + netAmount;
            this.purchase.purchaseOrderId = this.poIdForPI;
            this.poRender++;
        },
        GetGoodsRecievedId: function (id, registrationNumber, date, netAmount) {
            this.gdIdForPI = id;
            this.poIdForPI = '';
            this.selectedValue = registrationNumber + ' - ' + date + ' - ' + netAmount;
            this.purchase.purchaseOrderId = this.poIdForPI;
            this.poRender++;
        },
        ExpandPurchaseOrder: function (val) {
            this.expandPurchaseOrder = val;
            if (val) {
                this.GetPOList();
            }
        },
        ExpandGoodsRecieve: function (val) {
            this.expandGoodsRecieve = val;
            if (val) {
                this.GetGoodsRecieveList();
            }
        },
        GetPOList: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


            if (this.purchase.supplierId != null && this.purchase.supplierId != '') {
                root.$https.get('/Purchase/PurchaseOrderList?supplierId=' + this.purchase.supplierId + '&isDropdown=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.poSuppliers = response.data.results;
                    });
            }

        },
        GetGoodsRecieveList: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


            if (this.purchase.supplierId != null && this.purchase.supplierId != '') {
                root.$https.get('/Purchase/PurchaseOrderList?supplierId=' + this.purchase.supplierId + '&isDropdown=' + true + '&documentType=SupplierQuotation', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.poGoodsRecieveSupplier = response.data.results;
                    });
            }

        },


        RemoveEffect: function (value) {
            if (value == 'invoiceNo') {
                this.recordForAdditionalOpt.invoiceNo = '';
                this.purchase.invoiceNo = '';
            }
            if (value == 'reference') {
                this.recordForAdditionalOpt.reference = '';
                this.purchase.reference = '';
            }
            if (value == 'isRaw') {
                this.recordForAdditionalOpt.isRaw = false;
                this.purchase.isRaw = false;
            }
            if (value == 'poNumber') {
                this.recordForAdditionalOpt.poNumber = '';
                this.purchase.poNumber = '';
            }
            if (value == 'goodsRecieveNumberAndDate') {
                this.recordForAdditionalOpt.goodsRecieveNumberAndDate = '';
                this.purchase.goodsRecieveNumberAndDate = '';
            }
            this.randerEffect++;
        },

        SaveRecord3: function (val) {

            if (val == 'Additional') {
                this.purchase.invoiceNo = this.recordForAdditionalOpt.invoiceNo;
                this.purchase.isRaw = this.recordForAdditionalOpt.isRaw;
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
            else if (val == 'Manual') {
                this.purchase.reference = this.recordForAdditionalOpt.reference;
                this.purchase.poNumber = this.recordForAdditionalOpt.poNumber;
                this.purchase.goodsRecieveNumberAndDate = this.recordForAdditionalOpt.goodsRecieveNumberAndDate;
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
            this.randerEffect++;

        },

        VatInputValues: function () {
            this.isVATInput = !this.isVATInput;
        },
        PaymentOpt: function () {
            this.isPaymentOpt = !this.isPaymentOpt;

        },
        updateSummary: function (summary) {
            this.purchase.grossAmount = summary.total;
            this.purchase.vatAmount = summary.vat;
            this.purchase.totalAfterDiscount = summary.totalAfterDiscount;
            this.purchase.discountAmount = summary.discount;
            this.purchase.totalAmount = summary.withVat;

        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        Attachment: function () {
            this.isAttachshow = true;
        },

        attachmentSave: function (attachment) {
            this.purchase.attachmentList = attachment;
            this.isAttachshow = false;
        },

        GetAccount: function () {
            this.accountRender++;
        },
        SelectPaymentMethod: function (value) {


            this.purchase.isCredit = value;
            if (this.purchase.isCredit) {
                this.purchase.bankCashAccountId = '';
            }
            else {
                if (localStorage.getItem('CashAccountId') != null && localStorage.getItem('CashAccountId') != undefined && localStorage.getItem('CashAccountId') != '') {

                    this.purchase.bankCashAccountId = localStorage.getItem('CashAccountId');
                }
            }
            this.rander++;
            this.randerSupplier++;
        },
        removeExpense: function (id) {
            this.purchase.purchasePostExpense = this.purchase.purchasePostExpense.filter((prod) => {
                return prod.id != id;
            });
        },
        getDate: function (date) {
            if (date == null || date == undefined) {
                return "";
            }
            else {
                return moment(date).format('LLL');
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

        ChangeSupplier: function () {

            this.supplierRender++;
            this.randerLineItem++;
        },
        languageChange: function (lan) {
            if (this.language == lan) {
                if (this.purchase.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/addproduct');
                }
                else {
                    this.$swal({
                        title: this.$t('AddPurchase.Error'),
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

        GetPoData: function (id, isEdit, gdIdForPI) {

            if (this.selectedValue != null && this.selectedValue != '') {
                this.selectedValue1 = this.selectedValue;
            }

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            if (id != null && id != '') {
                var multi = localStorage.getItem('IsMultiUnit') == 'true' ? true : false
                root.$https.get('/Purchase/PurchaseOrderDetail?Id=' + id + '&isMultiUnit=' + multi, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            if (root.internationalPurchase == 'true') {
                                root.purchase.actionProcess = response.data.actionProcess;
                                root.purchase.purchaseAttachments = response.data.purchaseAttachments;
                                root.purchase.paymentVoucher = response.data.paymentVoucher;
                                response.data.purchaseOrderExpenses.forEach(function (item) {
                                    if (item.amount - item.usedAmount > 0) {
                                        root.purchase.purchasePostExpense.push({
                                            id: item.id,
                                            date: item.date,
                                            bankCashAccountId: item.bankCashAccountId,
                                            contactAccountId: item.contactAccountId,
                                            vatAccountId: item.vatAccountId,
                                            taxRateId: item.taxRateId,
                                            taxMethod: item.taxMethod,
                                            voucherNumber: item.voucherNumber,
                                            narration: item.narration,
                                            chequeNumber: item.chequeNumber,
                                            amount: item.amount - item.usedAmount,
                                            paymentMode: item.paymentMode,
                                            paymentMethod: item.paymentMethod,
                                        });
                                    }
                                });
                            }

                            if (!isEdit) {
                                root.purchase.goodReceiveNoteItems = [];
                                root.purchase.purchaseOrderId = response.data.id;
                                root.purchase.supplierId = response.data.supplierId;
                                root.purchase.taxMethod = response.data.taxMethod;
                                root.purchase.taxRateId = response.data.taxRateId;

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


                                root.rander++;
                                root.supplierRender++;
                                root.expandPurchaseOrder = false;
                                root.poIdForPI = false;
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

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            }
            else if (gdIdForPI != null && gdIdForPI) {
                let multi = localStorage.getItem('IsMultiUnit') == 'true' ? true : false
                root.$https.get('/Purchase/PurchaseOrderDetail?Id=' + gdIdForPI + '&isMultiUnit=' + multi, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            if (root.internationalPurchase == 'true') {
                                root.purchase.actionProcess = response.data.actionProcess;
                                root.purchase.purchaseAttachments = response.data.purchaseAttachments;
                                root.purchase.paymentVoucher = response.data.paymentVoucher;
                                response.data.purchaseOrderExpenses.forEach(function (item) {
                                    if (item.amount - item.usedAmount > 0) {
                                        root.purchase.purchasePostExpense.push({
                                            id: item.id,
                                            date: item.date,
                                            bankCashAccountId: item.bankCashAccountId,
                                            contactAccountId: item.contactAccountId,
                                            vatAccountId: item.vatAccountId,
                                            taxRateId: item.taxRateId,
                                            taxMethod: item.taxMethod,
                                            voucherNumber: item.voucherNumber,
                                            narration: item.narration,
                                            chequeNumber: item.chequeNumber,
                                            amount: item.amount - item.usedAmount,
                                            paymentMode: item.paymentMode,
                                            paymentMethod: item.paymentMethod,
                                        });
                                    }
                                });
                            }

                            if (!isEdit) {
                                root.purchase.goodReceiveNoteItems = [];
                                root.purchase.supplierQuotationId = response.data.id;
                                root.purchase.purchaseOrderId = '';
                                root.purchase.supplierId = response.data.supplierId;
                                root.purchase.taxMethod = response.data.taxMethod;
                                root.purchase.taxRateId = response.data.taxRateId;
                                root.purchase.isCredit = false;

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

                                root.rander++;
                                root.supplierRender++;
                                root.expandPurchaseOrder = false;
                                root.poIdForPI = false;
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

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            }
            //else if (gdIdForPI != null && gdIdForPI) {
            //    this.GetGRNData(gdIdForPI, false);
            //}
        },
        GetGRNData: function (id, isEdit) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            console.log(isEdit);
            var multi = localStorage.getItem('IsMultiUnit') == 'true' ? true : false
            root.$https.get('/Purchase/GoodReceiveDetail?Id=' + id + '&isMultiUnit=' + multi, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {


                        if (!isEdit) {
                            root.purchase.goodReceiveNoteItems = [];
                            //root.purchase.purchaseOrderId = response.data.id;
                            root.purchase.supplierId = response.data.supplierId;
                            root.purchase.taxMethod = response.data.taxMethod;
                            root.purchase.taxRateId = response.data.taxRateId;
                            root.purchase.isCredit = false;

                            response.data.goodReceiveNoteItems.forEach(function (item) {
                                {
                                    root.purchase.goodReceiveNoteItems.push({
                                        rowId: item.id,
                                        id: item.id,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.product.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        basicUnit: item.unit == null ? '' : item.unit.name,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        quantity: item.remainingQuantity,
                                        highQty: item.highQty,
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        serial: item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.product.guarantee,
                                        guaranteeDate: item.guaranteeDate,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                    });
                                }
                            });
                            root.rander++;
                            root.randerLineItem++;
                            root.gdIdForPI = '';
                            root.expandGoodsRecieve = false;
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

                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        AutoIncrementCode: function () {

            
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            var branchId = localStorage.getItem('BranchId');
            
            root.$https.get("/Purchase/GoodReceiveAutoGenerateNo?branchId=" + branchId, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {
                        root.purchase.registrationNo = response.data;
                    }
                });




        },
        SavePurchaseItems: function (goodReceiveNoteItems, discount, adjustmentSignProp, transactionLevelDiscount) {
            this.purchase.goodReceiveNoteItems = goodReceiveNoteItems;

            this.purchase.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

            this.purchase.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            this.getTotalAmount();
        },

        updateDiscountChanging: function (isFixed, isBeforeTax) {
            this.purchase.isFixed = isFixed
            this.purchase.isBeforeTax = isBeforeTax
        },

        getTotalAmount: function () {
            this.purchase.dueAmount = this.$refs.childComponentRef.getTotalAmount();
        },

        savePurchasePost: function (invoiceType) {

            this.purchase.approvalStatus = invoiceType;

            this.loading = true;
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            if (this.purchase.invoiceDate == "Invalid date") {
                this.purchase.invoiceDate = "";
            }
            if (this.purchase.taxMethod == "غير شامل") {
                this.purchase.taxMethod = "Exclusive";
            }
            if (this.purchase.taxMethod == "شامل") {
                this.purchase.taxMethod = "Inclusive";
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




            var purchasePost = {
                id: this.purchase.id,
                date: this.purchase.date,

                registrationNo: this.purchase.registrationNo,
                supplierId: this.purchase.supplierId,
                invoiceNo: this.purchase.invoiceNo,
                invoiceDate: this.purchase.invoiceDate,
                purchaseOrder: this.purchase.purchaseOrder,
                wareHouseId: this.purchase.wareHouseId,
                purchaseOrderId: this.purchase.purchaseOrderId,
                supplierQuotationId: this.purchase.supplierQuotationId,
                taxRateId: this.purchase.taxRateId,
                taxMethod: this.purchase.taxMethod,
                isRaw: this.purchase.isRaw,
                goodReceiveNoteItems: this.purchase.goodReceiveNoteItems,
                purchaseAttachments: this.purchase.purchaseAttachments,
                purchasePostExpense: this.purchase.purchasePostExpense,
                approvalStatus: this.purchase.approvalStatus,
                partiallyPurchase: this.purchase.partiallyPurchase,
                dueAmount: this.purchase.dueAmount,
                isCredit: this.purchase.isCredit,
                attachmentList: this.purchase.attachmentList,
                bankCashAccountId: this.purchase.isCredit ? '' : this.purchase.bankCashAccountId,
                paymentType: this.purchase.isCredit ? '' : this.purchase.paymentType,
                poNumber: this.purchase.poNumber,
                reference: this.purchase.reference,
                goodsRecieveNumberAndDate: this.purchase.goodsRecieveNumberAndDate,
                discount: this.purchase.discount,
                transactionLevelDiscount: this.purchase.transactionLevelDiscount,
                isDiscountOnTransaction: this.purchase.isDiscountOnTransaction,
                isFixed: this.purchase.isFixed,
                isBeforeTax: this.purchase.isBeforeTax,
                totalAfterDiscount: this.purchase.totalAfterDiscount,
                branchId: localStorage.getItem('BranchId'),
                bomId : this.purchase.bomId,
                saleOrderId : this.purchase.saleOrderId,
                totalAmount: this.purchase.totalAmount,
                vatAmount: this.purchase.vatAmount,
                discountAmount: this.purchase.discountAmount,
                grossAmount: this.purchase.grossAmount,
                note: this.purchase.note,
                billingId: this.purchase.billingId,
            };

            purchasePost.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            this.$https
                .post('/Purchase/SaveGoodReceiveInformation', purchasePost, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then((response) => {

                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal({
                            title: this.$t('AddPurchase.Saved'),
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        })
                        root.$router.push({
                            path: '/GoodReceive',
                            query: {
                                data: 'Addpurchase'
                            }
                        })

                    }
                    else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal({
                            title: this.$t('AddPurchase.Saved'),
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        root.$router.push({
                            path: '/GoodReceive',
                            query: {
                                data: 'Addpurchase'
                            }
                        })
                    }
                    else {
                        root.$swal({
                            title: this.$t('AddPurchase.Error'),
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }

                })
                .catch((error) => {
                    console.log(error);
                    root.$swal.fire({
                        icon: 'error',
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                        text: error.response.data,
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });
                    root.loading = false;
                })
                .finally(() => (root.loading = false));
        },
        savePurchasePostPrint: function (isPurchasePost) {
            this.loading = true;
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }

            var purchasePost = {
                id: this.purchase.id,
                date: this.purchase.date,
                registrationNo: this.purchase.registrationNo,
                supplierId: this.purchase.supplierId,
                invoiceNo: this.purchase.invoiceNo,
                invoiceDate: this.purchase.invoiceDate,
                purchaseOrder: this.purchase.purchaseOrder,
                wareHouseId: this.purchase.wareHouseId,
                purchasePostItems: this.purchase.goodReceiveNoteItems,
                isPurchasePost: isPurchasePost,
            };
            purchasePost.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            this.$https
                .post("/PurchasePost/SavePurchasePostInformation", purchasePost, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then((response) => {
                    root.loading = false;
                    root.$swal.fire({
                        icon: "success",
                        title: "Saved Successfully",
                        showConfirmButton: false,

                        timer: 800,
                        timerProgressBar: true,
                    });
                    root.printId = response.data.id;
                }).then(function () {
                    root.$https
                        .get("/PurchasePost/PurchasePostDetail?Id=" + root.printId, {
                            headers: { Authorization: `Bearer ${token}` },
                        })
                        .then(function (response) {
                            if (response.data != null) {

                                root.printDetails = response.data;
                                root.printRender++;
                            }
                        });
                })
                .catch((error) => {
                    console.log(error);
                    root.$swal.fire({
                        icon: "error",
                        title: "Something Went Wrong!",
                        text: error,
                    });

                    root.loading = false;
                })
                .finally(() => (root.loading = false));
        },
        savePurchase: function () {
            this.loading = true;
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.purchase.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            this.$https.post("/Purchase/SavePurchaseInformation", root.purchase, { headers: { Authorization: `Bearer ${token}` }, })
                .then((response) => {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                        root.loading = false
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

                        });
                        root.$router.go('addpurchase');
                    }
                    else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        root.loading = false
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

                        });

                        root.$router.push({
                            path: '/GoodReceive',
                            query: {
                                data: 'Addpurchase'
                            }
                        })
                    }
                    else {
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
                .catch((error) => {
                    console.log(error);
                    root.$swal.fire({
                        icon: "error",
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                        text: error,
                    });

                    root.loading = false;
                })
                .finally(() => (root.loading = false));
        },

        goToPurchase: function () {
            {
                this.$router.push({
                    path: '/GoodReceive',
                    query: {
                        data: 'Addpurchase'
                    }
                })
            }


        },
    },
    created: function () {
        

        var root = this;
        this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;



        if (root.$route.query.Add == 'false') {
            root.$route.query.data = this.$store.isGetEdit;
        }

        this.$emit('update:modelValue', this.$route.name);

        if (localStorage.getItem('IsPurchaseCredit') == 'true') {

            this.purchase.isCredit = true
        }
        else {
            this.purchase.isCredit = false
        }
        if (!this.purchase.isCredit) {
            if (localStorage.getItem('CashAccountId') != null && localStorage.getItem('CashAccountId') != undefined && localStorage.getItem('CashAccountId') != '' && localStorage.getItem('CashAccountId') != 'null' && localStorage.getItem('CashAccountId') != "null" && localStorage.getItem('CashAccountId') != "00000000-0000-0000-0000-000000000000" && localStorage.getItem('CashAccountId') != '00000000-0000-0000-0000-000000000000') {

                this.purchase.bankCashAccountId = localStorage.getItem('CashAccountId');
            }
        }

        this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');

        this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
        this.CanSelectWarehouse = localStorage.getItem('CanSelectWarehouse')
        this.internationalPurchase = localStorage.getItem('InternationalPurchase');
        this.purchase.partiallyPurchase = localStorage.getItem('PartiallyPurchase') == 'true' ? true : false;
        this.purchase.autoPurchaseVoucher = localStorage.getItem('AutoPurchaseVoucher') == 'true' ? true : false;
        this.purchase.expenseToGst = localStorage.getItem('ExpenseToGst') == 'true' ? true : false;
        this.defaultVat = localStorage.getItem('DefaultVat');
        //this.purchaseOrder = localStorage.getItem('PurchaseOrder') == 'true' ? true : false;


        if (this.$route.query.data != undefined) {
            var detail = this.$route.query.data;

            this.purchase.actionProcess = detail.actionProcess;
            this.purchase.date = detail.date;
            this.purchase.discountAmount = detail.discountAmount;
            this.purchase.id = detail.id;
            this.purchase.clone = this.$route.query.clone == 'true' ? true : false;
            this.purchase.isConversion = this.$route.query.isConversion == 'true' ? true : false;
            if (this.purchase.isConversion) {
                if (this.$route.query.clone == 'PurchaseOrder') {

                    this.purchase.purchaseOrderId = detail.id;
                }
                else if (this.$route.query.clone == 'SaleOrder') {

                    
                    this.purchase.purchaseOrderId = null;
                    this.purchase.bomId = this.$route.query.bomId;
                    this.purchase.saleOrderId = this.$route.query.saleOrderId;
                }
                else {
                    this.purchase.supplierQuotationId = detail.id;

                }
                this.purchase.supplierId = detail.supplierId;


                this.selectedValue = detail.registrationNo + " " + moment(detail.date).format("LLL") + " " + detail.netAmount;
                this.selectedValue1 = detail.registrationNo + " " + moment(detail.date).format("LLL") + " " + detail.netAmount;
                this.purchase.reference = detail.reference;
                this.recordForAdditionalOpt.reference = detail.reference;
                this.purchase.supplierQuotationNo = this.selectedValue1;
                this.purchase.documentType = '';
                this.purchase.id = '00000000-0000-0000-0000-000000000000';
                this.AutoIncrementCode();

                this.purchase.wareHouseId = localStorage.getItem('WareHouseId');
                this.purchase.taxRateId = localStorage.getItem('TaxRateId');
                this.purchase.taxMethod = localStorage.getItem('taxMethod');
                this.discountTypeOption = localStorage.getItem('DiscountTypeOption');
                this.purchase.isDiscountOnTransaction = localStorage.getItem('DiscountTypeOption') === 'At Transaction Level' ? true : false;

                this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-';
                this.purchase.date = moment().format("LLL");
                this.purchase.invoiceDate = detail.invoiceDate;




            }
            else {
                this.selectedValue1 = detail.purchaseOrderValue;
                this.selectedValue = detail.purchaseOrderValue;

                if (this.purchase.clone) {
                    this.purchase.id = '00000000-0000-0000-0000-000000000000';
                    this.AutoIncrementCode();


                }


                this.purchase.invoiceDate = detail.invoiceDate;
                this.purchase.invoiceFixDiscount = detail.invoiceFixDiscount;
                this.purchase.invoiceNo = detail.invoiceNo;
                this.purchase.billingId = detail.billingId;
                this.purchase.isPurchasePost = detail.isPurchasePost;
                this.purchase.isPurchaseReturn = detail.isPurchaseReturn;
                this.purchase.isRaw = detail.isRaw;
                this.purchase.netAmount = detail.netAmount;
                this.purchase.purchaseInvoiceId = detail.purchaseInvoiceId;
                this.purchase.purchaseOrderId = detail.purchaseOrderId;
                this.purchase.purchaseOrderNo = detail.purchaseOrderNo;
                this.purchase.goodReceiveNoteItems = detail.purchasePostItems;
                this.purchase.registrationNo = detail.registrationNo;
                this.purchase.supplierId = detail.supplierId;
                this.purchase.taxMethod = detail.taxMethod;
                this.purchase.taxRateId = detail.taxRateId;
                this.purchase.wareHouseId = detail.wareHouseId;
                this.purchase.attachmentList = detail.attachmentList;
                this.purchase.poNumber = detail.poNumber;
                this.purchase.goodsRecieveNumberAndDate = detail.goodsRecieveNumberAndDate;
                this.purchase.reference = detail.reference;

                this.recordForAdditionalOpt.goodsRecieveNumberAndDate = detail.goodsRecieveNumberAndDate;
                this.recordForAdditionalOpt.reference = detail.reference;
                this.recordForAdditionalOpt.invoiceNo = detail.invoiceNo;
                this.recordForAdditionalOpt.isRaw = detail.isRaw;
                this.recordForAdditionalOpt.poNumber = detail.poNumber;

                this.purchase.bankCashAccountId = detail.bankCashAccountId;
                this.purchase.paymentType = detail.paymentType;
                this.purchase.isCredit = detail.isCredit;
                if (this.purchase.purchaseOrderId != null && this.internationalPurchase == 'true') {
                    this.GetPoData(this.purchase.purchaseOrderId, true, this.gdIdForPI);
                }

                this.purchase.discount = detail.discount;
                this.purchase.transactionLevelDiscount = detail.transactionLevelDiscount;
                this.purchase.isDiscountOnTransaction = detail.isDiscountOnTransaction;
                this.purchase.isFixed = detail.isFixed;
                this.purchase.isBeforeTax = detail.isBeforeTax;
                this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'

                this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'
                this.rander++;
            }
        }
        else {
            this.purchase.wareHouseId = localStorage.getItem('WareHouseId');
            this.purchase.taxRateId = localStorage.getItem('TaxRateId');
            this.purchase.taxMethod = localStorage.getItem('taxMethod');
            this.discountTypeOption = localStorage.getItem('DiscountTypeOption');
            this.purchase.isDiscountOnTransaction = localStorage.getItem('DiscountTypeOption') === 'At Transaction Level' ? true : false;

            this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-';
            this.purchase.date = moment().format("LLL");
        }
    },
    mounted: function () {
        this.language = this.$i18n.locale;
        this.CanSelectWarehouse = localStorage.getItem('CanSelectWarehouse') == 'true' ? true : false;

        this.purchase.allowPreviousFinancialPeriod = localStorage.getItem('AllowPreviousFinancialPeriod') == 'true' ? true : false;
        this.internationalPurchase = localStorage.getItem('InternationalPurchase');

        this.currency = localStorage.getItem('Currency');

        this.paymentTypeOptions = ['Cash', 'Bank'];

        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
            this.options = ['Inclusive', 'Exclusive'];
        }
        else {
            this.options = ['شامل', 'غير شامل'];
        }

        this.raw = localStorage.getItem('IsProduction');


        this.AutoIncrementCode();

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
}
</style>
