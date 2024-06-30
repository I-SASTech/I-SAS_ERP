<template>
    <div class="row" v-if="isValid('CanAddServiceSaleOrder') || isValid('CanDraftServiceSaleOrder') || isValid('CanEditServiceSaleOrder') || isValid('CanDuplicateServiceSaleOrder')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">{{ $t('AddSaleOrder.AddServiceSaleOrder') }}</h4>
                                <h4 v-else class="page-title">{{ $t('AddSaleOrder.AddServiceSaleOrder') }}</h4>                                
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
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Customer') }} : <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown v-model="v$.purchase.customerId.$model" :paymentTerm="purchase.paymentMethod" @update:modelValue="getAddress" ref="CustomerDropdown" :modelValue="purchase.customerId" :key="customerRender" />
                            <a href="javascript:void(0);" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight" class="text-primary">{{ $t('AddSaleOrder.ViewCustomerDetails') }}</a>
                            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
                                <div class="offcanvas-header">
                                    <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddSaleOrder.CustomerDetails') }}</h5>
                                  <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                                </div>
                                <div class="offcanvas-body">
                                    <div class="row">
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddSaleOrder.Mobile') }}  :</label>
                                            <input type="text" class="form-control" v-model="purchase.mobile" />
                                        </div>
                                        
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddSaleOrder.CustomerAddress') }}  :</label>
                                            <textarea rows="3" v-model="purchase.customerAddress" class="form-control"> </textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--<fieldset class="form-group">
                        <div class="row">
                            <div class="col-form-label col-lg-4 pt-0">
                                <span id="ember694" class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.PaymentMethodForSo') }} : <span class="text-danger">*</span></span>
                            </div>
                            <div class="col-lg-8">
                                <div class="form-check form-check-inline">
                                    <input v-model="purchase.paymentMethod" name="contact-sub-type" id="a49946497" class=" form-check-input" type="radio" value="Cash">
                                    <label class="form-check-label pl-0" for="a49946497" v-if="($i18n.locale == 'en' ||isLeftToRight())">Cash</label>
                                    <label class="form-check-label pl-0" for="a49946497" v-else>الـنـقـدي</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input v-model="purchase.paymentMethod" name="contact-sub-type" id="a9ff8eb35" class=" form-check-input" type="radio" value="Credit">
                                    <label class="form-check-label pl-0" for="a9ff8eb35" v-if="($i18n.locale == 'en' ||isLeftToRight())">Credit</label>
                                    <label class="form-check-label pl-0" for="a9ff8eb35" v-else>آجـل</label>
                                </div>
                            </div>
                        </div>
                    </fieldset>-->

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.SaleOrder') }} # <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.registrationNo" disabled class="form-control" type="text">
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Refrence') }}#</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.refrence" class="form-control" type="text">
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.ClientPurchaseNo') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.clientPurchaseNo" class="form-control" type="text">
                        </div>
                    </div>

                    <!--<div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.WareHouse') }} :<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown :disable="true" v-model="v$.purchase.wareHouseId.$model" />
                        </div>
                    </div>-->

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Transporter/Cargo') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <logisticDropdown v-model="purchase.logisticId" :modelValue="purchase.logisticId" />
                        </div>
                    </div>  
                    
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.CustomerInquiry') }} #</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.DeliveryDate') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker></datepicker>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.DeliveryAddress') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddSaleOrder.BillingAddress') }} </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.OrderFor') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="purchase.description" class="form-control" />
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Date') }} </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.date" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="saleOrder && (isValid('CanDraftServiceQuotation') || isValid('CanViewServiceQuotation'))">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Quotation') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <quotationdropdown v-model="purchase.quotationId" v-bind:isservice="true" @update:modelValue="GetQuotationDetail(purchase.quotationId)" :modelValue="purchase.quotationId" />
                        </div>
                    </div>

                    

                    <div class="row form-group" v-if="(purchase.paymentMethod=='Cash' || purchase.paymentMethod=='السيولة النقدية') && isRaw=='true' ">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.SheduleDelivery') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="purchase.sheduleDelivery" :options="['Advance', 'After Delivery']" :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                            </multiselect>
                            <multiselect v-else v-model="purchase.sheduleDelivery" :options="['تقدم', 'بعد الولادة']" :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group" v-if="(purchase.sheduleDelivery=='After Delivery' || purchase.sheduleDelivery=='بعد الولادة') && (purchase.paymentMethod=='Cash' || purchase.paymentMethod=='السيولة النقدية') && isRaw=='true' ">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Days') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" v-model="purchase.days" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="(purchase.paymentMethod=='Cash' || purchase.paymentMethod=='السيولة النقدية') && isRaw=='true' ">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.DeliveryTerms') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="form-group">
                                <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="purchase.isFreight">
                                    <label for="inlineCheckbox1">{{ $t('AddSaleOrder.Fregiht') }} </label>
                                </div>
                                <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                    <input type="checkbox" id="inlineCheckbox2" v-model="purchase.isLabour">
                                    <label for="inlineCheckbox2">{{ $t('AddSaleOrder.Labour') }} </label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddSaleOrder.SalePerson') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddSaleOrder.ReferedBy') }} </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" />
                        </div>
                    </div>


                    <div class="row form-group" v-if="saleDefaultVat=='DefaultVatHead' || saleDefaultVat=='DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.TaxMethod') }} :<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" :options="['Inclusive', 'Exclusive']" v-bind:disabled="purchase.saleOrderItems.length>0" @click="purchase.isFixed = false" v-model="purchase.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                            <multiselect v-else :options="['شامل', 'غير شامل']" v-bind:disabled="purchase.saleOrderItems.length>0" v-model="purchase.taxMethod" @select="purchase.isFixed = false" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group" v-if="saleDefaultVat=='DefaultVatHead' || saleDefaultVat=='DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddPurchase.VAT%') }} :<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <taxratedropdown v-model="purchase.taxRateId" v-bind:modelValue="purchase.taxRateId" :isDisable="purchase.saleOrderItems.length>0? true :false" :key="customerRender" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddSaleOrder.DiscountType') }} </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="['At Transaction Level', 'At Line Item Level']" v-bind:disabled="purchase.saleOrderItems.length>0" v-model="discountTypeOption" @select="purchase.isDiscountOnTransaction = (discountTypeOption === 'At Transaction Level' ? false : true)" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSale.UploadWarrantyLogo') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div :key="renderImg">
                                <div class="input-group mb-3"
                                    v-if="!((imageSrc == '' && purchase.warrantyLogoPath != '') || (imageSrc != '' && purchase.warrantyLogoPath == '') || (imageSrc != '' && purchase.warrantyLogoPath != '')|| (imageSrc != '' && purchase.warrantyLogoPath != null))">
                                    <input ref="imgupload" type="file" class="form-control" id="inputGroupFile02"
                                        @change="uploadImage()" accept="image/*" name="image">
                                </div>
                                <div class="text-right " v-if="imageSrc != ''">
                                    <img v-if="imageSrc != ''" class="float-right" :src="imageSrc" width="200" height="150"/>
                                    
                                </div>
                                <div v-else class="mt-2"  >
                                    <span v-if="purchase.warrantyLogoPath!=null && purchase.warrantyLogoPath!=''">
                                        <img :src="'data:image/png;base64,' + purchase.warrantyLogoPath" width="200" height="150" />
                                    </span>
                                        
                                            
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group" v-if="imageSrc != '' || purchase.warrantyLogoPath != ''">
                        <label class="col-form-label col-lg-4">
                        </label>
                        <div class="inline-fields col-lg-8">
                            <button class="btn btn-danger  btn-sm mt-2" v-on:click="removeImage()">Remove</button>
                        </div>
                    </div>
                </div>

                <sale-service-item :saleItems="purchase.saleOrderItems" ref="childComponentRef" @update:modelValue="SavePurchaseItems" :wareHouseId="purchase.wareHouseId" :newtaxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId"  @discountChanging="updateDiscountChanging"  :adjustmentProp="purchase.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction" :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed" :isBeforeTax="purchase.isBeforeTax"/>

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items" v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">

                        <button class="btn btn-outline-primary  mr-2"
                                v-on:click="savePurchase('Draft')"
                                v-if="isValid('CanDraftServiceSaleOrder')"
                                v-bind:disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity=='').length > 0">
                            <i class="far fa-save"></i>  {{ $t('AddSaleOrder.SaveAsDraft') }}
                        </button>

                        <div class="btn-group"  v-if="isValid('CanAddServiceSaleOrder')">
                            <button v-on:click="savePurchase('Approved')"
                                    v-bind:disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity=='').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice=='').length > 0 || purchase.saleOrderItems.filter(x => x.outOfStock).length > 0"
                                    type="button" class="btn btn-outline-primary me-0">
                                {{ $t('AddSaleOrder.SaveAsPost') }}
                            </button>
                            <button v-bind:disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity=='').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice=='').length > 0  || purchase.saleOrderItems.filter(x => x.outOfStock).length > 0"
                                    type="button" class="btn btn-outline-primary dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="sr-only">Dropdown</span> <i class="mdi mdi-chevron-down"></i>
                            </button>
                            <div class="dropdown-menu">
                                <a v-on:click="savePurchase('Approved',true)" v-if="isValid('CanAddServiceSaleOrder') && isValid('CanPrintServiceSaleOrder') " class="dropdown-item" href="javascript:void(0);">{{ $t('AddSaleOrder.SaveasPostandPrint') }}</a>
                            </div>
                        </div>

                        <button class="btn btn-danger  mr-2"
                                v-on:click="goToPurchase">
                            {{ $t('AddSaleOrder.Cancel') }}
                        </button>
                    </div>
                    <div class="button-items" v-else>
                        <button class="btn btn-outline-primary  mr-2"
                                v-on:click="savePurchase('Draft')"
                                v-if="isValid('CanDraftServiceSaleOrder') && isValid('CanEditServiceSaleOrder')"
                                v-bind:disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity=='').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice=='').length > 0">
                            <i class="far fa-save"></i>  {{ $t('AddSaleOrder.UpdateAsDraft') }}
                        </button>

                        <div class="btn-group" v-if="isValid('CanAddServiceSaleOrder') && isValid('CanEditServiceSaleOrder')">
                            <button v-on:click="savePurchase('Approved')"
                                    v-bind:disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity=='').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice=='').length > 0 || purchase.saleOrderItems.filter(x => x.outOfStock).length > 0"
                                    type="button" class="btn btn-outline-primary me-0">
                                {{ $t('AddSaleOrder.UpdateAsPost') }}
                            </button>
                            <button v-bind:disabled="v$.$invalid || purchase.saleOrderItems.filter(x => x.quantity=='').length > 0 || purchase.saleOrderItems.filter(x => x.unitPrice=='').length > 0  || purchase.saleOrderItems.filter(x => x.outOfStock).length > 0"
                                    type="button" class="btn btn-outline-primary dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="sr-only">Dropdown</span> <i class="mdi mdi-chevron-down"></i>
                            </button>
                            <div class="dropdown-menu">
                                <a v-on:click="savePurchase('Approved',true)"  v-if="isValid('CanAddServiceSaleOrder') && isValid('CanEditServiceSaleOrder') && isValid('CanPrintServiceSaleOrder')" class="dropdown-item" href="javascript:void(0);">{{ $t('AddSaleOrder.UpdateAsPostandPrint') }}</a>
                            </div>
                        </div>

                        <button class="btn btn-danger  mr-2"
                                v-on:click="goToPurchase">
                            {{ $t('AddSaleOrder.Cancel') }}
                        </button>
                    </div>
                </div>



                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4" style="border-right: 1px solid #eee;">
                                    <div class="form-group pe-3">
                                        <label>{{ $t('AddSaleOrder.TermAndConditionEnglish') }}:</label>
                                        <textarea class="form-control " rows="3" v-model="purchase.termAndCondition" />
                                    </div>
                                </div>
                                <div class="col-lg-4" style="border-right: 1px solid #eee;">
                                    <div class="form-group pe-3">
                                        <label>{{ $t('AddSaleOrder.TermAndConditionArabic') }}:</label>
                                        <textarea class="form-control " rows="3" v-model="purchase.termAndConditionAr" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3">
                                        <div class="font-xs mb-1">{{ $t('AddSaleOrder.Attachment') }}</div>

                                        <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSaleOrder.Attachment') }} </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddSaleOrder.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-lg-12 mt-5 mb-5" v-if="purchase.id != '00000000-0000-0000-0000-000000000000' && purchase.approvalStatus==3">
                    <div class="accordion" id="accordionExample">
                        <div class="accordion-item">
                            <h5 class="accordion-header m-0" id="headingOne">
                                <button class="accordion-button fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    Payment
                                </button>
                            </h5>
                            <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                <div class="accordion-header p-3">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="payment=true"> Add Payment</a>
                                </div>
                                <div class="accordion-body">
                                    <purchaseorder-payment :totalAmount="totalAmount" :customerAccountId="customerAccountId" :show="payment" v-if="payment" @close="paymentSave" :isSaleOrder="'true'" :isPurchase="'false'" :purchaseOrderId="purchase.id" :formName="'BankReceipt'" />

                                    <div>
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
                                                <tr v-for="(payment,index) in purchase.paymentVoucher" v-bind:key="index">
                                                    <td>
                                                        {{index+1}}
                                                    </td>
                                                    <th>{{getDate(payment.date)}}</th>
                                                    <th class="text-right">{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                    <th class="text-center"><span v-if="payment.paymentMode==0">Cash</span><span v-if="payment.paymentMode==1">Bank</span></th>
                                                    <th>{{payment.narration}}</th>
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

        <bulk-attachment :attachmentList="purchase.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        <SmartDigitalInvoice :printDetails="printDetails" :isTouchScreen="'SaleServiceOrder'" :formName="'SaleOrder'" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printTemplate=='Template6'" v-bind:key="printRender" />
        <loading v-model:active="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
    
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import moment from "moment";
    
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    //import VueBarcode from 'vue-barcode';
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

                show: false,
                customerAccountId: '',
                printTemplate: '',
                isRaw: '',
                SaleOrder: 'SaleOrder',
                payment: false,
                saleOrder: false,
                daterander: 0,
                rander: 0,
                render: 0,
                customerRender: 0,
                imageSrc: '',
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    customerAddress: "",
                    logisticId: "",
                    barCode: "",
                    registrationNo: "",
                    quotationId: "",
                    customerId: "",
                    wareHouseId: "",
                    refrence: "",
                    days: '',
                    purchaseOrder: "",
                    paymentMethod: "",
                    sheduleDelivery: "",
                    note: '',
                    noteAr: '',
                    isFreight: false,
                    isLabour: false,
                    isQuotation: false,
                    isSerial: false,
                    soInventoryReserve: '',
                    terminalId: '',
                    invoicePrefix: '',
                    saleOrderItems: [],
                    attachmentList: [],
                    path: '',
                    clientPurchaseNo: '',
                    description: '',
                    isService: true,
                    taxMethod: '',
                    taxRateId: '',
                    isCredit: false,
                    isRemove: false,
                    mobile: '',
                    email: '',
                    cashCustomerId: '',
                    warrantyLogoPath: '',


                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0,
                    branchId: ''
                },
                loading: false,
                randerCustomer: 0,
                printDetails: [],
                printRender: 0,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                saleDefaultVat: '',
                renderImg: 0,
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    registrationNo: { required },
                    customerId: { required },
                    refrence: {},
                    wareHouseId: { required },
                    paymentMethod: { required },

                    saleOrderItems: { required },
                    description: {
                        maxLength: maxLength(110)
                    }
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
                    root.$refs.childComponentRef.ClearList();
                    root.$https.get('/Purchase/QuotationTemplateDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                response.data.quotationTemplateItems.forEach(function (so) {
                                    

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

            getAddress: function () {
                this.purchase.customerAddress = this.$refs.CustomerDropdown.GetCustomerAddress().address;
                this.purchase.mobile = this.$refs.CustomerDropdown.GetCustomerAddress().mobile;
                this.purchase.email = this.$refs.CustomerDropdown.GetCustomerAddress().email;
                this.purchase.cashCustomerId = this.$refs.CustomerDropdown.GetCustomerAddress().vatNo;
            },
            isCredit: function (value) {
                this.purchase.isCredit = value;
                value ? this.purchase.paymentMethod = 'Credit' : this.purchase.paymentMethod = 'Cash'
                this.randerCustomer++;

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

                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.purchase.warrantyLogoPath = response.data;
                        }
                    },
                        function () {
                            this.loading = false;
                            root.$swal({
                                title: root.$t('Error'),
                                text: root.$t('SomethingWrong'),
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },
            removeImage: function () {
                this.imageSrc = '';
                this.purchase.warrantyLogoPath = '';
                this.renderImg++;
                this.purchase.isRemove = true

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
            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
                }
            },
            GetQuotationDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.purchase.date = moment(response.data.date).format('llll');
                            root.purchase.customerId = response.data.customerId;
                            root.purchase.refrence = response.data.refrence;
                            root.purchase.days = response.data.days;
                            root.purchase.purchaseOrder = response.data.purchaseOrder;
                            root.purchase.paymentMethod = response.data.paymentMethod;
                            root.purchase.sheduleDelivery = response.data.sheduleDelivery;
                            root.purchase.note = response.data.note;
                            root.purchase.noteAr = response.data.noteAr;
                            root.purchase.isFreight = response.data.isFreight;
                            root.purchase.isLabour = response.data.isLabour;
                            root.purchase.path = response.data.path;
                            root.purchase.clientPurchaseNo = response.data.clientPurchaseNo;
                            root.purchase.description = response.data.description;

                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                response.data.saleOrderItems.forEach(function (x) {

                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                response.data.saleOrderItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }

                            root.$refs.childComponentRef.ClearList();
                            response.data.saleOrderItems.forEach(function (so) {
                                root.$refs.childComponentRef.addProduct(so.productId, so.product,so.quantity,so.unitPrice, so,so, false, so.description);
                            });
                            root.customerRender++;
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
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
                root.$https
                    .get("/Purchase/SaleOrderAutoGenerateNo?terminalId=" +terminalId + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix'), {
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
            savePurchase: function (status, print) {
                
                this.purchase.approvalStatus = status
                localStorage.setItem('active', status);

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.purchase.branchId = localStorage.getItem('BranchId');

                this.purchase.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
                this.purchase.soInventoryReserve = localStorage.getItem('SoInventoryReserve');

                this.purchase.saleOrderItems.forEach(function (x) {
                    x.highUnitPrice ? x.quantity = (x.highQty * x.unitPerPack) + x.quantity : x.quantity = x.totalPiece;

                });

                this.purchase.terminalId = localStorage.getItem('TerminalId');
                this.purchase.invoicePrefix = localStorage.getItem('InvoicePrefix');
                this.$https
                    .post('/Purchase/SaveSaleOrderInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false

                        root.info = response.data
                        if (print) {
                            root.PrintInvoice(response.data);

                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,
                            }).then(function (response) {
                                if (response != undefined) {
                                    if (root.purchase.id == "00000000-0000-0000-0000-000000000000" && !root.purchase.isDuplicate) {
                                        root.$router.go('AddSaleServiceOrder');

                                    } else {
                                        root.$router.push({
                                            path: '/SaleServiceOrder',
                                            query: {
                                                data: 'AddSaleServiceOrder'
                                            }
                                        })
                                    }
                                }
                            });
                        }


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

            },
            GetPaymentVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/SaleOrderPaymentList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.paymentVoucher = response.data;
                        }
                    });
            },
            paymentSave: function () {
                this.payment = false;
                this.GetPaymentVoucher();
            },
            goToPurchase: function () {
                if (this.isValid('CanViewServiceSaleOrder') || this.isValid('CanDraftServiceSaleOrder')) {
                    this.$router.push({
                        path: '/SaleServiceOrder',
                        query: {
                            data: 'AddSaleServiceOrder'
                        }
                    })
                }
                else {
                    this.$router.go();
                }


            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {

                            if (root.$route.query.data == undefined) {
                                root.purchase.note = response.data.termsInEng;
                                root.purchase.noteAr = response.data.termsInAr;
                                root.purchase.warrantyLogoPath = response.data.warrantyImageForPrint;

                            }


                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;
                            root.headerFooter.exchangeDays = response.data.exchangeDays;
                            root.headerFooter.printOptions = response.data.printOptions;
                            root.headerFooter.invoicePrint = response.data.invoicePrint;
                            root.headerFooter.welcomeLineEn = response.data.welcomeLineEn;
                            root.headerFooter.welcomeLineAr = response.data.welcomeLineAr;
                            root.headerFooter.closingLineEn = response.data.closingLineEn;
                            root.headerFooter.closingLineAr = response.data.closingLineAr;
                            root.headerFooter.contactNo = response.data.contactNo;
                            root.headerFooter.managementNo = response.data.managementNo;

                            root.headerFooter.businessAddressArabic = response.data.businessAddressArabic;
                            root.headerFooter.businessAddressEnglish = response.data.businessAddressEnglish;
                            root.headerFooter.headerImage = response.data.headerImageForPrint;
                            root.headerFooter.footerImage = response.data.footerImageForPrint;
                            root.headerFooter.footerImageForPrint = response.data.footerImageForPrint;
                            root.headerFooter.warrantyImage = response.data.warrantyImage;
                            root.headerFooter.warrantyImageForPrint = response.data.warrantyImageForPrint;

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });


            },
            PrintInvoice: function (value) {
                this.GetHeaderDetail();
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Purchase/SaleOrderDetail?id=" + value, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetails = response.data;
                            root.printDetails.saleItems = response.data.saleOrderItems;
                            root.printDetails.customerAddressWalkIn = response.data.customerAddress;
                            root.printDetails.termAndCondition = response.data.note;
                            root.printDetails.termAndConditionAr = response.data.noteAr;

                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.printDetails.saleOrderItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.printDetails.saleOrderItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printRender++;
                            root.loading = false
                        }
                    }).catch(error => {
                        console.log(error)
                        root.loading = false
                        if (root.purchase.id == "00000000-0000-0000-0000-000000000000" && !root.purchase.isDuplicate) {
                            root.$router.go('AddSaleServiceOrder');

                        } else {
                            root.$router.push({
                                path: '/SaleServiceOrder',
                                query: {
                                    data: 'AddSaleServiceOrder'
                                }
                            })
                        }
                    })
            },

            GetUserDefineFlow: function () {
                var root = this;
                if (localStorage.getItem('quotationToSaleOrder') == undefined) {
                    root.$https.get('/Company/UserDefineFlowEdit?companyId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                localStorage.setItem('quotationToSaleOrder', response.data.quotationToSaleOrder);
                                localStorage.setItem('quotationToSaleInvoice', response.data.quotationToSaleInvoice);
                                localStorage.setItem('partiallyQuotation', response.data.partiallyQuotation);
                                localStorage.setItem('partiallySaleOrder', response.data.partiallySaleOrder);

                                root.saleOrder = localStorage.getItem('quotationToSaleOrder') == 'true' ? true : false;
                            }
                            else {
                                console.log("error: something wrong from db.");
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                }
                else {
                    this.saleOrder = localStorage.getItem('quotationToSaleOrder') == 'true' ? true : false;

                }


            }

        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
            this.GetHeaderDetail()
            if (this.$route.query.data != undefined) {

                
                if (this.$route.query.data.isDuplicate) {
                    this.$route.query.data.id = '00000000-0000-0000-0000-000000000000';
                    this.purchase = this.$route.query.data;
                    this.purchase.isClose = false;
                    this.AutoIncrementCode();
                    this.purchase.date = moment().format('llll');
                }
                else {
                    this.purchase = this.$route.query.data;
                    this.purchase.date = moment(this.purchase.date).format('llll');
                }
                this.customerAccountId = this.$route.query.data.customerAccountId;
                if (this.purchase.paymentMethod == "Credit") {
                    this.purchase.isCredit = true
                }

                this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'

                this.attachment = true;
                this.rander++;
                this.render++;
            }
            else {
                this.purchase.wareHouseId = localStorage.getItem('WareHouseId');
                this.purchase.taxRateId = localStorage.getItem('TaxRateId');
                this.purchase.taxMethod = localStorage.getItem('taxMethod');
                this.purchase.paymentMethod = "Cash";
                this.purchase.isCredit = false;
            }
            this.GetUserDefineFlow();
            this.printTemplate = localStorage.getItem('PrintTemplate');
        },

        mounted: function () {

            this.isRaw = localStorage.getItem('IsProduction');
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();

                this.purchase.date = moment().format('llll');
                this.daterander++;
            }
            this.purchase.barCode = Math.floor(Math.random() * 100000000000);
        },
    };
