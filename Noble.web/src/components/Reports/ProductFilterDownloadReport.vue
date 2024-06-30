<template>
    <div>
        <div ref="mychildcomponent" hidden id='productDownlaodReport' class="col-md-7">
            <!--HEADER-->
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
                                <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">Product Report</span>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>           
            <div class="col-md-12 ">
                <div class="row">

                    <table class="table col-md-12 m-auto">
                        <tr class="heading" style="font-size:14px;">
                            <th class="text-center" style="color:black !important;">#</th>
                            <th class="text-center" style="color:black !important;" >{{ $t('Product.Product') }} </th>
                            <th class="text-center" style="color:black !important;">{{ $t('Product.Item') }} </th>
                            <th class="text-center" style="color:black !important;">{{ $t('Product.Category') }} </th>
                            <th class="text-center" style="color:black !important;">{{ $t('Product.Color') }} </th>
                            <th class="text-center" style="color:black !important;">{{ $t('Product.Origin') }}</th>
                            <th class="text-center" style="color:black !important;">{{ $t('Product.SalePrice') }}</th>
                        </tr>
                        <tr style="font-size:13px; page-break-after: always;" v-for="(item, index) in list" v-bind:key="item.id">
                            <td class="text-center" style="color:black !important;">{{index+1}}</td>
                            <td class="text-center" style="color:black !important;" >{{item.masterProductEn}}</td>
                            <td class="text-center" style="color:black !important;">{{item.englishName}}</td>
                            <td class="text-center" style="color:black !important;">{{item.categoryNameEn}}</td>
                            <td class="text-center" style="color:black !important;">{{item.colorName}}</td>
                            <td class="text-center" style="color:black !important;">{{item.originNameEn}}</td>
                            <td class="text-center" style="color:black !important;">{{item.salePrice}}</td>
                            
                           
                        </tr>

                       
                    </table>
                   
                </div>
            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    export default {
        props: ['printDetails',  'headerFooter', ],
        data: function () {
            return {
               list:[],
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                Print: false,
                render: 0
            }
        },

        created: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            var root = this;

            if (this.printDetails.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

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


                        link.setAttribute('download', 'Product Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                        root.$emit('close');
                    });
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