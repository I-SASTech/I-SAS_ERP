<template>
    <div>
        <div ref="mychildcomponent" hidden  class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
                <div class="col-md-12" style="height:60mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-borderless">
                                <tr>
                                    <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                        <p class="mb-0">
                                            <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                            <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:13px;color:black !important;font-weight:bold;">
                                                Tel: {{headerFooters.company.phoneNo}}
                                            </span>
                                        </p>
                                    </td>
                                    <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                        <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                                    </td>
                                    <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                        <p class="text-right mb-0">
                                            <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                            <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:13px;color:black !important;font-weight:bold;">
                                                هاتف: {{headerFooters.company.phoneNo}}:
                                            </span>
                                        </p>
                                    </td>
                                </tr>

                                <tr>

                                    <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">
                                        <div style="text-align: center;">
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">Product Ledger Report</span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(fromDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(toDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:حتى الآن</td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>

                <!--INFORMATION-->
                <div class="col-md-12 mt-4">
                    <div class="row">
                        <div class="col-md-12" v-for="product in list" v-bind:key="product.name">

                            <table class="table mb-0">
                                <thead class="thead-light">
                                    <tr>
                                        <td colspan="6">
                                            <h5>
                                                {{product.name}}
                                            </h5>
                                        </td>

                                    </tr>
                                    <tr>
                                        <th colspan="2" class="text-center">
                                            Inventory
                                        </th>

                                        <th colspan="2" class="text-center">
                                            COGS

                                        </th>
                                        <th colspan="2" class="text-center">
                                            Sale

                                        </th>


                                    </tr>
                                </thead>
                                <thead class="thead-light">
                                    <tr>
                                        <th class="text-center">
                                            Quantity
                                        </th>

                                        <th class="text-center">
                                            Balance

                                        </th>
                                        <th class="text-center">
                                            Quantity
                                        </th>

                                        <th class="text-center">
                                            Balance

                                        </th>
                                        <th class="text-center">
                                            Quantity
                                        </th>

                                        <th class="text-center">
                                            Balance

                                        </th>


                                    </tr>
                                </thead>
                                <tbody>


                                    <tr>
                                        <th class="text-center">
                                            {{product.quantity}}
                                        </th>

                                        <th class="text-center">
                                            {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }}  {{nonNegative(product.inventoryBalance)}}

                                        </th>
                                        <th class="text-center">
                                            {{product.quantity}}
                                        </th>

                                        <th class="text-center">
                                            {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }} {{nonNegative(product.cogsBalance)}}
                                        </th>
                                        <th class="text-center">
                                            {{product.quantity}}
                                        </th>

                                        <th class="text-center">
                                            {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }} {{nonNegative(product.saleBalance)}}

                                        </th>


                                    </tr>
                                </tbody>
                            </table>

                        </div>
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
        props: ['printDetails', 'headerFooter', 'formName', 'fromDate', 'toDate'],
       
        data: function () {
            return {
              
              
                isHeaderFooter: '',
                invoicePrint: '',
                arabic: '',
                english: '',
                list: [],
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                }
            }
        },
       
     
        methods: {

             nonNegative: function (value) {



                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            getDate: function (date) {
                return moment(date).format('l');
            },
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
                        link.setAttribute('download', 'Comparision Ledger Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                        root.$emit('close');
                    });
            },



        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            var root = this;
            if (this.printDetails.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

