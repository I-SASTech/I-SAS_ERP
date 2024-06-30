<template>
    <div class="col-lg-12">
        <div class=" table-responsive mt-3">
            <table class="table">
                <thead class="thead-light table-hover">
                    <tr>
                        <th style="width: 30px;">
                            #
                        </th>
                        <th style="width: 150px;">
                            {{ $t('PurchaseItem.Product') }}
                        </th>
                        <th style="width: 90px;" class="text-center" v-if="isService">
                            {{ $t('SaleItem.Unit') }}        
                        </th>
                        <th style="width: 150px;" v-if="colorVariants">
                            {{ $t('SaleItem.Color') }}
                        </th>
                        

                        <th style="width: 90px;" class="text-center">
                            {{ $t('PurchaseItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 65px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('PurchaseItem.UnitPerPack') }}
                        </th>
                        <th style="width: 90px;" class="text-center" v-if="isMultiUnit == 'true'">
                            {{ $t('PurchaseItem.HighQty') }}
                        </th>
                        <th style="width: 80px;" class="text-center">
                            {{ $t('PurchaseItem.Qty') }}
                        </th>
                        <th style="width: 100px;" class="text-center" v-if="isMultiUnit == 'true'">
                            {{ $t('PurchaseItem.TOTALQTY') }}
                        </th>
                        <th style="width: 60px;" class="text-center" v-if="purchase != undefined">
                            {{ $t('PurchaseItem.CurrentQty') }}
                        </th>
                        <th style="width:110px;" class="text-center"
                            v-if="(purchaseProducts.filter(x => x.isExpire).length > 0) && !po">
                            {{ $t('PurchaseItem.ExpDate') }}
                        </th>

                        <th style="width:110px;" class="text-center" v-if="isValid('OpenBatch')">
                            {{ $t('PurchaseItem.BatchNo') }}
                        </th>

                        <th style="width:150px;" class="text-center"
                            v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                            {{ $t('PurchaseItem.WarrantyType') }}
                        </th>
                        <th style="width: 100px;"
                            v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                            {{ $t('PurchaseItem.Guarantee') }}
                        </th>
                        <th style="width: 100px;" class="text-center"
                            v-if="purchaseProducts.filter(x => x.serial).length > 0 && isSerial && !po">
                            {{ $t('PurchaseItem.Serial') }}
                        </th>
                        <th style="width: 70px;" class="text-center" v-if="!isDiscountOnTransaction">
                            {{ $t('PurchaseItem.Disc%') }}
                        </th>
                        <th style="width: 70px;" class="text-center" v-else>

                        </th>
                        <th v-if="defaultVat == 'DefaultVatItem' || defaultVat == 'DefaultVatHeadItem'"
                            style="width: 125px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th style="width: 85px;" class="text-end">
                            {{ $t('PurchaseItem.LineTotal') }}
                        </th>
                        <th style="width: 40px"></th>
                    </tr>
                </thead>
                <tbody>

                    <tr v-for="(prod, index) in purchaseProducts" :key="prod.productId + index" v-bind:class="{ 'alert-danger': prod.outOfStock }">
                        <td class="border-top-0">
                            {{ index + 1 }}
                        </td>
                        <td class="border-top-0">
                            <span v-if="prod.productId == null">
                                {{ prod.description }}
                            </span>
                            <span v-else>
                                {{ products.find(x => x.id == prod.productId).displayName }}
                            </span>
                        </td>
                        <td v-if="isService">
                            <input type="text" v-model="prod.unitName"
                                   @focus="$event.target.select()"
                                   class="form-control text-center" />
                        </td>

                        <td v-if="colorVariants">
                            <colordropdown v-model="prod.colorId" :isSaleItem="true"
                                           @update:modelValue="GetColorName(prod.colorId, prod)" :modelValue="prod.colorId" />
                        </td>

                        <td class="border-top-0">
                            <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false"
                                              @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                        </td>
                        <td class="text-center" v-if="isValid('CanViewUnitPerPack')">
                            {{ prod.unitPerPack }}
                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit == 'true'">
                            <decimal-to-fixed v-model="prod.highQty"
                                              v-bind:disabled="prod.productId == null || prod.isService"
                                              @focus="$event.target.select()" :isQunatity="true"
                                              @update:modelValue="updateLineTotal(prod.highQty, 'highQty', prod)" />
                            <small style="font-weight: 500;font-size:70%;">
                                {{ prod.levelOneUnit }}
                            </small>
                        </td>

                        <td class="border-top-0 text-center">
                            <decimal-to-fixed v-model="prod.quantity" style="" @focus="$event.target.select()"
                                              :isQunatity="true" @update:modelValue="updateLineTotal(prod.quantity, 'quantity', prod)" />
                            <small v-if="isMultiUnit == 'true'" style="font-weight: 500;font-size:70%;">
                                {{ prod.basicUnit }}
                            </small>
                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit == 'true'">
                            {{ parseInt(parseFloat(prod.highQty * prod.unitPerPack) + parseFloat(prod.quantity)) }}
                        </td>

                        <td class="border-top-0 text-center" v-if="purchase != undefined">
                            {{ prod.isService ? 0 : prod.inventory.currentQuantity }}
                        </td>
                        <td class="border-top-0 text-center"
                            v-if="(purchaseProducts.filter(x => x.isExpire).length > 0) && !po">
                            <datepicker v-if="prod.isExpire || isFifo" v-model="prod.expiryDate" />
                            <span v-else>
                                -
                            </span>
                        </td>
                        <td class="border-top-0 text-center" v-if="isValid('OpenBatch')">
                            <input type="text" v-model="prod.batchNo" @focus="$event.target.select()"
                                   class="form-control input-border text-center tableHoverOn" />
                        </td>

                        <td class="border-top-0 text-center"
                            v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                            <warranty-type-dropdown v-if="prod.guarantee" v-model="prod.warrantyTypeId"
                                                    :modelValue="prod.warrantyTypeId" />
                            <span v-else>
                                -
                            </span>
                        </td>

                        <td class="border-top-0  text-center"
                            v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                            <datepicker v-if="prod.guarantee" v-model="prod.guaranteeDate" />
                            <span v-else>
                                -
                            </span>
                        </td>
                        <td class="border-top-0 text-center"
                            v-if="purchaseProducts.filter(x => x.serial).length > 0 && isSerial && !po">
                            <input type="text" v-model="prod.serialNo" v-if="prod.serial"
                                   @focus="$event.target.select()"
                                   class="form-control input-border text-center tableHoverOn" />
                            <span v-else>
                                -
                            </span>
                        </td>
                        <td v-if="!isDiscountOnTransaction">
                            <div v-if="prod.discountSign == '%'">

                                <div class="input-group">
                                    <decimal-to-fixed v-model="prod.discount" v-bind:salePriceCheck="false"
                                                      @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" class="btn btn-primary"
                                            type="button" id="button-addon2">
                                        {{ prod.discountSign }}
                                    </button>
                                </div>
                            </div>
                            <div v-else-if="prod.discountSign == 'F'">

                                <div class="input-group">
                                    <decimal-to-fixed v-model="prod.fixDiscount" v-bind:salePriceCheck="false"
                                                      @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" class="btn btn-primary"
                                            type="button" id="button-addon2">
                                        {{ prod.discountSign }}
                                    </button>
                                </div>
                            </div>
                        </td>

                        <td v-else>
                        </td>
                        <td v-if="defaultVat == 'DefaultVatItem' || defaultVat == 'DefaultVatHeadItem'">
                            <taxratedropdown v-model="prod.taxRateId" @update:modelValue="getVatValue(prod.taxRateId, prod)" />
                        </td>

                        <td class="border-top-0 text-end">
                            {{ currency }} {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0, -1))   }}
                        </td>
                        <td class="text-end">
                            <a href="javascript:void(0);" @click="removeProduct(prod.rowId)">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </a>
                        </td>
                    </tr>

                    <tr v-if="servicePurchase && purchase == undefined && (purchaseOrderId == '' || purchaseOrderId == null)">
                        <td class="border-top-0">
                        </td>
                        <td class="border-top-0">
                            <textarea rows="2" v-model="newItem.description" class="form-control input-border " />
                        </td>
                        <td class="border-top-0" v-if="isService">
                            <input type="text" v-model="newItem.unitName"
                                   @focus="$event.target.select()"
                                   class="form-control text-center" />                        </td>
                        <td class="border-top-0">
                            <decimal-to-fixed v-model="newItem.unitPrice" />
                        </td>
                        <!--<td class="text-center" v-if="isValid('CanViewUnitPerPack')">
                        </td>-->
                        <td class="border-top-0 text-center" v-if="isMultiUnit == 'true'">
                            <decimal-to-fixed v-bind:salePriceCheck="false" v-model="newItem.highQty" :disabled="true" />
                        </td>

                        <td class="border-top-0 text-center">
                            <decimal-to-fixed v-bind:salePriceCheck="false" v-model="newItem.quantity" />
                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit == 'true'">
                        </td>

                        <td class="border-top-0 text-center" v-if="purchase != undefined">
                        </td>
                        <td class="border-top-0 text-center"
                            v-if="(purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo) && !po">
                        </td>
                        <td class="border-top-0 text-center" v-if="isFifo && !po">
                        </td>

                        <td class="border-top-0 text-center"
                            v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                        </td>

                        <td class="border-top-0  text-center"
                            v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                        </td>
                        <td class="border-top-0 text-center"
                            v-if="purchaseProducts.filter(x => x.serial).length > 0 && isSerial && !po">
                        </td>
                        <td class="border-top-0">
                        </td>

                        <td class="border-top-0 text-right">
                        </td>
                        <td class="border-top-0 pt-0 text-end">
                            <button @click="newItemProduct(false)" title="Add Item" v-bind:disabled="newItem.description == ''"
                                    class="btn btn-primary btn-sm btn-round  btn-icon float-right">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="table-responsive  mt-4" v-if="colorVariants">
            <table class="table add_table_list_bg" style="table-layout:fixed;">
                <thead class="thead-light table-hover">
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th>
                            {{ $t('SaleItem.Product') }}12
                        </th>
                        <th class="text-center">
                            {{ $t('SaleItem.Color') }}
                        </th>
                        <th v-for="size in saleSizeAssortment" class="text-center" :key="size.sizeId">
                            {{ size.name }}
                        </th>
                        <th class="text-center">
                            Total
                        </th>
                    </tr>
                </thead>
                <tbody>

                    <tr v-for="(prod, index) in purchaseProducts" :key="prod.rowId" v-bind:class="{ 'alert-danger': prod.outOfStock }" class="tble_border_remove"
                        style="background:#EAF1FE ;">
                        <td>{{ index + 1 }}</td>
                        <td>
                            <span>
                                {{ products.find(x => x.id == prod.productId).displayName }}
                            </span>

                            <span v-if="products.find(x => x.id == prod.productId).promotionOffer != undefined && products.find(x => x.id == prod.productId).promotionOffer.fixedDiscount > 0"
                                  class="badge badge-pill badge-success">
                                Rs {{
                                        (products.find(x => x.id ==
                                            prod.productId).promotionOffer.fixedDiscount).toFixed(3).slice(0, -1)
                                }},
                                ({{
                                        products.find(x => x.id == prod.productId).promotionOffer.stockLimit -
                                        products.find(x => x.id == prod.productId).promotionOffer.quantityOut
                                }})
                            </span>
                            <span v-if="products.find(x => x.id == prod.productId).promotionOffer != undefined && products.find(x => x.id == prod.productId).promotionOffer.discountPercentage > 0"
                                  class="badge badge-pill badge-success">
                                {{
                                        (products.find(x => x.id ==
                                            prod.productId).promotionOffer.discountPercentage).toFixed(3).slice(0, -1)
                                }}%,
                                ({{
                                        products.find(x => x.id == prod.productId).promotionOffer.stockLimit -
                                        products.find(x => x.id == prod.productId).promotionOffer.quantityOut
                                }})
                            </span>
                            <span v-if="products.find(x => x.id == prod.productId).bundleCategory != undefined"
                                  class="badge badge-pill badge-success">
                                {{ products.find(x => x.id == prod.productId).bundleCategory.buy }} +
                                {{ products.find(x => x.id == prod.productId).bundleCategory.get }},
                                ({{
                                        products.find(x => x.id == prod.productId).bundleCategory.stockLimit -
                                        products.find(x => x.id == prod.productId).bundleCategory.quantityOut
                                }})
                            </span>
                        </td>

                        <td class="text-center">
                            {{ prod.colorName }}
                        </td>
                        <td v-for="size in prod.saleSizeAssortment" :key="size.sizeId" class="text-center">
                            <decimal-to-fixed v-model="size.quantity" v-bind:salePriceCheck="false"
                                              @update:modelValue="sizeQtyVerify(prod)" v-bind:disabled="sizeAllowInput(size.sizeId, prod)"
                                              class="form-control input-border text-center tableHoverOn" />
                        </td>
                        <td class="text-center">
                            {{ prod.quantity }}
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">

                <div class="mt-4" v-if="purchase == undefined && (purchaseOrderId == '' || purchaseOrderId == null)">
                    <product-dropdown @update:modelValues="addProduct" :raw="raw" :isservice="servicePurchase" :isForRaw="isForRaw"
                                      v-if="purchase == undefined && (purchaseOrderId == '' || purchaseOrderId == null)" />
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                <div class="mt-4 mb-5" v-bind:key="rendered + 'g'">
                    <table class="table" style="background-color: #f1f5fa;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:68%;">
                                    <span class="fw-bold">{{ $t('SaleItem.GrossTotal') }} </span>
                                </td>
                                <td class="text-end" style="width:32%;">{{ parseFloat(summary.withDisc).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}</td>
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
                                        <button v-if="taxMethod == ('Inclusive' || 'شامل')" class="btn btn-primary"
                                                type="button" id="button-addon2" disabled>
                                            %
                                        </button>
                                        <button v-else class="btn btn-primary" v-on:click="UpdateDiscountField('fixed')"
                                                type="button" id="button-addon2">
                                            {{ isFixedDiscount ? 'F' : '%' }}
                                        </button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:32%;">
                                    {{
                                    parseFloat(transactionLevelTotalDiscount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,")
                                    }}
                                </td>
                            </tr>
                            <tr v-if="(isDiscountOnTransaction && isDiscountBeforeVat && transactionLevelDiscount > 0)">
                                <td colspan="2" style="width:68%;">
                                    <span style="height:33px !important; ">{{ $t('SaleItem.TotalAfterDiscount') }}</span>

                                </td>

                                <td class="text-end" style="width:32%;">
                                    {{
                                    parseFloat(summary.totalAfterDiscount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,")
                                    }}
                                </td>
                            </tr>
                            <tr v-if="!isDiscountOnTransaction">
                                <td colspan="2" style="width:68%;">
                                    <span class="fw-bold">{{ $t('SaleItem.DiscountBeforeVat') }}</span>

                                </td>
                                <td class="text-end" style="width:32%;">
                                    {{
 parseFloat(summary.discount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,")
                                    }}
                                </td>
                            </tr>
                            <tr v-for="(vat, index) in paidVatList" :key="index">
                                <td class="fw-bold" colspan="2" style="width:68%;">{{ vat.name }} % ({{ taxMethod }})</td>
                                <td class="text-end" style="width:32%;">
                                    {{
                                    parseFloat(vat.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                        "$1,")
                                    }}
                                </td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && !isDiscountBeforeVat">
                                <td style="width:48%;" class="px-0">
                                    <span style="width:100%;" class="m-0 p-0">{{ $t('SaleItem.DiscountAfterVat') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" style="padding: 6px 4px; border-radius: 0;"
                                           v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('SaleItem.ApplyBeforeTax') }}</small>
                                        </a>
                                    </span>
                                </td>
                                <td style="width:20%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="transactionLevelDiscount" @update:modelValue="calcuateSummary" />
                                        <button v-if="taxMethod == ('Inclusive' || 'شامل')" class="btn btn-primary" disabled
                                                type="button" id="button-addon2">
                                            %
                                        </button>
                                        <button v-else class="btn btn-primary" v-on:click="UpdateDiscountField('fixed')"
                                                type="button" id="button-addon2">
                                            {{ isFixedDiscount ? 'F' : '%' }}
                                        </button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:32%;">
                                    {{
                                    parseFloat(transactionLevelTotalDiscount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,")
                                    }}
                                </td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && !isDiscountBeforeVat && transactionLevelDiscount > 0">
                                <td colspan="2" style="width:68%;">
                                    <span style="height:33px !important; ">{{ $t('SaleItem.TotalAfterDiscount') }}</span>

                                </td>

                                <td class="text-end" style="width:32%;">
                                    {{
                                    parseFloat(summary.totalAfterDiscount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,")
                                    }}
                                </td>
                            </tr>
                            <tr>
                                <td style="width:48%;">
                                    <input class="form-control" type="text" :value="$t('SaleItem.Adjustment')"
                                           style="border: 1px dashed #1761fd;" />
                                </td>
                                <td style="width:20%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="adjustment" @update:modelValue="calcuateSummary" />
                                        <button v-on:click="OnChangeOveallDiscount" class="btn btn-primary" type="button"
                                                id="button-addon2">
                                            {{ adjustmentSign }}
                                        </button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:35%;">
                                    {{ adjustmentSign == '+' ? adjustment : (-1) * adjustment }}
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:68%;">
                                    <span style="font-weight:bolder; font-size:16px">
                                        {{ $t('SaleItem.TotalDuewithVAT') }}
                                        ({{ currency }})
                                    </span>
                                </td>
                                <td class="text-end" style="width: 32%; font-weight: bolder; font-size: 16px">
                                    {{
                                    parseFloat(summary.withVat).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,")
                                    }}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    /*import Multiselect from 'vue-multiselect'*/
    export default {
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems', 'raw', 'taxMethod', 'taxRateId', 'po', 'purchaseid', 'purchaseOrderId', 'adjustmentProp', 'adjustmentSignProp', 'isDiscountOnTransaction', 'transactionLevelDiscountProp', 'isFixed', 'isBeforeTax', 'documentType', 'isForRaw1'],
        mixins: [clickMixin],
        //components: {
        //    Multiselect
        //},
        data: function () {
            return {
                isForRaw: false,
                transactionLevelDiscount: 0,
                adjustment: 0,
                adjustmentSign: '+',
                isDiscountBeforeVat: false,
                isFixedDiscount: false,
                isService: false,
                transactionLevelTotalDiscount: 0,

                options: [],
                saleSizeAssortment: [],
                colorList: [],
                newItem: {
                    description: '',
                    unitName: '',
                    unitPrice: 0,
                    highQty: 0,
                    quantity: 0,
                    discount: 0,
                    fixDiscount: 0
                },

                colorVariants: false,
                isSerial: false,
                isFifo: false,
                decimalQuantity: false,
                internationalPurchase: '',
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                purchaseProducts: [],
                loading: false,
                vats: [],
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    totalAfterDiscount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVat: 0,
                    inclusiveVat: 0,
                    totalCarton: 0,
                    totalPieces: 0
                },
                currency: '',
                searchTerm: '',
                isMultiUnit: '',
                wareRendered: 0,
                isRaw: false,
                servicePurchase: false,
                productList: [],
                defaultVat: false,
                paidVatList: []
            };
        },
        validations() {},
        filters: {

        },
        computed: {
            itemDisable() {
                if (this.taxMethod != '' && this.taxMethod != null && this.taxRateId != '00000000-0000-0000-0000-000000000000' && this.taxRateId != undefined)
                    return false;
                return true;
            }
        },

        methods: {
            ClearRecord: function () {

                this.purchaseProducts = [];
                this.summary = {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVat: 0,
                    inclusiveVat: 0,
                    totalCarton: 0,
                    totalPieces: 0
                };
                this.newItem = {
                    description: '',
                    unitName: '',
                    unitPrice: 0,
                    highQty: 0,
                    quantity: 0,
                    discount: 0,
                    fixDiscount: 0
                };
                this.paidVatList = [];

            },

            UpdateDiscountField: function (prop) {
                if (prop === 'fixed')
                    this.isFixedDiscount = this.isFixedDiscount ? false : true
                if (prop === 'beforeTax')
                    this.isDiscountBeforeVat = this.isDiscountBeforeVat ? false : true
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
            newItemProduct: function (Edit, purchaseOrder) {


                var rate = 0;
                if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined && this.taxRateId != '' && this.taxRateId != null) {
                    rate = this.vats.find((value) => value.id == this.taxRateId).rate;
                }

                if (Edit == true) {
                    this.purchaseProducts.push({
                        rowId: rowId,
                        productId: null,
                        description: purchaseOrder.description,
                        unitName: purchaseOrder.unitName,
                        unitPrice: purchaseOrder.unitPrice,
                        quantity: purchaseOrder.remainingQuantity,
                        receiveQuantity: 0,
                        highQty: purchaseOrder.highQty,
                        totalPiece: 0,
                        remainingQuantity: 0,
                        levelOneUnit: '',
                        basicUnit: '',
                        unitPerPack: 0,
                        discount: purchaseOrder.discount,
                        fixDiscount: purchaseOrder.fixDiscount,
                        taxRateId: this.taxRateId,
                        rate: rate,
                        taxMethod: this.taxMethod,
                        expiryDate: "",
                        isExpire: false,
                        isService: true,
                        batchNo: "",
                        lineTotal: 0,

                        guarantee: false,
                        serial: false,

                        serialNo: '',
                        guaranteeDate: '',
                        warrantyTypeId: '',
                        inventory: null,
                        discountSign: '%',
                    });

                }
                else {
                    var rowId = this.createUUID();

                    this.purchaseProducts.push({
                        rowId: rowId,
                        productId: null,
                        description: this.newItem.description,
                        unitName: this.newItem.unitName,
                        unitPrice: this.newItem.unitPrice,
                        quantity: this.newItem.quantity,
                        receiveQuantity: 0,
                        highQty: this.newItem.highQty,
                        totalPiece: 0,
                        remainingQuantity: 0,
                        levelOneUnit: '',
                        basicUnit: '',
                        unitPerPack: 0,
                        discount: this.newItem.discount,
                        fixDiscount: this.newItem.fixDiscount,
                        taxRateId: this.taxRateId,
                        rate: rate,
                        taxMethod: this.taxMethod,
                        expiryDate: "",
                        isExpire: false,
                        isService: true,
                        batchNo: "",
                        lineTotal: 0,

                        guarantee: false,
                        serial: false,

                        serialNo: '',
                        guaranteeDate: '',
                        warrantyTypeId: '',
                        inventory: null,
                        discountSign: '%',
                    });
                }



                this.newItem.description = '';
                this.newItem.unitName = '';
                this.newItem.unitPrice = 0;
                this.newItem.highQty = 0;
                this.newItem.quantity = 0;
                this.newItem.discount = 0;
                this.newItem.fixDiscount = 0;

                var product = this.purchaseProducts.find((x) => {
                    return x.rowId == rowId;
                });

                this.updateLineTotal(product.quantity, "quantity", product);
            },

            newItemProductForQuotation: function (productId, product, isTemp, taxRateId, taxMethod) {


                isTemp = false;

                var rate = 0;
                if (taxRateId != "00000000-0000-0000-0000-000000000000" && taxRateId != undefined && taxRateId != '' && taxRateId != null) {
                    rate = this.vats.find((value) => value.id == taxRateId).rate;
                }

                var rowId = this.createUUID();
                this.purchaseProducts.push({
                    rowId: rowId,
                    productId: null,
                    description: product.description,
                    unitName: product.unitName,
                    unitPrice: product.unitPrice,
                    quantity: product.quantity,
                    receiveQuantity: 0,
                    highQty: product.highQty,
                    totalPiece: 0,
                    remainingQuantity: 0,
                    levelOneUnit: '',
                    basicUnit: '',
                    unitPerPack: 0,
                    discount: product.discount,
                    fixDiscount: product.fixDiscount,
                    taxRateId: taxRateId,
                    rate: rate,
                    taxMethod: taxMethod,
                    expiryDate: "",
                    isExpire: false,
                    isService: true,
                    batchNo: "",
                    lineTotal: 0,

                    guarantee: false,
                    serial: false,

                    serialNo: '',
                    guaranteeDate: '',
                    warrantyTypeId: '',
                    inventory: null,
                    discountSign: '%',
                });



                var product12 = this.purchaseProducts.find((x) => {
                    return x.rowId == rowId;
                });

                this.updateLineTotal(product.quantity, "quantity", product12);
            },

            changeProduct: function (NewProdId, rowId) {

                this.purchaseProducts = this.purchaseProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },

            GetColorName: function (colorId, product) {
                if (colorId != null && colorId != undefined && colorId != '') {
                    var color = this.colorList.find(x => x.id == colorId);
                    if (color != undefined) {
                        product.colorName = color.name;
                    }
                } else {
                    product.colorName = '';
                }
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
                        return a + parseFloat(c.quantity)
                    }, 0)) {
                        product['outOfStock'] = true;
                    } else {
                        product['outOfStock'] = false;
                    }
                } else {
                    product['outOfStock'] = true;
                }
            },

            changeVatInformation: function (value, prop) {
                var root = this;
                if (prop == 'TaxMethod') {
                    root.purchaseProducts.forEach(function (item) {
                        item.taxMethod = value;
                        root.taxMethod = value;
                        root.updateLineTotal(item.unitPrice, "unitPrice", item);
                    });
                }
                else if (prop == 'DiscountType') {
                    root.transactionLevelDiscount = 0;
                    root.purchaseProducts.forEach(function (item) {
                        item.discount = 0;
                        item.fixDiscount = 0;
                        root.updateLineTotal(item.unitPrice, "unitPrice", item);
                    });
                }
                else if (prop == 'TaxRateId') {
                    root.purchaseProducts.forEach(function (item) {
                        item.taxRateId = value;
                        root.updateLineTotal(item.unitPrice, "unitPrice", item);
                    });
                }
            },

            calcuateSummary: function () {

                this.summary.item = this.purchaseProducts.length;
                if (this.decimalQuantity) {
                    this.summary.totalPieces = this.purchaseProducts.reduce((totalQty, prod) => totalQty + parseFloat(prod.quantity), 0);
                } else {
                    this.summary.totalPieces = this.purchaseProducts.reduce((totalQty, prod) => totalQty + parseInt(prod.quantity), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.totalCarton = this.purchaseProducts.reduce((totalCarton, prod) => totalCarton + parseFloat(prod.highQty), 0);
                } else {
                    this.summary.totalCarton = this.purchaseProducts.reduce((totalCarton, prod) => totalCarton + parseInt(prod.highQty), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.qty = this.purchaseProducts.reduce((qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                } else {
                    this.summary.qty = this.purchaseProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }

                //this.summary.total = this.purchaseProducts.reduce((total, prod) => total + (prod.totalPiece) * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                this.summary.total = this.purchaseProducts.reduce((total, prod) => total + parseFloat(prod.lineTotal), 0).toFixed(2);


                if (!this.isDiscountOnTransaction) {
                    this.transactionLevelDiscount = 0;
                }
                var vatRate = 0;
                var discountOnly = 0;
                var discountForInclusiveVat = 0;
                var root = this;
                const taxIdList = [...new Set(this.purchaseProducts.map(item => item.taxRateId))];
                root.paidVatList = []
                //'isDiscountOnTransaction', 'transactionLevelDiscount'
                taxIdList.forEach(function (taxId) {
                    vatRate = root.vats.find((value) => value.id == taxId);
                    var filteredRecord = root.purchaseProducts
                        .filter((x) => x.taxRateId === taxId);
                    var totalQtyWithotFree = root.purchaseProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.discount != 0 || x.discount != "" || x.offerQuantity != 0)
                        .reduce((discount, prod) =>
                            discount + (prod.totalPiece ? (prod.offerQuantity ? 0 : (((prod.totalPiece * prod.unitPrice) * prod.discount) / 100)) : 0), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "" || x.offerQuantity != 0)
                        .reduce((discount, prod) =>
                            discount + (prod.totalPiece ? (prod.offerQuantity ? 0 : (root.taxMethod == ("Inclusive" || "شامل") ? prod.fixDiscount + (prod.fixDiscount * vatRate.rate / 100) : prod.fixDiscount)) : 0), 0);

                    var paidVat = filteredRecord
                        .reduce((vat, prod) => (vat + ((prod.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.lineTotal) - (root.isDiscountBeforeVat ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : 0)) * vatRate.rate) / (100 + vatRate.rate) : ((parseFloat(prod.lineTotal) - (root.isDiscountBeforeVat && !root.isFixedDiscount && root.isDiscountOnTransaction ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : (root.isDiscountBeforeVat && root.isFixedDiscount && root.isDiscountOnTransaction ? (root.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.totalPiece) : 0))) * vatRate.rate) / 100)), 0).toFixed(3).slice(0, -1)
                    discountForInclusiveVat += parseFloat(filteredRecord
                        .reduce((vat, prod) => (vat + ((prod.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.lineTotal) * vatRate.rate) / (100 + vatRate.rate) : 0)), 0).toFixed(3).slice(0, -1))

                    root.paidVatList.push({
                        name: vatRate.name,
                        amount: paidVat
                    })

                });
                //root.transactionLevelDiscount = root.transactionLevelDiscount;
                // this.summary.discount = discountOnly
                this.summary.withDisc = this.summary.total;

                this.summary.vat = this.paidVatList.reduce((vat, paidVat) => (vat + parseFloat(paidVat.amount)), 0).toFixed(3).slice(0, -1);

                var exclusiveVat = this.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(this.summary.vat);
                this.transactionLevelTotalDiscount = ((this.isDiscountBeforeVat && this.isDiscountOnTransaction) ? (this.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc - discountForInclusiveVat) / 100) : (this.isFixedDiscount ? parseFloat(this.transactionLevelDiscount) : parseFloat(this.transactionLevelDiscount) * this.summary.withDisc / 100)) : (this.isFixedDiscount ? parseFloat(this.transactionLevelDiscount) : (parseFloat(this.summary.withDisc) + parseFloat(exclusiveVat)) * parseFloat(this.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                var totalIncDisc = (this.isDiscountBeforeVat && this.isDiscountOnTransaction && this.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc) / 100) : parseFloat(this.transactionLevelTotalDiscount)
                this.adjustment = (this.adjustment == '' || this.adjustment == null) ? 0 : parseFloat(this.adjustment)

                this.summary.withVat = (parseFloat(this.summary.withDisc) + parseFloat(exclusiveVat) + (this.adjustmentSign == '+' ? this.adjustment : (-1) * this.adjustment)).toFixed(3).slice(0, -1);

                this.summary.withVat = (parseFloat(this.summary.withVat) - totalIncDisc).toFixed(3).slice(0, -1);

                //calculate bundle Amount
                if (this.purchaseProducts.filter(x => x.isBundleOffer).length > 0) {

                    //get bundle get quantity
                    var bundle = {
                        item: 0,
                        qty: 0,
                        total: 0,
                        discount: 0,
                        withDisc: 0,
                        vat: 0,
                        withVat: 0,
                        quantityLimit: 0
                    };

                    var bundleProducts = this.purchaseProducts.filter(x => x.isBundleOffer != undefined && x.offerQuantity > 0);

                    bundle.total = bundleProducts.reduce((total, prod) =>
                        total + prod.offerQuantity * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                    //var bundleExclusiveTax = bundleProducts.reduce((total, prod) =>
                    //    total + (prod.taxMethod == "Exclusive" ? (bundle.total * prod.rate/100) : 0), 0);

                    var discountBundle = bundleProducts.filter((x) => x.discount != 0 || x.discount != "")
                        .reduce((discount, prod) =>
                            discount + (prod.offerQuantity * prod.unitPrice * prod.discount) / 100, 0);

                    var fixDiscountBundle = bundleProducts
                        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                        .reduce((discount, prod) => discount + prod.fixDiscount, 0);

                    bundle.discount = (parseFloat(discountBundle) + parseFloat(fixDiscountBundle)).toFixed(3).slice(0, -1);

                    bundle.withDisc = (bundle.total - bundle.discount).toFixed(3).slice(0, -1);

                    bundle.vat = bundleProducts
                        .reduce((vat, prod) => vat + (((prod.unitPrice * prod.offerQuantity) -
                            ((prod.unitPrice * prod.offerQuantity * prod.discount) / 100)) *
                            parseFloat(prod.rate)) / ((prod.taxMethod == "Exclusive" || prod.taxMethod == "غير شامل") ? 100 : prod.rate + 100), 0).toFixed(3).slice(0, -1);

                    this.summary.bundleAmount = (parseFloat(bundle.withDisc) + parseFloat(exclusiveVat)).toFixed(3).slice(0, -1);
                    this.summary.withVat = (this.summary.withVat - bundle.withDisc);

                } else {
                    this.summary.bundleAmount = 0;
                }
                if (this.isDiscountOnTransaction) {
                    this.summary.discount = totalIncDisc;
                }
                else {
                    this.summary.discount = discountOnly;
                }
                this.summary.totalAfterDiscount = this.isDiscountOnTransaction && !this.isDiscountBeforeVat ? parseFloat(this.summary.withVat - this.summary.discount).toFixed(3).slice(0, -1) :
                    parseFloat((this.summary.total - this.summary.discount).toFixed(3).slice(0, -1));

                this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

                this.$emit("summary", this.summary);
            },

            updateLineTotal: function (e, prop, product) {


                var discount = 0
                if (prop == "unitPrice") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = 0;
                    }
                    product.unitPrice = e;

                }

                if (prop == "quantity") {
                    if (e <= 0 || e == '') {
                        e = '';
                    }
                    if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                        e = parseFloat(String(e).slice(0, -1))
                    product.quantity = this.decimalQuantity ? e : Math.round(e);
                }

                if (prop == "highQty") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    product.highQty = Math.round(e);
                }

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

                if (product.highUnitPrice && product.isService == false) {
                    product.totalPiece = (parseFloat(product.highQty == '' ? 0 : product.highQty)) + (parseFloat(product.quantity == '' ? 0 : product.quantity) / (product.unitPerPack == null ? 0 : product.unitPerPack));

                }
                else {
                    product.totalPiece = (parseFloat(product.highQty == '' ? 0 : product.highQty) * (product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                }

                var vat = 0;

                vat = this.vats.find((value) => value.id == product.taxRateId);
                var total = product.totalPiece * product.unitPrice;

                discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                var calculateVAt = 0;
                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                    calculateVAt = product.discountSign == 'F' ? (((total * 100) / (100 + vat.rate)) - discount) * vat.rate / 100 : (total - discount) * vat.rate / (vat.rate + 100);

                } else {
                    calculateVAt = ((total - discount) * vat.rate) / 100;
                }

                product.lineTotal = product.totalPiece * product.unitPrice - discount;

                //discount = product.discount == 0 ? product.totalPiece * product.fixDiscount : (product.totalPiece * product.unitPrice * product.discount) / 100;

                if (this.purchase != undefined && !product.isService) {
                    if (this.purchase != undefined) {
                        if (product.totalPiece > product.remainingQuantity || product.totalPiece > product.inventory.currentQuantity) {
                            product['outOfStock'] = true;
                        } else {
                            product['outOfStock'] = false;
                        }
                    }
                }

                product.discountAmount = discount;
                product.vatAmount = calculateVAt;
                product.totalAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? product.lineTotal : product.lineTotal + product.vatAmount;
                product.grossAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? total * 100 / (100 + vat.rate) : total;
                
                this.purchaseProducts['product'] = product;

                this.calcuateSummary();
                this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

            },

            addProduct: function (productId, newProduct, tempItem, isTemp, poVatId, poTaxMethod) {
                

                if (productId == undefined) {
                    productId = '';
                    newProduct = '';
                }


                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined && !this.colorVariants) {
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                } else {

                    this.products.push(newProduct);
                    var prod = this.products.find((x) => x.id == productId);
                    var rate = 0;

                    if (isTemp) {
                        if (poVatId != "00000000-0000-0000-0000-000000000000" && poVatId != undefined) {
                            rate = this.getVatValue(poVatId, prod);
                        }
                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: tempItem.unitPrice,
                            description: tempItem.description,
                            unitName: tempItem.unitName,
                            quantity: tempItem.remainingQuantity,
                            receiveQuantity: 0,
                            highQty: tempItem.highQty,
                            totalPiece: 0,
                            remainingQuantity: 0,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.unit == null ? '' : prod.unit.name,
                            unitPerPack: tempItem.unitPerPack,
                            discount: tempItem.discount,
                            fixDiscount: tempItem.fixDiscount,
                            taxRateId: poVatId,
                            rate: rate,
                            taxMethod: poTaxMethod,
                            expiryDate: "",
                            isExpire: prod.isExpire,
                            batchNo: "",
                            lineTotal: 0,
                            guarantee: prod.guarantee,
                            serial: prod.serial,
                            isService: prod.serviceItem,
                            serialNo: '',
                            guaranteeDate: '',
                            inventory: prod.inventory != null ? prod.inventory.currentQuantity : 0,
                            highUnitPrice: prod.highUnitPrice,
                            discountSign: '%',
                        });
                    } else {
                        if (this.defaultVat == 'DefaultVat' || this.defaultVat == 'DefaultVatItem') {
                            rate = this.getVatValue(prod.taxRateId, prod);
                        }
                        if (this.defaultVat == 'DefaultVatHead' || this.defaultVat == 'DefaultVatHeadItem') {
                            if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {
                                rate = this.getVatValue(this.taxRateId, prod);
                            }
                        }


                        

                        var taxRateId = '';
                        var taxMethod = '';
                        if (this.defaultVat == 'DefaultVat' || this.defaultVat == 'DefaultVatItem') {
                            taxRateId = prod.taxRateId;
                            taxMethod = prod.taxMethod;
                        }
                        else if (this.defaultVat == 'DefaultVatHead' || this.defaultVat == 'DefaultVatHeadItem') {
                            taxRateId = this.taxRateId;
                            taxMethod = this.taxMethod;
                        }



                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            description: '',
                            unitName: '',
                            unitPrice: 0,
                            quantity: 0,
                            receiveQuantity: 0,
                            highQty: 0,
                            totalPiece: 0,
                            remainingQuantity: 0,
                            levelOneUnit: '',
                            basicUnit: '',
                            unitPerPack: 0,
                            discount: 0,
                            fixDiscount: 0,
                            taxRateId: taxRateId,
                            rate: rate,
                            taxMethod: taxMethod,
                            expiryDate: "",
                            isExpire: false,
                            isService: false,
                            batchNo: "",
                            lineTotal: 0,
                            guarantee: false,
                            serial: false,
                            serialNo: '',
                            guaranteeDate: '',
                            warrantyTypeId: '',
                            inventory: 0,

                            saleSizeAssortment: [],
                            productSizes: [],
                            colorId: '',
                            colorName: '',
                            highUnitPrice: 0,
                            discountSign: '%',
                        });

                        this.GetProductInfo(productId);
                    }

                    var product = this.purchaseProducts.find((x) => {
                        return x.productId == productId;
                    });

                    this.getVatValue(product.taxRateId, product);

                    this.product.id = "";
                    this.rendered++;
                }

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

                root.$https.get('/Product/GetProductDetailForInvoiceQuery?id=' + id + "&isFifo=" + root.isFifo + "&openBatch=" + openBatch + "&colorVariants=" + root.colorVariants + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var newProduct = response.data;
                        var prod = root.purchaseProducts.find((x) => x.productId == id);

                        if (prod != undefined) {
                            var sizeAssortmentList = [];
                            if (root.colorVariants) {
                                newProduct.saleSizeAssortment.forEach(function (item) {
                                    sizeAssortmentList.push({
                                        sizeId: item.sizeId,
                                        name: item.name,
                                        quantity: item.quantity,
                                    });
                                });
                            }


                            prod.unitPrice = newProduct.purchasePrices;
                            prod.levelOneUnit = newProduct.levelOneUnit;
                            prod.basicUnit = newProduct.basicUnit;
                            prod.unitPerPack = newProduct.unitPerPack;
                            prod.isExpire = newProduct.isExpire;
                            prod.isService = prod.serviceItem;
                            prod.guarantee = newProduct.guarantee;
                            prod.serial = newProduct.serial;
                            prod.inventory = newProduct.currentQuantity;
                            prod.productSizes = newProduct.productSizes;
                            prod.highUnitPrice = newProduct.highUnitPrice;
                            prod.saleSizeAssortment = sizeAssortmentList;
                            
                            root.updateLineTotal(prod.quantity, "quantity", prod);
                        }
                    }
                });
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

            getVatValueForSummary: function (id, prod) {

                var vat = this.vats.find((value) => value.id == id);
                prod.taxRateId = id;
                prod.rate = vat.rate;
                return vat.rate;
            },
            removeProduct: function (id) {

                this.purchaseProducts = this.purchaseProducts.filter((prod) => {
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
                        headers: {
                            Authorization: `Bearer ${token}`
                        },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {


                        //Purchase Order Edit or Supplier Quotation Edit
                        if (root.$route.query.data != undefined) {
                            if (root.$route.query.data.purchaseOrderItems != undefined) {

                                root.$route.query.data.purchaseOrderItems.forEach(function (item) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        description: item.description,
                                        unitName: item.unitName,
                                        isService: item.isService,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.productId == null ? false : item.product.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        levelOneUnit: item.productId == null ? '' : item.product.levelOneUnit,
                                        quantity: item.quantity,
                                        receiveQuantity: item.receiveQuantity,
                                        highQty: item.highQty,
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        serial: item.productId == null ? false : item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.productId == null ? false : item.product.guarantee,
                                        guaranteeDate: item.guaranteeDate,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        basicUnit: item.productId == null ? '' : item.product.basicUnit,
                                        warrantyTypeId: item.warrantyTypeId,
                                        totalPiece: 0,
                                        highUnitPrice: item.product == null ? 0 : item.product.highUnitPrice,
                                        discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',

                                    });
                                });

                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;
                                for (var k = 0; k < root.purchaseProducts.length; k++) {
                                    if (root.purchaseProducts[k].productId != null) {
                                        root.products.push(root.purchaseProducts[k].product);
                                    }

                                    root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                                    if (root.isMultiUnit) {
                                        root.updateLineTotal(root.purchaseProducts[k].highQty, "highQty", root.purchaseProducts[k]);
                                        root.updateLineTotal(root.purchaseProducts[k].unitPerPack, "unitPerPack", root.purchaseProducts[k]);
                                    }

                                    root.updateLineTotal(root.purchaseProducts[k].unitPrice, "unitPrice", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].discount, "discount", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].fixDiscount, "fixDiscount", root.purchaseProducts[k]);

                                }
                                root.calcuateSummary()
                            }
                            else if (root.$route.query.data.goodReceiveNoteItems != undefined) {
                                //Purchase Order Edit

                                root.$route.query.data.goodReceiveNoteItems.forEach(function (item) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        description: item.description,
                                        unitName: item.unitName,
                                        isService: item.isService,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.productId == null ? false : item.product.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        levelOneUnit: item.productId == null ? '' : item.product.levelOneUnit,
                                        quantity: item.quantity,
                                        receiveQuantity: item.receiveQuantity,
                                        highQty: item.highQty,
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        serial: item.productId == null ? false : item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.productId == null ? false : item.product.guarantee,
                                        guaranteeDate: item.guaranteeDate,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        basicUnit: item.productId == null ? '' : item.product.basicUnit,
                                        warrantyTypeId: item.warrantyTypeId,
                                        totalPiece: 0,
                                        highUnitPrice: item.product == null ? 0 : item.product.highUnitPrice,
                                        discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',

                                    });
                                });

                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;
                                for (var ko = 0; ko < root.purchaseProducts.length; ko++) {
                                    if (root.purchaseProducts[ko].productId != null) {
                                        root.products.push(root.purchaseProducts[ko].product);
                                    }

                                    root.updateLineTotal(root.purchaseProducts[ko].quantity, "quantity", root.purchaseProducts[ko]);
                                    if (root.isMultiUnit) {
                                        root.updateLineTotal(root.purchaseProducts[ko].highQty, "highQty", root.purchaseProducts[ko]);
                                        root.updateLineTotal(root.purchaseProducts[ko].unitPerPack, "unitPerPack", root.purchaseProducts[ko]);
                                    }

                                    root.updateLineTotal(root.purchaseProducts[ko].unitPrice, "unitPrice", root.purchaseProducts[ko]);
                                    root.updateLineTotal(root.purchaseProducts[ko].discount, "discount", root.purchaseProducts[ko]);
                                    root.updateLineTotal(root.purchaseProducts[ko].fixDiscount, "fixDiscount", root.purchaseProducts[ko]);

                                }
                                root.calcuateSummary()
                            }

                            else if (root.$route.query.data.purchasePostItems != undefined) {

                                root.purchaseProducts = root.$route.query.data.purchasePostItems;
                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;

                                for (var i = 0; i < root.purchaseProducts.length; i++) {
                                    root.products.push(root.purchaseProducts[i].product);
                                    root.updateLineTotal(root.purchaseProducts[i].quantity, "quantity", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].unitPrice, "unitPrice", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].discount, "discount", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].fixDiscount, "fixDiscount", root.purchaseProducts[i]);

                                }
                                root.calcuateSummary()
                            }
                        }



                        else if (root.purchaseItems != undefined && root.purchaseItems.length > 0) {
                            root.purchaseItems.forEach(function (item) {
                                if (item.isService == true) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        batchNo: item.batchNo,
                                        description: item.description,
                                        unitName: item.unitName,
                                        isService: item.isService,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.productId == null ? false : item.product.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        quantity: item.quantity,
                                        highQty: item.highQty,
                                        remainingQuantity: item.remainingQuantity,
                                        inventory: item.productId == null ? null : item.product.inventory,
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        levelOneUnit: item.productId == null ? '' : item.product.levelOneUnit,
                                        basicUnit: item.productId == null ? '' : item.product.basicUnit,
                                        serial: item.productId == null ? false : item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.productId == null ? false : item.product.guarantee,
                                        guaranteeDate: item.guaranteeDate,
                                        warrantyTypeId: item.warrantyTypeId,
                                        totalPiece: 0,
                                        highUnitPrice: item.product == null ? 0 : item.product.highUnitPrice,
                                        discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
                                    });
                                }
                            });

                            root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                            root.adjustmentSign = root.adjustmentSignProp;
                            for (let j = 0; j < root.purchaseProducts.length; j++) {
                                if (root.purchaseProducts[j].productId) {
                                    root.products.push(root.purchaseProducts[j].product);
                                }
                                root.updateLineTotal(root.purchaseProducts[j].quantity, "quantity", root.purchaseProducts[j]);

                                if (root.isMultiUnit) {
                                    root.updateLineTotal(root.purchaseProducts[j].highQty, "highQty", root.purchaseProducts[j]);
                                    root.updateLineTotal(root.purchaseProducts[j].unitPerPack, "unitPerPack", root.purchaseProducts[j]);
                                }
                                root.updateLineTotal(root.purchaseProducts[j].unitPrice, "unitPrice", root.purchaseProducts[j]);
                                root.updateLineTotal(root.purchaseProducts[j].discount, "discount", root.purchaseProducts[j]);
                                root.updateLineTotal(root.purchaseProducts[j].fixDiscount, "fixDiscount", root.purchaseProducts[j]);

                            }

                            root.calcuateSummary()
                        }

                        else if (root.purchase != undefined) {

                            if (root.purchase.purchasePostItems != undefined) {
                                //Purchase Return Edit
                                root.purchase.purchasePostItems.forEach(function (item) {
                                    if (item.remainingQuantity > 0) {
                                        root.purchaseProducts.push({
                                            rowId: item.id,
                                            id: item.id,
                                            batchNo: item.batchNo,
                                            description: item.description,
                                            unitName: item.unitName,
                                            isService: item.isService,
                                            discount: item.discount,
                                            expiryDate: item.expiryDate,
                                            isExpire: item.productId == null ? false : item.product.isExpire,
                                            fixDiscount: item.fixDiscount,
                                            product: item.product,
                                            productId: item.productId,
                                            purchaseId: item.purchaseId,
                                            quantity: item.quantity,
                                            highQty: item.highQty,
                                            remainingQuantity: item.remainingQuantity,
                                            inventory: item.productId == null ? null : item.product.inventory,
                                            unitPerPack: item.unitPerPack,
                                            taxMethod: item.taxMethod,
                                            taxRateId: item.taxRateId,
                                            unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                            levelOneUnit: item.productId == null ? '' : item.product.levelOneUnit,
                                            basicUnit: item.productId == null ? '' : item.product.basicUnit,
                                            serial: item.productId == null ? false : item.product.serial,
                                            serialNo: item.serialNo,
                                            guarantee: item.productId == null ? false : item.product.guarantee,
                                            guaranteeDate: item.guaranteeDate,
                                            warrantyTypeId: item.warrantyTypeId,
                                            totalPiece: 0,
                                            highUnitPrice: item.product == null ? 0 : item.product.highUnitPrice,
                                            discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
                                        });
                                    }
                                });

                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;
                                for (var j = 0; j < root.purchaseProducts.length; j++) {
                                    if (root.purchaseProducts[j].productId) {
                                        root.products.push(root.purchaseProducts[j].product);
                                    }
                                    root.updateLineTotal(root.purchaseProducts[j].quantity, "quantity", root.purchaseProducts[j]);

                                    if (root.isMultiUnit) {
                                        root.updateLineTotal(root.purchaseProducts[j].highQty, "highQty", root.purchaseProducts[j]);
                                        root.updateLineTotal(root.purchaseProducts[j].unitPerPack, "unitPerPack", root.purchaseProducts[j]);
                                    }
                                    root.updateLineTotal(root.purchaseProducts[j].unitPrice, "unitPrice", root.purchaseProducts[j]);
                                    root.updateLineTotal(root.purchaseProducts[j].discount, "discount", root.purchaseProducts[j]);
                                    root.updateLineTotal(root.purchaseProducts[j].fixDiscount, "fixDiscount", root.purchaseProducts[j]);

                                }

                                root.calcuateSummary()
                            }
                        }

                    });
            },
            getTotalAmount: function () {
                return this.summary.withVat;
            },
            clearList: function () {
                this.purchaseProducts = [];
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

            GetColorData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/ColorList?isActive=true' + '&isVariance=true', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.colorList = response.data.results.colors;
                    }
                });
            },
        },

        created: function () {
            this.isForRaw = this.isForRaw1;
         

            this.isDiscountBeforeVat = this.isBeforeTax == true ? true : false;
            this.isFixedDiscount = this.isFixed == true ? true : false;
            this.isService = localStorage.getItem('ServicePurchase') == 'true' ? true : false;

            this.transactionLevelDiscount = this.transactionLevelDiscountProp;
            if (this.$i18n.locale == 'en') {
                this.options = ['Inclusive', 'Exclusive'];
            } else {
                this.options = ['شامل', 'غير شامل'];
            }
            this.defaultVat = localStorage.getItem('DefaultVat');

            //this.$barcodeScanner.init(this.onBarcodeScanned);
            //For Scanner Code
            var root = this;
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
            localStorage.setItem("BarcodeScan", 'Purchase')
            this.servicePurchase = localStorage.getItem('ServicePurchase') == 'true' ? true : false;
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            //this.$barcodeScanner.init(this.onBarcodeScanned);
            this.getData();

            if (localStorage.getItem('ColorVariants') == 'true') {
                this.GetSizeData();
                this.GetColorData();
            }
        },

        mounted: function () {
            this.isDiscountBeforeVat = this.isBeforeTax == true ? true : false;
            this.isFixedDiscount = this.isFixed == true ? true : false;
            this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.internationalPurchase = localStorage.getItem('InternationalPurchase');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;

            //this.GetProductList();
                this.currency = localStorage.getItem('currency');
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            
        },
    };
</script>
