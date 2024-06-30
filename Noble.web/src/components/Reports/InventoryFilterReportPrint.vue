<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-7" style="background-color:white">
            <!--HEADER-->
            <div class="border border-dark col-md-12 mb-1 " style="height:40mm;background-color:white" v-if="isHeaderFooter=='true'">
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
                            <span style="font-size:14px;">{{ $t('InventoryFilterReportPrint.StockReport') }}</span>
                        </p>
                    </div>
                    <div class="col-md-4 " style="height:40mm;">
                        <table class="m-auto" v-if="arabic=='true'">
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
            <div style="height:90mm;background-color:white" v-else></div>
            <!--INFORMATION-->
            <div class="border border-dark col-md-12 my-1 " style="height:200mm;background-color:white">
                <br />
                <div class="row ">
                    <h6 class="m-auto">
                        {{dates}}
                    </h6>
                    <h6 class="m-auto" v-if="isShown && list.inventoryList.length > 0">
                        {{ $t('InventoryFilterReportPrint.WareHouseName') }}: {{list.inventoryList[0].wareHouseName}}
                    </h6>
                    <table class="table col-md-11 m-auto">
                        <tr class="heading" style="font-size:14px;">
                            <th class="text-left">#</th>
                            <th class="text-left">{{ $t('InventoryFilterReportPrint.Date') }}</th>
                            <th class="text-left">{{ $t('InventoryFilterReportPrint.TransactionType') }}</th>
                            <th class="text-left">{{ $t('InventoryFilterReportPrint.ProductName') }}</th>
                            <th v-if="!isShown" class="text-left">{{ $t('InventoryFilterReportPrint.WareHouseName') }}</th>
                            <th class="text-right">{{ $t('InventoryFilterReportPrint.QuantityIn') }}</th>
                            <th class="text-right">{{ $t('InventoryFilterReportPrint.QuantityOut') }}</th>
                        </tr>
                        <tr style="font-size:13px;" v-for="(item, index) in list.inventoryList" v-bind:key="item.id">
                            <td class="text-left">{{index+1}}</td>
                            <td class="text-left">{{convertDate(item.date)}}</td>
                            <td class="text-left">{{item.transactionType}}</td>
                            <td class="text-left"><span v-if="language=='en'">{{item.productName}}</span><span v-else>{{item.productNameArabic}}</span> </td>
                            <td v-if="!isShown" class="text-left">
                                <span v-if="language=='en'">{{item.wareHouseName}}</span>
                                <span v-else>{{item.wareHouseNameArabic}}</span><br />
                            </td>
                            <td class="text-center" v-if="isMultiUnit == 'true'">
                                {{item.transactionType=='StockOut'?'--':item.transactionType=='StockIn'?item.highQtyIn:item.transactionType=='SaleInvoice'?'--':item.transactionType=='PurchaseReturn'?item.highQtyOut:item.transactionType=='PurchasePost'?item.highQtyIn:item.transactionType=='CashReceipt'?'--':item.transactionType=='CashReceipt'?'--':''}}
                            </td>
                            <td class="text-center" v-else>
                                {{item.transactionType=='StockOut'?'--':item.transactionType=='StockIn'?item.quantityIn:item.transactionType=='SaleInvoice'?'--':item.transactionType=='PurchaseReturn'?item.quantityOut:item.transactionType=='PurchasePost'?item.quantityIn:item.transactionType=='CashReceipt'?'--':item.transactionType=='CashReceipt'?'--':''}}
                            </td>
                            <td class="text-center" v-if="isMultiUnit == 'true'">
                                {{item.transactionType=='StockOut'?item.highQtyOut:item.transactionType=='StockIn'?'--':item.transactionType=='SaleInvoice'?item.highQtyOut:item.transactionType=='PurchaseReturn'?'--':item.transactionType=='PurchasePost'?'--':item.transactionType=='CashReceipt'?'--':item.transactionType=='CashReceipt'?'--':''}}
                            </td>
                            <td class="text-center" v-else>
                                {{item.transactionType=='StockOut'?item.quantityOut:item.transactionType=='StockIn'?'--':item.transactionType=='SaleInvoice'?item.quantityOut:item.transactionType=='PurchaseReturn'?'--':item.transactionType=='PurchasePost'?'--':item.transactionType=='CashReceipt'?'--':item.transactionType=='CashReceipt'?'--':''}}
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--INFORMATION FOOTER-->
            <div class="col-md-12" style="height:30mm;background-color:white">
                <div class="row">
                    <table class="table text-center">
                        <tr>
                            <td>
                                {{ $t('InventoryFilterReportPrint.ReceivedBy') }}
                            </td>
                            <td>
                                {{ $t('InventoryFilterReportPrint.ApprovedBy') }}
                            </td>
                            <td>
                                {{ $t('InventoryFilterReportPrint.PreparedBy') }}
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
            <!--FOOTER-->
            <!--<div class="border border-dark col-md-12 " style="height: 35mm;">
        <div class="row">
            <div class="col-md-3 text-center" style="height: 35mm;">
                <u><b><span style="font-size:16px;">Terms & Conditions</span></b></u><br />
                <span style="font-size:11px;">
                    All Goods are received in perfect condition.<br />
                    Goods received will not be return or replace.<br />
                    Warranty Claim by Authorized Dealers.<br />
                    Warranty does not cover Consumeables.<br />
                    Paid amount is not refundable.
                </span>
            </div>
            <div class="col-md-6  text-center " style="height: 35mm;">
                <p style="padding-top:15px;">
                    Warranty period 5 days after receiving the goods.
                </p>
            </div>
            <div class="col-md-3 text-center" style="height: 35mm;">
                <u><b><span style="font-size:16px;">البنود و الظروف</span></b></u><br />
                <span class=" text-center" style="font-size:11px;">
                    يتم استلام جميع البضائع في حالة ممتازة.<br />
                    لن يتم إرجاع البضائع المستلمة أو استبدالها.<br />
                    مطالبة الضمان من قبل التجار المعتمدين.<br />
                    الضمان لا يغطي السلع الاستهلاكية.<br />
                    المبلغ المدفوع غير قابل للاسترداد.
                </span>
            </div>
        </div>
    </div>-->
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    export default {
        props: ['printDetails', 'isPrint', 'isShown', 'dates', 'headerFooter'],
        data: function () {
            return {
                list: {
                    inventoryList:
                        [
                            
                        ]
                },
                Print: false,
                render: 0,
                isMultiUnit: '',
                language: '',
                isHeaderFooter: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                arabic: '',
                english: '',
            }
        },
        mounted: function () {
            var getLocale = this.$i18n.locale;
            this.language = getLocale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.headerFooters = this.headerFooter;
            var root = this;
            root.Print = root.isPrint;
            if(this.printDetails.length > 0 && root.Print){
                this.list.inventoryList = this.printDetails;
                setTimeout(function() {
                    root.printInvoice();
                }, 125)
            }
        },
        methods: {
            convertDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
            printInvoice: function (){
                this.$htmlToPaper('inventoryDetailReport');
            }
        }
    }
</script>
