<template>
    <div class="row" v-if="(isValid('CanViewProductionRecipe'))">
        <div class="col-lg-12 col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('RecipeNo.RecipeLists') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('RecipeNo.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('RecipeNo.RecipeLists') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddProductionRecipe')" v-on:click="AddPurchaseOrder"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('RecipeNo.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('RecipeNo.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('RecipeNo.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" >

                            <button v-on:click="search22(true)" :disabled ="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                        v-if="isValid('CanViewProductionRecipe')" v-on:click="makeActive('Draft')"
                                        id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab"
                                        aria-controls="v-pills-home" aria-selected="true">{{ $t('RecipeNo.Draft') }}</a>
                                </li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Approved' }"
                                        v-if="isValid('CanViewProductionRecipe')" v-on:click="makeActive('Approved')"
                                        id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab"
                                        aria-controls="v-pills-profile" aria-selected="false">{{ $t('RecipeNo.Post') }}</a>
                                </li>
                            </ul>
                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('RecipeNo.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('RecipeNo.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('RecipeNo.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table ">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>

                                                    <th>
                                                        {{ $t('RecipeNo.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('RecipeNo.RecipeName') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('RecipeNo.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('RecipeNo.ProductName') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('RecipeNo.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        Status
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in recipeNoList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td
                                                        v-if="isValid('CanAddProductionRecipe') || isValid('CanAddProductionRecipe')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{ purchaseOrder.registrationNumber }}</a>
                                                        </strong>

                                                    </td>
                                                    <td v-else>
                                                        {{ purchaseOrder.registrationNumber }}

                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.recipeName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.productName }}
                                                    </td>
                                                    <td class="text-center">
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <toggle-button
                                                            v-on:change="ChangeStatus(purchaseOrder.id, purchaseOrder.isActive)"
                                                            v-model="purchaseOrder.isActive" color="#3178F6" />
                                                    </td>
                                                    <td
                                                        v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1"
                                                            v-on:click="ViewRecipeNo(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')">
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Approved'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('RecipeNo.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('RecipeNo.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('RecipeNo.Reject') }}</a>
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
                                                        {{ $t('RecipeNo.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('RecipeNo.RecipeName') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('RecipeNo.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('RecipeNo.ProductName') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('RecipeNo.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        Status
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in recipeNoList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.registrationNumber }}

                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.recipeName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.productName }}
                                                    </td>
                                                    <td class="text-center">
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <toggle-button
                                                            v-on:change="ChangeStatus(purchaseOrder.id, purchaseOrder.isActive)"
                                                            v-model="purchaseOrder.isActive" color="#3178F6" />
                                                    </td>
                                                    <td
                                                        v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1"
                                                            v-on:click="ViewRecipeNo(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')">
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
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
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
            recipeNoList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',


            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            }
        }
    },
    watch: {
        // search: function (val) {
        //     this.getData(val, 1, this.active);
        // }
    },
    methods: {
        search22: function () {
            this.getData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active);

        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        DeleteFile: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Batch/DeletePo', this.selected, { headers: { "Authorization": `Bearer ${token}` } })
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
                for (let i in this.recipeNoList) {
                    this.selected.push(this.recipeNoList[i].id);
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
            this.$https.get('/Batch/RecipeNoList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.recipeNoList = response.data.results;
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
                    root.$https.get('/Batch/DeleteRecipeNo?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
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
            var root =  this;
        //    this.$router.push('/AddRecipeNo');
            root.$router.push({
                            path: '/AddRecipeNo',
                            query: { 
                                Add: true, }
                        })
        },

        ChangeStatus: function (id, isActive) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/ChangeRecipeStatus?id=' + id + '&isActive=' + isActive, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: "Status Updated Successfully!",
                            type: 'success',
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        EditPurchaseOrder: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/RecipeNoDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddRecipeNo',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        ViewRecipeNo: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/RecipeNoDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/ViewRecipe',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        }
    },
    created() {
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        if (this.$route.query.data != undefined) {
            this.makeActive(this.$route.query.data);
        }
        else {
            this.makeActive("Draft");

        }
        //this.getData(this.search, 1);
    },
    updated: function () {
        if (this.selected.length < this.recipeNoList.length) {
            this.selectAll = false;
        } else if (this.selected.length == this.recipeNoList.length) {
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