<template>
    <div class="row mt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-if=" isValid('CanViewDetailItem') ">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="card-header p-0">


                                <div class="row">
                                    <div class="col-sm-8 ">
                                        <h3 class="page_title"> {{ $t('ProductView.ProductDetail')}} {{product.code}}</h3>       
                                    </div>

                                    <div class="col-sm-4">
                                       
                                    </div>
                                </div>


                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">

                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.ProductNameEn'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.englishName}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.ProductNameAr'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.arabicName}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.ProductSubcategory'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.subCategoryNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.ProductSubcategory'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.subCategoryNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.Unit'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.unitNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.Unit'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.unitNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isMultiUnit=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.LevelOneUnit') }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.levelOneUnit}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.Color'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.colorNameEn}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.Color'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.colorNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.salePrice'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.salePrice}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.PackSize')}} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>{{product.length}} * {{product.width}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.Assortment')}} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>{{product.assortment}}</span>
                                                </div>

                                            </div>
                                            <!--<div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <label class="text  font-weight ">{{ $t('Product.IsSaleReturnDays') }} : </label>

        </div>
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <span>{{isSaleReturn==true?$t('Active'):$t('De-Active')}}</span>
        </div>

    </div>-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.ExpiryDate') }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>{{product.isExpire==true?$t('ProductView.Active'):$t('ProductView.De-Active')}}</span>
                                                </div>

                                            </div>
                                            <div class="row" :key="render+'ass'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.Barcode') }} :</label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 text-left">
                                                    <barcode :width="auto" :height="30" v-bind:value="product.barcode"></barcode>
                                                </div>

                                            </div>





                                        </div>






                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">

                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.ProductCategory'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.categoryNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.ProductCategory'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.categoryNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.ProductBrand'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.brandNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.ProductBrand'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.brandNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.Size'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.sizeNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.Size'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.sizeNameAr}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="isMultiUnit=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.UnitPerPack') }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.unitPerPack}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="english=='true'">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$englishLanguage($t('ProductView.Origin'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.originNameEn}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{$arabicLanguage($t('ProductView.Origin'))  }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.originNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.MinStockLevel')}} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.stockLevel}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.Shelf/Location')}} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.shelf}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.StyleNumber')}} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.styleNumber}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.SaleReturnDays') }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>  {{product.saleReturnDays}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="text  font-weight ">{{ $t('ProductView.Active') }} : </label>

                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <span>{{product.isActive==true?$t('ProductView.Active'):$t('ProductView.De-Active')}}</span>
                                                </div>

                                            </div>




                                        </div>








                                    </div>
                                    <!--<div class="row" :key="render+'ass'">
                                        <div class="col-sm-4">
                                            <label class="text  font-weight ">{{ $t('Product.Barcode') }} :</label>

                                        </div>
                                        <div class="col-sm-4">
                                            <barcode :width="auto" :height="30" v-bind:value="product.barcode"></barcode>
                                        </div>

                                    </div>-->
                                <div class="row" style=" font-size:17px !important ">

                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                        <label class="text  font-weight ">{{ $t('ProductView.Description')}} : </label><br />
                                        <textarea class="form-control" style="height:164px !important;max-height:164px !important" v-model=" product.description"></textarea>

                                    </div>
                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 ">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 card" style="height: 200px;padding:15px" :key="renderedImage">
                                                <AddProductImage v-bind:disable="true" v-on:picPath="getImage" v-bind:path="image" />
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 text-right mt-3">
                                        <router-link :to="'/products'"><a href="javascript:void(0)" class="btn btn-danger "> {{ $t('ProductView.Close') }}</a></router-link>

                                    </div>
                                </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div v-else>  <acessdenied></acessdenied></div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Vue3Barcode from 'vue3-barcode'


    export default {
        mixins: [clickMixin],

        components: {
            'barcode': Vue3Barcode,
        },
        data: function () {

            return {
                arabic: '',
                english: '',
                isRaw: '',
                isMultiUnit: '',
                subCategoryOptions: [],
                value: '',
                roles: [],
                options: ['Wholesaler', 'Retailer', 'Dealer', 'Distributer'],
                active: 'personal',
                rendered: 0,
                storedColors: [],
                renderedImage: 0,
                language: 'Nothing',
                product: {
                    id: '00000000-0000-0000-0000-000000000000',
                    image: '',
                    englishName: '',
                    arabicName: '',
                    categoryId: '',
                    barcode: '',
                    length: 1,
                    width: 1,
                    subCategoryId: '',
                    imagePath: '',
                    isActive: true,
                    isRaw: false,
                    levelOneUnit: '',
                    basicUnit: '',
                    unitPerPack: null,
                    salePriceUnit: '',
                    salePrice: 0,
                    unitId: '',
                    assortment: '',
                    styleNumber: '',
                },
                loading: false,
                catId: '',
                isCounter: 0,
                isEnabled: false,
                isDisable: false,
                isSaleReturn: false,
                randomNumber: '',
                render: 0,
                subCategoryId: [],
                decoded: [],
                permission: [],
                image: '',
                renderedTaxMethod: 0,
                show: false,
                type: '',
                newSubCategory: {
                    id: '',
                    code: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    categoryId: '',
                    isActive: true
                },
            }
        },

        filters: {},
        methods: {

            IsSave: function () {
                this.show = false;
                this.getSubcategory(this.product.categoryId);
            },

            unitPackSizeChange: function (value) {
                this.product.width = value;
            },


            focusOut: function () {

                return this.product.salePrice = this.product.salePrice.toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },


            changeValue: function (value) {
                if (value == false) {
                    this.product.saleReturnDays = '';
                }
            },
            getImage: function (value) {
                this.product.image = value;
            },


            generateBarcode: function (x) {
                if (x) {
                    this.randomNumber = 0; //multiply to generate random number between 0, 100
                    this.product.barcode = '';
                    this.isDisable = false
                    this.isEnabled = false
                }
                else {


                    this.randomNumber = Math.random() * 100; //multiply to generate random number between 0, 100
                    this.product.barcode = this.randomNumber
                    this.isDisable = true
                    this.isEnabled = true
                }

            },
            writeBarcode: function () {
                this.isDisable = true
                this.isEnabled = true

            },
            GoToProduct: function () {
                this.$router.push('/products');
            },
            makeActive: function (tab) {

                this.active = tab;
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
                        root.renderedTaxMethod++;
                    }
                });
            },

            getBase64Image: function (path) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.image = response.data;
                            root.renderedImage++
                        }
                    });
            }
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            var getLocale = this.$i18n.locale;
            this.language = getLocale;

            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.isRaw = localStorage.getItem('IsProduction');
            
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
                this.GetLastDetails();
            }

            if (this.$route.query.data != undefined) {

                this.product = this.$route.query.data;
                this.render++;
                this.isDisable = true;
                this.isEnabled = true;
                if (this.product.saleReturnDays != null && this.product.saleReturnDays != '' && this.product.saleReturnDays != 0) {
                    this.isSaleReturn = true;
                }

                this.getBase64Image(this.product.image);
            }
        }
    }

</script>
<style scoped>
    .font-weight {
        font-style: normal;
        font-weight: 600;
        font-size: 15px;
        line-height: 26px;
        letter-spacing: 0.01em;
        color: #35353D;
    }
   /* textarea.form-control {
        max-width: 100%;
        padding: 10px 10px 0 0;
        resize: none;
        border: none;
        border: none;
        border-radius: 0;
        line-height: 2;
    }*/
</style>