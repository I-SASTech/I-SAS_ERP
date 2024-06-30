<template>
 <modal show="show" v-if="isValid('BasicMail') || isValid('StandardMail')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('EmailCompose.NewMessage') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12">
                        <label class="text  font-weight-bolder"> {{ $t('Email') }}:</label>
                        <input class="form-control" v-model="email" type="text" disabled />
                    </div>
                    <div class="form-group has-label col-sm-12">
                        <label>{{ $t('Cc') }}:</label>
                        <multiselect v-model="emailCompose.cc" tag-placeholder="Add Email" placeholder="Search or add a Cc" label="cc" track-by="id" :options="emailCompose.cc" :multiple="true" :taggable="true" @tag="AddCCToList"></multiselect>
                    </div>
                    <div class="form-group has-label col-sm-12">
                        <label class="text  font-weight-bolder"> {{ $t('EmailCompose.Subject') }}:</label>
                        <input class="form-control" v-model="emailCompose.subject" type="text" />
                    </div>
                    
                    <div class="form-group has-label col-sm-12">
                        <label class="text  font-weight-bolder"> {{ $t('EmailCompose.Description') }}: </label>
                        <textarea class="form-control"  rows="5" v-model="emailCompose.body" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12">
                        <div class="checkbox form-check-inline mx-2 mt-2">
                            <input v-model="emailCompose.isInvoice" 
                                   type="checkbox" id="inlineCheckbox120013423ds">
                            <label for="inlineCheckbox120013423ds">{{ $t('Invoice Attachment') }}</label>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12">
                        <a class="btn btn-primary" href="javascript:void(0)" v-if="emailCompose.isInvoice" v-on:click="PrintRdlc(emailCompose.documentId)" >{{ $t('Invoice Attachment View') }} </a>
                    </div>
                    <div class="form-group col-sm-12">
                        <div class="font-xs mb-1"> <strong>{{ $t('Other Attachments') }} <span v-if="emailCompose.attachmentList.length>0">({{ emailCompose.attachmentList.length }})</span></strong></div>
                        <div v-for="(item,index) in emailCompose.attachmentList" v-bind:key="item.path">
                            {{index+1 }}- &nbsp;&nbsp;{{item.fileName }}
                        </div>
                        <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1 mt-1">
                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSale.Attachment') }}
                        </button>
                        <div>
                            <small class="text-muted">
                                {{ $t('AddSale.FileSize') }}
                            </small>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor">{{ $t('Send') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />
            <bulk-attachment :attachmentList="emailCompose.attachmentList" :show="show1" v-if="show1" @close="attachmentSave" />
        </div>
       


    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    
    export default {
        props: ['shows', 'id','from','customerEmail'],
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                reportsrc: '',
                changereport:0,
                page: this.from,
                email: this.customerEmail,
                show: false,
                show1: false,
                emailCompose: {
                    documentId: this.id,
                    subject:'',
                    cc:[],
                    body:'',
                    isInvoice:false,
                    attachmentList:[],
                }
                
            }
        },
        methods: {
            Attachment: function () {
                this.show1 = true;
            },
            attachmentSave: function (attachment) {
                this.emailCompose.attachmentList = attachment;
                this.show1 = false;
            },
            close: function () {
                this.$emit('close');
            },
            AddCCToList: function (newEmail) {

                if(!newEmail.includes('@'))
                {
                    this.error = 'Error: ';
                    this.$swal({
                        title: 'Invalid Mail Account',
                        text: "Email must be contain '@' and '.com'",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
                else
                {
                    var uid = this.createUUID()
                    const email = {
                        cc: newEmail,
                        id: uid
                    }
                    this.emailCompose.cc.push(email);
                }

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
            SaveColor()
            {
                debugger; //eslint-disable-line
                var root = this;
                var token = '';

                if (token == '') 
                {
                    token = localStorage.getItem('token');
                }

                var isSaleOrder = false;
                var isCreditNote = false;
                if((this.page == 'ServiceSaleOrder' || this.page == 'SaleOrder') || (this.page == 'ServiceQuotation' || this.page == 'Quotation'))
                {
                    isSaleOrder = true;
                } 
                if(this.page == 'CreditNote' || this.page == 'DebitNote')
                {
                    isCreditNote = true;
                }
                var isDeliveryNote = false;
                if(this.page == 'DeliveryChallan')
                {
                    isDeliveryNote = true;
                }
                this.loading = true;

                if(this.page == 'Purchase' || this.page == 'PurchaseOrder' || this.page == 'SupplierQuotation' || this.page == 'GoodsReceive')
                {
                    var isPurchaseOrder = false;
                    var isGoodsReceive = false;

                    if(this.page == 'PurchaseOrder' || this.page == 'SupplierQuotation')
                    {
                        isPurchaseOrder = true;
                    }
                    else if(this.page == 'GoodsReceive')
                    {
                        isGoodsReceive = true;
                    }
                    root.$https.post('Sale/SendPurchaseEmails?id=' + this.emailCompose.documentId + '&isPurchaseOrder=' + isPurchaseOrder + '&isGoodsReceive='+ isGoodsReceive, this.emailCompose , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data.isSuccess) {
                            root.$swal({
                                title: "Email",
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else
                        {
                            root.$swal({
                                title: 'Error',
                                text: response.data.isAddUpdate,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                        root.loading = false;
                    });
                }
                else
                {
                    root.$https.post('Sale/SendSaleEmail?id=' + this.emailCompose.documentId + '&isSaleOrder=' + isSaleOrder + '&isCreditNote='+ isCreditNote + '&isDeliveryNote=' + isDeliveryNote, this.emailCompose , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data.isSuccess) {
                            root.$swal({
                                title: "Email",
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else
                        {
                            root.$swal({
                                title: 'Error',
                                text: response.data.isAddUpdate,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                        root.loading = false;
                    });
                }
            },
            PrintRdlc: function (id) {
                var root = this;
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                root.show = true;

                var isMultiUnit = false;

                isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;

                if(this.page == 'SaleInvocie')
                {
                    root.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + false + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&CompanyId=' + companyId + '&formName=SaleRecored' + "&isQuotation=" + true + "&IsDayStart=" + false + '&isDownload=' + false
                }
                else if(this.page == 'CreditNote')
                {
                    this.reportsrc = this.$ReportServer + '/SalesModuleReports/CreditNote/CreditNoteReport.aspx?id=' + id + '&CompanyId=' + companyId + '&formName=CreditNote' + '&isDownload=' + false;
                }
                else if(this.page == 'DebitNote')
                {
                    this.reportsrc = this.$ReportServer + '/SalesModuleReports/CreditNote/CreditNoteReport.aspx?id=' + id + '&CompanyId=' + companyId + '&formName=DebitNote' + '&isDownload=' + false;
                }
                else if(this.page == 'DeliverChallan')
                {
                    this.reportsrc = this.$ReportServer + '/SalesModuleReports/DeliveryNote/DeliveryNoteReport.aspx?Id=' + id + '&isFifo=' + true + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + true + '&CompanyId=' + companyId + '&formName=DeliveryChallan' + '&isDownload=' + false;
                }
                else if((this.page == 'ServiceSaleOrder' || this.page == 'SaleOrder') || (this.page == 'ServiceQuotation' || this.page == 'Quotation'))
                {
                    var reportname = false;
                    if (this.page == 'ServiceSaleOrder' || this.page == 'SaleOrder') {
                        reportname = true;
                    }
                    else if (this.page == 'ServiceQuotation' || this.page == 'Quotation') {
                       reportname = false
                    }
                    this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleOrder/SaleOrderReport.aspx?Id=' + id + '&isFifo=' + false + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + true + '&CompanyId=' + companyId + '&formName=' + this.page + '&reportName=' + reportname + '&isDownload=' + false
                }
                else if(this.page == 'Purchase')
                {
                    this.reportsrc = this.$ReportServer + '/PurchaseModule/PurchaseInvoice/PurchaseInvoiceReport.aspx?Id=' + id 
                              + '&isFifo=' + false 
                              + '&openBatch=' + 0 
                              + '&isReturn=' + false 
                              + '&deliveryChallan=' + false 
                              + '&simpleQuery=' + false 
                              + '&colorVariants=' + false 
                              + '&isMultiUnit=' + isMultiUnit 
                              + '&isReturnView=' + false 
                              + '&CompanyId=' + companyId 
                              + '&formName=PurchasePOS'
                              + '&isDownload='+ false;
                }
                else if(this.page == 'PurchaseOrder' || this.page == 'SupplierQuotation')
                {
                    this.reportsrc = this.$ReportServer + '/PurchaseModule/PurchaseOrder/PurchaseOrderReport.aspx?Id=' + id 
                    + '&isFifo=' + false 
                    + '&openBatch=' + 0 
                    + '&isReturn=' + false 
                    + '&deliveryChallan=' + false 
                    + '&simpleQuery=' + false 
                    + '&colorVariants=' + false 
                    + '&isMultiUnit=' + isMultiUnit 
                    + '&isReturnView=' + true 
                    + '&CompanyId=' + companyId 
                    + '&formName=PurchaseOrder' 
                    + '&isDownload=' + false
                }
                else if(this.page == 'GoodsReceive')
                {
                    this.reportsrc = this.$ReportServer + '/PurchaseModule/GoodsReceive/GoodsReceiveReport.aspx?Id=' + id 
                                    + '&isFifo=' + false 
                                    + '&openBatch=' + 0 
                                    + '&isReturn=' + false 
                                    + '&deliveryChallan=' + false 
                                    + '&simpleQuery=' + false 
                                    + '&colorVariants=' + false 
                                    + '&isMultiUnit=' + isMultiUnit
                                    + '&isReturnView=' + true 
                                    + '&CompanyId=' + companyId 
                                    + '&formName=GoodReceive' 
                                    + '&isDownload=' + false
                }
                this.changereport++;
                
            },
        },
        created: function()
        {
            

        },
        mounted: function () {
        }
    }
</script>