<template>
    <div class="row" v-if="isValid('CreditInvoices') || isValid('CashInvoices') || isValid('CanHoldInvoices') || isValid('CanEditHoldInvoices')">
        <div class="col-lg-12" v-if="isDayAlreadyStart">
            <div class="row">
                <div class="col d-flex align-items-baseline">
                    <div class="media" v-if="sale.documentType=='SaleInvoice'">
                        <span class="circle-singleline" style="background-color: #1761FD !important;">{{prefix.sInvoice}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="isService==true">{{ $t('Sale.SaleServiceInvoice') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <h6 class="m-0 font-20" v-else>{{ $t('Sale.SaleInvoice') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;" v-if="formName!='CreateDocument'">{{ sale.registrationNo }}</p>
                                <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>

                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType=='GlobalInvoice'">
                        <span class="circle-singleline" style="background-color: #03D87F !important;">{{prefix.globalInvoice}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20">{{ $t('Sale.GlobalInvoice') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType=='ReceiptInvoice'">
                        <span class="circle-singleline" style="background-color: #12a4ed !important;">{{prefix.receiptInvoice}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20">{{ $t('Sale.ReceiptInvoice') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType=='SaleOrder' || sale.documentType=='ServiceSaleOrder' ">
                        <span class="circle-singleline" style="background-color: purple !important;">{{prefix.sOrder}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="isService==true">{{ $t('SaleOrder.ServiceSaleOrder') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <h6 class="m-0 font-20" v-else>{{ $t('SaleOrder.SaleOrder') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;" v-if="sale.documentType=='ServiceSaleOrder'">{{ sale.registrationNo }}</p>
                                <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType=='SaleQuotation' || formName=='ServiceQuotation'  ">
                        <span class="circle-singleline" style="background-color: #f5325c !important;">{{prefix.sQutation}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="isService==true">{{ $t('Quotation.ServiceQuotation') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <h6 class="m-0 font-20" v-else>{{ $t('Quotation.Quotation') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;" v-if="sale.documentType=='ServiceQuotation'">{{ sale.registrationNo }}</p>
                                <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType == 'ProformaInvoice' || sale.documentType=='ServiceProformaInvoice'">
                        <span class="circle-singleline" style="background-color: #ffb822!important;">{{prefix.proformaSaleInvoice}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="isService==true">{{ $t('ProformaInvoices.ServiceProformaInvoice') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <h6 class="m-0 font-20" v-else>{{ $t('ProformaInvoices.ProformaInvoices') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;" v-if="sale.documentType=='ServiceProformaInvoice' || sale.documentType=='ProformaInvoice'">{{ sale.registrationNo }}</p> <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType == 'PurchaseOrder'">
                        <span class="circle-singleline" style="background-color: #ffb822!important;">{{prefix.pOrder}} </span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20" v-if="isService==true">{{ $t('Dashboard.CustomerPurchaseOrder') }}<span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <h6 class="m-0 font-20" v-else>{{ $t('Dashboard.CustomerPurchaseOrder') }}<span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <p class="text-muted mb-0" style="font-size:13px !important;" v-if="sale.documentType=='PurchaseOrder'">{{ sale.registrationNo }}</p> <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="media" v-if="sale.documentType == 'CreditNote'">
                        <span class="circle-singleline" style="background-color: gray!important;">{{prefix.creditNote}}</span>
                        <div class="media-body align-self-center ms-3">
                            <h6 class="m-0 font-20">{{ $t('CreditNote.CreditNote') }} <span class="mx-2" style="font-size: 13px !important;">{{ sale.date }}</span></h6>
                            <div class="col d-flex ">
                                <button type="button" class="btn btn-link btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModalModify" style="padding-top: 0px !important;padding-left: 0px !important;" v-if="formName=='CreateDocument'">
                                    Change Document Type
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-auto align-self-center" v-if="(formName=='ServiceQuotation' || formName=='ServiceSaleOrder' || formName=='ServiceProformaInvoice' || formName=='PurchaseOrder')">

                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                        {{ $t('Sale.Close') }}
                    </a>
                </div>
                <div class="col-auto align-self-center" v-else>
                    <div class="form-check form-check-inline" v-if="isValid('CashInvoices')">
                        <input v-model="sale.isCredit" @change="isCredit(false)" name="contact-sub-type" id="a49946497" class=" form-check-input" type="radio" v-bind:value="false" v-bind:disabled="sale.isEditPaidInvoice">
                        <label class="form-check-label pl-0" for="a49946497">{{ $t('AddSale.Cash') }}</label>
                    </div>
                    <div class="form-check form-check-inline" v-if="isValid('CreditInvoices')">
                        <input v-model="sale.isCredit" @change="isCredit(true)" name="contact-sub-type" id="a9ff8eb35" class=" form-check-input" type="radio" v-bind:value="true" v-bind:disabled="sale.isEditPaidInvoice">
                        <label class="form-check-label pl-0" for="a9ff8eb35">{{ $t('AddSale.Credit') }}</label>
                    </div>

                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                        {{ $t('Sale.Close') }}
                    </a>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row">
                <div class="col-lg-6">
                    <div class="row form-group" v-if="isValid('CashInvoicesForCustomer') || isValid('CreditInvoices')">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSale.Customer') }} : <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown :disable="sale.isEditPaidInvoice" v-model="sale.customerId" :formName="formName" :paymentTerm="paymentMethod" ref="CustomerDropdown" @update:modelValue="emptyCashCustomer" :modelValue="sale.customerId" :key="customerRender" :isCredit="sale.isCredit" />
                            <a v-if="isWalkIn" href="javascript:void(0);" class="text-secondary">
                                {{ $t('AddSale.ViewCustomerDetails') }}
                            </a>
                            <a v-else-if="sale.customerId != null && sale.customerId != '' " href="javascript:void(0);" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight" class="text-primary">{{ $t('AddSale.ViewCustomerDetails') }}</a>
                            <a v-else href="javascript:void(0);" class="text-secondary">
                                {{
                                    $t('AddSale.ViewCustomerDetails')
                                }}
                            </a>
                            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                                <div class="offcanvas-header">
                                    <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddSale.ViewCustomerDetails') }}</h5>
                                    <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'margin-left:257px !important' : 'margin-left:0px !important'" type="button" class="btn btn-outline-primary" @click="UpdateCustomerDetail(sale.customerIdForUpdate)">{{ $t('AddSale.Update') }}</button>
                                    <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas" aria-label="Close"></button>
                                </div>
                                <div class="offcanvas-body">
                                    <div class="row">
                                        <div class="col-lg-12 form-group">
                                            <label> {{ $t('AddSale.CustomerId') }}:</label>
                                            <input type="text" class="form-control" readonly v-model="sale.customerCode" />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('Display Name') }} :</label>
                                            <input type="text" class="form-control" readonly v-model="sale.name" />
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
                                                    <input type="text" class="form-control" readonly v-model="sale.companyNameEnglish" />
                                                </div>
                                                <div class="col-lg-6 form-group">
                                                    <input type="text" class="form-control" readonly v-model="sale.companyNameArabic" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddCustomer.CommercialRegistrationNo') }} :</label>
                                            <input type="text" class="form-control" v-model="sale.commercialRegistrationNo" disabled />
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
                                            <label>{{ $t('AddSale.CustomerAddress') }} :</label>
                                            <textarea rows="3" v-model="sale.address" class="form-control"> </textarea>
                                        </div>
                                        <div class="row" v-if="multipleAddress">
                                            <div class="chat-box-left" style="width: 100%; height: 330px !important;min-height: 200px;">
                                                <ul class="nav nav-pills mb-3 nav-justified" id="pills-tab" role="tablist">
                                                    <li class="nav-item">
                                                        <a class="nav-link active" id="general_chat_tab" data-bs-toggle="pill" href="#general_chat">Delivery</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" id="group_chat_tab" data-bs-toggle="pill" href="#group_chat">Shipping</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" id="personal_chat_tab" data-bs-toggle="pill" href="#personal_chat">Billing</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" id="National_tab" data-bs-toggle="pill" href="#National_chat">National</a>
                                                    </li>
                                                </ul>

                                                <div class="chat-list" data-simplebar="init" style="height:330px !important ;min-height: 200px;">
                                                    <div class="simplebar-wrapper" style="margin: 0px;">
                                                        <div class="simplebar-height-auto-observer-wrapper">
                                                            <div class="simplebar-height-auto-observer"></div>
                                                        </div>
                                                        <div class="simplebar-mask">
                                                            <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                                                                <div class="simplebar-content-wrapper" style="height: 250px !important;min-height: 200px; overflow: hidden scroll;">
                                                                    <div class="simplebar-content" style="padding: 0px;min-height: 200px; ">
                                                                        <div class="tab-content " id="pills-tabContent">
                                                                            <div class="tab-pane fade active show" id="general_chat">
                                                                                <a href="javascript:void(0)" v-for="person  in deliveryAddressList" v-bind:key="person.id">
                                                                                    <div class="media new-message " style="padding: 7px 10px 10px 7px !important;" v-if="person.type=='Delivery'">
                                                                                        <div class="media-left" style="text-align: left !important;">
                                                                                            <div class="checkbox form-check-inline d-flex">
                                                                                                <span class="badge badge-boxed  badge-outline-primary me-2" v-bind:class="person.isDefault?'visibilityOn':'visibility'" v-show="person.isDefault">Default</span>
                                                                                                <input type="checkbox" v-if="person.isOffice" v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,false)">
                                                                                                <input type="checkbox" v-else v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,true)"><label v-bind:for="person.id"></label>
                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="media-body">
                                                                                            <div class="d-inline-block" style="width: 100% !important;text-align:left;">
                                                                                                <h6>{{ person.area }}</h6>
                                                                                                <p>{{ person.address }}</p>
                                                                                                <p>{{person.googleLocation }}</p>
                                                                                                <p>{{ person.nearBy }}</p>

                                                                                            </div>

                                                                                        </div><!-- end media-body -->
                                                                                    </div>

                                                                                </a>
                                                                                <div class="text-center mt-4 d-flex justify-content-center align-items-center flex-column ">
                                                                                    <div>
                                                                                        <a v-on:click="AddRow1('Delivery')" data-bs-dismiss="offcanvas" aria-label="Close" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">

                                                                                            Register an Address
                                                                                        </a>
                                                                                    </div>
                                                                                    <div v-if="!deliveryAddressList.some(x=>x.type=='Delivery')" class="text-center"> No Address Registered</div>

                                                                                </div>

                                                                            </div>
                                                                            <!--end general chat-->

                                                                            <div class="tab-pane fade" id="group_chat">
                                                                                <div class="tab-pane fade active show" id="general_chat">
                                                                                    <a href="javascript:void(0)" v-for="person  in deliveryAddressList" v-bind:key="person.id">
                                                                                        <div class="media new-message " style="padding: 7px 10px 10px 7px !important;" v-if="person.type=='Shipping'">
                                                                                            <div class="media-left" style="text-align: left !important;">
                                                                                                <div class="checkbox form-check-inline d-flex">
                                                                                                    <span class="badge badge-boxed  badge-outline-primary me-2" v-bind:class="person.isDefault?'visibilityOn':'visibility'" v-show="person.isDefault">Default</span>
                                                                                                    <input type="checkbox" v-if="person.isOffice" v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,false)">
                                                                                                    <input type="checkbox" v-else v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,true)">                                                                                                    <label v-bind:for="person.id"></label>
                                                                                                </div>

                                                                                            </div>
                                                                                            <div class="media-body">
                                                                                                <div class="d-inline-block" style="width: 100% !important;text-align:left;">
                                                                                                    <h6>{{ person.area }}</h6>
                                                                                                    <p>{{ person.address }}</p>
                                                                                                    <p>{{person.googleLocation }}</p>
                                                                                                    <p>{{ person.nearBy }}</p>

                                                                                                </div>

                                                                                            </div><!-- end media-body -->
                                                                                        </div>

                                                                                    </a>
                                                                                    <div class="text-center d-flex justify-content-center align-items-center flex-column mt-4">
                                                                                        <a v-on:click="AddRow1('Shipping')" data-bs-dismiss="offcanvas" aria-label="Close" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">

                                                                                            Register an Address
                                                                                        </a>
                                                                                    </div>
                                                                                    <div v-if="!deliveryAddressList.some(x=>x.type=='Shipping')" class="text-center"> No Address Registered</div>

                                                                                </div>

                                                                            </div>
                                                                            <!--end group chat-->

                                                                            <div class="tab-pane fade" id="personal_chat">
                                                                                <div class="tab-pane fade active show" id="general_chat">
                                                                                    <a href="javascript:void(0)" v-for="person  in deliveryAddressList" v-bind:key="person.id">
                                                                                        <div class="media new-message " style="padding: 7px 10px 10px 7px !important;" v-if="person.type=='Billing'">
                                                                                            <div class="media-left" style="text-align: left !important;">
                                                                                                <div class="checkbox form-check-inline d-flex">
                                                                                                    <span class="badge badge-boxed  badge-outline-primary me-2" v-bind:class="person.isDefault?'visibilityOn':'visibility'" v-show="person.isDefault">Default</span>
                                                                                                    <input type="checkbox" v-if="person.isOffice" v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,false)">
                                                                                                    <input type="checkbox" v-else v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,true)">
                                                                                                    <label v-bind:for="person.id"></label>
                                                                                                </div>

                                                                                            </div>
                                                                                            <div class="media-body">
                                                                                                <div class="d-inline-block" style="width: 100% !important;text-align:left;">
                                                                                                    <h6>{{ person.area }}</h6>
                                                                                                    <p>{{ person.address }}</p>
                                                                                                    <p>{{person.googleLocation }}</p>
                                                                                                    <p>{{ person.nearBy }}</p>

                                                                                                </div>

                                                                                            </div><!-- end media-body -->
                                                                                        </div>

                                                                                    </a>
                                                                                    <div class="text-center d-flex justify-content-center align-items-center flex-column mt-4">
                                                                                        <a v-on:click="AddRow1('Billing')" data-bs-dismiss="offcanvas" aria-label="Close" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">

                                                                                            Register an Address
                                                                                        </a>
                                                                                    </div>
                                                                                    <div v-if="!deliveryAddressList.some(x=>x.type=='Billing')" class="text-center"> No Address Registered</div>

                                                                                </div>

                                                                                <!--end media-body-->
                                                                            </div>
                                                                            <div class="tab-pane fade" id="National_chat">
                                                                                <div class="tab-pane fade active show" id="general_chat">
                                                                                    <a href="javascript:void(0)" v-for="person  in deliveryAddressList" v-bind:key="person.id">
                                                                                        <div class="media new-message " style="padding: 7px 10px 10px 7px !important;" v-if="person.type=='National'">
                                                                                            <div class="media-left" style="text-align: left !important;">
                                                                                                <div class="checkbox form-check-inline d-flex">
                                                                                                    <span class="badge badge-boxed  badge-outline-primary me-2" v-bind:class="person.isDefault?'visibilityOn':'visibility'" v-show="person.isDefault">Default</span>
                                                                                                    <input type="checkbox" v-if="person.isOffice" v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,false)">
                                                                                                    <input type="checkbox" v-else v-model="person.isOffice" v-bind:id="person.id"  v-on:change="DefaultOnList(person,true)">                                                                                                    <label v-bind:for="person.id"></label>
                                                                                                </div>

                                                                                            </div>
                                                                                            <div class="media-body">
                                                                                                <div class="d-inline-block" style="width: 100% !important;text-align:left;">
                                                                                                    <h6>{{ person.area }}</h6>
                                                                                                    <p>{{ person.address }}</p>
                                                                                                    <p>{{person.googleLocation }}</p>
                                                                                                    <p>{{ person.nearBy }}</p>

                                                                                                </div>

                                                                                            </div><!-- end media-body -->
                                                                                        </div>

                                                                                    </a>
                                                                                    <div class="text-center d-flex justify-content-center align-items-center flex-column mt-4">
                                                                                        <div>
                                                                                            <a v-on:click="AddRow1('National')" data-bs-dismiss="offcanvas" aria-label="Close" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">

                                                                                                Register an Address
                                                                                            </a>
                                                                                        </div>

                                                                                        <div v-if="!deliveryAddressList.some(x=>x.type=='National')"> No Address Registered</div>

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
                                                        <div class="simplebar-placeholder" style="width: auto; height: 330px !important;min-height: 200px;"></div>
                                                    </div>
                                                    <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                                                        <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
                                                    </div>
                                                    <div class="simplebar-track simplebar-vertical" style="visibility: visible;">
                                                        <div class="simplebar-scrollbar" style="height: 328px !important; transform: translate3d(0px, 0px, 0px); display: block;"></div>
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
                            <a href="javascript:void(0);" class="text-secondary" v-if="sale.isEditPaidInvoice">
                                {{$t('AddStockValue.Discount&Vat/Taxoptions')}}
                            </a>
                            <a href="javascript:void(0);" class="text-primary" v-on:click="VatInputValues()" v-else>
                                {{$t('AddStockValue.Discount&Vat/Taxoptions')}}
                            </a>

                        </div>
                    </div>

                    <div class="row" v-if="isVATInput">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">{{ $t('AddSale.DiscountType') }}</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <multiselect :options="[$t('AddStockValue.AtTransactionLevel') , $t('AddStockValue.AtLineItemLevel')]" @update:modelValue="ChangeVat(discountTypeOption,'DiscountType')" v-model="discountTypeOption" @select="sale.isDiscountOnTransaction = (discountTypeOption === 'At Transaction Level' ? false : true)" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </div>
                        </div>
                        <div class="row form-group" v-if="saleDefaultVat == 'DefaultVatHead' || saleDefaultVat == 'DefaultVatHeadItem'">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.TaxMethod') }} :<span class="text-danger"> *</span></span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())" @update:modelValue="ChangeVat(sale.taxMethod,'TaxMethod')" :options="['Inclusive', 'Exclusive' , 'No Tax']" @click="sale.isFixed = false" v-model="sale.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                                <multiselect v-else :options="['شامل', 'غير شامل']" @update:modelValue="ChangeVat(sale.taxMethod,'TaxMethod')" v-model="sale.taxMethod" @select="sale.isFixed = false" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </div>
                        </div>

                        <div class="row form-group" v-if="saleDefaultVat == 'DefaultVatHead' || saleDefaultVat == 'DefaultVatHeadItem'">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.VAT%') }} :<span class="text-danger"> *</span></span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <taxratedropdown v-model="sale.taxRateId" @update:modelValue="ChangeVat(sale.taxRateId,'TaxRateId')" v-bind:modelValue="sale.taxRateId" :key="customerRender" />
                            </div>
                        </div>
                        <div class="row form-group" v-if="(isCustomerPriceLabel && isSalePriceLabel) || ( isValid('CanPricingOnSaleInvoice') || isValid('CanPricingOnSaleOrder') || isValid('CanPricingOnQuotation'))">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.PriceLabeling') }}:</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <priceLabelingDropdown v-model="priceLabeling" :modelValue="priceLabelingId" @update:modelValue="GetPrice(priceLabeling,null)" :key="priceLabelRender"></priceLabelingDropdown>
                            </div>
                        </div>
                        <div class="row form-group" v-else-if="isCustomerPriceLabel || ( isValid('CanPricingOnSaleInvoice') || isValid('CanPricingOnSaleOrder') || isValid('CanPricingOnQuotation'))">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.PriceLabeling') }}:</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <priceLabelingDropdown v-model="priceLabeling" :disabled="'true'" :modelValue="priceLabelingId" @update:modelValue="GetPrice(priceLabeling,null)" :key="priceLabelRender"></priceLabelingDropdown>
                            </div>
                        </div>
                        <div class="row form-group" v-else-if="isSalePriceLabel || ( isValid('CanPricingOnSaleInvoice') || isValid('CanPricingOnSaleOrder') || isValid('CanPricingOnQuotation'))">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.PriceLabeling') }}:</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <priceLabelingDropdown v-model="priceLabeling" :modelValue="priceLabelingId" @update:modelValue="GetPrice(priceLabeling,null)" :key="priceLabelRender"></priceLabelingDropdown>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">
                    <div class="row form-group" v-if="sale.allowPreviousFinancialPeriod">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddSale.InvoiceDate') }}:</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="sale.date" v-if="sale.isEditPaidInvoice" :isDisable="true" />
                            <datepicker v-model="sale.date" v-else />
                        </div>
                    </div>
                    <div class="row form-group">
                        <!-- <label class="col-form-label col-lg-2">
                                <span class="tooltip-container text-dashed-underline "></span>
                            </label> -->
                        <div class="inline-fields col-lg-8">
                            <a v-if="isWalkIn" href="javascript:void(0);" class="text-secondary">
                                Options
                            </a>
                            <a v-else-if="sale.customerId != null && sale.customerId != ''" href="javascript:void(0);" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight" class="text-primary">Options</a>
                            <a v-else href="javascript:void(0);" class="text-secondary">Options</a>
                            <div class="row" v-if="!isWalkIn" v-bind:key="randerEffect">
                                <div class="col-lg-12 pt-2" v-if="(sale.saleOrderId != null && sale.saleOrderId != '' && sale.saleOrderId != undefined) || (sale.quotationId != null && sale.quotationId != '' && sale.quotationId != undefined) || (sale.deliveryChallanId != null && sale.deliveryChallanId != '' && sale.deliveryChallanId != undefined) || (sale.proformaId != null && sale.proformaId != '' && sale.proformaId != undefined) || (sale.purchaseOrderId != null && sale.purchaseOrderId != '' && sale.purchaseOrderId != undefined)">

                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;" v-if="sale.saleOrderId != null && sale.saleOrderId != '' && sale.saleOrderId != undefined">

                                        <span>{{ $t('AddSale.SaleOrder') }} {{ saleOrderDescription }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('saleOrder')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;" v-else-if="sale.quotationId != null && sale.quotationId != '' && sale.quotationId != undefined">

                                        <span>{{ $t('Dashboard.Quotation') }} {{ saleOrderDescription }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('quotation')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;" v-else-if="sale.deliveryChallanId != null && sale.deliveryChallanId != '' && sale.deliveryChallanId != undefined">

                                        <span>{{ $t('DeliveryNote.DeliveryNote') }}{{ deliveryDescription }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('DeliveryNote')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;" v-else-if="sale.proformaId != null && sale.proformaId != '' && sale.proformaId != undefined">

                                        <span>Proforma Invoice {{ deliveryDescription }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('ProfomaInvoice')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;" v-else-if="sale.purchaseOrderId != null && sale.purchaseOrderId != '' && sale.purchaseOrderId != undefined">

                                        <span>Purchase Order {{ deliveryDescription }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('PurchaseOrder')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-12 pt-2" v-if="sale.dueDate != null && sale.dueDate != '' && sale.dueDate != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>Sale Due Date ({{ sale.dueDate }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('DueDate')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2" v-if="sale.deliveryDate != null && sale.deliveryDate != '' && sale.deliveryDate != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.DeliveryDate') }} ({{ sale.deliveryDate }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('deliveryDate')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <!-- <div class="col-lg-5 pt-2 "  v-if="sale.wareHouseId!=null && sale.wareHouseId!='' && sale.wareHouseId!=undefined ">
                                            <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                                <span>{{ $t('AddSale.WareHouse')}}({{wareHouseDescription }})</span>
                                                <span style="position:absolute; right: -12px; top: -8px;">
                                                    <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('WareHouse')">
                                                        <i class="fas fa-times"></i>
                                                    </button>
                                                </span>
                                            </div>

                                        </div> -->
                                <div class="col-lg-12 pt-2 " v-if="sale.logisticId!=null && sale.logisticId!='' && sale.logisticId!=undefined ">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.Transporter/Cargo')}}({{logisticDescription }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('logisticId')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 " v-if="sale.areaId != null && sale.areaId != '' && sale.areaId != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.Area') }}({{ areaDescription }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('areaId')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 " v-if="sale.employeeId != null && sale.employeeId != '' && sale.employeeId != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{$t('AddSale.SalePerson') }}({{ employeeDescription }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('employeeId')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 pe-2" v-if="sale.customerInquiry != null && sale.customerInquiry != '' && sale.customerInquiry != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.CustomerInquiry') }}({{ sale.customerInquiry }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('customerInquiry')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 pe-2" v-if="sale.deliveryAddress != null && sale.deliveryAddress != '' && sale.deliveryAddress != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.DeliveryAddress') }}({{ sale.deliveryAddress }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('deliveryAddress')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 pe-2" v-if="sale.doctorName != null && sale.doctorName != '' && sale.doctorName != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.DoctorName') }}({{ sale.doctorName }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('doctorName')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 pe-2" v-if="sale.hospitalName != null && sale.hospitalName != '' && sale.hospitalName != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.HospitalName') }}({{ sale.hospitalName }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('hospitalName')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 pe-2" v-if="sale.billingAddress != null && sale.billingAddress != '' && sale.billingAddress != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.BillingAddress') }}({{ sale.billingAddress }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('billingAddress')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-lg-12 pt-2 pe-2" v-if="sale.description != null && sale.description != '' && sale.description != undefined">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{ $t('AddSale.InvoiceFor') }}({{ sale.description }})</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveEffect('description')">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4" v-if="isValid('UploadWarrentyLogo')">
                            <div class="row ">
                                <label v-if="imageSrc == '' && sale.warrantyLogoPath=='' ">
                                    <span class="tooltip-container text-dashed-underline ">
                                        {{ $t('AddSale.UploadWarrantyLogo') }}
                                    </span>
                                </label>
                                <div :key="renderImg">
                                    <div class="input-group mb-3" v-if="!((imageSrc == '' && sale.warrantyLogoPath != '') || (imageSrc != '' && sale.warrantyLogoPath == '') || (imageSrc != '' && sale.warrantyLogoPath != '') || (imageSrc != '' && sale.warrantyLogoPath != null))">
                                        <input ref="imgupload" type="file" class="form-control" id="inputGroupFile02" @change="uploadImage()" accept="image/*" name="image">
                                    </div>
                                    <div class="text-right " v-if="imageSrc != ''">
                                        <img v-if="imageSrc != ''" class="float-right" :src="imageSrc" style="width: 121px !important;height:86px !important ;" />

                                    </div>
                                    <div v-else class="mt-2">
                                        <span v-if="sale.warrantyLogoPath != null && sale.warrantyLogoPath != ''">
                                            <img :src="'data:image/png;base64,' + sale.warrantyLogoPath" style="width: 121px !important;height:86px !important ;" />
                                        </span>

                                    </div>
                                </div>
                            </div>
                            <div class="row " v-if="imageSrc != '' || sale.warrantyLogoPath != ''">
                                <label>
                                </label>
                                <div class="col-8 offset-2">
                                    <button class="btn btn-danger  btn-sm mt-2" v-on:click="removeImage()">Remove</button>

                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight2" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">Options</h5>
                            <!-- <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button> -->
                            <!-- <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'margin-left:412px !important' : 'margin-left:0px !important'" type="button" class="btn btn-outline-primary" v-on:click="UpdateDetail">{{ $t('AddSale.Update') }}</button> -->
                            <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <div class="row">

                                <div class="col-md-12 mb-2" v-if="record3.selectedValue!=''">
                                    <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                        <span>{{record3.selectedValue }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon" style="font-size: .4rem;  padding: 0.2rem 0.35rem;" @click="RemoveRecord3()">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-md-6" v-if="formName!='ServiceQuotation' && (isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
                                    <div class="row">
                                        <div class="col-lg-6 form-group text-right">
                                            <b>{{ $t('Dashboard.Quotation') }} </b>
                                        </div>
                                        <div class="col-lg-6 form-group text-left">
                                            <button v-if="expandQuotation" v-on:click="QuotationListWithCanvas(false)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                                            <button v-else v-on:click="QuotationListWithCanvas(true)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                                        </div>
                                        <div v-if="expandQuotation" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in quotationListWithCanvas" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);" v-on:click="GetSaleOrderDetail(saleValue.id, false, saleValue,'NotSave')">
                                                    <span>{{ index + 1 }}- {{ saleValue.registrationNumber }}--{{ getDate(saleValue.date) }}</span>
                                                    <span class="float-end">{{ currency }} {{ parseFloat(saleValue.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}</span>
                                                </a>
                                                <br />
                                            </p>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-6" v-if=" formName!='ServiceQuotation' && formName!='PurchaseOrder' && !sale.isCashCustomer && formName!='ServiceSaleOrder' && isValid('CreditInvoices') && (isValid('CanViewSaleOrder') || isValid('CanAddSaleOrder'))">
                                    <div class="row">

                                        <div class="col-lg-6 form-group text-right ">
                                            <b>{{ $t('AddSale.SaleOrder') }} </b>
                                        </div>
                                        <div class="col-lg-6 form-group text-left ">
                                            <button v-if="expandSale" v-on:click="SaleOrderListWithCanvas(false)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                                            <button v-else v-on:click="SaleOrderListWithCanvas(true)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                                        </div>
                                        <div v-if="expandSale" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in saleOrderListWithCanvas" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);" v-on:click="GetSaleOrderDetail(saleValue.id, true, saleValue,'NotSave')">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{
                                                            getDate(saleValue.date)
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

                                <div class="col-md-6" v-if="formName!='ServiceSaleOrder' && formName!='ServiceQuotation' && formName!='PurchaseOrder' && isValid('DeliveryNoteToSale') && !sale.isCashCustomer ">

                                    <div class="row  ">
                                        <div class="col-lg-6 form-group text-right ">
                                            <b> {{ $t('DeliveryNote.DeliveryNote') }}</b>

                                        </div>
                                        <div class="col-lg-6 form-group text-left ">

                                            <button v-if="expandDeliveryChallan" v-on:click="DeliveryChallanListWithCanvas(false)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                                            <button v-else v-on:click="DeliveryChallanListWithCanvas(true)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                                        </div>

                                        <div v-if="expandDeliveryChallan" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in deliverychallanListWithCanvas" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);" v-on:click="GetDeliveryChallanRecord(saleValue.id, saleValue,'NotSave')">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{
                                                            getDate(saleValue.date)
                                                        }}
                                                    </span>
                                                </a>
                                                <br />
                                            </p>
                                        </div>

                                    </div>

                                </div>

                                <div class="col-md-6" v-if="formName!='ServiceSaleOrder' && formName!='ServiceQuotation' && formName!='PurchaseOrder' && formName!='ServiceProformaInvoice' && isValid('CanViewProforma')">

                                    <div class="row  ">
                                        <div class="col-lg-6 form-group text-right ">
                                            <b> Proforma Invoice</b>

                                        </div>
                                        <div class="col-lg-6 form-group text-left ">

                                            <button v-if="expandPerfomaInvoice" v-on:click="SaleListWithCanvas(false,'SaleInvoice')" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                                            <button v-else v-on:click="SaleListWithCanvas(true,'SaleInvoice')" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                                        </div>

                                        <div v-if="expandPerfomaInvoice" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in saleListWithCanvas" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);" v-on:click="GetSaleRecord(saleValue.id, saleValue,'NotSave','proforma')">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{ getDate(saleValue.date) }}--{{ currency }} {{saleValue.netAmount}}
                                                    </span>
                                                </a>
                                                <br />
                                            </p>
                                        </div>

                                    </div>

                                </div>
                                <div class="col-md-6" v-if="!sale.isCashCustomer && formName=='SaleInvoice' && isValid('ViewCustomerPO')">

                                    <div class="row  ">
                                        <div class="col-lg-6 form-group text-right ">
                                            <b> Purchase Order</b>

                                        </div>
                                        <div class="col-lg-6 form-group text-left ">

                                            <button v-if="expandPurchaseOrder" v-on:click="SaleListWithCanvas(false,'PurchaseOrder')" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                                            <button v-else v-on:click="SaleListWithCanvas(true,'PurchaseOrder')" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                                        </div>

                                        <div v-if="expandPurchaseOrder" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in saleListWithCanvas" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);" v-on:click="GetSaleRecord(saleValue.id, saleValue,'NotSave','purchaseOrder')">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{ getDate(saleValue.date) }}--{{ currency }} {{saleValue.netAmount}}
                                                    </span>
                                                </a>
                                                <br />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" v-if="formName!='ServiceSaleOrder' && formName!='ServiceQuotation' && formName!='PurchaseOrder' && !sale.isCashCustomer">
                                    <div class="row  ">
                                        <div class="col-lg-6 form-group text-right ">
                                            <b>Inquiry</b>
                                        </div>
                                        <div class="col-lg-6 form-group text-left ">
                                            <button v-if="expandInqury" v-on:click="SaleListWithCanvas(false,'Inquiry')" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                                            <button v-else v-on:click="SaleListWithCanvas(true,'Inquiry')" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                                        </div>

                                        <div v-if="expandInqury" class="col-lg-12 form-group">
                                            <p v-for="(saleValue, index) in saleListWithCanvas" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                                                <a href="javascript:void(0);" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="GetSaleRecord(saleValue.id, saleValue)">
                                                    <span>
                                                        {{ index + 1 }}- {{ saleValue.registrationNumber }}--{{ getDate(saleValue.date) }}
                                                    </span>
                                                </a>
                                                <br />
                                            </p>
                                        </div>

                                    </div>

                                </div>
                                <div class="col-md-12 text-end mt-2 mb-2" v-if="formName!='ServiceQuotation'">
                                    <div class="button-items">
                                        <button class="btn btn-outline-primary" v-bind:disabled="!isButtonDisabledOfRecord3" v-on:click="SaveRecord3">
                                            <i class="far fa-save "></i>
                                            {{ $t('AddCustomer.btnSave') }}
                                        </button>
                                        <button type="button" v-on:click="RemoveRecord3" class="btn btn-danger ">
                                            Cancel
                                        </button>
                                    </div>

                                </div>

                                <div class="accordion" id="accordionExample">
                                    <div class="accordion-item">
                                        <h5 class="accordion-header m-0" id="headingOne">
                                            <button class="accordion-button fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                Additional Options
                                            </button>
                                        </h5>
                                        <div id="collapseOne" class="accordion-collapse collapse " aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-lg-12 form-group" v-if="isValid('CanSelectWarehouse') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.WareHouse')
                                                                }} :<span class="text-danger"> *</span>
                                                            </span>
                                                        </label>
                                                        <warehouse-dropdown v-model="sale.wareHouseId" :disable="sale.isEditPaidInvoice" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="((sale.customerId != null && sale.customerId != '00000000-0000-0000-0000-000000000000') || sale.isCredit) && isValid('CanAddDueDate') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Invoice Due Date :</span>
                                                        </label>
                                                        <datepicker v-model="record1.dueDate" v-bind:key="dueDateRef" />
                                                    </div>
                                                    <div class="col-lg-6 form-group">
                                                        <label v-if="formName=='PurchaseOrder'">
                                                            <span class="tooltip-container text-dashed-underline ">PO Reference#</span>
                                                        </label>
                                                        <label v-else>
                                                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.Refrence')}}#</span>
                                                        </label>
                                                        <input v-model="record1.refrenceNo" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-12 form-group" v-if="isValid('CanAddInvoiceFor')">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline" v-if="formName == 'ServiceQuotation'">
                                                                {{ $t('AddSale.QuotationFor') }}
                                                            </span>
                                                            <span class="tooltip-container text-dashed-underline" v-else-if="formName == 'ServiceSaleOrder'">
                                                                {{ $t('AddSale.SaleOrderFor') }}
                                                            </span>
                                                            <span class="tooltip-container text-dashed-underline" v-else-if=" formName!='PurchaseOrder'">
                                                                PO For
                                                            </span>
                                                            <span class="tooltip-container text-dashed-underline" v-else>
                                                                {{ $t('AddSale.InvoiceFor') }}
                                                            </span>
                                                        </label>
                                                        <input v-model="record1.description" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanSelectTransporter') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.Transporter/Cargo')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <logisticDropdown v-model="record1.logisticId" :modelValue="record1.logisticId" @update:modelValue="LogisticRecord" ref="logisticDropdown" v-bind:key="logisticRender" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddDeliverDate') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                Dispatch Date
                                                            </span>
                                                        </label>
                                                        <datepicker v-model="record1.dispatchDate"></datepicker>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddDeliverDate') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.DeliveryDate')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <datepicker v-model="record1.deliveryDate" v-bind:key="dueDateRef1"></datepicker>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddDeliverDate') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                Pickup Date
                                                            </span>
                                                        </label>
                                                        <datepicker v-model="record1.pickupDate"></datepicker>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('ReferredBy') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Referred By</span>
                                                        </label>
                                                        <input class="form-control" v-model="record1.referredBy" />
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddSalePerson') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">{{$t('AddSale.SalePerson') }}</span>
                                                        </label>
                                                        <employeeDropdown v-model="record1.employeeId" @update:modelValue="EmployeeRecord" v-bind:key="employeeRender" :modelValue="record1.employeeId" ref="employeeIdRef"></employeeDropdown>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="formName=='ServiceQuotation' && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.ValidityDate') }} </span>
                                                        </label>
                                                        <datepicker v-model="record1.validityDate"></datepicker>
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="isArea == 'true' && isValid('CanSelectArea') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                                   $t('AddSale.Area')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <areadropdown v-model="record1.areaId" v-bind:modelValue="record1.areaId" @update:modelValue="AreaRecord" ref="AreaDropdown" />
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="formName=='ServiceQuotation' || formName=='ServiceSaleOrder' && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">{{$t('AddQuotation.ClientPurchaseNo')}}:</span>
                                                        </label>
                                                        <input v-model="record1.clientPurchaseNo" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="formName=='ServiceQuotation' && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.Type') }}:#</span>
                                                        </label>
                                                        <multiselect v-model="record1.purpose" :options="['Quotation', 'Proposal']" :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                                                        </multiselect>
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddCustomerInquiry') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.CustomerInquiry')
                                                                }} #
                                                            </span>
                                                        </label>
                                                        <input v-model="record1.customerInquiry" class="form-control" />
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="isForMedical == 'true' && isValid('CanAddDoctorName') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.DoctorName')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <input v-model="record1.doctorName" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-12 form-group" v-if="wholesalePriceActivation && isValid('CanSelectPriceType') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                                $t('AddSale.SelectPriceType')
                                                                }}:<span class="text-danger"> *</span>
                                                            </span>
                                                        </label>
                                                        <multiselect v-model="priceType" label="name" :options="priceoptions" :searchable="false" :close-on-select="true" :show-labels="false" placeholder="Pick a value"></multiselect>
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="isForMedical == 'true' && isValid('CanAddHospitalName') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.HospitalName')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <input v-model="record1.hospitalName" class="form-control" />
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddPONumber')">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.PONumber')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <input v-model="record1.poNumber" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="isValid('CanAddPONumber')">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline "> {{$t('AddSale.PODate') }}</span>
                                                        </label>
                                                        <datepicker v-bind:key=randerPoDate v-model="record1.poDate" />
                                                    </div>
                                                    <div class="col-md-12 text-end">
                                                        <div class="button-items">
                                                            <button class="btn btn-outline-primary" v-if="isUpdateRecord1" v-on:click="SaveRecord1" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                                <i class="far fa-save "></i>
                                                                <span>Update </span>
                                                            </button>
                                                            <button v-else class="btn btn-outline-primary" v-bind:disabled="!isButtonDisabledOfRecord" v-on:click="SaveRecord1" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                                <i class="far fa-save "></i>
                                                                {{ $t('AddCustomer.btnSave') }}
                                                            </button>

                                                            <button class="btn btn-danger" v-if="isUpdateRecord1" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                                Close
                                                            </button>
                                                            <button class="btn btn-danger" v-else v-on:click="RemoveRecord1" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                                {{ $t('AddCustomer.Cancel') }}
                                                            </button>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="accordion-item">
                                        <h5 class="accordion-header m-0" id="headingTwo">
                                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                Manual References
                                            </button>
                                        </h5>
                                        <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-lg-6 form-group" v-if=" formName!='ServiceQuotation' && (isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Quotation No. & Date </span>
                                                        </label>
                                                        <input v-model="record2.quotationNo" v-bind:disabled="!(record2.quotationId == '' || record2.quotationId == null)" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if=" formName!='ServiceQuotation' && (isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Valid Upto / For </span>
                                                        </label>
                                                        <input v-model="record2.quotationValidUpto" v-bind:disabled="!(record2.quotationId == '' || record2.quotationId == null)" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if=" formName!='ServiceProformaInvoice' && isValid('CanViewProforma') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Proforma Invoice No. & Date </span>
                                                        </label>
                                                        <input v-model="record2.peroformaInvoiceNo" v-bind:disabled="!(record2.proformaId == '' || record2.proformaId == null)" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="formName!='ServiceProformaInvoice' && isValid('CanViewProforma') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Valid Upto / For </span>
                                                        </label>
                                                        <input v-model="record2.perfomaValidUpto" v-bind:disabled="!(record2.proformaId == '' || record2.proformaId == null)" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Purchase Order No. & Date </span>
                                                        </label>
                                                        <input v-model="record2.purchaseOrderNo" v-bind:disabled="!(record2.purchaseOrderId == '' || record2.purchaseOrderId == null)" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if="formName!='ServiceSaleOrder' &&  (isValid('CanViewSaleOrder') || isValid('CanAddSaleOrder')) && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Sale Order No. & Date </span>
                                                        </label>
                                                        <input v-model="record2.saleOrderNo" v-bind:disabled="!(record2.saleOrderId == '' || record2.saleOrderId == null)" class="form-control" />
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Inquiry No. & Date </span>
                                                        </label>
                                                        <input v-model="record2.inquiryNo" v-bind:disabled="!(record2.inquiryId == '' || record2.inquiryId == null)" class="form-control" />
                                                    </div>
                                                    <div class="col-lg-6 form-group" v-if=" isValid('DeliveryNoteToSale') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">Delivery Note No. & Date </span>
                                                        </label>
                                                        <input v-model="record2.deliveryNoteAndDate" v-bind:disabled="!(record2.deliveryChallanId == '' || record2.deliveryChallanId == null)" class="form-control" />
                                                    </div>

                                                    <div class="col-lg-6 form-group" v-if="isRaw == 'true' && isValid('CanSelectDispatchNote') && formName!='PurchaseOrder'">
                                                        <label>
                                                            <span class="tooltip-container text-dashed-underline ">
                                                                {{
                                                    $t('AddSale.DispatchNote')
                                                                }}
                                                            </span>
                                                        </label>
                                                        <multiselect v-model="record2.dispatchNote" :options="options" :multiple="true" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('AddSale.SelectOption')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :preselect-first="true" @update:modelValue="GetDispatchNoteDetail(record2.dispatchNote, record2.dispatchNote.length)">
                                                        </multiselect>
                                                    </div>
                                                    <div class="col-md-12 text-end">
                                                        <div class="button-items">

                                                            <button v-if="isUpdateRecord2" class="btn btn-outline-primary" v-on:click="SaveRecord" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                                <i class="far fa-save "></i>
                                                                Update
                                                            </button>
                                                            <button v-else class="btn btn-outline-primary" v-bind:disabled="!isButtonDisabledOfRecord2" v-on:click="SaveRecord" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                                <i class="far fa-save "></i>
                                                                {{ $t('AddCustomer.btnSave') }}
                                                            </button>
                                                            <button v-if="isUpdateRecord2" class="btn btn-danger" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                                Close
                                                            </button>
                                                            <button v-else class="btn btn-danger" v-on:click="RemoveRecord2" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                                {{ $t('AddCustomer.Cancel') }}
                                                            </button>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12 text-end mt-2">
                                        <div class="button-items">

                                            <button type="button" class="btn btn-danger " data-bs-dismiss="offcanvas" aria-label="Close">
                                                Close Options
                                            </button>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>

                <sale-service-item v-if="isService" :saleItems="sale.saleItems" ref="childComponentRef" :wareHouseId="sale.wareHouseId" :wholesale="priceType" :saleOrderId="sale.saleOrderId" @update:modelValue="SaveSaleItems" @summary="updateSummary" @NoteSave="NoteSave" @discountChanging="updateDiscountChanging" :newtaxMethod="sale.taxMethod" :taxRateId="sale.taxRateId" :adjustmentProp="sale.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="sale.isDiscountOnTransaction" :transactionLevelDiscountProp="sale.transactionLevelDiscount" :isFixed="sale.isFixed" :isbeforetax="sale.isBeforeTax" :noteVal="sale.note" :formName="formName" :isEditPaidInvoice="sale.isEditPaidInvoice" />
                <sale-item v-else :saleItems="sale.saleItems" ref="childComponentRef" :wareHouseId="sale.wareHouseId" @NoteSave="NoteSave" :wholesale="priceType" :saleOrderId="sale.saleOrderId" @update:modelValue="SaveSaleItems" @summary="updateSummary" :newtaxMethod="sale.taxMethod" :taxRateId="sale.taxRateId" @discountChanging="updateDiscountChanging" :adjustmentProp="sale.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="sale.isDiscountOnTransaction" :transactionLevelDiscountProp="sale.transactionLevelDiscount" :isFixed="sale.isFixed" :isbeforetax="sale.isBeforeTax" v-bind:key="randerForIsFixed" :noteVal="sale.note" :formName="formName" :isEditPaidInvoice="sale.isEditPaidInvoice" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">

                    <div class="button-items" v-if="sale.isCredit || sale.documentType!='SaleInvoice' ">
                        <button class="btn btn-outline-primary  mr-2" v-if="isValid('CanHoldInvoices') && sale.isEditPaidInvoice == false && formName!='PurchaseOrder'  && formName!='ServiceProformaInvoice'" v-on:click="saveSale('Hold', false)" v-bind:disabled="v$.sale.$invalid">
                            <i class="far fa-save"></i>
                            <span v-if="sale.id == '00000000-0000-0000-0000-000000000000' ">
                                {{ $t('AddSale.SaveasDraft') }}
                            </span>
                            <span v-if="sale.id != '00000000-0000-0000-0000-000000000000'">
                                {{ $t('AddSale.HoldAndUpdate') }}
                            </span>
                        </button>
                        <button v-on:click="saveSale('Credit', false)" v-if="isValid('CashInvoices') && sale.isEditPaidInvoice == true" type="button" class="btn btn-outline-primary me-2">
                            Update
                        </button>
                        <div class="btn-group" v-if="sale.isEditPaidInvoice == false">
                            <button v-on:click="saveSale('Credit', false)" v-if="isValid('CashInvoices') && sale.isEditPaidInvoice == true" type="button" class="btn btn-outline-primary me-0">
                                {{ $t('AddSale.SaveAndPost') }}
                            </button>
                            <button v-on:click="saveSale('Credit', false)" v-else v-bind:disabled="v$.sale.$invalid || sale.saleItems.filter(x => x.outOfStock).length > 0 " type="button" class="btn btn-outline-primary me-0">
                                {{ $t('AddSale.SaveAndPost') }}
                            </button>
                            <button v-bind:disabled="v$.sale.$invalid  || sale.saleItems.filter(x => x.outOfStock).length > 0 " type="button" class="btn btn-outline-primary dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="sr-only">Dropdown</span> <i class="mdi mdi-chevron-down"></i>
                            </button>
                            <div class="dropdown-menu" style="box-shadow: none !important;background-color: transparent !important;border-color: transparent !important;">
                                <a v-on:click="saveSale('Credit', true)" v-if="isValid('CreditInvoices') && isValid('CanPrintInvoices')" class="btn btn-outline-primary" style="width: 162px !important;" href="javascript:void(0);">
                                    {{ $t('AddSale.SaveasPostandPrint') }}
                                </a>
                            </div>
                        </div>

                        <button class="btn btn-danger mr-2" v-on:click="goToSale">
                            {{ $t('AddSale.Cancel') }}
                        </button>
                    </div>
                    <div class="button-items" v-else>
                        <button class="btn btn-outline-primary" v-if="isValid('CanHoldInvoices') && sale.isEditPaidInvoice == true" v-on:click="saveSale('Paid', false)" v-bind:disabled="v$.sale.$invalid">
                            <i class="far fa-save"></i>
                            Update
                        </button>
                        <button class="btn btn-outline-primary" v-if="isValid('CanHoldInvoices') && sale.isEditPaidInvoice == false" v-on:click="saveSale('Hold', false)" v-bind:disabled="v$.sale.$invalid">
                            <i class="far fa-save"></i>
                            <span v-if="sale.id == '00000000-0000-0000-0000-000000000000'">
                                {{ $t('AddSale.SaveasDraft') }}
                            </span>
                            <span v-if="sale.id != '00000000-0000-0000-0000-000000000000'">
                                {{ $t('AddSale.HoldAndUpdate') }}
                            </span>
                        </button>

                        <button class="btn btn-outline-primary" data-bs-toggle="offcanvas" data-bs-target="#offcanvasPayment" aria-controls="offcanvasTop" v-on:click="OnEditInvoice()" v-if="isValid('CashInvoices') && sale.isEditPaidInvoice == false" v-bind:disabled="v$.sale.$invalid || sale.saleItems.filter(x => x.outOfStock).length > 0 ">
                            {{ $t('AddSale.Pay') }}
                        </button>
                        <button class="btn btn-danger mr-2" v-on:click="goToSale">
                            {{ $t('AddSale.Cancel') }}
                        </button>
                    </div>
                </div>

                <div class="accordion mt-3 mb-5" id="accordionExample1">
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingOne1">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne1" aria-expanded="false" aria-controls="collapseOne1">
                                {{ $t('AddSaleOrder.TermandCondition') }}
                            </button>
                        </h5>
                        <div id="collapseOne1" class="accordion-collapse collapse " aria-labelledby="headingOne1" data-bs-parent="#accordionExample1">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <p class="text-white font-weight-bold" style="background: #3178f6; padding: 5px;">
                                            {{ $t('PrintSetting.TermsInEnglish') }}:
                                        </p>
                                        <textarea class="form-control" v-model="sale.termAndCondition" rows="3"></textarea>
                                    </div>

                                    <div class="col-sm-12 mt-3">
                                        <p class="text-white font-weight-bold" style="background: #3178f6; padding: 5px;">
                                            {{ $t('PrintSetting.TermsInArabic')  }} :
                                        </p>
                                        <textarea class="form-control" rows="3" v-model="sale.termAndConditionAr" v-bind:key="randerTerms"></textarea>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingTwo2">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo2" aria-expanded="false" aria-controls="collapseTwo2">
                                {{ $t('AddSale.Attachment') }}
                            </button>
                        </h5>
                        <div id="collapseTwo2" class="accordion-collapse collapse" aria-labelledby="headingTwo2" data-bs-parent="#accordionExample1">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="form-group ps-3">
                                        <div class="font-xs mb-1"> <strong>{{ $t('AddSale.Attachment') }} <span v-if="sale.attachmentList.length>0">({{ sale.attachmentList.length }})</span></strong></div>
                                        <div v-for="(item,index) in sale.attachmentList" v-bind:key="item.path">
                                            {{index+1 }}- &nbsp;&nbsp;{{item.fileName }}

                                        </div>

                                        <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1 mt-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSale.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddSale.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item" v-if="sale.id != '00000000-0000-0000-0000-000000000000' && sale.invoiceType == 'Credit' && (sale.documentType=='SaleOrder' || sale.documentType=='ServiceSaleOrder') ">
                        <h5 class="accordion-header m-0" id="headingTwo3">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo3" aria-expanded="false" aria-controls="collapseTwo3">
                                Payment
                            </button>
                        </h5>
                        <div id="collapseTwo3" class="accordion-collapse collapse" aria-labelledby="headingTwo3" data-bs-parent="#accordionExample1">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary" v-on:click="payment = true"> Add Payment</a>

                                        <purchaseorder-payment :totalAmount="sale.totalAmount"
                                                               :customerAccountId="customerAccountId" :show="payment" v-if="payment"
                                                               @close="paymentSave" :isSaleOrder="'true'" :isPurchase="'false'"
                                                               :purchaseOrderId="sale.id" :formName="'BankReceipt'" />

                                        <div class=" table-responsive">
                                            <table class="table ">
                                                <thead class="thead-light m-0">
                                                    <tr>
                                                        <th>#</th>
                                                        <th style="width:20%;">{{ $t('AddSaleOrder.Date') }} </th>
                                                        <th class="text-right">{{ $t('AddSaleOrder.Amount') }} </th>
                                                        <th class="text-center">{{ $t('AddSaleOrder.PaymentMode') }} </th>
                                                        <th>{{ $t('AddSaleOrder.Description') }} </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(payment, index) in sale.paymentVoucher"
                                                        v-bind:key="index">
                                                        <td>
                                                            {{ index+ 1}}
                                                        </td>
                                                        <th>{{ getDate(payment.date) }}</th>
                                                        <th class="text-right">
                                                            {{ currency }}
                                                            {{
                                                            parseFloat(payment.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                "$1,")
                                                            }}
                                                        </th>
                                                        <th class="text-center">
                                                            <span v-if="payment.paymentMode == 0">Cash</span><span v-if="payment.paymentMode == 1">Bank</span>
                                                        </th>
                                                        <th>{{ payment.narration }}</th>
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
                <div class="offcanvas offcanvas-top" tabindex="-1" id="offcanvasPayment" aria-labelledby="offcanvasRightLabel" style="height:65vh;">
                    <div class="offcanvas-header">
                        <h5 id="offcanvasRightLabel" class="m-0">

                            {{ $t('AddSale.DueAmount') }} ({{
                                sale.salePayment.dueAmount
                                
                            }}) {{ currency }}
                        </h5>
                        <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body">
                        <div class="row">
                            <div class="col-md-6 col-lg-6 cpe cbe">
                                <div class="row form-group">
                                    <div class="col-lg-4 form-group d-grid">
                                        <label>{{ $t('AddSale.PaymentType') }}:</label>
                                        <button v-on:click="isPayment(togglePayCash)" type="button" v-bind:class="sale.salePayment.paymentType == 'Cash' ? 'btn-primary' : 'btn-light'" class="btn  btn-block">
                                            <i class="fas fa-money-bill-alt"></i> {{
                                                    $t('AddSale.Cash')
                                            }}
                                        </button>
                                    </div>
                                    <div class="col-lg-4 form-group d-grid">
                                        <label>&nbsp;</label>
                                        <button v-on:click="isPayment(toggleBank)" type="button" v-bind:class="sale.salePayment.paymentType == 'Bank' ? 'btn-primary' : 'btn-light'" class="btn  btn-block">
                                            <i class="fas fa-money-bill-alt"></i> {{
                                                    $t('AddSale.Bank')
                                            }}
                                        </button>
                                    </div>
                                    <div class="col-lg-4 form-group d-grid">
                                        <label>&nbsp;</label>
                                        <button v-if="sale.customerId != null" v-on:click="isPayment(toggleAdvance)" type="button" v-bind:class="sale.salePayment.paymentType == 'Advance' ? 'btn-primary' : 'btn-light'" class="btn  btn-block">
                                            <i class="fas fa-money-bill-alt"></i> {{
                                                    $t('AddSale.Advance')
                                            }}
                                        </button>
                                        <button v-if="sale.customerId == null" v-on:click="isPayment(toggleOtherCurrency)" type="button" v-bind:class="sale.salePayment.paymentType == 'OtherCurrency' ? 'btn-primary' : 'btn-light'" class="btn  btn-block">
                                            <i class="fas fa-money-bill-alt"></i> {{
                                                    $t('AddSale.OtherCurrency')
                                            }}
                                        </button>
                                    </div>
                                    <div class="col-lg-4 form-group d-grid" v-if="sale.customerId != null &&  sale.paymentTerms=='Credit'">
                                        <label>&nbsp;</label>
                                        <button v-if="sale.customerId != null &&  sale.paymentTerms=='Credit'" v-on:click="isPayment(toggleCreditCustomer)" type="button" v-bind:class="sale.salePayment.paymentType == 'Credit' ? 'btn-primary' : 'btn-light'" class="btn  btn-block">
                                            <i class="fas fa-money-bill-alt"></i> Credit Payment
                                        </button>

                                    </div>
                                </div>

                                <div class="row " v-if="sale.salePayment.paymentType == 'Bank' && IsPaymentOptions">
                                    <div class="col-lg-4 form-group d-grid" v-for="bank in bankList" v-bind:key="bank.id">
                                        <button v-on:click="selectBank(bank.id,bank.name)" type="button" class="btn" v-bind:class=" bank.id==bankId ? 'btn-primary' : 'btn-light'">
                                            <div class="text-truncate" style="font-size:12px;">{{ bank.name }} </div>
                                            <img src="Product.png" v-if="bank.image==null || bank.image=='' || bank.image==undefined" style="width: 45px; height: 35px;">
                                            <img :src="'data:image/png;base64,' + bank.image" v-else style="width: 55px; height: 35px;">
                                            <div class="text-truncate" style="font-size:12px;">{{ bank.nameArabic }} </div>
                                        </button>
                                    </div>
                                </div>

                                <div class="row " v-if="sale.salePayment.paymentType == 'OtherCurrency'">
                                    <div class="col-lg-4 form-group d-grid" v-for="currency in currencyList" v-bind:key="currency.id">
                                        <button v-on:click="selectCurrency(currency.id, currency.name)" type="button" v-bind:class="currency.id == sale.otherCurrency.currencyId ? 'btn-primary' : 'btn-light'" class="btn  btn-block">
                                            {{ currency.sign }} <br />
                                            <img src="Product.png" v-if="currency.image == null || currency.image == '' || currency.image == undefined" style="width: 90px; height: 40px;">
                                            <img :src="'data:image/png;base64,' + currency.image" v-else style="width: 35px; height: 20px;">
                                        </button>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-6 col-lg-6 cps">
                                <div class="row form-group" v-if="sale.customerId != null && sale.salePayment.paymentType != 'Credit'">
                                    <div class="col-lg-12">
                                        <label v-if="sale.salePayment.paymentType == 'Cash'">
                                            {{
                                    $t('AddSale.CashAccount')
                                            }}:
                                        </label>
                                        <label v-if="sale.salePayment.paymentType == 'Bank'">
                                            {{
                                             $t('AddSale.BankAccount')
                                            }}:
                                        </label>
                                        <label v-if="sale.salePayment.paymentType == 'Advance'">
                                            {{
                                                $t('AddSale.AdvanceAccount')
                                            }}:
                                        </label>
                                        <accountdropdown v-model="sale.bankCashAccountId" :disabled="sale.salePayment.paymentType == 'Advance'" :formName="sale.salePayment.paymentType == 'Cash' ? 'CashPay' : sale.salePayment.paymentType == 'Bank' ? 'BankPay' : 'AdvanceReceipt'" :advance="'true'" :key="accountRender"></accountdropdown>
                                    </div>
                                </div>
                                <div class="row form-group" v-if="sale.salePayment.paymentType == 'OtherCurrency'">
                                    <div class="col-lg-4">
                                        <label>{{ $t('AddSale.Amount') }}:</label>
                                        <decimal-to-fixed @update:modelValue="OtherCurrencyCalculate()" v-model="sale.otherCurrency.amount" />
                                    </div>

                                    <div class="col-lg-4">
                                        <label> {{ $t('AddSale.Rate') }}:</label>
                                        <decimal-to-fixed @update:modelValue="OtherCurrencyCalculate()" v-model="sale.otherCurrency.rate" />
                                    </div>

                                    <div class="col-lg-4">
                                        <label>{{ $t('AddSale.Total') }}:</label>
                                        <input type="number" class="form-control" @focus="$event.target.select()" disabled :value="(sale.otherCurrency.rate * sale.otherCurrency.amount)" />
                                    </div>
                                </div>

                                <div class="row form-group">
                                    <div class="col-lg-12">
                                        <label>{{ $t('AddSale.Received') }}:</label>
                                        <div class="input-group">
                                            <input type="text" v-model="sale.salePayment.received" @focus="$event.target.select()" class="form-control" @keyup="calculatBalance($event.target.value)">
                                            <button v-if="IsPaymentOptions" @click="addPayment(sale.salePayment.received, sale.salePayment.paymentType, IsPaymentOptions==true? sale.salePayment.paymentName:sale.salePayment.paymentType )" class="btn btn-primary" type="button" v-bind:disabled="!(sale.salePayment.paymentType == 'Bank' && sale.salePayment.received > 0 && sale.salePayment.paymentOptionId!=null && sale.salePayment.paymentOptionId!='00000000-0000-0000-0000-000000000000' ) && !(sale.salePayment.paymentType == 'Cash' && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Advance' && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Credit' && sale.salePayment.received > 0)">Add</button>
                                            <button v-else @click="addPayment(sale.salePayment.received, sale.salePayment.paymentType, IsPaymentOptions==true? sale.salePayment.paymentName:sale.salePayment.paymentType )" class="btn btn-primary" type="button" v-bind:disabled="!(sale.salePayment.paymentType == 'Bank' && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Cash' && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Advance' && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Credit' && sale.salePayment.received > 0)">Add</button>
                                        </div>
                                    </div>
                                </div>

                                <div class="row form-group" v-if="sale.saleOrderId != '' && sale.saleOrderId != null && autoPaymentVoucher">
                                    <div class="col-lg-12">
                                        <label>{{ $t('AddSale.AdvanceBalance') }} :</label>
                                        <input type="number" class="form-control" disabled @focus="$event.target.select()" :value="soPaymentTotal " />
                                    </div>
                                </div>

                                <div class="row form-group">
                                    <div class="col-lg-6">
                                        <label>{{ $t('AddSale.Balance') }} :</label>
                                        <input type="number" class="form-control" disabled @focus="$event.target.select()" :value="sale.salePayment.balance" />
                                    </div>
                                    <div class="col-lg-6">
                                        <label>{{ $t('AddSale.Change') }} :</label>
                                        <input type="number" class="form-control" disabled @focus="$event.target.select()" :value="sale.salePayment.change" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <h6>Payment Detail </h6>
                                        <table class="table mb-0">
                                            <thead class="">
                                                <tr v-for="(payment, index) in sale.salePayment.paymentTypes" :key="index+8">
                                                    <th style="width:80%;" v-if="payment.paymentType=='Bank'">
                                                        {{payment.paymentType}} <span v-if="payment.paymentType=='OtherCurrency'">({{ $t('TouchScreen.Amount') }}={{payment.otherAmount}}, {{ $t('TouchScreen.Rate') }}={{payment.rate}})</span> : {{payment.amount }} &nbsp; &nbsp; &nbsp;{{payment.name}}
                                                    </th>
                                                    <th style="width:80%;" v-else>
                                                        {{payment.paymentType}} <span v-if="payment.paymentType=='OtherCurrency'">({{ $t('TouchScreen.Amount') }}={{payment.otherAmount}}, {{ $t('TouchScreen.Rate') }}={{payment.rate}})</span> : {{payment.amount }}
                                                    </th>
                                                    <th class="text-center" style="width:20%;">
                                                        <a href="javascript:void(0);" @click="removeFromPaymentList(index)"><i class="las la-trash-alt text-secondary font-16"></i></a>
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="offcanvas-footer">
                        <hr class="hr-dashed hr-menu mt-0" />
                        <div class="row form-group">
                            <div class="col-lg-12">
                                <div class="button-items">
                                    <button class="btn btn-primary" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="saveSale('Paid', false)" v-bind:disabled="!isPaymentValid">
                                        {{ $t('AddSale.Submit') }}
                                    </button>

                                    <button class="btn btn-primary" data-bs-dismiss="offcanvas" aria-label="Close" v-if="isValid('CanPrintInvoices')" v-on:click="saveSale('Paid', true)" v-bind:disabled="!isPaymentValid">
                                        {{ $t('AddSale.SubmitAsPrint') }}
                                    </button>
                                    <button class="btn btn-danger" data-bs-dismiss="offcanvas" aria-label="Close">
                                        {{ $t('AddSale.Cancel') }}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="modal fade" id="exampleModalModify" tabindex="-1" role="dialog" aria-labelledby="exampleModalDefaultLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5>Sale Document Type</h5>
                                    <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio10" value="SaleInvoice" v-model="sale.documentType" class="full_size">
                                                    <label for="radio10">
                                                        Sale Invoice
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="alert custom-alert custom-alert-success icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio11" value="GlobalInvoice" v-model="sale.documentType" class="full_size">
                                                    <label for="radio11">
                                                        Global Invoice
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="alert custom-alert custom-alert-info icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio12" value="ReceiptInvoice" v-model="sale.documentType" class="full_size">
                                                    <label for="radio12">
                                                        Receipt Invoice
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="custom_code alert custom-alert custom-alert-light icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio13" value="SaleOrder" v-model="sale.documentType" class="full_size">
                                                    <label for="radio13">
                                                        Sale Order
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio14" value="SaleQuotation" v-model="sale.documentType" class="full_size">
                                                    <label for="radio14">
                                                        Sale Quotation
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="alert custom-alert custom-alert-warning icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio15" value="ProformaInvoice" v-model="sale.documentType" class="full_size">
                                                    <label for="radio15">
                                                        Proforma Invoice
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="custom_code1 alert custom-alert custom-alert-warning icon-custom-alert shadow-sm fade show d-flex justify-content-between ps-5">
                                        <div class="media">
                                            <div class="media-body align-self-center">
                                                <div class="radio radio-primary mt-2">
                                                    <input type="radio" name="radio10" id="radio16" value="CreditNote" v-model="sale.documentType" class="full_size">
                                                    <label for="radio16">
                                                        Credit Note
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary btn-sm px-5" data-bs-dismiss="modal">Ok</button>
                        </div>
                        <!--end modal-footer-->
                    </div>
                </div>
            </div>
            <!--end modal-->
        </div>
        <div class="row d-flex justify-content-center align-items-center" style="height: 70vh;" v-else>
            <div class="col-lg-6 col-sm-6 ml-auto mr-auto">
                <div class="card p-3 text-center " v-if="bankDetail">
                    <h4 class="">{{ $t('FirstStartInvoice') }}</h4>
                    <router-link :to="{ path: '/WholeSaleDay', query: { token_name: 'DayStart_token', fromDashboard: 'true' } }">
                        <a href="javascript:void(0)" class="btn btn-outline-danger ">
                            {{
 $t('Dashboard.DayStart')
                            }}
                        </a>
                    </router-link>
                </div>
                <div class="card p-5 text-center" v-else>
                    <h4 class="">{{ $t('FirstStartInvoice') }}</h4>
                    <router-link :to="{ path: '/dayStart', query: { token_name: 'DayStart_token', fromDashboard: 'true' } }">
                        <a href="javascript:void(0)" class="btn btn-outline-danger ">
                            {{
 $t('Dashboard.DayStart')
                            }}
                        </a>
                    </router-link>
                </div>
            </div>
        </div>

        <AddAddress :newaddress="newAddress" :isSale="true" :show="show1" v-if="show1" @close="show1=false" @IsSave="IsSave" :type="type" />

        <bulk-attachment :attachmentList="sale.attachmentList" :shows="show" v-if="show" @close="attachmentSave" />

       
        <invoicedetailsprint :show="showReport" v-if="showReport" :reportsrc="reportsrc" :changereport="changereport" @close="IsSavePrint" @IsSave="IsSavePrint" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import moment from "moment";
    import {  required,maxLength } from '@vuelidate/validators'; 
    import useVuelidate from '@vuelidate/core';
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from "vue-multiselect";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

   
    export default {
        props: ['formName'],
        name: "AddSale",
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
                payment: false,
                customerAccountId: '',

                isSalePrice: false,
                isSalePriceLabel: false,
                isCustomerPrice: false,
                isCustomerPriceLabel: false,
                priceLabelingId: '',
                priceLabelRender: 0,

                priceType: null,
                randerPoDate: 0,
                priceoptions: [{
                    id: 1,
                    name: 'Retail Price'
                }, {
                    id: 2,
                    name: 'Wholesale Price'
                }],
                priceLabeling: [],

                wholesalePriceActivation: false,
                bankId: null,
                employeeDescription: '',
                logisticDescription: '',
                areaDescription: '',
                toogletab: false,
                isService: false,
                isUpdateRecord1: false,

                record1: {
                    dueDate: '',
                    description: '',
                    referredBy: '',
                    employeeId: '',
                    validityDate: '',
                    deliveryDate: '',
                    logisticId: '',
                    areaId: '',
                    refrenceNo: '',
                    clientPurchaseNo: '',
                    customerInquiry: '',
                    doctorName: '',
                    hospitalName: '',
                    poNumber: '',
                    poDate: '',

                },
                record2: {
                    saleOrderNo: '',
                    purchaseOrderNo: '',
                    inquiryNo: '',
                    deliveryNoteAndDate: '',
                    quotationNo: '',
                    quotationValidUpto: '',

                    peroformaInvoiceNo: '',
                    perfomaValidUpto: '',
                    dispatchDate: '',
                    pickupDate: '',
                    dispatchNote: '',
                },
                isUpdateRecord2: false,
                record3: {
                    saleOrderId: "",
                    deliveryChallanId: "",
                    quotationId: "",
                    quotationTemplateId: "",
                    proformaId: "",
                    purchaseOrderId: "",
                    selectedValue: "",
                    document: "",
                    detail: "",
                },
                isUpdateRecord3: false,

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
                prefix: {
                    sInvoice: '',
                    sReturn: '',
                    sOrder: '',
                    sQutation: '',
                    pInvoice: '',
                    pReturn: '',
                    pOrder: '',
                    employee: '',
                    sOrdeTracking: '',
                    receiptInvoice: '',
                    globalInvoice: '',
                    debitNote: '',
                    creditNote: '',
                    proformaSaleInvoice: '',
                },
                show1: false,
                multipleAddress: false,
                IsPaymentOptions: false,
                expandPerfomaInvoice: false,
                expandPurchaseOrder: false,
                expandDeliveryChallan: false,
                expandDeliveryAddress: false,
                deliveryAddressList: [],
                isVATInput: false,
                expandSale: false,
                isWalkIn: false,
                expandQuotation: false,
                expandInqury: false,
                saleListWithCanvas: [],
                saleOrderListWithCanvas: [],
                quotationListWithCanvas: [],
                deliverychallanListWithCanvas: [],
                discountTypeOption: 'At Line Item Level',
                onPayScreen: false,
                custemerOldId: '',
                saleOrderDescription: '',
                wareHouseDescription: '',
                deliveryDescription: '',
                show: false,
                imageSrc: '',
                adjustmentSignProp: '+',
                randerAll: 0,
                randerForIsFixed: 0,
                renderImg: 0,
                randerTerms: 0,
                dueDateRef: 0,
                dueDateRef1: 0,
                randerEffect: 0,
                controlRequest: 0,
                printSize: '',
                printTemplate: '',
                saleOrderPerm: false,
                bankDetail: false,
                autoPaymentVoucher: false,
                soPaymentTotal: 0,
                customerRender: 0,
                logisticRender: 0,
                employeeRender: 0,
                saleOrder: false,
                isRaw: '',
                isMultiUnit: '',
                isForMedical: '',
                isArea: '',
                removeCheck: false,
                options: [],
                value: "",
                bankList: [],
                currencyList: [],
                autoNumber: '',
                autoNumberCredit: '',
                toggleCash: false,
                toggleCredit: true,
                currency: '',
                togglePayCash: 'Cash',
                language: 'Nothing',
                paymentMethod: '',

                toggleBank: 'Bank',
                toggleCreditCustomer: 'Credit',
                toggleAdvance: 'Advance',
                addSale: 'addSale',
                toggleOtherCurrency: 'OtherCurrency',

                rendered: 0,
                dateRander: 0,

                sale: {
                    id: "00000000-0000-0000-0000-000000000000",
                    dispatchNote: [],
                    attachmentList: [],
                    paymentVoucher: [],
                    documentType: "SaleInvoice",
                    allowPreviousFinancialPeriod: false,
                    isEditPaidInvoice: false,
                    deliveryId: "",
                    referredBy: "",
                    branchId: "",
                    shippingId: "",
                    billingId: "",
                    nationalId: "",
                    proformaId: "",
                    purchaseOrderId: "",
                    barCode: "",
                    poDate: "",
                    saleOrderNo: "",
                    paymentTerms: "",
                    deliveryNoteAndDate: "",
                    quotationNo: "",
                    quotationValidUpto: "",
                    peroformaInvoiceNo: "",
                    perfomaValidUpto: "",
                    dispatchDate: "",
                    pickupDate: "",
                    inquiryNo: "",
                    purchaseOrderNo: "",
                    customerCode: "",
                    prefix: "",
                    englishName: "",
                    arabicName: "",
                    companyNameEnglish: "",
                    companyNameArabic: "",
                    commercialRegistrationNo: "",
                    customerIdForUpdate: "",
                    shippingAddress: "",
                    isCashCustomer: false,
                    address: "",
                    contactNo1: "",
                    vatNo: "",
                    poNumber: "",
                    refrenceNo: "",
                    clientPurchaseNo: "",
                    validityDate: "",
                    purpose: "",
                    saleOrderId: "",
                    deliveryChallanId: "",
                    quotationId: "",
                    quotationTemplateId: "",
                    date: "",
                    doctorName: "",
                    hospitalName: "",
                    counterId: '',
                    logisticId: '',
                    dayStart: false,
                    isRemove: false,
                    isPurchaseOrder: false,
                    registrationNo: "",
                    customerId: null,
                    dueDate: "",
                    deliveryDate: "",
                    customerInquiry: "",
                    deliveryAddress: "",
                    taxMethod: "",
                    taxRateId: "",
                    wareHouseId: "",
                    creditAmount: 0,
                    saleItems: [],
                    isCredit: false,
                    isService: false,
                    isSerial: false,
                    cashCustomer: "",
                    mobile: "",
                    email: "",
                    cashCustomerId: "",
                    code: "",
                    invoiceType: "",
                    employeeId: "",
                    areaId: "",
                    customerAddressWalkIn: "",
                    change: 0,
                    grossAmount: 0,
                    vatAmount: 0,
                    discountAmount: 0,
                    totalAfterDiscount: 0,
                    totalAmount: 0,
                    amount: 0,
                    soInventoryReserve: '',
                    autoPaymentVoucher: '',
                    description: '',
                    termAndCondition: '',
                    termAndConditionAr: '',
                    warrantyLogoPath: '',
                    terminalId: '',
                    saleHoldTypes: "",
                    invoicePrefix: '',
                    salePayment: {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentOptionId: null,
                        paymentName: 'Cash',
                    },
                    otherCurrency: {
                        currencyId: null,
                        rate: 0,
                        amount: 0
                    },
                    bankCashAccountId: '',
                    userName: '',
                    isCashInvoicesForCustomer: false,
                    partiallyQuotation: false,
                    partiallySaleOrder: false,
                    vatSelectOnSale: false,
                    isButtonDisabled: false,
                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0
                },
                printId: '00000000-0000-0000-0000-000000000000',
                printDetailsPos: [],
                printDetailsCashVoucher: [],
                printDetails: [],
                printRender: 0,
                loading: false,
                summary: Object,
                companyId: '00000000-0000-0000-0000-000000000000',
                CompanyID: '',
                search: '',
                UserID: '',
                employeeId: '',
                customerAdvanceAccountId: '',
                isDayAlreadyStart: false,
                IsProduction: false,
                headerFooter: {
                    returnDays: '',
                    footerEn: '',
                    footerAr: '',
                    printSizeA4: '',
                    company: ''
                },
                accountRender: 0,
                overWrite: false,
                BusinessLogo: '',
                BusinessNameArabic: '',
                BusinessNameEnglish: '',
                BusinessTypeArabic: '',
                BusinessTypeEnglish: '',
                CompanyNameArabic: '',
                CompanyNameEnglish: '',
                saleDefaultVat: '',

                cashCustomerOptions: [],
                selectedCashCustomer: '',
                reportsrc: '',
                changereport: 0,
                showReport: 0,
            };
        },
        validations() {
            return {
                sale: {
                    date: {
                        required
                    },
                    registrationNo: {
                        required
                    },
                    customerId: {
                        required
                    },

                    dueDate: {},
                    wareHouseId: {},
                    saleItems: {
                        required
                    },
                    mobile: {},
                    code: {},
                    description: {
                        maxLength: maxLength(110)
                    }
                }
            }
        },
        computed: {
            isButtonDisabledOfRecord() {
                const properties = Object.values(this.record1);
                return properties.some(property => property != '');
            },
            isButtonDisabledOfRecord2() {
                const properties = Object.values(this.record2);
                return properties.some(property => property != '');
            },
            isButtonDisabledOfRecord3() {
                const properties = Object.values(this.record3);
                return properties.some(property => property != '');
            },
            isPaymentValid: function () {

                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                if (this.sale.salePayment.paymentType == "Bank" && this.sale.customerId != null && this.sale.customerId != '') {

                    if (this.sale.bankCashAccountId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.bankCashAccountId == null) {
                        return false;
                    }
                }

                if (this.sale.salePayment.paymentType == "Bank" && (this.sale.customerId == null || this.sale.customerId == '')) {

                    if (this.sale.salePayment.paymentOptionId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.salePayment.paymentOptionId == null) {
                        return false;
                    }
                }

                if (this.sale.salePayment.paymentType == "OtherCurrency") {
                    if (this.sale.otherCurrency.currencyId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.otherCurrency.currencyId == null) {
                        return false;
                    }
                }

                if ((parseFloat(this.sale.salePayment.received) <= parseFloat(this.sale.salePayment.dueAmount))) {
                    if (((parseFloat(this.sale.salePayment.received) + paymentTypesAmount)) >= this.sale.salePayment.dueAmount) {
                        return true
                    }
                } else {
                    if (((parseFloat(this.sale.salePayment.received) + paymentTypesAmount)) >= this.sale.salePayment.dueAmount) {
                        return true
                    }
                }
                return false;
            }

        },
        watch: {
            formName: function () {

                this.ResetAll();

                this.isService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;

                this.sale.isService = this.isService;

                this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
                this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;
                this.IsPaymentOptions = localStorage.getItem('PaymentOptions') == 'true' ? true : false;

                this.saleOrderPerm = localStorage.getItem('saleOrderPerm') == 'true' ? true : false;
                this.autoPaymentVoucher = localStorage.getItem('AutoPaymentVoucher') == 'true' ? true : false;
                this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;
                this.CompanyID = localStorage.getItem('CompanyID');
                this.language = this.$i18n.locale;
                this.sale.counterId = localStorage.getItem('CounterId');
                this.headerFooter.returnDays = localStorage.getItem('ReturnDays');
                this.isForMedical = localStorage.getItem('isForMedical');
                this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
                this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
                this.headerFooter.printSizeA4 = localStorage.getItem('PrintSizeA4');
                this.printTemplate = localStorage.getItem('PrintTemplate');
                this.printSize = localStorage.getItem('PrintSizeA4');
                if (!this.sale.isCredit) {
                    this.customerRender++;
                }

            },
        },
        methods: {
            ChangeVat: function (value, prop) {
                this.$refs.childComponentRef.changeVatInformation(value, prop);
            },

            GetPaymentVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/SaleOrderPaymentList?id=' + this.sale.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.sale.paymentVoucher = response.data;
                        }
                    });
            },

            paymentSave: function () {
                this.payment = false;
                this.GetPaymentVoucher();
            },

            GetPrice: function (priceLabeling, customerId) {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var cust = customerId == null ? "00000000-0000-0000-0000-000000000000" : customerId;
                var id = priceLabeling == null ? "00000000-0000-0000-0000-000000000000" : priceLabeling.id == undefined ? priceLabeling : priceLabeling.id;
                root.$https.get('/Product/GetPriceRecordChange?priceLebelId=' + id + '&customerId=' + cust, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data) {

                            root.$store.dispatch('GetPriceRecordList', response.data);
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                           root.loading = false;
                            console.log(error);
                        });
            },
            RemoveRecord1: function () {
                this.record1 = {
                    dueDate: '',
                    description: '',
                    referredBy: '',
                    employeeId: '',
                    validityDate: '',
                    deliveryDate: '',
                    logisticId: '',
                    areaId: '',
                    refrenceNo: '',
                    clientPurchaseNo: '',
                    customerInquiry: '',
                    doctorName: '',
                    hospitalName: '',
                    poNumber: '',
                    poDate: '',

                };
                this.sale.dueDate = this.record1.dueDate;
                this.sale.description = this.record1.description;
                this.sale.referredBy = this.record1.referredBy;
                this.sale.employeeId = this.record1.employeeId;
                this.sale.validityDate = this.record1.validityDate;
                this.sale.deliveryDate = this.record1.deliveryDate;
                this.sale.logisticId = this.record1.logisticId;
                this.sale.areaId = this.record1.areaId;
                this.sale.refrenceNo = this.record1.refrenceNo;
                this.sale.clientPurchaseNo = this.record1.clientPurchaseNo;
                this.sale.customerInquiry = this.record1.customerInquiry;
                this.sale.doctorName = this.record1.doctorName;
                this.sale.hospitalName = this.record1.hospitalName;
                this.sale.poNumber = this.record1.poNumber;
                this.sale.poDate = this.record1.poDate;

                const properties = Object.values(this.record1);
                if (properties.some(property => property != '' && property != null)) {
                    this.isUpdateRecord1 = true;

                } else {
                    this.isUpdateRecord1 = false;

                }

            },

            SaveRecord1: function () {

                this.sale.dueDate = this.record1.dueDate;
                this.sale.description = this.record1.description;
                this.sale.referredBy = this.record1.referredBy;
                this.sale.employeeId = this.record1.employeeId;
                this.sale.validityDate = this.record1.validityDate;
                this.sale.deliveryDate = this.record1.deliveryDate;
                this.sale.logisticId = this.record1.logisticId;
                this.sale.areaId = this.record1.areaId;
                this.sale.refrenceNo = this.record1.refrenceNo;
                this.sale.clientPurchaseNo = this.record1.clientPurchaseNo;
                this.sale.customerInquiry = this.record1.customerInquiry;
                this.sale.doctorName = this.record1.doctorName;
                this.sale.hospitalName = this.record1.hospitalName;
                this.sale.poNumber = this.record1.poNumber;
                this.sale.poDate = this.record1.poDate;
                this.sale.dispatchDate = this.record1.dispatchDate;
                this.sale.pickupDate = this.record1.pickupDate;

                const properties = Object.values(this.record1);
                if (properties.some(property => property != '' && property != null)) {
                    this.isUpdateRecord1 = true;

                } else {
                    this.isUpdateRecord1 = false;

                }

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
            },
            IsSavePrint: function () {

                this.showReport = false;
                this.ResetAll();
            },
            RemoveRecord2: function () {
                this.record2 = {
                    saleOrderNo: '',
                    purchaseOrderNo: '',
                    inquiryNo: '',
                    deliveryNoteAndDate: '',
                    quotationNo: '',
                    quotationValidUpto: '',

                    peroformaInvoiceNo: '',
                    perfomaValidUpto: '',
                    dispatchDate: '',
                    pickupDate: '',
                    dispatchNote: '',
                };
                this.sale.saleOrderNo = this.record2.saleOrderNo;
                this.sale.purchaseOrderNo = this.record2.purchaseOrderNo;
                this.sale.inquiryNo = this.record2.inquiryNo;
                this.sale.deliveryNoteAndDate = this.record2.deliveryNoteAndDate;
                this.sale.quotationNo = this.record2.quotationNo;
                this.sale.quotationValidUpto = this.record2.quotationValidUpto;
                this.sale.peroformaInvoiceNo = this.record2.peroformaInvoiceNo;
                this.sale.perfomaValidUpto = this.record2.perfomaValidUpto;
                this.sale.dispatchNote = this.record2.dispatchNote;
                this.sale.dispatchDate = this.record2.dispatchDate;
                this.sale.pickupDate = this.record2.pickupDate;

                const properties = Object.values(this.record2);

                if (properties.some(property => property != '' && property != null)) {
                    this.isUpdateRecord2 = true;

                } else {
                    this.isUpdateRecord2 = false;

                }

            },
            RemoveRecord3: function () {
                this.record3.selectedValue ='';
                this.expandSale = false;
                this.expandQuotation = false;
                this.expandPurchaseOrder = false;
                this.expandPerfomaInvoice = false;
                this.expandInqury = false;
                this.expandDeliveryChallan = false;

                const properties = Object.values(this.record3);

                if (properties.some(property => property != '' && property != null)) {
                    this.isUpdateRecord3 = true;

                } else {
                    this.isUpdateRecord3 = false;

                }

            },
            SaveRecord3: function () {

                this.expandSale = false;
                this.expandQuotation = false;
                this.expandPurchaseOrder = false;
                this.expandPerfomaInvoice = false;
                this.expandInqury = false;
                this.expandDeliveryChallan = false;
                this.sale.saleOrderId = this.record3.saleOrderId;
                this.sale.deliveryChallanId = this.record3.deliveryChallanId;
                this.sale.quotationId = this.record3.quotationId;
                this.sale.quotationTemplateId = this.record3.quotationTemplateId;
                this.sale.proformaId = this.record3.proformaId;
                this.sale.purchaseOrderId = this.record3.purchaseOrderId;
                if (this.record3.document == 'ProformaInvoice') {
                    this.GetSaleRecord(this.record3.proformaId, this.record3.detail, '', 'proforma')

                } else if (this.record3.document == 'PurchaseOrder') {
                    this.GetSaleRecord(this.record3.purchaseOrderId, this.record3.detail, '', 'purchaseOrder')

                } else if (this.record3.document == 'SaleOrder') {
                    this.GetSaleOrderDetail(this.record3.saleOrderId, true, this.record3.detail, '');

                } else if (this.record3.document == 'Quotation') {
                    this.GetSaleOrderDetail(this.record3.quotationId, false, this.record3.detail, '');

                } else if (this.record3.document == 'DeliveryChallan') {
                    this.GetDeliveryChallanRecord(this.record3.deliveryChallanId, this.record3.detail, '');

                }

                const properties = Object.values(this.record3);

                if (properties.some(property => property != '' && property != null)) {
                    this.isUpdateRecord3 = true;

                } else {
                    this.isUpdateRecord3 = false;

                }

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
            },

            SaveRecord: function () {

                this.sale.saleOrderNo = this.record2.saleOrderNo;
                this.sale.purchaseOrderNo = this.record2.purchaseOrderNo;
                this.sale.inquiryNo = this.record2.inquiryNo;
                this.sale.deliveryNoteAndDate = this.record2.deliveryNoteAndDate;
                this.sale.quotationNo = this.record2.quotationNo;
                this.sale.quotationValidUpto = this.record2.quotationValidUpto;
                this.sale.peroformaInvoiceNo = this.record2.peroformaInvoiceNo;
                this.sale.perfomaValidUpto = this.record2.perfomaValidUpto;
                this.sale.dispatchDate = this.record2.dispatchDate;
                this.sale.pickupDate = this.record2.pickupDate;

                this.sale.dispatchNote = this.record2.dispatchNote;

                const properties = Object.values(this.record2);

                if (properties.some(property => property != '' && property != null)) {
                    this.isUpdateRecord2 = true;

                } else {
                    this.isUpdateRecord2 = false;

                }

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
            },
            PrintRdlc: function (id) {
                var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if (isBlind) {
                    this.showReport = false;
                } else {
                    this.showReport = true;
                }
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                

                this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + false + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&CompanyId=' + companyId + '&formName=' + this.formName
                this.changereport++;
                this.loading = false;
            },
            ResetAll: function () {

                var root = this;
                var IsDayStart = 'false';
                this.bankId = null;
                this.employeeDescription = '',
                    this.logisticDescription = '',
                    this.areaDescription = '',
                    this.newAddress = {
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
                        isDefault: false,
                        isOffice: false,
                        allHour: false,
                        allDaySelection: false,
                        isNew: false,
                    },
                    this.prefix = {
                        sInvoice: '',
                        sReturn: '',
                        sOrder: '',
                        sQutation: '',
                        pInvoice: '',
                        pReturn: '',
                        pOrder: '',
                        employee: '',
                        sOrdeTracking: '',
                        receiptInvoice: '',
                        globalInvoice: '',
                        debitNote: '',
                        creditNote: '',
                        proformaSaleInvoice: '',
                    },
                    this.show1 = false,
                    this.multipleAddress = false,
                    this.IsPaymentOptions = false,
                    this.expandPerfomaInvoice = false,
                    this.expandPurchaseOrder = false,
                    this.expandDeliveryChallan = false,
                    this.expandDeliveryAddress = false,
                    this.deliveryAddressList = [],
                    this.isVATInput = false,
                    this.expandSale = false,
                    this.isWalkIn = false,
                    this.expandQuotation = false,
                    this.expandInqury = false,
                    this.saleListWithCanvas = [],
                    this.saleOrderListWithCanvas = [],
                    this.quotationListWithCanvas = [],
                    this.deliverychallanListWithCanvas = [],
                    this.discountTypeOption = 'At Line Item Level',
                    this.onPayScreen = false,
                    this.custemerOldId = '',
                    this.saleOrderDescription = '',
                    this.wareHouseDescription = '',
                    this.deliveryDescription = '',
                    this.show = false,

                    this.imageSrc = '',
                    this.adjustmentSignProp = '+',
                    this.randerAll = 0,
                    this.renderImg = 0,
                    this.dueDateRef = 0,
                    this.dueDateRef1 = 0,
                    this.randerEffect = 0,
                    this.printSize = '',
                    this.printTemplate = '',
                    this.saleOrderPerm = false,
                    this.bankDetail = false,
                    this.autoPaymentVoucher = false,
                    this.soPaymentTotal = 0,
                    this.customerRender = 0,
                    this.logisticRender = 0,
                    this.employeeRender = 0,
                    this.saleOrder = false,
                    this.isRaw = '',
                    this.isMultiUnit = '',
                    this.isForMedical = '',
                    this.isArea = '',
                    this.removeCheck = false,
                    this.options = [],
                    this.value = "",
                    this.autoNumber = '',
                    this.autoNumberCredit = '',
                    this.toggleCash = false,
                    this.toggleCredit = false,
                    this.currency = '',
                    this.togglePayCash = 'Cash',
                    this.language = 'Nothing',
                    this.paymentMethod = '',

                    this.toggleBank = 'Bank',
                    this.toggleAdvance = 'Advance',
                    this.addSale = 'addSale',
                    this.toggleOtherCurrency = 'OtherCurrency',

                    this.rendered = 0,
                    this.dateRander = 0,
                    this.record2= {
                    saleOrderNo: '',
                    purchaseOrderNo: '',
                    inquiryNo: '',
                    deliveryNoteAndDate: '',
                    quotationNo: '',
                    quotationValidUpto: '',

                    peroformaInvoiceNo: '',
                    perfomaValidUpto: '',
                    dispatchDate: '',
                    pickupDate: '',
                    dispatchNote: '',
                };
                this.isUpdateRecord2= false;
                this.record3={
                    saleOrderId: "",
                    deliveryChallanId: "",
                    quotationId: "",
                    quotationTemplateId: "",
                    proformaId: "",
                    purchaseOrderId: "",
                    selectedValue: "",
                    document: "",
                    detail: "",
                };
                this.isUpdateRecord3= false;

                    this.sale = {
                        id: "00000000-0000-0000-0000-000000000000",
                        dispatchNote: [],
                        attachmentList: [],
                        documentType: "SaleInvoice",
                        allowPreviousFinancialPeriod: localStorage.getItem('AllowPreviousFinancialPeriod') == 'true' ? true : false,
                        deliveryId: "",
                        isEditPaidInvoice: false,
                        referredBy: "",
                        branchId: "",
                        paymentTerms: "",
                        shippingId: "",
                        billingId: "",
                        nationalId: "",
                        creditAmount: 0,
                        proformaId: "",
                        barCode: "",
                        poDate: "",
                        customerCode: "",
                        prefix: "",
                        englishName: "",
                        arabicName: "",
                        companyNameEnglish: "",
                        companyNameArabic: "",
                        saleOrderNo: "",
                        deliveryNoteAndDate: "",
                        quotationNo: "",
                        quotationValidUpto: "",
                        peroformaInvoiceNo: "",
                        dispatchDate: "",
                        pickupDate: "",
                        perfomaValidUpto: "",
                        inquiryNo: "",
                        saleHoldTypes: "",
                        purchaseOrderNo: "",
                        commercialRegistrationNo: "",
                        customerIdForUpdate: "",
                        shippingAddress: "",
                        isCashCustomer: false,
                        address: "",
                        contactNo1: "",
                        vatNo: "",
                        poNumber: "",
                        refrenceNo: "",
                        clientPurchaseNo: "",
                        validityDate: "",
                        purpose: "",
                        saleOrderId: "",
                        deliveryChallanId: "",
                        quotationId: "",
                        quotationTemplateId: "",
                        date: "",
                        doctorName: "",
                        hospitalName: "",
                        counterId: '',
                        logisticId: '',
                        dayStart: false,
                        isRemove: false,
                        isPurchaseOrder: false,
                        registrationNo: "",
                        customerId: null,
                        dueDate: "",
                        deliveryDate: "",
                        customerInquiry: "",
                        deliveryAddress: "",
                        taxRateId : localStorage.getItem('TaxRateId'),
                        taxMethod : localStorage.getItem('taxMethod'),
                        wareHouseId: "",
                        saleItems: [],
                        isCredit: false,
                        isService: localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true,
                        isSerial: false,
                        cashCustomer: "",
                        mobile: "",
                        email: "",
                        cashCustomerId: "",
                        code: "",
                        invoiceType: "",
                        employeeId: "",
                        areaId: "",
                        customerAddressWalkIn: "",
                        change: 0,
                        discountAmount: 0,
                        totalAfterDiscount: 0,
                        grossAmount: 0,
                        vatAmount: 0,
                        amount: 0,
                        soInventoryReserve: '',
                        autoPaymentVoucher: '',
                        description: '',
                        termAndCondition: '',
                        termAndConditionAr: '',
                        warrantyLogoPath: '',
                        terminalId: '',
                        invoicePrefix: '',
                        salePayment: {
                            dueAmount: 0,
                            received: 0,
                            balance: 0,
                            change: 0,
                            paymentTypes: [],
                            paymentType: 'Cash',
                            paymentOptionId: null,
                            paymentName: 'Cash',
                        },
                        otherCurrency: {
                            currencyId: null,
                            rate: 0,
                            amount: 0
                        },
                        bankCashAccountId: '',
                        userName: '',
                        isCashInvoicesForCustomer: false,
                        partiallyQuotation: false,
                        partiallySaleOrder: false,
                        vatSelectOnSale: false,
                        isButtonDisabled: false,
                        discount: 0,
                        isDiscountOnTransaction: false,
                        isFixed: false,
                        isBeforeTax: true,
                        transactionLevelDiscount: 0
                    },
                    this.printId = '00000000-0000-0000-0000-000000000000',
                    this.printDetailsPos = [],
                    this.printDetailsCashVoucher = [],
                    this.printDetails = [],
                    this.printRender = 0,
                    this.loading = false,
                    this.summary = Object,
                    this.companyId = '00000000-0000-0000-0000-000000000000',
                    this.CompanyID = '',
                    this.search = '',
                    this.UserID = '',
                    this.employeeId = '',
                    this.customerAdvanceAccountId = '',
                    this.isDayAlreadyStart = false,
                    this.IsProduction = false,

                    this.accountRender = 0,
                    this.overWrite = false,

                    this.saleDefaultVat = '',
                    this.reportsrc = '',
                    this.changereport = 0,
                    this.showReport = 0,

                    this.cashCustomerOptions = [];
                this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');

                this.sale.wareHouseId = localStorage.getItem('WareHouseId');
                this.sale.date = moment().format('LLL');
                this.sale.counterId = localStorage.getItem('CounterId');
                this.sale.allowPreviousFinancialPeriod = localStorage.getItem('AllowPreviousFinancialPeriod') == 'true' ? true : false;
                this.sale.dayStart = IsDayStart == 'false' ? false : true;

                this.selectedCashCustomer = '';
                this.$route.query.data == undefined;
                if (root.$refs.childComponentRef != undefined) {
                    this.$refs.childComponentRef.ReRanderProduct();

                }
                if (root.$refs.childComponentRef != undefined) {
                    this.$refs.childComponentRef.EmtySaleProductList();

                }
                if (root.$refs.CustomerDropdown != undefined) {
                    
                    this.$refs.CustomerDropdown.EmptyRecord();
                    if (localStorage.getItem('IsCashCustomer') == 'true') {
                      let id=  this.$refs.CustomerDropdown.CompareWalkIn(true);
                      if(id!=null)
                      {
                        this.sale.customerId=id;


                      }


                    }


                }

                if (root.$refs.employeeIdRef != undefined) {
                    this.$refs.employeeIdRef.EmptyRecord();

                }
                if (root.$refs.AreaDropdown != undefined) {
                    this.$refs.AreaDropdown.EmptyRecord();

                }
                if (root.$refs.logisticDropdown != undefined) {
                    this.$refs.logisticDropdown.EmptyRecord();

                }

                var IsExpenseDay = localStorage.getItem('IsExpenseDay');
                var IsDayStartOn = 'false';

                if (this.formName == 'ServiceSaleOrder' || this.formName == 'ServiceQuotation' || this.formName == 'ServiceProformaInvoice' || this.formName == 'PurchaseOrder' || this.formName == 'ProformaInvoice') {
                    if (this.formName == 'ServiceQuotation') {
                        this.sale.documentType = 'ServiceQuotation';

                    }
                    if (this.formName == 'ServiceSaleOrder') {
                        this.sale.documentType = 'ServiceSaleOrder';

                    }
                    if (this.formName == 'ServiceProformaInvoice') {
                        this.sale.documentType = 'ServiceProformaInvoice';

                    }
                    if (this.formName == 'ProformaInvoice') {
                        this.sale.documentType = 'ProformaInvoice';

                    }
                    if (this.formName == 'PurchaseOrder') {
                        this.sale.documentType = 'PurchaseOrder';

                    }
                    this.sale.isCredit = false

                    this.AutoIncrementCode();

                    this.sale.date = moment().format('llll');
                    this.isDayAlreadyStart = true;

                }
                else {

                    this.sale.documentType = 'SaleInvoice';

                    if (localStorage.getItem('IsCashCustomer') == 'true') {
                        this.sale.isCredit = false
                    } else {
                        if (localStorage.getItem('IsSaleCredit') != 'true') {
                            this.sale.isCredit = false
                        } else {
                            this.sale.isCredit = true
                        }
                    }
                    if (!this.sale.isCredit) {
                        if (this.sale.bankCashAccountId == '') {
                            if (localStorage.getItem('CashAccountId') != null && localStorage.getItem('CashAccountId') != undefined && localStorage.getItem('CashAccountId') != '' && localStorage.getItem('CashAccountId') != 'null' && localStorage.getItem('CashAccountId') != "null" && localStorage.getItem('CashAccountId') != "00000000-0000-0000-0000-000000000000" && localStorage.getItem('CashAccountId') != '00000000-0000-0000-0000-000000000000') {

                                this.sale.bankCashAccountId = localStorage.getItem('CashAccountId');
                            }
                        }

                    }

                    if (this.isValid('CashInvoicesForCustomer') && !this.isValid('CashInvoicesForWalkIn')) {

                        this.sale.isCashInvoicesForCustomer = true;
                    }

                    if (IsDayStart != 'true') {
                        this.isDayAlreadyStart = true;
                        this.language = this.$i18n.locale;

                        this.isArea = localStorage.getItem('IsArea');
                        this.currency = localStorage.getItem('currency');
                        this.isMultiUnit = 'false';
                        this.isRaw = localStorage.getItem('IsProduction');
                        this.sale.date = moment().format('LLL');

                        this.AutoIncrementCode();

                    } else {

                        if (IsExpenseDay == 'true') {
                            this.isDayAlreadyStart = false;
                        } else {
                            this.UserID = localStorage.getItem('UserID');
                            this.employeeId = localStorage.getItem('EmployeeId');
                            if (IsDayStartOn == 'true') {

                                this.isDayAlreadyStart = true;

                                root.isArea = localStorage.getItem('IsArea');
                                root.currency = localStorage.getItem('currency');
                                root.isMultiUnit = 'false';
                                root.isRaw = localStorage.getItem('IsProduction');

                                root.sale.date = moment().format('LLL');
                                root.AutoIncrementCode();

                            }
                            else {
                                this.isDayAlreadyStart = false;
                            }
                        }
                    }
                }

            },
            AddRow1: function (type) {
                debugger; //eslint-disable-line

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
            DeliveryChallanListWithCanvas: function (value) {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.expandDeliveryChallan = value;

                if (this.sale.customerId == null || this.sale.customerId == '') {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: "Please Select Customer ",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                    });

                    return;
                }

                var url = '';

                {
                    url = '/Purchase/DeliveryChallanList?isDropdown=true' + '&branchId=' + localStorage.getItem('BranchId')
                }

                root.deliverychallanListWithCanvas = [];
                this.$https.get(url + '&customerId=' + this.sale.customerId + '&IsService=' + this.isService, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {

                        if (response.data != null) {

                            response.data.deliveryChallanListLookUpModels.forEach(function (sup) {
                                root.deliverychallanListWithCanvas.push({
                                    id: sup.id,
                                    date: sup.date,
                                    registrationNumber: sup.registrationNumber,
                                    name: sup.registrationNumber + '  ' + sup.date,
                                });
                            });
                        }
                    });
            },
            SaleListWithCanvas: function (value, documentType) {

                var root = this;
                var status = '';

                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                if (documentType == 'SaleInvoice' || documentType == 'PurchaseOrder') {
                    if (documentType == 'SaleInvoice') {
                        this.expandPerfomaInvoice = value;
                        this.expandPurchaseOrder = false;

                        status = 'ProformaInvoice'
                    } else if (documentType == 'PurchaseOrder') {
                        this.expandPurchaseOrder = value;
                        this.expandPerfomaInvoice = false;

                        status = 'PurchaseOrder'
                    }

                    if (this.sale.customerId == null || this.sale.customerId == '') {
                        this.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Please Select Customer ",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        return;
                    }

                    var url = '';

                    {
                        url = '/Sale/SaleList?isDropdown=true' + '&branchId=' + localStorage.getItem('BranchId')
                    }

                    root.saleListWithCanvas = [];
                    this.$https.get(url + '&status=' + status + '&customerId=' + this.sale.customerId + '&IsService=' + this.isService, {
                        headers: {
                            Authorization: `Bearer ${token}`
                        },
                    })
                        .then(function (response) {

                            if (response.data != null) {

                                response.data.results.sales.forEach(function (sup) {
                                    root.saleListWithCanvas.push({
                                        id: sup.id,
                                        date: sup.date,
                                        registrationNumber: sup.registrationNumber,
                                        netAmount: sup.netAmount,
                                        name: sup.registrationNumber + '  ' + sup.date + '--' + sup.netAmount,
                                    });
                                });
                            }
                        });

                }

                if (documentType == 'Inquiry') {
                    this.expandInqury = value;

                    if (this.sale.customerId == null || this.sale.customerId == '') {
                        this.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Please Select Customer ",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        return;
                    }

                    root.saleListWithCanvas = [];

                }

            },

            LogisticRecord: function (id, detail) {

                if (detail != undefined) {
                    console.log(id);
                    this.logisticDescription = detail.name;

                }

            },
            AreaRecord: function (id, detail) {

                if (detail != undefined) {
                    console.log(id);
                    this.areaDescription = detail.area;

                }

            },
            EmployeeRecord: function (id, detail) {

                if (detail != undefined) {
                    this.employeeDescription = detail.name;

                }

            },
            GetWareHouseRecord: function (id, detail) {

                if (detail != undefined) {

                    this.wareHouseDescription = detail.name;
                }

            },
            NoteSave: function (note) {

                this.sale.note = note;

            },
            
            GetDeliveryChallanRecord: function (id, detail, notSave) {
                var root = this;
                if(this.record3.selectedValue!=null && this.record3.selectedValue!=undefined
                 && this.record3.selectedValue!='' && notSave == 'NotSave' )
                {
                    this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Un-Select Current Document than select New",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });
                    return;
                }

                if (notSave == 'NotSave') {

                    this.record3.quotationId = '';
                    this.record3.saleOrderId = '';
                    this.record3.deliveryChallanId = id;
                    this.record3.selectedValue = '(' + detail.registrationNumber + ' ' + this.getDate(detail.date) + ')';
                    this.record3.document = 'DeliveryChallan';
                    this.record3.detail = detail;

                    this.record3.proformaId = '';
                    this.record3.quotationTemplateId = '';
                    return;

                }

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {
                    this.sale.deliveryChallanId = id;
                    root.sale.saleOrderId = '';
                    root.sale.deliveryNoteAndDate = '';
                    root.sale.deliveryNoteAndDate = '';
                    root.sale.quotationId = '';
                    root.deliveryDescription = '(' + detail.registrationNumber + ' ' + root.getDate(detail.date) + ')';

                    root.sale.dueDate = '';
                    root.sale.employeeId = '';
                    root.sale.description = '';
                    root.sale.termAndCondition = '';
                    root.sale.termAndConditionAr = '';
                    root.randerTerms++;
                    root.sale.note = '';
                    root.sale.saleOrderId = '';
                    root.sale.quotationId = '';
                    root.sale.doctorName = '';
                    root.sale.hospitalName = '';
                    root.sale.logisticId = '';
                    root.sale.saleOrderNo = '';
                    root.sale.peroformaInvoiceNo = '';
                    root.sale.purchaseOrderNo = '';
                    root.sale.inquiryNo = '';
                    root.sale.quotationNo = '';
                    root.sale.referredBy = '';
                    root.sale.deliveryDate = '';
                    root.sale.deliveryNoteAndDate = '';

                    root.sale.perfomaValidUpto = '';
                    root.sale.dispatchDate = '';
                    root.sale.pickupDate = '';
                    root.sale.quotationValidUpto = '';
                    root.logisticDescription = '';
                    root.sale.customerInquiry = '';
                    root.sale.poNumber = '';

                    root.sale.refrenceNo = '';
                    root.sale.clientPurchaseNo = '';
                    root.sale.validityDate = '';
                    root.sale.purpose = '';

                    root.sale.poDate = '';
                    root.logisticRender++;
                    root.employeeRender++;

                    root.$https.get('/Purchase/DeliveryChallanDetail?Id=' + id + '&isDeliveryChallan=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                if (root.multipleAddress) {
                                    if (root.deliveryAddressList.length > 0) {

                                        for (let i = 0; i < root.deliveryAddressList.length; i++) {
                                            const cat = root.deliveryAddressList[i];

                                            switch (cat.id) {
                                                case response.data.deliveryId:
                                                case response.data.shippingId:
                                                case response.data.nationalId:
                                                case response.data.billingId:
                                                    cat.isOffice = true;
                                                    break;
                                                default:
                                                    cat.isOffice = false;
                                                    break;
                                            }
                                        }

                                    }

                                }

                                root.$refs.childComponentRef.EmtySaleProductList();
                                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                    response.data.deliveryChallanItems.forEach(function (x) {

                                        x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                        x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                        x.unitPerPack = x.product.unitPerPack;
                                    });
                                }
                                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                    response.data.deliveryChallanItems.forEach(function (x) {

                                        x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                        x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                        x.unitPerPack = x.product.unitPerPack;
                                    });
                                }

                                root.sale.deliveryDate = response.data.date;
                                root.record1.deliveryDate = response.data.date;

                                root.sale.deliveryNoteAndDate = response.data.registrationNo + ' ' + response.data.date;
                                root.record2.deliveryNoteAndDate = response.data.registrationNo + ' ' + response.data.date;

                                root.dueDateRef1++;
                                response.data.deliveryChallanItems.forEach(function (so) {
                                    if (root.isService) {
                                        so.taxRateId = root.sale.taxRateId;
                                        so.taxMethod = root.sale.taxMethod;
                                        so.discount = 0;
                                        so.serial = '';
                                        so.fixDiscount = 0;
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantity, so.unitPrice, true, so, false);
                                    } else {
                                        so.taxRateId = root.sale.taxRateId;
                                        so.taxMethod = root.sale.taxMethod;
                                        so.discount = 0;
                                        so.serial = '';
                                        so.fixDiscount = 0;
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantity, so.unitPrice, true, 0, so, null, false, true, root.sale.taxRateId, root.sale.taxMethod);
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

            GetSaleRecord: function (id, detail, notSave, isProforma) {
                if(this.record3.selectedValue!=null && this.record3.selectedValue!=undefined
                 && this.record3.selectedValue!='' && notSave == 'NotSave' )
                {
                    this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Un-Select Current Document than select New",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });
                    return;
                }

                var root = this;
                if (notSave == 'NotSave') {
                    if (isProforma == 'proforma') {

                        this.record3.proformaId = id;
                        this.record3.selectedValue = '(' + detail.registrationNumber + ' ' + this.getDate(detail.date) + '--' + this.currency + ' ' + detail.netAmount + ')';
                        this.record3.document = 'ProformaInvoice';
                        this.record3.detail = detail;

                        this.record3.saleOrderId = '';
                        this.record3.quotationId = '';
                        this.record3.deliveryChallanId = '';
                        this.record3.quotationTemplateId = '';
                        this.record3.purchaseOrderId = '';
                        return;
                    } else if (isProforma == 'purchaseOrder') {

                        this.record3.purchaseOrderId = id;
                        this.record3.selectedValue = '(' + detail.registrationNumber + ' ' + this.getDate(detail.date) + '--' + this.currency + ' ' + detail.netAmount + ')';
                        this.record3.document = 'PurchaseOrder';
                        this.record3.detail = detail;

                        this.record3.proformaId = '';
                        this.record3.saleOrderId = '';
                        this.record3.quotationId = '';
                        this.record3.deliveryChallanId = '';
                        this.record3.quotationTemplateId = '';
                        return;
                    }

                }
                
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {
                    if (isProforma == 'proforma') {
                        this.sale.proformaId = id;
                    } else if (isProforma == 'purchaseOrder') {
                        this.sale.purchaseOrderId = id;

                    }
                    root.sale.peroformaInvoiceNo = '';
                    root.sale.saleOrderId = '';
                    root.sale.quotationId = '';
                    root.sale.deliveryChallanId = '';
                    root.deliveryDescription = '(' + detail.registrationNumber + ' ' + root.getDate(detail.date) + '--' + this.currency + ' ' + detail.netAmount + ')';

                    root.$https.get('/Sale/SaleDetail?id=' + id + '&simpleQuery=' + false, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null && response.data != undefined) {


                                var detail = response.data;
                                root.sale.taxMethod = detail.taxMethod;

                                if (notSave == 'ConvertToInvoice') {
                                    root.sale.customerId = detail.customerId;
                                    root.customerRender++;
                                }

                                root.sale.deliveryNoteAndDate = detail.deliveryNoteAndDate;
                                root.sale.perfomaValidUpto = detail.perfomaValidUpto;
                                root.sale.dispatchDate = detail.dispatchDate;
                                root.sale.pickupDate = detail.pickupDate;

                                root.sale.quotationValidUpto = detail.quotationValidUpto;

                                root.sale.dueDate = detail.dueDate;
                                root.sale.mobile = detail.mobile;
                                root.sale.email = detail.email;
                                root.sale.code = detail.code;
                                root.sale.areaId = detail.areaId;
                                root.sale.employeeId = detail.employeeId;
                                root.sale.description = detail.description;
                                root.sale.termAndCondition = detail.termAndCondition;
                                root.sale.termAndConditionAr = detail.termAndConditionAr;
                                root.randerTerms++;

                                root.sale.note = detail.note;
                                root.sale.attachmentList = detail.attachmentList;
                                root.sale.doctorName = detail.doctorName;
                                root.sale.hospitalName = detail.hospitalName;
                                root.sale.logisticId = detail.logisticId;

                                root.sale.discount = detail.discount;
                                root.sale.isDiscountOnTransaction = detail.isDiscountOnTransaction;
                                root.sale.isFixed = detail.isFixed;
                                root.sale.saleOrderNo = detail.saleOrderNoAndDate;
                                root.sale.peroformaInvoiceNo = detail.peroformaInvoiceNo;
                                root.sale.purchaseOrderNo = detail.registrationNo + ' ' + detail.date;
                                root.sale.inquiryNo = detail.inquiryNoAndDate;
                                root.sale.quotationNo = detail.quotationNoAndDate;
                                root.sale.referredBy = detail.referredBy;
                                root.sale.deliveryDate = detail.deliveryDate;
                                root.sale.isBeforeTax = detail.isBeforeTax;
                                root.sale.transactionLevelDiscount = detail.transactionLevelDiscount;
                                root.employeeDescription = detail.employeeName;
                                root.logisticDescription = detail.logisticNameEn + ' ' + detail.logisticNameAr;
                                root.discountTypeOption = detail.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                                root.sale.taxRateId = detail.taxRateId;
                                root.sale.customerInquiry = detail.customerInquiry;
                                root.sale.poNumber = detail.registrationNo;
                                root.sale.poDate = detail.date;
                                root.adjustmentSignProp = detail.discount >= 0 ? '+' : '-';
                                root.$refs.childComponentRef.EmtySaleProductList(root.sale.taxMethod);
                                //For Record
                                root.record2.saleOrderNo = root.sale.saleOrderNo;
                                root.record2.purchaseOrderNo = detail.registrationNo + ' ' + detail.date;
                                root.record2.inquiryNo = root.sale.inquiryNo;
                                root.record2.deliveryNoteAndDate = root.sale.deliveryNoteAndDate;
                                root.record2.quotationNo = root.sale.quotationNo;
                                root.record2.quotationValidUpto = root.sale.quotationValidUpto;
                                root.record2.peroformaInvoiceNo = root.sale.peroformaInvoiceNo;
                                root.record2.perfomaValidUpto = root.sale.perfomaValidUpto;
                                root.record2.dispatchDate = root.sale.dispatchDate;
                                root.record2.pickupDate = root.sale.pickupDate;

                                root.record2.dispatchNote = root.sale.dispatchNote;
                                //Record 2

                                const properties = Object.values(root.record2);
                                if (properties.some(property => property != '' && property != null)) {
                                    root.isUpdateRecord2 = true;

                                } else {
                                    root.isUpdateRecord2 = false;

                                }

                                root.record1.dueDate = root.sale.dueDate;
                                root.record1.description = root.sale.description;
                                root.record1.referredBy = root.sale.referredBy;
                                root.record1.employeeId = root.sale.employeeId;
                                root.record1.validityDate = root.sale.validityDate;
                                root.record1.deliveryDate = root.sale.deliveryDate;
                                root.record1.logisticId = root.sale.logisticId;
                                root.record1.areaId = root.sale.areaId;
                                root.record1.refrenceNo = root.sale.refrenceNo;
                                root.record1.clientPurchaseNo = root.sale.clientPurchaseNo;
                                root.record1.customerInquiry = root.sale.customerInquiry;
                                root.record1.doctorName = root.sale.doctorName;
                                root.record1.hospitalName = root.sale.hospitalName;
                                root.record1.poNumber = detail.registrationNo;
                                root.record1.poDate = detail.date;
                                root.randerPoDate++;
                                const properties12 = Object.values(root.record1);
                                if (properties12.some(property => property != '' && property != null)) {
                                    root.isUpdateRecord1 = true;
                                }
                                else {
                                    root.isUpdateRecord1 = false;
                                }

                                if (!root.isService) {
                                    if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                        detail.saleItems.forEach(function (x) {

                                            x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                            x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                            x.unitPerPack = x.product.unitPerPack;
                                        });
                                    }
                                    if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                        detail.saleItems.forEach(function (x) {

                                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                            x.unitPerPack = x.product.unitPerPack;
                                        });
                                    }
                                }

                                detail.saleItems.forEach(function (so) {
                                    if (root.isService) {
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantity, so.unitPrice, true, so);

                                    } else {
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantity, so.unitPrice, true, 0, so, null, false);
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

            createUUID: function () {

                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },

            getDate: function (date) {
                return moment(date).format('LL');
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
            SaleOrderListWithCanvas: function (value) {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.expandSale = value;
                this.randerCanvas++;

                if (this.sale.customerId == null || this.sale.customerId == '') {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: "Please Select Customer ",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                    });

                    return;
                }

                var url = '';

                {
                    url = '/Purchase/SaleServiceOrderList?isDropdown=true' + '&branchId=' + localStorage.getItem('BranchId')
                }

                root.saleOrderListWithCanvas = [];
                this.$https.get(url + '&customerId=' + this.sale.customerId + '&isService=' + this.isService, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (sup) {
                                root.saleOrderListWithCanvas.push({
                                    id: sup.id,
                                    date: sup.date,
                                    netAmount: sup.netAmount,
                                    registrationNumber: sup.registrationNumber,
                                    name: sup.registrationNumber + '  ' + sup.netAmount,
                                });
                            });
                        }
                    })
                    .then(function () {
                        root.value = root.saleOrderListWithCanvas.find(function (x) {
                            return x.id == root.values;
                        });
                    });
            },

            QuotationListWithCanvas: function (value) {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.expandQuotation = value;
                this.randerCanvas++;

                if (this.sale.customerId == null || this.sale.customerId == '') {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: "Please Select Customer ",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                    });

                    return;
                }

                var url = '';

                {
                    url = '/Purchase/SaleServiceOrderList?isDropdown=true' + '&branchId=' + localStorage.getItem('BranchId')
                }

                root.quotationListWithCanvas = [];
                this.$https.get(url + '&isQuotation=true' + '&customerId=' + this.sale.customerId + '&isService=' + this.isService, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (sup) {
                                root.quotationListWithCanvas.push({
                                    id: sup.id,
                                    date: sup.date,
                                    netAmount: sup.netAmount,
                                    registrationNumber: sup.registrationNumber,
                                    name: sup.registrationNumber + '  ' + sup.netAmount,
                                });
                            });
                        }
                    })
                    .then(function () {
                        root.value = root.quotationListWithCanvas.find(function (x) {
                            return x.id == root.values;
                        });
                    });
            },

            Attachment: function () {
                this.show = true;
            },
            VatInputValues: function () {

                this.isVATInput = !this.isVATInput;
            },

            attachmentSave: function (attachment) {
                this.sale.attachmentList = attachment;
                this.show = false;
            },

            removeImage: function () {
                this.imageSrc = '';
                this.sale.warrantyLogoPath = '';
                this.renderImg++;
                this.sale.isRemove = true

            },
            uploadImage: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = this.$refs.imgupload.files;

                var fileData = new FormData();

                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }

                this.imageSrc = URL.createObjectURL(this.$refs.imgupload.files[0]);

                root.$https.post('/Company/UploadFilesAsync', fileData, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.sale.warrantyLogoPath = response.data;
                        }
                    },
                        function () {
                           root.loading = false;
                            root.$swal({
                                title: root.$t('Error'),
                                text: root.$t('SomethingWrong'),
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            getAddress: function () {

                if (this.$refs.CustomerDropdown != null) {

                    this.sale.customerAddressWalkIn = this.$refs.CustomerDropdown.GetCustomerAddress().address;
                    this.sale.mobile = this.$refs.CustomerDropdown.GetCustomerAddress().mobile;
                    this.sale.email = this.$refs.CustomerDropdown.GetCustomerAddress().email;

                }

                
            },

            GetQuotationTemplateDetail: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {
                    var isCheck = root.$refs.childComponentRef.CheckRecordInProduct();
                    if (isCheck == false) {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Please Wait Product are loading",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        return;
                    }
                    root.$refs.childComponentRef.ClearList();
                    root.$https.get('/Purchase/QuotationTemplateDetail?Id=' + id, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                response.data.quotationTemplateItems.forEach(function (so) {

                                    root.sale.taxRateId = localStorage.getItem('TaxRateId');
                                    root.sale.taxMethod = localStorage.getItem('taxMethod');
                                    root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantity, so.unitPrice, false, so, true);

                                });

                                root.customerRender++;
                                root.logisticRender++;
                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }
            },
            RemoveEffect: function (effectremove) {
                var root = this;
                if (effectremove == 'description') {

                    root.sale.description = '';
                }
                if (effectremove == 'hospitalName') {

                    root.sale.hospitalName = '';
                }
                if (effectremove == 'billingAddress') {

                    root.sale.billingAddress = '';
                }
                if (effectremove == 'customerInquiry') {

                    root.sale.customerInquiry = '';
                }
                if (effectremove == 'doctorName') {

                    root.sale.doctorName = '';
                }
                if (effectremove == 'deliveryAddress') {

                    root.sale.deliveryAddress = '';
                }
                if (effectremove == 'logisticId') {
                    root.logisticDescription = '';
                    if (root.$refs.logisticDropdown != undefined) {
                        root.$refs.logisticDropdown.EmptyRecord();

                    }
                    root.sale.logisticId = '';
                }
                if (effectremove == 'areaId') {
                    root.areaDescription = '';
                    if (root.$refs.AreaDropdown != undefined) {
                        root.$refs.AreaDropdown.EmptyRecord();

                    }
                    root.sale.areaId = '';
                }
                if (effectremove == 'employeeId') {
                    root.employeeDescription = '';
                    if (root.$refs.employeeIdRef != undefined) {
                        root.$refs.employeeIdRef.EmptyRecord();

                    }
                    root.sale.employeeId = '';
                }
                if (effectremove == 'DueDate') {
                    root.sale.dueDate = '';
                    root.dueDateRef++;
                }
                if (effectremove == 'deliveryDate') {
                    root.sale.deliveryDate = '';
                    root.dueDateRef1++;
                }

                if (effectremove == 'DeliveryNote') {
                    root.deliveryDescription = '';
                    root.saleOrderDescription = '';
                    root.sale.deliveryChallanId = '';
                    root.sale.saleOrderId = '';
                    root.sale.quotationId = '';
                    root.$refs.childComponentRef.EmtySaleProductList();
                    root.sale.saleItems = [];
                    root.record3 = {
                        saleOrderId: "",
                        deliveryChallanId: "",
                        quotationId: "",
                        quotationTemplateId: "",
                        proformaId: "",
                        purchaseOrderId: "",
                        selectedValue: "",
                        document: "",
                        detail: "",
                    };

                } else if (effectremove == 'saleOrder') {
                    root.sale.deliveryChallanId = '';
                    root.deliveryDescription = '';
                    root.sale.quotationId = '';
                    root.sale.saleOrderId = '';
                    root.saleOrderDescription = '';

                    root.record3 = {
                        saleOrderId: "",
                        deliveryChallanId: "",
                        quotationId: "",
                        quotationTemplateId: "",
                        proformaId: "",
                        purchaseOrderId: "",
                        selectedValue: "",
                        document: "",
                        detail: "",
                    };

                    root.$refs.childComponentRef.EmtySaleProductList();
                    root.sale.saleItems = [];
                } else if (effectremove == 'ProfomaInvoice') {
                    root.sale.deliveryChallanId = '';
                    root.deliveryDescription = '';
                    root.sale.quotationId = '';
                    root.sale.saleOrderId = '';
                    root.sale.proformaId = '';
                    root.sale.purchaseOrderId = '';
                    root.saleOrderDescription = '';
                    root.record3 = {
                        saleOrderId: "",
                        deliveryChallanId: "",
                        quotationId: "",
                        quotationTemplateId: "",
                        proformaId: "",
                        purchaseOrderId: "",
                        selectedValue: "",
                        document: "",
                        detail: "",
                    };

                    root.$refs.childComponentRef.EmtySaleProductList();
                    root.sale.saleItems = [];

                } else if (effectremove == 'PurchaseOrder') {
                    root.sale.deliveryChallanId = '';
                    root.deliveryDescription = '';
                    root.sale.quotationId = '';
                    root.sale.saleOrderId = '';
                    root.sale.proformaId = '';
                    root.sale.purchaseOrderId = '';
                    root.saleOrderDescription = '';
                    root.record3 = {
                        saleOrderId: "",
                        deliveryChallanId: "",
                        quotationId: "",
                        quotationTemplateId: "",
                        proformaId: "",
                        purchaseOrderId: "",
                        selectedValue: "",
                        document: "",
                        detail: "",
                    };

                    root.$refs.childComponentRef.EmtySaleProductList();
                    root.sale.saleItems = [];

                } else if (effectremove == 'quotation') {
                    root.sale.deliveryChallanId = '';
                    root.deliveryDescription = '';
                    root.sale.saleOrderId = '';
                    root.sale.quotationId = '';
                    root.saleOrderDescription = '';
                    root.$refs.childComponentRef.EmtySaleProductList();
                    root.sale.saleItems = [];
                    root.record3 = {
                        saleOrderId: "",
                        deliveryChallanId: "",
                        quotationId: "",
                        quotationTemplateId: "",
                        proformaId: "",
                        purchaseOrderId: "",
                        selectedValue: "",
                        document: "",
                        detail: "",
                    };

                }
                this.randerEffect++;

            },

            GetSaleOrderDetail: function (id, isSaleOrder, detail, notSave) {
                if(this.record3.selectedValue!=null && this.record3.selectedValue!=undefined
                 && this.record3.selectedValue!='' && notSave == 'NotSave' )
                {
                    this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Un-Select Current Document than select New",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });
                    return;
                }


                var root = this;
                if (notSave == 'NotSave') {

                    if (isSaleOrder) {
                        this.record3.saleOrderId = id;
                        this.record3.selectedValue = '(' + detail.registrationNumber + ' ' + this.getDate(detail.date) + ')-' + root.currency + ' ' + parseFloat(detail.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                        this.record3.quotationId = '';
                        this.record3.document = 'SaleOrder';
                        this.record3.detail = detail;

                        this.record3.deliveryChallanId = '';
                        this.record3.proformaId = '';
                        this.record3.purchaseOrderId = '';
                        this.record3.quotationTemplateId = '';
                        return;

                    } else {
                        this.record3.quotationId = id;
                        this.record3.document = 'Quotation';
                        this.record3.detail = detail;

                        this.record3.selectedValue = '(' + detail.registrationNumber + ' ' + this.getDate(detail.date) + ')-' + root.currency + ' ' + parseFloat(detail.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                        this.record3.saleOrderId = '';
                        this.record3.deliveryChallanId = '';
                        this.record3.proformaId = '';
                        this.record3.purchaseOrderId = '';
                        this.record3.quotationTemplateId = '';
                        return;

                    }
                }

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

                                if (isSaleOrder) {
                                    root.sale.saleOrderId = id;

                                    root.sale.quotationId = '';
                                    root.sale.saleOrderNo = '';
                                    if (notSave == 'ConvertToInvoice') {
                                        root.saleOrderDescription = '(' + response.data.registrationNo + ' ' + root.getDate(response.data.date) + ')-' + root.currency + ' ' + parseFloat(response.data.totalAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.sale.customerId = response.data.customerId;
                                        root.sale.isCredit = false;
                                        root.paymentMethod = 'Cash'

                                        root.customerRender++;
                                        root.randerEffect++;
                                        root.isWalkIn = false;

                                    } else {
                                        root.saleOrderDescription = '(' + detail.registrationNumber + ' ' + root.getDate(detail.date) + ')-' + root.currency + ' ' + parseFloat(detail.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");

                                    }
                                } else {
                                    root.sale.saleOrderId = '';
                                    root.sale.quotationNo = '';
                                    root.sale.quotationId = id;
                                    if (notSave == 'ConvertToInvoice') {
                                        root.saleOrderDescription = '(' + response.data.registrationNo + ' ' + root.getDate(response.data.date) + ')-' + root.currency + ' ' + parseFloat(response.data.totalAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.sale.customerId = response.data.customerId;

                                        root.sale.isCredit = false;
                                        root.paymentMethod = 'Cash'
                    

                                        root.customerRender++;
                                        root.randerEffect++;
                                        root.isWalkIn = false;

                                    } else {
                                        root.saleOrderDescription = '(' + detail.registrationNumber + ' ' + root.getDate(detail.date) + ')-' + root.currency + ' ' + parseFloat(detail.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");

                                    }


                                    //  if (root.sale.validityDate != null && root.sale.validityDate != '' && root.sale.validityDate != undefined) {

                                    //     root.$swal({
                                    //          title: 'Are you sure to Continue?',
                                    //          text: 'Quotation Validity date is Expired',
                                    //          type: "warning",
                                    //         showCancelButton: true,
                                    //          confirmButtonColor: "#DD6B55",
                                    //          confirmButtonText: 'Confirm',
                                    //          closeOnConfirm: false,
                                    //         closeOnCancel: false
                                    //     }).then(function (result) {
                                    //         if (result.isConfirmed) {
                                    //              console.log("OK");

                                    //         } else {
                                    //             root.$swal('Cancelled', 'Quotation is Cancelled ', );
                                    //             return;
                                    //         }
                                    //     });

                                    //  }

                                }

                                root.sale.dueDate = response.data.dueDate;
                                root.sale.employeeId = response.data.employeeId;
                                root.sale.description = response.data.description;
                                root.sale.termAndCondition = response.data.note;
                                root.sale.termAndConditionAr = response.data.noteAr;
                                root.randerTerms++;
                                root.sale.note = response.data.noteDescription;
                                root.sale.doctorName = response.data.doctorName;
                                root.sale.hospitalName = response.data.hospitalName;
                                root.sale.logisticId = response.data.logisticId;
                                root.sale.taxMethod = response.data.taxMethod;
                                root.sale.discount = response.data.discount;
                                root.sale.isDiscountOnTransaction = response.data.isDiscountOnTransaction;
                                root.sale.isFixed = response.data.isFixed;
                                root.sale.saleOrderNo = response.data.saleOrderNoAndDate;
                                root.sale.peroformaInvoiceNo = response.data.peroformaInvoiceNo;
                                root.sale.purchaseOrderNo = response.data.purchaseOrderNo;
                                root.sale.inquiryNo = response.data.inquiryNoAndDate;
                                root.sale.quotationNo = response.data.quotationNoAndDate;
                                root.sale.referredBy = response.data.referredBy;
                                root.sale.deliveryDate = response.data.deliveryDate;
                                root.sale.isBeforeTax = response.data.isBeforeTax;
                                root.sale.deliveryNoteAndDate = response.data.deliveryNoteAndDate;
                                root.sale.perfomaValidUpto = response.data.perfomaValidUpto;
                                root.sale.dispatchDate = response.data.dispatchDate;
                                root.sale.pickupDate = response.data.pickupDate;

                                root.sale.quotationValidUpto = response.data.quotationValidUpto;
                                root.sale.transactionLevelDiscount = response.data.transactionLevelDiscount;
                                root.logisticDescription = response.data.logisticNameEn + ' ' + response.data.logisticNameAr;
                                root.discountTypeOption = response.data.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                                root.sale.taxRateId = response.data.taxRateId;
                                root.sale.customerInquiry = response.data.customerInquiry;
                                root.sale.poNumber = response.data.poNumber;

                                root.sale.refrenceNo = response.data.refrenceNo;
                                root.sale.clientPurchaseNo = response.data.clientPurchaseNo;
                                root.sale.validityDate = response.data.validityDate;
                                root.sale.purpose = response.data.purpose;

                                if (response.data.poDate != null && response.data.poDate != '' && response.data.poDate != undefined) {
                                    root.sale.poDate = response.data.poDate;
                                    root.randerPoDate++;

                                }

                                // root.sale.mobile = response.data.mobile;

                                // root.sale.poNumber = response.data.registrationNo;
                                // root.sale.poDate = response.data.date;

                                root.$refs.childComponentRef.EmtySaleProductList(root.sale.taxMethod);
                                response.data.saleOrderItems.forEach(function (so) {
                                    if (root.isService) {
                                        //if (so.quantityOut > 0) {
                                        //if (localStorage.getItem('IsMultiUnit') == 'true') {
                                        //    so.highQty = parseInt(parseInt(so.quantityOut) / parseInt(so.product.unitPerPack));
                                        //    so.quantityOut = parseInt(parseInt(so.quantityOut) % parseInt(so.product.unitPerPack));
                                        //}
                                        root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantityOut, so.unitPrice, true, so);

                                    } else {

                                        if (so.quantityOut > 0) {
                                            if (localStorage.getItem('IsMultiUnit') == 'true') {
                                                so.highQty = parseInt(parseInt(so.quantityOut) / parseInt(so.product.unitPerPack));
                                                so.quantityOut = parseInt(parseInt(so.quantityOut) % parseInt(so.product.unitPerPack));
                                            }
                                            root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantityOut, so.unitPrice, true, 0, so, null, false);
                                        }
                                    }
                                });
                                root.GetSaleOrderUsedPaymentDetail(response.data.id);

                                root.soPaymentTotal = response.data.paymentVoucher.reduce((total, item) => total + item.amount, 0);
                                root.logisticRender++;
                                root.employeeRender++;
                                root.dateRander++;
                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }
            },
            GetSaleOrderUsedPaymentDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Purchase/SaleInvoicePaymentList?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {

                            var remaining = root.soPaymentTotal - response.data;
                            if (remaining > 0) {
                                root.soPaymentTotal = remaining;
                            } else {
                                root.soPaymentTotal = 0;
                            }
                            console.log(root.soPaymentTotal);
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            },
            GetDispatchNoteDetail: function (id, index) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {
                    root.$https.get('/Sale/DispatchNoteDetail?Id=' + id[index - 1].id, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                response.data.dispatchNoteItems.forEach(function (dn) {
                                    root.$refs.childComponentRef.addProduct(dn.productId, dn.product, dn.quantity);
                                });
                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }
            },
            getBank: function () {
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/PaymentOptionsList?isActive=true', {
                    headers: { "Authorization": `Bearer ${token}` }
                }).then(function (response) {
                    if (response.data != null) {
                        root.bankList = response.data.paymentOptions;
                    }
                })
            },
            selectBank: function (id, name) {

                this.otherCurrencyId = null;
                this.bankId = id;
                this.isActive = true;
                this.sale.salePayment.paymentOptionId = id;
                this.sale.salePayment.paymentName = name;
            },
            getCurrency: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/CurrencyList', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.currencyList = response.data.currencies;
                        root.currencyList.splice(0, 1);
                    }
                })
            },
            selectCurrency: function (id, name) {
                this.bankId = null;
                this.otherCurrencyId = id;
                this.isActive = true;
                this.sale.otherCurrency.currencyId = id;
                this.sale.salePayment.paymentName = name;
            },
            isCredit: function (valueOnToggle) {

                if (this.sale.id == "00000000-0000-0000-0000-000000000000") {
                    if (this.controlRequest != 0) {
                        if (!this.sale.isCredit) {
                            this.sale.customerId = null;
                            this.sale.customerAddressWalkIn = null;
                            this.sale.cashCustomer = "Walk-In";
                            this.paymentMethod = 'Cash';
                            this.customerRender++;
                        } else {
                            this.sale.customerId = null;
                            this.paymentMethod = 'Credit';
                            this.customerRender++;
                        }
                    }
                    if (valueOnToggle == true) {

                        //if (this.sale.quotationId != null && this.sale.quotationId != '') {
                        //    this.ResetAll();
                        //} else if (this.sale.proformaId != null && this.sale.proformaId != '') {
                        //    this.ResetAll();
                        //} else if (this.sale.saleOrderId != null && this.sale.saleOrderId != '') {
                        //    this.ResetAll();
                        //} else if (this.sale.deliveryChallanId != null && this.sale.deliveryChallanId != '') {
                        //    this.ResetAll();
                        //}

                    }

                    this.controlRequest++;

                } else {
                    {
                        if (!this.sale.isCredit) {
                            this.sale.customerId = null;
                            this.sale.customerAddressWalkIn = null;
                            this.sale.cashCustomer = "Walk-In";
                            this.paymentMethod = 'Cash';
                            this.customerRender++;
                        } else {
                            this.sale.customerId = null;
                            this.paymentMethod = 'Credit';
                            this.customerRender++;
                        }
                    }
                    if (valueOnToggle == true) {

                        //if (this.sale.quotationId != null && this.sale.quotationId != '') {
                        //    this.ResetAll();
                        //} else if (this.sale.proformaId != null && this.sale.proformaId != '') {
                        //    this.ResetAll();
                        //} else if (this.sale.saleOrderId != null && this.sale.saleOrderId != '') {
                        //    this.ResetAll();
                        //} else if (this.sale.deliveryChallanId != null && this.sale.deliveryChallanId != '') {
                        //    this.ResetAll();
                        //}

                    }
                    this.controlRequest++;
                }

            },
            isPayment: function (credit) {

                if (credit == 'Bank') {
                    if (localStorage.getItem('BankAccountId') != null && localStorage.getItem('BankAccountId') != undefined && localStorage.getItem('BankAccountId') != '' && localStorage.getItem('BankAccountId') != 'null' && localStorage.getItem('BankAccountId') != "null" && localStorage.getItem('BankAccountId') != "00000000-0000-0000-0000-000000000000" && localStorage.getItem('BankAccountId') != '00000000-0000-0000-0000-000000000000') {

                        this.sale.bankCashAccountId = localStorage.getItem('BankAccountId');
                    }
                }
                else if (credit == 'Cash') {
                    if (localStorage.getItem('CashAccountId') != null && localStorage.getItem('CashAccountId') != undefined && localStorage.getItem('CashAccountId') != '' && localStorage.getItem('CashAccountId') != 'null' && localStorage.getItem('CashAccountId') != "null" && localStorage.getItem('CashAccountId') != "00000000-0000-0000-0000-000000000000" && localStorage.getItem('CashAccountId') != '00000000-0000-0000-0000-000000000000') {

                        this.sale.bankCashAccountId = localStorage.getItem('CashAccountId');
                    }
                }

                if (credit == 'Advance') {
                    this.sale.bankCashAccountId = this.customerAdvanceAccountId;
                    this.accountRender++;
                }
                if (credit == 'Credit') {
                    console.log("Working");
                }
                this.sale.salePayment.paymentOptionId = null;
                this.bankId = null;
                this.sale.salePayment.paymentType = credit;
                this.accountRender++;
            },
            DefaultOnList: function (record,value) {

                 {
                    if(value)
                    {
                        this.deliveryAddressList.forEach(function (cat) {
                        // If the current record in the list is not the same as the input record
                        if (record.id !== cat.id) {
                            // Set isOffice to false for other records
                            cat.isOffice = false;
                        } else {
                            // Set isOffice to true for the current record
                            record.isOffice = true;
                        }
                    });
                    }
                    else
                    {
                        this.deliveryAddressList.forEach(function (cat) {
                        // If the current record in the list is not the same as the input record
                         {
                            // Set isOffice to false for other records
                            cat.isOffice = false;
                        } 
                    });
                    }
                    
                    // Loop through the delivery address list
                   
    }
            },
            emptyCashCustomer: function (customerId, advanceAccountId, customerDetail) {
                if (this.custemerOldId != customerId) {
                    // this.getAddress();
                }
                if (customerDetail != null && customerDetail != undefined && customerDetail != '') {

                    if (customerDetail.englishName == 'Walk-In') {
                        this.isWalkIn = true
                    } else {
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
                        this.sale.paymentTerms = customerDetail.paymentTerms;
                        this.deliveryAddressList = customerDetail.deliveryAddressList;
                        
                        debugger; //eslint-disable-line


                        if(this.sale.billingId !=null && this.sale.billingId!=''&& this.sale.billingId!=undefined )
                        {
                            let  id=this.sale.billingId;
                            this.deliveryAddressList.forEach(function (cat) {
                                // If the current record in the list is not the same as the input record
                                if (id== cat.id) {
                                    // Set isOffice to false for other records
                                    cat.isOffice = true;
                                } else {
                                    cat.isOffice = false;
                                }
                            });



                        }

                        // this.sale.deliveryChallanId = '';
                        this.expandSale = false;
                        this.expandQuotation = false;
                        // this.deliveryDescription = '';
                        // this.sale.saleOrderId = '';
                        // this.sale.quotationId = '';
                        // this.saleOrderDescription = '';
                        this.deliverychallanListWithCanvas = [];
                        this.quotationListWithCanvas = [];
                        this.saleOrderListWithCanvas = [];

                    }

                } else {
                    this.isWalkIn = false;
                }
                this.custemerOldId = customerId;
                this.sale.customerId = customerId;
                this.customerAdvanceAccountId = advanceAccountId;
                this.selectedCashCustomer = ''

                if (this.sale.customerId != '00000000-0000-0000-0000-000000000000' && this.sale.customerId != null) {
                    if (this.sale.isCredit) {
                        this.paymentMethod = 'Credit';
                    }

                    this.search = "";
                }
                else {
                    this.paymentMethod = 'Cash';
                    // this.sale.cashCustomer = "Walk-In";
                    // this.selectedCashCustomer = this.cashCustomerOptions.find(function (x) {
                    //     return x.name == 'Walk-In';
                    // })
                    this.sale.mobile = '';
                    // this.sale.cashCustomerId = '';
                    this.sale.customerId = '';
                    this.sale.email = '';
                    this.sale.customerAddressWalkIn = '';
                }
                if (this.isRaw == 'true') {
                    var root = this;
                    var token = "";
                    if (token == '') {
                        token = localStorage.getItem("token");
                    }
                    root.options = [];
                    this.$https
                        .get('/Sale/DispatchNoteList?isDropdown=' + true + '&customerId=' + customerId + '&branchId=' + localStorage.getItem('BranchId'), {
                            headers: {
                                Authorization: `Bearer ${token}`
                            },
                        })
                        .then(function (response) {

                            if (response.data != null) {
                                response.data.results.forEach(function (dn) {
                                    root.options.push({
                                        id: dn.id,
                                        name: dn.registrationNumber,
                                    });
                                });
                            }
                        })
                        .then(function () {
                            root.value = root.options.find(function (x) {
                                return x.id == root.values;
                            });
                        });
                }

                if (this.isCustomerPrice) {
                    this.GetPrice(null, this.sale.customerId)
                } else if (this.isCustomerPriceLabel) {
                    this.GetPriceRecordLabelId(customerDetail.priceLabelId);
                }

            },
            GetPriceRecordLabelId: function (val) {

                this.priceLabelingId = val;
                this.priceLabelRender++;

                this.GetPrice(val, null);
            },
            GotoPage: function (link) {
                this.$router.push({
                    path: link
                });
            },
            GetTime: function (x) {
                if (x == null || x == undefined || x == '')
                    return '';
                return moment(x).format('HH:mm:ss');

            },
            setCustomer: function (value) {
                console.log(value);
            },
            OnEditInvoice: function () {

                this.updateSummary(this.summary);
                this.sale.discountAmount = parseFloat(this.summary.discount);
                this.sale.totalAfterDiscount = parseFloat(this.summary.totalAfterDiscount);
                this.sale.vatAmount = parseFloat(this.summary.vat);
                this.sale.totalAmount = parseFloat(this.summary.withVat);

                this.sale.salePayment.dueAmount = (this.sale.salePayment.dueAmount) <= 0 ? '0' : (this.sale.salePayment.dueAmount);
                var total = 0;
                if (this.sale.salePayment.paymentTypes != '' && this.sale.salePayment.paymentTypes != undefined) {
                    this.sale.salePayment.paymentTypes.forEach(x => {

                        total += x.amount;
                    });
                    total = total + parseFloat(this.sale.salePayment.received);
                }
                if (total != 0) {
                    if (this.sale.salePayment.balance <= total) {
                        this.sale.salePayment.balance = 0;
                        this.sale.salePayment.change = total - this.sale.salePayment.dueAmount;
                    }
                }

                this.onPayScreen = true;
            },
            CreditAmountReceived: function () {

                if (this.sale.salePayment.received == '') {
                    this.sale.salePayment.received = 0;
                }
                if (this.sale.salePayment.creditAmount == '') {
                    this.sale.salePayment.creditAmount = 0;
                }

                if (this.sale.salePayment.balance > this.sale.salePayment.creditAmount) {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: "Credit Amount not Greater Than Balance Amount ",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                    });
                    return;

                }
                this.sale.salePayment.received = parseFloat(this.sale.salePayment.received) + parseFloat(this.sale.creditAmount);
                this.calculatBalance(this.sale.creditAmount);
                // this.sale.salePayment.received = parseFloat(this.sale.creditAmount == '' ? 0 : parseFloat(this.sale.salePayment.received)-parseFloat(this.sale.creditAmount)).toFixed(3).slice(0, -1) ;
            },
            calculatBalance: function (received) {

                if (received == '') {
                    received = 0;
                }
                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                this.sale.salePayment.balance = (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount < 0 ? (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount : 0;
                this.sale.salePayment.change = (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount > 0 ? (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount : 0;

                this.sale.change = this.sale.salePayment.change;
            },
            OtherCurrencyCalculate: function () {
                this.sale.salePayment.received = parseFloat(this.sale.otherCurrency.amount == '' ? 0 : this.sale.otherCurrency.amount).toFixed(3).slice(0, -1) * parseFloat(this.sale.otherCurrency.rate == '' ? 0 : this.sale.otherCurrency.rate).toFixed(3).slice(0, -1);
            },

            addPayment: function (amount, paymentType, name) {

                var root = this;

                this.sale.salePayment.received = 0;
                if (paymentType == 'Cash') {
                    var payment = this.sale.salePayment.paymentTypes.find((x) => x.paymentType == paymentType)
                    if (payment != undefined) {
                        payment.amount += parseFloat(amount);
                        payment.name = name;
                    } else {
                        this.sale.salePayment.paymentTypes.push({
                            paymentType: paymentType,
                            amount: parseFloat(amount),
                            name: name,
                            rate: 0,
                            otherAmount: 0,
                            bankCashAccountId: root.sale.bankCashAccountId
                        });
                    }
                } else if (paymentType == 'Bank') {

                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: 0,
                        otherAmount: 0,
                        bankCashAccountId: root.sale.bankCashAccountId
                    });
                } else if (paymentType == 'Credit') {

                    var paymentTypesAmount;
                    if (this.sale.salePayment.paymentTypes.length > 0) {
                        paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                            total + payment.amount, 0);
                    } else {
                        paymentTypesAmount = 0;
                    }

                    const balance = (parseFloat(amount) + paymentTypesAmount);
                    const amountVal = this.sale.salePayment.dueAmount;

                    if (balance > amountVal) {
                        this.calculatBalance(0);
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Credit Amount not Greater Than Balance Amount ",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });
                        return;
                    }

                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: 0,
                        otherAmount: 0,
                        bankCashAccountId: root.sale.bankCashAccountId
                    });

                } else if (paymentType == 'CashVoucher') {
                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: 0,
                        otherAmount: 0,
                        bankCashAccountId: root.sale.bankCashAccountId
                    });
                } else {
                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: root.sale.otherCurrency.rate,
                        otherAmount: root.sale.otherCurrency.amount,
                        bankCashAccountId: root.sale.bankCashAccountId
                    });
                }
                //var payment = this.sale.salePayment.paymentTypes.find((x) => x.paymentType == paymentType)
                //if (payment != undefined) {
                //    payment.amount += parseFloat(amount);
                //} else {
                //    this.sale.salePayment.paymentTypes.push({ paymentType: paymentType, amount: parseFloat(amount) });
                //}

                this.calculatBalance(0);
                this.sale.otherCurrency.amount = 0;
                this.sale.otherCurrency.rate = 0;
            },

            removeFromPaymentList: function (paymentType) {
                this.sale.salePayment.paymentTypes.splice(paymentType, 1);
                //this.sale.salePayment.paymentTypes = this.sale.salePayment.paymentTypes.filter((x) =>
                //    x.paymentType != paymentType);

                this.calculatBalance(0);
            },

            updateSummary: function (summary) {
                this.sale.grossAmount = summary.total;
                this.sale.vatAmount = summary.vat;
                this.sale.amount = summary.withVat;

                this.sale.discountAmount = summary.discount;
                this.sale.totalAfterDiscount = summary.totalAfterDiscount;
                this.sale.totalAmount = summary.withVat;

                this.summary = summary;
                this.sale.salePayment.dueAmount = parseFloat(this.summary.withVat)

            },
            AutoIncrementCode: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                if (this.formName == 'ServiceSaleOrder' || this.formName == 'ServiceQuotation') {
                    if (this.formName == 'ServiceSaleOrder') {
                        root.sale.isQuotation = false
                    }
                    if (this.formName == 'ServiceQuotation') {
                        root.sale.isQuotation = true
                    }

                    var terminalId = '';

                    if (localStorage.getItem('TerminalId') != null && localStorage.getItem('TerminalId') != undefined && localStorage.getItem('TerminalId') != "null" && localStorage.getItem('TerminalId') != 'null') {
                        terminalId = localStorage.getItem('TerminalId');
                    }
                    root.$https
                        .get("/Purchase/SaleOrderAutoGenerateNo?isQuotation=" + root.sale.isQuotation + '&terminalId=' + terminalId + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&branchId=' + localStorage.getItem('BranchId'), {
                            headers: {
                                Authorization: `Bearer ${token}`
                            },
                        })
                        .then(function (response) {
                            if (response.data != null) {
                                root.sale.registrationNo = response.data.documentNo;
                                root.prefix = response.data.prefixiesLookupModel;
                            }
                        });

                } else {
                    // Sale Service Invoice
                    root.$https.get("/Sale/SaleAutoGenerateNo?userId=" + localStorage.getItem('UserID') + '&terminalId=' + localStorage.getItem('TerminalId') + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&branchId=' + localStorage.getItem('BranchId'), {
                        headers: {
                            Authorization: `Bearer ${token}`
                        },
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                if (root.sale.id == '00000000-0000-0000-0000-000000000000') {
                                    if (root.formName == 'ServiceProformaInvoice') {
                                        root.sale.registrationNo = response.data.proformaInvoice;

                                    } 
                                    else if (root.formName == 'PurchaseOrder') {
                                        root.sale.registrationNo = response.data.purchaseOrder;

                                    } 
                                    else if (root.formName == 'ProformaInvoice') {
                                        root.sale.registrationNo = response.data.proformaInvoice;

                                    } 
                                    else {
                                        root.sale.registrationNo = response.data.paid;

                                    }

                                    root.autoNumber = response.data.hold;
                                    root.autoNumberCredit = response.data.credit;
                                    root.prefix = response.data.prefixiesLookupModel;
                                } else {
                                    root.autoNumber = response.data.paid;
                                    root.autoNumberCredit = response.data.credit;
                                    root.prefix = response.data.prefixiesLookupModel;
                                }
                            }
                        })
                        .catch(error => {
                            console.log(error)
                            root.$swal.fire({
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                        });

                }

            },
            SaveSaleItems: function (saleItems, discount, adjustmentSignProp, transactionLevelDiscount) {
                this.sale.saleItems = saleItems;
                this.sale.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

                this.sale.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            },
            updateDiscountChanging: function (isFixed, isBeforeTax) {

                this.sale.isFixed = isFixed;
                this.sale.isBeforeTax = isBeforeTax;

            },
            PrintInvoice: function (value) {

                var id = value;
                var root = this;
                this.isTouchScreen = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.printDetails = [];
                root.printDetailsCashVoucher = [];
                root.$https.get("/Sale/SaleDetail?id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetailsPos = response.data;

                            //if (localStorage.getItem('IsMultiUnit') == 'true') {

                            //    root.printDetails.saleItems.forEach(function (x) {

                            //        x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                            //        x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                            //        x.unitPerPack = x.product.unitPerPack;
                            //    });
                            //}
                            root.printRender++;
                            root.loading = false
                        }
                    })
                    .catch(error => {
                        var path = localStorage.getItem('CurrentPath');
                        root.$router.go(path)
                        console.log(error)
                    })
            },


            saveSale: function (invoiceType, print) {

                // if (this.$route.query.data != undefined) {
                //     this.sale.deliveryChallanId = this.$route.query.data.deliveryChallanId;
                //     this.sale.saleOrderId = this.$route.query.data.saleOrderId;
                //     this.sale.quotationId = this.$route.query.data.quotationId;
                //     this.sale.proformaId = this.$route.query.data.proformaId;
                //     // alert(this.sale.quotationId);
                // }
                
                if (invoiceType == 'Hold') {
                    this.sale.saleHoldTypes = "Hold";
                } else {
                    this.sale.saleHoldTypes = "Default";
                }
                this.loading = true;
                this.sale.isButtonDisabled = true;
                var root = this;
                localStorage.setItem('active', invoiceType);
                root.sale.invoiceType = invoiceType;
                var token = localStorage.getItem('token');

                if (this.sale.salePayment.received != 0) {
                    this.addPayment(this.sale.salePayment.received, this.sale.salePayment.paymentType, this.sale.salePayment.paymentName);
                }
                if (this.sale.shippingAddress == undefined) {
                    this.sale.shippingAddress = '';
                }
                if (this.sale.employeeId == undefined) {
                    this.sale.employeeId = '';
                }
                if (this.sale.logisticId == undefined) {
                    this.sale.logisticId = '';
                }

                this.toogletab = this.sale.isCredit ? true : false;
                this.sale.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
                this.sale.soInventoryReserve = localStorage.getItem('SoInventoryReserve');
                this.sale.autoPaymentVoucher = localStorage.getItem('AutoPaymentVoucher');
                this.sale.userName = localStorage.getItem('LoginUserName');
                this.sale.partiallyQuotation = localStorage.getItem('partiallyQuotation') == 'true' ? true : false;
                this.sale.partiallySaleOrder = localStorage.getItem('partiallySaleOrder') == 'true' ? true : false;
                this.sale.terminalId = localStorage.getItem('TerminalId');
                this.sale.invoicePrefix = localStorage.getItem('InvoicePrefix');
                this.sale.allowPreviousFinancialPeriod = localStorage.getItem('AllowPreviousFinancialPeriod') == 'true' ? true : false;


                this.sale.branchId = localStorage.getItem('BranchId');
                this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

                if (this.multipleAddress) {
                    if (this.deliveryAddressList.length > 0) {
                        // var defaultAddress = this.deliveryAddressList.find(x => x.type == 'Delivery' && x.isOffice);
                        // if (defaultAddress != null) {
                        //     this.sale.deliveryId = defaultAddress.id;
                        // }

                        // var shippingAddress = this.deliveryAddressList.find(x => x.type == 'Shipping' && x.isOffice);
                        // if (shippingAddress != null) {
                        //     this.sale.shippingId = shippingAddress.id;
                        // }
                        var billingAddress = this.deliveryAddressList.find(x =>x.isOffice);
                        if (billingAddress != null) {
                            this.sale.billingId = billingAddress.id;
                        }
                        else
                        {
                            this.sale.billingId='';
                        }

                        // var nationalAddress = this.deliveryAddressList.find(x => x.type == 'National' && x.isOffice);
                        // if (nationalAddress != null) {
                        //     this.sale.nationalId = nationalAddress.id;
                        // }

                    }

                }

                this.sale.date = moment().format("l");

                this.sale.saleItems.forEach(function (x) {
                    x.quantity = x.totalPiece;
                });
                

                this.$https.post('/Sale/SaveSaleInformation', root.sale, { headers: { "Authorization": `Bearer ${token}` } }).then(response => {
                    
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                  
                        if (print) {
                            if (root.$route.query.isDuplicate == 'true') {
                                root.addSale = 'saleService';
                            }
                            if (root.headerFooter.printSizeA4 == 'Thermal') {
                                root.PrintInvoice(response.data.message.id);
                            } else {
                                root.PrintRdlc(response.data.message.id);
                            }
                        }
                        else {
                            root.$swal({
                                title: root.$t('SavedSuccessfully'),
                                text: root.$t('Saved'),
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {

                                if (ok != null) {
                                    if (root.sale.id == '00000000-0000-0000-0000-000000000000' && !root.sale.isDuplicate) {
                                        root.ResetAll();
                                        
                                    } else {
                                        if (root.formName == 'ServiceQuotation') {
                                            if (root.isService) {
                                            root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: root.formName,
                                                }
                                                });
                                            } 
                                            else 
                                            {
                                                root.$router.push({
                                                    path: '/SaleServiceOrder',
                                                    query:{
                                                        formName: 'Quotation',
                                                    }
                                                });
                                            }

                                        } else if (root.formName == 'ServiceSaleOrder') {
                                            if (root.isService) {
                                                root.$router.push({
                                                    path: '/SaleServiceOrder',
                                                    query:{
                                                        formName:  root.formName,
                                                    }
                                                });
                                            } else {
                                                root.$router.push({
                                                    path: '/SaleServiceOrder',
                                                    query:{
                                                        formName: 'SaleOrder',
                                                    }

                                                });
                                            }

                                        } else if (root.formName == 'ServiceProformaInvoice') {
                                            if (root.isService) {

                                                root.$router.push({
                                                    path: '/ServiceProformaInvoice',
                                                    query:{
                                                        formName: root.formName,
                                                    }
                                                });
                                            } 
                                            else 
                                            {
                                                root.$router.push({
                                                    path: '/ServiceProformaInvoice',
                                                    query:{
                                                        formName: 'ProformaInvoice',
                                                    }
                                                });

                                            }

                                        }
                                        else if (root.formName == 'ProformaInvoice') {
                                            if (root.isService) {

                                                root.$router.push({
                                                path: '/ServiceProformaInvoice', 
                                                query:{
                                                    formName : 'ProformaInvoice',
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName : 'ProformaInvoice',
                                                }
                                            });

                                            }

                                        } else if (root.formName == 'PurchaseOrder') {
                                            if (root.isService) {
                                                root.$router.push({
                                                    path: '/ServiceProformaInvoice',
                                                    query:{
                                                        formName: root.formName,
                                                    }
                                                });
                                            } else {
                                                root.$router.push({
                                                    path: '/ServiceProformaInvoice',
                                                    query:{
                                                        formName: root.formName,
                                                    }
                                                });

                                            }

                                        } else if (root.formName == 'CreateDocument') {
                                            if (root.isService) {
                                                root.$router.push({
                                                    path: '/SaleService',
                                                    query:{
                                                        formName: 'Document',
                                                    }
                                                });
                                            } else {
                                                root.$router.push({
                                                    path: '/SaleService',
                                                    query:{
                                                        formName: 'Document',
                                                    }
                                                });
                                            }

                                        } else {
                                            if (root.isService) {
                                                root.$router.push({
                                                    path: '/SaleService',
                                                    query: {
                                                        data: 'addSaleService'
                                                    }
                                                });
                                            } else {
                                                root.$router.push({
                                                    path: '/SaleService',
                                                    query: {
                                                        data: 'addSaleService'
                                                    }
                                                });
                                            }
                                        }
                                    }
                                }
                            });
                            root.loading = false
                        }
                    }
                    else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        if (print) {
                            root.addSale = 'saleService';

                            if (root.headerFooter.printSizeA4 == 'Thermal') {
                                root.PrintInvoice(response.data.message.id);
                            } else {
                                root.PrintRdlc(response.data.message.id);
                            }
                        } else {
                            root.$swal({
                                title: root.$t('UpdateSuccessfully'),
                                text: root.$t('Updated'),
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.sale.invoiceType == 'Hold') {

                                        if (root.formName == 'ServiceQuotation') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: 'Quotation',
                                                }
                                            });
                                            }

                                        } else if (root.formName == 'ServiceSaleOrder') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: 'SaleOrder',
                                                }
                                            });
                                            }

                                        } else if (root.formName == 'ServiceProformaInvoice') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName: 'ProformaInvoice',
                                                }
                                            });

                                            }

                                        }
                                        else if (root.formName == 'ProformaInvoice') {
                                            if (root.isService) {

                                                root.$router.push({
                                                path: '/ServiceProformaInvoice', 
                                                query:{
                                                    formName : 'ProformaInvoice',
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName : 'ProformaInvoice',
                                                }
                                            });

                                            }

                                        } else if (root.formName == 'PurchaseOrder') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });


                                            }

                                        } else if (root.formName == 'CreateDocument') {
                                            if (root.isService) {

                                                root.$router.push({
                                                path: '/SaleService',
                                                query:{
                                                    formName:  'Document',
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleService',
                                                query:{
                                                    formName: 'Document',
                                                }
                                            });

                                            }

                                        } else {
                                            if (root.isService) {
                                                root.$router.push({
                                                    path: '/SaleService',
                                                    query: {
                                                        data: 'addSaleService'
                                                    }
                                                });
                                            } else {
                                                root.$router.push({
                                                    path: '/SaleService',
                                                    query: {
                                                        data: 'addSaleService'
                                                    }
                                                });
                                                
                                            }

                                        }
                                    } else {

                                        if (root.formName == 'ServiceQuotation') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: 'Quotation',
                                                }
                                            });
                                            }

                                        } else if (root.formName == 'ServiceSaleOrder') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: root.formName,
                                                }

                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleServiceOrder',
                                                query:{
                                                    formName: 'SaleOrder',
                                                }
                                            });
                                            }

                                        }  else if (root.formName == 'PurchaseOrder') {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/ServiceProformaInvoice',
                                                query:{
                                                    formName: root.formName,
                                                }
                                            });

                                            }

                                        } else if (root.formName == 'CreateDocument') {
                                            if (root.isService) {

                                                root.$router.push({
                                                path: '/SaleService',
                                                query:{
                                                    formName: 'Document',
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleService',
                                                query:{
                                                    formName:'Document',
                                                }
                                            });

                                            }

                                        } else {
                                            if (root.isService) {
                                                root.$router.push({
                                                path: '/SaleService',
                                                query: {
                                                    data: 'addSaleService'
                                                }
                                            });
                                            } else {
                                                root.$router.push({
                                                path: '/SaleService',
                                                query: {
                                                    data: 'addSaleService'
                                                }
                                            });

                                            }

                                        }

                                    }
                                }
                            });
                            root.loading = false
                        }

                    } 
                    else 
                    {
                        root.$swal({
                            title: root.$t('Error'),
                            text: root.$t('SomethingWrong'),
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.loading = false
                    }
                })
                    .catch(error => {
                        

                        root.$swal.fire({
                            type: 'error',
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error.response.data,
                            confirmButtonClass: "btn btn-danger",
                            showConfirmButton: true,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                        root.sale.isButtonDisabled = false;
                        root.loading = false
                    })
                    .finally(() => console.log('finally'))

            },

            goToSale: function () {
                if (this.isValid('CanViewHoldInvoices') || this.isValid('CanViewPaidInvoices') || this.isValid('CanViewCreditInvoices')) {

                    var root = this;
                    

                    if (root.formName == 'ServiceQuotation') {
                        if (root.isService) {
                            root.$router.push({
                            path: '/SaleServiceOrder',
                            query:{
                                formName : root.formName,
                            }
                        });

                        } else {
                            root.$router.push({
                            path: '/SaleServiceOrder',
                            query:{
                                formName : 'Quotation',
                            }
                        });


                        }

                    } else if (root.formName == 'ServiceSaleOrder') {
                        if (root.isService) {
                            root.$router.push({
                            path: '/SaleServiceOrder',
                            query:{
                                formName : root.formName,
                            }

                        });
                        } else {
                            root.$router.push({
                            path: '/SaleServiceOrder',
                            query:{
                                formName : 'SaleOrder',
                            }
                        });
                        }

                    } 
                    else if (root.formName == 'ServiceProformaInvoice') {
                        if (root.isService) {

                            root.$router.push({
                            path: '/ServiceProformaInvoice', 
                            query:{
                                formName : 'ServiceProformaInvoice',
                            }
                        });
                        } else {
                            root.$router.push({
                            path: '/ServiceProformaInvoice',
                            query:{
                                formName : 'ServiceProformaInvoice',
                            }
                        });

                        }

                    } 
                    else if (root.formName == 'ProformaInvoice') {
                        if (root.isService) {

                            root.$router.push({
                            path: '/ServiceProformaInvoice', 
                            query:{
                                formName : 'ProformaInvoice',
                            }
                        });
                        } else {
                            root.$router.push({
                            path: '/ServiceProformaInvoice',
                            query:{
                                formName : 'ProformaInvoice',
                            }
                        });

                        }

                    } 
                    else if (root.formName == 'PurchaseOrder') {
                        if (root.isService) {

                            root.$router.push({
                            path: '/ServiceProformaInvoice',
                            query:{
                                formName : root.formName,
                            }
                        });
                        } else {
                            root.$router.push({
                            path: '/ServiceProformaInvoice',
                            query:{
                                formName : root.formName,
                            }
                        });

                        }

                    } else if (root.formName == 'CreateDocument') {
                        if (root.isService) {

                            root.$router.push({
                            path: '/SaleService',
                            query:{
                                formName : 'Document',
                            }
                        });
                        } else {
                            root.$router.push({
                            path: '/SaleService',
                            query:{
                                formName : 'Document',
                            }
                        });
                        }

                    } else {
                        if (root.isService) {
                            root.$router.push({
                            path: '/SaleService',
                            query: {
                                data: 'addSaleService'
                            }

                        });
                        } else {
                            root.$router.push({
                            path: '/SaleService',
                            query: {
                                data: 'addSaleService'
                            }

                        });

                        }

                    }

                } else {
                    this.ResetAll();
                    // this.$router.go();
                }

            },

            GetUserDefineFlow: function () {
                var root = this;

                if (localStorage.getItem('quotationToSaleOrder') == undefined) {
                    root.$https.get('/Company/UserDefineFlowEdit?companyId=' + localStorage.getItem('CompanyID'), {
                        headers: {
                            "Authorization": `Bearer ${localStorage.getItem('token')}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                localStorage.setItem('quotationToSaleOrder', response.data.quotationToSaleOrder);
                                localStorage.setItem('quotationToSaleInvoice', response.data.quotationToSaleInvoice);
                                localStorage.setItem('partiallyQuotation', response.data.partiallyQuotation);
                                localStorage.setItem('partiallySaleOrder', response.data.partiallySaleOrder);

                                root.saleOrder = localStorage.getItem('quotationToSaleInvoice') == 'true' ? true : false;
                            } else {
                                console.log("error: something wrong from db.");
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                } else {
                    this.saleOrder = localStorage.getItem('quotationToSaleInvoice') == 'true' ? true : false;

                }

            }

        },
        created: function () {
            var root =  this;
            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = root.$store.isGetEdit;
            }

            this.isService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
            this.sale.isService = this.isService;
            this.sale.wareHouseId = localStorage.getItem('WareHouseId');

            this.sale.taxRateId = localStorage.getItem('TaxRateId');
            this.sale.taxMethod = localStorage.getItem('taxMethod');
            this.discountTypeOption = localStorage.getItem('DiscountTypeOption');
            this.sale.isDiscountOnTransaction = localStorage.getItem('DiscountTypeOption') === 'At Transaction Level' ? true : false;
            this.sale.isCredit = false

            if (this.formName == 'ServiceSaleOrder' || this.formName == 'ServiceQuotation' || this.formName == 'ServiceProformaInvoice' || this.formName == 'ProformaInvoice' || this.formName == 'PurchaseOrder') {
                if (this.formName == 'ServiceQuotation') {
                    this.sale.documentType = 'ServiceQuotation';
                   
                    

                }
                if (this.formName == 'ServiceSaleOrder') {
                    this.sale.documentType = 'ServiceSaleOrder';
                    this.sale.wareHouseId = localStorage.getItem('WareHouseId');

                }
                if (this.formName == 'ServiceProformaInvoice') {
                    this.sale.documentType = 'ServiceProformaInvoice';

                }if (this.formName == 'ProformaInvoice') {

                    this.sale.documentType = 'ProformaInvoice';

                }
                // if (this.formName == 'ProformaInvoice') {
                //     this.sale.documentType = 'ProformaInvoice';

                // }
                if (this.formName == 'PurchaseOrder') {
                    this.sale.documentType = 'PurchaseOrder';
                    this.sale.isPurchaseOrder = true;

                }

                this.isDayAlreadyStart = true;

                if (this.$route.query.data != undefined) {
                    var datasaleOrder = this.$route.query.data;

                    // if (datasaleOrder.invoiceType == 4) {
                    //     this.sale.proformaInvoiceId = this.sale.id;
                    //     this.sale.id = '00000000-0000-0000-0000-000000000000';
                    //     this.sale.registrationNo = datasaleOrder.registrationNo;
                    // }
                    this.sale.date = datasaleOrder.date;
                    this.sale.isEditPaidInvoice = this.$route.query.isEditPaidInvoice == 'true' ? true : false;
                    this.sale.clone = this.$route.query.clone == 'true' ? true : false;
                    this.sale.invoiceType = datasaleOrder.approvalStatus == 'Draft' || datasaleOrder.approvalStatus == 4 ? 'Hold' : 'Credit';
                    this.sale.dueDate = datasaleOrder.dueDate;
                    // this.sale.mobile = datasaleOrder.mobile;
                    // this.sale.email = datasaleOrder.email;
                    // this.sale.code = datasaleOrder.code;
                    // this.sale.areaId = datasaleOrder.areaId;
                    this.sale.employeeId = datasaleOrder.employeeId;
                    this.sale.isCredit = true;
                    if (this.formName == 'ServiceSaleOrder' || this.formName == 'ServiceQuotation') {
                        this.paymentMethod = 'Cash';
                    }

                    this.sale.id = datasaleOrder.id;
                    if (this.sale.clone) {
                        this.AutoIncrementCode();
                        this.sale.id = '00000000-0000-0000-0000-000000000000';

                        this.controlRequest++;

                    }
                    else {
                        this.sale.registrationNo = datasaleOrder.registrationNo;

                    }

                    this.prefix = datasaleOrder.prefixiesLookupModel;
                    this.sale.customerId = datasaleOrder.customerId;
                    this.sale.wareHouseId = datasaleOrder.wareHouseId;
                    this.sale.description = datasaleOrder.description;
                    this.sale.saleItems = datasaleOrder.saleOrderItems == null ? datasaleOrder.saleItems : datasaleOrder.saleOrderItems;
                    this.sale.termAndCondition = datasaleOrder.note;
                    this.sale.termAndConditionAr = datasaleOrder.noteAr;
                    this.randerTerms++;
                    this.sale.note = datasaleOrder.noteDescription;
                    this.sale.warrantyLogoPath = datasaleOrder.warrantyLogoPath;
                    this.sale.attachmentList = datasaleOrder.attachmentList;
                    this.sale.inquiryId = datasaleOrder.inquiryId;
                    this.sale.purchaseOrderId = datasaleOrder.purchaseOrderId;
                    this.sale.inquiryId = datasaleOrder.inquiryId;
                    this.sale.billingId = datasaleOrder.billingId;
                    this.sale.deliveryChallanId = datasaleOrder.deliveryChallanId;
                    this.sale.saleOrderId = datasaleOrder.saleOrderId;
                    this.sale.quotationId = datasaleOrder.quotationId;
                    this.sale.doctorName = datasaleOrder.doctorName;
                    this.sale.hospitalName = datasaleOrder.hospitalName;
                    this.sale.logisticId = datasaleOrder.logisticId;
                    this.sale.taxMethod = datasaleOrder.taxMethod;
                    this.sale.isButtonDisabled = false;
                    this.sale.discount = datasaleOrder.discount;
                    this.sale.isDiscountOnTransaction = datasaleOrder.isDiscountOnTransaction;
                    this.sale.isFixed = datasaleOrder.isFixed;
                    this.sale.saleOrderNo = datasaleOrder.saleOrderNoAndDate;
                    this.sale.peroformaInvoiceNo = datasaleOrder.peroformaInvoiceNo;
                    this.sale.purchaseOrderNo = datasaleOrder.purchaseOrderNo;
                    this.sale.inquiryNo = datasaleOrder.inquiryNoAndDate;
                    this.sale.quotationNo = datasaleOrder.quotationNoAndDate;
                    this.sale.referredBy = datasaleOrder.referredBy;
                    this.sale.deliveryDate = datasaleOrder.deliveryDate;
                    this.sale.isBeforeTax = datasaleOrder.isBeforeTax;
                    this.sale.deliveryNoteAndDate = datasaleOrder.deliveryNoteAndDate;
                    this.sale.perfomaValidUpto = datasaleOrder.perfomaValidUpto;
                    this.sale.dispatchDate = datasaleOrder.dispatchDate;
                    this.sale.pickupDate = datasaleOrder.pickUpDate;

                    this.sale.quotationValidUpto = datasaleOrder.quotationValidUpto;
                    this.sale.transactionLevelDiscount = datasaleOrder.transactionLevelDiscount;
                    // this.custemerOldId = datasaleOrder.customerId;
                    // this.employeeDescription= datasaleOrder.employeeName;
                    this.logisticDescription = datasaleOrder.logisticNameEn + ' ' + datasaleOrder.logisticNameAr;
                    this.discountTypeOption = datasaleOrder.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                    this.sale.taxRateId = datasaleOrder.taxRateId;
                    this.sale.customerInquiry = datasaleOrder.customerInquiry;
                    this.sale.poNumber = datasaleOrder.poNumber;

                    this.sale.refrenceNo = datasaleOrder.refrenceNo;
                    this.sale.clientPurchaseNo = datasaleOrder.clientPurchaseNo;
                    this.sale.validityDate = datasaleOrder.validityDate;
                    this.sale.purpose = datasaleOrder.purpose;

                    this.sale.poDate = datasaleOrder.poDate;
                    this.adjustmentSignProp = datasaleOrder.discount >= 0 ? '+' : '-';
                    this.sale.salePayment = {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentName: 'Cash',
                        paymentOptionId: '',
                    };
                    //For Record
                    this.record2.saleOrderNo = this.sale.saleOrderNo;
                    this.record2.purchaseOrderNo = this.sale.purchaseOrderNo;
                    this.record2.inquiryNo = this.sale.inquiryNo;
                    this.record2.deliveryNoteAndDate = this.sale.deliveryNoteAndDate;
                    this.record2.quotationNo = this.sale.quotationNo;
                    this.record2.quotationValidUpto = this.sale.quotationValidUpto;
                    this.record2.peroformaInvoiceNo = this.sale.peroformaInvoiceNo;
                    this.record2.perfomaValidUpto = this.sale.perfomaValidUpto;
                    this.record2.pickupDate = this.sale.pickupDate;
                    this.record2.dispatchDate = this.sale.dispatchDate;

                    this.record2.dispatchNote = this.sale.dispatchNote;
                    //Record 2

                    const properties = Object.values(this.record2);
                    if (properties.some(property => property != '' && property != null)) {
                        root.isUpdateRecord2 = true;

                    } else {
                        root.isUpdateRecord2 = false;

                    }

                    this.record1.dispatchDate = this.sale.dispatchDate;
                    this.record1.pickupDate = this.sale.pickupDate;
                    this.record1.dueDate = this.sale.dueDate;
                    this.record1.description = this.sale.description;
                    this.record1.referredBy = this.sale.referredBy;
                    this.record1.employeeId = this.sale.employeeId;
                    this.record1.validityDate = this.sale.validityDate;
                    this.record1.deliveryDate = this.sale.deliveryDate;
                    this.record1.logisticId = this.sale.logisticId;
                    this.record1.areaId = this.sale.areaId;
                    this.record1.refrenceNo = this.sale.refrenceNo;
                    this.record1.clientPurchaseNo = this.sale.clientPurchaseNo;
                    this.record1.customerInquiry = this.sale.customerInquiry;
                    this.record1.doctorName = this.sale.doctorName;
                    this.record1.hospitalName = this.sale.hospitalName;
                    this.record1.poNumber = this.sale.poNumber;
                    this.record1.poDate = this.sale.poDate;

                    const properties67 = Object.values(this.record1);
                    if (properties67.some(property => property != '' && property != null)) {
                        root.isUpdateRecord1 = true;

                    } else {
                        root.isUpdateRecord1 = false;

                    }

                    if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                        this.sale.saleItems.forEach(function (x) {

                            x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                            x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                            x.unitPerPack = x.product.unitPerPack;
                        });
                    }
                    if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                        this.sale.saleItems.forEach(function (x) {

                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                            x.unitPerPack = x.product.unitPerPack;
                        });
                    }
                }

            }
            else {

                if (localStorage.getItem('IsCashCustomer') == 'true') {
                    this.sale.isCredit = false
                }
                else {
                    if (localStorage.getItem('IsSaleCredit') != 'true') {
                        // this.GetCashCustomer();
                        this.sale.isCredit = false;
                    }
                    else {
                        this.sale.isCredit = true;
                        this.paymentMethod = 'Credit';
                    }
                }

                if (!this.sale.isCredit) {
                    if (this.sale.bankCashAccountId == '') {
                        if (localStorage.getItem('CashAccountId') != null && localStorage.getItem('CashAccountId') != undefined && localStorage.getItem('CashAccountId') != '' && localStorage.getItem('CashAccountId') != 'null' && localStorage.getItem('CashAccountId') != "null" && localStorage.getItem('CashAccountId') != "00000000-0000-0000-0000-000000000000" && localStorage.getItem('CashAccountId') != '00000000-0000-0000-0000-000000000000') {

                            this.sale.bankCashAccountId = localStorage.getItem('CashAccountId');
                        }
                    }
                }
                if (this.$route.query.data != undefined) {
                    this.customerAccountId = this.$route.query.data.customerAccountId;

                    var data = this.$route.query.data;
                    if (!data.isDuplicate) {
                        this.sale.id = data.id;
                        this.sale.saleOrderId = data.saleOrderId;
                        this.sale.registrationNo = data.registrationNo;
                    }
                    else {
                        this.AutoIncrementCode();
                        this.controlRequest++;
                    }
                    if (data.invoiceType == 4) {
                        this.sale.proformaInvoiceId = this.sale.id;
                        this.sale.id = '00000000-0000-0000-0000-000000000000';
                        this.sale.registrationNo = data.registrationNo;
                    }
                    this.sale.isEditPaidInvoice = this.$route.query.isEditPaidInvoice == 'true' ? true : false;
                    this.sale.clone = this.$route.query.clone == 'true' ? true : false;

                    this.sale.date = data.date;
                    this.sale.isDuplicate = data.isDuplicate;
                    this.prefix = data.prefixiesLookupModel;

                    this.sale.invoiceType = data.invoiceType;
                    this.sale.deliveryNoteAndDate = data.deliveryNoteAndDate;
                    this.sale.perfomaValidUpto = data.perfomaValidUpto;
                    this.sale.dispatchDate = data.dispatchDate;
                    this.sale.pickupDate = data.pickUpDate;
                    this.sale.quotationValidUpto = data.quotationValidUpto;

                    this.sale.billingId = data.billingId;
                    this.sale.cashCustomer = data.cashCustomer;
                    this.sale.dueDate = data.dueDate;
                    this.sale.mobile = data.mobile;
                    this.sale.email = data.email;
                    // this.sale.cashCustomerId = data.cashCustomerId;
                    this.sale.customerAddressWalkIn = data.customerAddressWalkIn;
                    this.sale.code = data.code;
                    this.sale.areaId = data.areaId;
                    this.sale.employeeId = data.employeeId;
                    this.sale.isCredit = data.isCredit;
                    this.paymentMethod = data.isCredit ? 'Credit' : 'Cash';
                    this.sale.customerId = data.customerId;
                    this.sale.wareHouseId = data.wareHouseId;
                    this.sale.description = data.description;
                    this.sale.deliveryChallanId = data.deliveryChallanId;





                    this.sale.saleItems = data.saleItems;



                    this.sale.termAndCondition = data.termAndCondition;
                    this.sale.termAndConditionAr = data.termAndConditionAr;
                    this.randerTerms++;

                    this.sale.warrantyLogoPath = data.warrantyLogoPath;
                    this.sale.note = data.note;
                    this.sale.attachmentList = data.attachmentList;
                    this.sale.inquiryId = data.inquiryId;
                    this.sale.purchaseOrderId = data.purchaseOrderId;
                    this.sale.inquiryId = data.inquiryId;
                    this.sale.deliveryChallanId = data.deliveryChallanId;
                    this.sale.saleOrderId = data.saleOrderId;
                    this.sale.quotationId = data.quotationId;
                    this.sale.proformaId = data.proformaId;
                    this.sale.doctorName = data.doctorName;
                    this.sale.hospitalName = data.hospitalName;
                    this.sale.logisticId = data.logisticId;
                    this.sale.taxMethod = data.taxMethod;
                    this.sale.isButtonDisabled = false;
                    this.sale.discount = data.discount;
                    this.sale.isDiscountOnTransaction = data.isDiscountOnTransaction;
                    this.sale.isFixed = data.isFixed;
                    this.sale.saleOrderNo = data.saleOrderNoAndDate;
                    this.sale.peroformaInvoiceNo = data.peroformaInvoiceNo;
                    this.sale.purchaseOrderNo = data.purchaseOrderNo;
                    this.sale.inquiryNo = data.inquiryNoAndDate;
                    this.sale.quotationNo = data.quotationNoAndDate;
                    this.sale.referredBy = data.referredBy;
                    this.sale.deliveryDate = data.deliveryDate;
                    this.sale.isBeforeTax = data.isBeforeTax;
                    this.sale.transactionLevelDiscount = data.transactionLevelDiscount;
                    this.custemerOldId = data.customerId;
                    this.employeeDescription = data.employeeName;
                    this.logisticDescription = data.logisticNameEn + ' ' + data.logisticNameAr;
                    this.discountTypeOption = data.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                    this.sale.taxRateId = data.taxRateId;
                    this.sale.customerInquiry = data.customerInquiry;
                    this.sale.poNumber = data.poNumber;
                    this.sale.poDate = data.poDate;
                    this.sale.refrenceNo = data.refrenceNo;
                    this.adjustmentSignProp = data.discount >= 0 ? '+' : '-';

                    //For Record
                    this.record2.saleOrderNo = this.sale.saleOrderNo;
                    this.record2.purchaseOrderNo = this.sale.purchaseOrderNo;
                    this.record2.inquiryNo = this.sale.inquiryNo;
                    this.record2.deliveryNoteAndDate = this.sale.deliveryNoteAndDate;
                    this.record2.quotationNo = this.sale.quotationNo;
                    this.record2.quotationValidUpto = this.sale.quotationValidUpto;
                    this.record2.peroformaInvoiceNo = this.sale.peroformaInvoiceNo;
                    this.record2.perfomaValidUpto = this.sale.perfomaValidUpto;
                    this.record2.pickupDate = this.sale.pickupDate;
                    this.record2.dispatchDate = this.sale.dispatchDate;
                    this.record2.dispatchNote = this.sale.dispatchNote;
                    //Record 2
                   

                    const properties = Object.values(this.record2);
                    if (properties.some(property => property != '' && property != null)) {
                        root.isUpdateRecord2 = true;

                    } else {
                        root.isUpdateRecord2 = false;

                    }
                    this.record1.dispatchDate = this.sale.dispatchDate;
                    this.record1.pickupDate = this.sale.pickupDate;
                    this.record1.dueDate = this.sale.dueDate;
                    this.record1.description = this.sale.description;
                    this.record1.referredBy = this.sale.referredBy;
                    this.record1.employeeId = this.sale.employeeId;
                    this.record1.validityDate = this.sale.validityDate;
                    this.record1.deliveryDate = this.sale.deliveryDate;
                    this.record1.logisticId = this.sale.logisticId;
                    this.record1.areaId = this.sale.areaId;
                    this.record1.refrenceNo = this.sale.refrenceNo;
                    this.record1.clientPurchaseNo = this.sale.clientPurchaseNo;
                    this.record1.customerInquiry = this.sale.customerInquiry;
                    this.record1.doctorName = this.sale.doctorName;
                    this.record1.hospitalName = this.sale.hospitalName;
                    this.record1.poNumber = this.sale.poNumber;
                    this.record1.poDate = this.sale.poDate;
                    const properties89 = Object.values(this.record1);
                    if (properties89.some(property => property != '' && property != null)) {
                        root.isUpdateRecord1 = true;

                    } else {
                        root.isUpdateRecord1 = false;

                    }

                    if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                        this.sale.saleItems.forEach(function (x) {

                            x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                            x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                            x.unitPerPack = x.product.unitPerPack;
                        });
                    }
                    if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                        this.sale.saleItems.forEach(function (x) {

                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                            x.unitPerPack = x.product.unitPerPack;
                        });
                    }

                    this.sale.salePayment = {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentName: 'Cash',
                        paymentOptionId: '',
                    };
                }
                else {
                    this.sale.taxRateId = localStorage.getItem('TaxRateId');
                    this.sale.taxMethod = localStorage.getItem('taxMethod');
                    this.discountTypeOption = localStorage.getItem('DiscountTypeOption');
                    this.sale.isDiscountOnTransaction = localStorage.getItem('DiscountTypeOption') === 'At Transaction Level' ? true : false;
                }
                if (this.isValid('CashInvoicesForCustomer') && !this.isValid('CashInvoicesForWalkIn')) {

                    this.sale.isCashInvoicesForCustomer = true;
                }

            }

            // this.GetHeaderDetail();

            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
            this.GetUserDefineFlow();
            this.$emit('update:modelValue', this.$route.name);

        },

        mounted: function () {
            this.isSalePrice = localStorage.getItem('IsSalePrice') == 'true' ? true : false;
            this.isSalePriceLabel = localStorage.getItem('IsSalePriceLabel') == 'true' ? true : false;
            this.isCustomerPrice = localStorage.getItem('IsCustomerPrice') == 'true' ? true : false;
            this.isCustomerPriceLabel = localStorage.getItem('IsCustomerPriceLabel') == 'true' ? true : false;

            this.priceType = this.priceoptions.find(option => option.id === 1);

            this.isService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
            this.sale.isService = this.isService;

            this.sale.wareHouseId = localStorage.getItem('WareHouseId');
            this.sale.allowPreviousFinancialPeriod = localStorage.getItem('AllowPreviousFinancialPeriod') == 'true' ? true : false;

            this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
            this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;
            this.IsPaymentOptions = localStorage.getItem('PaymentOptions') == 'true' ? true : false;

            this.BusinessLogo = localStorage.getItem('BusinessLogo');
            this.BusinessNameArabic = localStorage.getItem('BusinessNameArabic');
            this.BusinessNameEnglish = localStorage.getItem('BusinessNameEnglish');
            this.BusinessTypeArabic = localStorage.getItem('BusinessTypeArabic');
            this.BusinessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
            this.CompanyNameArabic = localStorage.getItem('CompanyNameArabic');
            this.CompanyNameEnglish = localStorage.getItem('CompanyNameEnglish');
            this.saleOrderPerm = localStorage.getItem('saleOrderPerm') == 'true' ? true : false;
            this.autoPaymentVoucher = localStorage.getItem('AutoPaymentVoucher') == 'true' ? true : false;
            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;
            this.CompanyID = localStorage.getItem('CompanyID');

            this.language = this.$i18n.locale;
            var IsDayStart = localStorage.getItem('DayStart');
            this.sale.dayStart = IsDayStart == 'false' ? false : true;
            var IsExpenseDay = localStorage.getItem('IsExpenseDay');
            var IsDayStartOn = localStorage.getItem('IsDayStart');
            this.sale.counterId = localStorage.getItem('CounterId');
            this.headerFooter.returnDays = localStorage.getItem('ReturnDays');
            this.isForMedical = localStorage.getItem('isForMedical');
            this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            this.headerFooter.printSizeA4 = localStorage.getItem('PrintSizeA4');
            this.printTemplate = localStorage.getItem('PrintTemplate');
            this.printSize = localStorage.getItem('PrintSizeA4');

            var root = this;
            if (this.formName == 'ServiceSaleOrder' || this.formName == 'ServiceQuotation') {
                if (this.$route.query.data == undefined) {
                    this.AutoIncrementCode();

                    this.sale.date = moment().format('llll');

                }

            }
            else {
                if (IsDayStart != 'true') {
                    this.isDayAlreadyStart = true;
                    this.language = this.$i18n.locale;
                    this.getBank();

                    this.getCurrency();
                    this.isArea = localStorage.getItem('IsArea');
                    this.currency = localStorage.getItem('currency');
                    this.isMultiUnit = 'false';
                    this.isRaw = localStorage.getItem('IsProduction');
                    if (this.$route.query.data == undefined) {
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {

                            if (root.isValid('CashInvoices')) {
                                root.isCredit(root.toggleCash);
                                root.sale.cashCustomer = "Walk-In";
                            } else if (root.isValid('CreditInvoices')) {
                                root.isCredit(root.toggleCredit);
                                //root.paymentMethod = 'Credit';
                            }
                        } else {
                            if (root.isValid('CashInvoices')) {
                                //root.paymentMethod = 'Cash';
                                root.isCredit(root.toggleCash);
                                root.sale.cashCustomer = "ادخل";
                            } else if (root.isValid('CreditInvoices')) {
                                root.isCredit(root.toggleCredit);
                                //root.paymentMethod = 'Credit';
                            }
                        }
                        this.AutoIncrementCode();

                    }
                    this.sale.date = moment().format('LLL');

                    // if (this.$route.query.data != undefined) {
                    //     this.warehouse = this.$route.query.data;
                    // }

                    // if (this.$route.query.mobiledata != undefined) {
                    //     this.sale.date = this.$route.query.mobiledata.orderDate;
                    // }
                } else {

                    if (IsExpenseDay == 'true') {
                        this.isDayAlreadyStart = false;
                    } else {
                        this.UserID = localStorage.getItem('UserID');
                        this.employeeId = localStorage.getItem('EmployeeId');
                        if (IsDayStartOn == 'true') {

                            this.isDayAlreadyStart = true;
                            root.getBank();

                            root.getCurrency();
                            root.isArea = localStorage.getItem('IsArea');
                            root.currency = localStorage.getItem('currency');
                            root.isMultiUnit = 'false';
                            root.isRaw = localStorage.getItem('IsProduction');
                            if (root.$route.query.data == undefined) {
                                if ((root.$i18n.locale == 'en' || root.isLeftToRight())) {
                                    if (root.isValid('CashInvoices')) {
                                        root.isCredit(root.toggleCash);
                                        root.sale.cashCustomer = "Walk-In";
                                    } else if (root.isValid('CreditInvoices')) {
                                        root.isCredit(root.toggleCredit);
                                        //root.paymentMethod = 'Credit';
                                    }
                                    //this.paymentMethod = 'Cash';
                                    //root.sale.cashCustomer = "Walk-In";
                                } else {
                                    if (root.isValid('CashInvoices')) {
                                        //root.paymentMethod = 'Cash';
                                        root.isCredit(root.toggleCash);
                                        root.sale.cashCustomer = "ادخل";
                                    } else if (root.isValid('CreditInvoices')) {
                                        root.isCredit(root.toggleCredit);
                                        //root.paymentMethod = 'Credit';
                                    }
                                    //root.sale.cashCustomer = "ادخل";
                                }
                                root.AutoIncrementCode();

                            }
                            root.sale.date = moment().format('LLL');
                            if (root.$route.query.data != undefined) {
                                root.warehouse = root.$route.query.data;
                            }

                            if (root.$route.query.mobiledata != undefined) {
                                root.sale.date = root.$route.query.mobiledata.orderDate;
                            }
                        } else {
                            this.isDayAlreadyStart = false;
                            this.AutoIncrementCode();
                        }
                    }

                }

            }
            let randomNumber = '';
            for (let i = 0; i < 11; i++) {
                randomNumber += Math.floor(Math.random() * 10);
            }
            this.sale.barCode = randomNumber;
            // this.GetHeaderDetail();

            if (this.$route.query.id != undefined) {
                if (this.$route.query.document == "ServiceQuotation") {
                    this.GetSaleOrderDetail(this.$route.query.id, false, '', 'ConvertToInvoice');


                }
                else if (this.$route.query.document == "ServiceSaleOrder") {
                    this.GetSaleOrderDetail(this.$route.query.id, true, '', 'ConvertToInvoice');

                }
                else if (this.$route.query.document == "PurchaseOrder") {
                   
                    var details = {
                        date : this.$route.query.id.date,
                        id : this.$route.query.id.id,
                        name : this.$route.query.id.registrationNumber + '--' + this.getDate(this.$route.query.id.date) + '--' + this.$route.query.id.netAmount,
                        netAmount : this.$route.query.id.netAmount,
                        registrationNumber: this.$route.query.id.registrationNumber
                    }
                    this.GetSaleRecord(this.$route.query.id.id,details,'ConvertToInvoice','purchaseOrder');

                }


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
    }
</style>
