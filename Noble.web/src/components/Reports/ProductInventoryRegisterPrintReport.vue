<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-7">
            <!--HEADER-->
            <div class="border border-dark col-md-12 mb-1 " style="height:40mm;">
                <div class="row">
                    <div class="col-md-4 text-center " style="height:40mm;">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
                                        <span style="font-size:14px;">Amir Ali Al-Shahri Trading Est.</span><br />
                                        <span style="font-size:19px;">System Computer</span><br />
                                        <span style="font-size:14px;">Wholesale and Retail</span><br />
                                        <span style="font-size:13px;">VAT No.: 300351958700003</span><br />
                                        <span style="font-size:12px;">P.O.Box: 41399 Jeddah 21521</span><br />
                                        <span style="font-size:11px;">
                                            Kindom of Saudi Arabia<br />
                                            Khalid Bin Waleed St.Jeddah<br />
                                            Tel: 35153147 / 6154848 Ext: 102, 103-Fax: 6578763
                                        </span>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 text-center my-5" style="height:40mm;">
                        <img src="https://www.sparksuite.com/images/logo.png" style="width:100%; max-width:300px;">
                        <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                            <span style="font-size:14px;">{{ $t('ProductInventoryRegisterPrintReport.ProductInventoryRegister') }}</span>
                        </p>
                    </div>
                    <div class="col-md-4 " style="height:40mm;">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
                                        <span style="font-size:14px;">مؤسسة امير علي الشهري التجارية.</span><br />
                                        <span style="font-size:19px;">كمبيوتر النظام</span><br />
                                        <span style="font-size:14px;">البيع بالجملة والتجزئة</span><br />
                                        <span style="font-size:13px;">رقم ضريبة القيمة المضافة: ۳۰۰۳۵۱۹۵۸۷۰۰۰۰۳</span><br />
                                        <span style="font-size:12px;">ص.ب: ۴۱۳۹۹ جدة ۲۱۵۲۱</span><br />
                                        <span style="font-size:11px;">
                                            المملكة العربية السعودية<br />
                                            شارع خالد بن الوليد ، جدة<br />
                                            هاتف: ۳۵۱۵۳۱۴۷/۶۱۵۴۸۴۸ تحويلة: ۱۰۲ ، ۱۰۳ - فاكس: ۶۵۷۸۷۶۳
                                        </span>
                                    </p>
                                </td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>           
            <!--INFORMATION-->
            <div class="border border-dark col-md-12 my-1 " style="height:200mm;">
                <br />
                <div class="row ">
                    <h6 class="m-auto">
                        {{dates}}
                    </h6>
                    <h6 class="m-auto" v-if="isShown && list.inventoryList.length > 0">
                        {{ $t('ProductInventoryRegisterPrintReport.WareHouseName') }}: {{list.inventoryList[0].wareHouseName}} || {{ $t('ProductInventoryRegisterPrintReport.ProductName') }} : {{list.inventoryList[0].productName}}
                    </h6>
                    <table class="table col-md-11 m-auto">
                        <tr class="heading" style="font-size:14px;">
                            <th class="text-left">#</th>
                            <th class="text-left">{{ $t('ProductInventoryRegisterPrintReport.Date') }}</th>
                            <th class="text-left">{{ $t('ProductInventoryRegisterPrintReport.TransactionType') }}</th>
                            <th v-if="!isShown" class="text-left">{{ $t('ProductInventoryRegisterPrintReport.ProductName') }}</th>
                            <th v-if="!isShown" class="text-left">{{ $t('ProductInventoryRegisterPrintReport.WareHouseName') }}</th>
                            <th class="text-right">{{ $t('ProductInventoryRegisterPrintReport.QuantityIn') }}</th>
                            <th class="text-right">{{ $t('ProductInventoryRegisterPrintReport.QuantityOut') }}</th>
                        </tr>
                        <tr style="font-size:13px;" v-for="(item, index) in list.inventoryList" v-bind:key="item.id">
                            <td class="text-left">{{index+1}}</td>
                            <td class="text-left">{{convertDate(item.date)}}</td>
                            <td class="text-left">{{item.transactionType}}</td>
                            <td v-if="!isShown" class="text-left">{{item.productName}}</td>
                            <td v-if="!isShown" class="text-left">{{item.wareHouseName }}</td>
                            <td class="text-right">
                                {{item.transactionType=='StockOut'?0:item.transactionType=='StockIn'?item.quantityIn:item.transactionType=='SaleInvoice'?0:item.transactionType=='PurchaseReturn'?item.quantityOut:item.transactionType=='PurchasePost'?item.quantityIn:item.transactionType=='CashReceipt'?0:item.transactionType=='CashReceipt'?0:''}}
                            </td>
                            <td class="text-right">
                                {{item.transactionType=='StockOut'?item.quantityOut:item.transactionType=='StockIn'?0:item.transactionType=='SaleInvoice'?item.quantityOut:item.transactionType=='PurchaseReturn'?0:item.transactionType=='PurchasePost'?0:item.transactionType=='CashReceipt'?0:item.transactionType=='CashReceipt'?0:''}}
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--INFORMATION FOOTER-->
            <div class="col-md-12 " style="height:40mm;">
                <div class="row">
                    <div class="col-md-4 p-0" style="height:10mm;">
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-12 border-dark border-top border-right border-left border-bottom" style="height:10mm;"> Total Quantity: {{ calulateTotalQty }}</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 p-0" style="height:10mm;">
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-12 border-dark border-top border-right border-left border-bottom" style="height:10mm;"> Total Quantity In: {{ calulateQtyIn - calulateQtyOut }}</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 p-0" style="height:10mm;">
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-12 border-dark border-top border-right border-left border-bottom" style="height:10mm;"> Total Quantity Out: {{ calulateQtyOut }}</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12  " style="height:30mm;">
                <div class="row">
                    <table class="table text-center">
                        <tr>
                            <td>
                                {{ $t('ProductInventoryRegisterPrintReport.ReceivedBy') }}:
                            </td>
                            <td>
                                {{ $t('ProductInventoryRegisterPrintReport.ApprovedBy') }}:
                            </td>
                            <td>
                                {{ $t('ProductInventoryRegisterPrintReport.PreparedBy') }}:
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
            <!--FOOTER-->
            <div class="border border-dark col-md-12 " style="height: 35mm;">
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
            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    export default {
        props:['printDetails', 'isShown', 'isPrint', 'dates'],
        data: function () {
            return {
                list: {
                    inventoryList:
                        [
                            
                        ]
                },
                Print: false,
                render: 0
            }
        },
        computed: {
            calulateQtyIn: function() {
                return this.list.inventoryList.reduce(function(a, c){return a + Number((c.transactionType=='StockOut'?0:c.transactionType=='StockIn'?c.quantityIn:c.transactionType=='SaleInvoice'?0:c.transactionType=='PurchaseReturn'?c.quantityOut:c.transactionType=='PurchasePost'?c.quantityIn:c.transactionType=='CashReceipt'?0:c.transactionType=='CashReceipt'?0:'') || 0)}, 0)
            },
            calulateQtyOut: function() {
                return this.list.inventoryList.reduce(function(a, c){return a + Number((c.transactionType=='StockOut'?c.quantityOut:c.transactionType=='StockIn'?0:c.transactionType=='SaleInvoice'?c.quantityOut:c.transactionType=='PurchaseReturn'?0:c.transactionType=='PurchasePost'?0:c.transactionType=='CashReceipt'?0:c.transactionType=='CashReceipt'?0:'') || 0)}, 0)
            },
            calulateTotalQty: function() {
                return this.list.inventoryList.reduce(function(a, c){return a + Number((c.transactionType=='StockOut'?0:c.transactionType=='StockIn'?c.quantityIn:c.transactionType=='SaleInvoice'?0:c.transactionType=='PurchaseReturn'?c.quantityOut:c.transactionType=='PurchasePost'?c.quantityIn:c.transactionType=='CashReceipt'?0:c.transactionType=='CashReceipt'?0:'') || 0)}, 0)
            }
        },
        mounted: function () {
            var root = this;
            
            this.Print = this.isPrint;
            if(this.printDetails.length > 0 && this.Print == true){
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
                this.Print = false;
            }
        }
    }
</script>


<style scoped>
    #font11 {
        font-size: 11px;
        line-height: 0;
    }

    #font16 {
        font-size: 16px;
    }
</style>