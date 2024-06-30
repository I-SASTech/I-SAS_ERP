<template>
    <div v-if="isValid('CanAddInventoryCount') || isValid('CanEditInventoryCount')">
        <div class="row">
            <div class="col-md-12 ml-auto mr-auto">
                <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddInventoryBlind.InventoryCountList') }} - {{documentId}} </h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddInventoryBlind.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddInventoryBlind.InventoryCountList') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <span>{{$t('AddInventoryBlind.SelectWarehouse')}}</span>
                                    <warehouse-dropdown :disable="isDisabled" ref="wareHouse" :value="warehouseId" v-model="warehouseId" @update:modelValue="GetProductData(warehouseId)" />
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                <div v-if="isDisabled" class="form-group mt-3"> 
                                    <span><b>{{$t('AddInventoryBlind.Date')}}:-</b></span>
                                    <br />
                                    <span>{{getDate(inventoryBlind.dateTime)}}</span>
                                </div>
                                <div v-else class="form-group">
                                    <span>{{$t('AddInventoryBlind.Date')}}</span>
                                    <VueCtkDateTimePicker v-model="inventoryBlind.dateTime"></VueCtkDateTimePicker>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class=" table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>
                                            #
                                        </th>
                                        <th>
                                            {{ $t('AddInventoryBlind.ProductName') }}
                                        </th>
                                        <th>
                                            {{$t('AddInventoryBlind.CategoryName')}}
                                        </th>
                                        <th>
                                            {{$t('AddInventoryBlind.Shelf/Location')}}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('AddInventoryBlind.CurrentStock') }}
                                        </th>
                                        <th v-if="isEdit" class="text-center">
                                            {{$t('AddInventoryBlind.PhysicalQuantity')}}
                                        </th>
                                        <th v-if="isEdit" class="text-center">
                                            {{$t('AddInventoryBlind.RemarksBI')}}
                                        </th>
                                        <th v-if="!isDisabled">

                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(details,index) in productList" v-bind:key="index" >

                                        <td style="width:5%">

                                            {{index+1}}
                                        </td>
                                        <td v-if="($i18n.locale == 'en' ||isLeftToRight())" style="width:20%">

                                            {{details.englishName}}
                                        </td>
                                        <td v-else-if="$i18n.locale == 'ar'" style="width:20%">

                                            {{details.arabicName}}
                                        </td>
                                        <td v-else style="width:20%">

                                            {{details.englishName}}
                                        </td>
                                        <td v-if="($i18n.locale == 'en' ||isLeftToRight())" style="width:20%">

                                            {{details.category.name}}
                                        </td>
                                        <td v-else-if="$i18n.locale == 'ar'" style="width:20%">

                                            {{details.category.nameArabic}}
                                        </td>
                                        <td v-else style="width:20%">

                                            {{details.category.name}}
                                        </td>
                                        <td style="width:10%">
                                            {{details.shelf}}
                                        </td>


                                        <td class="text-center" style="width:10%">
                                            {{details.inventory==null? 0: details.inventory.currentQuantity}} <br />

                                        </td>

                                        <td v-if="isEdit" class="text-center" style="width:10%">
                                            <input v-model="details.inventory.physicalQuantity "
                                                   type="number"
                                                   v-bind:disabled="isDisabled"
                                                   class="form-control  text-center " />
                                        </td>
                                        <td v-if="isEdit" class="text-center" style="width:22%">
                                            <input v-model="details.remarks "
                                                   type="text"
                                                   v-bind:disabled="isDisabled"
                                                   class="form-control " />
                                        </td>
                                        <td style="width:3%" v-if="!isDisabled">
                                            <button @click="removeProduct(details.id)"
                                                    title="Remove Item"
                                                    v-bind:disabled="isDisabled"
                                                    class="btn ">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </td>


                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="!isDisabled">
                            <div v-if="isEdit == true">
                                <button type="button" class="btn btn-outline-primary" v-if="(productList.length > 0 && inventoryBlind.dateTime != '')" v-on:click="SaveInentoryBlind(true)"> {{ $t('AddInventoryBlind.btnUpdate') }}</button>
                                <button type="button" class="btn btn-outline-primary" v-else disabled v-on:click="SaveInentoryBlind(true)"> {{ $t('AddInventoryBlind.btnUpdate') }}</button>
                            </div>


                            <div v-if="isEdit == false">
                                <button type="button" class="btn btn-outline-primary me-2" v-if="(productList.length > 0 && inventoryBlind.dateTime != '')" v-on:click="SaveInentoryBlind(false)"> {{ $t('AddInventoryBlind.btnSave') }}</button>
                                <button type="button" class="btn btn-outline-primary me-2" v-else disabled v-on:click="SaveInentoryBlind(false)"> {{ $t('AddInventoryBlind.btnSave') }}</button>
                                <button type="button" class="btn btn-outline-primary me-2" v-if="(productList.length > 0 && inventoryBlind.dateTime != '' && isValid('CanPrintInventoryCount'))" v-on:click="PrintProduct(false)"> {{ $t('AddInventoryBlind.SaveAndPrintBlind') }}</button>
                                <button type="button" class="btn btn-outline-primary " v-else-if="isValid('CanPrintInventoryCount')" disabled v-on:click="PrintProduct(false)"> {{ $t('AddInventoryBlind.SaveAndPrintBlind') }}</button>
                            </div>
                            <button class="btn btn-outline-danger"
                                    v-on:click="goToInventoryBlindList">
                                {{ $t('AddInventoryBlind.Cancel') }}
                            </button>
                        </div>
                        <div class="modal-footer justify-content-right" v-else>
                            <button class="btn btn-danger"
                                    v-on:click="goToInventoryBlindList">
                                {{ $t('AddInventoryBlind.Cancel') }}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="false"></loading>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    //import VueCtkDateTimePicker from 'vue-ctk-date-time-picker';
    //import Vue from 'vue'
    //Vue.component('VueCtkDateTimePicker', VueCtkDateTimePicker);
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";
    
    
    export default {
        components: {
            Loading
        },
        mixins: [clickMixin],
        data: function () {
            return {
                printRender: 0,
                warehouseId: '00000000-0000-0000-0000-000000000000',
                arabic: '',
                english: '',
                isEdit: false,
                show: false,
                type: '',
                documentId: '',
                date: '',
                randerDate: 0,
                productList: [],
                search: '',
                isDisabled: false,
                searchQuery: '',

                loading: false,
                headerFooter: {
                    company: '',
                    date: '',
                    documentId: '',
                    wareHouseName: ''
                },
                inventoryBlind: {
                    id: '00000000-0000-0000-0000-000000000000',
                    dateTime: '',
                    warehouseId: '',
                    documentId: '',
                    note: '',
                    isCounted: false,
                    inventoryBlindDetailVms: []
                },
                disabledDate: ''

            }
        },
        created: function () {
            var root = this;

            if (root.$route.query.Add == 'false') {
                root.$route.query.data = this.$store.isGetEdit;
            }

            this.$emit('update:modelValue', this.$route.name);
            if (this.$route.query.data != undefined) {

                this.productList = this.$route.query.data.inventoryBlindDetailModels;
                this.isEdit = true;
                this.inventoryBlind.dateTime = this.$route.query.data.dateTime
                this.disabledDate = moment(this.$route.query.data.dateTime).format('MM/dd/yyyy hh:mm tt')
                this.warehouseId = this.$route.query.data.warehouseId
                this.documentId = this.$route.query.data.documentId;
                this.inventoryBlind.note = this.$route.query.data.note;
                this.inventoryBlind.id = this.$route.query.data.id;
                this.isDisabled = this.$route.query.isDisabled == "true" ? true : false
                this.headerFooter.wareHouseName = this.$route.query.data.warehouseName
                this.headerFooter.date = moment(this.$route.query.data.dateTime).format('DD/MM/YYY')

            }


        },
        methods: {

            languageChange: function (lan) {

                if (this.language == lan) {
                    if (this.inventoryBlind.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/InventoryBlind');
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
            getDate: function (x) {
                return moment(x).format('dd/MM/yyyy hh:mm a');
            },
            removeProduct: function (id) {
                this.productList = this.productList.filter((x) => {
                    return x.id != id;
                });

            },
            goToInventoryBlindList: function () {
                this.$router.push('/InventoryBlindList');
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
                        }
                    });
            },
            PrintProduct: function (isCounted) {

                var root = this;
                if (this.isEdit) {
                    this.headerFooter.wareHouseName = this.$refs.wareHouse.GetWareHouseName()
                }
                if (this.productList.length != 0) {
                    this.show = true
                    this.printRender++;

                    setTimeout(function () {
                        root.show = false
                        root.SaveInentoryBlind(isCounted);
                    }, 1000)
                }

            },
            SaveInentoryBlind: function (isCounted) {
                this.loading = true
                var root = this;
                //root.inventoryBlind.dateTime = this.date;
                root.inventoryBlind.warehouseId = this.warehouseId;
                root.inventoryBlind.documentId = this.documentId;
                root.inventoryBlind.note = this.note;
                root.inventoryBlind.isCounted = isCounted;

                root.productList.forEach(function (x) {
                    root.inventoryBlind.inventoryBlindDetailVms.push({
                        productId: x.id,
                        currentQuantity: x.inventory.currentQuantity,
                        physicalQuantity: x.inventory.physicalQuantity,
                        remarks: x.remarks
                    });
                });

                // var url = '/Product/AddInventoryBlind';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post('/Product/AddInventoryBlind', root.inventoryBlind, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        if (response.data == 'Success') {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data has been added successfully' : 'تم إضافة البيانات بنجاح',
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            });
                            root.loading = false;
                            root.$router.push('/InventoryBlindList')
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: ""(root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Some error occured' : 'حدث خطأ ما', 
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }
                });
            },
            getPage: function () {

                this.GetProductData(this.warehouseId);
            },
            GetProductData: function (wareHouseId) {
                var root = this;
                this.loading = true
                if (!root.isEdit) {
                    this.headerFooter.wareHouseName = this.$refs.wareHouse.GetWareHouseName()
                    this.headerFooter.date = root.inventoryBlind.dateTime

                    var url = '/Product/GetProductInformation?wareHouseId=' + wareHouseId + '&isDropdown=' + false + '&isProductList=' + true;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            root.productList = response.data.results.products;
                            
                        }
                        root.loading = false;
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                        .finally(() => root.loading = false)
                }
            },
            GetDocumentId: function () {
                var root = this;

                var url = '/Product/GetBlindInventoryCode';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.documentId = response.data;
                        root.headerFooter.documentId = response.data
                    }
                });
            }

        },
        mounted: function () {
            this.GetHeaderDetail();
            if (!this.isEdit) {
                this.inventoryBlind.dateTime = "";
                this.GetDocumentId();
            }
            this.language = this.$i18n.locale;
            this.randerDate++
            this.rander++;
        }
    }
</script>