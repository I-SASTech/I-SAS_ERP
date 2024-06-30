<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title DayHeading" id="myModalLabel">
                    {{
 $t('QuickProductItem.QuickItemRegistration')
                    }}
                </h5>
            </div>

            <div class="modal-body ">
                <div class="row ">
                    <div v-bind:key="rendered" class="col-sm-12 form-group">
                        <label>
                            {{ $t('QuickProductItem.ProductCode') }} :<span class="text-danger">
                                *
                            </span>
                        </label>
                        <div>
                            <input readonly class="form-control" v-model="product.code" />

                        </div>
                    </div>
                    <div v-if="english == 'true'" class="col-sm-12 form-group "
                         v-bind:class="{ 'has-danger': v$.product.englishName.$error }">
                        <label class="text  font-weight-bolder ">
                            {{
$englishLanguage($t('QuickProductItem.ItemName'))
                            }} :<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control " v-model="v$.product.englishName.$model" type="text" />
                        <span v-if="v$.product.englishName.$error" class="error text-danger">
                            <span v-if="!v$.product.englishName.required">
                                {{
 $t('QuickProductItem.Name')
                                }}
                            </span>
                            <span v-if="!v$.product.englishName.maxLength">
                                {{
 $t('QuickProductItem.NameLength')
                                }}
                            </span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="col-sm-12 form-group "
                         v-bind:class="{ 'has-danger': v$.product.arabicName.$error }">
                        <label class="text  font-weight-bolder "
                               v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'padding-top:5px;'">
                            {{
$arabicLanguage($t('QuickProductItem.ItemName'))
                            }} :<span class="text-danger">
                                *
                            </span>
                        </label>
                        <input class="form-control" v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                               v-model="v$.product.arabicName.$model" type="text" />
                        <span v-if="v$.product.arabicName.$error" class="error text-danger">
                            <span v-if="!v$.product.arabicName.required">
                                {{
 $t('QuickProductItem.Name')
                                }}
                            </span>
                            <span v-if="!v$.product.arabicName.maxLength">
                                {{
 $t('QuickProductItem.NameLength')
                                }}
                            </span>
                        </span>
                    </div>
                    <div class="col-sm-12 form-group">
                        <label v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'padding-top:10px;'">
                            {{
                                $t('QuickProductItem.salePrice')
                            }} :
                        </label>
                        <div>
                            <my-currency-input v-model="product.salePrice"></my-currency-input>
                        </div>
                    </div>
                    <div class="col-sm-12 form-group " v-if="invoiceWoInventory">
                        <label>
                            {{ $t('QuickProductItem.ProductCategory') }} :<span class="text-danger">
                                *
                            </span>
                        </label>
                        <div>
                            <categorydropdown :isTemporary="invoiceWoInventory ? isTemporary : false"
                                              v-model="product.categoryId"></categorydropdown>
                        </div>
                    </div>
                    <div class="col-sm-12 form-group " v-if="isMultiUnit == 'true'">
                        <label>{{ $t('QuickProductItem.LevelOneUnit') }} :</label>
                        <div>
                            <unitleveldropdown v-model="product.levelOneUnit" :modelValue="product.levelOneUnit">
                            </unitleveldropdown>
                        </div>
                    </div>
                    <div class="col-sm-12 form-group " v-if="invoiceWoInventory">
                        <label>{{ $t('QuickProductItem.Unit') }} :</label>
                        <div>
                            <unitdropdown v-model="product.unitId" :modelValue="product.unitId">
                            </unitdropdown>
                        </div>
                    </div>
                    <div class="col-sm-12 form-group " v-if="invoiceWoInventory">
                        <label>{{ $t('QuickProductItem.Barcode') }} :</label>
                        <div class="p-0">
                            <input class="form-control " type="text" v-model="product.barcode" />
                        </div>
                    </div>

                    <div class="col-xs-12 " v-if="invoiceWoInventory">
                        <div class="row">
                            <div class="col-sm-6 form-group  ">
                                <button v-if="product.barcode == ''" class="btn btn-outline-primary "
                                        v-on:click="generateBarcode(false)">
                                    {{
 $t('QuickProductItem.Generate')
                                    }}
                                </button>
                                <button v-if="product.barcode != ''" class="btn btn-outline-danger"
                                        v-on:click="generateBarcode(true)">
                                    {{
 $t('QuickProductItem.Delete')
                                    }}
                                </button>
                            </div>
                            <div class="col-sm-6 form-group">
                                <barcode :height="30" v-bind:value="product.barcode"></barcode>

                            </div>
                        </div>
                    </div>
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="product.serviceItem">
                            <label for="inlineCheckbox1"> Service Item </label>
                        </div>
                    </div>
                </div>
            </div>

            <div v-if="!loading">
                <div class="modal-footer justify-content-right">
                    <button type="button" class="btn btn-outline-primary  " v-on:click="SaveProduct"
                            v-bind:disabled="v$.product.$invalid">
                        {{ $t('QuickProductItem.btnSave') }}
                    </button>
                    <button type="button" class="btn btn-outline-danger  mr-3 " v-on:click="close()">
                        {{
                            $t('QuickProductItem.btnClear')
                        }}
                    </button>
                </div>
            </div>
            <div v-else>
                <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
            </div>
        </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Vue3Barcode from 'vue3-barcode'

    import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'newproduct', 'type'],
        components: {
            'barcode': Vue3Barcode,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                rendered: 0,
                arabic: '',
                english: '',
                loading: false,
                isTemporary: true,
                isEnabled: false,
                isDisable: false,
                isMultiUnit: '',
                randomNumber: '',
                invoiceWoInventory: false,
                product:{}
            }
        },
        validations() {
            return {
                product:
                {
                    englishName: {
                        maxLength: maxLength(50)
                    },
                    categoryId: {
                    },
                    arabicName: {
                        // required: requiredIf((x) => {
                        //     if (x.englishName == '' || x.englishName == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.product.englishName;
                        }),
                        maxLength: maxLength(50)
                    }
                }
            }
        },
        methods: {


            generateBarcode: function (x) {

                if (x) {

                    this.randomNumber = 0; //multiply to generate random number between 0, 100
                    this.product.barcode = '';
                    this.isDisable = false
                    this.isEnabled = false
                }
                else {

                    this.randomNumber = Math.floor(Math.random() * 10000000000); //multiply to generate random number between 0, 100
                    this.product.barcode = this.randomNumber
                    this.isDisable = true
                    this.isEnabled = true
                }

            },

            writeBarcode: function () {
                this.isDisable = true;
                this.isEnabled = true;
            },

            close: function () {
                this.$emit('close');
            },

            SaveProduct: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                this.product.categoryIdQuick = this.product.categoryId;

                this.$https.post('/Product/SaveQuickProduct', this.product, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {

                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            });
                            root.product.id = response.data.message.id
                            if (root.addInvoice) {
                                root.$emit('closeOnSave');
                            }
                            else {
                                root.$emit('closeOnSave', response.data.categoryList.results.products);

                            }
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
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Product/ProductAutoGenerateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            root.product.code = response.data;
                            root.rendered++;

                        }
                    });
            },
            GetLastDetails: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/CompanyAccountSetupDetails', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.product.taxMethod = response.data.taxMethod;
                        root.product.taxRateId = response.data.taxRateId;
                    }
                });
            },
        },
        created: function () {
            this.product= this.newproduct;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory') == 'true' ? true : false;

            if (this.product.id == '00000000-0000-0000-0000-000000000000' || this.product.id == undefined || this.product.id == '')
                this.AutoIncrementCode();
            this.GetLastDetails();
        }
    }
</script>
