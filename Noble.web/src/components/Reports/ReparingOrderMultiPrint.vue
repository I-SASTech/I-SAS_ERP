<template>
    <div>
        <div hidden id='reparingOrderthermal'>

            <div class="col-md-12 col-12  " style="height:45mm;background-color:white !important">
                <div style="background-color:white !important;display:flex">

                    <div style="background-color:white !important;width:40%">
                        <div style="background-color:white !important;width:100%">
                            <span style="font-size:15px;color:black !important;font-weight:500">{{headerFooters.company.addressEnglish}}</span>
                        </div>
                        <div style="background-color:white !important;width:100%">
                            <span style="font-size:15px;color:black !important;font-weight:500">{{headerFooters.company.nameEnglish}}</span>
                        </div>
                        <div class="row" style="background-color:white !important">

                            <div class="col-12">
                                <span style="font-size:15px;color:black !important;font-weight:500;">
                                    {{headerFooters.company.phoneNo}}
                                </span>
                            </div>

                            <div class="col-5 text-right mt-1">Customer:</div>
                            <div class="col-7 mt-1">
                                <span style="font-size:16px;color:black !important;font-weight:500;">
                                    <span>{{printDetails.customerNameEn}}</span>

                                </span>
                            </div>
                            <div class="col-5 text-right"></div>
                            <div class="col-7">
                                <span style="font-size:16px;color:black !important;font-weight:500;">
                                    <span>{{printDetails.mobile}}</span>

                                </span>
                            </div>
                            <div class="col-5 text-right mt-2">&nbsp;</div>
                            <div class="col-7 mt-2">
                                <span style="font-size:16px;color:black !important;font-weight:500;">
                                    <span>{{printDetails.address}}</span>

                                </span>
                            </div>


                        </div>

                    </div>
                    <div style="background-color:white !important;text-align:left !important;width:40%">
                        <img :src="headerFooters.company.logoPath" style="width:100%; height:180px; padding-top:5px !important; margin:0 !important">
                    </div>
                    <div style="text-align:right;background-color:white !important;width:40%">
                        <div class="row">
                            <div class="col-6 mb-1"></div>
                            <div class="col-12 mb-1">   <span style="font-size:13px;color:#979187">Record1</span></div>

                            <div class="col-12 mb-1">
                                <span style="border:1px solid;border-color:black !important;font-size:18px;font-weight:bold;padding:3px">Repair Job Receipt</span>
                            </div>
                            <div class="col-6 text-left"><span style="font-size:14px;color:black">Received Date:</span></div>
                            <div class="col-6 text-right "><span style="font-size:16px;color:black">{{convertMainDate(printDetails.date)}}</span></div>

                            <div class="col-6 " style="text-align:left;"> <span style="font-size:14px;color:black;margin-bottom:5px; ">Completion Date</span></div>
                            <div class="col-6" style="text-align:right;"><span style="font-size:16px;color:black">{{convertMainDate(printDetails.expectedDate)}}</span></div>

                            <div class="col-6  mt-2" style="text-align:left"> <span style="font-size:14px;color:black">Refrence No:</span></div>
                            <div class="col-6 mt-2" style="text-align:right"><span style="font-size:16px;color:black;font-weight:bold">{{printDetails.registrationNo}}</span></div>

                            <div class="col-6 text-left "> <span style="font-size:14px;color:black">Registered By:</span></div>
                            <div class="col-6 text-right "><span style="font-size:16px;color:black">{{printDetails.employeeNameEn}}</span></div>
                        </div>


                    </div>

                </div>



            </div>

            <div class="col-md-12 col-12 " style="height: 100mm;background-color:white !important;padding-top:35px !important ">
                <table class="table ">

                    <tr class="heading" style="font-size:14px;">
                        <th class="text-center" style="width:2%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            #
                        </th>
                        <th class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Ups Description</th>
                        <th class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Warranty Category</th>
                        <th class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Problem Issue</th>
                        <th class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Acessory Include</th>
                        <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Serial No</th>
                        <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Completion Date</th>
                        <th class="text-center" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">Estimate Amount</th>


                    </tr>
                    <tr class="heading" style="font-size:14px;" v-for="(order,index) in printDetails.multiUpsLineItems" v-bind:key="order.id">
                        <td style="width:2%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">{{index+1}}</td>
                        <td class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{order.upsDescription}}
                        </td>
                        <td class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{order.warrantyCategory}}
                        </td>

                        <td class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{order.problem}}
                        </td>
                        <td class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{order.acessoryIncluded}}
                        </td>
                        <td class="text-center" style="width:5%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{order.serialNo}}
                        </td>
                        <td class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{convertMainDate(order.completeDate)}}
                        </td>

                        <td class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid black !important;">
                            {{order.estimateAmount}}
                        </td>





                    </tr>


                </table>








            </div>

            <div class="col-md-12 col-12 mt-2 " style="height:40mm;padding-top :50px !important;background-color:white !important">

                <div class="row " style="background-color:white !important">
                    <div class="col-6 text-center ">
                        <div class="row" style="background-color:white !important">
                            <div class="col-8 ml-auto mt-auto">
                                <p style="font-size:16px;color:black;border-top:1px solid;width:70%"> (Signature)</p>

                            </div>

                        </div>


                    </div>
                    <div class="col-6 text-center ">
                        <div class="row">
                            <div class="col-8 ml-auto mt-auto">
                                <p style="font-size:16px;color:black;border-top:1px solid;width:90%;margin-bottom:0px !important;padding-bottom:0px !important"> (Customer Signature)</p>
                                <p style="font-size:12px;color:black;width:90%;margin-top:0px !important;padding-top:0px !important">(I have read and accept  Terms and Conditions)</p>

                            </div>

                        </div>


                    </div>

                </div>
                <div class="row  " style="background-color:white !important;border-color: black; border-width: 0.05px !important;border-bottom-style: dotted;">
                    <div class="col-12 text-right">
                        <p style="font-size:20px;color:black;font-weight:bold"><span>شرائط و ضوابط</span>  </p>

                    </div>
                    <div class="col-11 text-right">
                        <i style="font-size:15px;color:black;font-weight:bold"> براہ کرم مرمت کے لیے جمع کرانے کے 20 دنوں کے اندر مرمت کے تمام آئٹمز جمع کریں۔</i><br />
                        <i style="font-size:15px;color:black;font-weight:bold">براہِ کرم اس بات کو یقینی بنائیں کہ آپ مرمت کی مصنوعات حاصل کرنے سے پہلے پوری طرح مطمئن ہیں۔ ایک بار جب آپ اشیاء کو جمع کر لیتے ہیں تو ہم کسی بھی ناقص مرمت کے ذمہ دار نہیں ہوں گے۔ </i><br />
                        <p style="font-size:15px;color:#826C5F;text-align:left">{{printDetails.dates}}</p><br />

                    </div>
                    <div class="col-1 text-right">
                        <i style="font-size:15px;color:black;"> <span>-1</span></i><br />
                        <i style="font-size:15px;color:black;"><span>-2</span> </i><br />
                    </div>

                </div>
                <div class="row " style="background-color:white !important">
                    <div class="col-12 text-center"> <span style="font-size:17px;color:black;font-weight:bold">   لیتے وقت  مکمل تسلی  کر  لاۓ  ورنہ دوکاندار ذمہ دار نہ ہو گا     <span>UPS  اپنا</span></span></div>


                </div>






            </div>

        </div>

    </div>

