<template>
    <div class="row"
        v-if="isValid('CanViewWarrantyCategory') || isValid('CanViewDescription') || isValid('CanViewProblem') || isValid('CanViewAccessory')">
        <div class="col-lg-12">

            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" v-if="formName == 'WarrantyCategory'">{{
                                    $t('ReparingOrder.WarrantyCategory') }}</h4>
                                <h4 class="page-title" v-if="formName == 'UpsDescription'">{{
                                    $t('ReparingOrder.UpsDescription') }}</h4>
                                <h4 class="page-title" v-if="formName == 'Problem'">{{ $t('ReparingOrder.Problem') }}</h4>
                                <h4 class="page-title" v-if="formName == 'AcessoryIncluded'">{{
                                    $t('ReparingOrder.AcessoryIncluded') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active" v-if="formName == 'WarrantyCategory'">{{
                                        $t('ReparingOrder.WarrantyCategory') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName == 'UpsDescription'">{{
                                        $t('ReparingOrder.UpsDescription') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName == 'Problem'">{{
                                        $t('ReparingOrder.Problem') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName == 'AcessoryIncluded'">{{
                                        $t('ReparingOrder.AcessoryIncluded') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddWarrantyCategory') || isValid('CanAddDescription') || isValid('CanAddProblem') || isValid('CanAddAccessory')"
                                    v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Categories.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-10" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('ReparingOrder.SearchbyName')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-2 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled ="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <!--<div class="card-header">
            <h4 class="card-title" v-if="formName=='WarrantyCategory'">{{ $t('ReparingOrder.WarrantyCategory') }}</h4>
            <h4 class="card-title" v-if="formName=='UpsDescription'">{{ $t('ReparingOrder.UpsDescription') }}</h4>
            <h4 class="card-title" v-if="formName=='Problem'">{{ $t('ReparingOrder.Problem') }}</h4>
            <h4 class="card-title" v-if="formName=='AcessoryIncluded'">{{ $t('ReparingOrder.AcessoryIncluded') }}</h4>

        </div>-->
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th class="text-center">#</th>

                                    <th v-if="english == 'true'" class="text-center">
                                        {{$englishLanguage($t('ReparingOrder.ReparingOrderName'))   }}
                                    </th>
                                    <th v-if="isOtherLang()" class="text-center">
                                        {{$arabicLanguage($t('ReparingOrder.ReparingOrderName'))   }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('ReparingOrder.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(reparingOrder, index) in reparingOrderlist" v-bind:key="reparingOrder.id">
                                    <td v-if="currentPage === 1" class="text-center">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else class="text-center">
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>


                                    <td v-if="english == 'true'" class="text-center">
                                        <strong
                                            v-if="isValid('CanEditWarrantyCategory') || isValid('CanEditDescription') || isValid('CanEditProblem') || isValid('CanEditAccessory')">
                                            <a href="javascript:void(0)" v-on:click="EditReparingOrder(reparingOrder.id)">
                                                {{ reparingOrder.name }}</a>
                                        </strong>
                                        <strong v-else>
                                            {{ reparingOrder.name }}
                                        </strong>

                                    </td>
                                    <td v-if="arabic == 'true'" class="text-center">
                                        <strong
                                            v-if="isValid('CanEditWarrantyCategory') || isValid('CanEditDescription') || isValid('CanEditProblem') || isValid('CanEditAccessory')">
                                            <a href="javascript:void(0)" v-on:click="EditReparingOrder(reparingOrder.id)">
                                                {{ reparingOrder.nameArabic }}</a>
                                        </strong>
                                        <strong v-else>
                                            {{ reparingOrder.nameArabic }}
                                        </strong>

                                    </td>


                                    <td class="text-center">
                                        <span v-if="reparingOrder.isActive"
                                            class="badge badge-boxed  badge-outline-success">{{ $t('ReparingOrder.Active') }}</span>
                                        <span v-else
                                            class="badge badge-boxed  badge-outline-danger">{{ $t('ReparingOrder.De-Active') }}</span>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }} {{ currentPage }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetReparingOrderData()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                            </b-pagination>
                        </div>
                    </div>

                </div>
            </div>

            <reparingOrdermodel :newReparingOrder="newReparingOrder" :show="show" :formName="formName" v-if="show"
                @close="IsSave" :type="type" />
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
export default {
    props: ['formName'],

    mixins: [clickMixin],
    data: function () {
        return {
            advanceFilters: false,
            arabic: '',
            english: '',
            searchQuery: '',
            show: false,
            reparingOrderlist: [],
            newReparingOrder: {
                id: '',
                name: '',
                nameArabic: '',
                description: '',
                reparingOrderTypes: '',
                code: '',
                isActive: true,
                branchId: '',
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
        }
    },
    watch: {
        // search: function (val) {
        //     this.GetReparingOrderData(val, 1, this.formName);
        // },
        formName: function () {
            this.search = '';
            this.GetReparingOrderData(this.search, 1, this.formName);
        }
    },
    methods: {

        search22: function () {
            this.GetReparingOrderData(this.search, this.currentPage, this.formName);

        },

        clearData: function () {
            this.search = "";
            this.GetReparingOrderData(this.search, this.currentPage, this.formName);


        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        IsSave: function () {

            this.show = false;
            this.search = '';
            this.GetReparingOrderData(this.search, this.currentPage);
        },
        getPage: function () {
            this.GetReparingOrderData(this.search, this.currentPage);
        },
        openmodel: function () {
            this.search = '';
            this.newReparingOrder = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                description: '',
                reparingOrderTypes: this.formName,
                isActive: true,
                branchId: '',

            }
            this.show = !this.show;
            this.type = "Add";
        },
        GetReparingOrderData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var branchId = localStorage.getItem('BranchId');

            root.$https.get('ReparingOrder/ReparingOrderTypeList?isDropdown=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&ReparingOrderTypes=' + this.formName + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {


                    root.reparingOrderlist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        EditReparingOrder: function (Id) {


            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/ReparingOrder/ReparingOrderTypeDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {

                        root.newReparingOrder.id = response.data.id;
                        root.newReparingOrder.reparingOrderTypes = response.data.reparingOrderTypes;
                        root.newReparingOrder.name = response.data.name;
                        root.newReparingOrder.nameArabic = response.data.nameArabic;
                        root.newReparingOrder.description = response.data.description;
                        root.newReparingOrder.code = response.data.code;
                        root.newReparingOrder.isActive = response.data.isActive;
                        root.show = !root.show;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        }
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.search = '';
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetReparingOrderData(this.search, 1);
    }
}
</script>