<template>
    <div class="row" v-if="isValid('CanViewQuotationDetail')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div class="card">
                <div class="card-body">
                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <h5 class="view_page_title">{{ $t('QuotationView.Quotation') }}</h5>
                        </div>
                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">

                            <!--<button class="btn btn-outline-danger  mr-2"
                                v-on:click="goToPurchase">
                            <i class="fas fa-arrow-circle-left fa-lg"></i>
                        </button>-->
                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <h5>{{ $t('QuotationView.From') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.nameEnglish:headerFooter.company.companyNameArabic}}</label>
                                    <br />
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.addressEnglish:headerFooter.company.addressArabic}}</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <h5>{{ $t('QuotationView.InvoiceTo') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{purchase.attn}} {{($i18n.locale == 'en' ||isLeftToRight())?purchase.customerNameEn:purchase.customerNameAr}}</label>
                                    <br />
                                    <label>{{purchase.customerAddress}}</label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.Mobile') }}</label>
                                    <p>{{ purchase.mobile }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.ClientPurchaseNo') }}</label>
                                    <p>{{ purchase.clientPurchaseNo }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.Quotation') }}</label>
                                    <p>{{ purchase.quotationNo }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.PaymentMethod') }}</label>
                                    <p>{{ purchase.paymentMethod }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.SheduleDelivery') }}</label>
                                    <p>{{ purchase.sheduleDelivery }}</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.Days') }}</label>
                                    <p>{{ purchase.days }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.SaleOrder') }}</label>
                                    <p>{{purchase.isClose==false?'Opened':'Closed'}}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('QuotationView.DeliveryTerms') }}</label>
                                    <p>
                                        {{ $t('QuotationView.Fregiht') }} <span class="small badge badge-success">{{ purchase.isFreight }}</span>
                                        {{ $t('QuotationView.Labour') }} <span class="small badge badge-success">{{ purchase.isLabour }}</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="  mt-3">
                                <table class="table add-table_list_bg" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <thead style="background-color: #3178f6;color:#ffffff;">
                                        <tr>
                                            <th style="width: 30px;" class="text-center">
                                                #
                                            </th>
                                            <th style="width: 120px;" class="text-center">
                                                Service
                                            </th>
                                            <th style="width: 120px;" class="text-center">
                                                Stuffing Location
                                            </th>
                                            <th class="text-center" style="width: 120px;">
                                                POL
                                            </th>
                                            <th style="width: 120px;" class="text-center">
                                                POD
                                            </th>
                                            <th style="width: 120px;" class="text-center">
                                                Carrier
                                            </th>
                                            <th style="width: 50px;" class="text-center">
                                                20'FT
                                            </th>
                                            <th style="width: 50px;" class="text-center">
                                                40'HC
                                            </th>
                                            <th style="width: 50px;" class="text-center">
                                                T.T
                                            </th>
                                            <th style="width: 50px;" class="text-center">
                                                ETD
                                            </th>
                                            <th style="width: 40px"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(prod , index) in purchase.importExportItems" v-bind:key="prod.rowId" style="background:#EAF1FE;">
                                            <td class="border-top-0 pl-1 pr-1">
                                                {{index+1}}
                                            </td>
                                            <td class="border-top-0 text-center pl-1 pr-1">
                                                {{prod.serviceName}}
                                            </td>

                                            <td class="border-top-0 text-center pl-1 pr-1">
                                                {{prod.stuffingLocationName}}
                                            </td>
                                            <td class="border-top-0 text-center pl-1 pr-1">
                                                {{prod.portOfLoadingName}}

                                            </td>
                                            <td class="border-top-0 text-center pl-1 pr-1">
                                                {{prod.portOfDestinationName}}
                                            </td>

                                            <td class="border-top-0 text-center pl-1 pr-1">
                                                {{prod.carrierName}}
                                            </td>

                                            <td class="border-top-0 pl-1 pr-1 text-center">
                                                {{prod.ft}}

                                            </td>

                                            <td class="border-top-0 pl-1 pr-1 text-center">
                                                {{prod.hc}}

                                            </td>

                                            <td class="border-top-0 pl-1 pr-1 text-center">
                                                {{prod.tt}}

                                            </td>
                                            <td class="border-top-0 pl-1 pr-1 text-center">
                                                {{prod.etd}}

                                            </td>

                                            <td class="border-top-0 pt-0 pl-1 pr-1">
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'arabicLanguage'">
                        <div class="col-md-12">
                            <quotation-email-item @update:modelValue="SavePurchaseItems" :key="rander" :purchase="purchase" />
                            <div class="row">
                                <div class="col-sm-8">
                                    <label>{{ $t('QuotationView.TermandCondition') }}:</label>
                                    {{purchase.note}}
                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>

        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <h5 class="view_page_title">{{ $t('QuotationView.BasicInfo') }}</h5>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('QuotationView.Quotation') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{purchase.registrationNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('QuotationView.Date') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{purchase.date}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('QuotationView.Refrence') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ purchase.refrence }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left" v-on:click="DownloadPdf">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :documentid="purchase.id" :show="show" v-if="show" @close="attachmentSave" />
        <quotationPdf :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && isDownloadPdf" v-bind:key="printRenderEmail" :fromEmailLink="true"/>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                isDownloadPdf: false,
                printDetails: [],
                daterander: 0,
                rander: 0,
                render: 0,
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    customerId: "",
                    refrence: "",
                    days: '',
                    purchaseOrder: "",
                    paymentMethod: "",
                    sheduleDelivery: "",
                    note: '',
                    isFreight: false,
                    isLabour: false,
                    isQuotation: true,
                    saleOrderItems: [],
                    attachmentList: [],
                    path: '',
                    clientPurchaseNo: '',

                    importExportItems: [],
                    orderTypeId: '',
                    incotermsId: '',
                    commodities: '',
                    natureOfCargo: '',
                    attn: '',
                    quotationValidDate: '',
                    freeTimePOL: '',
                    freeTimePOD: '',
                },                loading: false,
                show: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            };
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function () {
                this.show = false;
            },

            SavePurchaseItems: function (saleOrderItems) {

                this.purchase.saleOrderItems = saleOrderItems;
            },
            

            goToPurchase: function () {
                this.$router.push('/Quotation');
            },
            addProduct: function () {
                this.purchase.importExportItems.push({
                    rowId: this.createUUID(),
                    serviceId: this.serviceId,
                    stuffingLocationId: this.stuffingLocationId,
                    portOfLoadingId: this.portOfLoadingId,
                    portOfDestinationId: this.portOfDestinationId,
                    carrierId: this.carrierId,
                    ft: this.ft,
                    hc: this.hc,
                    tt: this.tt,
                    etd: this.etd,
                });

                this.serviceId = '';
                this.stuffingLocationId = '';
                this.portOfLoadingId = '';
                this.portOfDestinationId = '';
                this.carrierId = '';
                this.ft = '';
                this.hc = '';
                this.tt = '';
                this.etd = '';

                this.itemRender++;
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https.get("/Sale/CompanyDetailForEmail?id=" + this.$route.query.cId, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                        }
                    });
            },
            ViewInvoice: function () {
                var root = this;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https.get('/Purchase/SaleOrderDetail?Id=' + this.$route.query.id + '&isEmail=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase = response.data;
                            root.purchase.date = moment(root.purchase.date).format('llll');
                            if (root.purchase.importExportItems != null && root.purchase.importExportItems != undefined) {
                                root.purchase.importExportItems.forEach(function (item) {
                                    item.rowId = item.id
                                });
                            }
                            root.attachment = true;
                            if (root.$route.query.unit == 'true') {

                                root.purchase.saleOrderItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.rander++;
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            DownloadPdf: function () {
                var root = this;
                root.loading = true
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'
                root.printDetailsPos = [];
                root.$https.get("/Purchase/SaleOrderDetail?Id=" + this.$route.query.id +  '&isEmail=' + true, {
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
                            root.printRenderEmail++
                            root.isDownloadPdf = true
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
        },
        created: function () {
            
            
            this.GetHeaderDetail();
            this.ViewInvoice()
        },
        mounted: function () {

           
        },
    };
</script>
