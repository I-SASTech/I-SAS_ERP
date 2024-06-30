<template>
    <div class="row" v-if="isValid('CanAddPurchaseReturn')">
        <div class="col-md-12 ">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;">PIR</span>
                            <div class="media-body align-self-center ms-3">
                                <h6 class="m-0 font-20">
                                    {{ $t('PurchaseReturn.PurchaseReturn') }} <span class="mx-2"
                                                                                    style="font-size: 13px !important;">{{ purchase.date }}</span>
                                </h6>
                                <div class="col d-flex ">
                                    <p class="text-muted mb-0" style="font-size:13px !important;">
                                        {{ registrationNo }}
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
                        <div class="row form-group" v-bind:key="supplierRender">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline ">
                                    {{
                                        $t('AddPurchaseOrder.Supplier')
                                    }} : <span class="text-danger">*</span>
                                </span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <supplierdropdown v-model="v$.purchase.supplierId.$model"
                                                  :disable="purchase.approvalStatus === 5 && purchase.id != '00000000-0000-0000-0000-000000000000'"
                                                  :modelValue="purchase.supplierId" :status="purchase.isRaw" />
                                <a v-if="purchase.supplierId != null && purchase.supplierId != '' " v-on:click="GetSupplierDetails()" href="javascript:void(0);" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight" class="text-primary mt-2">{{ $t('AddSale.ViewCustomerDetails') }}</a>
                                <a v-else href="javascript:void(0);" class="text-secondary mt-2">
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
                                                <textarea rows="3" v-model="sale.billingAddress" class="form-control"> </textarea>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-lg-6">
                        <a v-if="purchase.supplierId != null && purchase.supplierId != ''" href="javascript:void(0);"
                           data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight"
                           class="text-primary">Options</a>
                        <a v-else href="javascript:void(0);" class="text-secondary">Options</a>

                        <div class="row" v-bind:key="randerEffect">
                            <div class="col-md-12" v-if="selectedValue1 != '' && selectedValue1 != null"
                                 :key="canvasSelectValueRender">
                                <div class="badge bg-success" style="position: relative;font-size: 13px !important;">
                                    <span>{{ selectedValue }}</span>
                                    <span style="position:absolute; right: -12px; top: -8px;">
                                        <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect('RemoveItems')">
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

                        </div>

                        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight2"
                             aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                            <div class="offcanvas-header">
                                <h5 id="offcanvasRightLabel" class="m-0">Options</h5>
                                <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'"
                                        type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas"
                                        aria-label="Close"></button>
                            </div>
                            <div class="offcanvas-body">
                                <div class="row">
                                    <div class="col-md-12 mb-2" v-if="selectedValue != '' && selectedValue != null"
                                         :key="canvasSelectValueRender">
                                        <div class="badge bg-success"
                                             style="position: relative;font-size: 13px !important;">
                                            <span>{{ selectedValue }}</span>
                                            <span style="position:absolute; right: -12px; top: -8px;">
                                                <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                        style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                        @click="RemoveEffect('CanvasSelectValue')">
                                                    <i class="fas fa-times"></i>
                                                </button>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-7">
                                        <div class="row">
                                            <div class="col-lg-6 form-group text-right">
                                                <b>{{ $t('Purchase.PurchaseInvoice') }} </b>
                                            </div>
                                            <div class="col-lg-6 form-group text-left">
                                                <button v-if="expandPurchaseInvoice"
                                                        v-on:click="ExpandPurchaseInvoice(false)" type="button"
                                                        class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                    <i class="ti-angle-double-up"></i>
                                                </button>
                                                <button v-else v-on:click="ExpandPurchaseInvoice(true)" type="button"
                                                        class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                                    <i class="ti-angle-double-down"></i>
                                                </button>
                                            </div>
                                            <div v-if="expandPurchaseInvoice" class="col-lg-12 form-group">
                                                <p v-for="(saleValue, index) in purchaseInvoiceList" v-bind:key="index"
                                                   style="border-bottom: 1px solid #cbcbcb; ">
                                                    <a href="javascript:void(0);"
                                                       v-on:click="GetPIId(saleValue.id, saleValue.registrationNumber, saleValue.date, saleValue.netAmount)">
                                                        <span>
                                                            {{ index + 1 }}-{{ saleValue.registrationNumber }}--{{
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
                                                    v-bind:disabled="(purchaseInoiceId == '')"
                                                    v-on:click="GetData(purchaseInoiceId)">
                                                <i class="far fa-save "></i>
                                                {{ $t('AddCustomer.btnSave') }}
                                            </button>
                                            <button type="button" v-on:click="RemoveEffect('purchaseInoiceId')"
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

                                                        <div class="col-lg-12 form-group">
                                                            <label class="col-form-label">
                                                                <span class="tooltip-container text-dashed-underline ">
                                                                    {{ $t('AddPurchaseReturn.WareHouse') }} :<span class="text-danger"> *</span>
                                                                </span>
                                                            </label>
                                                            <div class="inline-fields">
                                                                <warehouse-dropdown v-bind:key="rander"
                                                                                    :modelValue="additionalOptions.wareHouseId"
                                                                                    v-model="v$.additionalOptions.wareHouseId.$model" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12 form-group" v-if="raw == 'true'">
                                                            <label class="col-form-label">
                                                            </label>
                                                            <div class="inline-fields">
                                                                <div class="checkbox form-check-inline mx-2">
                                                                    <input type="checkbox" id="inlineCheckbox1"
                                                                           v-model="additionalOptions.isRaw"
                                                                           @change="ChangeSupplier">
                                                                    <label for="inlineCheckbox1">
                                                                        {{
                                                                        $t('AddPurchaseReturn.RawProduct')
                                                                        }}
                                                                    </label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12 text-end mt-2 mb-2">
                                                            <div class="button-items">
                                                                <button class="btn btn-outline-primary"
                                                                        v-bind:disabled="(additionalOptions.wareHouseId == '') && !additionalOptions.isRaw"
                                                                        v-on:click="SaveCanvasData('Additional')">
                                                                    <i class="far fa-save "></i>
                                                                    {{ $t('AddCustomer.btnSave') }}
                                                                </button>
                                                                <button type="button"
                                                                        v-on:click="RemoveCanvasData('Additional')"
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

                                        <div class="col-md-12 text-end mt-2 mb-4">
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

                    <purchase-item @update:modelValue="SavePurchaseItems" :taxMethod="purchase.taxMethod" @summary="updateSummary"
                                   :taxRateId="purchase.taxRateId" :purchase="purchase" :raw="purchase.isRaw" :key="rander"
                                   @discountChanging="updateDiscountChanging" :adjustmentProp="purchase.discount"
                                   :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction"
                                   :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed"
                                   :isBeforeTax="purchase.isBeforeTax" />

                    <div class="col-lg-12 invoice-btn-fixed-bottom">
                        <div v-if="!loading && purchase.id == '00000000-0000-0000-0000-000000000000'"
                             class="col-md-12 arabicLanguage">

                            <button class="btn btn-outline-primary me-2" v-if="isValid('CanAddPurchaseReturn')"
                                    :disabled="v$.purchase.$invalid || purchase.purchasePostItems.filter(x => x.outOfStock).length > 0 || purchase.purchasePostItems.filter(x => x.totalPiece == '').length > 0 || purchase.purchasePostItems.filter(x => x.unitPrice == '').length > 0 || (isFifo ? (purchase.purchasePostItems.filter(x => x.expiryDate == '').length > 0 || purchase.purchasePostItems.filter(x => x.batchNo == '').length > 0) : false)"
                                    v-on:click="savePurchase">
                                {{ $t('AddPurchaseReturn.Save') }}
                            </button>
                            <button class="btn btn-danger me-2" v-on:click="goToPurchase">
                                {{ $t('AddPurchaseReturn.Cancel') }}
                            </button>
                        </div>
                    </div>
                </div>
                <purchaseReturnHistorymodel :newpurchaseReturn="purchaseReturn" :show="show" v-if="show"
                                            @close="show = false" />
                <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true">
                </loading>
            </div>

        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    import moment from "moment";

    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    //import Multiselect from 'vue-multiselect'
    //import Vue3Barcode from 'vue3-barcode'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                sale: {},
                selectedValue: '',
                selectedValue1: '',
                canvasSelectValueRender: 0,
                purchaseInoiceId: '',
                expandPurchaseInvoice: false,
                purchaseInvoiceList: [],
                randerEffect: 0,
                additionalOptions: {
                    isRaw: false,
                    wareHouseId: "",
                },

                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',

                rendered: 0,
                registrationNo: "",
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    supplierId: "",
                    invoiceNo: "",
                    invoiceDate: "",
                    purchaseOrder: "",
                    wareHouseId: "",
                    isRaw: false,
                    isPurchaseReturn: true,
                    purchasePostItems: [],
                    purchaseInvoiceId: "",

                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0,

                    grossAmount: 0,
                    vatAmount: 0,
                    discountAmount: 0,
                    totalAmount: 0,
                    branchId: '',
                },
                purchaseReturn: [],
                show: false,
                raw: '',
                rander: 0,
                counter: 0,
                currency: '',
                loading: false,
                disable: false,
                isFifo: false,
                language: 'Nothing',
                supplierRender: 0,
                options: [],
                purchaseInvoiceRender: 0
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    registrationNo: { required },
                    supplierId: { required },
                    invoiceNo: {},
                    invoiceDate: {},
                    wareHouseId: {},
                },
                additionalOptions: {
                    wareHouseId: {},
                }
            }
        },
        methods: {
            GetSupplierDetails: function () {
                if (this.purchase.supplierId != null && this.purchase.supplierId != '') {
                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    root.$https.get('/Contact/ContactDetail?id=' + this.purchase.supplierId + '&multipleAddress= true', { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            root.sale = response.data;
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
            GetPIId: function (id, registrationNumber, date, netAmount) {
                this.purchaseInoiceId = id;
                this.selectedValue = registrationNumber + ' - ' + date + ' - ' + netAmount;
                this.canvasSelectValueRender++;
            },
            ExpandPurchaseInvoice: function (val) {
                this.expandPurchaseInvoice = val;

                if (val) {
                    this.GetPIList();
                }
            },
            GetPIList: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                if (this.purchase.supplierId != null && this.purchase.supplierId != '') {
                    root.$https.get('/PurchasePost/PurchasePostList?SupplierId=' + this.purchase.supplierId + '&isDropdown=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            root.purchaseInvoiceList = response.data.results;
                        });
                }

            },
            RemoveEffect: function (value) {
                if (value == 'wareHouseId') {
                    this.purchase.wareHouseId = '';
                    this.additionalOptions.wareHouseId = '';
                }
                if (value == 'isRaw') {
                    this.purchase.isRaw = false;
                    this.additionalOptions.isRaw = false;
                }
                if (value == 'CanvasSelectValue') {
                    this.selectedValue = '';
                    this.canvasSelectValueRender++;
                }
                if (value == 'purchaseInoiceId') {
                    this.selectedValue = '';
                    this.canvasSelectValueRender++;
                }
                if (value == 'RemoveItems') {
                    this.purchase.purchaseInvoiceId = '';
                    this.purchase.purchasePostItems = [];
                    this.purchase.id = '00000000-0000-0000-0000-000000000000';
                    this.purchase.date = moment().format("LLL");

                    this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'

                    this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'
                    this.selectedValue1 = '',
                        this.rander++;
                }

            },
            SaveCanvasData: function (value) {
                if (value == 'Additional') {

                    this.purchase.wareHouseId = this.additionalOptions.wareHouseId;
                    this.purchase.isRaw = this.additionalOptions.isRaw;
                    this.randerEffect++;
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
            },
            RemoveCanvasData: function (value) {
                if (value == 'Additional') {
                    this.purchase.wareHouseId = '';
                    this.additionalOptions.wareHouseId = '';
                    this.purchase.isRaw = false;
                    this.additionalOptions.isRaw = false;
                }
            },

            updateSummary: function (summary) {
                this.purchase.grossAmount = summary.total;
                this.purchase.vatAmount = summary.vat;
                this.purchase.discountAmount = summary.discount;
                this.purchase.totalAmount = summary.withVat;

            },

            updateDiscountChanging: function (isFixed, isBeforeTax) {
                this.purchase.isFixed = isFixed
                this.purchase.isBeforeTax = isBeforeTax
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            ViewPurchaseHistory: function (id) {

                this.show = !this.show;

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;

                root.$https.get('/PurchasePost/PurchaseReturnHistory?id=' + id + '&isReturnView=' + true + '&isMultiUnit=' + isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.purchaseReturn = response.data.purchaseReturnListHistory;

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            LoadSupplierInvoice: function () {
                this.purchaseInvoiceRender++;
            },
            ChangeSupplier: function () {
                this.supplierRender++;
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

            GetData: function (id, value) {
                if (this.selectedValue != '' && this.selectedValue != null) {
                    this.selectedValue1 = this.selectedValue;
                    this.canvasSelectValueRender++;
                }
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
                root.$https.get('/PurchasePost/PurchasePostDetail?id=' + id + '&isMultiUnit=' + isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.purchase = response.data;
                        root.purchase.purchaseInvoiceId = response.data.id;
                        root.purchase.purchasePostItems = response.data.purchasePostItems;
                        root.purchase.id = '00000000-0000-0000-0000-000000000000';
                        root.purchase.date = moment().format("LLL");

                        root.discountTypeOption = root.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'

                        root.adjustmentSignProp = root.purchase.discount >= 0 ? '+' : '-'

                        root.rander++;
                        root.purchaseInoiceId = '';
                        root.expandPurchaseInvoice = false;
                        root.CheckPurchaseReturnCounter(id);
                        if (value != false) {

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

                });
            },
            CheckPurchaseReturnCounter: function (id) {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.disable = false;
                root.$https
                    .get('/PurchasePost/PurchaseReturnHistoryCounter?id=' + id, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {

                        if (response.data != 0) {
                            root.counter = response.data;
                            root.disable = true;
                        }
                    });
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/PurchasePost/PurchaseReturnAutoGenerateNo?terminalId=" + localStorage.getItem('TerminalId') + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&userID=' + localStorage.getItem('UserID') + '&branchId=' + localStorage.getItem('BranchId'), {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.registrationNo = response.data.purchaseReturn;
                        }
                    });
            },
            SavePurchaseItems: function (purchaseOrderItems) {
                this.purchase.purchasePostItems = purchaseOrderItems;
            },
            savePurchase: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.purchase.branchId = localStorage.getItem('BranchId');

                this.purchase.isPurchaseReturn = true;
                this.purchase.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
                this.$https
                    .post('/PurchasePost/SavePurchasePostInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
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

                            }).then(function (ok) {
                                if (ok != null) {
                                    root.$router.push({
                                        path: '/PurchaseReturn',
                                        query: {
                                            data: 'PurchaseReturns'
                                        }
                                    })
                                }
                            });
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
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

                            }).then(function (ok) {
                                if (ok != null) {
                                    root.$router.push({
                                        path: '/PurchaseReturn',
                                        query: {
                                            data: 'PurchaseReturns'
                                        }
                                    })
                                }
                            });
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
                    .catch(error => {

                        root.$swal.fire(
                            {
                                type: 'error',
                                icon: 'error',
                                title: root.$t('AddPurchaseReturn.Error'),
                                text: error.response.data,
                                confirmButtonClass: "btn btn-danger",
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },

            goToPurchase: function () {
                if (this.isValid('CanViewPurchaseReturn')) {
                    this.$router.push({
                        path: '/PurchaseReturn',
                        query: {
                            data: 'PurchaseReturns'
                        }
                    })
                }
                else {
                    this.$router.go();
                }


            },
        },
        created: function () {

            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }


            if (this.$route.query.data != undefined) {

                this.purchase.clone = this.$route.query.clone == 'true' ? true : false;
                this.purchase.supplierId = this.$route.query.data.supplierId;
                this.purchase.isConversion = this.$route.query.isConversion == 'true' ? true : false;

                if (this.purchase.isConversion == true) {
                    this.GetData(this.$route.query.data.id, false);
                    this.selectedValue = this.$route.query.data.registrationNo + " " + moment(this.$route.query.data.date).format("LLL") + " " + this.$route.query.data.netAmount;
                    this.selectedValue1 = this.$route.query.data.registrationNo + " " + moment(this.$route.query.data.date).format("LLL") + " " + this.$route.query.data.netAmount;
                    this.purchase.id = '00000000-0000-0000-0000-000000000000';

                    this.$route.query.data = undefined;


                }






            }




            this.$emit('update:modelValue', this.$route.name);
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
        },
        mounted: function () {

            this.language = this.$i18n.locale;
            //this.options = ['Inclusive', 'Exclusive'];
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }
            this.raw = localStorage.getItem('IsProduction');

            this.purchase.invoiceDate = moment().format("DD MM YYYY");
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
            }
            // if (this.$route.query.data != undefined) {
            //     this.warehouse = this.$route.query.data;
            // }
            this.purchase.date = moment().format("LLL");
            this.currency = localStorage.getItem('Currency');
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

