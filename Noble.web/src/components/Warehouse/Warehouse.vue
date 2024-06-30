<template>
    <div class="row" v-if="isValid('CanViewWareHouse') || isValid('Noble Admin')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Warehouse.WareHouse') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Warehouse.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Warehouse.WareHouse') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddWareHouse')" v-on:click="AddWarehouse" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Warehouse.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Warehouse.Close') }}
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
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('Warehouse.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class=" col-lg-4 mt-1">

<button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
    {{ $t('Sale.ClearFilter') }}
</button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th width="10%">
                                        {{ $t('Warehouse.Code') }}
                                    </th>
                                    <th v-if="english=='true'" width="20%">
                                        {{$englishLanguage($t('Warehouse.NameEn') ) }}
                                    </th>
                                    <th v-if="isOtherLang()" width="20%">
                                        {{$arabicLanguage($t('Warehouse.NameAr'))}}
                                    </th>

                                    <th width="25%">
                                        {{ $t('Warehouse.address') }}
                                    </th>

                                    <th width="20%">
                                        {{ $t('Warehouse.city') }}
                                    </th>
                                    <th width="10%">
                                        {{ $t('Warehouse.Active') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details,index) in resultQuery" v-bind:key="details.id">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td v-if="isValid('CanEditWareHouse') || isValid('Noble Admin')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditWarehouseInfo(details.id)">{{details.storeID}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        <strong>
                                            {{details.storeID}}
                                        </strong>
                                    </td>

                                    <td v-if="english=='true'">{{details.name}}</td>
                                    <td v-if="isOtherLang()">{{details.nameArabic}}</td>
                                    <td>{{details.address}}</td>
                                    <td>{{details.city}}</td>
                                    <td>
                                        <span v-if="details.isActive" class="badge badge-boxed  badge-outline-success">{{$t('Warehouse.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Warehouse.De-Active')}}</span>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />


                </div>
            </div>


        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],

        name: 'Warehouse',
        data: function () {
            return {
                advanceFilters: false,
                arabic: '',
                english: '',
                show: false,
                search: '',
                searchQuery: '',
                companyId: '00000000-0000-0000-0000-000000000000',
                warehouselist: [

                ]

            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {

                    return this.warehouselist.filter((cur) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => cur.name.toLowerCase().includes(v) || cur.nameArabic.toLowerCase().includes(v))
                    })
                } else {
                    return root.warehouselist;
                }
            },
        },
        methods: {

            search22: function () {
                this.GetWarehouseData(this.search, this.currentPage, this.active);
            },

            clearData: function () {
                this.search = "";
                this.GetWarehouseData(this.search, this.currentPage, this.active);

            },




            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            AddWarehouse: function () {
            //    this.$router.push('/AddWarehouse')
                this.$router.push({
                            path: '/AddWarehouse',
                            query: { 
                                Add: true,
                                  }
                        })
            },
            GetWarehouseData: function (comapnyId) {
                var root = this;
                var url = '/Company/GetWarehouseInformation?companyId=' + comapnyId + '&searchTerm=' + this.search;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.warehouselist = response.data.warehousesListModels;
                    }
                });
            },
            EditWarehouseInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/WarehouseDetailsViaId?Id=' + id + '&companyId=' + this.companyId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddWarehouse',
                            query: { data: '',
                                Add: false,
                                 companyId: root.$route.query.id }
                        })
                    }
                });

            },
            DeleteWarehouseInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/DeleteWarehouse?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.$swal.fire({
                            icon: 'warning',
                            title: 'Deleted Successfully',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.GetWarehouseData();
                    }
                });
            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.id != undefined) {
                this.companyId = this.$route.query.id;
                this.GetWarehouseData(this.$route.query.id);
            } else {
                this.GetWarehouseData('00000000-0000-0000-0000-000000000000');
            }
        }
    }
</script>