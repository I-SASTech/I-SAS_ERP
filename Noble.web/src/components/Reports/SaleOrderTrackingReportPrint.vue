<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-12">
            <div class="border border-dark col-md-12 mb-1 " style="height:40mm;" v-if="isHeaderFooter == 'true'">
                <div class="row">
                    <div class="col-md-4 text-center">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p>
                                        <span style="font-size:25px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.nameEnglish
                                        }}</span><br />
                                        <span style="font-size:17px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.categoryEnglish
                                        }}</span><br />
                                        <span style="font-size:16px;color:black !important;font-weight:bold;">VAT No.:
                                            {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                        <span style="font-size:16px;color:black !important;font-weight:bold;">Cr
                                            No.:{{ headerFooters.company.companyRegNo }}</span><br />
                                        <span style="font-size:15px;color:black !important;font-weight:bold;">
                                            Tel: {{ headerFooters.company.phoneNo }}
                                        </span>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                        <img :src="headerFooters.company.logoPath"
                            style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                        <p
                            style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family:'Times New Roman', Times;">
                            <span style="font-size:14px;">Sale Order Tracking
                                            Report</span>
                        </p>
                    </div>
                    <div class="col-md-4 " style="height:40mm;">
                        <table class="text-right" v-if="arabic == 'true'">
                            <tr>
                                <td>
                                    <p>
                                        <span style="font-size:25px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.nameArabic
                                        }}.</span><br />
                                        <span style="font-size:17px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.categoryArabic
                                        }}</span><br />
                                        <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة
                                            القيمة المضافة: {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                        <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل
                                            التجاري :{{ headerFooters.company.companyRegNo }}</span><br />
                                        <span style="font-size:15px;color:black !important;font-weight:bold;">
                                            هاتف: {{ headerFooters.company.phoneNo }}:
                                        </span>
                                    </p>
                                </td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="height:45mm;" v-else></div>

            <div class="border border-dark" style="height:15mm;margin-top:1mm;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:10mm;font-size:12px;">
                        <div class="row">
                            <div class="col-md-2 text-center font-weight-bold">From Date:</div>
                            <div class="col-md-2 text-center font-weight-bold">{{ list.fromDate }}</div>
                            <div class="col-md-2 text-center font-weight-bold" v-if="arabic == 'true'">:من التاريخ</div>

                            <div class="col-md-2 text-center font-weight-bold">To Date:</div>
                            <div class="col-md-2 text-center font-weight-bold">{{ list.toDate }}</div>
                            <div class="col-md-2 text-center font-weight-bold" v-if="arabic == 'true'">:حتى الآن</div>
                        </div>
                    </div>
                </div>
            </div>
            <!--INFORMATION-->
           
            <div >
                <div class=" col-md-12">
                    <div class="row ">
                        <table class="table col-md-12 m-auto">
                            <tr class="heading" style="font-size:13px;">
                                <th class="text-left" style="width:4%">#</th>
                                <th class="text-left" style="width:10%">SOT No</th>
                                <th class="text-left" style="width:10%">Emp Name</th>
                                <th class="text-left" style="width:20%">Pro EN</th>
                                <th class="text-left" style="width:16%">Pro AN</th>
                                <th class="text-left" style="width:10%">Pro Code</th>
                                <th class="text-left" style="width:10%">Qty</th>
                                <th class="text-left" style="width:10%">Unit Price</th>
                                <th class="text-left" style="width:10%">Total</th>
                            </tr>
                        </table>
                        <table class="table col-md-12 m-auto" v-for="(purchase) in list.inventoryList" v-bind:key="purchase.id">
                            <tr style="font-size:13px;" v-for="(item) in purchase.saleOrderItem"
                                v-bind:key="item.id">
                                <td class="text-left" style="width:4%">{{ item.number }}</td>
                                <td class="text-left" style="width:10%">{{ purchase.registrationNumber }}</td>
                                <td class="text-left" style="width:10%">{{ purchase.employeeName }}</td>
                                <td class="text-left" style="width:20%">{{ item.productNameEn }}</td>
                                <td class="text-left" style="width:16%">{{ item.productNameAr }}</td>
                                <td class="text-left" style="width:10%">{{ item.code }}</td>
                                <td class="text-left" style="width:10%">{{ item.quantity }}</td>
                                <td class="text-left" style="width:10%">{{ item.unitPrice }}</td>
                                <td class="text-left" style="width:10%">{{ item.total }}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <!--INFORMATION FOOTER-->
        </div>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    props: ['printDetails', 'isPrint', 'isShown', 'headerFooter', 'toDate', 'fromDate'],
    data: function () {
        return {
            arabic: '',
            english: '',
            language: '',
            isMultiUnit: '',
            isHeaderFooter: '',
            list: {
                toDate: '',
                fromDate: '',
                inventoryList:
                    [

                    ]
            },
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
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.isMultiUnit = localStorage.getItem('IsMultiUnit');

        var root = this;
        this.language = this.$i18n.locale;
        this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
        root.Print = root.isPrint;

        if (this.printDetails.length > 0) {
            this.headerFooters = this.headerFooter;
            this.list.inventoryList = this.printDetails;
            this.list.fromDate = this.fromDate;
            this.list.toDate = this.toDate ;

            setTimeout(function () {
                root.printInvoice();
            }, 125)
        }
    },
    methods: {
        printInvoice: function () {
            this.$htmlToPaper('inventoryDetailReport');
        }
    }
}
</script>