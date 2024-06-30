<template>
    <div>
        <div hidden id='productPrint' class="col-md-7">
            <!--HEADER-->
            <div class="border border-dark col-md-12 mb-1 " style="height:40mm;">
                <div class="row">
                    <div class="col-md-4 text-center " style="height:40mm;">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
                                        <span style="font-size:19px;">{{headerFooters.company.nameEnglish}}</span><br />
                                        <span style="font-size:13px;">{{headerFooters.company.categoryEnglish}}</span><br />
                                        <span style="font-size:13px;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:12px;">{{headerFooters.company.companyNameEnglish}}</span><br />
                                        <span style="font-size:11px;">
                                            {{headerFooters.company.addressEnglish}}<br />
                                            Tel: {{headerFooters.company.phoneNo}}
                                        </span>
                                    </p>
                                </td>

                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 text-center my-5" style="height:40mm;">

                        <img src="noblepos-retail.png" style="width:100%; max-width:300px;">
                        <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                            <span style="font-size:14px;">{{ $t('ProductPrintReport.ProductDetail') }}</span>
                        </p>

                    </div>
                    <div class="col-md-4 " style="height:40mm;">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
                                        <span style="font-size:19px;">{{headerFooters.company.nameArabic}}.</span><br />
                                        <span style="font-size:13px;">{{headerFooters.company.categoryArabic}}</span><br />
                                        <span style="font-size:13px;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:12px;">{{headerFooters.company.companyNameArabic}}</span><br />
                                        <span style="font-size:11px;">
                                            {{headerFooters.company.addressArabic}}<br />
                                            هاتف: {{headerFooters.company.phoneNo}}:
                                        </span>
                                    </p>
                                </td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="card-header p-0">


                                <div class="row">
                                    <div class="col-sm-8">
                                        <h5> {{product.code}}</h5>       
                                    </div>

                                    <div class="col-sm-4" :key="renderedImage">
                                        <productimage v-bind:path="image" />
                                    </div>
                                </div>


                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-sm-6">

                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.ProductNameEn'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.englishName}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.ProductNameAr'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.arabicName}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.ProductSubcategory'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.subCategoryNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.ProductSubcategory'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.subCategoryNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.Unit'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.unitNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.Unit')) }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.unitNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isMultiUnit=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.LevelOneUnit') }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.levelOneUnit}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.Color'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.colorNameEn}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.Color'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.colorNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.salePrice'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.salePrice}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.PackSize')}} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>{{product.length}} * {{product.width}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.Assortment')}} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>{{product.assortment}}</span>
                                                </div>

                                            </div>
                                            <!--<div class="row">
                                    <div class="col-sm-6">
                                        <label class="text  font-weight-bolder ">{{ $t('Product.IsSaleReturnDays') }} : </label>

                                    </div>
                                    <div class="col-sm-6">
                                        <span>{{isSaleReturn==true?$t('Active'):$t('De-Active')}}</span>
                                    </div>

                                </div>-->
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.ExpiryDate') }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>{{product.isExpire==true?$t('ProductPrintReport.Active'):$t('ProductPrintReport.De-Active')}}</span>
                                                </div>

                                            </div>





                                        </div>






                                        <div class="col-sm-6">

                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.ProductCategory'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.categoryNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.ProductCategory'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.categoryNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.ProductBrand'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.brandNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.ProductBrand'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.brandNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.Size'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.sizeNameEn}}</span>
                                                </div>
                                            </div>
                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.Size'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.sizeNameAr}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="isMultiUnit=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.UnitPerPack') }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.unitPerPack}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="english=='true'">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$englishLanguage($t('ProductPrintReport.Origin'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.originNameEn}}</span>
                                                </div>
                                            </div>

                                            <div class="row" v-if="isOtherLang()">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('ProductPrintReport.Origin'))  }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.originNameAr}}</span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.MinStockLevel')}} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.stockLevel}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.Shelf/Location')}} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.shelf}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.StyleNumber')}} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.styleNumber}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.SaleReturnDays') }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>  {{product.saleReturnDays}}</span>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.Active') }} : </label>

                                                </div>
                                                <div class="col-sm-6">
                                                    <span>{{product.isActive==true?$t('ProductPrintReport.Active'):$t('ProductPrintReport.De-Active')}}</span>
                                                </div>

                                            </div>




                                        </div>








                                    </div>
                                    <div class="row" :key="render+'ass'">
                                        <div class="col-sm-6">
                                            <label>{{ $t('ProductPrintReport.Barcode') }} :</label>

                                        </div>
                                        <div class="col-sm-6">
                                            <barcode :width="auto" :height="50" v-bind:value="product.barcode"></barcode>
                                        </div>

                                    </div>
                                    <div class="row" style="background-color: rgb(251, 251, 251); font-size:17px !important ">

                                        <div class="col-sm-4">
                                            <label class="text  font-weight-bolder ">{{ $t('ProductPrintReport.Description')}} : </label>

                                        </div>
                                        <div class="col-sm-8">
                                            <span>  {{product.description}}</span>
                                        </div>

                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <!--<div class="col-md-12  " style="height:30mm;">
                <div class="row">
                    <table class="table text-center">
                        <tr>
                            <td>
                                {{ $t('AllReports.ApprovedBy') }}:
                            </td>
                            <td>
                                {{ $t('AllReports.PreparedBy') }}:
                            </td>
                        </tr>

                    </table>
                </div>
            </div>-->

        </div>
    </div>

</template>



<style scoped>
    #font11 {
        font-size: 11px;
        line-height: 0;
    }

    #font16 {
        font-size: 16px;
    }
</style>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Vue3Barcode from 'vue3-barcode'


    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'newheaderFooter'],

        components: {
            'barcode': Vue3Barcode,
        },
        data: function () {

            return {
                headerFooter:this.newheaderFooter,
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
                headerFooters: {
                    company: ''
                }
            }
        },
        created() {
            this.headerFooters = this.headerFooter;
           
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
                this.isRaw = localStorage.getItem('IsProduction');
            
            if (this.printDetails != undefined) {
                this.product = this.printDetails;
                this.render++;
                this.isDisable = true;
                this.isEnabled = true;
                if (this.product.saleReturnDays != null && this.product.saleReturnDays != '' && this.product.saleReturnDays != 0) {
                    this.isSaleReturn = true;
                }

                this.getBase64Image(this.product.image);
            }
        },

        filters: {},
        methods: {
            PrintInvoice: function () {
                this.$htmlToPaper('productPrint');
            },
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
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

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
            var root = this;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            var getLocale = this.$i18n.locale;
            this.language = getLocale;
            setTimeout(function () {
                root.PrintInvoice();
            }, 125)
            //if (token == '') {
            //    this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            //    this.isRaw = localStorage.getItem('IsProduction');
            //}
            //if (this.$route.query.data == undefined) {
            //    this.AutoIncrementCode();
            //    this.GetLastDetails();
            //}

            //if (this.$route.query.data != undefined) {

            //    this.product = this.$route.query.data;
            //    this.render++;
            //    this.isDisable = true;
            //    this.isEnabled = true;
            //    if (this.product.saleReturnDays != null && this.product.saleReturnDays != '' && this.product.saleReturnDays != 0) {
            //        this.isSaleReturn = true;
            //    }

            //    this.getBase64Image(this.product.image);
            //}
        }
    }

</script>