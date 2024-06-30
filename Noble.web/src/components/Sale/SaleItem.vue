<template>
    <div class="col-lg-12">
        <div class="col-lg-4 form-group">
            <input v-model="barCode" @change="onBarcodeScanned(barCode)" type="text" class="form-control" placeholder="Barcode Scanning..." />
        </div>
        <div class="table-responsive mt-3">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th style="width: 150px;">
                            {{ $t('SaleItem.Product') }}
                        </th>
                        <th class="text-center" style="width: 50px;" v-if="isValid('Scheme')">
                            Scheme
                        </th>
                        <th v-if="colorVariants" class="text-center" style="width: 200px;">
                            {{ $t('SaleItem.Description') }}
                        </th>

                        <th style="width: 150px;" v-if="colorVariants">
                            {{ $t('SaleItem.Color') }}
                        </th>

                        <th class="text-center" style="width: 90px;">
                            {{ $t('SaleItem.UnitPrice') }}
                        </th>

                        <th class="text-center" style="width: 70px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('SaleItem.UnitPerPack') }}
                        </th>
                        <th class="text-center" style="width: 80px;" v-if="isFifo">
                            {{ $t('SaleItem.BatchNo') }}
                        </th>
                        <th class="text-center" style="width: 80px;" v-if="isFifo">
                            {{ $t('SaleItem.ExpiryDate') }}
                        </th>
                        <th style="width: 80px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleItem.HighQty') }}
                        </th>
                        <th class="text-center" style="width: 80px;">
                            {{ $t('SaleItem.Quantity') }}
                        </th>
                        <th style="width: 80px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleItem.TOTALQTY') }}
                        </th>
                        <th class="text-center" style="width: 50px;" v-if="isValid('Scheme')">
                            Scheme
                        </th>
                        <th class="text-center" style="width: 50px;" v-if="isValid('Scheme')">
                            +
                        </th>
                        <th class="text-center" style="width: 80px;" v-if="isValid('SerialNumber')">
                            {{ $t('SaleItem.SerialNo') }}
                        </th>
                        <th class="text-center" style="width: 100px;" hidden>
                            {{ $t('SaleItem.ReturnDays') }}
                        </th>
                        <th style="width: 100px;" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                            {{ $t('SaleItem.Bundle') }}
                        </th>

                        <th class="text-center" style="width: 115px;" v-if="isSerial">
                            {{ $t('SaleItem.Serial') }}
                        </th>
                        <th style="width: 100px;" v-if="isSerial">
                            {{ $t('SaleItem.Guarantee') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="!isDiscountOnTransaction">
                            {{ $t('SaleItem.DISC%') }}
                        </th>

                        <!--<th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 105px;">
                                {{ $t('AddPurchase.TaxMethod') }}
                            </th>-->
                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 130px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th class="text-end" style="width: 100px;">
                            {{ $t('SaleItem.LineTotal') }}
                        </th>
                        <th style="width: 30px;"></th>
                    </tr>
                </thead>
                <tbody>

                    <tr v-for="(prod, index) in saleProducts" :key="rendered + index" v-bind:class="{'alert-danger':prod.outOfStock}">
                        <td>{{index+1}}</td>
                        <td>
                            <span>
                                {{products.find(x => x.id == prod.productId).displayName}}
                            </span>

                            <span class="badge rounded-pill badge-soft-primary">{{prod.promotionName}}</span>
                            <!--<span v-if="prod.promotionId != null && prod.promotionId != undefined && prod.promotionId != ''" class="badge rounded-pill badge-soft-primary">
                                    <span v-if="prod.discountSign == 'F'">
                                        {{(products.find(x => x.id == prod.productId).promotionOffer.discount).toFixed(3).slice(0,-1)}}, ({{products.find(x => x.id == prod.productId).promotionOffer.stockLimit - products.find(x => x.id == prod.productId).promotionOffer.quantityOut}})
                                    </span>
                                    <span v-if="prod.discountSign == '%'">
                                        {{(products.find(x => x.id == prod.productId).promotionOffer.discount).toFixed(3).slice(0,-1)}}%, ({{products.find(x => x.id == prod.productId).promotionOffer.stockLimit - products.find(x => x.id == prod.productId).promotionOffer.quantityOut}})
                                    </span>
                                </span>
                                <span v-if="products.find(x => x.id == prod.productId).bundleCategory != undefined" class="badge rounded-pill badge-soft-primary">
                                    {{products.find(x => x.id == prod.productId).bundleCategory.buy}} + {{products.find(x => x.id == prod.productId).bundleCategory.get}}, ({{products.find(x => x.id == prod.productId).bundleCategory.stockLimit - products.find(x => x.id == prod.productId).bundleCategory.quantityOut}})
                                </span>-->
                        </td>
                        <td class="border-top-0 text-center" v-if="isValid('Scheme')">
                            <input type="number" v-model="prod.schemePhysicalQuantity" @focus="$event.target.select()" class="form-control text-center" />
                        </td>
                        <td v-if="colorVariants">
                            <textarea class="form-control input-border" v-model="prod.description" />
                        </td>
                        <td v-if="colorVariants">
                            <colordropdown v-model="prod.colorId" :isSaleItem="true" @update:modelValue="GetColorName(prod.colorId, prod)" :modelValue="prod.colorId" />
                        </td>

                        <td v-on:dblclick="counter += 1, openmodel('unitPrice'+index)" v-if="!changePriceDuringSale && dayStart=='true'">
                            <decimal-to-fixed disable="((isAuthour.changePriceDuringSale && isAuthour.column==('unitPrice'+index))==true?'true':'false') || isEditPaidInvoice" v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                        </td>
                        <td v-else>
                            <decimal-to-fixed v-model="prod.unitPrice" :disable="prod.isFree || isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                        </td>

                        <td class="text-center" v-if="isValid('CanViewUnitPerPack')">
                            {{prod.unitPerPack}}
                        </td>
                        <td class="text-center" v-if="isFifo">
                            {{prod.batchNo}}
                        </td>
                        <td class="text-center" v-if="isFifo">
                            {{getDate(prod.batchExpiry)}}

                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'" :title="prod.levelOneUnit" data-tippy-arrow="true" data-tippy-animation="fade">
                            <decimal-to-fixed v-model="prod.highQty" :isQunatity="true"
                                              v-bind:salePriceCheck="false"
                                              @update:modelValue="updateLineTotal(prod.highQty, 'highQty', prod)" :disable="isEditPaidInvoice" />
                            <!--<small style="font-weight: 500;font-size:70%;">
                                {{prod.levelOneUnit}}
                            </small>-->
                        </td>

                        <td class="text-center" :title="prod.basicUnit" data-tippy-arrow="true" data-tippy-animation="fade">
                            <decimal-to-fixed v-model="prod.quantity" :isQunatity="true"
                                              v-bind:salePriceCheck="false"
                                              :disable="prod.isFree || isEditPaidInvoice"
                                              @update:modelValue="updateLineTotal(prod.quantity, 'quantity', prod)" />
                        </td>

                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            {{parseInt(parseFloat(prod.highQty*prod.unitPerPack) + parseFloat(prod.quantity))}}
                        </td>
                        <td class="border-top-0 text-center" v-if="isValid('Scheme')">
                            <input type="number" v-model="prod.schemeQuantity"
                                   @focus="$event.target.select()"
                                   class="form-control text-center"
                                   @keyup="updateLineTotal($event.target.value, 'schemeQuantity', prod)" v-if="isEditPaidInvoice" disabled />
                            <input type="number" v-model="prod.schemeQuantity"
                                   @focus="$event.target.select()"
                                   class="form-control text-center"
                                   @keyup="updateLineTotal($event.target.value, 'schemeQuantity', prod)" v-else />
                        </td>
                        <td class="border-top-0 text-center" v-if="isValid('Scheme')">
                            <input type="number" v-model="prod.scheme"
                                   @focus="$event.target.select()"
                                   class="form-control text-center"
                                   @keyup="updateLineTotal($event.target.value, 'scheme', prod)" v-if="isEditPaidInvoice" disabled />
                            <input type="number" v-model="prod.scheme"
                                   @focus="$event.target.select()"
                                   class="form-control text-center"
                                   @keyup="updateLineTotal($event.target.value, 'scheme', prod)" v-else />
                        </td>
                        <td v-if="isValid('SerialNumber')">
                            <input v-model="prod.serialNo" type="text" class="form-control text-center" v-if="isEditPaidInvoice" disabled />
                            <input v-model="prod.serialNo" type="text" class="form-control text-center" v-else />
                        </td>
                        <td v-if="prod.saleReturnDays > 0" hidden>
                            <input v-model="prod.saleReturnDays" type="text" class="form-control text-center" v-if="isEditPaidInvoice" disabled />
                            <input v-model="prod.saleReturnDays" type="text" class="form-control text-center" v-else />
                        </td>
                        <td class="text-center" v-else hidden>
                            <span>--</span>
                        </td>
                        <td class="text-center" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                            <span class="badge badge-pill badge-info">{{prod.bundleOffer}}</span>
                        </td>

                        <td class="text-center" v-if="isSerial">
                            <button @click="AddSerial(prod)" v-if="prod.isSerial" title="Add Serial" class="btn btn-primary btn-sm"> Add Serial </button>
                            <span v-else>-</span>
                        </td>
                        <td class="border-top-0  text-center" v-if="isSerial">
                            <datepicker v-model="prod.guaranteeDate" v-if="prod.guarantee" />
                            <span v-else>-</span>
                        </td>

                        <td v-if="!isDiscountOnTransaction">
                            <div v-if="prod.discountSign == '%'">
                                <div class="input-group" v-if="(!giveDiscountDuringSale && dayStart==='true') " v-on:dblclick="counter += 1, openmodel1('discount'+index)">
                                    <decimal-to-fixed v-model="prod.discount" :disable="prod.isFree || isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="prod.isFree || isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                                <div class="input-group" v-else>
                                    <decimal-to-fixed v-model="prod.discount" :disable="prod.isFree || isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="prod.isFree || isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                            </div>
                            <div v-else-if="prod.discountSign == 'F'">
                                <div class="input-group" v-if="(!giveDiscountDuringSale && dayStart==='true')" v-on:dblclick="counter += 1, openmodel2('fixDiscount'+index)">
                                    <decimal-to-fixed v-model="prod.fixDiscount" :disable="prod.isFree || isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="prod.isFree || isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                                <div class="input-group" v-else>
                                    <decimal-to-fixed v-model="prod.fixDiscount" :disable="prod.isFree || isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="prod.isFree || isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                            </div>
                        </td>

                        <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                            <taxratedropdown v-model="prod.taxRateId" @update:modelValue="getVatValue(prod.taxRateId, prod)" />
                        </td>

                        <td class="text-end">
                            {{currency}} {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                        </td>
                        <td class="text-end">
                            <a href="javascript:void(0);" v-if="isEditPaidInvoice"><i class="las la-trash-alt text-dark font-16"></i></a>
                            <a href="javascript:void(0);" v-else @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>

        <div class="table-responsive  mt-4" v-if="colorVariants">
            <table class="table add_table_list_bg" style="table-layout:fixed;">
                <thead class="m-0">
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th>
                            {{ $t('SaleItem.Product') }}
                        </th>
                        <th class="text-center">
                            {{ $t('SaleItem.Color') }}
                        </th>
                        <th v-for="size in saleSizeAssortment" class="text-center" :key="size.sizeId">
                            {{size.name}}
                        </th>
                        <th class="text-center">
                            Total
                        </th>
                    </tr>
                </thead>
                <tbody id="sale-item">

                    <tr v-for="(prod, index) in saleProducts" :key="prod.rowId" v-bind:class="{'alert-danger':prod.outOfStock}" class="tble_border_remove" style="background:#EAF1FE ;">
                        <td>{{index+1}}</td>
                        <td>
                            <span>
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </span>

                            <span v-if="products.find(x => x.id == prod.productId).promotionOffer!=undefined && products.find(x => x.id == prod.productId).promotionOffer.fixedDiscount > 0" class="badge badge-pill badge-success">
                                Rs {{(products.find(x => x.id == prod.productId).promotionOffer.fixedDiscount).toFixed(3).slice(0,-1)}}, ({{products.find(x => x.id == prod.productId).promotionOffer.stockLimit - products.find(x => x.id == prod.productId).promotionOffer.quantityOut}})
                            </span>
                            <span v-if="products.find(x => x.id == prod.productId).promotionOffer!=undefined && products.find(x => x.id == prod.productId).promotionOffer.discountPercentage > 0" class="badge badge-pill badge-success">
                                {{(products.find(x => x.id == prod.productId).promotionOffer.discountPercentage).toFixed(3).slice(0,-1)}}%, ({{products.find(x => x.id == prod.productId).promotionOffer.stockLimit - products.find(x => x.id == prod.productId).promotionOffer.quantityOut}})
                            </span>
                            <span v-if="products.find(x => x.id == prod.productId).bundleCategory != undefined" class="badge badge-pill badge-success">
                                {{products.find(x => x.id == prod.productId).bundleCategory.buy}} + {{products.find(x => x.id == prod.productId).bundleCategory.get}}, ({{products.find(x => x.id == prod.productId).bundleCategory.stockLimit - products.find(x => x.id == prod.productId).bundleCategory.quantityOut}})
                            </span>
                        </td>

                        <td class="text-center">
                            {{prod.colorName}}
                        </td>
                        <td v-for="size in prod.saleSizeAssortment" :key="size.sizeId" class="text-center">
                            <input v-model="size.quantity " v-bind:class="size.quantity > size.currentQuantity ? 'input-txt-danger' : ''"
                                   type="text" @focus="$event.target.select()"
                                   @keyup="sizeQtyVerify(prod)" data-toggle="tooltip" data-placement="top" :title="'Current Quantity : ' + size.currentQuantity"
                                   v-bind:disabled="sizeAllowInput(size.sizeId, prod)"
                                   class="form-control text-center tableHoverOn" />
                        </td>
                        <td class="text-center">
                            {{prod.quantity}}
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>

        <div class="row  ">
            <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5" v-if="formName == 'SaleReturn'">

            </div>
            <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5" v-else>
                <div class="mt-4" v-if="isValid('CanAddItem') || isValid('CanViewItem')">
                    <product-dropdown :raw="false"
                                      @update:modelValues="addProduct"
                                      ref="productDropdownRef"
                                      :fromSale="true"
                                      v-bind:key="randerProductAfterNew"
                                      width="100%"
                                      :disable="isEditPaidInvoice" />
                </div>

                <div class="mt-1" v-if="(invoiceWoInventory && isValid('CanAddItem') && isValid('CanViewItem') && isValid('CanViewItem') ) && (isDiscountOnTransaction && saleProducts.length>0)">
                    <textarea class="form-control" style="padding-top: 7px;" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="8" />

                </div>
                <div class="mt-1" v-else-if="(invoiceWoInventory && isValid('CanAddItem') && isValid('CanViewItem') && isValid('CanViewItem') ) && (isDiscountOnTransaction)">
                    <textarea class="form-control" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="6" />

                </div>
                <div class="mt-1" v-else-if="(invoiceWoInventory && isValid('CanAddItem') && isValid('CanViewItem') && isValid('CanViewItem') ) && (!isDiscountOnTransaction && paidVatList.length>0)">
                    <textarea class="form-control" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="7" />

                </div>
                <div class="mt-2" v-else-if="(invoiceWoInventory && isValid('CanAddItem') && isValid('CanViewItem') && isValid('CanViewItem') ) && (!isDiscountOnTransaction)">
                    <textarea class="form-control" style="padding-top: 7px;" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="5" />
                </div>
                <div class="mt-4" v-else-if="isDiscountOnTransaction && saleProducts.length>0">
                    <textarea class="form-control" style="padding-top: 9px;" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="10" />
                </div>
                <div class="mt-4" v-else-if="isDiscountOnTransaction ">
                    <textarea class="form-control" style="padding-top: 7px;" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="8" />
                </div>

                <div class="mt-4" v-else-if="!isDiscountOnTransaction && paidVatList.length>0">
                    <textarea class="form-control" style="padding-top: 9px;" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="9" />
                </div>
                <div class="mt-4" v-else-if="!isDiscountOnTransaction ">
                    <textarea class="form-control" style="padding-top: 11px;" @blur="NoteSave" v-model="note" placeholder="Write Note Here.." rows="7" />
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2 card text-center mt-4" style="background-color: #f1f5fa;">
                <div class="card-body">
                    <span class="fw-bold">No Of Items</span><br />
                    <div style="padding-top: 13px !important;"> <span class="fw-bold pt-2">{{summary.item }}</span></div>
                    <hr>
                    <span class="fw-bold">Total Items</span><br />
                    <div>
                        <span class="fw-bold pt-2">{{summary.qty}}</span>
                    </div>
                </div>

            </div>
            <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5">
                <div class="mt-4" v-bind:key="rendered + 'g'">

                    <table class="table" style="background-color: #f1f5fa;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:68%;">
                                    <span class="fw-bold">{{ $t('SaleItem.GrossTotal') }}  </span>
                                </td>
                                <td class="text-end" style="width:32%;">{{ parseFloat(summary.withDisc).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && isDiscountBeforeVat">
                                <td style="width:48%;" class="px-0">
                                    <span>{{ $t('SaleItem.DiscountBeforeVat') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('SaleItem.ApplyAfterTax') }}</small>
                                        </a>
                                    </span>
                                </td>
                                <td style="width:20%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="transactionLevelDiscount" @update:modelValue="calcuateSummary" />
                                        <button v-if="taxMethod == ('Inclusive' || 'شامل')" class="btn btn-primary" type="button" id="button-addon2" disabled>%</button>
                                        <button v-else class="btn btn-primary" v-on:click="UpdateDiscountField('fixed')" type="button" id="button-addon2">{{isFixedDiscount?'F':'%'}}</button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:32%;">{{ parseFloat(transactionLevelTotalDiscount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr v-if="(isDiscountOnTransaction && isDiscountBeforeVat && transactionLevelDiscount>0)">
                                <td colspan="2" style="width:68%;">
                                    <span style="height:33px !important; ">{{ $t('SaleItem.TotalAfterDiscount') }}</span>

                                </td>

                                <td class="text-end" style="width:32%;">{{ parseFloat(summary.totalAfterDiscount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr v-if="!isDiscountOnTransaction ">
                                <td colspan="2" style="width:68%;">
                                    <span class="fw-bold">{{ $t('SaleItem.DiscountBeforeVat') }}</span>

                                </td>
                                <td class="text-end" style="width:32%;">{{parseFloat(summary.discount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr v-for="(vat,index) in paidVatList" :key="index">
                                <td class="fw-bold" colspan="2" style="width:68%;">{{vat.name}} % ({{taxMethod}})</td>
                                <td class="text-end" style="width:32%;">{{ parseFloat(vat.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && !isDiscountBeforeVat">
                                <td style="width:48%;" class="px-0">
                                    <span style="width:100%;" class="m-0 p-0">{{ $t('SaleItem.DiscountAfterVat') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" style="padding: 6px 4px; border-radius: 0;" v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('SaleItem.ApplyBeforeTax') }}</small>
                                        </a>
                                    </span>
                                </td>
                                <td style="width:20%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="transactionLevelDiscount" @update:modelValue="calcuateSummary" />
                                        <button v-if="taxMethod == ('Inclusive' || 'شامل')" class="btn btn-primary" disabled type="button" id="button-addon2">%</button>
                                        <button v-else class="btn btn-primary" v-on:click="UpdateDiscountField('fixed')" type="button" id="button-addon2">{{isFixedDiscount?'F':'%'}}</button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:32%;">{{ parseFloat(transactionLevelTotalDiscount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && !isDiscountBeforeVat && transactionLevelDiscount>0">
                                <td colspan="2" style="width:68%;">
                                    <span style="height:33px !important; ">{{ $t('SaleItem.TotalAfterDiscount') }}</span>

                                </td>

                                <td class="text-end" style="width:32%;">{{ parseFloat(summary.totalAfterDiscount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="width:48%;">
                                    <input class="form-control" type="text" :value="$t('SaleItem.Adjustment')" style="border: 1px dashed #1761fd;" />
                                </td>
                                <td style="width:20%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="adjustment" @update:modelValue="calcuateSummary" :disable="isEditPaidInvoice" />
                                        <button v-on:click="OnChangeOveallDiscount" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{adjustmentSign}}</button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:32%;">{{adjustmentSign == '+'? parseFloat(adjustment).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"):(-1)*(parseFloat(adjustment).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")) }}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:68%;">
                                    <span style="font-weight:bolder; font-size:16px"> {{ $t('SaleItem.TotalDuewithVAT') }} ({{currency}})</span>
                                </td>
                                <td class="text-end" style="width: 32%; font-weight: bolder; font-size: 16px">{{ parseFloat(summary.withVat).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>

        </div>

        <authorize-user-model :authorize="authorize"
                              :show="show"
                              v-if="show"
                              @result="result"
                              @close="show = false" />

        <add-serial-model :item="serialItem"
                          :show="showSerial"
                          v-if="showSerial"
                          @update:modelValue="updateSerial"
                          @close="showSerial = false" />
    </div>
</template>

<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    /*import Multiselect from 'vue-multiselect'*/
    export default {
        name: "SaleItem",
        props: ['saleItems', 'isEditPaidInvoice', 'formName', 'wareHouseId', 'saleOrderId', 'newtaxMethod', 'wholesale', 'taxRateId', 'adjustmentProp', 'adjustmentSignProp', 'isDiscountOnTransaction', 'transactionLevelDiscountProp', 'isFixed', 'isbeforetax', 'noteVal'],
        mixins: [clickMixin],
        //components: {
        //    Multiselect
        //},
        data: function () {
            return {
                taxMethod: this.newtaxMethod,
                transactionLevelDiscount: 0,
                randerProductAfterNew: 0,
                adjustment: 0,
                counterForBarCodeRequest: 0,
                adjustmentSign: '+',
                isDiscountBeforeVat: false,
                transactionLevelTotalDiscount: 0,
                saleSizeAssortment: [],
                colorList: [],
                options: [],
                dayStart: '',
                note: '',
                colorVariants: false,
                isFifo: false,
                isSerial: false,
                soInventoryReserve: false,
                decimalQuantity: false,
                invoiceWoInventory: false,
                fixDiscount: '',
                discount: '',
                bundle: '',
                counter: 0,
                isMultiUnit: '',
                isAuthour: {
                    changePriceDuringSale: false,
                    giveDiscountDuringSale: false,
                    column: '',
                },
                changePriceDuringSale: false,
                giveDiscountDuringSale: false,
                useQuantity: false,
                show: false,
                authorize: {
                    column: '',
                    userName: '',
                    password: '',
                },
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                saleProducts: [],
                loading: false,
                vats: [],
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    totalAfterDiscount: 0,
                    vat: 0,
                    withVat: 0,
                    bundleAmount: 0,
                    totalCarton: 0,
                    totalPieces: 0
                },
                currency: '',
                count: 0,
                productList: [],
                serialItem: '',
                showSerial: false,
                vatSelectOnSale: false,
                wholsalePriceActive: true,
                saleDefaultVat: '',
                paidVatList: []
            };
        },
        validations() { },
        filter: {},
        methods: {
            UpdateDiscountField: function (prop) {

                this.transactionLevelDiscount = 0;
                if (prop === 'fixed') {
                    this.isFixedDiscount = this.isFixedDiscount ? false : true;
                }

                if (prop === 'beforeTax') {
                    this.isDiscountBeforeVat = this.isDiscountBeforeVat ? false : true;
                }
                this.calcuateSummary();

                this.$emit("discountChanging", this.isFixedDiscount, this.isDiscountBeforeVat);
            },

            OnChangeOveallDiscount: function () {

                this.adjustmentSign = this.adjustmentSign == '+' ? '-' : '+'
                this.calcuateSummary()
            },

            NoteSave: function () {
                this.$emit("NoteSave", this.note);
            },

            NewItemChangeDiscount: function (prod) {
                if (prod.discountSign === '%') {
                    prod.discountSign = 'F';
                    prod.fixDiscount = 0
                    prod.discount = 0
                } else {
                    prod.discountSign = '%';
                    prod.discount = 0
                    prod.fixDiscount = 0
                }
            },

            OnChangeDiscountType: function (prod) {
                if (prod.discountSign === '%') {
                    prod.discountSign = 'F';
                    prod.fixDiscount = 0
                    prod.discount = 0
                    this.updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)
                } else {
                    prod.discountSign = '%';
                    prod.discount = 0
                    prod.fixDiscount = 0
                    this.updateLineTotal(prod.discount, 'discount', prod)
                }
            },
            AddSerial: function (item) {

                this.serialItem = item;
                this.showSerial = true;
            },

            updateSerial: function (serial, item) {

                var prod = this.saleProducts.find(x => x.rowId == item.rowId);
                if (prod != undefined) {
                    prod.serial = serial;
                }
                this.showSerial = false;
            },

            result: function (x) {
                this.isAuthour = x;
            },
            openmodel: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },
            openmodel1: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },
            openmodel2: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },

            changeProduct: function (NewProdId, rowId) {
                this.saleProducts = this.saleProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);
            },

            GetColorName: function (colorId, product) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/ColorDetail?id=' + colorId + '&productId=' + product.productId + '&isVariance=true', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        product.saleSizeAssortment.forEach(function (item) {
                            var size = response.data.variationInventories.find(x => x.sizeId == item.sizeId);
                            if (size != undefined) {
                                item.currentQuantity = size.quantity;
                            } else {
                                item.currentQuantity = 0;
                            }
                        });

                        if (colorId != null && colorId != undefined && colorId != '') {
                            product.colorName = response.data.name;
                            product.currentQuantity = response.data.quantity;
                        } else {
                            product.colorName = '';
                        }
                    }
                });
            },

            sizeAllowInput: function (sizeId, product) {

                if (product.productSizes != null && product.productSizes != undefined && product.productSizes.length != 0) {
                    var size = product.productSizes.find(x => x.sizeId == sizeId);
                    if (size != undefined) {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return true;
                }
            },

            sizeQtyVerify: function (product) {

                if (product.saleSizeAssortment != null && product.saleSizeAssortment != undefined) {
                    if (parseFloat(product.totalPiece) < product.saleSizeAssortment.reduce(function (a, c) {
                        return a + parseFloat(c.quantity == '' ? 0 : c.quantity)
                    }, 0)) {
                        product['outOfStock'] = true
                    } else {
                        product['outOfStock'] = false
                    }
                } else {
                    product['outOfStock'] = true
                }
            },

            changeVatInformation: function (value, prop) {
                var root = this;
                if (prop == 'TaxMethod') {
                    root.saleProducts.forEach(function (item) {
                        item.taxMethod = value;
                        root.taxMethod = value;
                        root.updateLineTotal(item.unitPrice, "unitPrice", item);
                    });
                }
                else if (prop == 'DiscountType') {
                    root.transactionLevelDiscount = 0;
                    root.saleProducts.forEach(function (item) {
                        item.discount = 0;
                        item.fixDiscount = 0;
                        root.updateLineTotal(item.unitPrice, "unitPrice", item);
                    });
                }
                else if (prop == 'TaxRateId') {
                    root.saleProducts.forEach(function (item) {
                        item.taxRateId = value;
                        root.updateLineTotal(item.unitPrice, "unitPrice", item);
                    });
                }

            },

            calcuateSummary: function () {
                this.summary.item = this.saleProducts.length;
                if (this.decimalQuantity) {
                    this.summary.totalPieces = this.saleProducts.reduce((totalQty, prod) => totalQty + parseFloat(prod.quantity), 0);
                } else {
                    this.summary.totalPieces = this.saleProducts.reduce((totalQty, prod) => totalQty + parseInt(prod.quantity), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.totalCarton = this.saleProducts.reduce((totalCarton, prod) => totalCarton + parseFloat(prod.highQty), 0);
                } else {
                    this.summary.totalCarton = this.saleProducts.reduce((totalCarton, prod) => totalCarton + parseInt(prod.highQty), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                } else {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }

                this.summary.total = this.saleProducts.reduce((total, prod) => total + parseFloat(prod.lineTotal), 0).toFixed(2);

                //this.summary.total = this.saleProducts.reduce((total, prod) =>
                //    total + (prod.totalPiece - prod.offerQuantity) * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                if (!this.isDiscountOnTransaction) {
                    this.transactionLevelDiscount = 0;
                }
                var vatRate = 0;
                var discountOnly = 0;
                /*var vatOnFixedDiscount = 0;*/
                var discountForInclusiveVat = 0;
                var root = this;
                const taxIdList = [...new Set(this.saleProducts.map(item => item.taxRateId))];
                root.paidVatList = []
                //'isDiscountOnTransaction', 'transactionLevelDiscount'
                taxIdList.forEach(function (taxId) {
                    vatRate = root.vats.find((value) => value.id == taxId);
                    var filteredRecord = root.saleProducts
                        .filter((x) => x.taxRateId === taxId);
                    var totalQtyWithotFree = root.saleProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.discount != 0 || x.discount != "")
                        .reduce((discount, prod) => discount + (prod.promotionOfferQuantity > 0 ? (prod.promotionOfferQuantity ? (((prod.promotionOfferQuantity * prod.unitPrice) * prod.discount) / 100) : 0) : (prod.totalPiece ? (((prod.totalPiece * prod.unitPrice) * prod.discount) / 100) : 0)), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                        .reduce((discount, prod) => discount + (prod.totalPiece ? ((root.taxMethod == ("Inclusive" || "شامل") ? prod.fixDiscount : prod.fixDiscount)) : 0), 0);

                    //vatOnFixedDiscount += filteredRecord
                    //    .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                    //    .reduce((discount, prod) => discount + (prod.totalPiece ? ((root.taxMethod == ("Inclusive" || "شامل") ? prod.fixDiscount * vatRate.rate / 100 : 0)) : 0), 0);

                    var paidVat = filteredRecord
                        .reduce((vat, prod) => (vat + ((prod.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.lineTotal) - (root.isDiscountBeforeVat ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : 0)) * vatRate.rate) / (100 + vatRate.rate) : ((parseFloat(prod.lineTotal) - (root.isDiscountBeforeVat && !root.isFixedDiscount && root.isDiscountOnTransaction ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : (root.isDiscountBeforeVat && root.isFixedDiscount && root.isDiscountOnTransaction ? (root.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.totalPiece) : 0))) * vatRate.rate) / 100)), 0).toFixed(3).slice(0, -1)
                    discountForInclusiveVat += parseFloat(filteredRecord
                        .reduce((vat, prod) => (vat + ((prod.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.lineTotal) * vatRate.rate) / (100 + vatRate.rate) : 0)), 0).toFixed(3).slice(0, -1))

                    root.paidVatList.push({
                        name: vatRate.name,
                        nameArabic: vatRate.nameArabic,
                        amount: paidVat
                    })

                });
                //root.transactionLevelDiscount = root.transactionLevelDiscount;
                // this.summary.discount = discountOnly;
                /*this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(3).slice(0, -1);*/
                this.summary.withDisc = this.summary.total;

                this.summary.vat = this.paidVatList.reduce((vat, paidVat) => (vat + parseFloat(paidVat.amount)), 0).toFixed(3).slice(0, -1);

                var exclusiveVat = parseFloat(this.summary.vat);

                this.transactionLevelTotalDiscount = ((this.isDiscountBeforeVat && this.isDiscountOnTransaction) ? (this.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc - discountForInclusiveVat) / 100) : (this.isFixedDiscount ? parseFloat(this.transactionLevelDiscount) : parseFloat(this.transactionLevelDiscount) * this.summary.withDisc / 100)) : (this.isFixedDiscount ? parseFloat(this.transactionLevelDiscount) : (parseFloat(this.summary.withDisc) + parseFloat(exclusiveVat)) * parseFloat(this.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                var totalIncDisc = (this.isDiscountBeforeVat && this.isDiscountOnTransaction && this.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc) / 100) : parseFloat(this.transactionLevelTotalDiscount)
                this.adjustment = (this.adjustment == '' || this.adjustment == null) ? 0 : parseFloat(this.adjustment)
                this.summary.withVat = (parseFloat(this.summary.withDisc) + (this.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(exclusiveVat)) + (this.adjustmentSign == '+' ? this.adjustment : (-1) * this.adjustment)).toFixed(3).slice(0, -1);

                this.summary.withVat = (parseFloat(this.summary.withVat) - totalIncDisc).toFixed(3).slice(0, -1);
                if (this.isDiscountOnTransaction) {
                    this.summary.discount = totalIncDisc;
                }
                else {
                    this.summary.discount = discountOnly
                }
                this.summary.totalAfterDiscount = this.isDiscountOnTransaction && !this.isDiscountBeforeVat ? parseFloat(this.summary.withVat - this.summary.discount).toFixed(3).slice(0, -1) : parseFloat((this.summary.total - this.summary.discount).toFixed(3).slice(0, -1));




                console.log(this.summary);
                //calculate bundle Amount
                //if (this.saleProducts.filter(x => x.isBundleOffer).length > 0) {

                //    //get bundle get quantity
                //    var bundle = {
                //        item: 0,
                //        qty: 0,
                //        total: 0,
                //        discount: 0,
                //        withDisc: 0,
                //        vat: 0,
                //        withVat: 0,
                //        quantityLimit: 0
                //    };

                //    var bundleProducts = this.saleProducts.filter(x => x.isBundleOffer != undefined && x.offerQuantity > 0);

                //    bundle.total = bundleProducts.reduce((total, prod) =>
                //        total + prod.offerQuantity * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                //    //var bundleExclusiveTax = bundleProducts.reduce((total, prod) =>
                //    //    total + (prod.taxMethod == "Exclusive" ? (bundle.total * prod.rate/100) : 0), 0);

                //    var discountBundle = bundleProducts.filter((x) => x.discount != 0 || x.discount != "")
                //        .reduce((discount, prod) =>
                //            discount + (prod.offerQuantity * prod.unitPrice * prod.discount) / 100, 0);

                //    var fixDiscountBundle = bundleProducts
                //        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                //        .reduce((discount, prod) => discount + prod.fixDiscount, 0);

                //    bundle.discount = (parseFloat(discountBundle) + parseFloat(fixDiscountBundle)).toFixed(3).slice(0, -1);

                //    bundle.withDisc = (bundle.total - bundle.discount).toFixed(3).slice(0, -1);

                //    bundle.vat = bundleProducts
                //        .reduce((vat, prod) => vat + (((prod.unitPrice * prod.offerQuantity) -
                //            ((prod.unitPrice * prod.offerQuantity * prod.discount) / 100)) *
                //            parseFloat(prod.rate)) / ((prod.taxMethod == "Exclusive" || prod.taxMethod == "غير شامل") ? 100 : prod.rate + 100), 0).toFixed(3).slice(0, -1);

                //    this.summary.bundleAmount = (parseFloat(bundle.withDisc) + parseFloat(exclusiveVat)).toFixed(3).slice(0, -1);
                //    this.summary.withVat = (this.summary.withVat - bundle.withDisc);
                //}
                //else {
                //    this.summary.bundleAmount = 0;
                //}
                if (this.isDiscountOnTransaction) {
                    this.summary.discount = totalIncDisc;
                }
                else {
                    this.summary.discount = discountOnly
                }
                this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount), this.isFixed, this.isBeforeTax);

                this.$emit("summary", this.summary, this.isFixedDiscount, this.isDiscountBeforeVat);
            },

            updateLineTotal: function (e, prop, product) {

                debugger; //eslint-disable-line
                var root = this;
                if (e != undefined) {
                    var discount = (product.discount == 0 || product.discount == "") ? (product.fixDiscount == 0 || product.fixDiscount == "") ? 0 : product.fixDiscount : product.discount;

                    if (prop == "unitPrice") {
                        product.unitPrice = e;
                    }

                    if (prop == "quantity") {
                        if (e <= 0 || e == '') {
                            e = 0;
                        }
                        if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                            e = parseFloat(String(e).slice(0, -1))
                        product.quantity = this.decimalQuantity ? e : Math.round(e);
                    }
                    if (prop == "highQty") {
                        if (e < 0 || e == '' || e == undefined) {
                            e = 0;
                        }
                        product.highQty = Math.round(e);
                    }
                    product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);


                    if (product.productId != null) {
                        var prod = root.products.find((x) => x.id == product.productId);

                        if (product.bundleId != null) {
                            if (product.totalPiece > product.buy) {
                                product['bundleOffer'] = product.buy.toString() + " + " + product.get.toString()
                                product['isBundleOffer'] = true


                            } else {
                                product['outOfStock'] = ""
                                product['outOfStock'] = false


                            }
                            //bundle category calculation
                            if (parseFloat(product.totalPiece) >= (product.get + product.buy)) {
                                var offer = Math.floor(parseFloat(product.totalPiece) / (product.get + product.buy));
                                if ((prod.bundleCategory.quantityOut + offer) <= product.stockLimit) {
                                    if (offer <= product.offerQuantityLimit) {
                                        product.offerQuantity = offer * product.get;
                                    } else {
                                        product.offerQuantity = product.offerQuantityLimit * product.get;
                                    }
                                } else {
                                    product.offerQuantity = 0;
                                    product.discount = 0;
                                    product.fixDiscount = 0;
                                    discount = 0;
                                }
                            } else {
                                product.offerQuantity = 0;
                                product.discount = 0;
                                product.fixDiscount = 0;
                                discount = 0;
                            }
                            //bundle category calculation end
                        }
                    }

                    if (product.wholesaleQuantity != null && product.wholesaleQuantity != undefined) {
                        if (product.wholesaleQuantity > 0 && product.totalPiece >= product.wholesaleQuantity) {
                            product.unitPrice = product.wholesalePrice;
                        } else {
                            product.unitPrice = product.salePrice;
                        }
                    }

                    if (parseInt(product.schemeQuantity) > 0 && parseInt(product.scheme) > 0 && parseInt(product.schemeQuantity) <= product.totalPiece) {
                        product.discountSign = 'F';

                        product.fixDiscount = ((product.totalPiece * product.unitPrice) /
                            ((parseInt(product.schemeQuantity) > 0 ? parseInt(product.schemeQuantity) : 0) + (parseInt(product.scheme) > 0 ? parseInt(product.scheme) : 0))) * (parseInt(product.scheme) > 0 ? parseInt(product.scheme) : 0);

                    } else {
                        if (parseInt(product.schemeQuantity) > 0 && parseInt(product.schemeQuantity) > product.totalPiece) {
                            product.fixDiscount = 0;
                        }
                        if (parseInt(product.schemeQuantity) == 0 || parseInt(product.scheme) == 0) {
                            product.fixDiscount = 0;
                        }
                    }

                    if (!this.invoiceWoInventory && product.productId != null && (root.formName == 'SaleInvoice' || root.formName == 'ServiceSaleOrder')) {
                        var bundleQuantity = product.bundleOfferQuantity == undefined ? 0 : product.bundleOfferQuantity;
                        if (parseFloat(product.totalPiece) + bundleQuantity > (product.currentQuantity + ((this.saleOrderId != null && this.saleOrderId != '' && this.soInventoryReserve) ? parseFloat(product.soQty) : 0))) {
                            product['outOfStock'] = true
                        }
                        else {
                            product['outOfStock'] = false
                        }

                    }

                    //End Calculate offer
                    if (prop == "discount") {
                        if (e == "" || e < 0) {
                            e = 0;
                        } else if (e > 100) {
                            e = 100;
                        }
                        product.discount = e;
                    }

                    if (prop == "fixDiscount") {
                        if (e == "" || e < 0) {
                            e = 0;
                        } else if (e > product.unitPrice) {
                            e = product.unitPrice;
                        }
                        product.fixDiscount = e;
                    }

                    var vat = 0;
                    var total = 0;
                    var calculateVAt = 0;

                    //here we will select quantity after deduct bundle quantity
                    if (product.promotionId != null) {
                        if (product.promotionType == 'BuyNGetNSameGroup' || product.promotionType == 'BuyNGetNSameItem') {
                            product.promotionName = product.buy + " + " + product.get;
                            if (parseFloat(product.totalPiece) >= (product.get + product.buy)) {
                                offer = Math.floor(parseFloat(product.totalPiece) / (product.get + product.buy));
                                if ((product.quantityOut + offer) <= product.stockLimit) {
                                    if (offer <= product.upToQuantity) {
                                        product.offerQuantity = offer * product.get;
                                    } else {
                                        product.offerQuantity = product.upToQuantity * product.get;
                                    }
                                } else {
                                    product.offerQuantity = (product.stockLimit - product.quantityOut) * product.get;
                                }
                            } else {
                                product.offerQuantity = 0;
                                product.discount = 0;
                                product.fixDiscount = 0;
                                discount = 0;
                            }
                            vat = this.vats.find((value) => value.id == product.taxRateId);
                            discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                            product.lineTotal = (product.totalPiece - product.offerQuantity) * product.unitPrice - discount;

                            calculateVAt = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? (product.lineTotal - discount) * vat.rate / (vat.rate + 100) : product.lineTotal * vat.rate / 100;
                            product.lineItemVAt = calculateVAt;

                        }

                        if (product.promotionType == 'BuyNGetNAnother') {
                            product.promotionName = product.buy + " + " + product.get;
                            if (parseFloat(product.totalPiece) >= product.buy) {
                                offer = Math.floor(parseFloat(product.totalPiece) / product.buy);

                                if (offer <= product.upToQuantity) {
                                    if ((product.quantityOut + offer) <= product.stockLimit) {
                                        product.promotionOfferQuantity = offer * product.get;

                                        var prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal(offer * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, offer * product.get);
                                        }
                                    } else {
                                        product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.get;

                                        prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal((product.stockLimit - product.quantityOut) * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, (product.stockLimit - product.quantityOut) * product.get);
                                        }
                                    }
                                } else {

                                    if ((product.quantityOut + offer) <= product.stockLimit) {
                                        product.promotionOfferQuantity = product.upToQuantity * product.get;

                                        prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal(product.upToQuantity * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, product.upToQuantity * product.get);
                                        }
                                    } else {
                                        product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.get;

                                        prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal((product.stockLimit - product.quantityOut) * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, (product.stockLimit - product.quantityOut) * product.get);
                                        }
                                    }
                                }

                            } else {
                                if (this.saleProducts.find(x => x.productId == product.getProductId) != undefined) {
                                    this.removeProduct(this.saleProducts.find(x => x.productId == product.getProductId).rowId);
                                }
                                product.offerQuantity = 0;
                                product.discount = 0;
                                product.fixDiscount = 0;
                                discount = 0;
                            }
                            vat = this.vats.find((value) => value.id == product.taxRateId);
                            discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                            product.lineTotal = (product.totalPiece - product.offerQuantity) * product.unitPrice - discount;

                            calculateVAt = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? (product.lineTotal - discount) * vat.rate / (vat.rate + 100) : product.lineTotal * vat.rate / 100;
                            product.lineItemVAt = calculateVAt;

                        }

                        if (product.promotionType == 'GroupNFixOrPercentageDiscount' || product.promotionType == 'ItemNFixOrPercentageDiscount') {
                            if (prod.promotionOffer.discountType == '%') {
                                product.promotionName = prod.promotionOffer.discount + "%";
                                product.discountSign = '%';
                                if (product.totalPiece >= product.offerQuantityLimit) {
                                    offer = Math.floor(parseFloat(product.totalPiece) / product.offerQuantityLimit);
                                    if (offer <= product.upToQuantity) {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.promotionOfferQuantity = offer * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        } else {
                                            product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        }
                                    } else {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.promotionOfferQuantity = product.upToQuantity * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        } else {
                                            product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        }

                                    }
                                } else {
                                    product.discount = 0;
                                    product.fixDiscount = 0;
                                    discount = 0;
                                }

                            } else {
                                product.promotionName = this.currency + ' ' + prod.promotionOffer.discount;
                                product.discountSign = 'F'
                                if (product.totalPiece >= product.offerQuantityLimit) {
                                    offer = Math.floor(parseFloat(product.totalPiece) / product.offerQuantityLimit);
                                    if (offer <= product.upToQuantity) {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.fixDiscount = offer * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = offer;
                                        } else {
                                            product.fixDiscount = (product.stockLimit - product.quantityOut) * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = offer;
                                        }

                                    } else {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.fixDiscount = product.upToQuantity * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = product.upToQuantity;
                                        } else {
                                            product.fixDiscount = (product.stockLimit - product.quantityOut) * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = product.upToQuantity;
                                        }

                                    }
                                } else {
                                    product.discount = 0;
                                    product.fixDiscount = 0;
                                    discount = 0;
                                }
                            }

                            vat = this.vats.find((value) => value.id == product.taxRateId);
                            total = product.totalPiece * product.unitPrice;
                            if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                calculateVAt = product.discountSign == 'F' ? (((total * 100) / (100 + vat.rate)) - discount) * vat.rate / 100 : (total - discount) * vat.rate / (vat.rate + 100);
                                product.lineItemVAt = calculateVAt;
                                product.lineTotal = product.discountSign == 'F' ? ((total * 100 / (100 + vat.rate)) - discount) * (100 + vat.rate) / 100 : total - discount;
                            } else {
                                product.lineItemVAt = ((total - discount) * vat.rate) / 100;
                                product.lineTotal = total - discount;
                            }
                        }
                    } else if (product.isBundleOffer) {
                        //isDiscountBeforeVat
                        vat = this.vats.find((value) => value.id == product.taxRateId);
                        discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                        product.lineTotal = (product.totalPiece - product.offerQuantity) * product.unitPrice - discount;

                        calculateVAt = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? (product.lineTotal - discount) * vat.rate / (vat.rate + 100) : product.lineTotal * vat.rate / 100;
                        product.lineItemVAt = calculateVAt;

                    } else {
                        //isDiscountBeforeVat
                        vat = this.vats.find((value) => value.id == product.taxRateId);
                        discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                        product.lineTotal = product.totalPiece * product.unitPrice - discount;

                        calculateVAt = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? (product.lineTotal - discount) * vat.rate / (vat.rate + 100) : product.lineTotal * vat.rate / 100;
                        product.lineItemVAt = calculateVAt;

                    }

                    product.discountAmount = discount;
                    product.vatAmount = product.lineItemVAt;
                    product.totalAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? product.lineTotal : product.lineTotal + product.lineItemVAt;
                    product.grossAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? ((product.totalPiece - product.offerQuantity) * product.unitPrice) * 100 / (100 + vat.rate) : (product.totalPiece - product.offerQuantity) * product.unitPrice;

                    this.saleProducts['product'] = product
                    this.calcuateSummary();
                    this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                }
            },
            ClearList: function () {

                this.saleProducts = [];
                this.products = [];
            },

            CheckRecordInProduct: function () {
                return this.$refs.productDropdownRef.productListCheck();
            },

            getProductFromProductDropdown: function (productId, qty) {
                var newProduct = this.$refs.productDropdownRef.productListValueCompare(productId);

                if (this.products.find(x => x.id == productId) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }
                var prod = this.products.find((x) => x.id == productId);

                var rate = 0;
                var taxRateId = '';
                var taxMethod = '';
                if (this.saleDefaultVat == 'DefaultVat' || this.saleDefaultVat == 'DefaultVatItem') {
                    if (prod.taxRateId != "00000000-0000-0000-0000-000000000000" && prod.taxRateId != undefined) {
                        rate = this.getVatValue(prod.taxRateId, prod);
                    }
                    taxRateId = prod.taxRateId;
                    taxMethod = prod.taxMethod;
                }
                if (this.saleDefaultVat == 'DefaultVatHead' || this.saleDefaultVat == 'DefaultVatHeadItem') {
                    if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {

                        rate = this.getVatValue(this.taxRateId, prod);
                    }
                    taxRateId = this.taxRateId;
                    taxMethod = this.taxMethod;
                }

                var uid = this.createUUID();
                this.saleProducts.push({
                    rowId: uid,
                    productId: prod.id,
                    description: prod.englishName,
                    unitPrice: '0',
                    quantity: qty,
                    soQty: 0,
                    highQty: 0,

                    discount: 0,
                    fixDiscount: 0,
                    discountSign: '%',

                    promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                    bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                    promotionName: '',
                    promotionType: prod.promotionOffer != null ? prod.promotionOffer.promotionType : null,
                    buy: prod.bundleCategory != null ? prod.bundleCategory.buy : prod.promotionOffer != null ? prod.promotionOffer.buy : 0,
                    get: prod.bundleCategory != null ? prod.bundleCategory.get : prod.promotionOffer != null ? prod.promotionOffer.get : 0,
                    offerQuantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : (prod.promotionOffer != null ? prod.promotionOffer.baseQuantity : 0),
                    stockLimit: prod.bundleCategory != null ? prod.bundleCategory.stockLimit : (prod.promotionOffer != null ? prod.promotionOffer.stockLimit : 0),
                    quantityOut: prod.bundleCategory != null ? prod.bundleCategory.quantityOut : (prod.promotionOffer != null ? prod.promotionOffer.quantityOut : 0),
                    upToQuantity: prod.promotionOffer == null ? 0 : prod.promotionOffer.upToQuantity,
                    includingBaseQuantity: prod.promotionOffer == null ? false : prod.promotionOffer.includingBaseQuantity,
                    getProductId: prod.promotionOffer == null ? '' : prod.promotionOffer.getProductId,
                    offerQuantity: 0,
                    promotionOfferQuantity: 0,

                    lineItemVAt: 0,
                    grossAmount: 0,
                    batchExpiry: prod.batchExpiry,
                    batchNo: prod.batchNo,
                    inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                    currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                    taxRateId: taxRateId,
                    taxMethod: taxMethod,
                    saleReturnDays: prod.saleReturnDays,
                    scheme: prod.scheme,
                    schemeQuantity: prod.schemeQuantity,
                    wholesalePrice: prod.wholesalePrice,
                    salePrice: prod.salePrice,
                    wholesaleQuantity: prod.wholesaleQuantity,
                    schemePhysicalQuantity: 0,
                    rate: rate,
                    serialNo: '',
                    serial: '',
                    guaranteeDate: '',
                    isSerial: newProduct.serial,
                    guarantee: newProduct.guarantee,
                    lineTotal: prod.salePrice * 1,
                    unitPerPack: newProduct.unitPerPack,
                    levelOneUnit: prod.levelOneUnit,
                    basicUnit: prod.basicUnit,
                    productSizes: prod.productSizes,
                    colorId: '',
                    colorName: '',
                    isFree: true,
                    highUnitPrice: newProduct.highUnitPrice,
                });

                var product = this.saleProducts.find((x) => {
                    return x.productId == productId && x.rowId == uid;
                });

                this.getVatValue(product.taxRateId, product);

                this.updateLineTotal(product.quantity, "quantity", product);
                this.updateLineTotal(product.highQty, "highQty", product);

                this.product.id = "";
                this.rendered++;
            },

            updateBatch: function (productId, batch) {

                var prd = this.saleProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.batchNo = batch.batchNumber;
                    prd.batchExpiry = batch.expiryDate;
                }
                this.updateLineTotal(prd.quantity, "quantity", prd);
            },

            addProduct: function (productId, newProduct, qty, price, so, quantityOut, soItem, batch, isTemplate) {
                


                var priceRecord = this.$store.ispriceRecordList;
                if (priceRecord !== null && priceRecord.length > 0) {
                    var priceRecordObj = priceRecord.find(x => x.productId == newProduct.id);
                    if (priceRecordObj != null) {

                        newProduct.salePrice = priceRecordObj.newPrice;

                    }
                }

                var uid = this.createUUID();
                if (isTemplate == undefined) {
                    isTemplate = false;
                }

                if (isTemplate) {
                    newProduct = this.$refs.productDropdownRef.productListValueCompare(productId);
                }

                if (this.saleProducts.some(x => x.productId == productId) && !this.isFifo && !this.colorVariants) {

                    var prd = this.saleProducts.find(x => x.productId == productId);
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else if (!so && batch != undefined && this.saleProducts.some(x => x.productId == productId && x.batchNo == batch.batchNumber) && this.isFifo) {
                    var prd1 = this.saleProducts.find(x => x.productId == productId && x.batchNo == newProduct.batchNo);
                    prd1.quantity++;
                    this.updateLineTotal(prd1.quantity, "quantity", prd1);
                }
                else {
                    var prod = ''
                    if (so && this.isFifo) {
                        this.products.push({
                            rowId: soItem.id,
                            arabicName: newProduct.arabicName,
                            assortment: newProduct.assortment,
                            barCode: newProduct.barCode,
                            basicUnit: soItem.product.basicUnit,
                            batchExpiry: soItem.expiryDate,
                            batchNo: soItem.batchNo,
                            brandId: newProduct.brandId,
                            bundleCategory: newProduct.bundleCategory,
                            category: newProduct.category,
                            categoryId: newProduct.categoryId,
                            code: newProduct.code,
                            colorId: newProduct.colorId,
                            colorName: newProduct.colorName,
                            discount: soItem.discount,
                            fixDiscount: soItem.fixDiscount,
                            colorNameArabic: newProduct.colorNameArabic,
                            currentQuantity: soItem.product.inventory.currentQuantity,
                            description: newProduct.description,
                            englishName: newProduct.englishName,
                            guarantee: soItem.product.guarantee,
                            id: newProduct.id,
                            image: newProduct.image,
                            inventory: soItem.product.inventory,
                            inventoryBatch: soItem.product.inventoryBatch,
                            isActive: newProduct.isActive,
                            isExpire: newProduct.isExpire,
                            isRaw: newProduct.isRaw,
                            displayName: newProduct.displayName,
                            length: soItem.product.length,
                            levelOneUnit: soItem.product.levelOneUnit,
                            originId: soItem.product.originId,
                            promotionOffer: soItem.product.promotionOffer,
                            purchasePrice: newProduct.purchasePrice,
                            salePrice: newProduct.salePrice,
                            salePriceUnit: newProduct.salePriceUnit,
                            saleReturnDays: soItem.product.saleReturnDays,
                            serial: soItem.product.serial,
                            serviceItem: newProduct.serviceItem,

                            shelf: newProduct.shelf,
                            sizeId: newProduct.sizeId,
                            sizeName: newProduct.sizeName,
                            sizeNameArabic: newProduct.sizeNameArabic,
                            stockLevel: newProduct.stockLevel,
                            styleNumber: newProduct.styleNumber,
                            subCategoryId: newProduct.subCategoryId,
                            taxMethod: soItem.product.taxMethod,
                            taxRate: newProduct.taxRate,
                            taxRateId: soItem.product.taxRateId,
                            taxRateValue: newProduct.taxRateValue,
                            unit: newProduct.unit,
                            unitId: newProduct.unitId,

                            unitPerPack: newProduct.unitPerPack,
                            width: newProduct.width,
                            highUnitPrice: newProduct.highUnitPrice,
                            discountSign: '%',
                        });
                        prod = this.products.find((x) => x.rowId == soItem.id);
                    } else if (this.isFifo && (batch != undefined || batch != null)) {

                        var inventoryData = {
                            autoNumbering: newProduct.inventory.autoNumbering,
                            averagePrice: newProduct.inventory.averagePrice,
                            batchNumber: newProduct.inventory.batchNumber,
                            bundleId: newProduct.inventory.bundleId,
                            buy: newProduct.inventory.buy,
                            currentQuantity: newProduct.inventory.currentQuantity,
                            currentStockValue: newProduct.inventory.currentStockValue,
                            date: newProduct.inventory.date,
                            documentId: newProduct.inventory.documentId,
                            documentNumber: newProduct.inventory.documentNumber,
                            expiryDate: newProduct.inventory.expiryDate,
                            get: newProduct.inventory.get,
                            id: newProduct.inventory.id,
                            isActive: newProduct.inventory.isActive,
                            isOpen: newProduct.inventory.isOpen,
                            price: newProduct.inventory.price,
                            product: newProduct.inventory.product,
                            description: newProduct.description,
                            productId: newProduct.inventory.productId,
                            promotionId: newProduct.inventory.promotionId,
                            quantity: newProduct.inventory.quantity,
                            remainingQuantity: newProduct.inventory.remainingQuantity,
                            salePrice: newProduct.inventory.salePrice,
                            serial: newProduct.inventory.serial,
                            stock: newProduct.inventory.stock,
                            stockId: newProduct.inventory.stockId,
                            transactionType: newProduct.inventory.transactionType,
                            wareHouseId: newProduct.inventory.wareHouseId,
                            warrantyDate: newProduct.inventory.warrantyDate,
                            highUnitPrice: newProduct.highUnitPrice,
                            discountSign: '%',
                        };

                        this.products.push({
                            rowId: uid,
                            arabicName: newProduct.arabicName,
                            assortment: newProduct.assortment,
                            barCode: newProduct.barCode,
                            basicUnit: newProduct.basicUnit,
                            batchExpiry: newProduct.batchExpiry,
                            batchNo: newProduct.batchNo,
                            brandId: newProduct.brandId,
                            bundleCategory: newProduct.bundleCategory,
                            category: newProduct.category,
                            categoryId: newProduct.categoryId,
                            code: newProduct.code,
                            colorId: newProduct.colorId,
                            colorName: newProduct.colorName,
                            displayName: newProduct.displayName,
                            colorNameArabic: newProduct.colorNameArabic,
                            currentQuantity: newProduct.currentQuantity,
                            description: newProduct.description,
                            englishName: newProduct.englishName,
                            guarantee: newProduct.guarantee,
                            id: newProduct.id,
                            image: newProduct.image,
                            inventory: inventoryData,
                            inventoryBatch: newProduct.inventoryBatch,
                            isActive: newProduct.isActive,
                            isExpire: newProduct.isExpire,
                            isRaw: newProduct.isRaw,

                            length: newProduct.length,
                            levelOneUnit: newProduct.levelOneUnit,
                            originId: newProduct.originId,
                            promotionOffer: newProduct.promotionOffer,
                            purchasePrice: newProduct.purchasePrice,
                            salePrice: newProduct.salePrice,
                            salePriceUnit: newProduct.salePriceUnit,
                            saleReturnDays: newProduct.saleReturnDays,
                            serial: newProduct.serial,
                            serviceItem: newProduct.serviceItem,

                            shelf: newProduct.shelf,
                            sizeId: newProduct.sizeId,
                            sizeName: newProduct.sizeName,
                            sizeNameArabic: newProduct.sizeNameArabic,
                            stockLevel: newProduct.stockLevel,
                            styleNumber: newProduct.styleNumber,
                            subCategoryId: newProduct.subCategoryId,
                            taxMethod: newProduct.taxMethod,
                            taxRate: newProduct.taxRate,
                            taxRateId: newProduct.taxRateId,
                            taxRateValue: newProduct.taxRateValue,
                            unit: newProduct.unit,
                            unitId: newProduct.unitId,

                            unitPerPack: newProduct.unitPerPack,
                            width: newProduct.width,
                            highUnitPrice: newProduct.highUnitPrice,
                            discountSign: '%',
                        });

                        prod = this.products.find((x) => x.rowId == uid);

                    }
                    else {
                        if (this.products.find(x => x.id == productId) == undefined || this.products.length <= 0) {
                            this.products.push(newProduct);
                        }
                        prod = this.products.find((x) => x.id == productId);
                    }

                    var rate = 0;
                    var taxRateId = '';
                    var taxMethod = '';
                    if (this.saleDefaultVat == 'DefaultVat' || this.saleDefaultVat == 'DefaultVatItem') {
                        if (prod.taxRateId != "00000000-0000-0000-0000-000000000000" && prod.taxRateId != undefined) {
                            rate = this.getVatValue(prod.taxRateId, prod);
                        }
                        taxRateId = prod.taxRateId;
                        taxMethod = prod.taxMethod;
                    }
                    if (this.saleDefaultVat == 'DefaultVatHead' || this.saleDefaultVat == 'DefaultVatHeadItem') {
                        if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {

                            rate = this.getVatValue(this.taxRateId, prod);
                        }

                        taxRateId = this.taxRateId;
                        taxMethod = this.taxMethod;
                    }

                    if (isTemplate) {
                        if (qty != null && qty != undefined && qty != 0) {
                            this.saleProducts.push({
                                rowId: uid,
                                productId: prod.id,
                                description: '',
                                unitPrice: price,
                                quantity: qty,
                                highQty: 0,
                                soQty: 0,
                                schemePhysicalQuantity: 0,
                                discount: 0,
                                fixDiscount: 0,
                                lineItemVAt: 0,
                                batchExpiry: prod.batchExpiry,
                                batchNo: prod.batchNo,
                                promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                                quantityOut: quantityOut == null ? null : quantityOut,
                                bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                                taxRateId: prod.taxRateId,
                                saleReturnDays: prod.saleReturnDays,
                                taxMethod: prod.taxMethod,
                                serialNo: '',
                                rate: rate,
                                serial: soItem.serial,
                                guaranteeDate: soItem.guaranteeDate,
                                isSerial: newProduct.serial,
                                guarantee: newProduct.guarantee,
                                inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                                currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                                lineTotal: prod.salePrice * 1,
                                buy: prod.bundleCategory != null ? prod.bundleCategory.buy : 0,
                                get: prod.bundleCategory != null ? prod.bundleCategory.get : 0,
                                quantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : 0,
                                offerQuantity: 0,
                                unitPerPack: newProduct.unitPerPack,
                                levelOneUnit: prod.levelOneUnit,
                                basicUnit: prod.basicUnit,
                                highUnitPrice: newProduct.highUnitPrice,
                            });

                        }
                    }
                    else if ((qty != null && qty != undefined && qty != 0) || (soItem != null && (soItem.highQty != null && soItem.highQty != undefined && soItem.highQty != 0))) {
                        this.saleProducts.push({
                            rowId: uid,
                            productId: prod.id,
                            description: '',
                            unitPrice: so ? price : '0',
                            quantity: qty,
                            highQty: soItem.highQty,
                            soQty: qty,
                            scheme: soItem.scheme,
                            schemeQuantity: soItem.schemeQuantity,
                            wholesalePrice: prod.wholesalePrice,
                            salePrice: prod.salePrice,
                            wholesaleQuantity: prod.wholesaleQuantity,
                            schemePhysicalQuantity: soItem.schemePhysicalQuantity,

                            discount: so ? (soItem.discount == undefined ? 0 : soItem.discount) : 0,
                            fixDiscount: so ? (soItem.fixDiscount == undefined ? 0 : soItem.fixDiscount) : 0,
                            lineItemVAt: 0,
                            batchExpiry: prod.batchExpiry,
                            batchNo: prod.batchNo,
                            promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                            quantityOut: quantityOut == null ? null : quantityOut,
                            bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                            taxRateId: soItem.taxRateId,
                            saleReturnDays: prod.saleReturnDays,
                            taxMethod: soItem.taxMethod,
                            serialNo: '',
                            rate: rate,
                            serial: soItem.serial,
                            guaranteeDate: soItem.guaranteeDate,
                            isSerial: newProduct.serial,
                            guarantee: newProduct.guarantee,
                            inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                            currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                            lineTotal: prod.salePrice * 1,
                            buy: prod.bundleCategory != null ? prod.bundleCategory.buy : 0,
                            get: prod.bundleCategory != null ? prod.bundleCategory.get : 0,
                            quantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : 0,
                            offerQuantity: 0,
                            unitPerPack: newProduct.unitPerPack,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.basicUnit,
                            highUnitPrice: newProduct.highUnitPrice,
                            discountSign: '%',
                        });
                    }
                    else {
                        this.saleProducts.push({
                            rowId: uid,
                            productId: prod.id,
                            description: prod.name,
                            unitPrice: '0',
                            quantity: this.isMultiUnit == 'true' ? 0 : 1,
                            soQty: 0,
                            highQty: 0,

                            discount: 0,
                            fixDiscount: 0,
                            discountSign: '%',

                            promotionId: null,
                            bundleId: null,
                            promotionName: '',
                            promotionType: null,
                            buy: 0,
                            get: 0,
                            offerQuantityLimit: 0,
                            stockLimit: 0,
                            quantityOut: 0,
                            upToQuantity: 0,
                            includingBaseQuantity: 0,
                            getProductId: '',
                            offerQuantity: 0,
                            promotionOfferQuantity: 0,

                            discountAmount: 0,
                            vatAmount: 0,
                            totalAmount: 0,
                            grossAmount: 0,

                            lineItemVAt: 0,
                            batchExpiry: '',
                            batchNo: '',
                            inventoryList: null,
                            currentQuantity: 0,
                            taxRateId: taxRateId,
                            taxMethod: taxMethod,
                            saleReturnDays: 0,
                            scheme: '',
                            schemeQuantity: '',
                            wholesalePrice: 0,
                            salePrice: 0,
                            wholesaleQuantity: 0,
                            schemePhysicalQuantity: 0,
                            rate: rate,
                            serialNo: '',
                            serial: '',
                            guaranteeDate: '',
                            isSerial: false,
                            guarantee: false,
                            lineTotal: 0,
                            unitPerPack: 0,
                            levelOneUnit: '',
                            basicUnit: '',
                            saleSizeAssortment: [],
                            productSizes: [],
                            colorId: '',
                            colorName: '',
                            isFree: false,
                            highUnitPrice: 0,
                        });
                        this.GetProductInfo(productId);
                    }
                }

                var product = this.saleProducts.find((x) => {
                    return x.productId == productId /*&& x.rowId == uid*/;
                });

                this.getVatValue(product.taxRateId, product);

                this.updateLineTotal(product.quantity, "quantity", product);
                this.updateLineTotal(product.highQty, "highQty", product);

                this.product.id = "";
                this.rendered++;
            },

            GetProductInfo: function (id) {
                var root = this;
                var token = localStorage.getItem('token');

                var openBatch;
                var batch = localStorage.getItem('openBatch')
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }

                root.$https.get('/Product/GetProductDetailForInvoiceQuery?id=' + id + '&wareHouseId=' + this.wareHouseId + "&isFifo=" + root.isFifo + "&openBatch=" + openBatch + "&colorVariants=" + root.colorVariants + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var newProduct = response.data;
                        var prod = root.saleProducts.find((x) => x.productId == id);

                        if (prod != undefined) {
                            var sizeAssortmentList = [];
                            if (root.colorVariants) {
                                response.data.saleSizeAssortment.forEach(function (item) {
                                    sizeAssortmentList.push({
                                        sizeId: item.sizeId,
                                        name: item.name,
                                        quantity: item.quantity,
                                        currentQuantity: 0,
                                    });
                                });
                            }


                            prod.unitPrice = newProduct.salePrice == 0 ? '0' : newProduct.salePrice;
                            prod.promotionId = newProduct.promotionOffer == null ? null : newProduct.promotionOffer.id;
                            prod.bundleId = newProduct.bundleCategory == null ? null : newProduct.bundleCategory.id;
                            prod.promotionType = newProduct.promotionOffer != null ? newProduct.promotionOffer.promotionType : null;
                            prod.buy = newProduct.bundleCategory != null ? newProduct.bundleCategory.buy : newProduct.promotionOffer != null ? newProduct.promotionOffer.buy : 0;
                            prod.get = newProduct.bundleCategory != null ? newProduct.bundleCategory.get : newProduct.promotionOffer != null ? newProduct.promotionOffer.get : 0;
                            prod.offerQuantityLimit = newProduct.bundleCategory != null ? newProduct.bundleCategory.quantityLimit : (newProduct.promotionOffer != null ? newProduct.promotionOffer.baseQuantity : 0);
                            prod.stockLimit = newProduct.bundleCategory != null ? newProduct.bundleCategory.stockLimit : (newProduct.promotionOffer != null ? newProduct.promotionOffer.stockLimit : 0);
                            prod.quantityOut = newProduct.bundleCategory != null ? newProduct.bundleCategory.quantityOut : (newProduct.promotionOffer != null ? newProduct.promotionOffer.quantityOut : 0);
                            prod.upToQuantity = newProduct.promotionOffer == null ? 0 : newProduct.promotionOffer.upToQuantity;
                            prod.includingBaseQuantity = newProduct.promotionOffer == null ? false : newProduct.promotionOffer.includingBaseQuantity;
                            prod.getProductId = newProduct.promotionOffer == null ? '' : newProduct.promotionOffer.getProductId;
                            prod.inventoryList = newProduct.inventoryBatch == null ? null : newProduct.inventoryBatch;
                            prod.currentQuantity = newProduct.inventory == null ? 0 : newProduct.inventory.currentQuantity;
                            prod.saleReturnDays = newProduct.saleReturnDays;
                            prod.scheme = newProduct.scheme;
                            prod.schemeQuantity = newProduct.schemeQuantity;
                            prod.wholesalePrice = newProduct.wholesalePrice;
                            prod.salePrice = newProduct.salePrice;
                            prod.wholesaleQuantity = newProduct.wholesaleQuantity;
                            prod.isSerial = newProduct.serial;
                            prod.guarantee = newProduct.guarantee;
                            prod.lineTotal = newProduct.salePrice * 1;
                            prod.unitPerPack = newProduct.unitPerPack;
                            prod.levelOneUnit = newProduct.levelOneUnit;
                            prod.basicUnit = newProduct.basicUnit;
                            prod.saleSizeAssortment = sizeAssortmentList;
                            prod.productSizes = newProduct.productSizes;
                            prod.highUnitPrice = newProduct.highUnitPrice;


                            root.updateLineTotal(prod.quantity, "quantity", prod);
                        }
                    }
                });
            },

            ReRanderProduct: function () {

                this.products = [];
                this.randerProductAfterNew++;
                // if (root.$refs.productDropdownRef != undefined) {
                //         this.$refs.productDropdownRef.ReRanderProduct();

                //     }

            },
            EmtySaleProductList: function (taxMethod) {


                this.saleProducts = [];
                this.paidVatList = [];

                this.note = '';
                this.newItem = {
                    description: '',
                    unitPrice: 0,
                    highQty: 0,
                    quantity: 0,
                    discount: 0,
                    fixDiscount: 0,
                    discountSign: '%',
                    styleNumber: ''
                };

                this.isAuthour = {
                    changePriceDuringSale: false,
                    giveDiscountDuringSale: false,
                    column: '',
                };
                this.authorize = {
                    column: '',
                    userName: '',
                    password: '',
                };
                this.product = {
                    id: "",
                };
                this.summary = {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVat: 0,
                    bundleAmount: 0,
                    totalCarton: 0,
                    totalPieces: 0
                };

                if (taxMethod != undefined && taxMethod != null && taxMethod != '') {
                    this.taxMethod = taxMethod;
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
            getVatValue: function (id, prod) {

                var vat = this.vats.find((value) => value.id == id);
                prod.taxRateId = id;
                prod.rate = vat.rate;
                if (prod.unitPrice != undefined) {
                    this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
                }
                return vat.rate;
            },
            getTaxMethod: function (method, prod) {
                prod.taxMethod = method;
                this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
            },
            removeProduct: function (id) {
                this.saleProducts = this.saleProducts.filter((prod) => {
                    return prod.rowId != id;
                });
                this.calcuateSummary();
            },

            getDate: function (x) {
                return moment(x).format("l");
            },
            quantityIncrease: function (productId) {

                if (this.saleProducts.some(x => x.productId == productId)) {
                    var prd = this.saleProducts.find(x => x.productId == productId);
                    if (prd.quantity < 0) {
                        prd.quantity--;
                    } else {
                        prd.quantity++;
                    }
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
            },
            onBarcodeScanned(barcode) {

                if (localStorage.getItem("BarcodeScan") != 'SaleItem')
                    return

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                //var warehouseId = ''
                //if (this.invoiceWoInventory == 'false')
                //    warehouseId = this.sale.wareHouseId
                this.counterForBarCodeRequest++;

                {
                    this.$https.get('/Product/GetProductDetailForInvoiceQuery?barCode=' + barcode, { headers: { "Authorization": `Bearer ${token}` } })

                        .then(function (response) {
                            if (response.data != null) {
                                var newProduct = response.data;
                                root.counterForBarCodeRequest = 0;
                                root.barCode = '';

                                if (root.saleProducts.length > 0) {
                                    var existingProduct = root.saleProducts.find(x => x.productId == newProduct.id)
                                    if (existingProduct != null) {
                                        root.quantityIncrease(newProduct.id)
                                    }
                                    else {
                                        root.addProduct(newProduct.id, newProduct)
                                    }
                                }
                                else {
                                    root.addProduct(newProduct.id, newProduct)
                                }
                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: "Item not found",
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    icon: 'error',
                                    timer: 1000,
                                    timerProgressBar: true,
                                });
                            }
                        });

                }

            },

            getData: function () {
                var root = this;

                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Product/TaxRateList", {
                        headers: {
                            Authorization: `Bearer ${token}`
                        },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        if (root.saleItems != undefined) {

                            root.saleItems.forEach(function (item) {
                                //if (item.quantity <= 0)
                                //    return
                                if (root.isFifo) {
                                    root.products.push({
                                        rowId: item.id,
                                        arabicName: item.arabicName,
                                        assortment: item.assortment,
                                        barCode: item.barCode,
                                        basicUnit: item.product.basicUnit,
                                        batchExpiry: item.expiryDate,
                                        batchNo: item.batchNo,
                                        brandId: item.brandId,
                                        bundleCategory: item.bundleCategory,
                                        category: item.category,
                                        categoryId: item.categoryId,
                                        code: item.code,
                                        colorId: item.colorId,
                                        colorName: item.colorName,
                                        serialNo: item.serialNo,
                                        displayName: item.displayName,
                                        scheme: item.scheme,
                                        schemeQuantity: item.schemeQuantity,
                                        schemePhysicalQuantity: item.schemePhysicalQuantity,

                                        colorNameArabic: item.colorNameArabic,
                                        currentQuantity: item.currentQuantity,
                                        description: item.description,
                                        englishName: item.productName,
                                        guarantee: item.product.guarantee,
                                        id: item.productId,
                                        image: item.image,
                                        inventory: item.product.inventory,
                                        inventoryBatch: item.product.inventoryBatch,
                                        isActive: item.isActive,
                                        isExpire: item.isExpire,
                                        isRaw: item.isRaw,

                                        length: item.length,
                                        levelOneUnit: item.product.levelOneUnit,
                                        originId: item.originId,
                                        promotionOffer: item.promotionOffer,
                                        purchasePrice: item.purchasePrice,
                                        salePrice: item.salePrice,
                                        salePriceUnit: item.salePriceUnit,
                                        saleReturnDays: item.saleReturnDays,
                                        serial: item.product.serial,
                                        serviceItem: item.serviceItem,

                                        shelf: item.shelf,
                                        sizeId: item.sizeId,
                                        sizeName: item.sizeName,
                                        sizeNameArabic: item.sizeNameArabic,
                                        stockLevel: item.stockLevel,
                                        styleNumber: item.styleNumber,
                                        subCategoryId: item.subCategoryId,
                                        taxMethod: item.taxMethod,
                                        taxRate: item.taxRate,
                                        taxRateId: item.taxRateId,
                                        taxRateValue: item.taxRateValue,
                                        unit: item.unit,
                                        unitId: item.unitId,

                                        unitPerPack: item.unitPerPack,
                                        width: item.width,
                                        saleSizeAssortment: item.saleSizeAssortment,
                                        highUnitPrice: item.product.highUnitPrice,
                                        discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
                                    });

                                } else {
                                    root.products.push(item.product);
                                }

                                var vat = root.vats.find((value) => value.id == item.taxRateId);

                                root.saleProducts.push({
                                    rowId: item.id,
                                    productId: item.productId,
                                    description: item.description,
                                    unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                    salePrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                    wholesalePrice: item.product.wholesalePrice == undefined ? 0 : item.product.wholesalePrice,
                                    wholesaleQuantity: item.product.wholesaleQuantity,
                                    quantity: item.quantity,
                                    scheme: item.scheme,
                                    schemeQuantity: item.schemeQuantity,
                                    schemePhysicalQuantity: item.schemePhysicalQuantity,
                                    highQty: item.highQty,
                                    discount: item.discount,
                                    offerQuantity: item.offerQuantity == undefined ? 0 : item.offerQuantity,
                                    fixDiscount: item.fixDiscount,
                                    taxRateId: item.taxRateId,
                                    taxMethod: item.taxMethod,
                                    serialNo: item.serialNo,
                                    rate: vat.rate,
                                    soQty: item.soQty,
                                    batchExpiry: item.batchExpiry,
                                    batchNo: item.batchNo,
                                    inventoryList: item.product.inventoryBatch == null ? null : item.product.inventoryBatch,
                                    currentQuantity: item.product.inventory == null ? 0 : item.product.inventory.currentQuantity,
                                    saleReturnDays: item.saleReturnDays,
                                    lineTotal: item.unitPrice * item.quantity,
                                    unitPerPack: item.unitPerPack,
                                    levelOneUnit: item.product.levelOneUnit,
                                    basicUnit: item.product.basicUnit,
                                    serial: item.serial,
                                    guaranteeDate: item.guaranteeDate,
                                    isSerial: item.product.serial,
                                    guarantee: item.product.guarantee,
                                    saleSizeAssortment: item.saleSizeAssortment,
                                    saleSizeAssortmentInventory: item.saleSizeAssortmentInventory,
                                    productSizes: item.product.productSizes,
                                    colorId: item.colorId,
                                    colorName: item.colorName,
                                    highUnitPrice: item.product.highUnitPrice,
                                    discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
                                    promotionId: item.promotionId,
                                    bundleId: item.bundleId,

                                    buy: item.buy,
                                    get: item.get,
                                    offerQuantityLimit: item.product.bundleCategory != null ? item.product.bundleCategory.quantityLimit : (item.product.promotionOffer != null ? item.product.promotionOffer.baseQuantity : 0),
                                    stockLimit: item.product.bundleCategory != null ? item.product.bundleCategory.stockLimit : (item.product.promotionOffer != null ? item.product.promotionOffer.stockLimit : 0),
                                    quantityOut: item.product.bundleCategory != null ? item.product.bundleCategory.quantityOut : (item.product.promotionOffer != null ? item.product.promotionOffer.quantityOut : 0),
                                    upToQuantity: item.product.promotionOffer == null ? 0 : item.product.promotionOffer.upToQuantity,
                                    includingBaseQuantity: item.product.promotionOffer == null ? false : item.product.promotionOffer.includingBaseQuantity,
                                    promotionOfferQuantity: item.promotionOfferQuantity,
                                    promotionType: item.product.promotionOffer != null ? item.product.promotionOffer.promotionType : null,
                                    getProductId: item.product.promotionOffer == null ? '' : item.product.promotionOffer.getProductId,
                                });

                                var product = root.saleProducts.find((x) => {
                                    return x.productId == item.productId && x.rowId == item.id;
                                });

                                if (root.colorVariants) {
                                    root.UpdateColorName(item.colorId, product);
                                }

                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;
                                root.getVatValue(product.taxRateId, product);
                                root.updateLineTotal(item.quantity, "quantity", product);
                                root.updateLineTotal(item.highQty, "highQty", product);
                                root.updateLineTotal(item.scheme, "scheme", product);
                                root.updateLineTotal(item.schemeQuantity, "schemeQuantity", product);

                                root.product.id = "";
                                root.rendered++;
                            });
                            root.$emit("details", root.saleProducts);
                        }
                    });
            },

            UpdateColorName: function (colorId, product) {
                product.saleSizeAssortment.forEach(function (item) {
                    var size = product.saleSizeAssortmentInventory.variationInventories.find(x => x.sizeId == item.sizeId);
                    if (size != undefined) {
                        item.currentQuantity = size.quantity;
                    } else {
                        item.currentQuantity = 0;
                    }
                });

                if (colorId != null && colorId != undefined && colorId != '') {
                    product.currentQuantity = product.saleSizeAssortmentInventory.quantity;
                }
            },

            GetSizeData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/SizeList?isActive=true' + '&isVariance=true', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.saleSizeAssortment = [];
                        response.data.results.sizes.forEach(function (item) {
                            root.saleSizeAssortment.push({
                                sizeId: item.id,
                                name: item.name,
                                quantity: 0,
                            });
                        });
                    }
                });
            },
        },
        created: function () {

            this.transactionLevelDiscount = this.transactionLevelDiscountProp;
            this.isDiscountBeforeVat = this.isbeforetax == true ? true : false;
            this.isFixedDiscount = this.isFixed == true ? true : false;

            if (this.$i18n.locale == 'en') {
                this.options = ['Inclusive', 'Exclusive'];
            } else {
                this.options = ['شامل', 'غير شامل'];
            }

            this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory') == 'true' ? true : false;
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');



            localStorage.setItem("BarcodeScan", 'SaleItem')

            this.getData();
            var root = this;
            if (this.$route.query.mobiledata != undefined) {
                for (var j = 0; j < this.$route.query.mobiledata.mobileOrderItemLookupModels.length; j++) {
                    this.saleProducts.quantity[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].quantity[j];
                }
                root.calcuateSummary();
                this.saleProducts.rowId = this.$route.query.mobiledata.mobileOrderItemLookupModels.rowId;
                this.saleProducts.quantity = this.$route.query.mobiledata.mobileOrderItemLookupModels.quantity;
            }

            if (localStorage.getItem('ColorVariants') == 'true') {
                this.GetSizeData();
            }
            this.note = this.noteVal;

        },

        mounted: function () {
            this.note = this.noteVal;
            this.isDiscountBeforeVat = this.isbeforetax == true ? true : false;
            this.isFixedDiscount = this.isFixed == true ? true : false;
            this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.soInventoryReserve = localStorage.getItem('SoInventoryReserve') == 'true' ? true : false;
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.currency = localStorage.getItem('currency');
            this.dayStart = localStorage.getItem('DayStart');

            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;

            this.changePriceDuringSale = localStorage.getItem('changePriceDuringSale');
            this.changePriceDuringSale == 'true' ? (this.changePriceDuringSale = true) : (this.changePriceDuringSale = false);
            this.giveDiscountDuringSale = localStorage.getItem('giveDicountDuringSale');
            this.giveDiscountDuringSale == 'true' ? (this.giveDiscountDuringSale = true) : (this.giveDiscountDuringSale = false);
        },

    };
</script>

<style scoped>
    /* Chrome, Safari, Edge, Opera */
    input::-webkit-outer-spin-button,
    input::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
    }

    /* Firefox */
    input[type=number] {
        -moz-appearance: textfield;
    }
</style>
