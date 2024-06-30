<template>
    <div v-if="isValid('CanPrintItemBarcode')" class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title"> {{ $t('MultipleBarcodePrinting.MultipleBarcodePrinting') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('MultipleBarcodePrinting.Home') }}</a></li>
                                    <li class="breadcrumb-item active"> {{
                                        $t('MultipleBarcodePrinting.MultipleBarcodePrinting') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card col-md-12 m-auto">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="text-left">
                                <div>
                                    <div class="row ">
                                        <div class=" form-group col-md-4">
                                            <label>{{ $t('MultipleBarcodePrinting.FinishingProduct') }} :<span
                                                    class="text-danger"> *</span></label>
                                            <multiselect v-model="selectedValue" :options="options" :multiple="false"
                                                @update:modelValue="GetProduct(selectedValue.id)" track-by="name"
                                                :clear-on-select="false" :preserve-search="false" :show-labels="false"
                                                label="name"
                                                :placeholder="$t('MultipleBarcodePrinting.PleaseSelectProduct')">
                                                <template v-slot:noResult>
                                                    <p> </p>
                                                </template>
                                            </multiselect>
                                        </div>
                                        <div hidden :key="render" class="form-group has-label col-md-4">
                                            <label class="text font-weight-bolder"> {{ $t('MultipleBarcodePrinting.Type')
                                            }}: <span class="text-danger"> *</span> <span
                                                    class="text-left text-danger">{{ barcodes.limit }}
                                                    Characters</span></label>
                                            <barcodeDropdown :modelValue="barcodes.type" disabled v-model="barcodes.type"
                                                ref="getBarcodeLimit" @update:modelValue="getLimit(barcodes.type)">
                                            </barcodeDropdown>
                                        </div>
                                        <div class="form-group has-label col-md-4">
                                            <label class="text  font-weight-bolder"> {{ $t('MultipleBarcodePrinting.Number')
                                            }}: <span class="text-danger"> *</span></label>
                                            <div v-bind:class="{ 'has-danger': v$.barcodes.code.$error }">
                                                <input class="form-control" v-model="v$.barcodes.code.$model" type="text" />
                                                <span v-if="v$.barcodes.code.$error" class="error text-danger">
                                                    <span v-if="!v$.barcodes.code.required"> {{
                                                        $t('MultipleBarcodePrinting.CodeRequired') }}</span>
                                                    <span v-if="!v$.barcodes.code.maxLength">{{
                                                        $t('MultipleBarcodePrinting.CodeLimit') }}</span>
                                                </span>
                                                <label
                                                    v-if="(barcodes.code == '' && message == true)">{{ $t('MultipleBarcodePrinting.BarcodeErrorMsg') }}</label>
                                            </div>
                                        </div>

                                        <div class="form-group has-label col-md-4">
                                            <label class="text  font-weight-bolder"> {{ $t('MultiBarcodePrinting.Printer')
                                            }}: <span class="text-danger"> *</span></label>
                                            <printer-list-dropdown v-model="barcodes.printerName"></printer-list-dropdown>
                                        </div>

                                        <div class="form-group has-label col-md-4">
                                            <label class="text font-weight-bolder">
                                                {{ $t('MultipleBarcodePrinting.salePrice') }} </label>
                                            <input class="form-control" v-model="barcodes.salePrice" type="text" />
                                        </div>

                                        <div class="form-group has-label col-md-4">
                                            <label class="text font-weight-bolder">
                                                {{ $t('MultipleBarcodePrinting.VATAmount') }} </label>
                                            <input class="form-control" v-model="barcodes.vatPrice" type="text" />
                                        </div>

                                        <div class="form-group has-label col-md-4">
                                            <label class="text font-weight-bolder">
                                                {{ $t('MultipleBarcodePrinting.TotalwithVAT') }} </label>
                                            <input class="form-control" v-model="barcodes.price" type="text" />
                                        </div>
                                        <div class="form-group has-label col-md-4">
                                            <label class="text  font-weight-bolder"> {{ $t('MultipleBarcodePrinting.Value')
                                            }}: <span class="text-danger"> *</span></label>
                                            <div v-bind:class="{ 'has-danger': v$.barcodes.value.$error }">
                                                <input class="form-control" @focus="$event.target.select()"
                                                    v-model="v$.barcodes.value.$model" type="number" />
                                                <span v-if="v$.barcodes.value.$error" class="error text-danger">
                                                    <span v-if="!v$.barcodes.value.required"> {{
                                                        $t('MultipleBarcodePrinting.ValueRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group has-label col-md-4">
                                            <label for="inlineCheckbox1"> {{ $t('MultipleBarcodePrinting.ShowText') }} :
                                            </label>
                                            <div>
                                                <div class="checkbox form-check-inline mx-2"><input type="checkbox"
                                                        v-model="barcodes.show" id="inlineCheckbox2"><label
                                                        for="inlineCheckbox2"></label></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                <button v-bind:disabled="v$.barcodes.$invalid" type="button" class="btn btn-primary  "
                                    v-on:click="PrintRdlc()"> {{ $t('MultipleBarcodePrinting.btnPrint') }}</button>
                                <button type="button" class="btn btn-outline-danger  mr-3 " v-on:click="close()">{{
                                    $t('MultipleBarcodePrinting.btnClear') }}</button>
                            </div>
                            <div class="row ">
                                <div class="form-group has-label col-sm-12">
                                    <barcode v-if="barcodes.show" :width="1.9" :display-value="true" :height="50"
                                        v-bind:value="barcodes.code"></barcode>
                                    <barcode v-else :width="1.9" :display-value="false" :height="50"
                                        v-bind:value="barcodes.code"></barcode>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength, between, minValue } from '@vuelidate/validators'; 
    import useVuelidate from '@vuelidate/core';
    import Vue3Barcode from 'vue3-barcode'
    import Multiselect from 'vue-multiselect'
    export default {
        mixins: [clickMixin],
        components: {
            'barcode': Vue3Barcode,
            Multiselect,
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                reportsrc1:'',
        
                changereportt:0,
                show:false,
                show1:false,
                selectedValue: '',
                render: 0,
                disabled: true,
                printType: '',
                barcodes: {
                    type: '',
                    code: '',
                    value: 1,
                    limit: '',
                    limitPerLine: 1,
                    itemEngName: '',
                    itemArName: '',
                    companyName: '',
                    price: 0,
                    show: true,
                    showPrice: true,
                    showProdName: true,
                    currency: '',
                    printerName: '',
                    vatPrice: 0,
                    salePrice: 0
                },
                barcodeList: [],
                printList: {
                    quantity: 0,
                    value: '',
                    isText: '',
                    type: '',
                    limitPerLine: 1
                },
                printRender: 0,
                productId: '',
                message: false,
                productList: [],
                productName: '',
                headerFooter: {
                    company: '',
                    date: ''
                },
                options: [],

                itemViewSetupList: [],
                
            }
        },
        validations() {

            return {
                printType: {
                    required
                },
                barcodes: {
                    code: {
                        required,
                        maxLength: maxLength(25)
                    },
                    value: {
                        required,
                        minValue: minValue(1)
                    },
                    limitPerLine: {
                        between: between(1, 3)
                    },
                    printerName: {
                    },
                },
            }

        },
        methods: {
            IsSave:function(){
                this.show1 = !this.show1;
            },
            GetProductList: function () {

                var root = this;
                var token = "";
                if (token == "") {
                    token = localStorage.getItem("token");
                }


                //search = search == undefined ? '' : search;
                // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;

                this.$https.get("/Product/GetProductBarcode?isRaw=" + false, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            response.data.results.products.forEach(function (prod) {
                                //taxMethod

                                root.options.push({
                                    id: prod.id,
                                    name:( prod.displayName != null || prod.displayName != '' ) ? prod.displayName : prod.englishName,
                                    salePrice: prod.salePrice,
                                    taxRate: prod.taxRateValue,
                                    taxMethod: prod.taxMethod
                                });
                            });
                            root.productList = response.data.results.products;

                        }
                    });


            },

            PrintRdlc:function() {
                
                var root = this;
                var companyId = '';
            
                companyId = localStorage.getItem('CompanyID');
                
                var barcode=JSON.stringify(this.barcodes)
                barcode = barcode.replace("&", "");
                console.log(barcode);
                this.$https.get(this.$ReportServer+'/Invoice/A4_DefaultTempletForm.aspx?companyId='+companyId +'&formName=MultiBarcodePrinting'+'&barcode='+barcode).catch(error => {
                        console.log(error);
                    })
                    .finally(() => root.loading = false);
            },
            GetProduct: function (id) {

                var data = this.productList.find(function (x) {
                    return x.id == id;
                });
                var root = this;
                if (data != null) {
                    
                    if (data.taxMethod == 'Inclusive' || data.taxMethod == 'شامل') {
                        root.barcodes.vatPrice = parseFloat(((data.salePrice * data.taxRateValue) / (100 + data.taxRateValue)).toFixed(3).slice(0, -1));
                        root.barcodes.price = parseFloat(data.salePrice)
                    }
                    else if (data.taxMethod == 'Exclusive' || data.taxMethod == 'غير شامل') {
                        root.barcodes.vatPrice = ((data.salePrice * data.taxRateValue) / (100)).toFixed(3).slice(0, -1);
                        root.barcodes.price = parseFloat(data.salePrice) + parseFloat(root.barcodes.vatPrice)

                    }
                    else if (data.taxMethod == 'Exempted' || data.taxMethod == 'معفى') {
                        root.barcodes.vatPrice = 0;
                        root.barcodes.price = parseFloat(data.salePrice)
                    }
                    if (data.barCode == '') {
                        root.barcodes.code = '';
                        root.barcodes.value = 1;
                        root.message = true;
                        root.barcodes.itemEngName = (data.barCodeDisplayName != null || data.barCodeDisplayName != '') ? data.barCodeDisplayName :(data.englishName != null || data.englishName != '') ? data.englishName.length >= 25 ? data.englishName.slice(0, 25) : data.englishName : data.arabicName.length >= 25 ? data.arabicName.slice(0, 25) : data.arabicName;
                        root.barcodes.itemArName = data.arabicName;
                        root.barcodes.salePrice = data.salePrice;
                    }
                    else {
                        root.barcodes.code = data.barCode;
                        root.barcodes.itemEngName = (data.barCodeDisplayName != null || data.barCodeDisplayName != '') ? data.barCodeDisplayName :(data.englishName != null || data.englishName != '') ? data.englishName.length >= 25 ? data.englishName.slice(0, 25) : data.englishName : data.arabicName.length >= 25 ? data.arabicName.slice(0, 25) : data.arabicName;
                        root.barcodes.itemArName = data.arabicName;
                        root.barcodes.salePrice = data.salePrice;
                    }
                }
                
            },
            close: function () {
                this.$router.push('/StartScreen');
            },
            getLimit: function (type, blist) {
                if (type != null) {
                    var newValue = blist.find(function (x) {
                        return x.id == type;
                    })
                    var spanText = newValue.limit;
                    this.barcodes.limit = spanText;
                }
                else {
                    this.barcodes.limit = '';
                }
            },
            PrintBarcode: function () {


                this.printList.quantity = parseInt(this.barcodes.value);
                this.printList.value = this.barcodes.code;
                this.printList.isText = this.barcodes.show;
                this.printList.type = this.barcodes.type;
                this.printList.isShown = true;

                if (this.barcodes.limitPerLine > 0 && this.barcodes.limitPerLine < 4)
                    this.printList.limitPerLine = this.barcodes.limitPerLine
                else {
                    this.printList.limitPerLine = 3
                    this.barcodes.limitPerLine = 3
                }

                this.printRender++;
            },
            GenerateBarcode: function () {


                this.randomNumber = Math.random() * 100; //multiply to generate random number between 0, 100
                this.barcodes.code = this.randomNumber
                this.printRender++;
            },
            GetHeaderDetail: function () {
                var root = this;

                var token = '';
                if (token == "") {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {

                            root.headerFooter.company = response.data;
                        }
                    });
            },
            getBarcodeType: function () {
                var root = this;
                var token = '';
                if (token == "") {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/CompanyAccountSetupDetails', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.barcodes.type = response.data.barcodeType;
                        var btype = root.barcodes.type;
                        root.barcodeList = root.$refs.getBarcodeLimit.options;
                        root.getLimit(btype, root.barcodeList);
                        root.render++;
                    }
                });
            },
            GetItemViewSetup: function () {
                var root = this;
                var token = '';
                if (token == "") {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/GetBarCodeSetupList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.itemViewSetupList = [];
                        root.itemViewSetupList = response.data.itemViewSetupList;

                        if (root.itemViewSetupList.length == 0 || root.itemViewSetupList == null) {
                            root.$swal({
                                title: 'Product Display Name',
                                text: "Please setup product display name first by click on button below ↓",
                                icon: 'warning',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Product Display Name Setup'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    root.$router.push({
                                        path: '/AddBarCodeSetup',
                                    })
                                }
                            });
                        }
                    }
                });
            },
        },
        

        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.GetItemViewSetup();
            this.GetProductList()
            this.getBarcodeType();
            this.barcodes.currency = localStorage.getItem('currency');
            this.GetHeaderDetail();
        },

        
    }
</script>


