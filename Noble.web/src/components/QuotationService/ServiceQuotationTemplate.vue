<template>
    <div class="row" v-if="(isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="row mb-4">
                <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                    <h5 class="page_title">Service Quotation Template</h5>
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)"> {{ $t('Quotation.Home') }}</a></router-link></li>
                            <li class="breadcrumb-item active" aria-current="page">{{ $t('Quotation.Quotation') }}</li>
                        </ol>
                    </nav>
                </div>
                <div class="col-lg-6 ">
                    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" class="col-md-12" v-if="isValid('CanAddQuotation') || isValid('CanDraftQuotation')">
                        <router-link :to="'/StartScreen'"><a href="javascript:void(0)" class="btn btn-outline-danger "> {{ $t('Quotation.Close') }}</a></router-link>
                    </div>
                </div>
            </div>
            <div class="card ">
                <div class="card-header">
                    <div class="row mb-3">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-5">
                            <div class="form-group ">
                                <label>{{ $t('Quotation.SearchByQuotation')}}</label>
                                <div>
                                    <input type="text" class="form-control search_input" v-model="search" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" name="search" id="search" :placeholder="$t('Quotation.Search')" />
                                    <span class="fas fa-search search_icon"></span>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                          
                            

                                   
                                    <div class="table-responsive">
                                        <table class="table table_list_bg">
                                            <thead class="">
                                                <tr>
                                                    <th>#</th>
                                                    <th>
                                                        {{ $t('Quotation.SONumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Quotation.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        Description
                                                    </th>


                                                    <th>
                                                        {{ $t('Quotation.Status') }}
                                                    </th>
                                                    <th>

                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder,index) in saleOrderList" v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>

                                                    <td>
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{purchaseOrder.registrationNumber}}</a>
                                                        </strong>
                                                    </td>
                                                    <td>
                                                        {{purchaseOrder.date}}
                                                    </td>
                                                    <td>
                                                        {{purchaseOrder.description}}
                                                    </td>
                                                  
                                                  
                                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                       
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="float-left">
                                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    </div>
                                    <div class="float-right">
                                        <div class="overflow-auto" v-on:click="getPage()" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="First"
                                                          prev-text="Previous"
                                                          next-text="Next"
                                                          last-text="Last"></b-pagination>
                                        </div>
                                        <div class="overflow-auto" v-on:click="getPage()" v-else>
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="الأولى"
                                                          prev-text="السابقة"
                                                          next-text="التالية"
                                                          last-text="الأخيرة"></b-pagination>
                                        </div>
                                    </div>
                               
                        </div>
                    </div>
                </div>
            </div>
            <quotationPdf :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && pdfShow" v-bind:key="printpdfRender" />
            <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :documentId="orderId" :headerFooter="headerFooter" :formName="'quotation'"></email-compose>
            <QuotationSmartDigitalInvoice :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && show" v-bind:key="printRender" />
           
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                orderId: '',
                emailComposeShow: false,
                show: false,
                showTemplate: false,
                pdfShow: false,
                active: 'Draft',
                search: '',
                searchQuery: '',
                saleOrderList: [],
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                currency: '',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                printDetails: [],
                printRender: 0,
                printpdfRender: 0,
                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },
                quotationTemplate: {
                    id: '00000000-0000-0000-0000-000000000000',
                    templateName: '',
                    description: '',
                },

            }
        },
        watch: {
            search: function (val) {
                this.getData(val, 1, this.active);
            }
        },
        methods: {
            TemplateofQuotation: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.$store.GetEditObj(response.data);
                        if (response.data != null) {
                            root.$router.push({
                                path: '/AddQuotationTemplate',
                                query: {
                                    data: '',
                                    Add: false
                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            //TemplateofQuotation: function (id) {
            //    
            //    this.quotationTemplate = {
            //        id: '00000000-0000-0000-0000-000000000000',
            //        templateName: '',
            //        description: '',
            //        templateItems:[]

            //    }
            //    this.showTemplate = !this.showTemplate;
            //    var root = this;
            //    var token = '';
            //    if (token == '') {
            //        token = localStorage.getItem('token');
            //    }
            //    root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
            //        .then(function (response) {
            //            if (response.data != null) {
            //                this.quotationTemplate.templateItems = response.data.saleOrderItems;
            //            }
            //        });
                
            //},
            sendEmail: function (id) {
                this.orderId = id
                this.emailComposeShow = true;
            },
            changeStatus: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                var saleOrder={ id: id, isClose:true };
                this.$https.post('/Purchase/SaveSaleOrderInformation', saleOrder, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: "Quotation Closed Successfully!",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: true
                        });
                        root.getPage();
                    }
                });            
            },
            DeleteFile: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Purchase/DeletePo', this.selected, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                    text: response.data.message.isAddUpdate,
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
                                }).then(function (result) {
                                    if (result) {
                                        root.$router.push('/purchase');
                                    }
                                });
                            } else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: response.data.message.isAddUpdate,
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false
                                });
                            }
                        }
                    },
                        function () {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update UnSuccessfully' : 'التحديث غير ناجح', 
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },
            select: function () {
                this.selected = [];
                if (!this.selectAll) {
                    for (let i in this.saleOrderList) {
                        this.selected.push(this.saleOrderList[i].id);
                    }
                }
            },
            getPage: function () {
                this.getData(this.search, this.currentPage, this.active);
            },

            makeActive: function (item) {
                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, item);
            },
            getData: function (search, currentPage, status) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Purchase/QuotationTemplateList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isService=true', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        root.saleOrderList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;

                    });
            },
            RemovePurchaseOrder: function (id) {
                var root = this;
                // working with IE and Chrome both
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!', 
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then(function (result) {
                    if (result) {

                        var token = '';
                        if (token == '') {
                            token = localStorage.getItem('token');
                        }
                        root.$https.get('/Purchase/DeleteSaleOrder?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                        text: response.data.message.isAddUpdate,
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });
                                } else {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: response.data.message.isAddUpdate,
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                }
                            },
                                function () {

                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Delete UnSuccessfully' : 'حذف غير ناجح',
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                });
                    }
                    else {
                        this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                    }
                });
            },
            AddPurchaseOrder: function () {

                this.$router.push('/AddQuotation');
            },
            EditPurchaseOrder: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                

                root.$https.get('/Purchase/QuotationTemplateDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddQuotationTemplate',
                                query: {
                                    data: '',
                                    Add: false,
                                    isService: true,

                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            ViewInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/QuotationView',
                                query: { data: '', Add: false }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {

                            root.headerFooter.isQuotationPrint = response.data.isQuotationPrint;
                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;
                            root.headerFooter.exchangeDays = response.data.exchangeDays;
                            root.headerFooter.printOptions = response.data.printOptions;
                            root.headerFooter.invoicePrint = response.data.invoicePrint;
                            root.headerFooter.welcomeLineEn = response.data.welcomeLineEn;
                            root.headerFooter.welcomeLineAr = response.data.welcomeLineAr;
                            root.headerFooter.closingLineEn = response.data.closingLineEn;
                            root.headerFooter.closingLineAr = response.data.closingLineAr;
                            root.headerFooter.contactNo = response.data.contactNo;
                            root.headerFooter.managementNo = response.data.managementNo;

                            root.headerFooter.businessAddressArabic = response.data.businessAddressArabic;
                            root.headerFooter.businessAddressEnglish = response.data.businessAddressEnglish;
                            root.headerFooter.headerImage = response.data.headerImage1ForPrint;
                            root.headerFooter.tagImageForPrint = response.data.tagImageForPrint;
                            root.headerFooter.proposalImageForPrint = response.data.proposalImageForPrint;
                        }

                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            PrintInvoice: function (value) {
                this.GetHeaderDetail();
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Purchase/SaleOrderDetail?id=" + value, {
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
                            root.show = true;
                            root.printRender++;
                        }
                    });
            },
            PrintInvoicePdf: function (value) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Purchase/SaleOrderDetail?id=" + value, {
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
                            root.printpdfRender++;
                            root.pdfShow = true;

                        }
                    });
            },
        },
        created: function () {
            this.GetHeaderDetail();
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.makeActive("Approved");
            this.currency = localStorage.getItem('currency');
            //this.getData(this.search, 1);
        },
        updated: function () {
            if (this.selected.length < this.saleOrderList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.saleOrderList.length) {
                if (this.selected.length == 0) {
                    this.selectAll = false;
                }
                else {
                    this.selectAll = true
                }
            }
        }
    }
</script>