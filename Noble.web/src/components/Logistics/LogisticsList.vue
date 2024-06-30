<template>
    <div class="row"
        v-if="(isValid('CanViewTransporter') && formName == 'Transporter') || (isValid('CanViewClearanceAgent') && formName == 'ClearanceAgent') || (isValid('CanViewShippingLiner') && formName == 'ShippingLinear')">


        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" v-if="formName == 'Transporter'">{{ $t('Logistics.Transporter/Cargo')
                                }}</h4>
                                <h4 class="page-title" v-if="formName == 'ClearanceAgent'">{{ $t('Logistics.ClearanceAgent')
                                }}</h4>
                                <h4 class="page-title" v-if="formName == 'ShippingLinear'">{{ $t('Logistics.ShippingLinear')
                                }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Logistics.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active" v-if="formName == 'Transporter'">{{
                                        $t('Logistics.Transporter/Cargo') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName == 'ClearanceAgent'">{{
                                        $t('Logistics.ClearanceAgent') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName == 'ShippingLinear'">{{
                                        $t('Logistics.ShippingLinear') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="(isValid('CanAddTransporter') && formName == 'Transporter') || (isValid('CanAddClearanceAgent') && formName == 'ClearanceAgent') || (isValid('CanAddShippingLiner') && formName == 'ShippingLinear')"
                                    v-on:click="Addlogistics" href="javascript:void(0);"
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
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('Logistics.SearchByLogistic')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1">

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
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>

                                    <th>#</th>
                                    <th>
                                        {{ $t('Logistics.Code') }}
                                    </th>
                                    <th v-if="english == 'true'">
                                        {{ $englishLanguage($t('Logistics.Name')) }}
                                    </th>
                                    <th v-if="isOtherLang()">
                                        {{ $arabicLanguage($t('Logistics.NameAr')) }}
                                    </th>

                                    <th>
                                        {{ $t('Logistics.DriverName') }}
                                    </th>
                                    <th>
                                        {{ $t('Logistics.ContactName') }}

                                    </th>
                                    <th>
                                        {{ $t('Logistics.ContactNumber') }}

                                    </th>

                                    <th v-if="formName == 'ClearanceAgent'">
                                        {{ $t('Logistics.ServiceFor') }}

                                    </th>
                                    <th v-if="formName == 'ClearanceAgent'">
                                        {{ $t('Logistics.Ports') }}

                                    </th>
                                    <th>
                                        {{ $t('Logistics.Status') }}
                                    </th>


                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(voucher, index) in logesticList" v-bind:key="voucher.id">

                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td
                                        v-if="(isValid('CanEditTransporter') && formName == 'Transporter') || (isValid('CanEditClearanceAgent') && formName == 'ClearanceAgent') || (isValid('CanEditShippingLiner') && formName == 'ShippingLinear')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="Editlogistics(voucher.id)">{{
                                                voucher.code }}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{ voucher.code }}
                                    </td>

                                    <td>
                                        {{ voucher.englishName }}
                                    </td>
                                    <td>
                                        {{ voucher.arabicName }}
                                    </td>
                                    <td>
                                        {{ voucher.xcoordinates }}
                                    </td>
                                    <td>
                                        {{ voucher.contactName }}
                                    </td>
                                    <td>
                                        {{ voucher.contactNo }}
                                    </td>
                                    <td v-if="formName == 'ClearanceAgent'">
                                        {{ voucher.serviceFor }}
                                    </td>
                                    <td v-if="formName == 'ClearanceAgent'">
                                        {{ getPort(voucher.ports) }}
                                    </td>
                                    <td>
                                        <span v-if="voucher.isActive" class="badge badge-boxed  badge-outline-success">{{
                                            $t('Logistics.Active') }}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{
                                            $t('Logistics.De-Active') }}</span>

                                    </td>


                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }} {{ currentPage
                        }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
    }}</span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{ (currentPage * 10) -
                            9 }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="getPage()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                            </b-pagination>
                        </div>
                    </div>

                </div>
            </div>

            <logisticsInvoice :formName="formName" :printId="printId"
                v-if="printId != undefined && printId != '00000000-0000-0000-0000-000000000000'" v-bind:key="printRender">

            </logisticsInvoice>
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
    props: ['formName'],
    data: function () {
        return {
            selected: [],
            selectAll: false,
            search: '',
            show: false,
            logesticList: [],
            type: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            currency: '',
            active: 'Draft',
            printDetails: [],
            printId: '00000000-0000-0000-0000-000000000000',
            printRender: 0,
            printed: false,
            arabic: '',
            english: '',

        }
    },

    methods: {
        search22: function () {
            this.GetlogisticsData(this.formName, this.search, this.currentPage);
        },

        clearData: function () {
            this.search = "";
            this.GetlogisticsData(this.formName, this.search, this.currentPage);

        },




        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        getPort: function (x) {
            if (this.$i18n.locale == 'ar') {
                if (x == 1) {
                    return 'ميناء جاف';
                }
                else if (x == 2) {
                    return 'الميناء البحري';
                }
                else if (x == 3) {
                    return 'مطار';
                } if (x == 4) {
                    return 'الميناء الجاف والميناء البحري';
                }
                else if (x == 5) {
                    return 'الميناء الجاف والميناء الجوي';
                }
                else if (x == 6) {
                    return 'الميناء البحري والميناء الجوي';
                }
                else if (x == 7) {
                    return 'الميناء الجاف والميناء البحري والميناء الجوي';
                }



            }
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                if (x == 1) {
                    return 'Dry Port';
                }
                else if (x == 2) {
                    return 'Sea Port';
                }
                else if (x == 3) {
                    return 'Air Port';
                } if (x == 4) {
                    return 'Dry Port & Sea Port';
                }
                else if (x == 5) {
                    return 'Dry Port & Air Port';
                }
                else if (x == 6) {
                    return 'Sea Port & Air Port';
                }
                else if (x == 7) {
                    return 'Dry Port,Sea Port & Air Port';
                }


            }
        },
        Addlogistics: function () {
            var root = this;
            var childFormName = this.formName;
            root.$router.push({
                path: '/AddLogistics',
                query: {
                    Add: true,
                    formName: childFormName
                }
            })
        },
        getPage: function () {

            this.GetlogisticsData(this.formName, this.search, this.currentPage);
        },
        GetlogisticsData: function (vtype, search, currentPage) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var branchId = localStorage.getItem('BranchId');

            search == undefined ? '' : search;
            root.$https.get('Region/LogisticList?logisticsForm=' + vtype + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isActive=false' + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.logesticList = response.data.results.logistics;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                }
            });
        },
        Editlogistics: function (id) {
            var root = this;
            var childFormName = this.formName;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


            root.$https.get('/Region/LogisticDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.$store.GetEditObj(response.data);
                    root.$router.push({
                        path: '/AddLogistics',
                        query: {
                            data: '',
                            Add: false,
                            formName: childFormName
                        }
                    })



                }
            });

        },

    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name + this.formName);
    },
    watch: {
        // search: function (val) {
        //     this.GetlogisticsData(this.formName, val, 1,);

        // },
        formName: function () {
            this.$emit('update:modelValue', this.$route.name + this.formName);
            this.GetlogisticsData(this.formName, this.search, 1);
        }
    },
    mounted: function () {

        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetlogisticsData(this.formName, this.search, 1);
        this.currency = localStorage.getItem('currency');
    }
}
</script>