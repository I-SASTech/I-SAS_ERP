<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-12">
            <div class="border border-dark col-md-12 mb-1 " style="height:40mm;" v-if="isHeaderFooter=='true'">
                <div class="row">
                    <div class="col-md-4 text-center">
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
                    <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                        <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                        <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                            <span style="font-size:14px;">{{ $t('RetailReportPrint.StockReport') }}</span>
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
            <div style="height:90mm;" v-else></div>

            <div class="border border-dark" style="height:15mm;margin-top:1mm;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:10mm;font-size:12px;">
                        <div class="row">
                            <div class="col-md-1">From Date:</div>
                            <div class="col-md-2">{{fromDate}}</div>
                            <div class="col-md-1">:من التاريخ</div>

                            <div class="col-md-1 text-center">To Date:</div>
                            <div class="col-md-2 text-center">{{toDate}}</div>
                            <div class="col-md-1">:حتى الآن</div>

                            <!--<div class="col-md-1 text-center">WareHouse:</div>
                            <div class="col-md-2 text-center"><span v-if="language=='en'">{{list.wareHouseName}}</span> <span v-else>{{list.wareHouseNameArabic}}</span> </div>
                            <div class="col-md-1">:مستودع</div>-->
                        </div>
                    </div>
                </div>
            </div>
            <!--INFORMATION-->
            <div class="border border-dark col-md-12 my-1 " style="height:350mm;">
                <div class="row ">
                    <table class="table col-md-12 m-auto">
                        <tr class="heading" style="font-size:14px;">
                            <th class="text-left">#</th>
                            <th>
                                {{ $t('RetailReportPrint.Date') }}
                            </th>
                            <th class="text-center">
                                {{ $t('RetailReportPrint.TotalCash') }}
                            </th>
                            <th class="text-center">
                                {{ $t('RetailReportPrint.TotalBank') }}
                            </th>
                            <th class="text-center">
                                {{ $t('RetailReportPrint.TotalCredit') }}
                            </th>
                            <th class="text-center">
                                {{ $t('RetailReportPrint.TotalExpense') }}
                            </th>
                            <th class="text-center">
                                {{ $t('RetailReportPrint.TotalVAT') }}
                            </th>
                        </tr>
                        <tr style="font-size:13px;" v-for="(inventory, index) in list" v-bind:key="inventory.id">
                            <td class="text-left">{{index+1}}</td>
                            <td>
                                <span>{{getDate(inventory.date)}}</span>
                            </td>
                            <td class="text-center">
                                {{inventory.totalCash}}
                            </td>
                            <td class="text-center">
                                {{inventory.totalBank}}
                            </td>
                            <td class="text-center">
                                {{inventory.totalCredit}}
                            </td>
                            <td class="text-center">
                                {{inventory.totalExpence}}
                            </td>
                            <td class="text-center">
                                {{inventory.totalVat *(-1)}}
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--INFORMATION FOOTER-->
        </div>
    </div>
</template>

<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],        props: ['printDetails', 'isPrint', 'isShown', 'headerFooter', 'fromDate','toDate'],
        data: function () {
            return {
                language: '',
                isMultiUnit:'',
                isHeaderFooter: '',
                list: [],
                Print: false,
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                }
            }
        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');

            var root = this;
            this.language = this.$i18n.locale;
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            root.Print = root.isPrint;
        
            if (this.printDetails.length > 0) {
                this.headerFooters = this.headerFooter;
                this.list = this.printDetails;

                setTimeout(function() {
                    root.printInvoice();
                }, 125)
            }
        },
        methods: {
            getDate: function (date) {

                return moment(date).format(' YYYY-MM-DD');
            },
            convertDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
            printInvoice: function (){
                this.$htmlToPaper('inventoryDetailReport');
            }
        }
    }
</script>