</template>

<script>
    import moment from "moment";

    
    export default {

        props: ['newprintDetails', 'headerFooter', 'AddReparingOrder','isPrintThis'],
        data: function () {
            return {
                printDetails: this.newprintDetails,
                qrValue: "",
                rander: 0,
                isHeaderFooter: '',
                nameEnglish: '',
                invoicePrint: '',
                arabic: '',
                english: '',
                addressEnglish: '',
                render: 0,
                currency: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                }

            }
        },
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        computed: {

        },
        created: function () {
            this.headerFooters = this.headerFooter;
            

            if (this.printDetails.remarks == null) {
                this.printDetails.remarks = '    ';
            }


            this.currency = localStorage.getItem('currency');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');


        },
        mounted: function () {
            var root = this;
            root.printPDF();



        },
        methods: {
            convertMainDate: function (x) {

                if (x == undefined)
                    return '';

                return moment(x).format('d MMMM, YYYY');
            },



            async printPDF() {
                var root = this;
                const localOptions = {
                    styles: [
                        'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
                        'https://unpkg.com/kidlat-css/css/kidlat.css',
                    ],
                    autoClose: true,
                };

                await  this.$htmlToPaper('reparingOrderthermal', localOptions, () => {

                    
                    root.$router.push({
                        path: '/MultiReparingOrder',

                    });


                });

                //await this.$htmlToPaper("reparingOrderthermal", localOptions)

              


            }
        }
    }
</script>


<style scoped>
    @media print {
        @page {
            size: landscape;
        }
    }
</style>