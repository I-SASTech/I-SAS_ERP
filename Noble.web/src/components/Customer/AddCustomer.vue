<template>
    <div class="row">
        <div class="col-lg-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;" v-if="formNames=='Supplier'">SU</span>
                            <span class="circle-singleline" style="background-color: #1761FD !important;" v-else>CU</span>
                                <div class="media-body align-self-center ms-3" v-if="formNames=='Supplier'">
                                    <h4 class="mb-0" v-if="newCustomer.id== '00000000-0000-0000-0000-000000000000'">{{ $t('AddSupplier.AddSupplier') }} <span class="ps-2 non-bold-text">{{ newCustomer.date }}</span></h4> 
                                    <h4 class="mb-0" v-else>{{ $t('AddSupplier.UpdateSupplier') }} <span class="ps-2 non-bold-text">{{ newCustomer.date }}</span></h4>
                                    <div class="col d-flex ">
                                        <p class="text-muted mb-0" style="font-size:13px !important;">{{ newCustomer.code }}</p>
                                    </div>
                                </div>
                            <div class="media-body align-self-center ms-3" v-else>
                                <h4 class="mb-0" v-if="newCustomer.id== '00000000-0000-0000-0000-000000000000'">{{ $t('AddCustomer.NewCustomer') }} <span class="ps-2 non-bold-text">{{ newCustomer.date }}</span></h4> 
                                <h4 class="mb-0" v-else>{{ $t('AddCustomer.UpdateCustomer') }} <span class="ps-2 non-bold-text">{{ newCustomer.date }}</span></h4>
                                <div class="col d-flex ">
                                    <p class="text-muted mb-0" style="font-size:13px !important;" v-if="newCustomer.isCashCustomer">{{ cashCustomerCode }}</p>
                                    <p class="text-muted mb-0" style="font-size:13px !important;" v-else>{{ contactCode }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <hr class="hr-dashed hr-menu my-0" />

        <div class="card border-0">
            <div class="card-body border-0">
                <div class="row">
                    <div class="col-lg-8">
                        <fieldset class="form-group" v-if="isCashCustomer">
                            <div class="row">
                                <div class="col-form-label col-lg-3 pt-0">
                                    <span id="ember695" class="tooltip-container text-dashed-underline ">Customer :</span>
                                </div>
                                <div class="col-lg-9" v-if="newCustomer.id== '00000000-0000-0000-0000-000000000000'">
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.isCashCustomer" @change="isCashCustomerFunc()" name="contact-sub-type12" id="a49946498" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a49946498">Walk-In</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.isCashCustomer" @change="isCashCustomerFunc()" name="contact-sub-type12" id="a9ff8eb36" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a9ff8eb36">Cash/Credit</label>
                                    </div>
                                </div>
                                <div class="col-lg-9" v-else>
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.isCashCustomer" v-bind:disabled="newCustomer.id!= '00000000-0000-0000-0000-000000000000'" name="contact-sub-type12" id="a49946498" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a49946498">Walk-In</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.isCashCustomer" v-bind:disabled="newCustomer.id!= '00000000-0000-0000-0000-000000000000'" name="contact-sub-type12" id="a9ff8eb36" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a9ff8eb36">Cash/Credit</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group" v-if="!newCustomer.isCashCustomer">
                            <div class="row">
                                <div class="col-form-label col-lg-3 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('AddCustomer.CustomerType') }}</span>
                                </div>
                                <div class="col-lg-9">
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.category" name="contact-sub-type" id="a49946497" class=" form-check-input" type="radio" value="B2B – Business to Business" v-on:click="changeCustomerType(true)">
                                        <label class="form-check-label pl-0" for="a49946497">{{ $t('AddCustomer.Business') }}({{ $t('AddCustomer.B2B') }})</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="newCustomer.category" name="contact-sub-type" id="a9ff8eb35" class=" form-check-input" type="radio" value="B2C – Business to Client" v-on:click="changeCustomerType(false)">
                                        <label class="form-check-label pl-0" for="a9ff8eb35">{{ $t('AddCustomer.Individual') }}({{ $t('AddCustomer.B2C') }})</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                                <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddCustomer.Code') }} </span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <div class="row">
                                    <div class="col-lg-4" v-if="newCustomer.isCashCustomer ">
                                        <input v-model="cashCustomerCode" v-bind:key="randerInput" disabled class="form-control" type="text">
                                    </div>
                                    <div class="col-lg-4" v-else>
                                        <input v-model="contactCode" v-bind:key="randerInput" disabled class="form-control" type="text">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row form-group" v-if="!newCustomer.isCashCustomer && newCustomer.category != 'B2C – Business to Client'">
                            <label class="col-form-label col-lg-3" v-if="newCustomer.category=='B2B – Business to Business'">{{ $t('AddCustomer.CompanyName') }}: <span class="text-danger"> *</span></label>
                            <label class="col-form-label col-lg-3" v-else>{{ $t('AddCustomer.CompanyName') }}: </label>
                            <div class="col-lg-3 form-group">
                                <input v-model="newCustomer.companyNameEnglish" :placeholder="$t('AddCustomer.EnglishName')" @change="DisplayName" class="form-control" type="text">
                            </div>
                            <div class="col-lg-3 form-group">
                                <input v-model="newCustomer.companyNameArabic" :placeholder="$t('AddCustomer.ArabicName')" @change="DisplayName()" class="form-control" type="text">
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                                <span id="ember695" class="tooltip-container text-dashed-underline " v-if="newCustomer.category=='B2C – Business to Client'">{{ $t('AddCustomer.PrimaryContact') }} : <span class="text-danger"> *</span></span>
                                <span id="ember695" class="tooltip-container text-dashed-underline " v-else>{{ $t('AddCustomer.PrimaryContact') }} :</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <div class="row">
                                    <div class="col-lg-4 form-group">
                                        <salutation-dropdown v-model="newCustomer.prefix" :modelValue="newCustomer.prefix" @change="DisplayName()" />
                                    </div>
                                    <div class="col-lg-4 form-group">
                                        <input v-model="newCustomer.englishName" @change="DisplayName()" :placeholder="$t('AddCustomer.EnglishName')" class="form-control" type="text">
                                    </div>
                                    <div class="col-lg-4 form-group">
                                        <input v-model="newCustomer.arabicName" @change="DisplayName()" :placeholder="$t('AddCustomer.ArabicName')" class="form-control" type="text">
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
                                <!-- <display-name-dropdown v-model="newCustomer.customerDisplayName" :modelValue="newCustomer.customerDisplayName" @update:modelValue="getData()" :newCustomer="newCustomer" :key="salutatioRender" /> -->
                            </div>
                        </div>

                        <div class="row form-group mt-4">
                            <label class="col-form-label col-lg-3 ">{{ $t('AddCustomer.RegistrationDate') }}</label>
                            <div class="col-lg-6">
                                <datepicker v-model="newCustomer.registrationDate"></datepicker>
                            </div>
                        </div>
                        <div class="row form-group" v-if="newCustomer.category == 'B2B – Business to Business'" :key="render">
                            <label class="col-form-label col-lg-3 ">{{ $t('AddCustomer.CustomerCategory') }} : <span class="text-danger"> *</span></label>
                            <div class="col-lg-6">
                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-bind:disabled="newCustomer.isCashCustomer" v-model="newCustomer.customerType" :options="['Establishment', 'Company','Factory','Manufacturer','Government']" :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                                </multiselect>
                                <multiselect v-else v-model="newCustomer.customerType" v-bind:disabled="newCustomer.isCashCustomer" :options="['مؤسسة', 'شركة','مصنع','الصانع','حكومة']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')">
                                </multiselect>
                            </div>
                        </div>

                        <!-- <div class="row form-group">
                <label class="col-form-label col-lg-3 ">{{ $t('AddCustomer.CustomerGroup') }}</label>
                <div class="col-lg-6">
                    <input class="form-control" v-model="newCustomer.customerGroup" type="text">
                </div>
            </div> -->
                        <div class="row form-group" v-if="formName!='Supplier'">
                            <label class="col-form-label col-lg-3">{{ $t('AddCustomer.CustomerGroup') }}</label>
                            <div class="col-lg-6">
                                <customergroupdropdown v-model="newCustomer.customerGroupId" :modelValue="newCustomer.customerGroupId" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-3">
                                {{ $t('AddCustomer.CustomerPhone') }} <br />
                                <a v-if="!isSkype" href="javascript:void(0)" class="text-primary" v-on:click="isSkype=true">{{ $t('AddCustomer.Addmoredetails') }}</a>
                            </label>
                            <div class="col-lg-3 form-group">
                                <input :placeholder="$t('AddCustomer.WorkPhone')" v-model="newCustomer.telephone" class="form-control" type="text">
                            </div>
                            <div class="col-lg-3 form-group">
                                <input :placeholder="$t('AddCustomer.Mobile')" v-model="newCustomer.contactNo1" class="form-control" type="text">
                            </div>
                            <div class="col-lg-3 form-group">
                                <input :placeholder="$t('WhatsApp Number')" v-model="newCustomer.whatsAppNumber" class="form-control" type="text">
                            </div>
                        </div>
                        <div class="row form-group" v-if="isSkype">
                            <label class="col-form-label col-lg-3">{{ $t('AddCustomer.SkypeName') }}</label>
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
                            <div class="form-group col-lg-3">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="newCustomer.isActive">
                                    <label for="inlineCheckbox1"> {{ $t('AddCustomer.Active') }} </label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="row mb-5 pb-3">
                    <div class="accordion col-md-12" id="accordionExample">
                        <div class="accordion-item">
                            <h5 class="accordion-header m-0" id="headingOne">
                                <button class="accordion-button fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                    <b>{{ $t('AddCustomer.OfficialInformation') }}</b>
                                </button>
                            </h5>
                            <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col-lg-8">
                                            <div class="row form-group">
                                                <label class="col-lg-5">{{ $t('AddCustomer.CommercialRegistrationNo') }} : <span class="text-danger" v-if="!newCustomer.isCashCustomer && newCustomer.category == 'B2B – Business to Business'"> *</span></label>
                                                <div class="col-lg-7">
                                                    <input v-model="newCustomer.commercialRegistrationNo" class="form-control" type="text">
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-5">{{ $t('AddCustomer.VAT/NTN/Tax No') }} : <span class="text-danger" v-if="!newCustomer.isCashCustomer && newCustomer.category == 'B2B – Business to Business'"> *</span></label>
                                                <div class="col-lg-7">
                                                    <input v-model="newCustomer.vatNo" class="form-control" type="text">
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-5">Customer Code : </label>
                                                <div class="col-lg-7">
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
                                    <b>{{ $t('AddCustomer.PaymentTerms') }}</b>
                                </button>
                            </h5>
                            <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col-lg-8">
                                            <div class="row form-group">
                                                <label class="col-lg-3">{{ $t('AddCustomer.PaymentTerms') }}</label>
                                                <div class="col-lg-3 form-group">
                                                    <multiselect v-model="newCustomer.paymentTerms" v-bind:disabled="newCustomer.isCashCustomer" :preselect-first="true" v-if="($i18n.locale == 'en' ||isLeftToRight()) " :options="[ 'Cash', 'Credit']" :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                                                    </multiselect>
                                                    <multiselect v-else v-model="newCustomer.paymentTerms" v-bind:disabled="newCustomer.isCashCustomer" :preselect-first="true" :options="[ 'نقد', 'آجل']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')">
                                                    </multiselect>
                                                </div>
                                            </div>
                                            <div class="row form-group" v-if="newCustomer.paymentTerms=='Credit'">
                                                <label class="col-lg-3"></label>
                                                <div class="col-lg-3 form-group">
                                                    <input v-model="newCustomer.creditLimit" :placeholder="$t('AddCustomer.CreditLimit')" class="form-control" type="number">
                                                </div>
                                                <div class="col-lg-3 form-group">
                                                    <input v-model="newCustomer.creditPeriod" :placeholder="$t('AddCustomer.CreditPeriod')" class="form-control" type="number">
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-3">{{ $t('AddCustomer.DeliveryTerm') }}</label>
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
                                    <b>{{ $t('AddCustomer.OtherDetails') }}</b>
                                </button>
                            </h5>
                            <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col-lg-8">

                                            <div class="row form-group">
                                                <label class="col-lg-3 ">Address Line 1</label>
                                                <div class="col-lg-6">
                                                    <textarea v-model="newCustomer.billingAddress" rows="3" class="form-control "></textarea>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-3 ">Address Line 2</label>
                                                <div class="col-lg-6">
                                                    <textarea v-model="newCustomer.deliveryAddress" rows="3" class="form-control "></textarea>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-3 ">{{ $t('AddCustomer.Currency') }}</label>
                                                <div class="col-lg-6">
                                                    <currency-dropdown v-model="newCustomer.currencyId" :modelValue="newCustomer.currencyId" />
                                                </div>
                                            </div>
                                            <div class="row form-group" v-if="formName!='Supplier'">
                                                <label class="col-lg-3 ">Region :</label>
                                                <div class="col-lg-6">
                                                    <areadropdown v-model="newCustomer.regionId" :modelValue="newCustomer.regionId" />
                                                </div>
                                            </div>
                                            <div class="row form-group" v-if="formName!='Supplier'">
                                                <label class="col-lg-3">{{ $t('AddCustomer.TaxRate') }} </label>
                                                <div class="col-lg-6">
                                                    <taxratedropdown v-model="newCustomer.taxRateId" :modelValue="newCustomer.taxRateId" />
                                                    <div class="form-text">
                                                        <small>{{ $t('AddCustomer.ToAssociateMore') }}</small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-group mt-4">
                                                <label class="col-lg-3">{{ $t('AddCustomer.Facebook') }}</label>
                                                <div class="col-lg-6">

                                                    <div class="input-group ">
                                                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fab fa-facebook-square"></i></button>
                                                        <input type="text" class="form-control" placeholder="" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row form-group">
                                                <label class="col-lg-3">{{ $t('AddCustomer.Twitter') }}</label>
                                                <div class="col-lg-6">
                                                    <div class="input-group ">
                                                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fab fa-twitter-square"></i></button>
                                                        <input type="text" class="form-control" placeholder="" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                    </div>
                                                    <!--<div class="input-group mb-0">
                                            <div class="input-group-prepend">
                                                <span style="background- : #e3ebf1;border: 1px solid #ffffff00;" class="input-group-text"><i class="fab fa-twitter-square"></i></span>
                                            </div>
                                            <input type="text" class="form-control" style="border-left: 1 !important;">
                                        </div>
                                        <span class="text-muted font-xs">http://www.twitter.com/<strong></strong></span>-->

                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-3">{{ $t('AddCustomer.Website') }}</label>
                                                <div class="col-lg-6">
                                                    <input v-model="newCustomer.website" class="form-control" type="text">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">

                                            <div class="row form-group">
                                                <label class="col-lg-6 ">City:</label>
                                                <div class="col-lg-12">
                                                    <input v-model="newCustomer.billingCity" class="form-control" type="text">
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-6">Country:</label>
                                                <div class="col-lg-12">
                                                    <countrydropdown v-model="newCustomer.billingCountry"></countrydropdown>
                                                    <!-- <input v-model="newCustomer.billingCountry" class="form-control" type="text"> -->
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label class="col-lg-6 ">Zip/Postal Code:</label>
                                                <div class="col-lg-12">
                                                    <input v-model="newCustomer.billingZipCode" class="form-control" type="text">
                                                </div>
                                            </div>


                                            <div class="form-check form-switch form-switch-dark">
                                                <input class="form-check-input" type="checkbox" id="customSwitchSecondary" v-model="newCustomer.isAddressOnAll">
                                                <label class="form-check-label" for="customSwitchSecondary">Address on All</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" v-if="formName!='Supplier'">
                            <h5 class="accordion-header m-0" id="headingFour">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                    <b>{{$t('AddCustomer.ContactPersons')}}</b>
                                </button>
                            </h5>
                            <div id="collapseFour" class="accordion-collapse collapse" aria-labelledby="headingFour" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <table class="table mb-0">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th style="width: 50px;">{{ $t('AddCustomer.Salutation') }}</th>
                                                        <th style="width: 50px">{{ $t('AddCustomer.FirstName') }}</th>
                                                        <th style="width: 50px">{{ $t('AddCustomer.LastName') }}</th>
                                                        <th style="width: 80px">{{ $t('AddCustomer.EmailAddress') }}</th>
                                                        <th style="width: 50px">{{ $t('AddCustomer.WorkPhone') }}</th>
                                                        <th style="width: 50px">{{ $t('AddCustomer.Mobile') }}</th>
                                                        <th style="width: 30px"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(person , index) in newCustomer.contactPersonList" :key="index">
                                                        <td class="border-top-0 text-center">
                                                            <salutation-dropdown v-model="person.prefix" :modelValue="person.prefix" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.firstName" @focus="$event.target.select()" class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.lastName" @focus="$event.target.select()" class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.email" @focus="$event.target.select()" class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.phone" @focus="$event.target.select()" class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 text-center">
                                                            <input type="text" v-model="person.mobile" @focus="$event.target.select()" class="form-control input-border tableHoverOn" />
                                                        </td>
                                                        <td class="border-top-0 pt-0 text-end">
                                                            <button title="Remove Item" id="bElim" type="button" class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow(index)">
                                                                <i class="dripicons-trash" aria-hidden="true"></i>
                                                            </button>
                                                            <!--<button title="Remove Item" v-on:click="RemoveRow(index)"
                                                        class="btn btn-secondary btn-neutral btn-round btn-sm  btn-icon">
                                                    <i class="nc-icon nc-simple-remove"></i>
                                                </button>-->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" class="border-top-0">
                                                            <button id="but_add" class="btn btn-success btn-sm" v-on:click="AddRow()">+ {{ $t('AddCustomer.AddContactPerson') }}</button>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" v-if="multipleAddress">
                            <h5 class="accordion-header m-0" id="headingSeven">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSeven" aria-expanded="false" aria-controls="collapseSeven">
                                    <b>Addresses</b>
                                </button>
                            </h5>
                            <div id="collapseSeven" class="accordion-collapse collapse" aria-labelledby="headingSeven" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col"></div>
                                        <div class="col-auto align-self-center mt-1 mb-1">
                                            <a v-on:click="AddRow1()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                                <i class="align-self-center icon-xs ti-plus"></i>
                                                {{ $t('color.AddNew') }}
                                            </a>
                                        </div>
                                        <div class="col-lg-12 pb-2">
                                            <table class="table mb-0">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th class="text-center" style="width:2%">#</th>
                                                        <th class="text-center" style="width:10%">Address For</th>
                                                        <th class="text-center" style="width:10%">Location Type</th>
                                                        <th class="text-center" style="width:12%">Contact Name</th>
                                                        <th class="text-center" style="width:13%">Contact Number</th>
                                                        <th class="text-center text-truncate" style="width:31%">Address</th>
                                                        <th class="text-center" style="width:5%">Default</th>
                                                        <th class="text-center" style="width:5%">Status</th>
                                                        <th class="text-center" style="width:12%"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(person , index) in newCustomer.deliveryAddressList" :key="index">
                                                        <td class="text-center">
                                                            {{ index+1 }}

                                                        </td>
                                                        <td class="text-center">
                                                            {{ person.type }}

                                                        </td>
                                                        <td class="text-center">
                                                            {{ person.area }}

                                                        </td>
                                                        <td class="text-center ">
                                                            {{ person.contactName }}

                                                        </td>
                                                        <td class="text-center ">
                                                            {{ person.contactNumber }}

                                                        </td>
                                                        <td class="text-center text-truncate" style="max-width: 50px !important;">
                                                            {{ person.address }}

                                                        </td>
                                                        <td class="text-center">
                                                            <span v-if="person.isDefault" class="badge badge-boxed  badge-outline-success">{{$t('color.Active')}}</span>
                                                            <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('color.De-Active')}}</span>
                                                        </td>

                                                        <td class="text-center">
                                                            <span v-if="person.isActive" class="badge badge-boxed  badge-outline-success">{{$t('color.Active')}}</span>
                                                            <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('color.De-Active')}}</span>
                                                        </td>

                                                        <td class="border-top-0 pt-0 text-end">
                                                            <button id="bEdit" type="button" class="btn btn-sm btn-soft-success btn-circle me-1" v-on:click="EditDetail(person)"><i class="dripicons-pencil"></i></button>
                                                            <button id="Duplicate" type="button" class="btn btn-sm btn-soft-primary btn-circle me-1" v-on:click="Duplicate(person)"><i class="dripicons-copy"></i></button>
                                                            <button title="Remove Item" id="bElim" type="button" class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow1(index)">
                                                                <i class="dripicons-trash" aria-hidden="true"></i>
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
                        <div class="accordion-item">
                            <h5 class="accordion-header m-0" id="headingFive">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                    <b>{{ $t('AddCustomer.Remarks') }}</b>
                                </button>
                            </h5>
                            <div id="collapseFive" class="accordion-collapse collapse" aria-labelledby="headingFive" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col-lg-8">
                                            <div class="row form-group">
                                                <div class="col-lg-8">
                                                    <label class="col-form-label">{{ $t('AddCustomer.Remarks') }} ({{ $t('AddCustomer.ForInternalUse') }})</label>
                                                    <textarea v-model="newCustomer.remarks" rows="3" class="form-control"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" v-if="formName!='Supplier'">
                            <h5 class="accordion-header m-0" id="headingSix">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                    <b>{{ $t('AddCustomer.Pricing') }}</b>
                                </button>
                            </h5>
                            <div id="collapseSix" class="accordion-collapse collapse" aria-labelledby="headingSix" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="row">
                                        <div class="col-md-6" v-if="isCustomerPriceLabel">
                                            <div class="row form-group">
                                                <div class="col-sm-12">
                                                    <priceLabelingDropdown v-model="priceLabelId" :modelValue="newCustomer.priceLabelId" v-on:input="GetPriceLabeling(priceLabelId)" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" v-if="!isCustomerPriceLabel">
                                            <div class="row form-group">
                                                <div class="col-sm-12">
                                                    <product-dropdown @update:modelValues="addProduct" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" v-if="!isCustomerPriceLabel">
                                            <table class="table mt-3">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>{{ $t('AddCustomer.Code') }}</th>
                                                        <th>
                                                            {{ $t('AddCustomer.Name') }}
                                                        </th>
                                                        <th>
                                                            {{ $t('AddCustomer.SalePrice') }}
                                                        </th>
                                                        <th>
                                                            {{ $t('AddCustomer.PurchasePrice') }}
                                                        </th>
                                                        <th>
                                                            {{ $t('AddCustomer.CostPrice') }}
                                                        </th>
                                                        <th>
                                                            {{ $t('AddCustomer.Price') }}
                                                        </th>
                                                        <th>{{ $t('AddCustomer.Action') }}</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(color,index) in newCustomer.productList" v-bind:key="index">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <td>{{ color.code }}</td>
                                                        <td>
                                                            <strong>
                                                                <a href="javascript:void(0)">{{color.displayName}}</a>
                                                            </strong>
                                                        </td>
                                                        <td>{{color.salePrice}}</td>
                                                        <td>{{color.purchasePrice}}</td>
                                                        <td>{{color.costPrice}}</td>
                                                        <td>
                                                            <decimal-to-fixed :textAlignLeft="true" v-model="color.price" />
                                                        </td>
                                                        <td>
                                                            <a href="javascript:void(0);" @click="removeProduct(index)"><i class="las la-trash-alt text-secondary font-16"></i></a>
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
                </div>
                <div class="row form-group">
                    <div class="col-lg-12 mt-4 mb-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-6" style="border-right: 1px solid #eee;">
                                        <div class="form-group ps-3">
                                            <div class="font-xs mb-1">{{ $t('AddCustomer.Attachments') }}</div>
                                            <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('PurchaseView.Attachment') }} </button>
                                            <div>
                                                <small class="text-muted">
                                                    {{ $t('AddCustomer.FileSize') }}
                                                </small>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <AddAddress :newaddress="newAddress" :show="show1" :isSale="false" :list="newCustomer.deliveryAddressList" v-if="show1" @close="show1=false" @IsSave="IsSave" :type="type" />

                <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                <bulk-attachment :attachmentList="newCustomer.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
            </div>

        </div>
        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class=" col-md-12">
                    <div class="button-items" v-if="newCustomer.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddCustomer')">
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.newCustomer.$invalid" v-if="newCustomer.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddCustomer')" v-on:click="SaveCustomer"><i class="far fa-save "></i> {{ $t('AddCustomer.btnSave') }}</button>
                        <button class="btn btn-danger" v-on:click="Cancel">{{ $t('AddCustomer.Cancel') }}</button>
                    </div>
                    <div class="button-items" v-else>
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.newCustomer.$invalid" v-if="newCustomer.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditCustomer')" v-on:click="SaveCustomer"><i class="far fa-save "></i> {{ $t('AddCustomer.btnUpdate') }}</button>
                        <button class="btn btn-danger" v-on:click="Cancel">{{ $t('AddCustomer.Cancel') }}</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
   import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from 'moment'
    import Multiselect from 'vue-multiselect'
    import { required , maxLength} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    

    export default ({
        mixins: [clickMixin],
        
        components: {
            Multiselect,
            Loading,

        },
        setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                render:0,
                options: [],
                formNames: '',
                priceLabelId: '',
                isCustomerPriceLabel: false,
                arabic: '',
                show: false,
                show1: false,
                multipleAddress: false,
                type: '',
                isCashCustomer: false,
                b2b: false,
                b2c: false,
                english: '',
                contactCode: '',
                cashCustomerCode: '',

                isSkype: false,
                salutatioRender: 0,
                randerElement: 0,
                randerInput: 0,
                newAddress: {
                    id: '',
                    area: '',
                    address: '',
                    address2: '',
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
                },
                customerType: '',
                displayName: '',

                newCustomer: {
                    date:'',
                    isAllowEmail: false,
                    isAutoEmail: false,
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    multipleAddress: false,
                    isAddressOnAll: false,
                    priceLabelId: '',
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
                    customerGroup: '',
                    contactNo1: '',
                    whatsAppNumber:'',
                    commercialRegistrationNo: '',
                    vatNo: '',
                    customerGroupId: '',
                    currencyId: '',
                    regionId: '',
                    taxRateId: '',
                    customerCode: '',
                    website: '',

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

                    contactPersonList: [{
                        prefix: '',
                        firstName: '',
                        lastName: '',
                        email: '',
                        phone: '',
                        mobile: ''
                    }],
                    deliveryAddressList: [],
                    productList: [],
                    remarks: '',
                    isCustomer: true,
                    isActive: true,
                    isCashCustomer: false,

                    paymentTerms: '',
                    deliveryTerm: '',
                    creditLimit: '',
                    creditPeriod: '',

                    attachmentList: []
                },
                loading: false,
                language: 'Nothing',

            }
        },
        validations() {
            return{
                newCustomer: {
                    customerType: {
                    },
                    englishName: {

                        maxLength: maxLength(250)
                    },
                    arabicName: {
                        maxLength: maxLength(250)
                    },
                    vatNo: {
                       
                        maxLength: maxLength(250)
                    },
                    paymentTerms: {
                    },
                    customerDisplayName: {
                        required
                    },
                }
            }
        },

        methods: {
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
            changeCustomerType: function(x)
            {
                if(x)
                {
                    this.newCustomer.customerType = '';
                }
                else
                {
                    this.newCustomer.customerType = 'Individual';
                }
                
            },
            GetPriceLabeling: function (val) {
                this.newCustomer.priceLabelId = val.id;
            },
            toDecimal: function (price) {
                return parseFloat(price).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            addProduct: function (productId, newProduct) {
                

                var prod = this.newCustomer.productList.find(x=> x.productId == productId);

                if (prod == undefined) {
                    if (newProduct.code != undefined) {
                        this.newCustomer.productList.push({
                            productId: productId,
                            productName: newProduct.englishName,
                            code: newProduct.code,
                            salePrice: newProduct.salePrice,
                            purchasePrice: newProduct.purchasePrices,
                            costPrice: newProduct.costPrice,
                            displayName: newProduct.displayName,
                            price: 0.00
                        });
                    }
                }
            },
            removeProduct: function (index) {
                this.newCustomer.productList.splice(index, 1);
            },
            isCashCustomerFunc: function () {

                if (this.newCustomer.isCashCustomer) {

                    this.newCustomer.customerType = 'Individual';
                    this.newCustomer.paymentTerms = 'Cash';

                } else {
                    this.newCustomer.customerType = '';
                    this.newCustomer.paymentTerms = 'Credit';
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

                        this.$router.go('/addCustomer');
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

            GetAutoCodeGenerator: function () {
                this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                let customer=true;
                if(this.formNames == 'Supplier')
                {
                    customer=false;
                }

                root.$https
                    .get('/Contact/AutoGenerateCode?isCustomer='+customer + '&isCashCustomer=' + this.isCashCustomer, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {

                            root.contactCode = response.data.contact;
                            root.cashCustomerCode = response.data.cashCustomer;
                            root.randerInput++;

                        }
                    });
            },

            DisplayName: function () {
                this.getData();
            },

            SaveCustomer: function () {

                if(this.$route.query.formName == 'Supplier')
            {

                this.newCustomer.isCustomer=false;
                
            }
          





                if (this.newCustomer.isCashCustomer) {
                    this.newCustomer.code = this.cashCustomerCode;

                } else {
                    this.newCustomer.code = this.contactCode;
                }

                if (this.newCustomer.code == '') {
                    return this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Code cannot null' : 'حفظ بنجالا يمكن أن يكون الرمز فارغًا',
                        text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error' : 'خطأ',
                        type: 'Error',
                        confirmButtonClass: "btn btn-danger",
                        buttonStyling: false,
                        icon: 'Error',
                        timer: 1500,
                        timerProgressBar: true,

                    })
                }

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var prd = root.newCustomer.deliveryAddressList.findIndex(x => x.address == '' || x.address == null);
                if (prd >= 0) {
                    root.newCustomer.deliveryAddressList.splice(prd, 1)
                }
                var av = root.newCustomer.contactPersonList.findIndex(x => x.firstName == '' || x.firstName == null && x.lastName == '' || x.lastName == null);
                if (av >= 0) {
                    root.newCustomer.contactPersonList.splice(prd, 1)
                }
                this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

                this.newCustomer.multipleAddress = this.multipleAddress;

                root.$https
                    .post('/Contact/SaveContact', this.newCustomer, {
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

                            }).then(function (ok) {
                                if (ok != null) {
                                     {
                                        if(root.$route.query.formName == 'Supplier')
                                        {
                                            root.$router.push({
                                            path: '/Supplier',

                                        })
                                        }
                                        else
                                        {
                                            root.$router.push({
                                            path: '/Customer',

                                        })
                                        }
                                       
                                    } 
                                }
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

                            }).then(function (ok) {
                                if (ok != null) {
                                     {
                                        if(root.$route.query.formName == 'Supplier')
                                        {
                                            root.$router.push({
                                            path: '/Supplier',

                                        })
                                        }
                                        else
                                        {
                                            root.$router.push({
                                            path: '/Customer',

                                        })
                                        }
                                    } 
                                }
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

            AddRow: function () {

                this.newCustomer.contactPersonList.push({
                    prefix: '',
                    firstName: '',
                    lastName: '',
                    email: '',
                    phone: '',
                    mobile: ''
                });

            },
            IsSave: function (value) {

                var root = this;
                if (value.isAlreadyDefault) {
                    value.isAlreadyDefault = false
                }
                if (value.isFirst) {
                    value.isFirst = false
                }


                if (value.add == 'Add' || value.add == 'Duplicate') {
                    if (value.isDefault) {
                        value.isOffice = false;
                        if (this.newCustomer.deliveryAddressList.length > 0) {
                            this.newCustomer.deliveryAddressList.forEach(function (cat) {
                                if (cat.type == value.type) {
                                    cat.isDefault = false;
                                    cat.isOffice = false;
                                }
                            })
                        }
                    }

                    this.newCustomer.deliveryAddressList.push(value);
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
                    if (value.isDefault) {
                        value.isOffice = false;
                        if (this.newCustomer.deliveryAddressList.length > 0) {
                            this.newCustomer.deliveryAddressList.forEach(function (cat) {
                                if (cat.type == value.type && value.id != cat.id) {
                                    cat.isDefault = false;
                                    cat.isOffice = false;
                                }
                            })
                        }

                        let findValue = this.newCustomer.deliveryAddressList.findIndex(x => x.id == value.id);
                        if (findValue != null && findValue != undefined) {
                            this.newCustomer.deliveryAddressList[findValue] = value;
                        }
                    }
                    else

                    {
                        let findValue = this.newCustomer.deliveryAddressList.findIndex(x => x.id == value.id);
                        if (findValue != null && findValue != undefined) {
                            this.newCustomer.deliveryAddressList[findValue] = value;
                        }
                    }
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Updated Successfully' : 'حفظ بنجاح',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Updated' : 'تم الحفظ',
                        type: 'success',
                        confirmButtonClass: "btn btn-success",
                        buttonStyling: false,
                        icon: 'success',
                        timer: 1500,
                        timerProgressBar: true,

                    });

                }

                this.show1 = false;

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
            RemoveRow: function (index) {
                this.newCustomer.contactPersonList.splice(index, 1);
            },
            AddRow1: function () {
                 //eslint-disable-line

                this.newAddress = {
                    id: this.createUUID(),
                    area: '',
                    address: '',
                    address2: '',
                    city: '',
                    country: '',
                    billingZipCode: '',
                    langitutue: '',
                    latitude: '',
                    contactName: '',
                    contactNumber: '',
                    fromTime: '',
                    toTime: '',
                    add: 'Add',
                    billingPhone: '',
                    deliveryHolidays: [],
                    type: '',
                    isActive: true,
                    allHour: false,
                    isFirst: false,
                    allDaySelection: false,
                    isDefault: false,
                    isOffice: false,
                    isOrignal: false,
                    isAlreadyDefault: false,
                };
                this.show1 = !this.show1;
                this.type = 'Add';
            },
            Duplicate: function (detail) {
                this.newAddress = {
                    id: this.createUUID(),
                    area: detail.area,
                    address: detail.address,
                    address2: detail.address2,
                    country:  detail.country,
                    city: detail.city,
                    billingZipCode: detail.billingZipCode,
                    langitutue: detail.langitutue,
                    latitude: detail.latitude,
                    fromTime: detail.fromTime,
                    toTime: detail.toTime,
                    googleLocation: detail.googleLocation,
                    nearBy: detail.nearBy,
                    add: 'Duplicate',
                    billingPhone: detail.billingPhone,
                    deliveryHolidays: detail.deliveryHolidays,
                    type: '',
                    isActive: detail.isActive,
                    isFirst: false,
                    allHour: detail.allHour,
                    allDaySelection: detail.allDaySelection,
                    contactName: detail.contactName,
                    contactNumber: detail.contactNumber,
                    isOffice: false,
                    isDefault: false,
                    isOrignal: false,
                    isAlreadyDefault: false,
                };
                if (this.newCustomer.isAddressOnAll) {
                    this.newAddress.address = this.newCustomer.billingAddress
                }


                this.show1 = !this.show1;
                this.type = 'Duplicate';

            },
            EditDetail: function (detail) {

                this.newAddress = {
                    id: detail.id,
                    type: detail.type,
                    area: detail.area,
                    address: detail.address,
                    address2: detail.address2,
                    city: detail.city,
                    country: detail.country,
                    billingZipCode: detail.billingZipCode,
                    langitutue: detail.langitutue,
                    latitude: detail.latitude,
                    fromTime: detail.fromTime,
                    toTime: detail.toTime,
                    googleLocation: detail.googleLocation,
                    nearBy: detail.nearBy,
                    add: 'Edit',
                    billingPhone: detail.billingPhone,
                    deliveryHolidays: detail.deliveryHolidays,
                    isActive: detail.isActive,
                    isFirst: false,
                    allHour: detail.allHour,
                    allDaySelection: detail.allDaySelection,
                    contactName: detail.contactName,
                    contactNumber: detail.contactNumber,
                    isOffice: false,
                    isDefault: detail.isOffice,
                    isOrignal: detail.isDefault ? true : false,
                    isAlreadyDefault: false,
                };
                this.show1 = !this.show1;
                this.type = 'Edit';

            },
            RemoveRow1: function (index) {
                this.newCustomer.deliveryAddressList.splice(index, 1);
            },

            Cancel: function () {
                if(this.$route.query.formName == 'Supplier')
                                        {
                                            this.$router.push({
                                            path: '/Supplier',

                                        })
                                        }
                                        else
                                        {
                                            this.$router.push({
                                            path: '/Customer',

                                        })
                                        }
            },

        },
        created: function () {
            var root =  this;

            if(root.$route.query.formName == 'Supplier')
            {

                this.formNames = 'Supplier';
                
            }
            else
            {
                this.formNames = 'Customer';
            }


            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }

            this.newCustomer.date = moment().format("DD MMM YYYY hh:mm A");

            if(this.newCustomer.category == 'B2B – Business to Business')
            {
                this.customerType = ['Establishment', 'Company','Factory','Manufacturer','Government'];
            }
            else
            {
                this.customerType = 'Individual'
            }

            this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;
            this.$emit('update:modelValue', this.$route.name)
            if (this.$route.query.data != undefined) {
                this.newCustomer = this.$route.query.data;
                this.newCustomer.date =  this.newCustomer.date == null ? moment().format("DD MMM YYYY hh:mm A") : moment(this.newCustomer.date).format('DD MMM YYYY hh:mm A');
                this.cashCustomerCode = this.newCustomer.code;
                this.contactCode = this.newCustomer.code;
                this.contactCode = this.newCustomer.code;
                this.randerInput++;
                this.getData();
                this.displayName=this.newCustomer.customerDisplayName;
                this.randerElement++;

            } else {
                this.GetAutoCodeGenerator();
                this.newCustomer.registrationDate = moment().format('llll');
                if (localStorage.getItem('IsCustomeCredit') != 'true') {
                    this.newCustomer.paymentTerms = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'نقد'
                } else {
                    this.newCustomer.paymentTerms = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Credit' : 'آجل'
                }

                this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
                this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
                if (this.b2b && !this.b2c) {
                    this.newCustomer.category = 'B2B – Business to Business';
                }
                if (!this.b2c && this.b2c) {
                    this.newCustomer.category = 'B2C – Business to Client';
                } else {
                    this.newCustomer.category = 'B2C – Business to Client';
                }
            }
        },
        mounted: function () {
            if(this.newCustomer.category == 'B2C – Business to Client')
            {
                this.newCustomer.customerType = 'Individual';
            }

            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

            this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;
            this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

            this.isCustomerPriceLabel = localStorage.getItem('IsCustomerPriceLabel') == 'true' ? true : false;

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
