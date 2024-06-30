<template>
    <modal :show="show" v-if=" isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf')|| isValid('CanReplyInquiryDashboard') ">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('EmailCompose.NewMessage') }}</h5>

                        </div>
                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <label>{{ $t('EmailCompose.To') }}: <span class="text-danger"> *</span></label>
                                        <multiselect v-model="emailCompoese.EmailTo" tag-placeholder="Add Email" placeholder="Search or add a tag" label="cc" track-by="id" :options="ccOptions" :multiple="true" :taggable="true" @tag="AddCCToList"></multiselect>

                                    </div>
                                   

                                    <div :key="render" class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> {{ $t('EmailCompose.Subject') }}:</label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="emailCompoese.subject" type="text" />

                                    </div>
                                    <div class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> {{ $t('EmailCompose.Description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="emailCompoese.description" type="text" />

                                    </div>



                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary  " v-on:click="SendEmail" v-if="formName === 'SimpleEmail'"> {{ $t('EmailCompose.Send') }}</button>
                                <button type="button" class="btn btn-primary  " v-on:click="SendEmail" v-else-if="isValid('CanSendSaleEmailAsLink') "> {{ $t('EmailCompose.Send') }}</button>
                                <button type="button" class="btn btn-primary  " v-on:click="SendEmailWithAttachment" v-if="isValid('CanSendSaleEmailAsPdf') && formName==='Invoice'"> {{ $t('EmailCompose.Send') }}</button>
                                <button type="button" class="btn btn-primary  " v-on:click="SendEmailOrderWithAttachment" v-if="isValid('CanSendSaleEmailAsPdf') && formName==='Order'"> {{ $t('EmailCompose.Send') }}</button>
                                <button type="button" class="btn btn-primary  " v-on:click="SendEmailQuotationWithAttachment" v-if="isValid('CanSendSaleEmailAsPdf') && formName==='quotation'"> {{ $t('EmailCompose.Send') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('EmailCompose.Close') }}</button>
                            </div>

                        </div>
                        
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        
                    </div>
                </div>
            </div>
            <quotationPdf :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && formName==='quotation'" v-bind:key="printRenderEmail" v-on:invoiceEmail="SendInvoiceAttachment" :isEmail="true"/>

        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
       import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'documentId','headerFooter','email', 'formName'],
        components: {
            Loading,
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                ccOptions: [],
                printDetails:[],
                printRenderEmail:0,
                emailCompoese: {
                    EmailTo: [],
                    subject: '',
                    description: '',
                    companyName: '',
                    buttonName: '',
                    emailPath: '',
                }
            }
        },
        validations() {
            
        },
        methods: {
            SendInvoiceAttachment: function (htmlData) {
               this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }




                this.emailCompoese.emailPath = htmlData.htmlString
                
                root.$https.post('/Report/SendEmailPdf', this.emailCompoese, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response != null) {
                            root.$swal({
                                title: "Success",
                                text: "Email Send Successfully",
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            })
                            root.loading = false
                            root.$emit('close');
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            SendEmailQuotationWithAttachment: function () {
                this.loading = true;
                var root = this;
                //var batch = localStorage.getItem('openBatch')
                //var openBatch = 0;
                //if (batch != undefined && batch != null && batch != "") {
                //    openBatch = batch
                //}
                //else {
                //    openBatch = 1
                //}
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.headerFooter != null || this.headerFooter != undefined) {
                    this.emailCompoese.companyName = (this.headerFooter.company.companyNameEnglish != '' || this.headerFooter.company.companyNameEnglish != null) ? this.headerFooter.company.companyNameEnglish : this.headerFooter.company.companyNameArabic
                }
                this.emailCompoese.buttonName = 'Quotation'

                root.$https.get("/Purchase/SaleOrderDetail?id=" + root.documentId, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetails = response.data;
                            if (localStorage.getItem('IsMultiUnit') == 'true') {

                                root.printDetails.saleOrderItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            //root.show = true;
                            root.printRenderEmail++;
                        }
                        
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    });
            },
            SendEmailOrderWithAttachment: function () {
                this.loading = true;
                var root = this;
                var batch = localStorage.getItem('openBatch')
                var openBatch = 0;
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.headerFooter != null || this.headerFooter != undefined) {
                    this.emailCompoese.companyName = (this.headerFooter.company.companyNameEnglish != '' || this.headerFooter.company.companyNameEnglish != null) ? this.headerFooter.company.companyNameEnglish : this.headerFooter.company.companyNameArabic
                }
                this.emailCompoese.buttonName = 'Sale Order'

                root.$https.get("/Purchase/SaleOrderDetail?id=" + root.documentId + '&isFifo=' + localStorage.getItem('fIFO') + '&openBatch=' + openBatch, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetails = response.data;
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.printDetails.saleOrderItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.printDetails.saleOrderItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printRenderEmail++;
                            root.pdfShow = true;
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    });
            },
            SendEmailWithAttachment: function () {
                this.loading = true;
                var root = this;
                var batch = localStorage.getItem('openBatch')
                var openBatch = 0;
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.headerFooter != null || this.headerFooter != undefined) {
                    this.emailCompoese.companyName = (this.headerFooter.company.companyNameEnglish != '' || this.headerFooter.company.companyNameEnglish != null) ? this.headerFooter.company.companyNameEnglish : this.headerFooter.company.companyNameArabic
                }
                this.emailCompoese.buttonName = 'Sale Invoice'
                root.$https.get("/Sale/SaleDetail?id=" + this.documentId + '&isFifo=' + localStorage.getItem('fIFO') + '&openBatch=' + openBatch, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.printDetails = response.data;

                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.printDetails.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.printDetails.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printRenderEmail++;
                            //root.loading = false
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    });
            },
           
            AddCCToList: function (newEmail) {
                var uid = this.createUUID()
                const email = {
                    cc: newEmail,
                    id: uid
                }
                this.emailCompoese.EmailTo.push(email)
                this.ccOptions.push(email)
            },
            
            createUUID: function () {

                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            close: function () {
                this.$emit('close');
            },


            SendEmail: function () {
               
                this.loading = true;
                var root = this
                var batch = localStorage.getItem('openBatch')
                var openBatch = 0;
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.headerFooter != null || this.headerFooter != undefined) {
                    this.emailCompoese.companyName = (this.headerFooter.company.companyNameEnglish != '' || this.headerFooter.company.companyNameEnglish != null) ? this.headerFooter.company.companyNameEnglish : this.headerFooter.company.companyNameArabic
                }
                if (this.formName === 'Invoice') {
                    this.emailCompoese.emailPath = this.$ClientIP + '/SaleEmail?id=' + this.documentId + '&companyId='
                        + localStorage.getItem('CompanyID') + '&multiUnit=' + localStorage.getItem('IsMultiUnit') + '&decimal=' +
                        localStorage.getItem('decimalQuantity') + '&fifo=' + localStorage.getItem('fIFO') +
                        '&openBatch=' + openBatch + '&currency' + localStorage.getItem('currency') + '&invoiceWoInventory='
                        + localStorage.getItem('InvoiceWoInventory') + '&lang=' + localStorage.getItem('locales')
                        + '&b2b=' + localStorage.getItem('b2b') + '&b2c=' + localStorage.getItem('b2c')
                        + '&taxInvoiceLabel=' + localStorage.getItem('taxInvoiceLabel') + '&taxInvoiceLabelAr=' + localStorage.getItem('taxInvoiceLabelAr')
                        + '&simplifyTaxInvoiceLabel=' + localStorage.getItem('simplifyTaxInvoiceLabel') + '&simplifyTaxInvoiceLabelAr=' + localStorage.getItem('simplifyTaxInvoiceLabelAr')
                        + '&invoicePrint=' + localStorage.getItem('InvoicePrint') + '&isHeaderFooter=' + localStorage.getItem('IsHeaderFooter')
                        + '&IsDeliveryNote=' + localStorage.getItem('IsDeliveryNote') + '&userName=' + localStorage.getItem('FullName')
                        + '&english=' + localStorage.getItem('English') + '&arabic=' + localStorage.getItem('Arabic')
                   
                    this.emailCompoese.buttonName = 'Sale Invoice'
                }
                else if (this.formName === 'Order') {
                    this.emailCompoese.emailPath = this.$ClientIP + '/SaleOrderEmail?id=' + root.documentId + '&cId='
                        + localStorage.getItem('CompanyID') + '&unit=' + localStorage.getItem('IsMultiUnit') + '&decimal='
                        + localStorage.getItem('decimalQuantity') + '&fifo=' + localStorage.getItem('fIFO') + '&oBatch='
                        + openBatch + '&cur=' + localStorage.getItem('currency') + '&woInventory=' + localStorage.getItem('InvoiceWoInventory')
                        + '&lang=' + localStorage.getItem('locales') + '&serial=' + localStorage.getItem('IsSerial') + '&isProd='
                        + localStorage.getItem('IsProduction')
                        + '&english=' + localStorage.getItem('English') + '&arabic=' + localStorage.getItem('Arabic')
                        + '&invoicePrint=' + localStorage.getItem('InvoicePrint') + '&isHeaderFooter=' + localStorage.getItem('IsHeaderFooter')
                    this.emailCompoese.buttonName = 'Sale Order'
                }
                else if (this.formName === 'quotation') {
                    this.emailCompoese.emailPath = this.$ClientIP + '/QuotationEmail?id=' + root.documentId + '&cId=' + localStorage.getItem('CompanyID')
                        + '&unit=' + localStorage.getItem('IsMultiUnit') + '&decimal=' + localStorage.getItem('decimalQuantity')
                        + '&cur=' + localStorage.getItem('currency') + '&lang=' + localStorage.getItem('locales')
                        + '&english=' + localStorage.getItem('English') + '&arabic=' + localStorage.getItem('Arabic')
                        + '&isHeaderFooter=' + localStorage.getItem('IsHeaderFooter')
                    this.emailCompoese.buttonName = 'Quotation'
                }
                root.$https.post('/Sale/SendEmail', this.emailCompoese, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (root.formName === 'SimpleEmail') {
                                root.$emit('update:modelValue', root.emailCompoese)
                            }
                            else {
                                root.$emit('close');
                            }
                            root.$swal({
                                title: "Success",
                                text: "Email Send Successfully",
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            })
                            
                        }
                        root.loading = false;
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.cc = this.email

        }
    }</script>