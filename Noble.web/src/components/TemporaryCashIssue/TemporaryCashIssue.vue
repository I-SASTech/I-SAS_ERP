<template>
    <div class="row" v-if="(isValid('CanViewTCIssue') || isValid('CanDraftTCIssue'))">
        <div class="col-lg-12 col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('TemporaryCashIssue.TemporaryCashIssue') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('TemporaryCashIssue.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('TemporaryCashIssue.TemporaryCashIssue') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanDraftTCIssue') || isValid('CanAddTCIssue')" v-on:click="AddPurchaseOrder" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('TemporaryCashIssue.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('TemporaryCashIssue.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('TemporaryCashIssue.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item" v-if="isValid('CanDraftTCIssue')"><a class="nav-link" v-bind:class="{active:active == 'Draft'}" v-on:click="makeActive('Draft')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('TemporaryCashIssue.Draft') }}</a></li>
                                <li class="nav-item" v-if="isValid('CanViewTCIssue')"><a class="nav-link" v-bind:class="{active:active == 'Approved'}" v-on:click="makeActive('Approved')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('TemporaryCashIssue.Post') }}</a></li>
                            </ul>

                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('SaleOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')"> {{ $t('SaleOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('SaleOrder.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.RegistrationNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.EmployeeName')}}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.Amount')}}
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
                                                    <td v-if="isValid('CanEditTCIssue')">
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{purchaseOrder.registrationNo}}</a>
                                                        </strong>

                                                    </td>
                                                    <td v-else>
                                                        {{purchaseOrder.registrationNo}}
                                                    </td>

                                                    <td>
                                                        {{purchaseOrder.date}}
                                                    </td>
                                                    <td>
                                                        {{purchaseOrder.userName}}
                                                    </td>
                                                    <td>
                                                        {{currency}} {{parseFloat(purchaseOrder.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>

                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('TemporaryCashIssue.Action')}} <i class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a href="javascript:void(0)" title="Print" class="dropdown-item" v-on:click="PrintInvoice(purchaseOrder.id)" v-bind:class="$i18n.locale == 'en' ? 'mr-1' : 'ml-1'" v-if="isValid('CanPrintTCIssue')">{{ $t('TemporaryCashIssue.Print')}}</a>
                                                        </div>
                                                    </td>


                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span v-if="currentPage === 1 && rowCount === 0">
                                                {{ $t('Pagination.ShowingEntries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }} {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }} of {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                        </div>
                                        <div class=" col-lg-6">
                                            <div class=" float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Approved'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('SaleOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')">{{ $t('SaleOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('SaleOrder.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.RegistrationNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.EmployeeName')}}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.Amount')}}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.ReturnAmount')}}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssue.UsedAmount')}}
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
                                                        {{purchaseOrder.registrationNo}}
                                                    </td>

                                                    <td>
                                                        {{purchaseOrder.date}}
                                                    </td>
                                                    <td>
                                                        {{purchaseOrder.userName}}
                                                    </td>
                                                    <td>
                                                        {{currency}} {{parseFloat(purchaseOrder.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>
                                                        {{currency}} {{parseFloat(purchaseOrder.returnAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>
                                                        {{currency}} {{parseFloat(purchaseOrder.amount-purchaseOrder.returnAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>

                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">Action <i class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="GoTo(purchaseOrder, 'Expenses')">{{ $t('TemporaryCashIssue.Expenses')}}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="GoTo(purchaseOrder, 'Expense Bill')">{{ $t('TemporaryCashIssue.ExpenseBills')}}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="GoTo(purchaseOrder, 'Supplier Payment')">{{ $t('TemporaryCashIssue.SupplierPaymentReceipt')}}</a>


                                                            <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 ml-1" v-on:click="ReturnModel(purchaseOrder)" v-if="isValid('CanPrintTCIssue') && (purchaseOrder.amount-(purchaseOrder.returnAmount+purchaseOrder.voucherAmount)!=0)"><i class="fas fa-money-bill"></i></a>
                                                            <a href="javascript:void(0)" title="Print" class="btn  btn-icon btn-primary btn-sm mr-1 ml-1" v-on:click="PrintInvoice(purchaseOrder.id)" v-if="isValid('CanPrintTCIssue')">{{ $t('TemporaryCashIssue.Print')}}</a>
                                                            <a href="javascript:void(0)" title="Print" class="btn  btn-icon btn-primary btn-sm" v-on:click="ShowHistory(purchaseOrder.temporaryCashIssueExpenses)"><i class="fas fa-history"></i></a>
                                                        </div>
                                                    </td>

                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span v-if="currentPage === 1 && rowCount === 0">
                                                {{ $t('Pagination.ShowingEntries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }} {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }} of {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                        </div>
                                        <div class=" col-lg-6">
                                            <div class=" float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <temporary-cash-issue-print :printDetails="printDetails" :headerFooter="headerFooter" v-if="printShow" v-bind:key="printRender" />
        <temporary-cash-issue-history :temporaryCash="history" :show="historyShow" v-if="historyShow" :key="render" @close="historyShow=false" />
        <PaymentModel :temporaryCash="temporaryCash" :show="purchaseBillShow" v-if="purchaseBillShow" @RefreshList="purchaseBillClose" @close="purchaseBillClose" />
        <temporary-cash-issue-Return v-if="show" :show="show" :amount="amount" :temporaryCashIssueId="documentId" :userId="userId" :isCashRequesterUser="isCashRequesterUser" @close="ReturnSave()" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>





<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
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

                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },
                printDetails: [],
                printRender: 0,
                render: 0,
                randerList: 0,
                amount: 0,
                purchaseBillShow: false,
                show: false,
                documentId: '',
                userId: '',
                isCashRequesterUser: '',
                temporaryCash: '',
                historyShow: false,
                printShow: false,
                history: '',
            }
        },
        watch: {
            search: function (val) {
                this.getData(val, 1, this.active);
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            GoTo: function (data, prop) {
                data.isTemporaryCashIssue = true;
                if (prop == 'Expenses') {
                    this.$store.GetEditObj(data);
                    this.$router.push({
                        path: '/adddailyexpense',
                        query: { data: '', Add: false }
                    });
                }

                if (prop == 'Supplier Payment') {
                    this.$store.GetEditObj(data);
                    this.$router.push({
                        path: '/addPaymentVoucher',
                        query: { data: '', Add: false, formName: 'BankPay' }
                    });
                }

                if (prop == 'Expense Bill') {
                    this.temporaryCash = data;
                    this.purchaseBillShow = true;
                }
            },

            ReturnSave: function () {
                this.show = false;
                this.getPage();
            },

            purchaseBillClose: function () {
                this.purchaseBillShow = false;
                this.getPage();
            },

            ShowHistory: function (data) {

                this.history = data;
                this.historyShow = true;
                this.render++;
            },

            ReturnModel: function (item) {
                this.documentId = item.id;
                this.userId = item.userId;
                this.amount = item.amount;
                this.isCashRequesterUser = item.isCashRequesterUser;
                this.show = true;
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
                this.$https.get('/EmployeeRegistration/TemporaryCashIssueList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.saleOrderList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
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
                var root = this;
          //      this.$router.push('/AddTemporaryCashIssue');
                root.$router.push({
                                path: '/AddTemporaryCashIssue',
                                query: { 
                                Add: true, }
                            })
            },

            EditPurchaseOrder: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/EmployeeRegistration/TemporaryCashIssueDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddTemporaryCashIssue',
                                query: { data: '',
                                Add: false, }
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
                root.$https.get("/EmployeeRegistration/TemporaryCashIssueDetail?id=" + value, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetails = response.data;

                            root.printShow = true;
                            root.printRender++;
                        }
                    });
            },
        },
        created: function () {
            if (this.$route.query.data == 'AddSaleOrders') {
                this.$emit('update:modelValue', 'AddSaleOrders');

            }
            else {
                this.$emit('update:modelValue', this.$route.name);

            }
        },
        mounted: function () {

            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.active = (localStorage.getItem('active'));
                this.getPage();
            }
            else {
                if (this.isValid('CanDraftTCIssue')) {
                    this.makeActive("Draft");
                }
                else if (this.isValid('CanViewTCIssue')) {
                    this.makeActive("Approved");
                }
            }

            this.isFifo = localStorage.getItem('fIFO');
            var batch = localStorage.getItem('openBatch')
            if (batch != undefined && batch != null && batch != "") {
                this.openBatch = batch
            }
            else {
                this.openBatch = 1
            }

            this.currency = localStorage.getItem('currency');
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