</script>

<style scoped>
    .poHeading {
        font-size: 32px;
        font-style: normal;
        line-height: 37px;
        font-weight: 500;
        font-size: 24px;
        line-height: 26px;
        color: #3178F6;
        text-align: center
    }

    .dateHeading {
        font-size: 16px;
        font-style: normal;
        font-weight: 400;
        line-height: 18px;
        letter-spacing: 0.01em;
        color: #35353D;
    }

    .bottomBorder {
        padding-top: 24px !important;
        border-bottom: 1px solid #EFF4F7;
    }

    .invoice_top_bg {
        background-color: #3178f6;
    }

    .title_heading {
        font-weight: 600;
    }

    .bg_color {
        background-color: #dee9fe;
        border: 1px solid rgb(49 120 246);
    }

    .vue-radio-button {
        height: 100%;
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-around;
        align-items: center;
    }

    .icon {
        object-fit: contain;
    }

    .v-radio-button-active {
        background-color: #b6d7ff73 !important;
    }

    .title {
        font-size: 11px;
        margin-left: -10px;
    }

    .bt_bg_color {
        background-color: #3178F6 !important;
        color: #ffffff !important;
        border-color: #3178F6 !important;
        box-shadow: 0px 14px 34px rgba(49, 120, 246, 0.5) !important;
        border-radius: 10px;
    }

    .pointer-event-remove {
        pointer-events: none;
    }
</style>
