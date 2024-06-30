<template>
    <div class="row">
        <div class="col-lg-12 px-3">
            <div class="row">
                <div class="col-sm-6">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;">BR</span>
                            <div class="media-body align-self-center ms-3">
                                <h6 class="m-0 font-20">{{ $t('BarCode Setup') }} </h6>
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
                        {{ $t('Item BarCode Display') }}
                    </a>
                </li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'DisplayName' }">
                    <div class="row ms-5">
                        <div class="col-lg-6">
                            <h5 class="fw-bold">Item BarCode Display</h5>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input  v-model="itemViewSetup.isCode" :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Code')"
                                            type="checkbox" id="inlineChe1">
                                        <label for="inlineChe1">{{ $t('Item Code') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isNameEn"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Name (English)')" type="checkbox"
                                            id="inlineChe2">
                                        <label for="inlineChe2">{{ $t('Item Name (English)') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isNameAr"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Name (Arabic)')" type="checkbox"
                                            id="inlineChe3">
                                        <label for="inlineChe3">{{ $t('Item Name (Arabic)') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isDescription"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Description')" type="checkbox"
                                            id="inlineChe4">
                                        <label for="inlineChe4">{{ $t('Item Description') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isCategory"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Category')" type="checkbox"
                                            id="inlineChe5">
                                        <label for="inlineChe5">{{ $t('Item Category') }}</label>
                                    </div>
                                </div>
                                <!-- <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isBarcode"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Barcode')" type="checkbox"
                                            id="inlineChe6">
                                        <label for="inlineChe6">{{ $t('Item Barcode') }}</label>
                                    </div>
                                </div> -->
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.isStyle"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Style/Model')" type="checkbox"
                                            id="inlineChe7">
                                        <label for="inlineChe7">{{ $t('Item Style/Model') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.brand" :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Brand')"
                                            type="checkbox" id="inlineChe8">
                                        <label for="inlineChe8">{{ $t('Item Brand') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.origin"
                                            :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Origin')" type="checkbox"
                                            id="inlineChe9">
                                        <label for="inlineChe9">{{ $t('Item Origin') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.size" :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Size')"
                                            type="checkbox" id="inlineChe10">
                                        <label for="inlineChe10">{{ $t('Item Size') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.color" :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Color')"
                                            type="checkbox" id="inlineChe11">
                                        <label for="inlineChe11">{{ $t('Item Color') }}</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2">
                                        <input v-model="itemViewSetup.unit" :key="randerBarCode" v-on:change="GetItemDisplayValue('Item Unit')"
                                            type="checkbox" id="inlineChe12">
                                        <label for="inlineChe12">{{ $t('Item Unit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <hr class="hr-dashed hr-menu mt-0" />

                    <div class="row mb-5 ms-4 ">
                        <div class="col-md-6">
                            <h5 class="fw-bold">Item BarCode Display Order</h5>
                            <p><b> Please Drag and Drop Items to Update the Item BarCode Display order.</b></p>
                            <h6>Item BarCode Display Order will be</h6>
                            <p class="text-info">BarCode Display Name takes only first 20 Characters.</p>
                            <h6><b>{{ displayName.length >= 25 ? displayName.slice(0, 25) : displayName }}</b></h6>
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
                    </div>
                </div>

            </div>

        </div>

        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class="col-md-12">
                    <div class="button-items">
                        <button class="btn btn-outline-primary" v-if="active == 'DisplayName'"
                            v-on:click="SaveItemViewSetup(false)"><i class="far fa-save "></i> {{ $t('AddCustomer.btnSave')
                            }}</button>

                        <button class="btn btn-danger" v-on:click="GotoPage('/StartScreen')">{{ $t('AddCustomer.Cancel')
                        }}</button>
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

            isForAllProduct: false,
            itemViewSetupList: [],
            barCodeSetup: {
                itemViewSetupList: [],
            },
            displayName: '',

            active: 'DisplayName',
            randerBarCode: 0,
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
    },
    methods: {
        makeActive: function (item) {
            this.active = item;
            if (item == "DisplayName") {
                this.GetItemViewSetup();
            }
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        GetItemDisplayValue: function (value) {
            
            const trueValues = Object.values(this.itemViewSetup).filter(value => value);
            const count = trueValues.length;
            
            if (count > 2) {
                if (value == "Item Code") {
                    this.itemViewSetup.isCode = false;
                }
                else if (value == "Item Name (English)") {
                    this.itemViewSetup.isNameEn = false;
                }
                else if (value == "Item Name (Arabic)") {
                    this.itemViewSetup.isNameAr = false;
                }
                else if (value == "Item Description") {
                    this.itemViewSetup.isDescription = false;
                }
                else if (value == "Item Barcode") {
                    this.itemViewSetup.isBarcode = false;
                }
                else if (value == "Item Category") {
                    this.itemViewSetup.isCategory = false;
                }
                else if (value == "Item Style/Model") {
                    this.itemViewSetup.isStyle = false;
                }
                else if (value == "Item Brand") {
                    this.itemViewSetup.brand = false;
                }
                else if (value == "Item Origin") {
                    this.itemViewSetup.origin = false;
                }
                else if (value == "Item Size") {
                    this.itemViewSetup.size = false;
                }
                else if (value == "Item Color") {
                    this.itemViewSetup.color = false;
                }
                else if (value == "Item Unit") {
                    this.itemViewSetup.unit = false;
                }
                this.randerBarCode++;
                this.$swal({
                    title: 'List Columns',
                    text: 'You can not select more than two values',
                    type: 'error',
                    icon: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
            else {
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
            }
        },

        updateDisplayName() {
            const newDisplayName = this.itemViewSetupList
                .sort((a, b) => a.sequence - b.sequence)
                .map(item => item.displayName);

            this.displayName = newDisplayName.join('  ');


        },
        SaveItemViewSetup: function () {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.barCodeSetup.itemViewSetupList = [];
            this.itemViewSetupList.forEach(x => {
                this.barCodeSetup.itemViewSetupList.push(x);
            })



            this.$https.post('/Product/SaveBarCodeSetup?isForAllProduct=' + root.isForAllProduct, this.barCodeSetup, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

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


            root.$https.get('/Product/GetBarCodeSetupList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.itemViewSetupList = [];

                    root.itemViewSetupList = response.data.itemViewSetupList;

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

                    root.randerBarCode++;
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