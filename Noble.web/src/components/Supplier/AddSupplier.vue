<template>
    <div class="row" v-if="isValid('CanAddSupplier') || isValid('CanEditSupplier') ">
        <div class="row">
            <div class="col-lg-12 ">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col d-flex align-items-baseline">
                            <div class="media">
                                <span class="circle-singleline" style="background-color: #1761FD !important;">SU</span>
                                <div class="media-body align-self-center ms-3">
                                    <h4 class="mb-0" v-if="newCustomer.id== '00000000-0000-0000-0000-000000000000'">{{ $t('AddSupplier.AddSupplier') }} <span class="ps-2 non-bold-text">{{ newCustomer.date }}</span></h4> 
                                    <h4 class="mb-0" v-else>{{ $t('AddSupplier.UpdateSupplier') }} <span class="ps-2 non-bold-text">{{ newCustomer.date }}</span></h4>
                                    <div class="col d-flex ">
                                        <p class="text-muted mb-0" style="font-size:13px !important;">{{ newCustomer.code }}</p>
                                    </div>
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
                    <div class="col-lg-8">
                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-3 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline "> {{ $t('AddSupplier.SupplierType') }} </span>
                                </div>
                                <div class="col-lg-9">
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.category" name="contact-sub-type" id="a49946497" class=" form-check-input" type="radio" value="B2B – Business to Business">
                                        <label class="form-check-label pl-0" for="a49946497">{{ $t('AddSupplier.Business') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.category" name="contact-sub-type" id="a9ff8eb35" class=" form-check-input" type="radio" value="B2C – Business to Client">
                                        <label class="form-check-label pl-0" for="a9ff8eb35">{{ $t('AddSupplier.Individual') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                                <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddSupplier.SupplierCode') }} </span>
                            </label> <div class="inline-fields col-lg-8">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <input v-model="newCustomer.code" disabled class="form-control" type="text">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row form-group" v-if="newCustomer.category == 'B2B – Business to Business'">
                            <label class="col-form-label col-lg-3">{{ $t('AddSupplier.CompanyName') }}:  <span class="text-danger"> *</span></label>
                            <div class="col-lg-3 form-group">
                                <input v-model="newCustomer.companyNameEnglish" placeholder="English" @change="DisplayName()" class="form-control" type="text">
                            </div>
                            <div class="col-lg-3 form-group">
                                <input v-model="newCustomer.companyNameArabic" placeholder="Arabic" @change="DisplayName()" class="form-control" type="text">
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                                <span id="ember695" class="tooltip-container text-dashed-underline ">{{ $t('AddSupplier.PrimaryContact') }}</span>
                            </label> <div class="inline-fields col-lg-8">
                                <div class="row">
                                    <div class="col-lg-4 form-group">
                                        <salutation-dropdown v-model="newCustomer.prefix" :modelValue="newCustomer.prefix" @change="DisplayName()" />
                                    </div>
                                    <div class="col-lg-4 form-group">
                                        <input v-model="newCustomer.englishName" @change="DisplayName()" placeholder="English Name" class="form-control" type="text">
                                    </div>
                                    <div class="col-lg-4 form-group">
                                        <input v-model="newCustomer.arabicName" @change="DisplayName()" placeholder="Arabic Name" class="form-control" type="text">
                                    </div>
                                </div>
                            </div>
                        </div>
                       

                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">{{ $t('AddCustomer.CustomerDisplayName') }} : <span class="text-danger"> *</span></label>
                            <div class="col-lg-6" v-bind:key="randerElement">
                                <multiselect v-model="displayName"  
                                        :options="options"  @select="asyncFind()"
                                        :placeholder="$t('DisplayNameDropdown.DisplayName')" 
                                        :searchable="true" 
                                        :multiple="false" 
                                        track-by="name"  
                                        :show-labels="false" 
                                        label="name" 
                                        
                                        @update:modelValue="getData()">
                            </multiselect>
                            </div>
                        </div>
                        <div class="row form-group mt-4">
                            <label class="col-form-label col-lg-3 ">{{ $t('AddCustomer.RegistrationDate') }}</label>
                            <div class="col-lg-6">
                                <datepicker v-model="newCustomer.registrationDate"></datepicker>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3 ">{{ $t('AddSupplier.SupplierCategory') }}</label>
                            <div class="col-lg-6">
                                <multiselect v-model="newCustomer.supplierType" v-if="($i18n.locale == 'en' ||isLeftToRight()) " :options="['wholesaler', 'Retailer','Wholesaler & Retailer', 'Dealer', 'Distributor','International Supplier', 'International Manufacturers', 'International Agent / Exporter']" :show-labels="false" v-bind:placeholder="$t('AddSupplier.SelectOption')">  </multiselect>
                                <multiselect v-model="newCustomer.supplierType" v-else :options="['جمله', 'قطاعي','بائع بالجملة', 'وكيل', 'موزع', 'مزود دولي', 'الشركات المصنعة العالمية', 'وكيل / مصدر دولي']" :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                                </multiselect>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3 ">{{ $t('AddSupplier.SupplierGroup') }}</label>
                            <div class="col-lg-6">
                                <input class="form-control" v-model="newCustomer.customerGroup" type="text">
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                                {{ $t('AddSupplier.CustomerPhone') }} <br />
                                                                      <a v-if="!isSkype" href="javascript:void(0)" v-on:click="isSkype=true">{{ $t('AddSupplier.Addmoredetails') }}</a>
                            </label>
                            <div class="col-lg-3 form-group">
                                <input placeholder="Work Phone" v-model="newCustomer.telephone" class="form-control" type="text">
                            </div>
                            <div class="col-lg-3 form-group">
                                <input placeholder="Mobile" v-model="newCustomer.contactNo1" class="form-control" type="text">
                            </div>
                            <div class="col-lg-3 form-group">
                                <input :placeholder="$t('WhatsApp Number')" v-model="newCustomer.whatsAppNumber" class="form-control" type="text">
                            </div>
                        </div>
                        <div class="row form-group" v-if="isSkype">
                            <label class="col-form-label col-lg-3">{{ $t('AddSupplier.SkypeName') }}</label>
                            <div class="col-lg-6">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span style="background-color: #e3ebf1;border: 1px solid #ffffff00;" class="input-group-text"><i class="fab fa-skype"></i></span>
                                    </div>
                                    <input type="text" class="form-control" style="border-left: 1;">
                                </div>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">{{ $t('AddCustomer.Email') }}</label>
                            <div class="col-lg-6">
                                <input v-model="newCustomer.email" class="form-control" type="text">
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                            </label>
                            <div class="form-group col-lg-3">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox14564" v-model="newCustomer.isAllowEmail">
                                    <label for="inlineCheckbox14564"> {{ $t('Allow Email') }} </label>
                                </div>
                            </div>
                            <div class="form-group col-lg-3">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox132323" v-model="newCustomer.isAutoEmail">
                                    <label for="inlineCheckbox132323"> {{ $t('Auto Email') }} </label>
                                </div>
                            </div>

                        </div>

                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                            </label>
                            <div class="form-group col-lg-3" v-if="isRaw=='true'">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="newCustomer.isRaw">
                                    <label for="inlineCheckbox1"> {{ $t('AddSupplier.RawSupplier') }}  </label>
                                </div>
                            </div>
                            <div class="form-group col-lg-3">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="newCustomer.isActive">
                                    <label for="inlineCheckbox1"> {{ $t('AddSupplier.Active') }}  </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionExample">
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingOne">
                            <button class="accordion-button fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                <b>{{ $t('AddSupplier.OfficialInformation') }}</b>
                            </button>
                        </h5>
                        <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">{{ $t('AddCustomer.CommercialRegistrationNo') }}: <span class="text-danger" v-if="newCustomer.supplierType != 'International Supplier' && newCustomer.supplierType != 'مزود دولي' && newCustomer.supplierType != 'International Manufacturers' && newCustomer.supplierType != 'الشركات المصنعة العالمية' && newCustomer.supplierType != 'International Agent / Exporter' && newCustomer.supplierType != 'وكيل / مصدر دولي'"> *</span></label>
                                            <div class="col-lg-6">
                                                <input v-model="newCustomer.commercialRegistrationNo" class="form-control" type="text">
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">{{ $t('AddCustomer.VAT/NTN/Tax No') }}: <span class="text-danger" v-if="newCustomer.supplierType != 'International Supplier' && newCustomer.supplierType != 'مزود دولي' && newCustomer.supplierType != 'International Manufacturers' && newCustomer.supplierType != 'الشركات المصنعة العالمية' && newCustomer.supplierType != 'International Agent / Exporter' && newCustomer.supplierType != 'وكيل / مصدر دولي'"> *</span></label>
                                            <div class="col-lg-6">
                                                <input v-model="newCustomer.vatNo" class="form-control" type="text">
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">Supplier Code : </label>
                                            <div class="col-lg-6">
                                                <input v-model="newCustomer.customerCode" class="form-control" type="text">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingTwo">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <b>{{ $t('AddSupplier.PaymentTerms') }}</b>
                            </button>
                        </h5>
                        <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">{{ $t('AddSupplier.PaymentTerms') }}</label>
                                            <div class="col-lg-3 form-group">
                                                <multiselect v-model="newCustomer.paymentTerms" :preselect-first="true" v-if="($i18n.locale == 'en' ||isLeftToRight()) " :options="[ 'Cash', 'Credit']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="newCustomer.paymentTerms" :preselect-first="true" :options="[ 'نقد', 'آجل']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')">
                                                </multiselect>
                                            </div>
                                        </div>
                                        <div class="row form-group" v-if="newCustomer.paymentTerms=='Credit'">
                                            <label class="col-form-label col-lg-3"></label>
                                            <div class="col-lg-3 form-group">
                                                <input v-model="newCustomer.creditLimit" :placeholder="$t('AddCustomer.CreditLimit')" class="form-control" type="number">
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <input v-model="newCustomer.creditPeriod" :placeholder="$t('AddCustomer.CreditPeriod')" class="form-control" type="number">
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">{{ $t('AddSupplier.DeliveryTerm') }}</label>
                                            <div class="col-lg-6">
                                                <input v-model="newCustomer.deliveryTerm" class="form-control" type="text">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingThree">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                <b> {{ $t('AddSupplier.OtherDetails') }}</b>
                            </button>
                        </h5>
                        <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 ">{{ $t('AddSupplier.Currency') }}</label>
                                            <div class="col-lg-6">
                                                <currency-dropdown v-model="newCustomer.currencyId" :modelValue="newCustomer.currencyId" />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">{{ $t('AddSupplier.TaxRate') }}</label>
                                            <div class="col-lg-6">
                                                <taxratedropdown v-model="newCustomer.taxRateId" :modelValue="newCustomer.taxRateId" />
                                                <div class="form-text">
                                                    <small>To associate more than one tax, you need to create a tax group in Settings.</small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row form-group mt-4">
                                            <label class="col-lg-3 col-form-label">Facebook</label>
                                            <div class="col-lg-6">

                                                <div class="input-group ">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fab fa-facebook-square"></i></button>
                                                    <input type="text" class="form-control" placeholder="" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                </div>


                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <label class="col-lg-3 col-form-label">Twitter</label>
                                            <div class="col-lg-6">
                                                <div class="input-group ">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fab fa-twitter-square"></i></button>
                                                    <input type="text" class="form-control" placeholder="" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                </div>


                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3">{{ $t('AddSupplier.Website') }}</label>
                                            <div class="col-lg-6">
                                                <input v-model="newCustomer.website" class="form-control" type="text">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingFour">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                               <b>{{ $t('AddSupplier.Address') }}</b>
                            </button>
                        </h5>
                        <div id="collapseFour" class="accordion-collapse collapse" aria-labelledby="headingFour" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="row mb-3">
                                            <label class=" ">{{ $t('AddSupplier.BillingAddress') }}</label>
                                        </div>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4  ">{{ $t('AddSupplier.Attention') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.billingAttention" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Country/Region') }}</label>
                                                <div class="col-lg-7 ">
                                                    <countrydropdown v-model="newCustomer.billingCountry" :modelValue="newCustomer.billingCountry" />
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Address') }}</label>
                                                <div class="col-lg-7 ">
                                                    <textarea rows="3" v-model="newCustomer.billingAddress" class="form-control "></textarea>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.City') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.billingCity" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.State') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.billingArea" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.ZipCode') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.billingZipCode" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Phone') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.billingPhone" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Fax') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.billingFax" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="row  mb-3">
                                            <label>{{ $t('AddSupplier.ShippingAddress') }}</label>

                                        </div>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Attention') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.shippingAttention" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Country/Region') }}</label>
                                                <div class="col-lg-7 ">
                                                    <countrydropdown v-model="newCustomer.shippingCountry" :modelValue="newCustomer.shippingCountry" />
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Address') }}</label>
                                                <div class="col-lg-7 ">
                                                    <textarea rows="3" v-model="newCustomer.shippingAddress" class="form-control "></textarea>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.City') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.shippingCity" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.State') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.shippingArea" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.ZipCode') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.shippingZipCode" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Phone') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.shippingPhone" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <div class="row">
                                                <label class="col-lg-4 ">{{ $t('AddSupplier.Fax') }}</label>
                                                <div class="col-lg-7 ">
                                                    <input v-model="newCustomer.shippingFax" class="form-control " type="text">
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingFive">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                <b> {{ $t('AddSupplier.BankDetails') }}</b>
                            </button>
                        </h5>
                        <div id="collapseFive" class="accordion-collapse collapse" aria-labelledby="headingFive" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="table-responsive">
                                            <table class="table mb-0">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th style="width: 50px;">{{ $t('AddSupplier.BankAccountTitle') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.BankAccountNo') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.IBAN') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.NameofBank') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.BranchName') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.RoutingCode') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.City') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.Country') }}</th>
                                                        <th style="width: 50px">{{ $t('AddSupplier.Address') }}</th>
                                                        <th style="width: 10px"></th>
                                                    </tr>
                                                </thead>
                                                <tbody id="purchase-item">
                                                    <tr v-for="(person , index) in newCustomer.contactBankAccountList" :key="index">
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.accountTitle"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.accountNo"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.iban"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.nameOfBank"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.branchName"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.routingCode"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.city"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.country"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>

                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.address"
                                                                   @focus="$event.target.select()"
                                                                   class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 pt-0 text-end">
                                                            <button title="Remove Item" id="bElim" type="button"
                                                                    class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveBankRow(index)">
                                                                <i class="dripicons-trash " aria-hidden="true"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" class="border-top-0">
                                                            <button id="but_add" class="btn btn-success btn-sm" v-on:click="AddBankRow()">+ {{ $t('AddSupplier.AddBankDetail') }}</button>

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
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingSix">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                <b>{{ $t('AddSupplier.ContactPerson') }}</b>
                            </button>
                        </h5>
                        <div id="collapseSix" class="accordion-collapse collapse" aria-labelledby="headingSix" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th style="width: 50px;">{{ $t('AddSupplier.Salutation') }}</th>
                                                    <th style="width: 50px">{{ $t('AddSupplier.FirstName') }}</th>
                                                    <th style="width: 50px">{{ $t('AddSupplier.LastName') }}</th>
                                                    <th style="width: 80px">{{ $t('AddSupplier.Email') }}</th>
                                                    <th style="width: 50px">{{ $t('AddSupplier.WorkPhone') }}</th>
                                                    <th style="width: 50px">{{ $t('AddSupplier.Mobile') }}</th>
                                                    <th style="width: 30px"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(person , index) in newCustomer.contactPersonList" :key="index">
                                                    <td class="border-top-0 text-center">
                                                        <salutation-dropdown v-model="person.prefix" :modelValue="person.prefix" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <input type="text" v-model="person.firstName"
                                                               @focus="$event.target.select()"
                                                               class="form-control input-border tableHoverOn" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <input type="text" v-model="person.lastName"
                                                               @focus="$event.target.select()"
                                                               class="form-control input-border tableHoverOn" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <input type="text" v-model="person.email"
                                                               @focus="$event.target.select()"
                                                               class="form-control input-border tableHoverOn" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <input type="text" v-model="person.phone"
                                                               @focus="$event.target.select()"
                                                               class="form-control input-border tableHoverOn" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <input type="text" v-model="person.mobile"
                                                               @focus="$event.target.select()"
                                                               class="form-control input-border tableHoverOn" />
                                                    </td>
                                                    <td class="border-top-0 pt-0 text-end">
                                                        <button title="Remove Item" id="bElim" type="button"
                                                                class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow(index)">
                                                            <i class="dripicons-trash" aria-hidden="true"></i>
                                                        </button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" class="border-top-0">
                                                        <button id="but_add" class="btn btn-success btn-sm" v-on:click="AddRow()">+ {{ $t('AddSupplier.AddContactPerson') }}</button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingSeven">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSeven" aria-expanded="false" aria-controls="collapseSeven">
                                <b>  {{ $t('AddSupplier.Remarks') }}</b>
                            </button>
                        </h5>
                        <div id="collapseSeven" class="accordion-collapse collapse" aria-labelledby="headingSeven" data-bs-parent="#accordionExample">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <div class="col-lg-8">
                                                <label class="col-form-label">{{ $t('AddSupplier.Remarks') }} (For Internal Use)</label>
                                                <textarea rows="3" v-model="newCustomer.remarks" class="form-control"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-12 mt-4 mb-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-6" style="border-right: 1px solid #eee;">
                                        <div class="form-group ps-3">
                                            <div class="font-xs mb-1">{{ $t('AddSupplier.AttachFiles') }}</div>
                                            <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('PurchaseView.Attachment') }} </button>
                                            <div>
                                                <small class="text-muted">
                                                    {{ $t('AddSupplier.FileSize') }}
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
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            <bulk-attachment :attachmentList="newCustomer.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        </div>
        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class=" col-md-12">
                    <div class="button-items" v-if="newCustomer.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddCustomer')">
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.newCustomer.$invalid" v-if="newCustomer.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddSupplier')" v-on:click="SaveSupplier"><i class="far fa-save"></i>  {{ $t('AddSupplier.btnSave') }}</button>
                        <button class="btn btn-danger" v-on:click="Cancel()">{{ $t('AddSupplier.btnClear') }}</button>
                    </div>
                    <div class="button-items" v-else>                    
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.newCustomer.$invalid" v-if="newCustomer.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditSupplier')" v-on:click="SaveSupplier"><i class="far fa-save"></i>  {{ $t('AddSupplier.btnUpdate') }}</button>
                        <button class="btn btn-danger" v-on:click="Cancel()">{{ $t('AddSupplier.btnClear') }}</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import moment from 'moment'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';


    export default ({
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect,
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                show: false,
                isRaw: '',
                arabic: '',
                english: '',
                b2b: false,
                b2c: false,
                displayName: '',
                isSkype: false,
                salutatioRender: 0,
                randerElement: 0,
                
                newCustomer: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    date:'',
                    prefix: '',
                    englishName: '',
                    arabicName: '',
                    companyNameEnglish: '',
                    companyNameArabic: '',
                    customerDisplayName: '',
                    telephone: '',
                    email: '',
                    registrationDate: '',
                    category: '',
                    customerType: '',
                    supplierType: '',
                    customerGroup: '',
                    contactNo1: '',

                    commercialRegistrationNo: '',
                    vatNo: '',
                    whatsAppNumber:'',
                    currencyId: '',
                    taxRateId: '',
                    website: '',
                    customerCode: '',

                    billingAttention: '',
                    billingCountry: '',
                    billingZipCode: '',
                    billingPhone: '',
                    billingArea: '',
                    billingAddress: '',
                    billingCity: '',
                    billingFax: '',

                    shippingAttention: '',
                    shippingCountry: '',
                    shippingZipCode: '',
                    shippingPhone: '',
                    shippingArea: '',
                    shippingAddress: '',
                    shippingCity: '',
                    shippingFax: '',

                    contactPersonList: [{ prefix: '', firstName: '', lastName: '', email: '', phone: '', mobile: '' }],

                    contactBankAccountList: [{ accountTitle: '', accountNo: '', iban: '', nameOfBank: '', branchName: '', routingCode: '', city: '', country: '', currency: '', address: '' }],

                    remarks: '',
                    isCustomer: false,
                    isActive: true,
                    isRaw: false,
                    isAllowEmail: false,
                    isAutoEmail: false,

                    paymentTerms: '',
                    deliveryTerm: '',
                    creditLimit: '',
                    creditPeriod: '',

                    attachmentList: []
                },
                options: [],
                loading: false,
                language: 'Nothing',
            }
        },
        validations() {
            return {
                newCustomer:
                {
                    category: { maxLength: maxLength(250) },
                    supplierType: { required },
                    englishName: {
                        maxLength: maxLength(250)
                    },
                    arabicName: {
                        required: requiredIf(function () {
                            if (this.newCustomer.category === 'B2B – Business to Business') {
                                return false;
                            } else {
                                return !this.newCustomer.englishName;
                            }
                        }),
                        maxLength: maxLength(250)
                    },
                    vatNo: {
                        required: requiredIf(function () {
                            if (
                                !this.newCustomer.commercialRegistrationNo ||
                                this.newCustomer.commercialRegistrationNo === '' ||
                                this.newCustomer.commercialRegistrationNo === null ||
                                this.newCustomer.commercialRegistrationNo === undefined
                            ) {
                                if (
                                    !['International Supplier', 'مزود دولي', 'International Manufacturers', 'الشركات المصنعة العالمية', 'International Agent / Exporter', 'وكيل / مصدر دولي'].includes(this.newCustomer.supplierType)
                                ) {
                                    return false;
                                }  
                                return true;
                            } else {
                                return false;
                            }
                        }),
                        maxLength: maxLength(250)
                    },
                    paymentTerms: { required },
                },
            }
        },
        methods: {
            // DisplayName: function () {
            //     this.salutatioRender++;
            // },
            asyncFind:function(){
                this.newCustomer.customerDisplayName=this.displayName.name;

            },
            getData: function () {
                
                if (this.newCustomer.prefix != '' && this.newCustomer.prefix != null && this.newCustomer.prefix != undefined) {
                    this.options.push({ name: this.newCustomer.prefix + ' ' + this.newCustomer.englishName });
                }
                if (this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined) {
                    this.options.push({ name: this.newCustomer.englishName });
                }
                if (this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                    this.options.push({ name: this.newCustomer.arabicName });
                }
                if (this.newCustomer.prefix != '' && this.newCustomer.prefix != null && this.newCustomer.prefix != undefined && this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                    this.options.push({ name: this.newCustomer.prefix + ' ' + this.newCustomer.arabicName });
                }
                if (this.newCustomer.companyNameEnglish != '' && this.newCustomer.companyNameEnglish != null && this.newCustomer.companyNameEnglish != undefined) {
                    this.options.push({ name: this.newCustomer.companyNameEnglish });
                }
                if (this.newCustomer.companyNameArabic != '' && this.newCustomer.companyNameArabic != null && this.newCustomer.companyNameArabic != undefined) {
                    this.options.push({ name: this.newCustomer.companyNameArabic });
                }
                if (this.newCustomer.companyNameEnglish != '' && this.newCustomer.companyNameEnglish != null && this.newCustomer.companyNameEnglish != undefined && this.newCustomer.companyNameArabic != '' && this.newCustomer.companyNameArabic != null && this.newCustomer.companyNameArabic != undefined) {
                    this.options.push({ name: this.newCustomer.companyNameEnglish + '  ' + this.newCustomer.companyNameArabic });
                }
                if ((this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined) && (this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined)) {
                    this.options.push({ name: this.newCustomer.englishName + '  ' + this.newCustomer.arabicName });
                }
            },
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.newCustomer.attachmentList = attachment;
                this.show = false;
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.newCustomer.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addsupplier');
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

            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Contact/AutoGenerateCode?issupplier=false'+ '&isCashCustomer=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.newCustomer.code = response.data.contact;
                        }
                    });
            },
            Cancel: function () {
                
                if (this.isValid('CanViewSupplier')) {
                    this.$router.push({
                        path: '/supplier',

                    })
                }
                else {
                    this.$router.go();
                }

            },
            DisplayName: function () {
                this.getData();
            },
            SaveSupplier: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.newCustomer.supplierType == 'جمله') {
                    this.newCustomer.supplierType = 1;
                }
                else if (this.newCustomer.supplierType == 'قطاعي') {
                    this.newCustomer.supplierType = 2;
                }
                else if (this.newCustomer.supplierType == 'بائع بالجملة') {
                    this.newCustomer.supplierType = 5;
                }
                else if (this.newCustomer.supplierType == 'وكيل') {
                    this.newCustomer.supplierType = 3;
                }
                else if (this.newCustomer.supplierType == 'موزع') {
                    this.newCustomer.supplierType = 4;
                }
                else if (this.newCustomer.supplierType == 'Wholesaler & Retailer') {
                    this.newCustomer.supplierType = 5;
                }
                else if (this.newCustomer.supplierType == 'International Supplier' || this.newCustomer.supplierType == 'مزود دولي') {
                    this.newCustomer.supplierType = 6;
                }
                else if (this.newCustomer.supplierType == 'International Manufacturers' || this.newCustomer.supplierType == 'الشركات المصنعة العالمية') {
                    this.newCustomer.supplierType = 7;
                }
                else if (this.newCustomer.supplierType == 'International Agent / Exporter' || this.newCustomer.supplierType == 'وكيل / مصدر دولي') {
                    this.newCustomer.supplierType = 8;
                }
                else {
                    console.log(this.newCustomer.supplierType);
                }
                root.$https
                    .post('/Contact/SaveContact', this.newCustomer, { headers: { "Authorization": `Bearer ${token}` } })
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

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewSupplier')) {
                                        root.$router.push({
                                            path: '/supplier',

                                        })
                                    }
                                    else {
                                        root.$router.go();
                                    }

                                }
                            });
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
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

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewSupplier')) {
                                        root.$router.push({
                                            path: '/supplier',

                                        })
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                }
                            });
                        }
                        else {
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
                        root.$swal.fire(
                            {
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

            AddRow: function () {
                this.newCustomer.contactPersonList.push({ prefix: '', firstName: '', lastName: '', email: '', phone: '', mobile: '' });
            },
            RemoveRow: function (index) {
                this.newCustomer.contactPersonList.splice(index, 1);
            },

            AddBankRow: function () {
                this.newCustomer.contactBankAccountList.push({ accountTitle: '', accountNo: '', iban: '', nameOfBank: '', branchName: '', routingCode: '', city: '', country: '', currency: '', address: '' });
            },
            RemoveBankRow: function (index) {
                this.newCustomer.contactBankAccountList.splice(index, 1);
            },
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
            root.$route.query.data = this.$store.isGetEdit;
            }
            this.newCustomer.date = moment().format("DD MMM YYYY hh:mm A");

            this.$emit('update:modelValue', this.$route.name);
            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isRaw = localStorage.getItem('IsProduction');



            if (this.$route.query.data == '00000000-0000-0000-0000-000000000000' || this.$route.query.data == undefined || this.$route.query.data == '') {
                this.GetAutoCodeGenerator();
                this.newCustomer.registrationDate = moment().format('llll');

                if(localStorage.getItem('IsSupplierCredit') != 'true'){
                    this.newCustomer.paymentTerms = (this.$i18n.locale == 'en' || this.isLeftToRight())? 'Cash' :'نقد'
                }
                else{
                    this.newCustomer.paymentTerms = (this.$i18n.locale == 'en' || this.isLeftToRight())? 'Credit' :'آجل'
                }
               

                this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
                this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
                if (this.b2b && !this.b2c) {
                    this.newCustomer.category = 'B2B – Business to Business';
                }
                if (!this.b2c && this.b2c) {
                    this.newCustomer.category = 'B2C – Business to Client';
                }
            }

            if (this.$route.query.data != undefined) {
                this.newCustomer = this.$route.query.data;    
                this.newCustomer.date =  this.newCustomer.date == null ? moment().format("DD MMM YYYY hh:mm A") : moment(this.newCustomer.date).format('DD MMM YYYY hh:mm A');
                this.displayName=this.newCustomer.customerDisplayName;
                this.randerElement++;
                this.getData();
                if (this.language == 'en') {
                    if (this.$route.query.data.supplierType == 1) {
                        this.newCustomer.supplierType = 'Wholesaler';
                    }
                    else if (this.$route.query.data.supplierType == 2) {
                        this.newCustomer.supplierType = 'Retailer';
                    }
                    else if (this.$route.query.data.supplierType == 5) {
                        this.newCustomer.supplierType = 'Wholesaler & Retailer';
                    }
                    else if (this.$route.query.data.supplierType == 3) {
                        this.newCustomer.supplierType = 'Dealer';
                    }
                    else if (this.$route.query.data.supplierType == 4) {
                        this.newCustomer.supplierType = 'Distributor';
                    }
                    else if (this.$route.query.data.supplierType == 6) {
                        this.newCustomer.supplierType = 'International Supplier';
                    }
                    else if (this.$route.query.data.supplierType == 7) {
                        this.newCustomer.supplierType = 'International Manufacturers';
                    }
                    else if (this.$route.query.data.supplierType == 8) {
                        this.newCustomer.supplierType = 'International Agent / Exporter';
                    }
                    else {
                        this.newCustomer.supplierType = '';
                    }
                }
                else {
                    if (this.$route.query.data.supplierType == 1) {
                        this.newCustomer.supplierType = 'جمله';
                    }
                    else if (this.$route.query.data.supplierType == 2) {
                        this.newCustomer.supplierType = 'قطاعي';
                    }
                    else if (this.$route.query.data.supplierType == 5) {
                        this.newCustomer.supplierType = 'بائع بالجملة';
                    }
                    else if (this.$route.query.data.supplierType == 3) {
                        this.newCustomer.supplierType = 'وكيل';
                    }
                    else if (this.$route.query.data.supplierType == 4) {
                        this.newCustomer.supplierType = 'موزع';
                    }
                    else if (this.$route.query.data.supplierType == 6) {
                        this.newCustomer.supplierType = 'مزود دولي';
                    }
                    else if (this.$route.query.data.supplierType == 7) {
                        this.newCustomer.supplierType = 'الشركات المصنعة العالمية';
                    }
                    else if (this.$route.query.data.supplierType == 8) {
                        this.newCustomer.supplierType = 'وكيل / مصدر دولي';
                    }
                    else {
                        this.newCustomer.supplierType = '';
                    }
                }

            }
        },

        mounted: function () {


        }
    })

</script>
<style scoped>
    .circle-singleline 
    {
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
    .non-bold-text {
        font-weight: normal;
        font-size: 14px !important;
    }
</style>