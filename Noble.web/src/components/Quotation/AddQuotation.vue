<template>
    <div class="row" v-if="isValid('CanEditQuotation') || isValid('CanDraftQuotation') || isValid('CanAddQuotation')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">{{
                                        $t('AddQuotation.AddQuotation')
                                }}</h4>
                                <h4 v-else class="page-title">{{ $t('AddQuotation.UpdateQuotation') }}</h4>
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
                    <div class="row form-group" v-bind:key="randerCustomer">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Customer') }} :
                                <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown v-model="v$.purchase.customerId.$model"
                                :paymentTerm="purchase.paymentMethod" ref="CustomerDropdown"
                                :modelValue="purchase.customerId" :key="randerCustomer" />
                            <a href="javascript:void(0);" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight"
                                aria-controls="offcanvasRight" class="text-primary">{{
                                        $t('AddQuotation.ViewCustomerDetails')
                                }}</a>
                            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                                aria-labelledby="offcanvasRightLabel">
                                <div class="offcanvas-header">
                                    <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddQuotation.CustomerDetails') }}
                                    </h5>
                                    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                                        aria-label="Close"></button>
                                </div>
                                <div class="offcanvas-body">
                                    <div class="row">
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddSaleOrder.Mobile') }} :</label>
                                            <input type="text" class="form-control" v-model="purchase.mobile" />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('CustomerAddress') }} :</label>
                                            <textarea rows="3" v-model="purchase.customerAddress"
                                                class="form-control"> </textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- <fieldset class="form-group">
                        <div class="row">
                            <div class="col-form-label col-lg-4 pt-0">
                                <span id="ember694" class="tooltip-container text-dashed-underline "> {{
                                        $t('AddQuotation.PaymentMethod')
                                }} : <span class="text-danger">*</span></span>
                            </div>
                            <div class="col-lg-8">
                                <div class="form-check form-check-inline">
                                    <input v-model="purchase.paymentMethod" name="contact-sub-type" id="a49946497"
                                        class=" form-check-input" type="radio" value="Cash">
                                    <label class="form-check-label pl-0" for="a49946497"
                                        v-if="($i18n.locale == 'en' || isLeftToRight())">Cash</label>
                                    <label class="form-check-label pl-0" for="a49946497" v-else>الـنـقـدي</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input v-model="purchase.paymentMethod" name="contact-sub-type" id="a9ff8eb35"
                                        class=" form-check-input" type="radio" value="Credit">
                                    <label class="form-check-label pl-0" for="a9ff8eb35"
                                        v-if="($i18n.locale == 'en' || isLeftToRight())">Credit</label>
                                    <label class="form-check-label pl-0" for="a9ff8eb35" v-else>آجـل</label>
                                </div>
                            </div>
                        </div>
                    </fieldset> -->

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">{{
                                    $t('AddQuotation.Quotation')
                            }} # <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.registrationNo" disabled class="form-control" type="text">
                        </div>
                    </div>

                    <!-- <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> Attn:</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.attn" class="form-control" type="text">
                        </div>
                    </div> -->
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddQuotation.Refrence')
                            }}#</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.refrence" class="form-control" type="text">
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.For') }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.for" class="form-control" type="text">
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.Type') }}
                                :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-model="purchase.purpose" :options="[$t('AddQuotation.Quotation'), $t('AddQuotation.Proposal')]"
                                @update:modelValue="AutoIncrementCode(purchase.purpose)" :show-labels="false"
                                v-bind:placeholder="$t('AddQuotation.SelectOption')">
                            </multiselect>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddQuotation.ClientPurchaseNo')
                            }}:</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.clientPurchaseNo" class="form-control" type="text">
                        </div>
                    </div>


                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.OrderType') }}
                                :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <importexportdropdown v-model="purchase.orderTypeId" :modelValue="purchase.orderTypeId"
                                :formName="'OrderType'" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.Incoterms') }}
                                :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <importexportdropdown v-model="purchase.incotermsId" :modelValue="purchase.incotermsId"
                                :formName="'Incoterms'" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.Commodity') }}
                                :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.commodities" class="form-control" type="text">
                        </div>
                    </div>
                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.NatureofCargo')
                            }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                v-model="purchase.natureOfCargo" :options="['Dangerious', 'Non Dangerious']"
                                :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                            </multiselect>
                            <multiselect v-else v-model="purchase.natureOfCargo" :options="['خطير', 'غير خطير']"
                                :show-labels="false" v-bind:placeholder="$t('AddQuotation.SelectOption')">
                            </multiselect>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddQuotation.ValidityDate') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.validityDate" />
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.Date') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.date" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddQuotation.Template') }}
                                :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <QuotationTemplateDropdown
                                @update:modelValue="GetQuotationTemplateDetail(purchase.quotationTemplateId)"
                                v-model="purchase.quotationTemplateId" :modelValue="purchase.quotationId"
                                :isservice="false" />
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
                                v-bind:disabled="purchase.saleOrderItems.length > 0" @click="purchase.isFixed = false"
                                v-model="purchase.taxMethod" :show-labels="false"
                                v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                            <multiselect v-else :options="['شامل', 'غير شامل']"
                                v-bind:disabled="purchase.saleOrderItems.length > 0" v-model="purchase.taxMethod"
                                @select="purchase.isFixed = false" :show-labels="false"
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
                                :isDisable="purchase.saleOrderItems.length > 0 ? true : false" :key="randerCustomer" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.DiscountType') }}
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="['At Transaction Level', 'At Line Item Level']"
                                v-bind:disabled="purchase.saleOrderItems.length > 0" v-model="discountTypeOption"
                                @select="purchase.isDiscountOnTransaction = (discountTypeOption === 'At Transaction Level' ? false : true)"
                                :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group"
                        v-if="purchase.paymentMethod == 'Cash' || purchase.paymentMethod == 'السيولة النقدية'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddQuotation.SheduleDelivery')
                            }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())"
                                v-model="purchase.sheduleDelivery" :options="['Advance', 'After Delivery']"
                                :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                            </multiselect>
                            <multiselect v-else v-model="purchase.sheduleDelivery" :options="['تقدم', 'بعد الولادة']"
                                :show-labels="false" v-bind:placeholder="$t('AddQuotation.SelectOption')">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group"
                        v-if="purchase.paymentMethod == 'Cash' || purchase.paymentMethod == 'السيولة النقدية'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddQuotation.Days') }}
                                :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" type="text" v-model="purchase.days" />
                        </div>
                    </div>

                    <!-- <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="category.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddCategory.Active') }} </label>
                        </div>
                    </div> -->

                    <div class="row form-group"
                        v-if="purchase.paymentMethod == 'Cash' || purchase.paymentMethod == 'السيولة النقدية'">
                        <label class="col-form-label col-lg-4">

                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="purchase.isFreight">
                                <label for="inlineCheckbox1"> {{ $t('AddQuotation.Fregiht') }} </label>
                            </div>
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox2" v-model="purchase.isLabour">
                                <label for="inlineCheckbox2"> {{ $t('AddQuotation.Labour') }} </label>
                            </div>

                        </div>
                    </div>


                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{
                                    $t('AddQuotation.QuotationValidtill')
                            }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.quotationValidDate" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.FreeTimeatPOL')
                            }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.freeTimePOL" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="importExportSale">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddQuotation.FreeTimeatPOD')
                            }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.freeTimePOD" />
                        </div>
                    </div>

                </div>



                <div class="row" v-if="importExportSale">
                    <div class="col-lg-12">
                        <div class=" mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="width: 30px;" class="text-center">
                                            #
                                        </th>
                                        <th style="width: 120px;" class="text-center">
                                            {{ $t('AddQuotation.Service') }}
                                        </th>
                                        <th style="width: 120px;" class="text-center">
                                            {{ $t('AddQuotation.StuffingLocation') }}
                                        </th>
                                        <th class="text-center" style="width: 120px;">
                                            POL
                                        </th>
                                        <th style="width: 120px;" class="text-center">
                                            POD
                                        </th>
                                        <th style="width: 120px;" class="text-center">
                                            {{ $t('AddQuotation.Carrier') }}
                                        </th>
                                        <th style="width: 50px;" class="text-center">
                                            20'FT
                                        </th>
                                        <th style="width: 50px;" class="text-center">
                                            40'HC
                                        </th>
                                        <th style="width: 50px;" class="text-center">
                                            T.T
                                        </th>
                                        <th style="width: 50px;" class="text-center">
                                            ETD
                                        </th>
                                        <th style="width: 40px"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(prod, index) in purchase.importExportItems" v-bind:key="prod.rowId">
                                        <td class="border-top-0 pl-1 pr-1">
                                            {{ index + 1 }}
                                        </td>
                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="prod.serviceId" :modelValue="prod.serviceId"
                                                :formName="'Service'" />
                                        </td>

                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="prod.stuffingLocationId"
                                                :modelValue="prod.stuffingLocationId" :formName="'StuffingLocation'" />
                                        </td>
                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="prod.portOfLoadingId"
                                                :modelValue="prod.portOfLoadingId" :formName="'PortOfLoading'" />
                                        </td>
                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="prod.portOfDestinationId"
                                                :modelValue="prod.portOfDestinationId" :formName="'PortOfDestination'" />
                                        </td>

                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="prod.carrierId" :modelValue="prod.carrierId"
                                                :formName="'Carrier'" />
                                        </td>

                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="prod.ft" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>

                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="prod.hc" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>

                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="prod.tt" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>
                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="prod.etd" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>

                                        <td class="border-top-0 pt-0 pl-1 pr-1">
                                            <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i
                                                    class="las la-trash-alt text-secondary font-16"></i></a>

                                        </td>
                                    </tr>

                                    <tr :key="itemRender">
                                        <td class="border-top-0 pl-1 pr-1">

                                        </td>
                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="serviceId" :formName="'Service'" />
                                        </td>

                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="stuffingLocationId"
                                                :formName="'StuffingLocation'" />
                                        </td>
                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="portOfLoadingId"
                                                :formName="'PortOfLoading'" />
                                        </td>
                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="portOfDestinationId"
                                                :formName="'PortOfDestination'" />
                                        </td>

                                        <td class="border-top-0 text-center pl-1 pr-1">
                                            <importexportdropdown v-model="carrierId" :formName="'Carrier'" />
                                        </td>

                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="ft" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>

                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="hc" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>

                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="tt" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>
                                        <td class="border-top-0 pl-1 pr-1">
                                            <input type="number" v-model="etd" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>

                                        <td class="border-top-0 pt-0 pl-1 pr-1">
                                            <button @click="addProduct()" title="Remove Item"
                                                v-bind:disabled="isAddProductValid"
                                                class="btn btn-primary btn-round btn-sm  btn-icon mt-2">
                                                <i class="fa fa-plus"></i>
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>


                <br />


                <quotation-item @update:modelValue="SavePurchaseItems" :key="rander" :taxMethod="purchase.taxMethod"
                    :taxRateId="purchase.taxRateId" @discountChanging="updateDiscountChanging"
                    :adjustmentProp="purchase.discount" :adjustmentSignProp="adjustmentSignProp"
                    :isDiscountOnTransaction="purchase.isDiscountOnTransaction"
                    :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :newisFixed="purchase.isFixed"
                    :newisBeforeTax="purchase.isBeforeTax" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items" v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">

                        <button class="btn btn-outline-primary  mr-2" v-on:click="savePurchase('Draft')"
                            v-if="isValid('CanDraftQuotation')"
                            :disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity == '' && x.highQty == '').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddQuotation.SaveAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary  mr-2" v-on:click="savePurchase('Approved')"
                            v-if="isValid('CanAddQuotation')"
                            :disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity == '' && x.highQty == '').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddQuotation.SaveAsPost') }}
                        </button>



                        <button class="btn btn-danger  mr-2" v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>
                    </div>
                    <div class="button-items" v-else>
                        <button class="btn btn-outline-primary   mr-2" v-on:click="savePurchase('Draft')"
                            v-if="isValid('CanDraftQuotation') && isValid('CanEditQuotation')"
                            :disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity == '' && x.highQty == '').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddQuotation.UpdateAsDraft') }}
                        </button>

                        <button class="btn btn-outline-primary   mr-2" v-on:click="savePurchase('Approved')"
                            v-if="isValid('CanAddQuotation') && isValid('CanEditQuotation')"
                            :disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity == '' && x.highQty == '').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddQuotation.UpdateAsPost') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>
                    </div>
                </div>

                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                    <div class="form-group pe-3">
                                        <label>{{ $t('AddQuotation.TermandCondition') }}:</label>
                                        <textarea class="form-control " rows="3" v-model="purchase.note" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3">
                                        <div class="font-xs mb-1">{{ $t('AddQuotation.Attachment') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                            class="btn btn-light btn-square btn-outline-dashed mb-1"><i
                                                class="fas fa-cloud-upload-alt"></i> {{ $t('AddQuotation.Attachment') }}
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

            </div>
        </div>




        <!-- Demo Start Code Here -->


        <!-- Demo End Code Here -->
        <bulk-attachment :attachmentList="purchase.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'

import moment from "moment";

import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
import Multiselect from 'vue-multiselect';
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

//import Vue3Barcode from 'vue3-barcode'
export default {
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
            discountTypeOption: 'At Line Item Level',
            adjustmentSignProp: '+',

            randerCustomer: 0,
            daterander: 0,
            rander: 0,
            render: 0,
            purchase: {
                id: "00000000-0000-0000-0000-000000000000",
                date: "",
                registrationNo: "",
                customerId: "",
                refrence: "",
                days: '',
                validityDate:"",
                purpose: "Quotation",
                for: "",
                purchaseOrder: "",
                paymentMethod: "Cash",
                sheduleDelivery: "",
                note: '',
                isFreight: false,
                isLabour: false,
                isQuotation: true,
                saleOrderItems: [],
                attachmentList: [],
                path: '',
                clientPurchaseNo: '',
                terminalId: '',
                invoicePrefix: '',

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


                discount: 0,
                isDiscountOnTransaction: false,
                isFixed: false,
                isBeforeTax: true,
                transactionLevelDiscount: 0
            },
            loading: false,
            show: false,
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
                    date: { required },
                    registrationNo: { required },
                    customerId: { required },
                    refrence: {},

                    // paymentMethod: { required },

                    saleOrderItems: { required },
                },
                }
    },
    methods: {
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

                {
                    root.$https.get('/Purchase/QuotationTemplateDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {

                                response.data.quotationTemplateItems.forEach(function (so) {
                                    root.$refs.childComponentRef.addProduct(so.productId, so.product, so.quantity, so.unitPrice, true);

                                });

                                root.randerCustomer++;
                                root.logisticRender++;
                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }


            }
        },
        addProduct: function () {
            this.purchase.importExportItems.push({
                rowId: this.createUUID(),
                serviceId: this.serviceId,
                stuffingLocationId: this.stuffingLocationId,
                portOfLoadingId: this.portOfLoadingId,
                portOfDestinationId: this.portOfDestinationId,
                carrierId: this.carrierId,
                ft: this.ft,
                hc: this.hc,
                tt: this.tt,
                etd: this.etd,
            });

            this.serviceId = '';
            this.stuffingLocationId = '';
            this.portOfLoadingId = '';
            this.portOfDestinationId = '';
            this.carrierId = '';
            this.ft = '';
            this.hc = '';
            this.tt = '';
            this.etd = '';

            this.itemRender++;
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

        removeProduct: function (id) {

            this.purchase.importExportItems = this.purchase.importExportItems.filter((prod) => {
                return prod.rowId != id;
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

        AutoIncrementCode: function (quotationType) {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            var terminalId='';
           
           if(localStorage.getItem('TerminalId')!=null && localStorage.getItem('TerminalId')!=undefined && localStorage.getItem('TerminalId')!="null" && localStorage.getItem('TerminalId')!='null')
           {
               terminalId=localStorage.getItem('TerminalId');
           }
            root.$https
                .get("/Purchase/SaleOrderAutoGenerateNo?isQuotation=" + root.purchase.isQuotation + '&quotationType=' + quotationType + '&terminalId=' + terminalId+ '&invoicePrefix=' + localStorage.getItem('InvoicePrefix'), {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then(function (response) {
                    if (response.data != null) {
                        root.purchase.registrationNo = response.data;
                    }
                });
        },
        SavePurchaseItems: function (saleOrderItems, discount, adjustmentSignProp, transactionLevelDiscount) {

            this.purchase.saleOrderItems = saleOrderItems;
            this.purchase.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

            this.purchase.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
        },

        updateDiscountChanging: function (isFixed, isBeforeTax) {
            this.purchase.isFixed = isFixed
            this.purchase.isBeforeTax = isBeforeTax
        },
        savePurchase: function (status) {
            this.purchase.approvalStatus = status
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.purchase.saleOrderItems.forEach(function (x) {
                //x.quantity = x.totalPiece;
                x.highUnitPrice ? x.quantity = (x.highQty * x.unitPerPack) + x.quantity : x.quantity = x.totalPiece;
            });
            this.$https
                .post('/Purchase/SaveSaleOrderInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                .then(response => {
                    root.loading = false
                    root.info = response.data

                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                        type: 'success',
                        icon: 'success',
                        timer: 1500,
                        timerProgressBar: true,
                    }).then(function (response) {
                        if (response != undefined) {
                            if (root.purchase.id == "00000000-0000-0000-0000-000000000000") {
                                root.$router.go('AddQuotation');

                            } else {
                                if (root.isValid('CanViewQuotation') || root.isValid('CanDraftQuotation')) {
                                    root.$router.push('/Quotation');
                                }
                                else {
                                    root.$router.go();
                                }
                            }
                        }
                    });

                })
                .catch(error => {
                    console.log(error)
                    if (localStorage.getItem('IsMultiUnit') == 'true') {
                        root.purchase.saleOrderItems.forEach(function (x) {

                            x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                            x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));

                        });
                    }
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error,
                        });

                    root.loading = false
                })
                .finally(() => root.loading = false)

        },

        goToPurchase: function () {
            if (this.isValid('CanViewQuotation') || this.isValid('CanDraftQuotation')) {
                this.$router.push('/Quotation');
            }
            else {
                this.$router.go();
            }

        },
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
        this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
        this.purchase.date = moment().format('llll');
        var root = this;
        if (this.$route.query.data != undefined) {

            this.purchase = this.$route.query.data;

            if (root.purchase.importExportItems != null && root.purchase.importExportItems != undefined) {
                root.purchase.importExportItems.forEach(function (item) {
                    item.rowId = item.id
                });
            }


            this.purchase.date = moment(this.purchase.date).format('llll');
            if (localStorage.getItem('IsMultiUnit') == 'true') {

                this.purchase.saleOrderItems.forEach(function (x) {

                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                    x.unitPerPack = x.product.unitPerPack;
                });
            }
            this.attachment = true;
            this.rander++;
            this.render++;
            this.rendered++;
        }
        else {
            this.purchase.wareHouseId = localStorage.getItem('WareHouseId');
            this.purchase.taxRateId = localStorage.getItem('TaxRateId');
            this.purchase.taxMethod = localStorage.getItem('taxMethod');
        }

        this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
        this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'

        this.importExportSale = localStorage.getItem('ImportExportSale') == 'true' ? true : false;
    },
    mounted: function () {

        if (this.$route.query.data == undefined) {
            this.AutoIncrementCode('Quotation');

        }
    },
};
</script>
