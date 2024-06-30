<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important">
            <!--HEADER-->
            <div v-if="list.isGatePass">
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <table class="m-auto">
                                <tr>
                                    <td>
                                        <p class="text-center">
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                Tel: {{headerFooters.company.phoneNo}}
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                        </div>
                        <div class="col-md-4">
                            <table class="m-auto">
                                <tr>
                                    <td>
                                        <p class="text-center">
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                هاتف: {{headerFooters.company.phoneNo}}:
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p style="text-align: center; margin: 0px;; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Gate Pass</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:60mm;" v-else></div>

                <div style="height:30mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div>
                                <!--Row 1-->
                                <div class="row mt-2">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Gate Pass No:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.registrationNo}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:بوابة المرور لا</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;"> Contractor :</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (list.contractorName != '' && list.contractorName != null) ? list.contractorName : list.contractorNameArabic : (list.contractorNameArabic != '' && list.contractorNameArabic != null) ? list.contractorNameArabic : list.contractorName}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:مقاول</div>
                                    </div>
                                </div>
                                <!--Row 1-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.fromDates}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:من التاريخ</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.toDates}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">: حتى تاريخه</div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="display:flex;">
                                        <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;">Note:</div>
                                        <div style="width:76%;text-align:center;font-weight:bold;color:black !important;">{{list.note}}</div>
                                        <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;">: عنوان</div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="height:300mm;border:2px solid !important;">
                    <div class="row ">
                        <div class="col-md-12 ">
                            <table class="table">
                                <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="width:60%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">اسم الصنف</th>
                                    <th class="text-center" style="width:30%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                </tr>

                                <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-else>
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="width:60%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Product Name</th>
                                    <th class="text-center" style="width:30%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>

                                </tr>

                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.gatePassItems" v-bind:key="item.id">
                                    <td class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.product.englishName}} {{item.product.arabicName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.quantity }}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


                <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter!='true'">
                    <div class="row">
                        <table class="table text-center">
                            <tr>

                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('AllReports.PreparedBy') }}::{{userName}}
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('AllReports.ApprovedBy') }}
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
            <div>
                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <table class="m-auto">
                                <tr>
                                    <td>
                                        <p class="text-center">
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                Tel: {{headerFooters.company.phoneNo}}
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                        </div>
                        <div class="col-md-4">
                            <table class="m-auto">
                                <tr>
                                    <td>
                                        <p class="text-center">
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                هاتف: {{headerFooters.company.phoneNo}}:
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p style="text-align: center; margin: 0px;; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Ware House Pass</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:60mm;" v-else></div>

                <div style="height:30mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div>
                                <!--Row 1-->
                                <div class="row mt-2">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Pass No:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.registrationNo}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">: تمرير لا</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;"> Contractor :</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (list.contractorName != '' && list.contractorName != null) ? list.contractorName : list.contractorNameArabic : (list.contractorNameArabic != '' && list.contractorNameArabic != null) ? list.contractorNameArabic : list.contractorName}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:مقاول</div>
                                    </div>
                                </div>
                                <!--Row 1-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.fromDates}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:من التاريخ</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.toDates}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">: حتى تاريخه</div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="display:flex;">
                                        <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;">Note:</div>
                                        <div style="width:76%;text-align:center;font-weight:bold;color:black !important;">{{list.note}}</div>
                                        <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;">: عنوان</div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="height:300mm;border:2px solid !important;">
                    <div class="row ">
                        <div class="col-md-12 ">
                            <table class="table">
                                <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="width:60%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">اسم الصنف</th>
                                    <th class="text-center" style="width:30%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                </tr>

                                <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-else>
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="width:60%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Product Name</th>
                                    <th class="text-center" style="width:30%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>

                                </tr>

                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.gatePassItems" v-bind:key="item.id">
                                    <td class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.product.englishName}} {{item.product.arabicName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.quantity }}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


                <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter!='true'">
                    <div class="row">
                        <table class="table text-center">
                            <tr>

                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('AllReports.PreparedBy') }}::{{userName}}
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('AllReports.ApprovedBy') }}
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>

<script>
    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
            'https://unpkg.com/kidlat-css/css/kidlat.css'
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,

    }
    export default {
        props: ['printDetails', 'headerFooter'],
        data: function () {
            return {
                qrValue: "",
                IsExpenseAccount: false,
                isHeaderFooter: '',
                currency: '',
                invoicePrint: '',
                IsDeliveryNote: '',
                arabic: '',
                english: '',
                userName: '',

                list: {
                   
                },
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                htmlData: {
                    htmlString: ''
                }
            }
        },
        filters: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },
        methods: {
          

            printInvoice: function () {
                
                this.$htmlToPaper('purchaseInvoice', options, () => {

                });
            },

        },
        created: function () {
           
        },
        mounted: function () {

            this.currency = localStorage.getItem('currency');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.userName = localStorage.getItem('FullName');
            var root = this;
            
            if (this.printDetails != undefined) {
                this.headerFooters = this.headerFooter;
                this.list = this.printDetails;
                setTimeout(function () {
                    root.printInvoice();
                }, 125)

            }
        },

    }
</script>