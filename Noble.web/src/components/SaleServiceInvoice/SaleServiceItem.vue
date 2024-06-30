<template>
    <div class="col-lg-12">
        <div class="table-responsive mt-3">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th class="text-center" style="width: 100px;">
                            {{ $t('SaleItem.ModelStyle') }}
                        </th>
                        <th class="text-center" style="width: 250px;">
                            {{ $t('SaleItem.ProductDescription') }}
                        </th>
                        <!-- <th class="text-center" style="width: 280px;">
                            {{ $t('SaleItem.Description') }}
                        </th> -->
                        <th class="text-center"  style="width: 100px;" >
                            {{ $t('SaleItem.Unit') }}   
                        </th>
                        <th class="text-center" style="width: 100px;" v-if="!invoiceWoInventory && formName != 'Quotation'">
                            {{ $t('SaleItem.CurrentQuantity') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleItem.HighQty') }}
                        </th>
                        <th class="text-center" style="width: 100px;">
                            {{ $t('SaleItem.Quantity') }}
                        </th>
                        <th class="text-center" style="width: 70px;" hidden>
                            {{ $t('SaleItem.UnitPerPack') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleItem.TOTALQTY') }}
                        </th>
                        <th class="text-center" style="width: 100px;">
                            {{ $t('SaleItem.UnitPrice') }}
                        </th>
                       

                        <th class="text-center" style="width: 100px;" hidden>
                            {{ $t('SaleItem.ReturnDays') }}
                        </th>
                        <th style="width: 100px;" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                            {{ $t('SaleItem.Bundle') }}
                        </th>
                        <th style="width: 100px;" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                            {{ $t('SaleItem.Bundle') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="isSerial">
                            {{ $t('SaleItem.Serial') }}
                        </th>
                       
                        <th style="width: 100px;" v-if="isSerial">
                            {{ $t('SaleItem.Guarantee') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="!isDiscountOnTransaction">
                            {{ $t('SaleItem.DISC%') }}
                        </th>

                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 130px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th style="width: 60px;" class="text-center">
                            {{ $t('SaleItem.Free') }}
                        </th>
                        <th class="text-end" style="width: 100px;">
                            {{ $t('SaleItem.LineTotal') }}
                        </th>
                        <th style="width: 40px;"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(prod, index) in saleProducts" :key="rendered + index">
                        <td>{{index+1}}</td>

                        <td class="text-center">
                            <input type="text" v-model="prod.styleNumber"
                                   @focus="$event.target.select()"
                                   class="form-control text-center" />

                        </td>

                        <td>
                            <textarea data-gramm="false" rows="2" class="form-control" v-model="prod.description" />
                        </td>
                        <td>
                            <input type="text" v-model="prod.unitName"
                                   @focus="$event.target.select()"
                                   class="form-control text-center" />
                        </td>


                        <td class="text-center" v-if="!invoiceWoInventory && formName != 'Quotation'">
                            {{prod.currentQuantity}}
                        </td>

                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            <decimal-to-fixed v-model="prod.highQty"
                                              v-bind:salePriceCheck="false"
                                              :isQunatity="true"
                                              :disable="isEditPaidInvoice"
                                              @update:modelValue="updateLineTotal(prod.highQty, 'highQty', prod)" />
                            <small style="font-weight: 500;font-size:70%;">
                                {{prod.levelOneUnit}}
                            </small>
                        </td>
                        <td class="text-center">
                            <decimal-to-fixed v-model="prod.quantity"
                                              v-bind:salePriceCheck="false"
                                              :isQunatity="true"
                                              :disable="isEditPaidInvoice"
                                              @update:modelValue="updateLineTotal(prod.quantity, 'quantity', prod)" />
                            <small style="font-weight: 500;font-size:70%;" v-if="isMultiUnit=='true'">
                                {{prod.basicUnit}}
                            </small>
                        </td>
                        <td class="text-center" hidden>
                            <decimal-to-fixed v-model="prod.unitPerPack" :disable="isEditPaidInvoice" />


                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            {{prod.totalPiece}}
                        </td>
                        <td v-if="prod.saleReturnDays > 0" hidden>
                            <decimal-to-fixed v-model="prod.saleReturnDays" :disable="isEditPaidInvoice" />
                        </td>
                        <td class="text-center" v-else hidden>
                            <span>--</span>
                        </td>
                        <td v-on:dblclick="counter += 1, openmodel('unitPrice'+index)" v-if="!changePriceDuringSale && dayStart=='true'">
                            <decimal-to-fixed disable="((isAuthour.changePriceDuringSale && isAuthour.column==('unitPrice'+index))==true?'true':'false') || isEditPaidInvoice" v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                        </td>
                        <td v-else>
                            <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" :disable="isEditPaidInvoice" />
                        </td>
                       
                        <td class="text-center" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                            <span class="badge badge-pill badge-info">{{prod.bundleOffer}}</span>
                        </td>

                        <td class="text-center" v-if="isSerial">
                            <button @click="AddSerial(prod)" v-if="prod.isSerial" v-bind:disabled="isEditPaidInvoice" title="Add Serial" class="btn btn-primary btn-sm"> Add Serial </button>
                            <span v-else>-</span>
                        </td>
                        <td class="border-top-0  text-center" v-if="isSerial">
                            <datepicker v-model="prod.guaranteeDate" v-if="prod.guarantee" />
                            <span v-else>-</span>
                        </td>


                        <td v-if="!isDiscountOnTransaction">
                            <div v-if="prod.discountSign == '%'">
                                <div class="input-group" v-if="(!giveDiscountDuringSale && dayStart==='true') " v-on:dblclick="counter += 1, openmodel1('discount'+index)">
                                    <decimal-to-fixed v-model="prod.c" v-bind:salePriceCheck="false" :disable="isEditPaidInvoice" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                                <div class="input-group" v-else>
                                    <decimal-to-fixed v-model="prod.discount" :disable="isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                            </div>
                            <div v-else-if="prod.discountSign == 'F'">
                                <div class="input-group" v-if="(!giveDiscountDuringSale && dayStart==='true')" v-on:dblclick="counter += 1, openmodel2('fixDiscount'+index)">
                                    <decimal-to-fixed v-model="prod.fixDiscount" :disable="isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                                <div class="input-group" v-else>
                                    <decimal-to-fixed v-model="prod.fixDiscount" :disable="isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                            </div>
                        </td>

                        <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                            <taxratedropdown v-model="prod.taxRateId" :isDisable="formName === 'saleReturn'?true:false" @update:modelValue="getVatValue(prod.taxRateId, prod)" v-if="isEditPaidInvoice" disabled />
                            <taxratedropdown v-model="prod.taxRateId" :isDisable="formName === 'saleReturn'?true:false" @update:modelValue="getVatValue(prod.taxRateId, prod)" v-else />
                        </td>

                        <td class="text-center">
                            <div class="checkbox form-check-inline">
                                <input type="checkbox" :id=" index + 'inlineCheckbox1'" v-bind:disabled="(formName === 'saleReturn'?true:false) || isEditPaidInvoice" v-model="prod.isFree" v-on:change="OnChangeDiscountType(prod)">
                                <label :for=" index + 'inlineCheckbox1'"> </label>
                            </div>
                        </td>
                        <td class="text-end">
                            {{currency}} {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                        </td>
                        <td class="text-end">
                            <a href="javascript:void(0);" v-if="isEditPaidInvoice"><i class="las la-trash-alt text-dark font-16"></i></a>
                            <a href="javascript:void(0);" v-else @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td class="text-center">
                            <input type="text" v-model="newItem.styleNumber"
                                   @focus="$event.target.select()"
                                   class="form-control text-center" />
                        </td>
                        <td>
                            <textarea data-gramm="false" rows="2" v-model="newItem.description" class="form-control" />
                        </td>
                        <td>
                            <input type="text" v-model="newItem.unitName "
                                   @focus="$event.target.select()"
                                   class="form-control text-center" />
                        </td>



                        <td class="text-center" v-if="!invoiceWoInventory && formName != 'Quotation'">
                        </td>

                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            <decimal-to-fixed v-model="newItem.highQty" :disable="isEditPaidInvoice" />
                        </td>

                        <td class="text-center">
                            <decimal-to-fixed v-model="newItem.quantity" :disable="isEditPaidInvoice" />
                        </td>
                        <td class="text-center" hidden>
                            <decimal-to-fixed v-model="newItem.unitPerPack" :disable="isEditPaidInvoice" />
                        </td>
                      
                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                        </td>
                        <td>
                            <decimal-to-fixed v-model="newItem.unitPrice" :disable="isEditPaidInvoice" />
                        </td>
                        <td hidden>
                            <input type="number"
                                   class="form-control text-center " v-bind:disabled="isEditPaidInvoice" />
                        </td>

                        <td class="text-center" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                        </td>

                        <td class="text-center" v-if="isSerial">
                        </td>

                        <td class="border-top-0  text-center" v-if="isSerial">
                        </td>

                        <td v-if="!isDiscountOnTransaction" class="text-center">
                            <div class="input-group" v-if="newItem.discountSign == '%'">
                                <decimal-to-fixed v-model="newItem.discount" v-bind:salePriceCheck="false" :disable="isEditPaidInvoice" @update:modelValue="updateLineTotal(newItem.discount, 'discount', newItem)" />
                                <button v-on:click="OnChangeDiscountType(newItem)" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{newItem.discountSign}}</button>
                            </div>

                            <div class="input-group" v-else-if="newItem.discountSign == 'F'">
                                <decimal-to-fixed v-model="newItem.fixDiscount" :disable="isEditPaidInvoice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(newItem.fixDiscount, 'fixDiscount', newItem)" />
                                <button v-on:click="OnChangeDiscountType(newItem)" v-bind:disabled="isEditPaidInvoice" class="btn btn-primary" type="button" id="button-addon2">{{newItem.discountSign}}</button>
                            </div>
                        </td>


                        <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" class="text-right">
                        </td>

                        <td class="text-center">
                        </td>

                        <td class="text-end">
                        </td>

                        <td class="text-end">
                            <button @click="newItemProduct()"
                                    title="Add Item" v-bind:disabled="newItem.description==''"
                                    class="btn btn-primary btn-sm btn-round btn-icon float-right">
                                <i class="fa fa-check"></i>
                            </button>
                            <!-- <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a> -->
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>


        <div class="row  ">
            <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5" v-if="formName === 'saleReturn'">

            </div>
            <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5" v-else>
                <div class="mt-4" v-if="isValid('CanAddItem') || isValid('CanViewItem') ">
                    <product-dropdown :raw="false"
                                      @update:modelValues="addProduct"
                                      v-bind:key="randerProductAfterNew"
                                      ref="productDropdownRef"
                                      :isservice="true"
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
                    <span class="fw-bold">No of Items</span><br />
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
                            <tr v-if="isDiscountOnTransaction && !isDiscountBeforeVat   && transactionLevelDiscount>0">
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
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        name: "SaleServiceItem",
        props: ['saleItems', 'wareHouseId', 'saleOrderId', 'newtaxMethod', 'taxRateId', 'formName', 'adjustmentProp', 'adjustmentSignProp', 'isDiscountOnTransaction', 'transactionLevelDiscountProp', 'isFixed', 'isbeforetax', 'noteVal', 'isEditPaidInvoice'],
        mixins: [clickMixin],
        data: function () {
            return {
                taxMethod: this.newtaxMethod,
                note: '',
                randerProductAfterNew: 0,
                transactionLevelDiscount: 0,
                adjustment: 0,
                adjustmentSign: '+',
                isDiscountBeforeVat: false,
                isFixedDiscount: false,
                transactionLevelTotalDiscount: 0,
                newItem: {
                    description: '',
                    unitPrice: 0,
                    highQty: 0,
                    quantity: 0,
                    discount: 0,
                    fixDiscount: 0,
                    discountSign: '%',
                    styleNumber: '',
                    unitName: '',
                    unitPerPack: 0,
                },

                paidVatList: [],
                dayStart: '',
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
                options: [],
                serialItem: '',
                showSerial: false,
                saleDefaultVat: '',
            };
        },
        validations() { },
        filter: {},
        methods: {
            ReRanderProduct: function () {

                this.products = [];
                this.randerProductAfterNew++;
                // if (root.$refs.productDropdownRef != undefined) {
                //         this.$refs.productDropdownRef.ReRanderProduct();

                //     }

            },
            UpdateDiscountField: function (prop) {
                if (prop === 'fixed')
                    this.isFixedDiscount = this.isFixedDiscount ? false : true;
                if (prop === 'beforeTax') {
                    this.isDiscountBeforeVat = this.isDiscountBeforeVat ? false : true;


                }

                this.$emit("discountChanging", this.isFixedDiscount, this.isDiscountBeforeVat);

                this.calcuateSummary();
            },
            OnChangeOveallDiscount: function () {
                this.adjustmentSign = this.adjustmentSign == '+' ? '-' : '+'
                this.calcuateSummary()
            },
            NewItemChangeDiscount: function (prod) {
                if (prod.discountSign === '%') {
                    prod.discountSign = 'F';
                    prod.fixDiscount = 0
                    prod.discount = 0
                }
                else {
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
                }
                else {
                    prod.discountSign = '%';
                    prod.discount = 0
                    prod.fixDiscount = 0
                    this.updateLineTotal(prod.discount, 'discount', prod)
                }
            },

            NoteSave: function () {

                this.$emit("NoteSave", this.note);

            },
            newItemProduct: function () {
                var taxRateId = this.taxRateId;
                var taxMethod = this.taxMethod;

                var vat = this.vats.find((value) => value.id == taxRateId);

                var rowId = this.createUUID();
                this.saleProducts.push({
                    rowId: rowId,
                    productId: null,
                    unitPrice: this.newItem.unitPrice,
                    quantity: this.newItem.quantity,
                    soQty: 0,
                    schemePhysicalQuantity: 0,
                    highQty: 0,
                    discount: this.newItem.discount,
                    fixDiscount: this.newItem.fixDiscount,
                    lineItemVAt: 0,
                    description: this.newItem.description,
                    styleNumber: this.newItem.styleNumber,
                    unitName: this.newItem.unitName,
                    serviceItem: true,
                    currentQuantity: 0,
                    taxRateId: taxRateId,
                    saleReturnDays: 0,
                    taxMethod: taxMethod,
                    rate: vat.rate,
                    serial: '',
                    guaranteeDate: '',
                    isSerial: false,
                    guarantee: false,
                    lineTotal: 0,
                    unitPerPack: this.newItem.unitPerPack,
                    levelOneUnit: '',
                    isFree: false,
                    discountSign: this.newItem.discountSign
                });

                this.newItem.description = '';
                this.newItem.styleNumber = '';
                this.newItem.unitPrice = 0;
                this.newItem.highQty = 0;
                this.newItem.quantity = 0;
                this.newItem.discount = 0;
                this.newItem.fixDiscount = 0;
                this.newItem.unitPerPack = 0;
                this.newItem.unitName = '';

                var product = this.saleProducts.find((x) => {
                    return x.rowId == rowId;
                });

                this.updateLineTotal(product.quantity, "quantity", product);
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

            GetProductList: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                this.isRaw = this.raw == undefined ? false : this.raw;
                //search = search == undefined ? '' : search;
                // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;

                this.$https
                    .get("/Product/GetProductBarcode?isRaw=" + root.isRaw + '&wareHouseId=' + this.wareHouseId, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.productList = response.data.results.products;

                        }
                    });


            },
            ClearList: function () {
                this.saleProducts = [];
                this.products = [];

            },
            onBarcodeScanned(barcode) {

                if (localStorage.getItem("BarcodeScan") != 'SaleItem')
                    return
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var warehouseId = ''
                if (!this.invoiceWoInventory) {


                    warehouseId = root.wareHouseId
                }

                this.$https.get('/Product/GetProductInformation?searchTerm=' + barcode + '&wareHouseId=' + warehouseId + "&isDropdown=true" + '&isDropdown=' + false, { headers: { "Authorization": `Bearer ${token}` } })

                    .then(function (response) {
                        if (response.data != null) {
                            var product1 = [];
                            response.data.results.products.forEach(function (product) {
                                product1.push({
                                    id: product.id,
                                    englishName: (product.displayName == null || product.displayName == '') ? product.code + " " + product.englishName : product.displayName,
                                    arabicName: (product.displayName == null || product.displayName == '') ? product.code + " " + product.arabicName : product.displayName,
                                    assortment: product.assortment,
                                    barCode: product.barCode,
                                    basicUnit: product.basicUnit,
                                    brandId: product.brandId,
                                    categoryId: product.categoryId,
                                    code: product.code,
                                    colorId: product.colorId,
                                    colorName: product.colorName,
                                    colorNameArabic: product.colorNameArabic,
                                    description: (product.displayName == null || product.displayName == '') ? product.code + " " + product.englishName : product.displayName,
                                    inventory: product.inventory,
                                    isActive: product.isActive,
                                    image: product.image,
                                    isExpire: product.isExpire,
                                    isRaw: product.isRaw,
                                    length: product.length,
                                    levelOneUnit: product.levelOneUnit,
                                    stockLevel: product.stockLevel,
                                    originId: product.originId,
                                    promotionOffer: product.promotionOffer,
                                    purchasePrice: product.purchasePrice,
                                    salePrice: product.salePrice,
                                    salePriceUnit: product.salePriceUnit,
                                    saleReturnDays: product.saleReturnDays,
                                    shelf: product.shelf,
                                    sizeId: product.sizeId,
                                    sizeName: product.sizeName,
                                    sizeNameArabic: product.sizeNameArabic,
                                    styleNumber: product.styleNumber,
                                    unitName: '',
                                    subCategoryId: product.subCategoryId,
                                    taxMethod: product.taxMethod,
                                    taxRate: product.taxRate,
                                    taxRateId: product.taxRateId,
                                    unit: product.unit,
                                    unitId: product.unitId,
                                    unitPerPack: product.unitPerPack,
                                    width: product.width,

                                })
                            })

                            root.addProduct(product1[0].id, product1[0]);


                        }
                    });



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
            CheckRecordInProduct: function () {

                return this.$refs.productDropdownRef.productListCheck();
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
                }
                else {
                    this.summary.totalPieces = this.saleProducts.reduce((totalQty, prod) => totalQty + parseInt(prod.quantity), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.totalCarton = this.saleProducts.reduce((totalCarton, prod) => totalCarton + parseFloat(prod.highQty), 0);
                }
                else {
                    this.summary.totalCarton = this.saleProducts.reduce((totalCarton, prod) => totalCarton + parseInt(prod.highQty), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }
                else {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }

                this.summary.total = this.saleProducts.reduce((total, prod) => total + (prod.isFree ? 0 : (prod.lineTotal)), 0).toFixed(2);
                console.log(this.saleProducts, this.summary);

                if (!this.isDiscountOnTransaction) {
                    this.transactionLevelDiscount = 0;
                }
                var vatRate = 0;
                var discountOnly = 0;
                var discountForInclusiveVat = 0;
                var root = this;
                const taxIdList = [...new Set(this.saleProducts.map(item => item.taxRateId))];
                root.paidVatList = []
                //'isDiscountOnTransaction', 'transactionLevelDiscount'
                taxIdList.forEach(function (taxId) {
                    vatRate = root.vats.find((value) => value.id == taxId);
                    var filteredRecord = root.saleProducts
                        .filter((x) => x.taxRateId === taxId);
                    var totalQtyWithotFree = root.saleProducts.reduce((qty, prod) => qty + (prod.isFree ? 0 : parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece)), 0);
                    //var discForSelectedDis = filteredRecord
                    //    .reduce((discount, prod) =>
                    //        discount + (prod.isFree ? 0 : (prod.totalPiece ? (prod.offerQuantity ? 0 : (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100)) : 0)), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.discount != 0 || x.discount != "" || x.offerQuantity != 0)
                        .reduce((discount, prod) =>
                            discount + (prod.isFree ? 0 : (prod.totalPiece ? (prod.offerQuantity ? 0 : (((prod.totalPiece * prod.unitPrice) * prod.discount) / 100)) : 0)), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "" || x.offerQuantity != 0)
                        .reduce((discount, prod) =>
                            discount + (prod.isFree ? 0 : (prod.totalPiece ? (prod.offerQuantity ? 0 : (root.taxMethod == ("Inclusive" || "شامل") ? prod.fixDiscount + (prod.fixDiscount * vatRate.rate / 100) : prod.fixDiscount)) : 0)), 0);

                    var paidVat = filteredRecord
                        .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((prod.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.lineTotal) - (root.isDiscountBeforeVat ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : 0)) * vatRate.rate) / (100 + vatRate.rate) : ((parseFloat(prod.lineTotal) - (root.isDiscountBeforeVat && !root.isFixedDiscount && root.isDiscountOnTransaction ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : (root.isDiscountBeforeVat && root.isFixedDiscount && root.isDiscountOnTransaction ? (root.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.totalPiece) : 0))) * vatRate.rate) / 100))), 0).toFixed(3).slice(0, -1)
                    discountForInclusiveVat += parseFloat(filteredRecord
                        .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((prod.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.lineTotal) * vatRate.rate) / (100 + vatRate.rate) : 0))), 0).toFixed(3).slice(0, -1))

                    root.paidVatList.push({
                        name: vatRate.name,
                        amount: paidVat
                    })

                });
                //root.transactionLevelDiscount = root.transactionLevelDiscount;
                // this.summary.discount = discountOnly
                /*this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(3).slice(0, -1);*/
                this.summary.withDisc = this.summary.total;

                this.summary.vat = this.paidVatList.reduce((vat, paidVat) => (vat + parseFloat(paidVat.amount)), 0).toFixed(3).slice(0, -1);

                var exclusiveVat = parseFloat(this.summary.vat);
                this.transactionLevelTotalDiscount = ((this.isDiscountBeforeVat && this.isDiscountOnTransaction) ? (this.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc - discountForInclusiveVat) / 100) : (this.isFixedDiscount ? parseFloat(this.transactionLevelDiscount) : parseFloat(this.transactionLevelDiscount) * this.summary.withDisc / 100)) : (this.isFixedDiscount ? parseFloat(this.transactionLevelDiscount) : (parseFloat(this.summary.withDisc) + parseFloat(exclusiveVat)) * parseFloat(this.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                var totalIncDisc = (this.isDiscountBeforeVat && this.isDiscountOnTransaction && this.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc) / 100) : parseFloat(this.transactionLevelTotalDiscount)
                this.adjustment = (this.adjustment == '' || this.adjustment == null) ? 0 : parseFloat(this.adjustment)

                this.summary.withVat = (parseFloat(this.summary.withDisc) + (this.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(exclusiveVat)) + (this.adjustmentSign == '+' ? this.adjustment : (-1) * this.adjustment)).toFixed(3).slice(0, -1);

                this.summary.withVat = (parseFloat(this.summary.withVat) - totalIncDisc).toFixed(3).slice(0, -1);

                console.log(this.summary, this.taxMethod);


                if (this.isDiscountOnTransaction) {
                    this.summary.discount = totalIncDisc;
                    //    alert(this.summary.discount);
                }
                else {
                    this.summary.discount = discountOnly
                }
                this.summary.totalAfterDiscount = this.isDiscountOnTransaction && !this.isDiscountBeforeVat ? parseFloat(this.summary.withVat - this.summary.discount).toFixed(3).slice(0, -1) :
                    parseFloat((this.summary.total - this.summary.discount).toFixed(3).slice(0, -1));


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
                //} else {
                //    this.summary.bundleAmount = 0;
                //}

                if (this.isDiscountOnTransaction) {
                    this.summary.discount = totalIncDisc;
                }
                else {
                    this.summary.discount = discountOnly;
                }
                this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

                this.$emit("summary", this.summary);
            },

            updateLineTotal: function (e, prop, product) {

                if (e != undefined) {
                    var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == ""
                        ? 0
                        : product.fixDiscount
                        : product.discount;

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


                    //End Calculate offer
                    if (prop == "discount") {
                        if (e == "" || e < 0) {
                            e = 0;
                        }
                        else if (e > 100) {
                            e = 100;
                        }
                        product.discount = e;
                    }

                    if (prop == "fixDiscount") {
                        if (e == "" || e < 0) {
                            e = 0;
                        }
                        else if (e > product.unitPrice) {
                            e = product.unitPrice;
                        }
                        product.fixDiscount = e;
                    }

                    var vat = 0;
                    var total = 0;
                    var calculateVAt = 0;
                    //here we will select quantity after deduct bundle quantity


                    //isDiscountBeforeVat
                    vat = this.vats.find((value) => value.id == product.taxRateId);
                    total = product.totalPiece * product.unitPrice;

                    discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                    if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                        calculateVAt = product.discountSign == 'F' ? (((total * 100) / (100 + vat.rate)) - discount) * vat.rate / 100 : (total - discount) * vat.rate / (vat.rate + 100);

                    }
                    else {
                        calculateVAt = ((total - discount) * vat.rate) / 100;
                    }
                    product.lineTotal = product.totalPiece * product.unitPrice - discount;
                    if (product.isFree) {
                        product.lineTotal = 0

                    }

                    product.discountAmount = product.isFree ? 0 : discount;
                    product.vatAmount = product.isFree ? 0 : calculateVAt;
                    product.totalAmount = product.isFree ? 0 : (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? product.lineTotal : product.lineTotal + product.vatAmount;
                    product.grossAmount = product.isFree ? 0 : (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? total * 100 / (100 + vat.rate) : total;

                    this.saleProducts['product'] = product
                    this.calcuateSummary();
                    this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

                }
            },

            addProduct: function (productId, newProduct, qty, price, so, soItem, isTemplate) {
                debugger; //eslint-disable-line

                var priceRecord = this.$store.ispriceRecordList;
                if (priceRecord !== null && priceRecord.length > 0) {
                    var priceRecordObj = priceRecord.find(x => x.productId == newProduct.id);
                    if (priceRecordObj != null) {

                        newProduct.salePrice = priceRecordObj.newPrice;

                    }
                }
                var taxRateId1 = '';
                var taxMethod1 = '';
                if (isTemplate == undefined) {
                    isTemplate = false;
                }
                if (isTemplate) {




                    if (this.saleDefaultVat == 'DefaultVatHead' || this.saleDefaultVat == 'DefaultVatHeadItem') {
                        taxRateId1 = this.taxRateId;
                        taxMethod1 = this.taxMethod;
                    }
                    else {
                        taxRateId1 = localStorage.getItem('TaxRateId');
                        taxMethod1 = localStorage.getItem('taxMethod');
                    }


                    if (productId != null || productId != undefined) {
                        newProduct = this.$refs.productDropdownRef.productListValueCompare(productId);
                    }
                }
                if (productId == null) {
                    if (soItem.taxRateId == null || soItem.taxRateId == undefined || soItem.taxRateId == '') {
                        if (taxRateId1 == null || taxRateId1 == undefined || taxRateId1 == '') {
                            taxRateId1 = localStorage.getItem('TaxRateId');

                        }

                        soItem.taxRateId = taxRateId1;
                    }
                    var vat = this.vats.find((value) => value.id == soItem.taxRateId);
                    this.saleProducts.push({
                        rowId: soItem.id,
                        itemId: soItem.id,
                        serviceProductId: soItem.serviceProductId,
                        productId: soItem.productId,
                        unitPrice: soItem.unitPrice == 0 ? '0' : soItem.unitPrice,
                        quantity: qty,
                        highQty: 0,
                        schemePhysicalQuantity: 0,
                        discount: soItem.discount == null ? 0 : soItem.discount,
                        offerQuantity: 0,
                        fixDiscount: soItem.fixDiscount == null ? 0 : soItem.fixDiscount,
                        taxRateId: soItem.taxRateId == null || soItem.taxRateId == undefined ? taxRateId1 : soItem.taxRateId,
                        taxMethod: soItem.taxMethod == null || soItem.taxMethod == undefined ? taxMethod1 : soItem.taxMethod,
                        rate: vat == null || vat == undefined ? 0 : vat.rate,
                        soQty: qty,
                        currentQuantity: 0,
                        saleReturnDays: 0,
                        lineTotal: 0,
                        unitPerPack: 0,
                        levelOneUnit: '',
                        basicUnit: '',
                        description: soItem.description,
                        serviceItem: true,
                        isFree: soItem.isFree,
                        serial: null,
                        guaranteeDate: '',
                        isSerial: false,
                        guarantee: false,
                        styleNumber: soItem.styleNumber,
                        unitName: soItem.unitName,
                        discountSign: '%',
                    });

                    var item = this.saleProducts.find((x) => {
                        return x.rowId == soItem.id;
                    });

                    this.updateLineTotal(item.unitPrice, "unitPrice", item);
                    this.updateLineTotal(item.quantity, "quantity", item);
                    this.updateLineTotal(item.highQty, "highQty", item);
                }
                else {
                    if (this.saleProducts.some(x => x.productId == productId)) {
                        var prd = this.saleProducts.find(x => x.productId == productId);
                        prd.quantity++;
                        this.updateLineTotal(prd.quantity, "quantity", prd);
                    }
                    else {

                        if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
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
                        if (isTemplate) {

                            this.saleProducts.push({
                                rowId: this.createUUID(),
                                productId: prod.id,
                                itemId: soItem.id,
                                unitPrice: price,
                                quantity: qty,
                                highQty: 0,
                                soQty: 0,
                                schemePhysicalQuantity: 0,
                                discount: 0,
                                fixDiscount: 0,
                                lineItemVAt: 0,
                                description: soItem.productName,
                                serviceItem: false,
                                quantityOut: 0,
                                taxRateId: prod.taxRateId,
                                saleReturnDays: prod.saleReturnDays,
                                taxMethod: prod.taxMethod,
                                rate: rate,
                                serial: '',
                                guaranteeDate: '',
                                isSerial: newProduct.serial,
                                guarantee: newProduct.guarantee,
                                currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                                lineTotal: prod.salePrice * 1,
                                offerQuantity: 0,
                                unitPerPack: newProduct.unitPerPack,
                                levelOneUnit: prod.levelOneUnit,
                                basicUnit: prod.basicUnit,
                                isFree: false,
                                styleNumber: '',
                                unitName: '',

                                discountSign: '%',
                            });
                        }

                        else if (qty != null && qty != undefined && qty != 0) {

                            this.saleProducts.push({
                                rowId: this.createUUID(),
                                productId: prod.id,
                                itemId: soItem.id,
                                unitPrice: so ? price : '0',
                                quantity: qty,
                                highQty: soItem.highQty,
                                soQty: qty,
                                discount: soItem.discount,
                                fixDiscount: soItem.fixDiscount,
                                lineItemVAt: 0,
                                schemePhysicalQuantity: 0,
                                description: soItem.description,
                                serviceItem: false,
                                taxRateId: soItem.taxRateId,
                                saleReturnDays: prod.saleReturnDays,
                                taxMethod: soItem.taxMethod,
                                rate: rate,
                                serial: soItem.serial,
                                guaranteeDate: soItem.guaranteeDate,
                                isSerial: newProduct.serial,
                                guarantee: newProduct.guarantee,
                                lineTotal: prod.salePrice * 1,

                                offerQuantity: 0,
                                unitPerPack: newProduct.unitPerPack,
                                levelOneUnit: prod.levelOneUnit,
                                basicUnit: prod.basicUnit,
                                isFree: soItem.isFree,
                                styleNumber: soItem.styleNumber,
                                unitName: soItem.unitName,

                                discountSign: '%',
                            });
                        }
                        else {

                            this.saleProducts.push({
                                rowId: this.createUUID(),
                                productId: prod.id,
                                unitPrice: '0',
                                quantity: this.isMultiUnit == 'true' ? 0 : 1,
                                soQty: 0,
                                highQty: 0,
                                schemePhysicalQuantity: 0,
                                discount: 0,
                                fixDiscount: 0,
                                lineItemVAt: 0,
                                discountAmount: 0,
                                vatAmount: 0,
                                grossAmount: 0,
                                totalAmount: 0,

                                description: newProduct.name,
                                serviceItem: false,
                                taxRateId: taxRateId,
                                saleReturnDays: 0,
                                taxMethod: taxMethod,
                                rate: rate,
                                serial: '',
                                guaranteeDate: '',
                                isSerial: false,
                                guarantee: false,
                                lineTotal: 0,
                                offerQuantity: 0,
                                unitPerPack: 0,
                                levelOneUnit: '',
                                basicUnit: '',
                                isFree: false,
                                discountSign: '%',
                            });

                            this.GetProductInfo(productId);
                        }

                    }
                    var product = this.saleProducts.find((x) => {
                        return x.productId == productId;
                    });

                    this.getVatValue(product.taxRateId, product);
                    this.updateLineTotal(product.quantity, "quantity", product);
                    this.updateLineTotal(product.highQty, "highQty", product);

                    this.product.id = "";
                    this.rendered++;
                }
            },

            GetProductInfo: function (id) {
                var root = this;
                var token = localStorage.getItem('token');

                root.$https.get('/Product/GetProductDetailForInvoiceQuery?id=' + id + '&wareHouseId=' + this.wareHouseId + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var newProduct = response.data;
                        var prod = root.saleProducts.find((x) => x.productId == id);

                        if (prod != undefined) {

                            prod.styleNumber = newProduct.styleNumber;
                            prod.unitPrice = newProduct.salePrice == 0 ? '0' : newProduct.salePrice;
                            prod.saleReturnDays = newProduct.saleReturnDays;
                            prod.isSerial = newProduct.serial;
                            prod.guarantee = newProduct.guarantee;
                            prod.levelOneUnit = newProduct.levelOneUnit;
                            prod.basicUnit = newProduct.basicUnit;
                            prod.unitPerPack = newProduct.unitPerPack;

                            root.updateLineTotal(prod.quantity, "quantity", prod);
                        }
                    }
                });
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
                    styleNumber: '',
                    unitName: '',
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
                this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
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

            getData: function () {
                var root = this;

                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Product/TaxRateList", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                            if (root.saleItems != undefined) {

                                root.saleItems.forEach(function (item) {
                                    var vat = root.vats.find((value) => value.id == item.taxRateId);
                                    if (root.formName === 'saleReturn' && item.remainingQuantity <= 0)
                                        return
                                    if (item.productId != null) {
                                        root.products.push(item.product);

                                        root.saleProducts.push({
                                            rowId: item.id,
                                            productId: item.productId,
                                            itemId: item.soItemId,
                                            unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                            quantity: item.quantity,
                                            schemePhysicalQuantity: 0,
                                            highQty: item.highQty,
                                            discount: item.discount,
                                            offerQuantity: item.offerQuantity == undefined ? 0 : item.offerQuantity,
                                            fixDiscount: item.fixDiscount,
                                            taxRateId: item.taxRateId,
                                            taxMethod: item.taxMethod,
                                            rate: vat.rate,
                                            soQty: item.soQty,
                                            currentQuantity: item.product.inventory == null ? 0 : item.product.inventory.currentQuantity,
                                            saleReturnDays: item.saleReturnDays,
                                            lineTotal: item.unitPrice * item.quantity,
                                            unitPerPack: item.unitPerPack,
                                            levelOneUnit: item.product.levelOneUnit,
                                            basicUnit: item.product.basicUnit,
                                            description: item.description,
                                            styleNumber: item.styleNumber,
                                            unitName: item.unitName,
                                            serviceItem: false,
                                            isFree: item.isFree,
                                            serial: item.serial,
                                            guaranteeDate: item.guaranteeDate,
                                            isSerial: item.product.serial,
                                            guarantee: item.product.guarantee,
                                            discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
                                        });
                                    }
                                    else {

                                        root.saleProducts.push({
                                            rowId: item.id,
                                            productId: item.productId,
                                            itemId: item.soItemId,
                                            unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                            quantity: item.quantity,
                                            highQty: 0,
                                            schemePhysicalQuantity: 0,
                                            discount: item.discount,
                                            offerQuantity: item.offerQuantity == undefined ? 0 : item.offerQuantity,
                                            fixDiscount: item.fixDiscount,
                                            taxRateId: item.taxRateId,
                                            taxMethod: item.taxMethod,
                                            rate: vat.rate,
                                            soQty: item.soQty,
                                            currentQuantity: 0,
                                            saleReturnDays: item.saleReturnDays,
                                            lineTotal: item.unitPrice * item.quantity,
                                            unitPerPack: item.unitPerPack,
                                            levelOneUnit: '',
                                            basicUnit: '',
                                            description: item.description,
                                            styleNumber: item.styleNumber,
                                            unitName: item.unitName,
                                            serviceItem: true,
                                            isFree: item.isFree,
                                            serial: item.serial,
                                            guaranteeDate: item.guaranteeDate,
                                            isSerial: false,
                                            guarantee: false,

                                            discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
                                        });
                                    }



                                    var product = root.saleProducts.find((x) => {
                                        return x.rowId == item.id;
                                    });

                                    root.getVatValue(product.taxRateId, product);
                                    root.updateLineTotal(product.quantity, "quantity", product);
                                    root.updateLineTotal(product.highQty, "highQty", product);
                                    root.product.id = "";
                                    root.rendered++;
                                });
                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;
                                root.calcuateSummary();
                                root.$emit("details", root.saleProducts);
}
                        }
                    })
            },
        },
        created: function () {

            this.isDiscountBeforeVat = this.isbeforetax == true ? true : false;
            this.isFixedDiscount = this.isFixed == true ? true : false;
            this.transactionLevelDiscount = this.transactionLevelDiscountProp;
            if (this.$i18n.locale == 'en') {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }
            this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory') == 'true' ? true : false;
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');

            //this.$barcodeScanner.init(this.onBarcodeScanned);
            //For Scanner Code
            var barcode = '';
            var interval;
            document.addEventListener('keydown', function (evt) {
                if (interval)
                    clearInterval(interval);
                if (evt.code === 'Enter') {
                    if (barcode) {
                        root.onBarcodeScanned(barcode);
                    }
                    barcode = '';
                    return;

                }
                if (evt.key !== 'Shift')
                    barcode += evt.key;
            });

            //End
            this.getData();
            var root = this;
            localStorage.setItem("BarcodeScan", 'SaleItem')
            if (this.$route.query.mobiledata != undefined) {
                //root.purchaseProducts = root.$route.query.data.mobileOrderItemLookupModels;
                for (var j = 0; j < this.$route.query.mobiledata.mobileOrderItemLookupModels.length; j++) {

                    /*  this.saleProducts.rowId[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].id[j];*/
                    this.saleProducts.quantity[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].quantity[j];
                    //root.updateLineTotal(root.purchaseProducts[j].quantity, "quantity", root.purchaseProducts[j]);
                    //root.updateLineTotal(root.purchaseProducts[j].unitPrice, "unitPrice", root.purchaseProducts[j]);

                }
                root.calcuateSummary();
                this.saleProducts.rowId = this.$route.query.mobiledata.mobileOrderItemLookupModels.rowId;
                this.saleProducts.quantity = this.$route.query.mobiledata.mobileOrderItemLookupModels.quantity;
                this.note = this.noteVal;

            }
        },
        mounted: function () {



            this.note = this.noteVal;

            this.soInventoryReserve = localStorage.getItem('SoInventoryReserve') == 'true' ? true : false;
            this.isDiscountBeforeVat = this.isbeforetax == true ? true : false;
            this.isFixedDiscount = this.isFixed == true ? true : false;
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            /*this.isMultiUnit = localStorage.getItem('IsMultiUnit');*/
            this.isMultiUnit = 'false';

            this.currency = localStorage.getItem('currency');
            this.dayStart = localStorage.getItem('DayStart');
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;



            this.changePriceDuringSale = localStorage.getItem('changePriceDuringSale');
            this.changePriceDuringSale == 'true' ? (this.changePriceDuringSale = true) : (this.changePriceDuringSale = false);
            this.giveDiscountDuringSale = localStorage.getItem('giveDicountDuringSale');
            this.giveDiscountDuringSale == 'true' ? (this.giveDiscountDuringSale = true) : (this.giveDiscountDuringSale = false);
            this.GetProductList();

        },

    };
</script>

