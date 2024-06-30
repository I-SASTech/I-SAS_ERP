<template>
    <div v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
        <div class="col-lg-6 col-sm-6 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6">
                            <h4 class="card-title DayHeading" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'" v-if="formName == 'Item'">{{ $t('ImportExportRecord.ImportItem') }}</h4>
                            <h4 class="card-title DayHeading" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'" v-if="formName == 'StockIn'">{{ $t('ImportExportRecord.ImportStockIn') }}</h4>
                            <h4 class="card-title DayHeading" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'" v-if="formName == 'Customer'">{{ $t('ImportExportRecord.ImportCustomer') }}</h4>
                            <h4 class="card-title DayHeading" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'" v-if="formName == 'Supplier'">{{ $t('ImportExportRecord.ImportSupplier') }}</h4>
                        </div>
                        <div class="col-md-6">
                            <h6 class="info-text" v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                                <xlsx-workbook>
                                    <xlsx-sheet :collection="sheet.data"
                                                v-for="sheet in sheets"
                                                :key="sheet.name"
                                                :sheet-name="sheet.name" />
                                    <xlsx-download :filename="formName == 'Item'?'Item Template.xlsx':formName == 'StockIn'?'Stock In Template.xlsx':formName == 'Customer'?'Customer Template.xlsx':formName == 'Supplier'?'Supplier Template.xlsx':'Wrong Template.xlsx'">
                                        <a class="btn btn-primary  btn-sm" style="color:#ffffff;" data-toggle="tooltip" data-placement="top" title="Download"><i class="fa fa-download"></i> {{ $t('ImportExportRecord.DownloadTemplate') }}</a>

                                    </xlsx-download>
                                </xlsx-workbook>
                            </h6>

                        </div>
                    </div>
                </div>
                <div class="card-body ">

                    <div class="row" :key="render">
                        <div class="col-lg-12 ml-auto mr-auto mb-2" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-if="formName == 'StockIn'">
                            <label>{{ $t('ImportExportRecord.WareHouse') }} :<span class="text-danger"> *</span></label>
                            <div>
                                <warehouse-dropdown v-model="wareHouseId" />
                            </div>
                        </div>
                        <div class="col-lg-12 ml-auto mr-auto">
                            <label>{{ $t('ImportExportRecord.File') }}</label>
                            <b-form-file v-model="file1"
                                         id="uplaodfile"
                                         :no-drop="true"
                                         accept=".xlsx"
                                         v-bind:class="$i18n.locale == 'en' ? '' : 'upload_css' "
                                         :state="Boolean(file1)"
                                         @change="onFileChanging"
                                         v-bind:placeholder="$t('ImportAttachment.ChooseFile')"></b-form-file>

                        </div>
                        <div class="col-lg-12 ml-auto mr-auto mt-4">
                            <b-progress :value="totalImportItem" :max="max" :label="'${((totalImportItem / max) * 100).toFixed(2)}%'" show-progress animated></b-progress>
                            <b-progress :max="totalImportRecord" height="15px" variant="success" :striped="striped">
                                <b-progress-bar :value="totalImportItem" :label="`${((totalImportItem / (totalImportRecord==0?1:totalImportRecord)) * 100).toFixed(0)}%`"></b-progress-bar>
                            </b-progress>
                        </div>

                        <div class="col-sm-12 col-md-12 col-lg-12 col-xl-12 ml-0 mr-0 mt-4 " v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                            <a href="javascript:void(0)" class="btn btnTotalHover " style="border-radius: 20px; background: #DDE9FF; color: #3178F6; ">{{ $t('ImportExportRecord.Total') }} <span>{{totalImportRecord}}</span></a>
                            <a href="javascript:void(0)" class="btn  btnUpdatedHover" style="border-radius: 20px; background: #B9E9C6; color: #198754; ">{{ $t('ImportExportRecord.Updated') }} <span>{{totalImportItem - errorCollection.length}}</span></a>
                            <a href="javascript:void(0)" class="btn  btnErrorHover" style="border-radius: 20px; background: #FEDCDC; color: #EB5757 ">{{ $t('ImportExportRecord.Error') }} <span>{{errorCollection.length}}</span></a>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-lg-6 mt-4">
                            <xlsx-workbook>
                                <xlsx-sheet :collection="sheet.data"
                                            v-for="sheet in errorSheets"
                                            :key="sheet.name"
                                            :sheet-name="sheet.name" />
                                <xlsx-download :filename="'Template.xlsx'">
                                    <a class="btn btn-outline-primary  btn-sm" data-toggle="tooltip" :disabled="isErrorFileDownload" data-placement="top" title="Download Error File"><i class="fa fa-download"></i> {{ $t('ImportExportRecord.ErrorFile') }}</a>

                                </xlsx-download>
                            </xlsx-workbook>
                        </div>
                        <div class="col-lg-6 mt-3 " v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                            <button class="btn btn-primary  "
                                    v-bind:class="{'disabled': (isUploadDisabled || ((formName == 'StockIn' )?(wareHouseId == null)?true:false:false )) }"
                                    @click="uploadFile">
                                <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('ImportExportRecord.Upload') }}
                            </button>
                            <button class="btn btn-danger   mr-2"
                                    v-on:click="onCancel">
                                {{ $t('ImportExportRecord.Cancel') }}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    //import { XlsxWorkbook, XlsxSheet, XlsxDownload } from "dist/vue3-xlsx.cjs.prod.js";
    
    import readXlsxFile from 'read-excel-file'
    export default {
        //components: {
        //    XlsxWorkbook,
        //    XlsxSheet,
        //    XlsxDownload
        //},
        data: function () {
            return {
                file1: null,
                loading: false,
                render: 0,
                year: '',
                fileInterval: '',
                sheets: [],
                collection: [],
                selectedFileData: [],
                isUploadDisabled: true,
                isErrorFileDownload: true,
                errorSheets: [],
                errorCollection: [],
                totalImportItem: 0,
                totalImportRecord: 0,
                wareHouseId: null,
                formName: ''
            }
        },
        methods: {

            onCancel: function () {
                var root = this
                if (this.formName == 'Item') {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                        text: "You will not be able to import item!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, Stop it!",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }).then(function (result) {
                        if (result.isConfirmed) {
                            root.$router.push('/products');

                        }

                    });
                }
                else if (this.formName == 'StockIn') {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                        text: "You will not be able to import item!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, Stop it!",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }).then(function (result) {
                        if (result.isConfirmed) {
                            root.$router.push({ path: '/stockValue', query: { formName: 'StockIn', token_name: 'WareHouse Management_token', fromDashboard: 'true' } });

                        }

                    });
                }
                else if (this.formName == 'Customer') {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                        text: "You will not be able to import item!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, Stop it!",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }).then(function (result) {
                        if (result.isConfirmed) {
                            root.$router.push('/Customer');
                        }

                    });
                }
                else if (this.formName == 'Supplier') {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                        text: "You will not be able to import item!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, Stop it!",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }).then(function (result) {
                        if (result.isConfirmed) {
                            root.$router.push('/supplier');
                        }

                    });
                }

            },
            onFileChanging: function (file) {
                this.errorSheets = []
                this.errorCollection = []
                var root = this;
                this.file1 = file.target.files ? event.target.files[0] : null;
                root.selectedFileData = []
                root.totalImportItem = 0;
                root.totalImportRecord = 0;
                root.isErrorFileDownload = true
                readXlsxFile(this.file1).then((allRows) => {
                    if (allRows.length > 1) {

                        if (root.formName == 'Item' && allRows[0].length <= 30 && allRows[0][0] == 'ProductNameEnglish') {
                            allRows.splice(0, 1)
                            allRows.forEach(function (data) {

                                root.selectedFileData.push({
                                    productNameEnglish: data[0],
                                    productNameArabic: data[1],
                                    itemNameEnglish: data[2],
                                    itemNameArabic: data[3],
                                    categoryNameEnglish: data[4],
                                    categoryNameArabic: data[5],
                                    subCategoryNameEnglish: data[6],
                                    subCategoryNameArabic: data[7],
                                    brandNameEnglish: data[8],
                                    brandNameArabic: data[9],
                                    originNameEnglish: data[10],
                                    originNameArabic: data[11],
                                    sizeNameEnglish: data[12],
                                    sizeNameArabic: data[13],
                                    colorNameEnglish: data[14],
                                    colorNameArabic: data[15],
                                    unitNameEnglish: data[16],
                                    unitNameArabic: data[17],
                                    salePrice: data[18],
                                    packSizeLength: data[19],
                                    packSizeWidth: data[20],
                                    minStockLevel: data[21],
                                    description: data[22],
                                    shelf: data[23],
                                    assortment: data[24],
                                    style: data[25],
                                    saleReturnDay: data[26],
                                    barCode: data[27],
                                    imagePath: data[28],
                                    rawMaterial: data[29]
                                })
                            })
                            root.totalImportRecord = allRows.length;
                            root.isUploadDisabled = false;
                        }
                        else if (root.formName == 'StockIn' && allRows[0].length === 5 && allRows[0][0] == 'ProductCode') {
                            allRows.splice(0, 1)
                            allRows = allRows.filter((e) => e[3] !== null && e[4] !== null)
                            allRows.forEach(function (data) {


                                root.selectedFileData.push({

                                    ProductCode: data[0],
                                    ProductNameEnglish: data[1],
                                    ProductNameArabic: data[2],
                                    Quantity: data[3],
                                    UnitPrice: data[4],
                                })


                            })


                            root.totalImportRecord = allRows.length;
                            root.isUploadDisabled = false;
                        }
                        else if ((root.formName == 'Customer' || root.formName == 'Supplier') && allRows[0].length === 16 && allRows[0][0] == 'Category') {
                            allRows.splice(0, 1)
                            allRows.forEach(function (data) {
                                root.selectedFileData.push({

                                    Category: data[0],
                                    EnglishName: data[1],
                                    ArabicName: data[2],
                                    CommercialRegistrationNo: data[3],
                                    VatNo: data[4],
                                    ContactPerson1: data[5],
                                    ContactPerson2: data[6],
                                    ContactNo1: data[7],
                                    Address: data[8],
                                    City: data[9],
                                    Remarks: data[10],
                                    CustomerType: data[11],
                                    Country: data[12],
                                    Telephone: data[13],
                                    Website: data[14],
                                    IsRaw: data[15],
                                })


                            })


                            root.totalImportRecord = allRows.length;
                            root.isUploadDisabled = false;
                        }
                        else {
                            root.file1 = null
                            root.$swal({
                                title: 'Wrong File',
                                text: "Please select correct file",
                                type: 'warning',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }

                    }
                })

            },
            uploadFile: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loding == true;

                root.isUploadDisabled = true;
                var url = ''
                var rows = ''
                if (root.formName == 'Item') {
                    rows = this.selectedFileData.splice(0, 100);
                    url = '/Product/UploadFilesForImportProduct'
                }
                else if (root.formName == 'StockIn') {
                    rows = this.selectedFileData.splice(0, 1000);
                    url = '/Product/UploadFilesForImportStock?warehouseId=' + this.wareHouseId
                }
                else if (root.formName == 'Customer') {
                    rows = this.selectedFileData.splice(0, 20);
                    url = '/Contact/UploadFilesForImport?isContact=' + true
                }
                else if (root.formName == 'Supplier') {
                    rows = this.selectedFileData.splice(0, 20);
                    url = '/Contact/UploadFilesForImport?isContact=' + false
                }
                root.$https.post(url, rows, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.totalImportItem += rows.length
                            if (response.data.length > 0) {

                                if (root.formName == 'Item') {
                                    response.data.forEach(function (x) {
                                        var errorData = {
                                            ProductNameEnglish: x.productNameEnglish,
                                            ProductNameArabic: x.productNameArabic,
                                            ItemNameEnglish: x.itemNameEnglish,
                                            ItemNameArabic: x.itemNameArabic,
                                            CategoryNameEnglish: x.categoryNameEnglish,
                                            CategoryNameArabic: x.categoryNameArabic,
                                            SubCategoryNameEnglish: x.subCategoryNameEnglish,
                                            SubCategoryNameArabic: x.subCategoryNameArabic,
                                            BrandNameEnglish: x.brandNameEnglish,
                                            BrandNameArabic: x.brandNameArabic,
                                            OriginNameEnglish: x.originNameEnglish,
                                            OriginNameArabic: x.originNameArabic,
                                            SizeNameEnglish: x.sizeNameEnglish,
                                            SizeNameArabic: x.sizeNameArabic,
                                            ColorNameEnglish: x.colorNameEnglish,
                                            ColorNameArabic: x.colorNameArabic,
                                            UnitNameEnglish: x.unitNameEnglish,
                                            UnitNameArabic: x.unitNameArabic,
                                            SalePrice: x.salePrice,
                                            PackSizeLength: x.packSizeLength,
                                            PackSizeWidth: x.packSizeWidth,
                                            MinStockLevel: x.minStockLevel,
                                            Description: x.description,
                                            Shelf: x.shelf,
                                            Assortment: x.assortment,
                                            Style: x.style,
                                            SaleReturnDay: x.saleReturnDay,
                                            BarCode: x.barCode,
                                            ImagePath: x.imagePath,
                                            RawMaterial: x.rawMaterial,
                                            ErrorDescription: x.errorDescription,
                                        }
                                        root.errorCollection.push(errorData)
                                    })
                                }
                                else if (root.formName == 'StockIn') {
                                    response.data.forEach(function (x) {
                                        var errorData = {
                                            ProductCode: x.productCode,
                                            ProductNameEnglish: x.productNameEnglish,
                                            ProductNameArabic: x.productNameArabic,
                                            Quantity: x.quantity,
                                            UnitPrice: x.unitPrice,
                                            ErrorDescription: x.errorDescription,
                                        }
                                        root.errorCollection.push(errorData)
                                    })
                                }
                                else if (root.formName == 'Customer' || root.formName == 'Supplier') {
                                    response.data.forEach(function (x) {
                                        var errorData = {
                                            Category: x.category,
                                            EnglishName: x.englishName,
                                            ArabicName: x.arabicName,
                                            CommercialRegistrationNo: x.commercialRegistrationNo,
                                            VatNo: x.vatNo,
                                            ContactPerson1: x.contactPerson1,
                                            ContactPerson2: x.contactPerson2,
                                            ContactNo1: x.contactNo1,
                                            Address: x.address,
                                            City: x.city,
                                            Remarks: x.remarks,
                                            CustomerType: x.customerType,
                                            Country: x.country,
                                            Telephone: x.telephone,
                                            Website: x.website,
                                            IsRaw: x.isRaw,
                                            ErrorDescription: x.errorDescription,
                                        }

                                        root.errorCollection.push(errorData)
                                    })
                                }
                                // root.errorCollection.push(response.data)

                            }
                            if (root.selectedFileData.length > 0) {
                                root.uploadFile()
                            }
                            else {
                                root.errorSheets.push({ name: "Template", data: [...root.errorCollection] });
                                if (root.errorCollection.length > 0) {
                                    root.isErrorFileDownload = false
                                }

                                root.file1 = null;

                            }
                        }

                    });

            },

            DownloadRecordForStockIn: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loding == true;
                root.isUploadDisabled = true;
                root.$https.get('/Product/DownloadStockFileAsync', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (response.data.length > 0) {
                                root.collection = []
                                // root.errorCollection.push(response.data)
                                response.data.forEach(function (x) {
                                    var data = {
                                        ProductCode: x.productCode,
                                        ProductNameEnglish: x.productNameEnglish,
                                        ProductNameArabic: x.productNameArabic,
                                        Quantity: x.quantity,
                                        UnitPrice: x.unitPrice,
                                    }
                                    root.collection.push(data)
                                })
                                root.sheets.push({ name: "Template", data: [...root.collection] });
                            }

                        }

                    });

            }
        },
        mounted: function () {
            if (this.$route.query.data == 'Item') {
                this.formName = 'Item'
                this.collection = ["ProductNameEnglish", "ProductNameArabic", "ItemNameEnglish", "ItemNameArabic", "CategoryNameEnglish", "CategoryNameArabic",
                    "SubCategoryNameEnglish", "SubCategoryNameArabic", "BrandNameEnglish", "BrandNameArabic", "OriginNameEnglish", "OriginNameArabic",
                    "SizeNameEnglish", "SizeNameArabic", "ColorNameEnglish", "ColorNameArabic", "UnitNameEnglish", "UnitNameArabic", "SalePrice",
                    "PackSizeLength", "PackSizeWidth", "MinStockLevel", "Description", "Shelf/Location", "Assortment", "Style/Model", "SaleReturnDay",
                    "BarCode", "ImagePath", "RawMaterial"]
                this.sheets = []
                this.sheets.push({ name: "Template", data: [this.collection] });
            }
            else if (this.$route.query.data == 'StockIn') {
                this.formName = 'StockIn'
                this.sheets = []
                this.DownloadRecordForStockIn()

            }
            else if (this.$route.query.data == 'Customer' || this.$route.query.data == 'Supplier') {
                this.formName = this.$route.query.data
                this.collection = ["Category", "EnglishName", "ArabicName", "CommercialRegistrationNo", "VatNo", "ContactPerson1",
                    "ContactPerson2", "ContactNo1", "Address", "City", "Remarks", "CustomerType",
                    "Country", "Telephone", "Website", "IsRaw"]
                this.sheets = []
                this.sheets.push({ name: "Template", data: [this.collection] });
            }
        }
    }
</script>

<style scoped>

    .btnTotalHover:hover {
        background-color: #DDE9FF !important;
        color: #3178F6 !important;
    }

    .btnUpdatedHover:hover {
        background-color: #B9E9C6 !important;
        color: #198754 !important;
    }

    .btnErrorHover:hover {
        background-color: #FEDCDC !important;
        color: #EB5757 !important;
    }
</style>


