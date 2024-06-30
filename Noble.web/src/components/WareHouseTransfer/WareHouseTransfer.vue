<template>
    <div class="row" v-if="isValid('CanViewStockTransfer') || isValid('CanDraftStockTransfer')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('WareHouseTransfer.WareHouseTransfer') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('WareHouseTransfer.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('WareHouseTransfer.WareHouseTransfer') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="AddWareHouseTransfer" v-if="isValid('CanDraftStockTransfer') || isValid('CanAddStockTransfer')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PurchaseOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('WareHouseTransfer.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>{{ $t('WareHouseTransfer.WareHouseTransferSearch') }}</label>
                                <div class="input-group">
                                    <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                                    <input v-model="search" type="text" class="form-control" :placeholder="$t('WareHouseTransfer.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                </div>
                            </div>
                        </div>
                        <div class=" col-md-3 mt-1" >

<button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
    {{ $t('Sale.ClearFilter') }}
</button>

</div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>{{ $t('WareHouseTransfer.FromDate') }}</label>
                                <datepicker v-model="fromDate" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>{{ $t('WareHouseTransfer.ToDate') }}</label>
                                <datepicker v-model="toDate" />
                            </div>
                        </div>
                    </div>


                </div>
                <div class="card-body">


                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"
                            v-if="isValid('CanDraftStockTransfer')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                               v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab" aria-selected="true">
                                {{$t('WareHouseTransfer.Draft')}}
                            </a>
                        </li>
                        <li class="nav-item"
                            v-if="isValid('CanDraftStockTransfer')"
                            v-on:click="makeActive('Approved')">
                            <a class="nav-link"
                               v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab" href="#profile" role="tab" aria-selected="false">
                                {{$t('WareHouseTransfer.Post')}}
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'Draft' }">
                            <div class="row mb-3" v-if="selected.length > 0">
                                <div class="col-md-3 ">
                                    <div class="dropdown">
                                        <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            {{ $t('WareHouseTransfer.BulkAction') }}
                                        </button>

                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:130px;">
                                                {{ $t('WareHouseTransfer.CODE') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.FromWareHouse') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.ToWareHouse') }}

                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('From Branch') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('To Branch') }}

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(wareHouseTransfer,index) in wareHouseTransferList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td v-if="isValid('CanEditStockTransfer')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditWareHouseTransfer(wareHouseTransfer.id)">{{wareHouseTransfer.code}}</a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{wareHouseTransfer.code}}
                                            </td>
                                            <td>
                                                {{wareHouseTransfer.date}}
                                            </td>
                                            <td>
                                                {{wareHouseTransfer.fromWareHouse}}
                                            </td>
                                            <td>
                                                {{wareHouseTransfer.toWareHouse}}
                                            </td>
                                            <td v-if="wareHouseTransfer.fromBranch != null && wareHouseTransfer.fromBranch != ' ' && allowBranches ">
                                                {{wareHouseTransfer.fromBranch}}
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                            <td v-if="wareHouseTransfer.toBranch != null && wareHouseTransfer.toBranch != ' ' && allowBranches">
                                                {{wareHouseTransfer.toBranch}}
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{$t('Pagination.ShowingEntries')}}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }}
                                        {{$t('Pagination.to')}} {{ currentPage * 10 }} of
                                        {{ rowCount }} {{$t('Pagination.entries')}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{$t('Pagination.Showing')}} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{$t('Pagination.entries')}}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }}
                                        {{$t('Pagination.of')}} {{ rowCount }} {{ $t('Pagination.entries') }}
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
                        <div class="tab-pane pt-3" id="profile" role="tabpanel" v-bind:class="{ active: active == 'Approved' }">
                            <div class="row mb-3" v-if="selected.length > 0">
                                <div class="col-md-3 ">
                                    <div class="dropdown">
                                        <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            {{ $t('WareHouseTransfer.BulkAction') }}
                                        </button>

                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:130px;">
                                                {{ $t('WareHouseTransfer.CODE') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.FromWareHouse') }}
                                            </th>
                                            <th>
                                                {{ $t('WareHouseTransfer.ToWareHouse') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('From Branch') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('To Branch') }}

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(wareHouseTransfer,index) in wareHouseTransferList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td v-if="isValid('CanEditStockTransfer')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditWareHouseTransfer(wareHouseTransfer.id)">{{wareHouseTransfer.code}}</a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{wareHouseTransfer.code}}
                                            </td>
                                            <td>
                                                {{wareHouseTransfer.date}}
                                            </td>
                                            <td>
                                                {{wareHouseTransfer.fromWareHouse}}
                                            </td>
                                            <td>
                                                {{wareHouseTransfer.toWareHouse}}
                                            </td>
                                            <td v-if="wareHouseTransfer.fromBranch != null && wareHouseTransfer.fromBranch != ' ' && allowBranches">
                                                {{wareHouseTransfer.fromBranch}}
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                            <td v-if="wareHouseTransfer.toBranch != null && wareHouseTransfer.toBranch != ' ' && allowBranches">
                                                {{wareHouseTransfer.toBranch}}
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{$t('Pagination.ShowingEntries')}}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{$t('Pagination.of')}} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{$t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                        $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class="float-end" v-on:click="getPage()">
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
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
            allowBranches: false,
                fromDate: '',
                toDate: '',
                search: '',
                searchQuery: '',
                wareHouseTransferList: [],
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                active: 'Draft',
                selected: [],
                selectAll: false,
                counter: 0,
                randerList: 0,
            }
        },
        watch: {
            // search: function (val) {
            //     this.getData(val, 1, this.active, this.fromDate, this.toDate);
            // },
            fromDate: function (fromDate) {
                this.counter++;
                if (this.counter != 1) {
                    localStorage.setItem('fromDate', fromDate);
                    this.getData(this.search, this.currentPage, this.active, fromDate, this.toDate);
                }
            },
            toDate: function (toDate) {

                this.counter++;
                if (this.counter != 2) {
                    localStorage.setItem('toDate', toDate);
                    this.getData(this.search, this.currentPage, this.active, this.fromDate, toDate);
                }
            }
        },
        methods: {

            search22: function () {
              this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
              },

             clearData: function () {
                this.search="";
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);

            },

            select: function () {
                this.selected = [];
                if (!this.selectAll) {
                    for (let i in this.wareHouseTransferList) {
                        this.selected.push(this.wareHouseTransferList[i].id);
                    }
                }
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            getPage: function () {
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
            },
            makeActive: function (item) {
                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, item, this.fromDate, this.toDate);
            },
            EditWareHouseTransfer: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/WareHouseTransferDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            var isMultiUnit = localStorage.getItem('IsMultiUnit');
                            if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                response.data.wareHouseTransferItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                });
                            }
                            if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                response.data.wareHouseTransferItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                });
                            }
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addwareHouseTransfer',
                                query: { data: '',
                                Add: false, }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },
            getData: function (search, currentPage, status, fromDate, toDate) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                localStorage.setItem('currentPage', this.currentPage);
                localStorage.setItem('active', this.active);
                this.$https.get('/Purchase/WareHouseTransferList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + fromDate + '&toDate=' + toDate + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.wareHouseTransferList = response.data.results;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                            root.currentPage = currentPage;
                            root.randerList++;
                        }

                    });
            },

            RemoveWareHouseTransfer: function (id) {


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
                        root.$https.get('/Purchase/DeleteWareHouseTransfer?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
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
            AddWareHouseTransfer: function () {
           //     this.$router.push('/addwareHouseTransfer');
                this.$router.push({
                                path: '/addwareHouseTransfer',
                                query: { 
                                    
                                    Add: true, }
                            })
            },
        },
        created: function () {

            if (this.$route.query.data == 'WareHouseTransfers') {
                this.$emit('update:modelValue', 'WareHouseTransfers');

            }
            else {
                this.$emit('update:modelValue', this.$route.name);

            }
            if (localStorage.getItem('fromDate') != null && localStorage.getItem('fromDate') != '' && localStorage.getItem('fromDate') != undefined) {
                this.fromDate = localStorage.getItem('fromDate');

            }
            else {
                this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

            }
            if (localStorage.getItem('toDate') != null && localStorage.getItem('toDate') != '' && localStorage.getItem('toDate') != undefined) {
                this.toDate = localStorage.getItem('toDate');

            }
            else {
                this.toDate = moment().format("DD MMM YYYY");
            }

            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        },
        mounted: function () {
            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.active = (localStorage.getItem('active'));
                this.getPage();


            }
            else {
                if (this.isValid('CanDraftStockTransfer')) {
                    this.makeActive('Draft')
                }
                else if (this.isValid('CanViewStockTransfer')) {
                    this.makeActive('Approved')
                }

            }
        },
        updated: function () {
            if (this.selected.length < this.wareHouseTransferList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.wareHouseTransferList.length) {
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