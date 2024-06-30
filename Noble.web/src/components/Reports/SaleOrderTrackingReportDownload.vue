<template>
    <div>
        <div ref="mychildcomponent" hidden id='inventoryDetailReport' class="col-md-12">
            <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter == 'true'">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-borderless">
                            <tr>
                                <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                    <p class="mb-0">
                                        <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.nameEnglish
                                        }}</span><br />
                                        <span style="font-size:15px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.categoryEnglish
                                        }}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.:
                                            {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">Cr
                                            No.:{{ headerFooters.company.companyRegNo }}</span><br />
                                        <span style="font-size:13px;color:black !important;font-weight:bold;">
                                            Tel: {{ headerFooters.company.phoneNo }}
                                        </span>
                                    </p>
                                </td>
                                <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                    <img :src="headerFooters.company.logoPath"
                                        style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                                </td>
                                <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                    <p class="text-right mb-0">
                                        <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.nameArabic
                                        }}.</span><br />
                                        <span style="font-size:15px;color:black !important;font-weight:bold;">{{
                                                headerFooters.company.categoryArabic
                                        }}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة
                                            القيمة المضافة: {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل
                                            التجاري :{{ headerFooters.company.companyRegNo }}</span><br />
                                        <span style="font-size:13px;color:black !important;font-weight:bold;">
                                            هاتف: {{ headerFooters.company.phoneNo }}:
                                        </span>
                                    </p>
                                </td>
                            </tr>

                            <tr>

                                <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">

                                    <div style="text-align: center;">
                                        <span style="font-size:20px;color:black;font-weight:bold;">Sale Order Tracking
                                            Report</span>
                                    </div>


                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="height:45mm;" v-else></div>

            <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table table-borderless">
                            <!--Row 1-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">
                                    From Date:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">
                                    {{ list.fromDate }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">
                                    :من التاريخ</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">
                                    To Date:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">
                                    {{ list.toDate }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">
                                    :حتى الآن</td>
                            </tr>
                        </table>
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
        </div>
    </div>
</template>

<script>
import moment from "moment";
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    props: ['printDetails', 'isPrint', 'isShown', 'headerFooter', 'toDate', 'fromDate'],
    data: function () {
        return {
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

            var form = new FormData();
            form.append('htmlString', this.$refs.mychildcomponent.innerHTML);
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.post('/Report/PrintPdf', form, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                .then(function (response) {
                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;
                    var date = moment().format('DD MMM YYYY');
                    link.setAttribute('download', 'Sale Order Tracking' + date + '.pdf');
                    document.body.appendChild(link);
                    link.click();

                    root.$emit('close');
                });
            /*this.$htmlToPaper('inventoryDetailReport');*/
        }
    }
}
</script>