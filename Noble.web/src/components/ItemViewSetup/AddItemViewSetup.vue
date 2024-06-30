<template>
    <div class="row">
        <div class="col-lg-12 px-3">
            <div class="row">
                <div class="col-sm-6">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;">IV</span>
                            <div class="media-body align-self-center ms-3">
                                <h6 class="m-0 font-20">{{ $t('Item View Setup') }} </h6>
                                <div class="col d-flex ">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <hr class="hr-dashed hr-menu mt-0" />
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'DisplayName' }"
                       v-on:click="makeActive('DisplayName')" data-bs-toggle="tab" href="#home" role="tab"
                       aria-selected="true">
                        {{ $t('Item Display Name') }}
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'ItemList' }" data-bs-toggle="tab" href="#profile"
                       role="tab" aria-selected="false" v-on:click="makeActive('ItemList')">
                        {{ $t('Item List View') }}
                    </a>
                </li>

            </ul>
            <div class="tab-content">
                <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'DisplayName' }">
                    <div class="row ms-5">
                        <div class="col-lg-6">
                            <h5 class="fw-bold">Invoice Display Name</h5>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isCode" v-on:change="GetItemDisplayValue('Item Code')"
                                               type="checkbox" id="inlineCheckbox12001">
                                        <label for="inlineCheckbox12001">{{ $t('Item Code') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isNameEn"
                                               v-on:change="GetItemDisplayValue('Item Name (English)')" type="checkbox"
                                               id="inlineCheckbox12002">
                                        <label for="inlineCheckbox12002">{{ $t('Item Name (English)') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isNameAr"
                                               v-on:change="GetItemDisplayValue('Item Name (Arabic)')" type="checkbox"
                                               id="inlineCheckbox12003">
                                        <label for="inlineCheckbox12003">{{ $t('Item Name (Arabic)') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isDescription"
                                               v-on:change="GetItemDisplayValue('Item Description')" type="checkbox"
                                               id="inlineCheckbox12004">
                                        <label for="inlineCheckbox12004">{{ $t('Item Description') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isCategory"
                                               v-on:change="GetItemDisplayValue('Item Category')" type="checkbox"
                                               id="inlineCheckbox12005">
                                        <label for="inlineCheckbox12005">{{ $t('Item Category') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isBarcode"
                                               v-on:change="GetItemDisplayValue('Item Barcode')" type="checkbox"
                                               id="inlineCheckbox12006">
                                        <label for="inlineCheckbox12006">{{ $t('Item Barcode') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isStyle"
                                               v-on:change="GetItemDisplayValue('Item Style/Model')" type="checkbox"
                                               id="inlineCheckbox12007">
                                        <label for="inlineCheckbox12007">{{ $t('Item Style/Model') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.brand" v-on:change="GetItemDisplayValue('Item Brand')"
                                               type="checkbox" id="inlineCheckbox120070021211">
                                        <label for="inlineCheckbox120070021211">{{ $t('Item Brand') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.origin"
                                               v-on:change="GetItemDisplayValue('Item Origin')" type="checkbox"
                                               id="inlineCheckbox120070031212">
                                        <label for="inlineCheckbox120070031212">{{ $t('Item Origin') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.size" v-on:change="GetItemDisplayValue('Item Size')"
                                               type="checkbox" id="inlineCheckbox1200700041213">
                                        <label for="inlineCheckbox1200700041213">{{ $t('Item Size') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.color" v-on:change="GetItemDisplayValue('Item Color')"
                                               type="checkbox" id="inlineCheckbox12007000061214">
                                        <label for="inlineCheckbox12007000061214">{{ $t('Item Color') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.unit" v-on:change="GetItemDisplayValue('Item Unit')"
                                               type="checkbox" id="inlineCheckbox120070251215">
                                        <label for="inlineCheckbox120070251215">{{ $t('Item Unit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <h5 class="fw-bold">Invoice Print Display Name</h5>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isCode"
                                               v-on:change="GetItemDisplayValueForPrint('Item Code')" type="checkbox"
                                               id="inlineCheckbox120011">
                                        <label for="inlineCheckbox120011">{{ $t('Item Code') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isNameEn"
                                               v-on:change="GetItemDisplayValueForPrint('Item Name (English)')" type="checkbox"
                                               id="inlineCheckbox120022">
                                        <label for="inlineCheckbox120022">{{ $t('Item Name (English)') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isNameAr"
                                               v-on:change="GetItemDisplayValueForPrint('Item Name (Arabic)')" type="checkbox"
                                               id="inlineCheckbox120033">
                                        <label for="inlineCheckbox120033">{{ $t('Item Name (Arabic)') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isDescription"
                                               v-on:change="GetItemDisplayValueForPrint('Item Description')" type="checkbox"
                                               id="inlineCheckbox120044">
                                        <label for="inlineCheckbox120044">{{ $t('Item Description') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isCategory"
                                               v-on:change="GetItemDisplayValueForPrint('Item Category')" type="checkbox"
                                               id="inlineCheckbox120055">
                                        <label for="inlineCheckbox120055">{{ $t('Item Category') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isBarcode"
                                               v-on:change="GetItemDisplayValueForPrint('Item Barcode')" type="checkbox"
                                               id="inlineCheckbox120066">
                                        <label for="inlineCheckbox120066">{{ $t('Item Barcode') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.isStyle"
                                               v-on:change="GetItemDisplayValueForPrint('Item Style/Model')" type="checkbox"
                                               id="inlineCheckbox120077">
                                        <label for="inlineCheckbox120077">{{ $t('Item Style/Model') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.brand"
                                               v-on:change="GetItemDisplayValueForPrint('Item Brand')" type="checkbox"
                                               id="inlineCheckbox1200700212111">
                                        <label for="inlineCheckbox1200700212111">{{ $t('Item Brand') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.origin"
                                               v-on:change="GetItemDisplayValueForPrint('Item Origin')" type="checkbox"
                                               id="inlineCheckbox1200700312122">
                                        <label for="inlineCheckbox1200700312122">{{ $t('Item Origin') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.size"
                                               v-on:change="GetItemDisplayValueForPrint('Item Size')" type="checkbox"
                                               id="inlineCheckbox12007000412133">
                                        <label for="inlineCheckbox12007000412133">{{ $t('Item Size') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.color"
                                               v-on:change="GetItemDisplayValueForPrint('Item Color')" type="checkbox"
                                               id="inlineCheckbox120070000612144">
                                        <label for="inlineCheckbox120070000612144">{{ $t('Item Color') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetupPrint.unit"
                                               v-on:change="GetItemDisplayValueForPrint('Item Unit')" type="checkbox"
                                               id="inlineCheckbox1200702512155">
                                        <label for="inlineCheckbox1200702512155">{{ $t('Item Unit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr class="hr-dashed hr-menu mt-0" />


                    <div class="row mb-5 ms-4 ">
                        <div class="col-md-6">
                            <h5 class="fw-bold">Item Display Order</h5>
                            <p><b> Please Drag and Drop Items to Update the Item Display Name.</b></p>
                            <h6>Item Display Name will be</h6>
                            <h6><b>{{ displayName }}</b></h6>
                            <draggable class="col-sm-12 border  py-2" v-model="resultData" style="margin: unset;">
                                <div class="row align-items-baseline p-0 m-0 mb-1" style="background:#f0f8ff"
                                     v-for="(module) in resultData" v-bind:key="module.name">
                                    <div>
                                        <h5>
                                            <a href="javascript:void(0)">
                                                {{ module.name }}
                                            </a>
                                        </h5>
                                    </div>
                                </div>
                            </draggable>
                        </div>
                        <div class="col-md-6">
                            <h5 class="fw-bold">Item Printing Display Order</h5>
                            <p><b> Please Drag and Drop Items to Update the Item Display Name for Print.</b></p>
                            <h6>Item Display Name will be</h6>
                            <h6><b>{{ displayNameForPrint }}</b></h6>
                            <draggable class="col-sm-12 border  py-2" v-model="resultData1" style="margin: unset;">
                                <div class="row align-items-baseline p-0 m-0 mb-1" style="background:#f0f8ff"
                                     v-for="(module) in resultData1" v-bind:key="module.name">
                                    <div>
                                        <h5>
                                            <a href="javascript:void(0)">
                                                {{ module.name }}
                                            </a>
                                        </h5>
                                    </div>
                                </div>
                            </draggable>
                        </div>
                    </div>
                    <div class="form-group col-md-6 mb-5">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox7" v-model="isForAllProduct">
                            <label for="inlineCheckbox7">
                                {{ $t('Do you want to apply this setup on all Products') }}
                            </label>
                        </div>
                    </div>
                </div>
                <div class="tab-pane pt-3" id="profile" role="tabpanel" v-bind:class="{ active: active == 'ItemList' }">
                    <div class="col-lg-12">
                        <h5 class="fw-bold">Item List View</h5>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.code" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Code')" type="checkbox"
                                           id="inlineCheckbox1200101">
                                    <label for="inlineCheckbox1200101">{{ $t('Item Code') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.itemName" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Name')" type="checkbox"
                                           id="inlineCheckbox1200202">
                                    <label for="inlineCheckbox1200202">{{ $t('Item Name') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.salePrice" :key="render"
                                           v-on:change="GetItemListOrderValue('Sale Price')" type="checkbox"
                                           id="inlineCheckbox1200303">
                                    <label for="inlineCheckbox1200303">{{ $t('Sale Price') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.purchasePrice" :key="render"
                                           v-on:change="GetItemListOrderValue('Purchase Price')" type="checkbox"
                                           id="inlineCheckbox1200404">
                                    <label for="inlineCheckbox1200404">{{ $t('Purchase Price') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.barCodeType" :key="render"
                                           v-on:change="GetItemListOrderValue('BarCode Type')" type="checkbox"
                                           id="inlineCheckbox1200505">
                                    <label for="inlineCheckbox1200505">{{ $t('BarCode Type') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.currentQuantity" :key="render"
                                           v-on:change="GetItemListOrderValue('Current Quantity')" type="checkbox"
                                           id="inlineCheckbox1200606">
                                    <label for="inlineCheckbox1200606">{{ $t('Current Quantity') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.image" :key="render"
                                           v-on:change="GetItemListOrderValue('Image')" type="checkbox"
                                           id="inlineCheckbox1200707">
                                    <label for="inlineCheckbox1200707">{{ $t('Image') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.styleNumber" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Style/Model')" type="checkbox"
                                           id="inlineCheckbox1200709">
                                    <label for="inlineCheckbox1200709">{{ $t('Item Style/Model') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.sku" :key="render"
                                           v-on:change="GetItemListOrderValue('SKU')" type="checkbox"
                                           id="inlineCheckbox1200708">
                                    <label for="inlineCheckbox1200708">{{ $t('SKU') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.partNumber" :key="render"
                                           v-on:change="GetItemListOrderValue('Part Number')" type="checkbox"
                                           id="inlineCheckbox12007001">
                                    <label for="inlineCheckbox12007001">{{ $t('Part Number') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.category" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Category')" type="checkbox"
                                           id="inlineCheckbox1200700200302">
                                    <label for="inlineCheckbox1200700200302">{{ $t('Item Category') }}</label>
                                </div>
                            </div>
                            <!-- <div class="col-md-3">
                            <div class="checkbox form-check-inline mx-2 mt-2">
                                <input v-model="itemListView.subCategory" :key="render"
                                    v-on:change="GetItemListOrderValue('Item SubCategory')" type="checkbox"
                                    id="inlineCheckbox1200700298456">
                                <label for="inlineCheckbox1200700298456">{{ $t('Item SubCategory') }}</label>
                            </div>
                        </div> -->
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.brand" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Brand')" type="checkbox"
                                           id="inlineCheckbox12007002">
                                    <label for="inlineCheckbox12007002">{{ $t('Item Brand') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.origin" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Origin')" type="checkbox"
                                           id="inlineCheckbox12007003">
                                    <label for="inlineCheckbox12007003">{{ $t('Item Origin') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.size" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Size')" type="checkbox"
                                           id="inlineCheckbox120070004">
                                    <label for="inlineCheckbox120070004">{{ $t('Item Size') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.color" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Color')" type="checkbox"
                                           id="inlineCheckbox1200700006">
                                    <label for="inlineCheckbox1200700006">{{ $t('Item Color') }}</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="checkbox form-check-inline mx-2 mt-2">
                                    <input v-model="itemListView.unit" :key="render"
                                           v-on:change="GetItemListOrderValue('Item Unit')" type="checkbox"
                                           id="inlineCheckbox12007025">
                                    <label for="inlineCheckbox12007025">{{ $t('Item Unit') }}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="hr-dashed hr-menu my-2" />
                    <div class="col-md-12 mb-5 pb-4">
                        <h5 class="fw-bold">Item List Header Display Order</h5>
                        <p><b> Please Drag and Drop Items to Update the Item list Display order.</b></p>
                        <h6>Item columns will be</h6>
                        <draggable class="col-sm-12 border py-2 d-flex" v-model="resultData2" style="margin: unset;">
                            <div class="row align-items-baseline p-0 m-0 mb-1 me-2" style="background:#f0f8ff"
                                 v-for="(module) in resultData2" v-bind:key="module.name">
                                <div>
                                    <h5>
                                        <a href="javascript:void(0)">
                                            {{ module.name }}
                                        </a>
                                    </h5>
                                </div>
                            </div>
                        </draggable>
                    </div>
                </div>
            </div>

        </div>

        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class="col-md-12">
                    <div class="button-items">
                        <button class="btn btn-outline-primary" v-if="active == 'DisplayName'"
                                v-on:click="SaveItemViewSetup(false)">
                            <i class="far fa-save "></i> {{
 $t('AddCustomer.btnSave')
                            }}
                        </button>
                        <button class="btn btn-outline-primary" v-if="active == 'ItemList'"
                                v-on:click="SaveItemViewSetup(true)">
                            <i class="far fa-save "></i> {{
 $t('AddCustomer.btnSave')
                            }}
                        </button>
                        <button class="btn btn-danger" v-on:click="GotoPage('/StartScreen')">
                            {{
 $t('AddCustomer.Cancel')
                            }}
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
    </div>
</template>

<script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { VueDraggableNext } from 'vue-draggable-next'

export default {
    components: {
        Loading,
        draggable: VueDraggableNext,
    },
    data: function () {
        return {
            loading: false,
            itemViewSetup: {
                isCode: false,
                isNameEn: false,
                isNameAr: false,
                isDescription: false,
                isCategory: false,
                isBarcode: false,
                isStyle: false,
                brand: false,
                origin: false,
                size: false,
                color: false,
                unit: false
            },
            itemViewSetupPrint: {
                isCode: false,
                isNameEn: false,
                isNameAr: false,
                isDescription: false,
                isCategory: false,
                isBarcode: false,
                isStyle: false,
                brand: false,
                origin: false,
                size: false,
                color: false,
                unit: false
            },
            itemListView: {
                code: false,
                itemName: false,
                salePrice: false,
                purchasePrice: false,
                barCodeType: false,
                currentQuantity: false,
                image: false,
                styleNumber: false,
                sku: false,
                partNumber: false,
                category: false,
                brand: false,
                origin: false,
                size: false,
                color: false,
                unit: false
            },
            isForAllProduct: false,
            itemViewSetupList: [],
            itemViewSetupListForPrint: [],
            itemListViewSetup: [],
            itemViews: {
                itemViewSetupList: [],
                itemViewSetupListForPrint: [],
                itemListViewSetup: [],
            },
            displayName: '',
            displayNameForPrint: '',

            active: 'DisplayName',
            render: 0,
        }
    },
    filters: {},
    validations: {},
    computed: {
        resultData: {
            get() {
                return this.itemViewSetupList;
            },

                set(val) {
                    this.itemViewSetupList = val;
                    var count = 1;
                    this.itemViewSetupList.forEach(function (x) {
                        x.sequence = count
                        count++
                    });
                    this.updateDisplayName();
                }
            },
            resultData1: {
                get() {
                    return this.itemViewSetupListForPrint;
                },

            set(val) {
                this.itemViewSetupListForPrint = val;
                var count = 1;
                this.itemViewSetupListForPrint.forEach(function (x) {
                    x.sequence = count
                    count++
                });
                this.updateDisplayName();
            }
        },
        resultData2: {
            get() {
                return this.itemListViewSetup;
            },

            set(val) {
                this.itemListViewSetup = val;
                var count = 1;
                this.itemListViewSetup.forEach(function (x) {
                    x.sequence = count
                    count++
                });
            }
        }
    },
    methods: {
        makeActive: function (item) {
            this.active = item;
            if (item == "DisplayName") {
                this.GetItemViewSetup();
            }
            else if (item == "ItemList") {
                this.GetItemViewSetup();
            }
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        GetItemListOrderValue: function (value) {

            const trueValues = Object.values(this.itemListView).filter(value => value);
            const count = trueValues.length;

            if (count > 7) {

                if (value == "Item Code") {
                    this.itemListView.code = false;
                }
                else if (value == "Item Name") {
                    this.itemListView.itemName = false;
                }
                else if (value == "Sale Price") {
                    this.itemListView.salePrice = false;
                }
                else if (value == "Purchase Price") {
                    this.itemListView.purchasePrice = false;
                }
                else if (value == "BarCode Type") {
                    this.itemListView.barCodeType = false;
                }
                else if (value == "Current Quantity") {
                    this.itemListView.currentQuantity = false;
                }
                else if (value == "Image") {
                    this.itemListView.image = false;
                }
                else if (value == "Item Style/Model") {
                    this.itemListView.styleNumber = false;
                }
                else if (value == "SKU") {
                    this.itemListView.sku = false;
                }
                else if (value == "Part Number") {
                    this.itemListView.partNumber = false;
                }
                else if (value == "Item Category") {
                    this.itemListView.category = false;
                }
                else if (value == "Item SubCategory") {
                    this.itemListView.subCategory = false;
                }
                else if (value == "Item Brand") {
                    this.itemListView.brand = false;
                }
                else if (value == "Item Origin") {
                    this.itemListView.origin = false;
                }
                else if (value == "Item Size") {
                    this.itemListView.size = false;
                }
                else if (value == "Item Color") {
                    this.itemListView.color = false;
                }
                else if (value == "Item Unit") {
                    this.itemListView.unit = false;
                }
                this.render++;
                this.$swal({
                    title: 'List Columns',
                    text: 'You can not select more than Seven values',
                    type: 'error',
                    icon: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
            else {
                if (value == "Item Code") {
                    if (this.itemListView.code) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Code',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });

                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Name") {
                    if (this.itemListView.itemName) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Name',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Sale Price") {
                    if (this.itemListView.salePrice) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Sale Price',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Purchase Price") {
                    if (this.itemListView.purchasePrice) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Purchase Price',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "BarCode Type") {
                    if (this.itemListView.barCodeType) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'BarCode Type',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Current Quantity") {
                    if (this.itemListView.currentQuantity) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Current Quantity',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Image") {
                    if (this.itemListView.image) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Image',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Style/Model") {
                    if (this.itemListView.styleNumber) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Style/Model',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "SKU") {
                    if (this.itemListView.sku) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'SKU',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Part Number") {
                    if (this.itemListView.partNumber) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Part Number',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Category") {
                    if (this.itemListView.category) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Category',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item SubCategory") {
                    if (this.itemListView.subCategory) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item SubCategory',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Brand") {
                    if (this.itemListView.brand) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Brand',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Origin") {
                    if (this.itemListView.origin) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Origin',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Size") {
                    if (this.itemListView.size) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Size',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Color") {
                    if (this.itemListView.color) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Color',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
                if (value == "Item Unit") {
                    if (this.itemListView.unit) {
                        this.itemListViewSetup.push({
                            id: '00000000-0000-0000-0000-000000000000',
                            name: value,
                            displayName: 'Item Unit',
                            sequence: 8
                        });
                        this.itemListViewSetup = this.itemListViewSetup.map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                    else {
                        this.itemListViewSetup = this.itemListViewSetup.filter(item => item.name !== value).map((item, index) => {
                            item.sequence = index + 1;
                            return item;
                        });
                    }
                }
            }
        },
        GetItemDisplayValue: function (value) {
            if (value == "Item Code") {
                if (this.itemViewSetup.isCode) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'PR-00001',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });

                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Name (English)") {
                if (this.itemViewSetup.isNameEn) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Shoes',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Name (Arabic)") {
                if (this.itemViewSetup.isNameAr) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'أحذية',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Description") {
                if (this.itemViewSetup.isDescription) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'These are Shoes',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Category") {
                if (this.itemViewSetup.isCategory) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Shoes Category',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Barcode") {
                if (this.itemViewSetup.isBarcode) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: '546512612391',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Style/Model") {
                if (this.itemViewSetup.isStyle) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Slipers',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Brand") {
                if (this.itemViewSetup.brand) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Brand',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Origin") {
                if (this.itemViewSetup.origin) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Origin',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Size") {
                if (this.itemViewSetup.size) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Size',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Color") {
                if (this.itemViewSetup.color) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Color',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Unit") {
                if (this.itemViewSetup.unit) {
                    this.itemViewSetupList.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Unit',
                        sequence: 8
                    });
                    this.itemViewSetupList = this.itemViewSetupList.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupList = this.itemViewSetupList.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            this.updateDisplayName();

        },
        GetItemDisplayValueForPrint: function (value) {
            if (value == "Item Code") {
                if (this.itemViewSetupPrint.isCode) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'PR-00001',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Name (English)") {
                if (this.itemViewSetupPrint.isNameEn) {

                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Shoes',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Name (Arabic)") {
                if (this.itemViewSetupPrint.isNameAr) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'أحذية',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Description") {
                if (this.itemViewSetupPrint.isDescription) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'These are Shoes',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Category") {
                if (this.itemViewSetupPrint.isCategory) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Shoes Category',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Barcode") {
                if (this.itemViewSetupPrint.isBarcode) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: '546512612391',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Style/Model") {
                if (this.itemViewSetupPrint.isStyle) {

                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Slipers',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Brand") {
                if (this.itemViewSetupPrint.brand) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Brand',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Origin") {
                if (this.itemViewSetupPrint.origin) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Origin',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Size") {
                if (this.itemViewSetupPrint.size) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Size',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Color") {
                if (this.itemViewSetupPrint.color) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Color',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            if (value == "Item Unit") {
                if (this.itemViewSetupPrint.unit) {
                    this.itemViewSetupListForPrint.push({
                        id: '00000000-0000-0000-0000-000000000000',
                        name: value,
                        displayName: 'Item Unit',
                        sequence: 8
                    });
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
                else {
                    this.itemViewSetupListForPrint = this.itemViewSetupListForPrint.filter(item => item.name !== value).map((item, index) => {
                        item.sequence = index + 1;
                        return item;
                    });
                }
            }
            this.updateDisplayName();
        },
        updateDisplayName() {
            // Create a new array to store names in the correct sequence
            const newDisplayName = this.itemViewSetupList
                .sort((a, b) => a.sequence - b.sequence)
                .map(item => item.displayName);

                // Update the displayName
                this.displayName = newDisplayName.join('  ');

                const newDisplayNameForPrint = this.itemViewSetupListForPrint
                    .sort((a, b) => a.sequence - b.sequence)
                    .map(item => item.displayName);

            // Update the displayName
            this.displayNameForPrint = newDisplayNameForPrint.join('  ');
        },
        SaveItemViewSetup: function (val) {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            // this.logistics.branchId = localStorage.getItem('BranchId');

                this.itemViews.itemViewSetupList = [];
                this.itemViewSetupList.forEach(x => {
                    this.itemViews.itemViewSetupList.push(x);
                })

            this.itemViews.itemViewSetupListForPrint = [];
            this.itemViewSetupListForPrint.forEach(x => {
                this.itemViews.itemViewSetupListForPrint.push(x);
            })
            this.itemViews.itemListViewSetup = [];
            this.itemListViewSetup.forEach(x => {
                this.itemViews.itemListViewSetup.push(x);
            })

            this.$https.post('/Product/SaveItemView?isForAllProduct=' + root.isForAllProduct + '&isItemList=' + val, this.itemViews, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data.isSuccess) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data has been Saved' : 'حفظ بنجاح',
                        type: 'success',
                        icon: 'success',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        confirmButtonClass: "btn btn-success",
                        buttonsStyling: false
                    })
                    root.GetItemViewSetup();
                }
                else {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: response.data.isAddUpdate,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                root.loading = false;
            }
            ).catch(() => {
                root.$swal({
                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: "Please Contact with Support.",
                    type: 'error',
                    confirmButtonClass: "btn btn-info",
                    buttonsStyling: false
                });
                root.loading = false;
            });
        },
        GetItemViewSetup: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


                root.$https.get('/Product/GetItemsViewSetup', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                    root.itemViewSetupList = [];
                    root.itemViewSetupListForPrint = [];
                    root.itemListViewSetup = [];

                    root.itemViewSetupList = response.data.itemViewSetupList;
                    root.itemViewSetupListForPrint = response.data.itemViewSetupListForPrint;
                    root.itemListViewSetup = response.data.itemListViewSetup;

                    root.itemViewSetupList.forEach(x => {
                        if (x.name == 'Item Code') {
                            root.itemViewSetup.isCode = true;
                        }
                        else if (x.name == 'Item Name (English)') {
                            root.itemViewSetup.isNameEn = true;
                        }
                        else if (x.name == 'Item Name (Arabic)') {
                            root.itemViewSetup.isNameAr = true;
                        }
                        else if (x.name == 'Item Description') {
                            root.itemViewSetup.isDescription = true;
                        }
                        else if (x.name == 'Item Category') {
                            root.itemViewSetup.isCategory = true;
                        }
                        else if (x.name == 'Item Barcode') {
                            root.itemViewSetup.isBarcode = true;
                        }
                        else if (x.name == 'Item Style/Model') {
                            root.itemViewSetup.isStyle = true;
                        }
                        else if (x.name == "Item Brand") {
                            root.itemViewSetup.brand = true;
                        }
                        else if (x.name == "Item Origin") {
                            root.itemViewSetup.origin = true;
                        }
                        else if (x.name == "Item Size") {
                            root.itemViewSetup.size = true;
                        }
                        else if (x.name == "Item Color") {
                            root.itemViewSetup.color = true;
                        }
                        else if (x.name == "Item Unit") {
                            root.itemViewSetup.unit = true;
                        }
                    });
                    root.itemViewSetupListForPrint.forEach(x => {
                        if (x.name == 'Item Code') {
                            root.itemViewSetupPrint.isCode = true;
                        }
                        else if (x.name == 'Item Name (English)') {
                            root.itemViewSetupPrint.isNameEn = true;
                        }
                        else if (x.name == 'Item Name (Arabic)') {
                            root.itemViewSetupPrint.isNameAr = true;
                        }
                        else if (x.name == 'Item Description') {
                            root.itemViewSetupPrint.isDescription = true;
                        }
                        else if (x.name == 'Item Category') {
                            root.itemViewSetupPrint.isCategory = true;
                        }
                        else if (x.name == 'Item Barcode') {
                            root.itemViewSetupPrint.isBarcode = true;
                        }
                        else if (x.name == 'Item Style/Model') {
                            root.itemViewSetupPrint.isStyle = true;
                        }
                        else if (x.name == "Item Brand") {
                            root.itemViewSetupPrint.brand = true;
                        }
                        else if (x.name == "Item Origin") {
                            root.itemViewSetupPrint.origin = true;
                        }
                        else if (x.name == "Item Size") {
                            root.itemViewSetupPrint.size = true;
                        }
                        else if (x.name == "Item Color") {
                            root.itemViewSetupPrint.color = true;
                        }
                        else if (x.name == "Item Unit") {
                            root.itemViewSetupPrint.unit = true;
                        }
                    });

                    root.itemListViewSetup.forEach((x) => {
                        if (x.name == "Item Code") {
                            root.itemListView.code = true;
                        }
                        else if (x.name == "Item Name") {
                            root.itemListView.itemName = true;
                        }
                        else if (x.name == "Sale Price") {
                            root.itemListView.salePrice = true;
                        }
                        else if (x.name == "Purchase Price") {
                            root.itemListView.purchasePrice = true;
                        }
                        else if (x.name == "BarCode Type") {
                            root.itemListView.barCodeType = true;
                        }
                        else if (x.name == "Current Quantity") {
                            root.itemListView.currentQuantity = true;
                        }
                        else if (x.name == "Image") {
                            root.itemListView.image = true;
                        }
                        else if (x.name == "Item Style/Model") {
                            root.itemListView.styleNumber = true;
                        }
                        else if (x.name == "SKU") {
                            root.itemListView.sku = true;
                        }
                        else if (x.name == "Part Number") {
                            root.itemListView.partNumber = true;
                        }
                        else if (x.name == "Item Category") {
                            root.itemListView.category = true;
                        }
                        else if (x.name == "Item SubCategory") {
                            root.itemListView.subCategory = true;
                        }
                        else if (x.name == "Item Brand") {
                            root.itemListView.brand = true;
                        }
                        else if (x.name == "Item Origin") {
                            root.itemListView.origin = true;
                        }
                        else if (x.name == "Item Size") {
                            root.itemListView.size = true;
                        }
                        else if (x.name == "Item Color") {
                            root.itemListView.color = true;
                        }
                        else if (x.name == "Item Unit") {
                            root.itemListView.unit = true;
                        }
                    })
                    root.render++;
                    root.updateDisplayName();
                    root.isForAllProduct = false;
                }
            });

        },
    },
    created: function () {
        this.GetItemViewSetup();
    },

        mounted: function () { },
    }
</script>

<style>
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
</style>