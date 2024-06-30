<template>

    <div class="row">
        <!--class="col-sm-12 offset-sm-1"-->
        <div class="col-sm-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PrintSetting.InvoiceSetting') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PrintSetting.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PrintSetting.InvoiceSetting') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('color.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-group">
                        <div class="form-group has-label col-sm-4 " v-bind:class="{'has-danger' : v$.printSetting.printSize.$error}">
                            <label class="text  font-weight-bolder"> {{ $t('PrintSetting.SelectPageSize') }}:<span class="text-danger"> *</span> </label>
                            <multiselect :options="optionsPageSize" v-model="printSetting.printSize" @update:modelValue="SelectTemplate(printSetting.printSize,true)" :show-labels="false" placeholder="Select Page Size">
                            </multiselect>
                            <span v-if="v$.printSetting.printSize.$error" class="error">
                                <span v-if="!v$.printSetting.printSize.required">{{ $t('PrintSetting.PageSizeIsRequired') }}</span>
                            </span>
                        </div>

                        <div v-bind:key="templateRender" class="form-group has-label col-sm-4 ">
                            <label class="text  font-weight-bolder"> {{ $t('PrintSetting.Template') }}:<span class="text-danger"> *</span> </label>
                            <multiselect :options="printTemplatelist" v-model="printSetting.printTemplate" :show-labels="false" v-bind:placeholder="$t('SelectInvoiceTemplate')">
                            </multiselect>
                        </div>
                        <div class="col-sm-4">
                            <label class="text  font-weight-bolder" v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">{{ $t('PrintSetting.PrintItemName') }}:<span class="text-danger"> *</span> </label>
                            <label class="text  font-weight-bolder" v-else>{{ $t('PrintSetting.HeaderSetting') }}:<span class="text-danger"> *</span> </label>
                            <multiselect :options="englishOptions" v-model="printSetting.invoicePrint" :show-labels="false" placeholder="Select">
                            </multiselect>
                        </div>


                        <!--<div class="col-sm-4">
                            <label class="text  font-weight-bolder">Walk-In Customer Invoice Type:<span class="text-danger"> *</span> </label>
                            <multiselect :options="walkInInvoiceTypeOptions" v-model="printSetting.walkInInvoiceType" :show-labels="false" placeholder="Select">
                            </multiselect>
                        </div>-->
                        <div class="row">
                            <div class="form-group col-md-12 ">

                                <button type="button" class="btn btn-soft-primary btn-smy  mx-2" v-on:click="printInvoice">{{ $t('PrintSetting.ViewTemplate') }}</button>
                                <a v-on:click="openmodel" href="javascript:void(0);" v-if="isValid('SaleHoldSetup')"
                                   class="btn btn-soft-primary btn-smy mx-1">
                                    {{ $t('Soft Delete Hold Type Setup') }}
                                </a>
                                <a v-on:click="openmodel1" href="javascript:void(0);" v-if="isValid('SaleHoldSetup')"
                                   class="btn btn-soft-primary btn-smy mx-1">
                                    {{ $t('Permanent Delete Hold Type Setup') }}
                                </a>
                                <div class="checkbox form-check-inline mx-2 ">
                                    <input type="checkbox" id="isQuotationPrint" v-model="printSetting.isQuotationPrint">
                                    <label for="isQuotationPrint">{{ $t('PrintSetting.QuotationPrint') }}</label>
                                </div>
                                <div class="checkbox form-check-inline mx-2 ">
                                    <input type="checkbox" id="isBlindPrint" v-model="printSetting.isBlindPrint">
                                    <label for="isBlindPrint">{{ $t('PrintSetting.IsBlindPrint') }}</label>
                                </div>
                                <div class="checkbox form-check-inline mx-2 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة' || printSetting.printSize=='A4'">
                                    <input type="checkbox" id="autoPaymentVoucher" v-model="printSetting.autoPaymentVoucher">
                                    <label for="autoPaymentVoucher">{{ $t('PrintSetting.AutoPaymentVoucher') }}</label>
                                </div>
                                <div class="checkbox form-check-inline mx-2 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة' || printSetting.printSize=='A4'">
                                    <input type="checkbox" id="termAndConditionLength" v-model="printSetting.termAndConditionLength">
                                    <label for="termAndConditionLength">{{ $t('PrintSetting.TermAndConditionLength') }}</label>
                                </div>
                                <div class="checkbox form-check-inline mx-2 " v-if="printSetting.printTemplate == 'SA – Template 6 A4' || printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 6'">
                                    <input type="checkbox" id="continueWithPage" v-model="printSetting.continueWithPage">
                                    <label for="continueWithPage">{{ $t('PrintSetting.ContinueWithPage') }}</label>
                                </div>
                            </div>



                        </div>


                        <div class="form-group has-label col-sm-4 " :key="renderedImage" v-if="printSetting.isQuotationPrint">
                            <label class="text  font-weight-bolder">Quotation Header Image: </label>
                            <PrintSettingImages :imagePath="printSetting.headerImage1" v-on:picPath="getHeaderImage" v-bind:path="printSetting.headerImage1ForPrint" />
                        </div>
                        <div class="form-group has-label col-sm-4 " :key="renderedImage2" v-if="printSetting.isQuotationPrint">
                            <label class="text  font-weight-bolder">Proposal Image: </label>
                            <PrintSettingImages :imagePath="printSetting.proposalImage" v-on:picPath="getProposalImage" v-bind:path="printSetting.proposalImageForPrint" />
                        </div>
                        <div class="form-group has-label col-sm-4 " :key="renderedImage2" v-if="printSetting.isQuotationPrint">
                            <label class="text  font-weight-bolder">Tags Images: </label>
                            <PrintSettingImages :imagePath="printSetting.tagsImages" v-on:picPath="getTagsImage" v-bind:path="printSetting.tagImageForPrint" />
                        </div>
                        <div class="form-group has-label col-sm-4 " :key="renderedImage" v-if="printSetting.printTemplate == 'SA – Template 6 A4' || printSetting.printTemplate == 'SA – Template 2 A4'  || printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 6'">
                            <label class="text  font-weight-bolder">Header Image: </label>
                            <PrintSettingImages :imagePath="printSetting.headerImage" v-on:picPath="getImage" v-bind:path="printSetting.headerImageForPrint" />
                        </div>
                        <div class="form-group has-label col-sm-4 " :key="renderedImage" v-if="printSetting.printTemplate == 'SA – Template 6 A4' || printSetting.printTemplate == 'SA – Template 2 A4'  || printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 6'">
                            <label class="text  font-weight-bolder">Footer Image: </label>
                            <PrintSettingImages :imagePath="printSetting.footerImage" v-on:picPath="getImage1" v-bind:path="printSetting.footerImageForPrint" />
                        </div>
                        <div class="form-group has-label col-sm-4 " :key="renderedImage" v-if="printSetting.printTemplate == 'SA – Template 6 A4' || printSetting.printTemplate == 'SA – Template 2 A4'  || printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 6'">
                            <label class="text  font-weight-bolder">WarrantyImage: </label>
                            <PrintSettingImages :imagePath="printSetting.warrantyImage" v-on:picPath="getImageForWarranty" v-bind:path="printSetting.warrantyImageForPrint" />

                        </div>





                        <div class="form-group has-label col-sm-4 " v-if="printSetting.isBlindPrint">
                            <label class="text  font-weight-bolder">{{ $t('AddTerminal.Printer') }}: </label>
                            <printer-list-dropdown v-model="printSetting.printerName" v-bind:key="renderPrinter" :modelValue="printSetting.printerName"></printer-list-dropdown>
                        </div>
                        <div class="form-group has-label col-sm-4 " v-if="!isDayStart">
                            <label class="text  font-weight-bolder">   {{$t('PrintSetting.BankAccount')}}: </label>
                            <accountdropdown v-model="printSetting.bankAccountId" :formName="bank" :modelValue="printSetting.bankAccountId" v-bind:key="bankRander"></accountdropdown>
                        </div>
                        <div class="form-group has-label col-sm-4 " v-if="!isDayStart">
                            <label class="text  font-weight-bolder">   {{ $t('PrintSetting.CashAccount') }}: </label>
                            <accountdropdown v-model="printSetting.cashAccountId" :formName='cash' :modelValue="printSetting.cashAccountId" v-bind:key="bankRander"></accountdropdown>
                        </div>


                        <div class="accordion" id="accordionExample">
                            <div class="accordion-item">
                                <h5 class="accordion-header m-0" id="headingOne">
                                    <button class="accordion-button fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                        {{ $t('PrintSetting.Permission') }}
                                    </button>
                                </h5>
                                <div id="collapseOne" class="accordion-collapse collapse " aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.WelcomeLine') }}: </label>
                                                <input class="form-control" v-bind:disabled="welcomeEn" v-model="printSetting.welcomeLineEn" type="text" />
                                            </div>
                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.WelcomeLineAr') }}: </label>
                                                <input class="form-control text-right" v-bind:disabled="welcomeAr" v-model="printSetting.welcomeLineAr" type="text" />
                                            </div>
                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.GratitudeMessage') }}: </label>
                                                <input class="form-control" v-bind:disabled="gratitudeMessageEn" v-model="printSetting.closingLineEn" type="text" />
                                            </div>
                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.GratitudeMessageAr') }}: </label>
                                                <input class="form-control text-right" v-bind:disabled="gratitudeMessageAr" v-model="printSetting.closingLineAr" type="text" />
                                            </div>
                                            <div class="form-group has-label" v-bind:class="printSetting.printSize!='A4 Invoice' ? ' col-sm-4' : ' col-sm-4'">
                                                <label class="text  font-weight-bolder">   {{ $t('PrintSetting.ReturnDays') }}: </label>
                                                <input class="form-control" v-model="printSetting.returnDays" @focus="$event.target.select()" type="number" />
                                            </div>
                                            <div class="form-group has-label" v-bind:class="printSetting.printSize!='A4 Invoice' ? ' col-sm-4' : ' col-sm-4'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.DaysforItemExchange') }}: </label>
                                                <input class="form-control" v-model="printSetting.exchangeDays" @focus="$event.target.select()" type="number" />
                                            </div>

                                            <div class="form-group has-label" v-bind:class="printSetting.printSize!='A4 Invoice' ? ' col-sm-4' : ' col-sm-4'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.StoreContactNo') }}: </label>
                                                <input class="form-control" v-bind:disabled="storeContact" v-model="printSetting.contactNo" @focus="$event.target.select()" type="number" />
                                            </div>
                                            <div class="form-group has-label" v-bind:class="printSetting.printSize!='A4 Invoice' ? ' col-sm-4' : ' col-sm-4'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.ManagementNo') }}: </label>
                                                <input class="form-control" v-bind:disabled="managementNo" v-model="printSetting.managementNo" @focus="$event.target.select()" type="number" />
                                            </div>

                                            <div class="form-group has-label col-sm-4" v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.BusinessAddressEnglish') }}: </label>
                                                <input class="form-control" v-bind:disabled="businessAddressEn" v-model="printSetting.businessAddressEnglish" @focus="$event.target.select()" type="text" />
                                            </div>
                                            <div class="form-group has-label col-sm-4" v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                                <label class="text  font-weight-bolder">{{ $t('PrintSetting.BusinessAddressArabic')}}: </label>
                                                <input class="form-control" v-bind:disabled="businessAddressAr" v-model="printSetting.businessAddressArabic" @focus="$event.target.select()" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-lg-12" v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                            <div class="row">
                                                <template v-for="(item) in printSetting.printOptions">
                                                    <div class=" col-4  " v-if="item.type === 'Header'" v-bind:key="item.id">
                                                        <div class="checkbox form-check-inline mx-2">
                                                            <input type="checkbox" v-bind:id="item.label" @update:modelValue="SavePrintOption(item)" v-model="item.value">
                                                            <label v-bind:for="item.label">{{$i18n.locale == 'ar' ? item.labelNameArabic: item.label }}</label>
                                                        </div>


                                                        <!--<toggle-button v-model="item.value" class="pr-2 pl-2 pt-2" color="#3178F6" @change="SavePrintOption(item)" />
                                                        <label class="text  font-weight-bolder">{{$i18n.locale == 'ar' ? item.labelNameArabic: item.label }}</label>-->
                                                    </div>
                                                </template>
                                            </div>
                                        </div>
                                        <div class="col-lg-12" v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                            <div class="row">
                                                <div class="col-12"> <hr /></div>
                                                <template v-for="item in printSetting.printOptions">
                                                    <div class=" col-4  " v-if="item.type === 'LineItem'" v-bind:key="item.id">
                                                        <div class="checkbox form-check-inline mx-2">
                                                            <input type="checkbox" v-bind:id="item.label" @update:modelValue="SavePrintOption(item)" v-model="item.value">
                                                            <label v-bind:for="item.label">{{$i18n.locale == 'ar' ? item.labelNameArabic: item.label }}</label>
                                                        </div>

                                                    </div>
                                                </template>
                                            </div>


                                        </div>
                                        <div class="col-lg-12" v-if="printSetting.printSize === 'Thermal Invoice' || printSetting.printSize === 'الـفـاتـورة طـابـعـة حـراريـة'">
                                            <div class="row">
                                                <div class="col-12"> <hr /></div>
                                                <template v-for="item in printSetting.printOptions">
                                                    <div class=" col-4  " v-if="item.type === 'Footer'" v-bind:key="item.id">
                                                        <div class="checkbox form-check-inline mx-2">
                                                            <input type="checkbox" v-bind:id="item.label" @update:modelValue="SavePrintOption(item)" v-model="item.value">
                                                            <label v-bind:for="item.label">{{$i18n.locale == 'ar' ? item.labelNameArabic: item.label }}</label>
                                                        </div>

                                                    </div>
                                                </template>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <!--<toggle-button v-model="item.value" class="pr-2 pl-2 pt-2" color="#3178F6" @change="SavePrintOption(item)" />
                                                <label class="text  font-weight-bolder">{{$i18n.locale == 'ar' ? item.labelNameArabic: item.label }}</label>-->
                                            <!--<div class="form-group has-label col-sm-4  ">
                                            </div>-->
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة' && english=='true' ">

                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="englishName" v-model="printSetting.englishName">
                                                    <label for="englishName">{{ $t('PrintSetting.ItemDescription(Eng)') }}</label>
                                                </div>


                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة' && isOtherLang() ">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="arabicName" v-model="printSetting.arabicName">
                                                    <label for="arabicName">{{ $t('PrintSetting.ItemDescription(Ar)') }}}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="isHeaderFooter" v-model="printSetting.isHeaderFooter">
                                                    <label for="isHeaderFooter">{{ $t('PrintSetting.Header') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="customerNameEn" v-model="printSetting.customerNameEn">
                                                    <label for="customerNameEn">{{ $t('PrintSetting.CustomerNameEnglish') }} </label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="customerNameAr" v-model="printSetting.customerNameAr">
                                                    <label for="customerNameAr">{{ $t('PrintSetting.CustomerNameArabic') }}</label>
                                                </div>
                                            </div>
                                            <!--<div class=" col-sm-4 " v-if="printSetting.printSize=='A4'">
                                                <span>
                                                    <toggle-button v-model="printSetting.showBankAccount" class="pr-2 pl-2 pt-2" color="#3178F6"/>
                                                    <label class="text  font-weight-bolder">{{ $t('PaymentVoucher.BankAccount') }} </label>
                                                </span>
                                            </div>-->
                                            <!--<div class=" col-sm-4 " v-if="printSetting.printSize=='A4'">
                                                <span>
                                                    <toggle-button v-model="printSetting.blindPrint" class="pr-2 pl-2 pt-2" color="#3178F6"/>
                                                    <label class="text  font-weight-bolder"> {{ $t('BlindPrint') }} </label>
                                                </span>
                                            </div>-->
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="styleNo" v-model="printSetting.styleNo">
                                                    <label for="styleNo">{{ $t('PrintSetting.Model/StyleNo') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="customerAddress" v-model="printSetting.customerAddress">
                                                    <label for="customerAddress">{{ $t('PrintSetting.CustomerAddress') }}</label>
                                                </div>

                                            </div>

                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="customerVat" v-model="printSetting.customerVat">
                                                    <label for="customerVat">{{ $t('PrintSetting.CustomerVAT') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="customerTelephone" v-model="printSetting.customerTelephone">
                                                    <label for="customerTelephone">{{ $t('PrintSetting.CustomerTelephone') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="customerNumber" v-model="printSetting.customerNumber">
                                                    <label for="customerNumber">{{ $t('PrintSetting.CustomerNumber') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="itemPieceSize" v-model="printSetting.itemPieceSize">
                                                    <label for="itemPieceSize">{{ $t('PrintSetting.PackSize') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="cargoName" v-model="printSetting.cargoName">
                                                    <label for="cargoName">{{ $t('PrintSetting.CargoName') }}</label>
                                                </div>

                                            </div>
                                            <div class=" col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <div class="checkbox form-check-inline mx-2">
                                                    <input type="checkbox" id="isDeliveryNote" v-model="printSetting.isDeliveryNote">
                                                    <label for="isDeliveryNote">{{ $t('PrintSetting.DeliveryNote') }}</label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="accordion-item">
                                <h5 class="accordion-header m-0" id="headingTwo">
                                    <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                        {{ $t('PrintSetting.TermAndCondition') }}
                                    </button>
                                </h5>
                                <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <p class="text-white font-weight-bold" style="background: #3178f6; padding: 5px;">   {{$englishLanguage($t('PrintSetting.TermsInEnglish'))   }}: </p>
                                                <textarea class="form-control" rows="3" v-model="printSetting.termsInEng"></textarea>
                                            </div>

                                            <div class="col-sm-12 mt-3">
                                                <p class="text-white font-weight-bold" style="background: #3178f6; padding: 5px;"> {{$arabicLanguage($t('PrintSetting.TermsInArabic'))  }}   : </p>
                                                <textarea class="form-control" rows="3" v-model="printSetting.termsInAr"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="accordion-item" v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                <h5 class="accordion-header m-0" id="headingThree">
                                    <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                        {{ $t('PrintSetting.BankDetail') }}
                                    </button>
                                </h5>
                                <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <label class="text  font-weight-bolder">   {{ $t('PrintSetting.BankName1') }}: </label>
                                                <bankdropdown v-model="printSetting.bank1Id" @update:modelValue="GetBank1Account" :key="randerBanks" :modelValue="printSetting.bank1Id"></bankdropdown>
                                            </div>

                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <label class="text  font-weight-bolder">  {{ $t('PrintSetting.BankName2') }}: </label>
                                                <bankdropdown v-model="printSetting.bank2Id" @update:modelValue="GetBank2Account" :key="randerBanks" :modelValue="printSetting.bank2Id"></bankdropdown>
                                            </div>
                                            <div class="form-group has-label col-sm-4" v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <label class="text  font-weight-bolder">   {{ $t('PrintSetting.BankAccount1') }}: </label>
                                                <input class="form-control" v-model="printSetting.bankAccount1" type="text" />
                                            </div>

                                            <div class="form-group has-label col-sm-4 " v-if="printSetting.printSize=='A4 Invoice' || printSetting.printSize=='A4' || printSetting.printSize=='A4 الـــفـــاتــــورة'">
                                                <label class="text  font-weight-bolder">  {{ $t('PrintSetting.BankAccount2') }}: </label>
                                                <input class="form-control " v-model="printSetting.bankAccount2" type="text" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>


                        </div>




                    </div>

                    <div class="row form-group">
                        <div class="col-md-4">
                            <label class="col-form-label">
                                {{ $t('AddSale.DiscountType') }}
                            </label>
                            <multiselect :options="[$t('AddStockValue.AtTransactionLevel') , $t('AddStockValue.AtLineItemLevel')]" v-model="printSetting.discountTypeOption" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>

                        <div class="col-md-4">
                            <label class="col-form-label">
                                {{ $t('AddPurchase.TaxMethod') }}
                            </label>
                            <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())" :options="['Inclusive', 'Exclusive']" v-model="printSetting.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                            <multiselect v-else :options="['شامل', 'غير شامل']" v-model="printSetting.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddStockValue.SelectMethod')" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>

                        <div class="col-md-4">
                            <label class="col-form-label">
                                {{ $t('AddPurchase.VAT%') }}
                            </label>
                            <taxratedropdown v-model="printSetting.taxRateId" v-bind:modelValue="printSetting.taxRateId" :key="render" />
                        </div>
                    </div>
                </div>
                <div class="card-footer" v-if="printSetting.id=='00000000-0000-0000-0000-000000000000'">

                    <button type="button" class="btn btn-soft-primary me-2" v-on:click="SaveprintSetting" v-bind:disabled="v$.printSetting.$invalid"> {{ $t('PrintSetting.btnSave') }}</button>
                    <button type="button" class="btn btn-soft-danger   " v-on:click="close()">{{ $t('PrintSetting.btnClear') }}</button>

                </div>
                <div class="card-footer" v-else>

                    <button type="button" class="btn btn-soft-primary me-2" v-on:click="SaveprintSetting" v-bind:disabled="v$.printSetting.$invalid"> {{ $t('PrintSetting.btnUpdate') }}</button>
                    <button type="button" class="btn btn-soft-danger " v-on:click="close()">{{ $t('PrintSetting.btnClear') }}</button>

                </div>
            </div>

        </div>
        <SaleInvoiceView :printDetails="printDetails" :show="show" :headerFooter="headerFooter" :printTemplate="printTemplate" :printSize="printSize" :isTouchScreen="sale" v-if="show " @close="show=false" v-bind:key="printRender"></SaleInvoiceView>
        <addholdsetup :newbrand="holdSetup" :show="holtSetupShow" v-if="holtSetupShow" @close="IsSave1" />
        <addpermanentdeleteholdsetup :newbrand="holdSetup1" :show="pDeleteholtSetupShow" v-if="pDeleteholtSetupShow" @close="IsSave2" />
    </div>
    <!--<div v-else> <acessdenied></acessdenied></div>-->


</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import Multiselect from 'vue-multiselect'
    // import { VueEditor } from "vue2-editor";
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect
            // VueEditor
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                pDeleteholtSetupShow: false,
                holdSetup1: {
                    Id: '',
                    holdRecordType: '',
                    isActive: true,
                },
                holtSetupShow: false,
                holdSetup: {
                    Id: '',
                    holdRecordType: '',
                    isActive: true,
                },

                language: 'Nothing',
                renderPrinter: 0,
                printSetting: {
                    id: '00000000-0000-0000-0000-000000000000',
                    returnDays: 0,
                    exchangeDays: 0,
                    printTemplate: '',
                    printSize: '',
                    termsInEng: '',
                    termsInAr: '',
                    cashAccountId: '',
                    bankAccountId: '',
                    bankAccount1: '',
                    bankAccount2: '',
                    welcomeLineEn: '',
                    welcomeLineAr: '',
                    closingLineEn: '',
                    closingLineAr: '',
                    bankIcon1: '',
                    bank1Id: '',
                    bankIcon2: '',
                    bank2Id: '',
                    contactNo: '',
                    managementNo: '',
                    isActive: true,
                    printerName: '',
                    businessAddressEnglish: '',
                    businessAddressArabic: '',
                    walkInInvoiceType: 'B2B',
                    isHeaderFooter: false,
                    englishName: false,
                    arabicName: false,
                    customerNameEn: false,
                    customerNameAr: false,
                    customerAddress: false,
                    customerVat: false,
                    customerNumber: false,
                    isQuotationPrint: false,
                    cargoName: false,
                    customerTelephone: false,
                    itemPieceSize: false,
                    styleNo: false,
                    blindPrint: false,
                    showBankAccount: false,
                    invoicePrint: '',
                    isBlindPrint: false,
                    autoPaymentVoucher: false,
                    isDeliveryNote: false,
                    termAndConditionLength: true,
                    printOptions: [],
                    walkInInvoiceTypeOptions: [],

                    withSubTotal: false,
                    continueWithPage: false,
                    subTotalWithDashes: false,
                    discountTypeOption: '',
                    taxMethod: '',
                    taxRateId: '',
                },
                test: false,
                sale: false,
                show: false,
                image: '',
                image2: '',
                arabic: '',
                printTemplate: '',
                filePath: null,
                english: '',
                render: 0,
                renderedImage: 0,
                renderedImage2: 0,
                randerToggle: 0,
                bankRander: 0,
                randerBanks: 0,
                cash: 'CashReceipt',
                bank: 'BankReceipt',
                isDayStart: '',
                printTemplatelist: [],
                englishOptions: [],
                optionsPageSize: [],
                printDetails: [],
                templateRender: 0,
                printRender: 0,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },

                storeContact: false,
                managementNo: false,
                welcomeEn: false,
                welcomeAr: false,
                gratitudeMessageAr: false,
                gratitudeMessageEn: false,
                termEn: false,
                termAr: false,
                businessAddressEn: false,
                businessAddressAr: false,
            }
        },
        computed: {
            isOptionValid: function () {
                if (this.printSetting.printSize == 'Thermal Invoice' || this.printSetting.printSize == 'الـفـاتـورة طـابـعـة حـراريـة') {
                    if (!this.storeContact && this.printSetting.contactNo == '') {
                        return false;
                    }

                    if (!this.managementNo && this.printSetting.managementNo == '') {
                        return false;
                    }

                    if (!this.welcomeEn && this.printSetting.welcomeLineEn == '') {
                        return false;
                    }

                    if (!this.welcomeAr && this.printSetting.welcomeLineAr == '') {
                        return false;
                    }

                    if (!this.gratitudeMessageEn && this.printSetting.closingLineEn == '') {
                        return false;
                    }

                    if (!this.gratitudeMessageAr && this.printSetting.closingLineAr == '') {
                        return false;
                    }

                    if (!this.termEn && this.printSetting.termsInEng == '') {
                        return false;
                    }

                    if (!this.termAr && this.printSetting.termsInAr == '') {
                        return false;
                    }

                    if (!this.businessAddressEn && this.printSetting.businessAddressEnglish == '') {
                        return false;
                    }

                    if (!this.businessAddressAr && this.printSetting.businessAddressArabic == '') {
                        return false;
                    }

                    return true;
                }
                else {
                    return true;
                }
            }
        },
        validations() {
            return {
                printSetting: {
                    printSize: {
                        required,
                    },
                    printTemplate: {
                        required,
                    },
                }
                }
        },
        methods: {
            openmodel: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Region/HoldTypeSetupDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data) {
                            root.holdSetup = response.data;
                            root.holtSetupShow = !root.holtSetupShow;
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        }
                    );
            },
            openmodel1: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Region/GetPermanentDeleteHoldTypeSetupDetails', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data) {
                            root.holdSetup1 = response.data;
                            root.pDeleteholtSetupShow = !root.pDeleteholtSetupShow;
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        }
                    );
            },
            IsSave1: function () {
                this.holtSetupShow = false;
            },
            IsSave2: function () {
                this.pDeleteholtSetupShow = false;
            },
            languageChange: function (lan) {

                var root = this;
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    root.$router.go('');



                }



            },
            QuotationPrint: function (x) {

                this.printSetting.isQuotationPrint = x;
                this.randerToggle++;


            },

            SaveSingelOption: function (value, label, labelNameArabic) {
                if (this.printSetting.id != '00000000-0000-0000-0000-000000000000') {


                    var item = {
                        printSettingId: this.printSetting.id,
                        value: value,
                        label: label,
                        labelNameArabic: labelNameArabic,
                        branchId: localStorage.getItem('BranchId')
                    }
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    this.$https.post('/Sale/SavePrintOption', item, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data.isSuccess == true) {
                            console.log('Ok');



                        }
                    });
                }

            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            GetBank1Account: function (account) {

                this.printSetting.bank1Id = account.id;
                this.printSetting.bankAccount1 = account.accountNumber;
            },
            GetBank2Account: function (account) {

                this.printSetting.bank2Id = account.id;
                this.printSetting.bankAccount2 = account.accountNumber;
            },
            getBase64Image: function () {



                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

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
                            root.printDetails = {
                                barCode: "85926908371",
                                cashCustomer: "Dummy",
                                change: "0.00",
                                code: "34",
                                creditCustomer: "Credit Customer",
                                customerAddress: "Street No 3,House No 52 ,a,dfdlk ui904 fdkjf sd444......",
                                //customerAddressWalkIn: "Walk-In Customer Address..........",
                                customerCode: "C-001",
                                customerId: null,
                                customerName: "Customer Name",
                                customerNameAr: "اسم الزبون",
                                customerNameEn: "Customer Name",
                                customerTelephone: "4353443534",
                                customerVat: "423423423",
                                date: "24 Jan 2022",
                                discountAmount: 0,
                                dueDate: null,
                                email: "",
                                employeeId: null,
                                id: "984e916e-0c3f-4ba2-c3f4-08d9dcd8ece3",
                                invoiceNo: null,
                                invoiceType: 1,
                                isCredit: false,
                                isSaleReturnPost: false,
                                logisticId: null,
                                logisticNameAr: null,
                                logisticNameEn: "Cargo34",
                                mobile: "",
                                partiallyInvoice: "Fully",
                                paymentAmount: "828.00",
                                paymentTypes: {
                                    amount: 644,
                                    amountCurrency: 0,
                                    bankCashAccountId: null,
                                    id: "0160c9ec-f485-4bc8-05cc-08d9df11f699",
                                    name: "Cash",
                                    otherAmount: 0,
                                    paymentOptionId: null,
                                    paymentOptionName: null,
                                    paymentType: 0,
                                    rate: 0,

                                },
                                quotationId: null,
                                refrenceNo: "",
                                registrationNo: "SI-00068",
                                returnInvoiceAmount: 0,
                                returnInvoiceRegNo: null,
                                saleInvoiceId: null,
                                saleItems: [
                                    {
                                        arabicName: "سندل رجالي باكستاني 40-45",
                                        bundleAmount: 0,
                                        bundleId: null,
                                        buy: 0,
                                        categoryName: "CA-00011--Roman for men",
                                        code: "PR-00163",
                                        description: "ROMAN SENDLE FOR MEN 40-45 (R151BL)",
                                        discount: 0,
                                        discountAmount: 0,
                                        fixDiscount: 0,
                                        get: 0,
                                        id: "972c9701-d88e-4ecd-98a4-08d9dcd8ece5",
                                        includingVat: 36,
                                        inclusiveVat: 0,
                                        offerQuantity: 0,
                                        productName: "ROMAN SENDLE FOR MEN 40-45 (R151BL)",
                                        promotionId: null,
                                        quantity: 1,
                                        remainingQuantity: 1,
                                        returnQuantity: 0,
                                        saleId: "00000000-0000-0000-0000-000000000000",
                                        saleReturnDays: "",
                                        soQty: 0,
                                        taxMethod: "Exclusive",
                                        taxRate: 15,
                                        taxRateId: "b9b905a8-bed6-48a3-b551-08d9bae08f80",
                                        total: 240,
                                        unitPrice: 240,
                                        wareHouseId: "00000000-0000-0000-0000-000000000000",
                                        product: {
                                            arabicName: "جزم رجالي هندي فارمل 6-10",
                                            barCode: "",
                                            basicUnit: "pieces",
                                            bundleCategory: null,
                                            code: "PR-00001",
                                            englishName: "GENTS SHOES INDIAN 6-10 (607BL)",
                                            guarantee: false,
                                            id: "ccbe0cb9-7bd5-44e1-1a61-08d9bae1890e",
                                            isExpire: false,
                                            length: "1",
                                            levelOneUnit: "Carton",
                                            promotionOffer: null,
                                            salePrice: 0,
                                            saleReturnDays: "",
                                            serial: false,
                                            styleNumber: "607BL",
                                            taxMethod: null,
                                            taxRateId: "b9b905a8-bed6-48a3-b551-08d9bae08f80",
                                            unitPerPack: 12,
                                            width: "12",

                                        }



                                    },
                                    {
                                        arabicName: "سندل رجالي باكستاني 40-45",
                                        bundleAmount: 0,
                                        bundleId: null,
                                        buy: 0,
                                        categoryName: "CA-00011--Roman for men",
                                        code: "PR-00163",
                                        description: "ROMAN SENDLE FOR MEN 40-45 (R151BL)",
                                        discount: 0,
                                        discountAmount: 0,
                                        fixDiscount: 0,
                                        get: 0,
                                        id: "972c9701-d88e-7ecd-98a4-08d9dcd8ece5",
                                        includingVat: 36,
                                        inclusiveVat: 0,
                                        offerQuantity: 0,
                                        productName: "ROMAN SENDLE FOR MEN 40-45 (R151BL)",
                                        promotionId: null,
                                        quantity: 1,
                                        remainingQuantity: 1,
                                        returnQuantity: 0,
                                        saleId: "00000000-0000-0000-0000-000000000000",
                                        saleReturnDays: "",
                                        soQty: 0,
                                        taxMethod: "Exclusive",
                                        taxRate: 15,
                                        taxRateId: "b9b905a8-bed6-48a3-b551-08d9bae08f80",
                                        total: 240,
                                        unitPrice: 240,
                                        wareHouseId: "00000000-0000-0000-0000-000000000000",
                                        product: {
                                            arabicName: "جزم رجالي هندي فارمل 6-10",
                                            barCode: "",
                                            basicUnit: "pieces",
                                            bundleCategory: null,
                                            code: "PR-00001",
                                            englishName: "GENTS SHOES INDIAN 6-10 (607BL)",
                                            guarantee: false,
                                            id: "ccbe0cb9-7bd5-44e1-1a61-08d9bae1890e",
                                            isExpire: false,
                                            length: "1",
                                            levelOneUnit: "Carton",
                                            promotionOffer: null,
                                            salePrice: 0,
                                            saleReturnDays: "",
                                            serial: false,
                                            styleNumber: "607BL",
                                            taxMethod: null,
                                            taxRateId: "b9b905a8-bed6-48a3-b551-08d9bae08f80",
                                            unitPerPack: 12,
                                            width: "12",

                                        }




                                    },
                                    {
                                        arabicName: "سندل رجالي باكستاني 40-45",
                                        bundleAmount: 0,
                                        bundleId: null,
                                        buy: 0,
                                        categoryName: "CA-00011--Roman for men",
                                        code: "PR-00163",
                                        description: "ROMAN SENDLE FOR MEN 40-45 (R151BL)",
                                        discount: 0,
                                        discountAmount: 0,
                                        fixDiscount: 0,
                                        get: 0,
                                        id: "972c9701-d88e-4ecd-99a4-08d9dcd8ece5",
                                        includingVat: 36,
                                        inclusiveVat: 0,
                                        offerQuantity: 0,
                                        productName: "ROMAN SENDLE FOR MEN 40-45 (R151BL)",
                                        promotionId: null,
                                        quantity: 1,
                                        remainingQuantity: 1,
                                        returnQuantity: 0,
                                        saleId: "00000000-0000-0000-0000-000000000000",
                                        saleReturnDays: "",
                                        soQty: 0,
                                        taxMethod: "Exclusive",
                                        taxRate: 15,
                                        taxRateId: "b9b905a8-bed6-48a3-b551-08d9bae08f80",
                                        total: 240,
                                        unitPrice: 240,
                                        wareHouseId: "00000000-0000-0000-0000-000000000000",
                                        product: {
                                            arabicName: "جزم رجالي هندي فارمل 6-10",
                                            barCode: "",
                                            basicUnit: "pieces",
                                            bundleCategory: null,
                                            code: "PR-00001",
                                            englishName: "GENTS SHOES INDIAN 6-10 (607BL)",
                                            guarantee: false,
                                            id: "ccbe0cb9-7bd5-44e1-1a61-08d9bae1890e",
                                            isExpire: false,
                                            length: "1",
                                            levelOneUnit: "Carton",
                                            promotionOffer: null,
                                            salePrice: 0,
                                            saleReturnDays: "",
                                            serial: false,
                                            styleNumber: "607BL",
                                            taxMethod: null,
                                            taxRateId: "b9b905a8-bed6-48a3-b551-08d9bae08f80",
                                            unitPerPack: 12,
                                            width: "12",

                                        }




                                    }
                                ],
                                salePayment: {
                                    balance: 0,
                                    change: 0,
                                    dueAmount: 828,
                                    id: "00000000-0000-0000-0000-000000000000",
                                    paymentOptionId: null,
                                    paymentType: 0,
                                },


                                saleOrderId: null,
                                saleOrderNo: null,
                                wareHouseId: "03feddad-209d-4ca9-e349-08d9bae090c1",
                                wareHouseName: "(Show Room)",
                                wareHouseNameAr: "(صالة العرض)",

                            };

                            root.getBase64Image();
                            root.show = true;
                            root.printTemplate = root.printSetting.printTemplate;
                            root.printSize = root.printSetting.printSize;
                            root.printRender++;

                        }
                    });
            },
            getImage: function (value) {
                this.printSetting.headerImage = value;
            },
            getHeaderImage: function (value) {
                this.printSetting.headerImage1 = value;
            },
            getTagsImage: function (value) {
                this.printSetting.tagsImages = value;
            },
            getProposalImage: function (value) {
                this.printSetting.proposalImage = value;
            },
            getImage1: function (value) {
                this.printSetting.footerImage = value;
            },
            getImageForWarranty: function (value) {
                this.printSetting.warrantyImage = value;
            },
            GetInvoicePrintSetting: function () {
                var root = this;

                root.headerFooter.footerEn = "Terms of service are the legal agreements between a service provider and a person who wants to use that service. The person must agree to abide by the terms of service in order to use the offered service. Terms of service can also be merely a disclaimer";
                root.headerFooter.footerAr = "شروط الخدمة هي الاتفاقيات القانونية بين مقدم الخدمة والشخص الذي يريد استخدام تلك الخدمة. يجب أن يوافق الشخص على الالتزام بشروط الخدمة من أجل استخدام الخدمة المقدمة. يمكن أن تكون شروط الخدمة أيضًا مجرد إخلاء مسؤولية ، خاصة فيما يتعلق باستخدام مواقع الويب";
                root.headerFooter.isHeaderFooter = true;
                root.headerFooter.englishName = true;
                root.headerFooter.arabicName = true;
                root.headerFooter.customerAddress = true;
                root.headerFooter.customerVat = true;
                root.headerFooter.customerNumber = "0000000";
                root.headerFooter.cargoName = true;
                root.headerFooter.customerTelephone = true;
                root.headerFooter.itemPieceSize = true;
                root.headerFooter.styleNo = true;
                root.headerFooter.blindPrint = false;
                root.headerFooter.showBankAccount = true;
                root.headerFooter.bankAccount1 = "439028409834";
                root.headerFooter.bankAccount2 = "23489238943";
                root.headerFooter.bankIcon1 = true;
                root.headerFooter.bankIcon2 = true;
                root.headerFooter.printOptions = root.printSetting.printOptions;
            },

            printInvoice: function () {



                this.show = false;
                this.printTemplate = this.printSetting.printTemplate;
                this.printSize = this.printSetting.printSize;
                this.GetHeaderDetail();
                this.printDetails = [];


            },
            SelectTemplate: function (value, x) {

                var root = this;

                if (x == true) {
                    this.printSetting.printTemplate = '';
                }

                if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                    root.englishOptions = ['English', 'Arabic', 'English & Arabic'];
                    if (root.printSetting.invoicePrint == 'English') {
                        root.printSetting.invoicePrint = 'English'
                    }
                    else if (root.printSetting.invoicePrint == 'العربية') {
                        root.printSetting.invoicePrint = 'Arabic'
                    }
                    else if (root.printSetting.invoicePrint == 'English & Arabic') {
                        root.printSetting.invoicePrint = 'English & Arabic'
                    }


                }
                else {
                    root.englishOptions = ['إنـجـلـيـزي', 'عـربـي', 'الانجليزي وعـربـي'];
                    if (root.printSetting.invoicePrint == 'English') {
                        root.printSetting.invoicePrint = 'إنـجـلـيـزي'
                    }
                    else if (root.printSetting.invoicePrint == 'العربية') {
                        root.printSetting.invoicePrint = 'عـربـي'
                    }
                    else if (root.printSetting.invoicePrint == 'English & Arabic') {
                        root.printSetting.invoicePrint = 'الانجليزي وعـربـي'
                    }
                }
                this.printTemplatelist = []
                if (root.$i18n.locale == 'en' || this.isLeftToRight()) {
                    if (value == 'A4 Invoice' || value == 'A4') {
                        if (this.isValid('DefaultTemplate')) {
                            this.printTemplatelist.push('Default Template')
                        }
                        if (this.isValid('SATemplate1A4')) {
                            this.printTemplatelist.push('SA – Template 1 A4')
                        }
                        if (this.isValid('SATemplate2A4')) {
                            this.printTemplatelist.push('SA – Template 2 A4')
                        }
                        this.printTemplatelist.push('SA – Template 3 A4')

                        this.printTemplatelist.push('SA – Template 4 A4')
                        this.printTemplatelist.push('PK – Template 5 A4')
                        this.printTemplatelist.push('PK – Template 6 A4')
                        this.printTemplatelist.push('Template8 A4')
                        this.printTemplatelist.push('SA – Template 5 A4')
                        this.printTemplatelist.push('SA – Template 6 A4')
                        this.printTemplatelist.push('PK-General A4')

                    }
                    else {
                        if (this.isValid('DefaultTemplate')) {
                            this.printTemplatelist.push('Default Template')
                        }
                        if (this.isValid('PKTemplate1Thermal')) {
                            this.printTemplatelist.push('PK – Template 1 Thermal')
                        }
                        if (this.isValid('PKTemplate2Thermal')) {
                            this.printTemplatelist.push('PK – Template 2 Thermal')
                        }
                        if (this.isValid('SATemplate1Thermal')) {
                            this.printTemplatelist.push('SA – Template 1 Thermal')
                        }
                        if (this.isValid('SATemplate2Thermal')) {
                            this.printTemplatelist.push('SA – Template 2 Thermal')
                        }

                    }

                }
                else {
                    if (value == 'A4 الـــفـــاتــــورة') {
                        if (this.isValid('DefaultTemplate')) {
                            this.printTemplatelist.push('الــقـالـب الافــتـراضـيـة')
                        }
                        if (this.isValid('SATemplate1A4')) {
                            this.printTemplatelist.push('السـعـوديـة - الــقـالـب A4 1')
                        }
                        if (this.isValid('SATemplate2A4')) {
                            this.printTemplatelist.push('السـعـوديـة - الــقـالـب A4 2')
                        }
                        if (this.isValid('SATemplate3A4')) {
                            this.printTemplatelist.push('السـعـوديـة - الــقـالـب A4 3')
                        }

                        this.printTemplatelist.push('السـعـوديـة - الــقـالـب A4 4')
                        this.printTemplatelist.push('PK Template 6')
                        this.printTemplatelist.push('قالب 8 A4')
                        this.printTemplatelist.push('السـعـوديـة - الــقـالـب A4 5')
                        this.printTemplatelist.push('السـعـوديـة - الــقـالـب A4 6')
                        this.printTemplatelist.push('PK-General A4')

                    }
                    else {
                        if (this.isValid('DefaultTemplate')) {
                            this.printTemplatelist.push('الــقـالـب الافــتـراضـيـة')
                        }
                        if (this.isValid('PKTemplate1Thermal')) {
                            this.printTemplatelist.push('PK – Template 1 Thermal')
                        }
                        if (this.isValid('PKTemplate2Thermal')) {
                            this.printTemplatelist.push('PK – Template 2 Thermal')
                        }
                        if (this.isValid('SATemplate1Thermal')) {
                            this.printTemplatelist.push('السـعـوديـة - الــقـالـب 1 حراري')
                        }
                        if (this.isValid('SATemplate2Thermal')) {
                            this.printTemplatelist.push('السـعـوديـة - الــقـالـب 2 حراري')
                        }

                    }


                }
                if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                    if (root.printSetting.printSize == 'A4 Invoice' || root.printSetting.printSize == 'A4') {
                        if (root.printSetting.printTemplate == 'Default') {
                            root.printSetting.printTemplate = 'Default Template'
                        }
                        else if (root.printSetting.printTemplate == 'Template1') {
                            root.printSetting.printTemplate = 'SA – Template 1 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template2') {
                            root.printSetting.printTemplate = 'SA – Template 2 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template3') {
                            root.printSetting.printTemplate = 'SA – Template 3 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template4') {
                            root.printSetting.printTemplate = 'SA – Template 4 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template5') {
                            root.printSetting.printTemplate = 'SA – Template 5 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template6') {
                            root.printSetting.printTemplate = 'SA – Template 6 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template8') {
                            root.printSetting.printTemplate = 'Template8 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template9') {
                            root.printSetting.printTemplate = 'PK – Template 6 A4'

                        }
                    }
                    else if (root.printSetting.printSize == 'Thermal Invoice') {
                        if (root.printSetting.printTemplate == 'Default') {
                            root.printSetting.printTemplate = 'Default Template'
                        }
                        else if (root.printSetting.printTemplate == 'PkTemplate1') {
                            root.printSetting.printTemplate = 'PK – Template 1 Thermal'

                        }
                        else if (root.printSetting.printTemplate == 'PkTemplate2') {
                            root.printSetting.printTemplate = 'PK – Template 2 Thermal'

                        }
                        else if (root.printSetting.printTemplate == 'RetailSaTemplate1') {
                            root.printSetting.printTemplate = 'SA – Template 1 Thermal'

                        }
                        else if (root.printSetting.printTemplate == 'RetailSaTemplate2') {
                            root.printSetting.printTemplate = 'SA – Template 2 Thermal'

                        }

                    }

                }
                else {

                    if (root.printSetting.printSize == 'A4 الـــفـــاتــــورة') {
                        if (root.printSetting.printTemplate == 'Default') {
                            root.printSetting.printTemplate = 'الــقـالـب الافــتـراضـيـة'
                        }
                        else if (root.printSetting.printTemplate == 'Template1') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب A4 1'

                        }
                        else if (root.printSetting.printTemplate == 'Template2') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب A4 2'

                        }
                        else if (root.printSetting.printTemplate == 'Template3') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب A4 3'

                        }
                        else if (root.printSetting.printTemplate == 'Template4') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب A4 4'

                        }
                        else if (root.printSetting.printTemplate == 'Template5') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب A4 5'

                        }
                        else if (root.printSetting.printTemplate == 'Template8') {
                            root.printSetting.printTemplate = 'قالب 8 A4'

                        }
                        else if (root.printSetting.printTemplate == 'Template9') {
                            root.printSetting.printTemplate = 'PK Template 2'

                        }
                        else if (root.printSetting.printTemplate == 'Template6') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب A4 6'

                        }
                    }

                    else if (root.printSetting.printSize == 'الـفـاتـورة طـابـعـة حـراريـة') {
                        if (root.printSetting.printTemplate == 'Default') {
                            root.printSetting.printTemplate = 'الــقـالـب الافــتـراضـيـة'
                        }
                        else if (root.printSetting.printTemplate == 'PkTemplate1') {
                            root.printSetting.printTemplate = 'PK – Template 1 Thermal'

                        }
                        else if (root.printSetting.printTemplate == 'PkTemplate2') {
                            root.printSetting.printTemplate = 'PK – Template 2 Thermal'

                        }
                        else if (root.printSetting.printTemplate == 'RetailSaTemplate1') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب 1 حراري'

                        }
                        else if (root.printSetting.printTemplate == 'RetailSaTemplate2') {
                            root.printSetting.printTemplate = 'السـعـوديـة - الــقـالـب 2 حراري'

                        }

                    }



                }

            },
            SelectTemplateEdit: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printTemplatelist = [];
                root.printSetting.PrintTemplate = '';

                root.$https.get('Sale/PrintTemplateList?print=' + value, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.printTemplatelist = response.data;
                            root.printSetting.printTemplate = response.data.find((x) => x == root.printSetting.printTemplate) == undefined ? "" : response.data.find((x) => x == root.printSetting.printTemplate);
                            root.templateRender++;
                        }
                    });
            },
            close: function () {
                this.$router.push('/StartScreen');
            },
            backConversion: function () {
                var root = this;



                if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                    if (root.printSetting.printSize == 'A4 Invoice' || root.printSetting.printSize == 'A4') {
                        if (root.printSetting.printTemplate == 'Default Template') {
                            root.printSetting.printTemplate = 'Default'
                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 1 A4') {

                            root.printSetting.printTemplate = 'Template1'

                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 2 A4') {

                            root.printSetting.printTemplate = 'Template2'

                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 3 A4') {

                            root.printSetting.printTemplate = 'Template3'

                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 4 A4') {

                            root.printSetting.printTemplate = 'Template4'

                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 5 A4') {

                            root.printSetting.printTemplate = 'Template5'

                        }
                        else if (root.printSetting.printTemplate == 'Template8 A4') {

                            root.printSetting.printTemplate = 'Template8'

                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 6 A4') {

                            root.printSetting.printTemplate = 'Template6'

                        }
                    }
                    else if (root.printSetting.printTemplate == 'PK – Template 6 A4') {

                        root.printSetting.printTemplate = 'Template9'

                    }

                    else if (root.printSetting.printSize == 'Thermal Invoice') {
                        if (root.printSetting.printTemplate == 'Default Template') {

                            root.printSetting.printTemplate = 'Default'
                        }
                        else if (root.printSetting.printTemplate == 'PK – Template 1 Thermal') {

                            root.printSetting.printTemplate = 'PkTemplate1'

                        }
                        else if (root.printSetting.printTemplate == 'PK – Template 2 Thermal') {

                            root.printSetting.printTemplate = 'PkTemplate2'

                        }
                        else if (root.printSetting.printTemplate == 'SA – Template 1 Thermal') {

                            root.printSetting.printTemplate = 'RetailSaTemplate1'

                        }

                        else if (root.printSetting.printTemplate == 'SA – Template 2 Thermal') {
                            root.printSetting.printTemplate = 'RetailSaTemplate2'

                        }

                    }
                }


                else {

                    if (root.printSetting.printSize == 'A4 الـــفـــاتــــورة') {
                        if (root.printSetting.printTemplate == 'الــقـالـب الافــتـراضـيـة') {
                            root.printSetting.printTemplate = 'Default'
                        }
                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 1 ') {
                            root.printSetting.printTemplate = 'Template1'

                        }
                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 3 ') {
                            root.printSetting.printTemplate = 'Template3'

                        }
                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 4 ') {
                            root.printSetting.printTemplate = 'Template4'

                        }
                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 5 ') {
                            root.printSetting.printTemplate = 'Template5'

                        }
                        else if (root.printSetting.printTemplate == 'قالب 8 A4') {
                            root.printSetting.printTemplate = 'Template8'

                        }
                        else if (root.printSetting.printTemplate == 'PK-Template ') {
                            root.printSetting.printTemplate = 'Template9'

                        }

                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب A4 6 ') {
                            root.printSetting.printTemplate = 'Template6'

                        }
                    }
                    else if (root.printSetting.printSize == 'الـفـاتـورة طـابـعـة حـراريـة') {
                        if (root.printSetting.printTemplate == 'الــقـالـب الافــتـراضـيـة') {
                            root.printSetting.printTemplate = 'Default'
                        }
                        else if (root.printSetting.printTemplate == 'PK – Template 1 Thermal') {
                            root.printSetting.printTemplate = 'PkTemplate1'

                        }
                        else if (root.printSetting.printTemplate == 'PK – Template 2 Thermal') {
                            root.printSetting.printTemplate = 'PkTemplate2'

                        }
                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب 1 حراري') {
                            root.printSetting.printTemplate = 'RetailSaTemplate1'

                        }
                        else if (root.printSetting.printTemplate == 'السـعـوديـة - الــقـالـب 2 حراري') {
                            root.printSetting.printTemplate = 'RetailSaTemplate2'

                        }

                    }



                }
            },
            SaveprintSetting: function () {

                var root = this;
                var token = '';




                this.backConversion();




                if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                    if (root.printSetting.printSize == 'A4 Invoice') {
                        root.printSetting.printSize = 'A4'

                    }
                    else if (root.printSetting.printSize == 'Thermal Invoice') {
                        root.printSetting.printSize = 'Thermal'

                    }
                    /*root.SelectTemplate(root.printSetting.printSize);*/


                }
                else {
                    if (root.printSetting.printSize == 'A4 الـــفـــاتــــورة') {
                        root.printSetting.printSize = 'A4'
                    }
                    else if (root.printSetting.printSize == 'الـفـاتـورة طـابـعـة حـراريـة') {
                        root.printSetting.printSize = 'Thermal'
                    }


                }
                if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                    if (root.printSetting.invoicePrint == 'English') {
                        root.printSetting.invoicePrint = 'English'
                    }
                    else if (root.printSetting.invoicePrint == 'Arabic') {
                        root.printSetting.invoicePrint = 'العربية'
                    }
                    else if (root.printSetting.invoicePrint == 'English & Arabic') {
                        root.printSetting.invoicePrint = 'English & Arabic'
                    }


                }
                else {
                    if (root.printSetting.invoicePrint == 'إنـجـلـيـزي') {
                        root.printSetting.invoicePrint = 'English'
                    }
                    else if (root.printSetting.invoicePrint == 'عـربـي') {
                        root.printSetting.invoicePrint = 'العربية'
                    }
                    else if (root.printSetting.invoicePrint == 'الانجليزي وعـربـي') {
                        root.printSetting.invoicePrint = 'English & Arabic'
                    }
                }







                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.printSetting.printSize != "A4") {
                    this.printSetting.isHeaderFooter = false;
                    this.printSetting.englishName = false;
                    this.printSetting.arabicName = false;
                    this.printSetting.customerAddress = false;
                    this.printSetting.customerVat = false;
                    this.printSetting.customerNumber = false;
                    this.printSetting.cargoName = false;
                    this.printSetting.customerTelephone = false;
                    this.printSetting.itemPieceSize = false;
                    this.printSetting.styleNo = false;
                    this.printSetting.showBankAccount = false;
                    this.printSetting.isDeliveryNote = false;
                    this.printSetting.termAndConditionLength = false;
                    this.printSetting.customerNameEn = false;
                    this.printSetting.customerNameAr = false;
                }


                //if (root.printSetting.isHeaderFooter == true) {
                //    localStorage.setItem('IsHeaderFooter', false);
                //}
                //else {
                //    localStorage.setItem('IsHeaderFooter', true);
                //}
                localStorage.setItem('IsHeaderFooter', this.printSetting.isHeaderFooter);
                localStorage.setItem('InvoicePrint', this.printSetting.invoicePrint);
                localStorage.setItem('IsBlindPrint', this.printSetting.isBlindPrint);
                localStorage.setItem('AutoPaymentVoucher', this.printSetting.autoPaymentVoucher);
                localStorage.setItem('IsDeliveryNote', this.printSetting.isDeliveryNote);
                localStorage.setItem('TermAndConditionLength', this.printSetting.termAndConditionLength);
                localStorage.setItem('PrintTemplate', this.printSetting.printTemplate);
                localStorage.setItem('PrintSizeA4', this.printSetting.printSize);

                this.printSetting.branchId = localStorage.getItem('BranchId');

                this.$https.post('/Sale/SaveprintSetting', this.printSetting, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {

                        if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                            if (root.printSetting.invoicePrint == 'English') {
                                root.printSetting.invoicePrint = 'English'
                            }
                            else if (root.printSetting.invoicePrint == 'العربية') {
                                root.printSetting.invoicePrint = 'Arabic'
                            }
                            else if (root.printSetting.invoicePrint == 'English & Arabic') {
                                root.printSetting.invoicePrint = 'English & Arabic'
                            }


                        }
                        else {
                            if (root.printSetting.invoicePrint == 'English') {
                                root.printSetting.invoicePrint = 'إنـجـلـيـزي'
                            }
                            else if (root.printSetting.invoicePrint == 'العربية') {
                                root.printSetting.invoicePrint = 'عـربـي'
                            }
                            else if (root.printSetting.invoicePrint == 'English & Arabic') {
                                root.printSetting.invoicePrint = 'الانجليزي وعـربـي'
                            }
                        }
                        if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                            if (root.printSetting.printSize == 'A4') {
                                root.printSetting.printSize = 'A4 Invoice'

                            }
                            else if (root.printSetting.printSize == 'Thermal') {
                                root.printSetting.printSize = 'Thermal Invoice'

                            }
                            root.SelectTemplate(root.printSetting.printSize);


                        }
                        else {
                            if (root.printSetting.printSize == 'A4') {
                                root.printSetting.printSize = 'A4 الـــفـــاتــــورة'
                            }
                            else if (root.printSetting.printSize == 'Thermal') {
                                root.printSetting.printSize = 'الـفـاتـورة طـابـعـة حـراريـة'
                            }

                            root.SelectTemplate(root.printSetting.printSize);

                        }
                        localStorage.setItem("PrinterName", root.printSetting.printerName)
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });


                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your printSetting Name  Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                });
            },
            SavePrintOption: function (item) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.FeildsDisabled();
                this.$https.post('/Sale/SavePrintOption', item, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {

                        console.log('Ok');

                    }
                });
            },
            EditprintSetting: function () {
                this.optionsPageSize = []
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (root.$i18n.locale == 'en' || this.isLeftToRight()) {
                    if (this.isValid('CanChooseA4InvoiceType')) {
                        this.optionsPageSize.push('A4 Invoice')
                    }
                    if (this.isValid('CanChooseThermalInvoiceType')) {
                        this.optionsPageSize.push('Thermal Invoice')
                    }


                }
                else {
                    if (this.isValid('CanChooseA4InvoiceType')) {
                        this.optionsPageSize.push('A4 الـــفـــاتــــورة')
                    }
                    if (this.isValid('CanChooseThermalInvoiceType')) {
                        this.optionsPageSize.push('الـفـاتـورة طـابـعـة حـراريـة')
                    }

                }
                var branchId = localStorage.getItem('BranchId');


                root.$https.get('/Sale/printSettingDetail?branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {

                            root.printSetting.id = response.data.id;
                            root.printSetting.isQuotationPrint = response.data.isQuotationPrint;
                            root.printSetting.headerImage = response.data.headerImage;
                            root.printSetting.footerImage = response.data.footerImage;
                            root.printSetting.headerImage1 = response.data.headerImage1;
                            root.printSetting.tagsImages = response.data.tagsImages;
                            root.printSetting.warrantyImage = response.data.warrantyImage;
                            root.printSetting.proposalImage = response.data.proposalImage;
                            root.printSetting.printTemplate = response.data.printTemplate;
                            root.printSetting.printSize = response.data.printSize;
                            root.printSetting.returnDays = response.data.returnDays;
                            root.printSetting.termsInEng = response.data.termsInEng;
                            root.printSetting.termsInAr = response.data.termsInAr;
                            root.printSetting.isActive = response.data.isActive;
                            root.printSetting.printerName = response.data.printerName;
                            root.printSetting.isHeaderFooter = response.data.isHeaderFooter;
                            root.printSetting.bankAccountId = response.data.bankAccountId;
                            root.printSetting.cashAccountId = response.data.cashAccountId;

                            root.printSetting.englishName = response.data.englishName;
                            root.printSetting.arabicName = response.data.arabicName;
                            root.printSetting.customerAddress = response.data.customerAddress;
                            root.printSetting.customerVat = response.data.customerVat;
                            root.printSetting.customerNumber = response.data.customerNumber;
                            root.printSetting.cargoName = response.data.cargoName;
                            root.printSetting.customerTelephone = response.data.customerTelephone;
                            root.printSetting.itemPieceSize = response.data.itemPieceSize;
                            root.printSetting.styleNo = response.data.styleNo;
                            root.printSetting.blindPrint = response.data.blindPrint;
                            root.printSetting.showBankAccount = response.data.showBankAccount;
                            root.printSetting.bankAccount1 = response.data.bankAccount1;
                            root.printSetting.bankAccount2 = response.data.bankAccount2;
                            root.printSetting.bankIcon1 = response.data.bankIcon1;
                            root.printSetting.bankIcon2 = response.data.bankIcon2;
                            root.printSetting.invoicePrint = response.data.invoicePrint;
                            root.printSetting.isBlindPrint = response.data.isBlindPrint;
                            root.printSetting.autoPaymentVoucher = response.data.autoPaymentVoucher;

                            root.printSetting.isDeliveryNote = response.data.isDeliveryNote;
                            root.printSetting.termAndConditionLength = response.data.termAndConditionLength;
                            root.printSetting.customerNameEn = response.data.customerNameEn;
                            root.printSetting.customerNameAr = response.data.customerNameAr;
                            root.printSetting.bank1Id = response.data.bank1Id;
                            root.printSetting.bank2Id = response.data.bank2Id;
                            root.printSetting.exchangeDays = response.data.exchangeDays;
                            root.printSetting.printOptions = response.data.printOptions;
                            console.log(response.data.printOptions);
                            root.printSetting.welcomeLineEn = response.data.welcomeLineEn;
                            root.printSetting.welcomeLineAr = response.data.welcomeLineAr;
                            root.printSetting.closingLineEn = response.data.closingLineEn;
                            root.printSetting.closingLineAr = response.data.closingLineAr;
                            root.printSetting.contactNo = response.data.contactNo;
                            root.printSetting.managementNo = response.data.managementNo;
                            root.printSetting.businessAddressArabic = response.data.businessAddressArabic;
                            root.printSetting.businessAddressEnglish = response.data.businessAddressEnglish;
                            root.printSetting.walkInInvoiceType = response.data.walkInInvoiceType;
                            root.printSetting.headerImageForPrint = response.data.headerImageForPrint;
                            root.printSetting.footerImageForPrint = response.data.footerImageForPrint;
                            root.printSetting.headerImage1ForPrint = response.data.headerImage1ForPrint;
                            root.printSetting.tagImageForPrint = response.data.tagImageForPrint;
                            root.printSetting.proposalImageForPrint = response.data.proposalImageForPrint;
                            root.printSetting.warrantyImageForPrint = response.data.warrantyImageForPrint;

                            root.printSetting.withSubTotal = response.data.withSubTotal;
                            root.printSetting.continueWithPage = response.data.continueWithPage;
                            root.printSetting.discountTypeOption = response.data.discountTypeOption;
                            root.printSetting.taxMethod = response.data.taxMethod;
                            root.printSetting.taxRateId = response.data.taxRateId;



                            if (root.$i18n.locale == 'en' || root.isLeftToRight()) {
                                if (root.printSetting.printSize == 'A4') {
                                    root.printSetting.printSize = 'A4 Invoice'

                                }
                                else if (root.printSetting.printSize == 'Thermal') {
                                    root.printSetting.printSize = 'Thermal Invoice'

                                }
                                root.SelectTemplate(root.printSetting.printSize);


                            }
                            else {
                                if (root.printSetting.printSize == 'A4') {
                                    root.printSetting.printSize = 'A4 الـــفـــاتــــورة'
                                }
                                else if (root.printSetting.printSize == 'Thermal') {
                                    root.printSetting.printSize = 'الـفـاتـورة طـابـعـة حـراريـة'
                                }

                                root.SelectTemplate(root.printSetting.printSize);

                            }
                            root.FeildsDisabled();

                            root.render++;
                            root.bankRander++;
                            root.randerBanks++;
                            root.renderedImage++;
                            root.renderedImage2++;
                            root.randerToggle++;

                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            },
            FeildsDisabled: function () {
                var root = this;

                var storeContactEn = root.printSetting.printOptions.find((x) => x.label == 'Store Contact No (Eng)');
                var storeContactAr = root.printSetting.printOptions.find((x) => x.label == 'Store Contact No (Ar)');
                if ((storeContactEn == undefined || !storeContactEn.value) && (storeContactAr == undefined || !storeContactAr.value)) {
                    root.storeContact = true;
                }
                else {
                    root.storeContact = false;
                }

                var managementNoEn = root.printSetting.printOptions.find((x) => x.label == 'Management No (Eng)');
                var managementNoAr = root.printSetting.printOptions.find((x) => x.label == 'Management No (Ar)');
                if ((managementNoEn == undefined || !managementNoEn.value) && (managementNoAr == undefined || !managementNoAr.value)) {
                    root.managementNo = true;
                }
                else {
                    root.managementNo = false;
                }

                var welcmAr = root.printSetting.printOptions.find((x) => x.label == 'Welcome/Tagline (Ar)');
                if (welcmAr == undefined || !welcmAr.value) {
                    root.welcomeAr = true;
                }
                else {
                    root.welcomeAr = false;
                }

                var welcmEn = root.printSetting.printOptions.find((x) => x.label == 'Welcome/Tagline (Eng)');
                if (welcmEn == undefined || !welcmEn.value) {
                    root.welcomeEn = true;
                }
                else {
                    root.welcomeEn = false;
                }

                var gratitudeAr = root.printSetting.printOptions.find((x) => x.label == 'Gratitude Message (Ar)');
                if (gratitudeAr == undefined || !gratitudeAr.value) {
                    root.gratitudeMessageAr = true;
                }
                else {
                    root.gratitudeMessageAr = false;
                }

                var gratitudeEn = root.printSetting.printOptions.find((x) => x.label == 'Gratitude Message (Eng)');
                if (gratitudeEn == undefined || !gratitudeEn.value) {
                    root.gratitudeMessageEn = true;
                }
                else {
                    root.gratitudeMessageEn = false;
                }

                var termsAr = root.printSetting.printOptions.find((x) => x.label == 'Terms & Conditions (Ar)');
                if (termsAr == undefined || !termsAr.value) {
                    root.termAr = true;
                }
                else {
                    root.termAr = false;
                }

                var termsEn = root.printSetting.printOptions.find((x) => x.label == 'Terms & Conditions (En)');
                if (termsEn == undefined || !termsEn.value) {
                    root.termEn = true;
                }
                else {
                    root.termEn = false;
                }

                var busAddAr = root.printSetting.printOptions.find((x) => x.label == 'Business Address (Arabic)');
                if (busAddAr == undefined || !busAddAr.value) {
                    root.businessAddressAr = true;
                }
                else {
                    root.businessAddressAr = false;
                }

                var busAddEn = root.printSetting.printOptions.find((x) => x.label == 'Business Address (English)');
                if (busAddEn == undefined || !busAddEn.value) {
                    root.businessAddressEn = true;
                }
                else {
                    root.businessAddressEn = false;
                }
            },
        },
        created() {
            this.$emit('update:modelValue', this.$route.name);

            this.walkInInvoiceTypeOptions = ['B2B', 'B2C']
            this.EditprintSetting();

        },
        mounted: function () {

            var getLocale = this.$i18n.locale;
            this.language = getLocale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isDayStart = localStorage.getItem('IsDayStart') == "true" ? true : false;


        }
    }
</script>